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

        public NextAddress nextAddress;
        public TraceFile tf;
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
            int longaddr = NextAddress.MakeLongAddress(bank, addr);

			LineData best = null;

			if (lines.Count <= 0) return null;


			for (int i=0;i<lines.Count-1;i++)
			{
				if (lines[i].nextAddress.GetLongAddress() == longaddr) // && lines[ i+1 ].address > addr)
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
            int longaddr = NextAddress.MakeLongAddress(bank, addr);


			LineData best = null;
			int bestdiff = int.MaxValue;


			if (lines.Count <= 0) return null;


			for (int i=0;i<lines.Count-1;i++)
			{
				//if (lines[i].bank == bank)
				//{
					int diff = Math.Abs(lines[i].nextAddress.GetLongAddress() - longaddr);
					//found exact
					if (diff == 0) return (lines[i]);

					if (diff < bestdiff && diff < 4)
					{
						bestdiff = diff;
						best = lines[i];

					}
				//}
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


			Regex registersregex = new Regex(@"^(?<filename>[_a-zA-Z0-9\\. :-]*)\|(?<line>[0-9]*)\|(?<bank>[0-9]*)\|(?<addr>[0-9]*)\|(?<type>[LFT])");

			string[] lines = File.ReadAllLines(filename);

			foreach (string s in lines)
			{
				if (!string.IsNullOrEmpty(s))
				{
                    Console.WriteLine(s);
					var match = registersregex.Match(s);
					//Console.WriteLine(match.Groups["label"] + " " + match.Groups["address"] + " " + match.Groups["type"] + " " + match.Groups["section"]);

					string fn = match.Groups["filename"].ToString();
                    fn = Path.GetFileName(fn);

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
                        ld.nextAddress = new NextAddress(addr,bank);
                        ld.tf = tracefile;

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


        
        public static TraceFile GetByTabPage(TabPage tab)
        {
            foreach (TraceFile t in traceFiles)
            {
                if (t.codefile.tab == tab)
                    return t;
            }

            return null;
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
		public static void SetPC(int pc,int bank,bool focus = false)
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



			foreach (TraceFile t in traceFiles)
			{
				LineData ld = t.DoesFileHaveAddress(pc,bank);
				if (ld != null)
                {

                    MainForm.sourceCodeView.UpdateMarginAddress(t);

					CurrentExecuteFile = t;
					CurrentExecuteLine = ld.lineNumber;
					Line line = t.codefile.codewindow.Lines[CurrentExecuteLine];
					line.MarkerAdd(SourceCodeView.EXECUTE_MARKER);


					if (focus)
						MainForm.mySourceWindow.FocusLine(t.codefile, ld.lineNumber); 

				}
			}


		}


        // -------------------------------------------------------------------------------------------------
        // Focus address
        //
        // \param   addr    The address.
        // \param   bank    The bank.
        // -------------------------------------------------------------------------------------------------
        public static void FocusAddr(int addr,int bank)
        {

            if (traceFiles == null) return;

            foreach (TraceFile t in traceFiles)
            {
                LineData ld = t.DoesFileHaveAddress(addr,bank);
                if (ld != null)
                {
                    MainForm.sourceCodeView.UpdateMarginAddress(t);
                    MainForm.mySourceWindow.FocusLine(t.codefile, ld.lineNumber); 

                }
            }


        }


        // -------------------------------------------------------------------------------------------------
        // Gets line datafrom address
        //
        // \param   na  The na.
        //
        // \return  The line datafrom address.
        // -------------------------------------------------------------------------------------------------
        public static LineData GetLineDatafromAddr(NextAddress na)
        {
            foreach (TraceFile t in traceFiles)
            {
                LineData ld = t.DoesFileHaveAddress(na.GetAddr(),na.GetBank());
                if (ld != null)
                {
                    return ld;
                }
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
				LineData ld = t.DoesFileHaveAddress(l.nextAddress.GetAddr(),l.nextAddress.GetBank());
				if (ld != null)
				{
					//var line = t.codefile.codewindow.Lines[CurrentExecuteLine];

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
