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
		//public int address;	//16bit address of data
		//public int bank;		//16bit bank number

        public NextAddress nextAddress;
        public TraceFile tf;
    }



	//was section in older code
	// One Tracefile per source code file
	public class TraceFile
	{
        // -------------------------------------------------------------------------------------------------
        // Constructor
        //
        // \param   _fn
        // The function.
        // -------------------------------------------------------------------------------------------------
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

            if (lines.Count <= 0) return null;

			for (int i=0;i<lines.Count-1;i++)
			{
				if (lines[i].nextAddress.GetLongAddress() == longaddr) // && lines[ i+1 ].address > addr)
					return (lines[i]);
			}

			return null;
		}


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Gets closest valid code address. </summary>
		///
		/// <remarks> 18/09/2018. </remarks>
		///
		/// <param name="addr"> The address. </param>
		/// <param name="bank"> The bank. </param>
		///
		/// <returns> The closest valid code address. </returns>
		/// -------------------------------------------------------------------------------------------------
		private LineData _GetCloestValidCodeAddress(int addr,int bank)
		{
            int longaddr = NextAddress.MakeLongAddress(bank, addr);


			LineData best = null;
			int bestdiff = int.MaxValue;


			if (lines.Count <= 0) return null;


			for (int i=0;i<lines.Count-1;i++)
			{
                int diff = Math.Abs(lines[i].nextAddress.GetLongAddress() - longaddr);
                //found exact
                if (diff == 0) return (lines[i]);

                if (diff < bestdiff && diff < 4)
                {
                    bestdiff = diff;
                    best = lines[i];

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

		private static Regex linenumberregex =
			new Regex(@"(?<line>[0-9]+)(:(?<colbegin>[0-9]+)(:(?<colend>[0-9]+))?)?");

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Parse trace data of line number, format: "line[:colbegin[:colend]]". </summary>
		///
		/// <remarks> 10/11/2018. </remarks>
		///
		/// <param name="s"> Data substring from the trace file. </param>
		///
		/// <returns> Integer tuple "line, colbegin, colend". Any 0 value means invalid string or missing value. </returns>
		/// -------------------------------------------------------------------------------------------------
		private static Tuple<int, int, int> ParseLineNumber(string s) {
			int line = 0, colbegin = 0, colend = 0;
			if (!string.IsNullOrEmpty(s)) {
				var match = linenumberregex.Match(s);
				if (match.Success) {
					line = int.Parse(match.Groups["line"].ToString());
					if (match.Groups["colbegin"].Success) {
						colbegin = int.Parse(match.Groups["colbegin"].ToString());
						if (match.Groups["colend"].Success) {
							colend = int.Parse(match.Groups["colend"].ToString());
						}
					}
				}
			}
			return new Tuple<int, int, int>(line, colbegin, colend);
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

			string filenameregexchars = @"[_a-zA-Z0-9\\., :/@#$%^(){}\[\]!+=~-]";
			Regex registersregex = new Regex(
				@"^(?<filename>" + filenameregexchars + @"*)\|"
				+ @"(?<line>[0-9:]+)\|"
				+ @"(?<macrofile>" + filenameregexchars + @"*)\|"
				+ @"(?<macroline>[0-9:]+)\|"
				+ @"(?<bank>-?[0-9]+)\|"
				+ @"(?<value>-?[0-9]+)\|"
				+ @"(?<type>[LFTDZ])\|"
				+ @"(?<label>.*)"
			);



			int index = 0;
			string[] lines = File.ReadAllLines(filename);

			foreach (string s in lines)
			{
				index++;
				if (!string.IsNullOrEmpty(s))
				{
					if (s.StartsWith("||")) {
						Console.WriteLine("# comment: " + s.Substring(2));
						continue;
					}
					if (s.StartsWith("|SLD.data.version|")) {
						var sldversion = s.Substring(18);
						if (!"0".Equals(sldversion)) {
							Console.WriteLine("# Unknown SLD version: " + sldversion);
							//TODO some warning about unsupported SLD version
						}
						continue;
					}
					var match = registersregex.Match(s);
					if (!match.Success) {
						Console.WriteLine("! matching failed for line: " + s);
						continue;
					}
                    Console.WriteLine(s);
					//Console.WriteLine(match.Groups["label"] + " " + match.Groups["address"] + " " + match.Groups["type"] + " " + match.Groups["section"]);

                    string mfn = match.Groups["macrofile"].ToString();
					var mline = ParseLineNumber(match.Groups["macroline"].ToString());

					string fn = match.Groups["filename"].ToString();
                    string label = match.Groups["label"].ToString();

					int bank = 0;
					int.TryParse(match.Groups["bank"].ToString(), NumberStyles.Integer, null, out bank);
					var line = ParseLineNumber(match.Groups["line"].ToString());
					int value = 0;
					int.TryParse(match.Groups["value"].ToString(), NumberStyles.Integer, null, out value);



					if (match.Groups["type"].ToString() == "T")     //Trace Data
					{
                        fn = Path.GetFileName(fn);

						TraceFile tracefile = TraceFile.FindTraceFile(fn);
						if (tracefile==null)
						{
							Console.WriteLine("Adding file "+fn);
							tracefile = new TraceFile( fn );
							traceFiles.Add(tracefile);
						}




						//found source level debug symbol
						LineData ld = new LineData();

						//ld.address = addr;
						//ld.bank = bank;
						ld.lineNumber = line.Item1-1;
                        ld.nextAddress = new NextAddress(value,bank);
                        ld.tf = tracefile;

						tracefile.lines.Add(ld);
					}
					else if (match.Groups["type"].ToString() == "L")        //Label
                    {
                        Labels.AddLabel(label,value,bank,false,false);
                    }
                    else if (match.Groups["type"].ToString() == "D")        //Define
					{
						Labels.AddLabel(label,value,bank,false,true);
					}
					else if (match.Groups["type"].ToString() == "F")        //Function
					{
						Labels.AddLabel(label,value,bank,true,false);
					}





				}
			}

		}


        
        // -------------------------------------------------------------------------------------------------
        // Gets by tab page
        //
        // \param   tab
        // The tab.
        //
        // \return  The by tab page.
        // -------------------------------------------------------------------------------------------------
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
					return l.nextAddress.GetAddr();


			}

			return -1;
		}







        // -------------------------------------------------------------------------------------------------
        // Goto line
        //
        // \param   pc
        // The PC.
        // -------------------------------------------------------------------------------------------------
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


        // -------------------------------------------------------------------------------------------------
        // Goto label
        //
        // \param   l
        // The l control.
        // -------------------------------------------------------------------------------------------------
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
                    return;

                }
			}



		}


        // -------------------------------------------------------------------------------------------------
        // Gets bank index
        //
        // \param   addr
        // The address.
        //
        // \return  The bank index.
        // -------------------------------------------------------------------------------------------------
		public static int GetBankIndex(int addr)
		{
			return ((addr >> 13) & 7);
		}


	}
}
