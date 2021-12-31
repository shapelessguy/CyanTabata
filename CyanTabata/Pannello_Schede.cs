using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class Pannello_Schede : Panel
    {
        public List<MButton> bottoni = new List<MButton>();
        public List<TextBox> bottoni_text = new List<TextBox>();
        public List<SottoPanel> sottopannelli = new List<SottoPanel>();
        public List<Scheda> Schede = new List<Scheda>();
        public int button_height = 70;
        public int modo = 0;
        static public int altezzatext = 13;
        Button Plus;
        public Pannello_Schede(List<Scheda> Schede, int modo)
        {
            this.modo = modo;
            this.Schede = Schede;
            Location = new Point(0, 100);
            Size = new Size(533 - 25, 3 * button_height);
            AutoScroll = true;
            Click += ClickNull;
            if (modo == 2)
            {
                Plus = new Button() { Visible = true, Text = "Aggiungi", };
                Controls.Add(Plus);
                Plus.Click += Aggiungi;
            }

            void ClickNull(object sender, EventArgs e)
            {
                Modificato(null, null);
            }

            for (int i = 0; i < Schede.Count; i++)
            {
                bottoni.Add(new MButton(i, modo)
                {
                    Visible = true,
                    Text = Schede[i].nome_scheda,
                    Font = new System.Drawing.Font("Palatino Linotype", 36, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    TabStop = false,
                });
                Controls.Add(bottoni[bottoni.Count - 1]);
                //bottoni[bottoni.Count - 1].ContextMenuStrip = contextMenuStrip;
                bottoni_text.Add(new TextBox()
                {
                    TextAlign = HorizontalAlignment.Center,
                    Visible = false,
                    Text = Schede[i].nome_scheda,
                    Font = new System.Drawing.Font("Palatino Linotype", 36, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    TabStop = false,
                    AutoSize = false,
                });
                Controls.Add(bottoni_text[bottoni_text.Count - 1]);
                bottoni_text[bottoni_text.Count - 1].LostFocus += Modificato;
                bottoni_text[bottoni_text.Count - 1].KeyDown += Modificato_Enter;
                if (modo == 2)
                {
                    sottopannelli.Add(new SottoPanel(Schede[i], i)
                    {
                        Visible = false,
                    });
                    Controls.Add(sottopannelli[bottoni.Count - 1]);
                    sottopannelli[bottoni.Count - 1].Creazione();
                }
                bottoni[bottoni.Count - 1].MouseClick += Clicks;
                bottoni[bottoni.Count - 1].MouseUp += Mouse_up;
            }
            if(Schede.Count>0) bottoni[0].Location = new Point(25, 50);

            Resize_Elements();
        }

        void Modificato_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Modificato(null, null); }
            int num_max_char = 10;
            if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back) foreach (TextBox txt in bottoni_text) if (txt.Text.Length >= num_max_char) e.SuppressKeyPress = true;
        }
        private void Aggiungi(object sender, EventArgs e)
        {
            string[] nomi = new string[Pannello1.arc.Schede.Count];
            for (int i = 0; i < Pannello1.arc.Schede.Count; i++) nomi[i] = Pannello1.arc.Schede[i].nome_scheda;
            string nome = "New Scheda";
            if (nomi.Contains(nome))
            {
                for (int j = 0; ; j++)
                {
                    if (!nomi.Contains(nome + " (" + j + ")")) { nome = nome + " (" + j + ")"; break; }
                }
            }
            Pannello1.arc.Schede.Insert(0, new Scheda(new List<string>() { nome, }, 0));
            Program.principale.panel_schede.Aggiorna();
            Program.principale.panel_principale.Aggiorna_PannelloSchede();
        }

        private void Mouse_up(object sender, MouseEventArgs e)
        {
            Program.principale.panel_principale.listBox1.Focus();
        }
        public void Modificato(object sender, EventArgs e)
        {
            for(int i=0; i< bottoni_text.Count; i++)
            {
                bottoni[i].SemiSaveModifica();
            }
            Program.Saving();
            Program.principale.panel_principale.Aggiorna_PannelloSchede();
        }
        private void Clicks(object sender, MouseEventArgs e)
        {
            
            if (modo == 1)
            {
                if (!Pannello1.pronto) { return; }
                Program.principale.panel_principale.timer.Tick -= Program.principale.panel_principale.Progresso;
                for (int i = 0; i < bottoni.Count; i++)
                {

                    if (PointToClient(MousePosition).Y >= bottoni[i].Location.Y && PointToClient(MousePosition).Y < bottoni[i].Location.Y + bottoni[i].Height)
                    {
                        Program.principale.panel_principale.StartScheda(Schede[i]);
                        Program.principale.panel_principale.label10.Text = "Scheda " + Schede[i].nome_scheda;
                        break;
                    }
                }
            }
            if (modo == 2)
            {
                for (int i = 0; i < bottoni.Count; i++)
                {

                    if (PointToClient(MousePosition).Y >= bottoni[i].Location.Y && PointToClient(MousePosition).Y < bottoni[i].Location.Y + bottoni[i].Height)
                    {
                        if (sottopannelli[i].Visible) { sottopannelli[i].Visible = false; Resize_Elements(); }
                        else { sottopannelli[i].Visible = true; Resize_Elements(); }
                        break;
                    }
                }
            }
        }

        public void Resize_Elements()
        {
            SuspendLayout();
            for (int i = 0; i < Schede.Count; i++)
            {
                if (modo == 1)
                {
                    bottoni[i].Size = new Size(Size.Width - 50, Height / 3);
                    bottoni[i].Location = new Point(25, bottoni[i].Height * i);
                    bottoni[i].Font = new Font("Arial", (int)(Width * 0.076 - 2), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    bottoni_text[i].Size = bottoni[i].Size;
                    bottoni_text[i].Location = bottoni[i].Location;
                    bottoni_text[i].Font = bottoni[i].Font;
                }
                if (modo == 2)
                {
                    Plus.Location = new Point((int)(Width - 75 - Plus.Width) / 2 + 37, bottoni[0].Location.Y - 40);
                    bottoni[i].Size = new Size(Size.Width - 50, Height / 20);
                    bottoni[i].ResizeElements();
                    if (i != 0)
                    {
                        if (sottopannelli[i - 1].Visible) bottoni[i].Location = new Point(25, sottopannelli[i - 1].Location.Y + sottopannelli[i - 1].Height);
                        else bottoni[i].Location = new Point(25, bottoni[i - 1].Location.Y + bottoni[i - 1].Height);
                    }
                    //else bottoni[i].Location = new Point(25, 50);
                    sottopannelli[i].Location = new Point(30, bottoni[i].Location.Y + bottoni[i].Height);
                    sottopannelli[i].Size = new Size(bottoni[i].Width -10, 20 * altezzatext + 6);
                    //sottopannelli[i].Formattazione.Location = 
                    sottopannelli[i].Resize_Elements();
                    bottoni[i].Font = new Font("Arial", (int)(Height * 0.02), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
            ResumeLayout();
        }
    }
}
