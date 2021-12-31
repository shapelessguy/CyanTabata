using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class MyTextBox: RichTextBox
    {
        Timer timer;
        public MyTextBox()
        {
            timer = new Timer()
            {
                Interval = 10,
                Enabled = true,
            };
        }


        public void KeyPressed(object sender, KeyEventArgs e)
        {
            int selection = SelectionStart;
            if (Last_indices != null) foreach (int intero in Last_indices) { if (selection == intero) { if (e.KeyCode == Keys.Delete) e.SuppressKeyPress = true; } }
            if (Punti_nome_esteso != null)
            {
                if (SelectionLength > 0) return;
                foreach (Point point in Punti_nome_esteso)
                {
                    if (point.X <= selection && point.Y >= selection)
                        if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Space && e.KeyCode != Keys.Back && e.KeyCode != Keys.OemBackslash
                            && !(e.KeyCode == Keys.V && e.Modifiers == (Keys.Control))
                            && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Enter)
                        {
                            e.SuppressKeyPress = true;
                        }
                }
            }
            if (Punti_tempi != null)
            {
                foreach (Point point in Punti_tempi)
                {
                    if (point.X <= selection && point.Y >= selection)
                        if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Space && e.KeyCode != Keys.Back && e.KeyCode != Keys.OemBackslash
                            && !(e.KeyCode == Keys.V && e.Modifiers == (Keys.Control))
                            && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Enter
                            && e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad2 && e.KeyCode != Keys.NumPad3 && e.KeyCode != Keys.NumPad4 && e.KeyCode != Keys.NumPad5 && e.KeyCode != Keys.NumPad6 && e.KeyCode != Keys.NumPad7 && e.KeyCode != Keys.NumPad8 && e.KeyCode != Keys.NumPad9
                            && e.KeyCode != Keys.D0 && e.KeyCode != Keys.D1 && e.KeyCode != Keys.D2 && e.KeyCode != Keys.D3 && e.KeyCode != Keys.D4 && e.KeyCode != Keys.D5 && e.KeyCode != Keys.D6 && e.KeyCode != Keys.D7 && e.KeyCode != Keys.D8 && e.KeyCode != Keys.D9)
                        {
                            e.SuppressKeyPress = true;
                        }
                }
            }
            if (Punti_ripetizioni != null)
            {
                foreach (Point point in Punti_ripetizioni)
                {
                    if (point.X <= selection && point.Y >= selection)
                        if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Space && e.KeyCode != Keys.Back
                            && !(e.KeyCode == Keys.V && e.Modifiers == (Keys.Control))
                            && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Enter
                            && e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad2 && e.KeyCode != Keys.NumPad3 && e.KeyCode != Keys.NumPad4 && e.KeyCode != Keys.NumPad5 && e.KeyCode != Keys.NumPad6 && e.KeyCode != Keys.NumPad7 && e.KeyCode != Keys.NumPad8 && e.KeyCode != Keys.NumPad9
                            && e.KeyCode != Keys.D0 && e.KeyCode != Keys.D1 && e.KeyCode != Keys.D2 && e.KeyCode != Keys.D3 && e.KeyCode != Keys.D4 && e.KeyCode != Keys.D5 && e.KeyCode != Keys.D6 && e.KeyCode != Keys.D7 && e.KeyCode != Keys.D8 && e.KeyCode != Keys.D9)
                        {
                            e.SuppressKeyPress = true;
                        }
                }
            }

        }

        List<Point> Punti_nome_esteso;
        List<Point> Punti_tempi;
        List<Point> Punti_ripetizioni;
        List<int> Last_indices;
        List<string> Info_righe;
        public void Check(object sender, EventArgs e)
        {
            if (!Pannello1.pronto) { return; }
            Punti_nome_esteso = new List<Point>();
            Punti_tempi = new List<Point>();
            Punti_ripetizioni = new List<Point>();
            Last_indices = new List<int>();
            Info_righe = new List<string>();
            
            int indice = 0;
            string[] righe = Text.Split(new[] { '\n'});
            for (int i = 0; i < righe.Length; i++) righe[i] = righe[i].Replace("\n", "").Replace("\r", "");
            for (int j = 0; j < righe.Length; j++)
            {
                string info = "";
                if (righe[j] == "") { info = "Errore null"; indice += 1; Info_righe.Add(info); continue; }
                string stringa = righe[j];
                string[] pezzi = stringa.Split(new[] { '>'}); for (int i = 0; i < pezzi.Length; i++) pezzi[i] = pezzi[i].Replace(">", "");
                    int max_num_char = 31;
                if (pezzi[0].Length >= max_num_char) Punti_nome_esteso.Add(new Point(indice, indice + pezzi[0].Length));
                if (pezzi[0].Length >= max_num_char)
                {
                    bool dopo_vuoto = true;
                    for (int i = max_num_char; i < pezzi[0].Length; i++) if (pezzi[0].Substring(i, 1) != " ") dopo_vuoto = false;
                    if (!dopo_vuoto) { info = "Errore lunghezza nome"; }
                }
                if (pezzi.Length > 1) Punti_tempi.Add(new Point(indice + pezzi[0].Length + 1, indice + pezzi[0].Length + 1 + pezzi[1].Length));
                if (pezzi.Length > 2) Punti_ripetizioni.Add(new Point(indice + pezzi[0].Length + 2 + pezzi[1].Length, indice + pezzi[0].Length + 2 + pezzi[1].Length + pezzi[2].Length));
                int tempo=0, rip=0;
                if (pezzi.Length > 1) if (!int.TryParse(pezzi[1], out tempo)) { if (info != "") info += ", "; info += "Errore tempo"; }
                if (pezzi.Length > 2) if (!int.TryParse(pezzi[2], out rip)) { if (info != "") info += ", "; info += "Errore ripetizioni"; }
                if(pezzi.Length > 3) { if (info != "") info += ", "; info += "Errore overload argomenti"; }
                if (info == "")
                {
                    if (pezzi.Length == 1) info = "Errore tempo";
                    if (pezzi.Length == 2) { if (pezzi[0].Replace(" ", "").ToLower() == "riposo" || pezzi[0].Replace(" ", "").ToLower() == "rest") { if (info != "") info += ", "; info += "Riposo"; } else { if (info != "") info += ", "; info += "Tempo"; } }
                    if (pezzi.Length == 3) { if (info != "") info += ", "; info += "Ripetizioni"; }
                }
                
                indice += righe[j].Length + 1;
                Last_indices.Add(indice - 1);
                Info_righe.Add(info);
                //Console.WriteLine(righe[j] + "_____" + Info_righe[j]);
            }
            foreach (SottoPanel panel in Program.principale.panel_schede.panel_schede.sottopannelli)
            {
                if (panel.Scheda_TXT.Focused)
                {
                    panel.saved = false;
                    panel.SetInfo(Info_righe);
                    panel.Info_righe.Clear();
                    for (int i = 0; i < Info_righe.Count; i++) { panel.Info_righe.Add(Info_righe[i]); }
                }
            }

        }
        private const int WM_PASTE = 0x0302;
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_USER = 0x400;
        private const int SB_VERT = 1;
        private const int EM_SETSCROLLPOS = WM_USER + 222;
        private const int EM_GETSCROLLPOS = WM_USER + 221;
        [DllImport("user32.dll")]
        private static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);

        void Timer_Tick(object sender, EventArgs e)
        {
            timer.Tick -= Timer_Tick;
            Focus();
            //Console.WriteLine("Scrolla");
            
        }
        int selected = 0;
        public bool IsAtMaxScroll()
        {
            int minScroll;
            int maxScroll;
            GetScrollRange(this.Handle, SB_VERT, out minScroll, out maxScroll);
            Point rtfPoint = Point.Empty;
            SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref rtfPoint);
            //Console.WriteLine(rtfPoint.Y);
            selected = rtfPoint.Y;

            return (rtfPoint.Y + this.ClientSize.Height >= maxScroll);
        }
        public bool IsAtMinScroll()
        {
            int minScroll;
            int maxScroll;
            GetScrollRange(this.Handle, SB_VERT, out minScroll, out maxScroll);
            Point rtfPoint = Point.Empty;
            SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref rtfPoint);
            //Console.WriteLine(rtfPoint.Y);
            selected = rtfPoint.Y;

            return (rtfPoint.Y <= minScroll);
        }

        
            internal static ushort HIWORD(IntPtr dwValue)
            {
                return (ushort)((((long)dwValue) >> 0x10) & 0xffff);
            }

            internal static ushort HIWORD(uint dwValue)
            {
                return (ushort)(dwValue >> 0x10);
            }

            internal static int GET_WHEEL_DELTA_WPARAM(IntPtr wParam)
            {
                return (short)HIWORD(wParam);
            }

            internal static int GET_WHEEL_DELTA_WPARAM(uint wParam)
            {
                return (short)HIWORD(wParam);
            }



        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg != WM_PASTE)
                {
                    try { base.WndProc(ref m); } catch (Exception) { }
                }
                else
                {
                    if (Focused)
                    {
                        Check(null, null);
                    }
                    base.WndProc(ref m);
                }
                if (m.Msg == WM_MOUSEWHEEL)
                {
                    bool up = GET_WHEEL_DELTA_WPARAM((uint)m.WParam) > 0; bool down = !up;
                    if (IsAtMaxScroll())
                    {
                        if (up) return;
                        else
                        {
                            int Y = Program.principale.panel_schede.panel_schede.AutoScrollPosition.Y;
                            Program.principale.panel_schede.panel_schede.AutoScrollPosition = new Point(0, 25 - Y);
                        }
                    }
                    if (IsAtMinScroll())
                    {
                        if (down) return;
                        else
                        {
                            int Y = Program.principale.panel_schede.panel_schede.AutoScrollPosition.Y;
                            Program.principale.panel_schede.panel_schede.AutoScrollPosition = new Point(0, -25 - Y);
                        }
                    }
                    //base.WndProc(ref m);

                }
            }
            catch (Exception) { }
        }
        
    }
}
