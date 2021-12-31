using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class PanelInfo : Panel
    {
        List<string> paths_files = new List<string>();
        int[] data;
        Label Data = new Label();
        List<Label> Nomi = new List<Label>();
        List<Label> Schede = new List<Label>();
        List<Button> Erase = new List<Button>();
        List<Button> Copy = new List<Button>();
        Label Nome_Scheda = new Label();
        Label Scheda_Progressi = new Label();
        Button Erase_Progressi = new Button();
        Button Progressi = new Button();
        EvProgressi progresso = new EvProgressi();
        List<Eventi> eventi = new List<Eventi>();
        ToolTip tooltip = new ToolTip();
        public PanelInfo()
        {
            Controls.Add(Data);
            Controls.Add(Progressi);
            AutoScroll = true;
            Progressi.Click += SchedaProgressiClick;
        }

        public void Aggiorna(List<Eventi> eventi, EvProgressi progresso, int[] data)
        {
            this.data = data;
            this.progresso = progresso;
            this.eventi = eventi;
            foreach(Label nome in Nomi) Controls.Remove(nome);
            foreach (Label scheda in Schede) Controls.Remove(scheda);
            foreach (Button erase in Erase) Controls.Remove(erase);
            foreach (Button copy in Copy) Controls.Remove(copy);
            Nomi.Clear();
            Schede.Clear();
            Erase.Clear();
            Copy.Clear();
            Controls.Remove(Scheda_Progressi);
            Controls.Remove(Nome_Scheda);
            Data.Text = Date.GetGiornosettimanatxt(new int[] {0,0,0,data[0], data[1], data[2] }) + "   " +data[0].ToString() + "/" + data[1].ToString() + "/" + data[2].ToString();
            Nome_Scheda = new Label() { Text = "Scheda Progressi", AutoSize = false, };
            Scheda_Progressi = new Label() { AutoSize = false, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle,};
            Erase_Progressi = new Button() { Visible = false, Text = "", TextAlign = ContentAlignment.MiddleCenter, AutoSize = false, BackgroundImage = Properties.Resources.Red_X, BackgroundImageLayout = ImageLayout.Stretch, };
            Controls.Add(Nome_Scheda);
            Controls.Add(Scheda_Progressi);
            Controls.Add(Erase_Progressi);
            Erase_Progressi.Click += Elimina_Progresso;
            foreach (Eventi evento in eventi)
            {
                Nomi.Add(new Label() { Text = evento.nome, AutoSize = false, });
                Schede.Add(new Label() { Text = evento.scheda, AutoSize = false, });
                Erase.Add(new Button() { Text = "", TextAlign = ContentAlignment.MiddleCenter, AutoSize = false, BackgroundImage = Properties.Resources.Red_X, BackgroundImageLayout = ImageLayout.Stretch, });
                Copy.Add(new Button() { Text = "", TextAlign = ContentAlignment.MiddleCenter, AutoSize = false, BackgroundImage = Properties.Resources.Clipboard, BackgroundImageLayout = ImageLayout.Stretch, });
                Controls.Add(Nomi[Nomi.Count - 1]);
                Controls.Add(Schede[Schede.Count - 1]);
                Controls.Add(Erase[Erase.Count - 1]);
                Controls.Add(Copy[Copy.Count - 1]);
                Erase[Erase.Count - 1].MouseClick += Elimina;
                Copy[Copy.Count - 1].MouseClick += Copy_Clipboard;
                tooltip.SetToolTip(Erase[Erase.Count - 1], "Elimina Scheda");
                tooltip.SetToolTip(Copy[Copy.Count - 1], "Copia negli appunti");



            }
            Data.Location = new Point(30, 15);
            try
            {
                if (progresso.scheda == null) { Progressi.Text = "Compila scheda Progressi"; Nome_Scheda.Text = ""; Nome_Scheda.Visible = false; Scheda_Progressi.Visible = false; Erase_Progressi.Visible=false; }
                else { Nome_Scheda.Show(); Scheda_Progressi.Show(); Progressi.Text = "Modifica scheda Progressi"; Scheda_Progressi.Text = progresso.scheda; Erase_Progressi.Show(); }
            }
            catch (Exception) { Progressi.Text = "Compila scheda Progressi"; Nome_Scheda.Text = ""; Nome_Scheda.Visible = false; Scheda_Progressi.Visible = false; }
            ResizeForm();

        }
        public void AggiornaProgresso(EvProgressi progresso)
        {
            this.progresso = progresso;
            try
            {
                if (progresso.scheda == null) { Progressi.Text = "Compila scheda Progressi"; Nome_Scheda.Text = ""; Nome_Scheda.Visible = false; Scheda_Progressi.Visible = false; Erase_Progressi.Visible = false; }
                else { Nome_Scheda.Text = "Scheda Progressi"; Nome_Scheda.Show(); Scheda_Progressi.Show(); Progressi.Text = "Modifica scheda Progressi"; Scheda_Progressi.Text = progresso.scheda; Erase_Progressi.Show(); }
            }
            catch (Exception) { Progressi.Text = "Compila scheda Progressi"; Nome_Scheda.Text = ""; Nome_Scheda.Visible = false; Scheda_Progressi.Visible = false; }
            ResizeForm();
        }
        public void ResizeForm()
        {
            Data.Size = new Size(Width - 55, (int)((double)Width * 0.014 + 15) * 2);
            Data.Font = new System.Drawing.Font("Arial", (int)((double)Width * 0.014 + 15), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Progressi.Font = new System.Drawing.Font("Arial", (int)((double)Width * 0.014 + 6), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //Data.Location = new System.Drawing.Point(0, 15);
            Progressi.Size = new Size(Width-80, (int)((double)Width * 0.014 + 15) * 2);
            Progressi.Location = new Point(10, Data.Location.Y + (int)(Data.Height*1.4));

            Nome_Scheda.Location = new Point(0, Progressi.Location.Y + (int)(Progressi.Height * 1.4));
            Nome_Scheda.Size = Data.Size;
            Nome_Scheda.Font = Data.Font;
            Scheda_Progressi.Font = new System.Drawing.Font("Arial", (int)((double)Width * 0.012 + 8), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            int altezza_progressi = 0;

            if (Nome_Scheda.Text != "")
            {
                altezza_progressi = Scheda_Progressi.Text.Split('\n').Length * (int)((double)(Width * 0.014 + 8) * 1.5);
                Scheda_Progressi.Size = new Size(Width - 30 - 3, altezza_progressi);
                Scheda_Progressi.Location = new Point(Nome_Scheda.Location.X + 3, Nome_Scheda.Location.Y + Nome_Scheda.Height);
                altezza_progressi = Scheda_Progressi.Location.Y - Progressi.Location.Y - Progressi.Height + Scheda_Progressi.Height;
                Erase_Progressi.Size = new Size((int)(Width * 0.05), (int)(Width * 0.05));
                Erase_Progressi.Location = new Point(Nome_Scheda.Width - Erase_Progressi.Width - 5, Nome_Scheda.Location.Y + 5);
                Erase_Progressi.BringToFront();
            }
            else Erase_Progressi.Visible = false;


            if (Nomi.Count>0) Nomi[0].Location = new Point(0, Progressi.Location.Y + (int)(Progressi.Height*1.4)+altezza_progressi);
            for (int i=0; i<Nomi.Count; i++)
            {
                int altezza = (Schede[i].Text.Split(new[] { '\n' }).Length +1)* (int)((double)(Width * 0.014 + 8)*1.5);
                Schede[i].Size = new Size(Width-30, altezza);
                Erase[i].Size = new Size((int)(Width * 0.05), (int)(Width * 0.05));
                Copy[i].Size = new Size((int)(Width * 0.05), (int)(Width * 0.05));
                Nomi[i].Size = Data.Size;
                Nomi[i].Font = Data.Font;
                Schede[i].Font = Scheda_Progressi.Font;
                if (i!= 0) Nomi[i].Location = new Point(0, Schede[i - 1].Location.Y + Schede[i-1].Height);
                Schede[i].Location = new Point(0, Nomi[i].Location.Y + Nomi[i].Height);
                Erase[i].Location = new Point(Nomi[i].Width, Nomi[i].Location.Y + 5);
                Copy[i].Location = new Point(Nomi[i].Width - Erase[i].Width - 5, Nomi[i].Location.Y + 5); 
                Erase[i].BringToFront();
                Copy[i].BringToFront();

            }
        }
        void Elimina_Progresso(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sicuro di voler eliminare questa scheda progressi?", "Eliminazione progresso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                string file_name = Program.path + @"\Progressi\" + progresso.datacode + ".txt";
                File.Delete(file_name);
                Pannello3.progressi.Clear();
                Pannello3.progressi = EvProgressi.GetProgressi(Program.path + @"\Progressi\");
                Program.principale.panel_cronologia.RefreshWindow();
                Program.principale.panel_cronologia.Info.AggiornaProgresso(null);
                //{ Progressi.Text = "Compila scheda Progressi"; Nome_Scheda.Text = ""; Nome_Scheda.Visible = false; Scheda_Progressi.Visible = false; }
                //progresso = null;
            }
        }
        void SchedaProgressiClick(object sender, EventArgs e)
        {
            if(progresso== null)
            {
                EvProgressi.Compila(data, null);
            }
            else
            {
                EvProgressi.Compila(data, progresso);
            }
        }
        void Elimina (object sender, MouseEventArgs e)
        {
            bool trovato = false; int i = 0;
            Point A = Cursor.Position;
            for (i = 0; i < Erase.Count; i++)
            {
                Point B = PointToScreen(Erase[i].Location);
                double distAB = Math.Sqrt(Math.Pow((A.X - B.X), 2) + Math.Pow((A.Y - B.Y), 2));
                if (distAB < Erase[i].Width * 0.7071 *2) { trovato = true; break; }
            }
            if (trovato)
            {
                if (MessageBox.Show("Sicuro di voler eliminare dalla cronologia questo evento?", "Eliminazione traguardo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    string file_name = Program.path + @"\Cronologia\" + eventi[i].datacode.ToString() + ".txt";
                    Pannello3.eventi.Remove(eventi[i]);
                    File.Delete(file_name);
                    Program.principale.panel_cronologia.RefreshWindow();
                    Controls.Remove(Nomi[i]);
                    Controls.Remove(Schede[i]);
                    Controls.Remove(Erase[i]);
                    Controls.Remove(Copy[i]);
                    Nomi.RemoveAt(i);
                    Schede.RemoveAt(i);
                    Erase.RemoveAt(i);
                    Copy.RemoveAt(i);
                    ResizeForm();
                }
            }

        }
        void Copy_Clipboard(object sender, EventArgs e)
        {
            bool trovato = false; int i = 0;
            Point A = Cursor.Position;
            for (i = 0; i < Copy.Count; i++)
            {
                Point B = PointToScreen(Copy[i].Location);
                double distAB = Math.Sqrt(Math.Pow((A.X - B.X), 2) + Math.Pow((A.Y - B.Y), 2));
                if (distAB < Copy[i].Width * 0.7071 * 2) { trovato = true; break; }
            }
            if (trovato)
            {
                Clipboard.SetText(Schede[i].Text);
            }
        }
    }
}
