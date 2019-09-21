using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace RemoteDebugger.Main
{

	public class LineData
	{
		public int lineNumber;	//line number in source file
		public int address;	//16bit address of data
		public int bank;		//16bit bank number
	}



	//was section in older code
	// One Tracefile per source code file
	public class TraceFile
	{
		public TraceFile(string _fn)
		{
			filename = _fn;
		}


		public string filename;
		public List<LineData> lines = new List<LineData>();
		public SourceCodeView.CodeFile codefile = null;
		/// -------------------------------------------------------------------------------------------------
		/// <summary> Query if 'line' is line legal. </summary>
		///
		/// <remarks> 05/09/2018. </remarks>
		///
		/// <param name="line"> The line. </param>
		///
		/// <returns> True if line legal, false if not. </returns>
		/// -------------------------------------------------------------------------------------------------
		public bool IsLineLegal(int line)
		{
			foreach (LineData ld in lines)
			{
				if (ld.lineNumber == line) return true;
			}

			return false;
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets a line. </summary>
		///
		/// <remarks> 05/09/2018. </remarks>
		///
		/// <param name="line"> The line. </param>
		///
		/// <returns> The line. </returns>
		/// -------------------------------------------------------------------------------------------------
		public LineData GetLine(int line)
		{
			foreach (LineData ld in lines)
			{
				if (ld.lineNumber == line) return ld;
			}

			return null;

		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Does file have address. </summary>
		///
		/// <remarks> 05/09/2018. </remarks>
		///
		/// <param name="addr"> The address. </param>
		///
		/// <returns> A LineData. </returns>
		/// -------------------------------------------------------------------------------------------------
		public LineData DoesFileHaveAddress(int addr,int bank)
		{
			LineData best = null;
			//uint diff = uint.MaxValue;

			if (lines.Count <= 0) return null;


			for (int i=0;i<lines.Count-1;i++)
			{
				if (lines[i].address == addr && lines[i].bank == bank) // && lines[ i+1 ].address > addr)
					return (lines[i]);


			}



			return null;
		}


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets cloest valid code address. </summary>
		///
		/// <remarks> 18/09/2018. </remarks>
		///
		/// <param name="addr"> The address. </param>
		/// <param name="bank"> The bank. </param>
		///
		/// <returns> The cloest valid code address. </returns>
		/// -------------------------------------------------------------------------------------------------
		private LineData _GetCloestValidCodeAddress(int addr,int bank)
		{
			LineData best = null;
			int bestdiff = int.MaxValue;


			if (lines.Count <= 0) return null;


			for (int i=0;i<lines.Count-1;i++)
			{
				if (lines[i].bank == bank)
				{
					int diff = Math.Abs(lines[i].address - addr);
					//found exact
					if (diff == 0) return (lines[i]);

					if (diff < bestdiff && diff < 4)
					{
						bestdiff = diff;
						best = lines[i];

					}
				}
			}


			return best;
		}
		
		
		


























		public static List<TraceFile> traceFiles;


		public static TraceFile FindTraceFile(string filename)
		{
			TraceFile result;
			try
			{
				result = traceFiles.First(s => s.filename == filename);

			}
			catch
			{
				result = null;
			}

			return (result);
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Parse trace data. </summary>
		///
		/// <remarks> 06/09/2018. </remarks>
		///
		/// <param name="filename"> Filename of the file. </param>
		/// -------------------------------------------------------------------------------------------------
		public static void ParseTraceData(string filename)
		{
			//z80.asm|25|5|32828
			traceFiles = new List<TraceFile>();


			Regex registersregex = new Regex(@"^(?<filename>[_a-zA-Z0-9\\.]*)\|(?<line>[0-9]*)\|(?<bank>[0-9]*)\|(?<addr>[0-9]*)\|(?<type>[LFT])");

			string[] lines = File.ReadAllLines(filename);

			foreach (string s in lines)
			{
				if (!string.IsNullOrEmpty(s))
				{
					var match = registersregex.Match(s);
					//Console.WriteLine(match.Groups["label"] + " " + match.Groups["address"] + " " + match.Groups["type"] + " " + match.Groups["section"]);

					string fn = match.Groups["filename"].ToString();
					int bank = 0;
					int.TryParse(match.Groups["bank"].ToString(), NumberStyles.Integer, null, out bank);
					int line = 0;
					int.TryParse(match.Groups["line"].ToString(), NumberStyles.Integer, null, out line);
					int addr = 0;
					int.TryParse(match.Groups["addr"].ToString(), NumberStyles.Integer, null, out addr);

					if (match.Groups["type"].ToString() == "T")
					{
						TraceFile tracefile = TraceFile.FindTraceFile(fn);
						if (tracefile==null)
						{
							Console.WriteLine("Adding file "+fn);
							tracefile = new TraceFile( fn );
							traceFiles.Add(tracefile);
						}




						//found source level debug symbol
						LineData ld = new LineData();

						ld.address = addr;
						ld.bank = bank;
						ld.lineNumber = line-1;


						tracefile.lines.Add(ld);
					}
					else if (match.Groups["type"].ToString() == "L")
					{
						Labels.AddLabel(fn,addr,bank,false);
					}
					else if (match.Groups["type"].ToString() == "F")
					{
						Labels.AddLabel(fn,addr,bank,true);
					}





				}
			}

		}


		private static int CurrentExecuteLine = 0;
		private static TraceFile CurrentExecuteFile = null;
		/// -------------------------------------------------------------------------------------------------
		/// <summary> Sets a PC. </summary>
		///
		/// <remarks> 06/09/2018. </remarks>
		///
		/// <param name="pc"> The PC. </param>
		/// -------------------------------------------------------------------------------------------------
		public static void SetPC(int pc,bool focus = false)
		{

			if (CurrentExecuteFile != null)
			{
				if (CurrentExecuteFile.codefile != null && CurrentExecuteFile.codefile.codewindow!=null)
				{
					Line line = CurrentExecuteFile.codefile.codewindow.Lines[CurrentExecuteLine];
					if (line != null)
						line.MarkerDelete(SourceCodeView.EXECUTE_MARKER);
				}
				CurrentExecuteFile = null;
				CurrentExecuteLine = 0;


			}

			if (traceFiles == null) return;


			int bank = MainForm.banks[ GetBankIndex(pc) ];

			foreach (TraceFile t in traceFiles)
			{
				LineData ld = t.DoesFileHaveAddress(pc,bank);
				if (ld != null)
				{

					CurrentExecuteFile = t;
					CurrentExecuteLine = ld.lineNumber;
					var line = t.codefile.codewindow.Lines[CurrentExecuteLine];
					line.MarkerAdd(SourceCodeView.EXECUTE_MARKER);


					if (focus)
						MainForm.mySourceWindow.FocusLine(t.codefile, ld.lineNumber); 

				}
			}




/*			if (CurrentExecuteLine>=0)
			{
				Section s = FindSection(CurrentExecuteSection);
				CodeFile cf = GetCodeFileFromSection(CurrentExecuteSection);

				var line = cf.codewindow.Lines[CurrentExecuteLine];
				line.MarkerDelete(EXECUTE_MARKER);

				CurrentExecuteLine = -1;
			}



			foreach (Section s in Sections)
			{
				LineData ld = s.DoesSectionHaveAddress(address);
				if (ld != null)
				{
					CodeFile cf = GetCodeFileFromSection(s.section);

					var line = cf.codewindow.Lines[(int)ld.lineNumber];
					line.MarkerAdd(EXECUTE_MARKER);

					CurrentExecuteLine = (int)ld.lineNumber;
					CurrentExecuteSection = s.section;

					Console.WriteLine("Focus Line!");
					Form1.Instance.SourceTab.SelectedTab = cf.tab;
					cf.codewindow.Lines[(int)ld.lineNumber].EnsureVisible();

					int linesOnScreen = cf.codewindow.LinesOnScreen - 2; // Fudge factor

					int linenum = (int)ld.lineNumber;
					var start = cf.codewindow.Lines[linenum - (linesOnScreen / 2)].Position;
					var end = cf.codewindow.Lines[linenum + (linesOnScreen / 2)].Position;

					cf.codewindow.ScrollRange(start, end);

					//Form1.Instance.FocusOnFile(s, ld.lineNumber);

					return;
				}
			}*/


		}



		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets cloest valid code address. </summary>
		///
		/// <remarks> 18/09/2018. </remarks>
		///
		/// <param name="addr"> The address. </param>
		/// <param name="bank"> The bank. </param>
		///
		/// <returns> The cloest valid code address. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static int GetCloestValidCodeAddress(int address)
		{

			int bank = MainForm.banks[ GetBankIndex(address) ];

			foreach (TraceFile t in traceFiles)
			{

				LineData l = t._GetCloestValidCodeAddress(address,bank);
				if (l != null)
					return l.address;


			}

			return -1;
		}







		public static void GotoLine(int pc)
		{
			if (traceFiles == null) return;


			int bank = MainForm.banks[ GetBankIndex(pc) ];




			foreach (TraceFile t in traceFiles)
			{
				LineData ld = t.DoesFileHaveAddress(pc,bank);
				if (ld != null)
				{
					var line = t.codefile.codewindow.Lines[CurrentExecuteLine];

					MainForm.mySourceWindow.FocusLine(t.codefile, ld.lineNumber); 

				}
			}



		}


		public static void GotoLabel(Labels.Label l)
		{
			if (traceFiles == null) return;


			foreach (TraceFile t in traceFiles)
			{
				LineData ld = t.DoesFileHaveAddress(l.address,l.bank);
				if (ld != null)
				{
					var line = t.codefile.codewindow.Lines[CurrentExecuteLine];

					MainForm.mySourceWindow.FocusLine(t.codefile, ld.lineNumber); 

				}
			}



		}


		public static int GetBankIndex(int addr)
		{
			return ((addr >> 13) & 7);
		}


	}
}
