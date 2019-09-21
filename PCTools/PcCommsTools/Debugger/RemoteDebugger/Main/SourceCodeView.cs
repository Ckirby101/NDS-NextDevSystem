using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace RemoteDebugger.Main
{
	public class SourceCodeView
	{

		public class CustomMenuItem : MenuItem
		{
			public object value;

			public CustomMenuItem(string text, EventHandler onClick , object val) : base(text, onClick)
			{
				value = val;
			}
		}

		private bool MarginClicked = false;
		
		public const int NUMBER_MARGIN = 1;
		//private const int BOOKMARK_MARKER = 2;
		public const int BREAKPOINT_MARGIN = 2;
		public const int CODE_MARGIN = 3;

		
		public const int CODE_MARKER = 4;
		public const int BREAKPOINT_MARKER = 5;
		public const int EXECUTE_MARKER = 6;
		public const int BACK_COLOR = 0x2A211C;
		public const int FORE_COLOR = 0xB7B7B7;


		public Scintilla HoverTipScintilla;
		public int Hoverpos;
		public Labels.Label HoverLabel;

		/// -------------------------------------------------------------------------------------------------
		/// <summary> A code file. </summary>
		///
		/// <remarks> 05/09/2018. </remarks>
		/// -------------------------------------------------------------------------------------------------
		public class CodeFile
		{
			public string TraceFileName;
			public string path;
			public Scintilla codewindow;
			public TabPage tab;


			public bool IsLineLegal(int line)
			{
				TraceFile s = TraceFile.FindTraceFile(TraceFileName);

				return s.IsLineLegal(line);
			}

			public LineData GetLine(int line)
			{
				TraceFile s = TraceFile.FindTraceFile(TraceFileName);

				return s.GetLine(line);
			}


		}


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Initializes the source files. </summary>
		///
		/// <remarks> 05/09/2018. </remarks>
		///
		/// <exception cref="get">  Thrown when a get error condition occurs. </exception>
		/// <exception cref="true"> Thrown when a true error condition occurs. </exception>
		///
		/// <param name="traceFiles"> The trace files. </param>
		/// <param name="tab">		  The tab. </param>
		/// <param name="path">		  Full pathname of the file. </param>
		/// -------------------------------------------------------------------------------------------------
		public void initSourceFiles(TraceFile[] traceFiles,TabControl tab ,string path)
		{


			foreach (TraceFile s in traceFiles)
			{
				//if (s.lines.Count>0)
				{

					string text = File.ReadAllText(path+"\\"+s.filename);
					if (!string.IsNullOrEmpty(text))
					{

						CodeFile cf = new CodeFile();
						cf.TraceFileName = s.filename;


						var page = new TabPage(s.filename);
						cf.tab = page;
						cf.codewindow = new ScintillaNET.Scintilla(); //new RichTextBox();
						cf.codewindow.Dock = DockStyle.Fill;
						cf.codewindow.Tag = (object)s.filename;
						page.Controls.Add(cf.codewindow);
						tab.TabPages.Add(page);
						//string text = File.ReadAllText("C:\\Users\\chris kirby\\Documents\\Spectrum Next\\bombjack\\game.c");
						cf.codewindow.Text = text;// LoadFile("C:\\Users\\chris kirby\\Documents\\Spectrum Next\\bombjack\\game.c",RichTextBoxStreamType.PlainText);
						page.Select();

						cf.codewindow.SetSelectionBackColor(true, IntToColor(0x114D9C));

						// Configure the default style
						cf.codewindow.StyleResetDefault();
						cf.codewindow.Styles[Style.Default].Font = "Consolas";
						cf.codewindow.Styles[Style.Default].Size = 10;
						cf.codewindow.Styles[Style.Default].BackColor = IntToColor(0x272822);
						cf.codewindow.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
						cf.codewindow.StyleClearAll();

						// Configure the CPP (C#) lexer styles




						cf.codewindow.Styles[Style.Asm.Operator].ForeColor = IntToColor(0x48A8EE);
						cf.codewindow.Styles[Style.Asm.Identifier].ForeColor = IntToColor(0x67d9ff);

						
						//cf.codewindow.Styles[Style.Asm.Identifier].ForeColor = IntToColor(0xD0DAE2);
						cf.codewindow.Styles[Style.Asm.Comment].ForeColor = Color.ForestGreen;
						cf.codewindow.Styles[Style.Asm.CommentDirective].ForeColor = Color.ForestGreen;
						//cf.codewindow.Styles[Style.Cpp.CommentLine].ForeColor = Color.ForestGreen;

						//cf.codewindow.Styles[Style.Asm.CommentDoc].ForeColor = IntToColor(0x2FAE35);
						cf.codewindow.Styles[Style.Asm.Number].ForeColor = IntToColor(0x60b48a);
						cf.codewindow.Styles[Style.Asm.String].ForeColor = Color.AliceBlue;
						cf.codewindow.Styles[Style.Asm.Character].ForeColor = Color.Aqua;
						//cf.codewindow.Styles[Style.Asm.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
						//cf.codewindow.Styles[Style.Asm.Operator].ForeColor = IntToColor(0xE0E0E0);
						//cf.codewindow.Styles[Style.Asm.Regex].ForeColor = IntToColor(0xff00ff);
						//cf.codewindow.Styles[Style.Asm.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
						//cf.codewindow.Styles[Style.Asm.Word].ForeColor = IntToColor(0x48A8EE);
						//cf.codewindow.Styles[Style.Asm.Word2].ForeColor = IntToColor(0xF98906);
						//cf.codewindow.Styles[Style.Asm.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
						//cf.codewindow.Styles[Style.Asm.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
						//cf.codewindow.Styles[Style.Asm.GlobalClass].ForeColor = IntToColor(0x48A8EE);

						cf.codewindow.Lexer = Lexer.Asm;
						//cf.codewindow.SetKeywords(1, "db dm ds dw defb defw ");
						//cf.codewindow.Styles[Style.Asm.MathInstruction].ForeColor = Color.AntiqueWhite;

						cf.codewindow.SetKeywords(2, "hl de bc a b c h l d e hl' de' bc' af af' sp ix iy ixl ixh iyl iyh");
						cf.codewindow.Styles[Style.Asm.Register].ForeColor = IntToColor(0xdfaf8f);

						cf.codewindow.SetKeywords(3, "macro endm setbank ord pcorg equ include incbin savebin message stack end defl pc align rb rw db dw defb defw hex");
						cf.codewindow.Styles[Style.Asm.Directive].ForeColor = IntToColor(0x8ca4dc);


						cf.codewindow.SetKeywords(0, "nop inc dec ex exx djnz rrca rla jr jp call cpl scf mul halt ld add sub nextreg adc sbc and or xor cp test ret rst out in push pop swapnib ldir ldirx lddrx lddrx ldpirx ldirscale ldws mirror pixeldn pixelad setae outinb");
						cf.codewindow.Styles[Style.Asm.CpuInstruction].ForeColor = IntToColor(0xc15c95);


						cf.codewindow.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
						cf.codewindow.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
						cf.codewindow.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
						cf.codewindow.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);



						//line number space
						var nums = cf.codewindow.Margins[NUMBER_MARGIN];
						nums.BackColor = Color.Black;
						nums.Width = 30;
						nums.Type = MarginType.Number;
						nums.Sensitive = true;
						nums.Mask = 0;


						//addr space
						var cmargin = cf.codewindow.Margins[CODE_MARGIN];
						cmargin.Width = 60;
						cmargin.BackColor = Color.Black;
						cmargin.Sensitive = true;
						cmargin.Type = MarginType.Text;
						cmargin.Mask = (1 << CODE_MARKER);


						//breakpoint space
						var cpmargin = cf.codewindow.Margins[BREAKPOINT_MARGIN];
						cpmargin.BackColor = Color.Black;
						cpmargin.Width = 10;
						cpmargin.Sensitive = true;
						cpmargin.Type = MarginType.Symbol;
						cpmargin.Mask = (1 << BREAKPOINT_MARKER);


						var bpmarker = cf.codewindow.Markers[BREAKPOINT_MARKER];
						bpmarker.Symbol = MarkerSymbol.Circle;
						bpmarker.SetBackColor(IntToColor(0xFF003B));
						bpmarker.SetForeColor(IntToColor(0x000000));
						bpmarker.SetAlpha(100);



						cf.codewindow.MarginClick += TextArea_MarginClick;

						cf.codewindow.Click += Codewindow_Click;
						cf.codewindow.DwellStart += Codewindow_Dwell;
						cf.codewindow.DwellEnd += Codewindow_DwellEnd;
						cf.codewindow.MouseDwellTime = 1000;

						cf.codewindow.MouseDown += Codewindow_MouseDown;




						//
						//var margin = cf.codewindow.Margins[BOOKMARK_MARGIN];
						//margin.Width = 10;
						//margin.Sensitive = true;
						//margin.Type = MarginType.Symbol;
						//margin.Mask = (1 << BOOKMARK_MARKER);
						//margin.Cursor = MarginCursor.Arrow;





						//var marker = cf.codewindow.Markers[BOOKMARK_MARKER];
						//marker.Symbol = MarkerSymbol.Circle;
						//marker.SetBackColor(IntToColor(0xFF003B));
						//marker.SetForeColor(IntToColor(0x000000));
						//marker.SetAlpha(100);





						//margin.Cursor = MarginCursor.Arrow;

						//var cmarker = cf.codewindow.Markers[CODE_MARKER];
						//cmarker.Symbol = MarkerSymbol.n;
						//cmarker.SetBackColor(IntToColor(0xa06000));
						//cmarker.SetForeColor(IntToColor(0x000000));
						//cmarker.SetAlpha(100);





						//bpmarker.Symbol = MarkerSymbol.Background;
						//bpmarker.SetBackColor(Color.DarkRed);

						var ecmarker = cf.codewindow.Markers[EXECUTE_MARKER];
						ecmarker.Symbol = MarkerSymbol.RoundRect;
						ecmarker.SetBackColor(Color.DarkBlue);
						ecmarker.SetAlpha(100);


						


						foreach (LineData ld in s.lines)
						{
							var line = cf.codewindow.Lines[ld.lineNumber];
							line.MarginText = ld.bank+" $"+ld.address.ToString("x4");

							//line.MarkerAdd(CODE_MARKER);
							//line.MarkerAdd(BREAKPOINT_MARKER);


							//current line marke works great
							//line.MarkerAdd(EXECUTE_MARKER);

						}

						s.codefile = cf;

						//codefiles.Add(cf);

					}


				}

			}


		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Event handler. Called by Codewindow for mouse down events. </summary>
		///
		/// <remarks> 18/09/2018. </remarks>
		///
		/// <param name="sender"> Source of the event. </param>
		/// <param name="e">	  Mouse event information. </param>
		/// -------------------------------------------------------------------------------------------------
		private void Codewindow_MouseDown(object sender, 
			System.Windows.Forms.MouseEventArgs e)
		{
			Scintilla s = (Scintilla)sender;

			if (e.Button == MouseButtons.Right)
			{
				ContextMenu cm = new ContextMenu();

				int position = s.CharPositionFromPoint(e.X, e.Y);


				int linenum = s.LineFromPosition(position);
				var line = s.Lines[linenum];

				TraceFile tf = TraceFile.FindTraceFile((string)s.Tag);
				if (tf != null)
				{
					LineData ld = tf.GetLine(linenum);
					string word = s.GetWordFromPosition(position);

					//step mode and on valid line add a set pc option
					if (tf.IsLineLegal(linenum) && Program.InStepMode)
					{
						cm.MenuItems.Add(new CustomMenuItem( "Set PC to $"+ld.address.ToString("X4"),new EventHandler(ContextSetPC),(object)ld.address ) );
					}

					if (!string.IsNullOrEmpty(word))
					{
						Labels.Label l = Labels.FindLabel(word);
						if (l != null)
						{
							if (!l.function)
							{
								cm.MenuItems.Add(new CustomMenuItem( "Add Variable "+l.label+" to Watch",new EventHandler(ContextAddToWatch),(object)l ) );
							}
						}

					}


					cm.MenuItems.Add("item2");
					//ContextMenu cm = new ContextMenu();
					//{
					//	MenuItem mi = new MenuItem("coming soon2 "+word);//  ,   (s, ea) => this.UndoRedo.Undo());
					//	cm.MenuItems.Add(mi);
					//}
					tf.codefile.codewindow.ContextMenu = cm;

				}


			}

			Console.WriteLine("hello");
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Context add to watch. </summary>
		///
		/// <remarks> 19/09/2018. </remarks>
		///
		/// <param name="sender"> Source of the event. </param>
		/// <param name="e">	  Event information. </param>
		/// -------------------------------------------------------------------------------------------------
		private void ContextAddToWatch(object sender, EventArgs e)
		{
			CustomMenuItem customMenuItem = sender as CustomMenuItem;

			Labels.Label l = (Labels.Label) customMenuItem.value;

			if (l != null)
			{
				MainForm.myWatches.AddWatchLabel(l);

			}

			//CustomMenuItem customMenuItem = sender as CustomMenuItem;
			//int pc = (int) customMenuItem.value;

			//Console.WriteLine("hello "+pc.ToString("X4"));

		}
		/// -------------------------------------------------------------------------------------------------
		/// <summary> Context set PC. </summary>
		///
		/// <remarks> 19/09/2018. </remarks>
		///
		/// <param name="sender"> Source of the event. </param>
		/// <param name="e">	  Event information. </param>
		/// -------------------------------------------------------------------------------------------------
		private void ContextSetPC(object sender, EventArgs e)
		{
			CustomMenuItem customMenuItem = sender as CustomMenuItem;
			int pc = (int) customMenuItem.value;


			MainForm.myNewRegisters.SetRegister(pc,Registers.Z80Register.pc);


			Console.WriteLine("hello "+pc.ToString("X4"));

		}
		/// -------------------------------------------------------------------------------------------------
		/// <summary> Event handler. Called by Codewindow for dwell end events. </summary>
		///
		/// <remarks> 10/09/2018. </remarks>
		///
		/// <param name="sender"> Source of the event. </param>
		/// <param name="e">	  Dwell event information. </param>
		/// -------------------------------------------------------------------------------------------------
		private void Codewindow_DwellEnd(object sender, DwellEventArgs e)
		{
			Scintilla s = (Scintilla)sender;

			if (s.CallTipActive)
			{
				s.CallTipCancel();

			}
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Event handler. Called by Codewindow for dwell events. </summary>
		///
		/// <remarks> 10/09/2018. </remarks>
		///
		/// <param name="sender"> Source of the event. </param>
		/// <param name="e">	  Dwell event information. </param>
		/// -------------------------------------------------------------------------------------------------
		private void Codewindow_Dwell(object sender, DwellEventArgs e)
		{

			Scintilla s = (Scintilla)sender;

			if (s.CallTipActive) return;

			TraceFile tf = TraceFile.FindTraceFile((string)s.Tag);
			if (tf != null)
			{

				//tf.codefile.codewindow.sh
				string word = s.GetWordFromPosition(e.Position);

				DoHoverTip(s, e.Position, word);


			}
		}



		/// -------------------------------------------------------------------------------------------------
		/// <summary> Executes the hover tip operation. </summary>
		///
		/// <remarks> 10/09/2018. </remarks>
		///
		/// <param name="s">    A Scintilla to process. </param>
		/// <param name="pos">  The position. </param>
		/// <param name="word"> The word. </param>
		/// -------------------------------------------------------------------------------------------------
		public void DoHoverTip(Scintilla s, int pos, string word)
		{
			HoverTipScintilla = s;
			Hoverpos = pos;

			HoverLabel = Labels.FindLabel(word);

			//string tip = word;
			if (HoverLabel != null)
			{
				if (HoverLabel.function)
				{
					//its a function label
					HoverTipScintilla.CallTipShow(Hoverpos,"Function :"+HoverLabel.label+" @ $"+HoverLabel.address.ToString("X4"));


				}
				else
				{
					Program.telnetConnection.SendCommand("read-memory "+HoverLabel.address.ToString()+" 2", HoverCallback);
				}





				//found a label
				//tip = tip + " $" + HoverLabel.address.ToString("X4");
			}
			else
			{
				//no label lets not display anything
				//s.CallTipShow(pos,tip);
			}



		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Callback, called when the hover. </summary>
		///
		/// <remarks> 10/09/2018. </remarks>
		///
		/// <param name="response"> The response. </param>
		/// <param name="tag">	    The tag. </param>
		/// -------------------------------------------------------------------------------------------------
		void HoverCallback(string[] response,int tag)
		{
			HoverTipScintilla.BeginInvoke(new MethodInvoker(delegate
			{
				int value;
				if (int.TryParse(response[0], NumberStyles.HexNumber, null, out value))
				{
					value = MainForm.Endian(value);
					int val8 = (value & 0xff);
					int val16 = (value & 0xffff);
				

					HoverTipScintilla.CallTipShow(Hoverpos,"Var :"+HoverLabel.label+" @ $"+HoverLabel.address.ToString("X4")+"  mem:"+response[0]+"  8:$"+val8.ToString("X2")+" / "+val8+"    16:$"+val16.ToString("X4")+" / "+val16);


				}


			}));

		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Event handler. Called by Codewindow for click events. </summary>
		///
		/// <remarks> 10/09/2018. </remarks>
		///
		/// <param name="sender"> Source of the event. </param>
		/// <param name="e">	  Event information. </param>
		/// -------------------------------------------------------------------------------------------------
		private void Codewindow_Click(object sender, EventArgs e)
		{
			if (MarginClicked)
			{
				MarginClicked = false;
				return;
			}


		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Event handler. Called by TextArea for margin click events. </summary>
		///
		/// <remarks> 19/09/2018. </remarks>
		///
		/// <param name="sender"> Source of the event. </param>
		/// <param name="e">	  Margin click event information. </param>
		/// -------------------------------------------------------------------------------------------------
		private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
		{
			if (!Program.InStepMode) return;

			MarginClicked = true;

			//Console.WriteLine("TextArea_MarginClick");




			Scintilla s = (Scintilla)sender;

			if (e.Margin == CODE_MARGIN || e.Margin == BREAKPOINT_MARGIN)
			{
				const uint mask = (1 << BREAKPOINT_MARKER);
				int linenum = s.LineFromPosition(e.Position);
				var line = s.Lines[linenum];

				TraceFile tf = TraceFile.FindTraceFile((string)s.Tag);
				//Section sec = FindSection((string)s.Tag);

				if (tf !=null && tf.IsLineLegal(linenum))
				{
					//Console.WriteLine("Line Ok");
					LineData ld = tf.GetLine(linenum);

					//Form1.commsthread.AddCommand(Command.disassmblememory, 50, " " + ld.Addess.ToString("X4") + "H 256");


					//now request memory and display is asm window


					if ((line.MarkerGet() & mask) > 0)
					{
						// Remove existing breakpoint
						//line.MarkerDelete(BOOKMARK_MARKER);
						//line.MarkerDelete(BREAKPOINT_MARKER);
						if (Breakpoint.RemoveBreakPointAtAddress(ld.address))
						{

						}


						//Form1.commsthread.AddCommand(Command.direct, 76, "disable-breakpoint 1");


					}
					else
					{
						// Add breakpoint
						if (Breakpoint.SetBreakPoint(ld.address, Breakpoint.BreakpointType.PC,
							"PC=" + ld.address.ToString("X4") + "H", tf.filename, linenum))
						{

						}
						// 
						// 
						// 
						//if (Program.AddBreakpoint(ld.address))
						//{
						//	line.MarkerAdd(BREAKPOINT_MARKER);

						//}
						// 
						//line.MarkerAdd(BOOKMARK_MARKER);

						//LineData ld = cf.GetLine(linenum);

						//Form1.commsthread.AddCommand(Command.setbreakpointaction, 76, " 1 break");
						//Form1.commsthread.AddCommand(Command.setbreakpoint, 75, " 1 PC="+ld.Addess.ToString("x4")+"H");
						//Form1.commsthread.AddCommand(Command.direct, 74, "enable-breakpoint 1");

					}


				}

			}


/*			if (e.Margin == BOOKMARK_MARGIN)
			{

				CodeFile cf = GetCodeFileFromSection((string)s.Tag);



				// Do we have a marker for this line?
				const uint mask = (1 << BOOKMARK_MARKER);
				int linenum = s.LineFromPosition(e.Position);
				var line = s.Lines[linenum];


				if (cf.IsLineLegal(linenum))
				{
					if ((line.MarkerGet() & mask) > 0)
					{
						// Remove existing breakpoint
						line.MarkerDelete(BOOKMARK_MARKER);
						line.MarkerDelete(BREAKPOINT_MARKER);


						//Form1.commsthread.AddCommand(Command.direct, 76, "disable-breakpoint 1");


					}
					else
					{
						// Add breakpoint
						line.MarkerAdd(BOOKMARK_MARKER);
						line.MarkerAdd(BREAKPOINT_MARKER);

						LineData ld = cf.GetLine(linenum);

						//Form1.commsthread.AddCommand(Command.setbreakpointaction, 76, " 1 break");
						Form1.commsthread.AddCommand(Command.setbreakpoint, 75, " 1 PC="+ld.Addess.ToString("x4")+"H");
						//Form1.commsthread.AddCommand(Command.direct, 74, "enable-breakpoint 1");

					}

				}



			}*/
		}


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Int to color. </summary>
		///
		/// <remarks> 05/09/2018. </remarks>
		///
		/// <param name="rgb"> The RGB. </param>
		///
		/// <returns> A Color. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static Color IntToColor(int rgb)
		{
			return System.Drawing.Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Sets break point marker. </summary>
		///
		/// <remarks> 08/09/2018. </remarks>
		///
		/// <param name="active">   True to active. </param>
		/// <param name="filename"> Filename of the file. </param>
		/// <param name="linenum">  The linenum. </param>
		///
		/// <returns> True if it succeeds, false if it fails. </returns>
		/// -------------------------------------------------------------------------------------------------
		public static bool SetBreakPointMarker(bool active, string filename, int linenum)
		{
			TraceFile tf = TraceFile.FindTraceFile(filename);
			const uint mask = (1 << BREAKPOINT_MARKER);

			if (tf.IsLineLegal(linenum))
			{
				var line = tf.codefile.codewindow.Lines[linenum];

				if (active)
				{
					if ((line.MarkerGet() & mask) == 0)
					{
						line.MarkerAdd(BREAKPOINT_MARKER);
						return true;
					}
				}
				else
				{
					if ((line.MarkerGet() & mask) != 0)
					{
						line.MarkerDelete(BREAKPOINT_MARKER);
						return true;
					}
								
				}


			}



			return false;

		}





	}
}
