using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class Pannello3 : Panel
    {
        private Label button_precedente;
        private Label button_successivo;
        private Label label_mese;
        private Label label_anno;
        

        private Timer timer;

        public static bool wait = false;

        public Panel Principale;
        private Panel Settimana;
        public PanelInfo Info;
        private readonly Label[] label = new Label[7];
        public readonly Bottoni[] button = new Bottoni[37];
        static public string font1 = "Script MT Bold";

        static public List<int> giorni_festivi = new List<int>();

        static public List<Eventi> eventi_giorno = new List<Eventi>();
        static private List<Eventi> eventi_mese = new List<Eventi>();
        static public List<Eventi> eventi = new List<Eventi>();
        static private List<EvProgressi> progressi_mese = new List<EvProgressi>();
        static public List<EvProgressi> progressi = new List<EvProgressi>();

        static readonly private double proporzione_massima = 2.5;  //  Larghezza/Altezza
        static private Size taglia_piccola, taglia_grande;
        static public int button_clicked = 0;
        static private int numero_giorni = 0;
        public int giorno = 0;
        public int mese;
        public int anno;
        static public int posizione_iniziale = 0;
        static public int lastWidth;
        static public int lastHeight;
        static public bool resize;
        public Pannello3()
        {
            DoubleBuffered = true;
            wait = false;
            Visible = true;
            mese = Input.data_utile[4]; anno = Input.data_utile[5];
            giorni_festivi.Add(6); giorni_festivi.Add(7);
            Calcoli_Mese();

            label_mese = new System.Windows.Forms.Label();
            label_anno = new System.Windows.Forms.Label();
            Principale = new System.Windows.Forms.Panel();

            //Definizione Pannello bottoni
            Principale = new Panel();
            Principale.MouseEnter += Enter;
            Principale.MouseClick += ClickNull;
            Controls.Add(Principale);
            MouseClick += ClickNull;

            timer = new Timer()
            {
                Enabled = true,
                Interval = 100,
            };
            timer.Tick += TimerF;

            Info = new PanelInfo()
            {
                Visible = false,
                BorderStyle = BorderStyle.Fixed3D,
            };
            Controls.Add(Info);

            //VisibleChanged += Visibilità;

            //Definizione bottoni
            for (int i = 0; i < 37; i++)
            {
                button[i] = new Bottoni()
                {
                    index = i
                };
                Principale.Controls.Add(button[i]);
            }
            //Definizione Pannello Settimana e componenti
            Settimana = new Panel();
            Settimana.MouseEnter += Enter;
            Settimana.MouseClick += ClickNull;
            Controls.Add(Settimana);
            for (int i = 0; i < 7; i++)
            {
                label[i] = new System.Windows.Forms.Label
                {
                    //AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                };
                label[i].MouseEnter += Enter;
                if (i == 0) label[i].Text = "Lunedì";
                if (i == 1) label[i].Text = "Martedì";
                if (i == 2) label[i].Text = "Mercoledì";
                if (i == 3) label[i].Text = "Giovedì";
                if (i == 4) label[i].Text = "Venerdì";
                if (i == 5) label[i].Text = "Sabato";
                if (i == 6) label[i].Text = "Domenica";
                Settimana.Controls.Add(label[i]);
                label[i].MouseClick += ClickNull;
                label[i].BringToFront();
            }

            //Definizione pulsanti Indietro-Avanti
            button_precedente = new System.Windows.Forms.Label
            {
                BackColor = System.Drawing.Color.Transparent,
                BackgroundImage = new Bitmap((System.Drawing.Image)(Properties.Resources.ResourceManager.GetObject("Freccia_sx"))),
                BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch,
                FlatStyle = System.Windows.Forms.FlatStyle.Popup
            };
            button_precedente.MouseClick += new MouseEventHandler(Button_precedente_Click);
            button_precedente.MouseEnter += new System.EventHandler(Button_precedente_MouseEnter);
            button_precedente.MouseLeave += new System.EventHandler(Button_precedente_MouseLeave);
            //button_precedente.MouseClick += ClickNull;
            Controls.Add(button_precedente);

            button_successivo = new System.Windows.Forms.Label
            {
                BackColor = System.Drawing.Color.Transparent,
                BackgroundImage = new Bitmap((System.Drawing.Image)(Properties.Resources.ResourceManager.GetObject("Freccia_dx"))),
                BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch,
                FlatStyle = System.Windows.Forms.FlatStyle.Popup
            };
            button_successivo.MouseClick += new MouseEventHandler(Button_successivo_Click);
            button_successivo.MouseEnter += new System.EventHandler(Button_successivo_MouseEnter);
            button_successivo.MouseLeave += new System.EventHandler(Button_successivo_MouseLeave);
            //button_successivo.MouseClick += ClickNull;
            Controls.Add(button_successivo);

            //Definizione label mese e anno
            label_mese.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            Controls.Add(label_mese);
            label_mese.MouseClick += ClickNull;
            label_anno.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            Controls.Add(label_anno);
            label_anno.MouseClick += ClickNull;

            RefreshWindow();
            //FinestraPrincipale.size = FinestraPrincipale.BackPanel.Size;
        }

        public void Visibilità(object sender, EventArgs e)
        {
            if (!Visible) {Info.Hide(); LocationChanging();}
        }
        public void RefreshWindow()
        {
            SuspendLayout();

            for (int i = 0; i < 37; i++)
            {
                if (giorni_festivi.Contains(i % 7 + 1)) button[i].festivo = true; else button[i].festivo = false;
                button[i].SuspendLayout();
                button[i].guadagno_pic.Hide(); button[i].spesa_pic.Hide(); button[i].trasferimento_pic.Hide(); button[i].note_pic.Hide(); button[i].guadagno.Hide(); button[i].spesa.Hide();
                button[i].ResumeLayout(false);
            }
            Calcoli_Mese();
            int[] ausiliare = { 0, 0, 0, 1, mese, anno };
            int[] data_ausiliare = new int[6];
            data_ausiliare[3] = 1; data_ausiliare[4] = mese; data_ausiliare[5] = anno;
            posizione_iniziale = Date.GetIntfromGiornoSettimana(Date.GetGiornosettimanatxt(ausiliare));
            label_mese.Text = Date.GetMesetxt(mese);
            label_anno.Text = Convert.ToString(anno);
            for (int i = 1; i < 37; i++)
            {
                if (i + posizione_iniziale - 1 < 37)
                {
                    button[i + posizione_iniziale - 1].Text = Convert.ToString(i);
                }
                List<string> paths = new List<string>();
                foreach (var evento in eventi_mese)
                {
                    if (evento.data[3] == i)
                    {
                        paths.Add(evento.datacode.ToString());
                    }
                }
                if(i + posizione_iniziale - 1>=0 && i + posizione_iniziale - 1<37) button[i + posizione_iniziale - 1].SetProgresso(null); 
                foreach (var progresso in progressi_mese)
                {
                    if (progresso.data[3] == i)
                    {
                        button[i + posizione_iniziale - 1].SetProgresso(progresso);
                    }
                }
                Calcoli_Giorno(i);
                if (i + posizione_iniziale - 1 >= 0 && i + posizione_iniziale - 1 < 37)
                {
                    button[i + posizione_iniziale - 1].SetCronologia(eventi_giorno);
                    button[i + posizione_iniziale - 1].SetData(new int[] { i, mese, anno });
                }

            }
            RefreshBottoni();
            /*
            Guadagno_Complessivo.Text = Funzioni_utili.FormatoStandard(guadagno_mese) + "\u20AC";
            Spesa_Complessiva.Text = Funzioni_utili.FormatoStandard(spesa_mese) + "\u20AC";
            FinestraPrincipale.BackPanel.Portafogli.Text = Funzioni_utili.FormatoStandard(Input.totali[0]) + "\u20AC";
            if (Input.totali.Count() > 1) { FinestraPrincipale.BackPanel.Cassaforte.Text = Funzioni_utili.FormatoStandard(Input.totali[1]) + "\u20AC"; }
            if (Input.totali.Count() == 1) { FinestraPrincipale.BackPanel.Portafogli_Pic.Show(); FinestraPrincipale.BackPanel.Portafogli.Show(); FinestraPrincipale.BackPanel.Banca_Pic.Hide(); FinestraPrincipale.BackPanel.Cassaforte_Pic.Hide(); FinestraPrincipale.BackPanel.Cassaforte.Hide(); }
            if (Input.totali.Count() == 2) { FinestraPrincipale.BackPanel.Portafogli_Pic.Show(); FinestraPrincipale.BackPanel.Portafogli.Show(); FinestraPrincipale.BackPanel.Banca_Pic.Hide(); FinestraPrincipale.BackPanel.Cassaforte_Pic.Visible = true; FinestraPrincipale.BackPanel.Cassaforte.Visible = true; }
            if (Input.totali.Count() > 2) { FinestraPrincipale.BackPanel.Portafogli_Pic.Show(); FinestraPrincipale.BackPanel.Portafogli.Show(); FinestraPrincipale.BackPanel.Banca_Pic.Visible = true; FinestraPrincipale.BackPanel.Cassaforte_Pic.Visible = true; FinestraPrincipale.BackPanel.Cassaforte.Visible = true; }
            FinestraPrincipale.BackPanel.Portafogli_Pic.BackgroundImage = Funzioni_utili.TakePicture(Input.metodi[0], 2);
            if (Input.totali.Count > 1) FinestraPrincipale.BackPanel.Cassaforte_Pic.BackgroundImage = Funzioni_utili.TakePicture(Input.metodi[1], 2);
            for (int i = 0; i < posizione_iniziale; i++) button[i].Visible = false;
            for (int i = posizione_iniziale; i < posizione_iniziale + numero_giorni; i++) button[i].Visible = true;
            for (int i = posizione_iniziale + numero_giorni; i < 37; i++) button[i].Visible = false;

            if (FinestraPrincipale.BackPanel.Panel_Ricerca != null) if (FinestraPrincipale.BackPanel.Panel_Ricerca.Visible)
                { FinestraPrincipale.BackPanel.Portafogli_Pic.Hide(); FinestraPrincipale.BackPanel.Portafogli.Hide(); FinestraPrincipale.BackPanel.Banca_Pic.Hide(); FinestraPrincipale.BackPanel.Cassaforte_Pic.Hide(); FinestraPrincipale.BackPanel.Cassaforte.Hide(); }
            */
            for (int i = 0; i < posizione_iniziale; i++) button[i].Visible = false;
            for (int i = posizione_iniziale; i < posizione_iniziale + numero_giorni; i++) button[i].Visible = true;
            for (int i = posizione_iniziale + numero_giorni; i < 37; i++) button[i].Visible = false;
            ResumeLayout();

        }
        public void RefreshBottoniColor()
        {
            for (int k = posizione_iniziale; k < numero_giorni + posizione_iniziale; k++)
                if (k - posizione_iniziale + 1 == Input.data_utile[3] && mese == Input.data_utile[4] && anno == Input.data_utile[5]) button[k].attuale = true; else button[k].attuale = false;
            foreach (Bottoni buttone in button) buttone.RefreshColor();
        }
        public void RefreshBottoni()
        {
            for (int k = posizione_iniziale; k < numero_giorni + posizione_iniziale; k++)
                if (k - posizione_iniziale + 1 == Input.data_utile[3] && mese == Input.data_utile[4] && anno == Input.data_utile[5]) button[k].attuale = true; else button[k].attuale = false;
            foreach (Bottoni buttone in button) buttone.Refresh_Bottoni();
        }


        public new void Click()
        {
            /*
            FinestraPrincipale.BackPanel.StandardCalendar.Visible = false;
            eventi_giorno.Clear();
            Calcoli_Giorno();
            Input.LoadAttributi();
            FinestraPrincipale.BackPanel.Panel_Giorno = new ProprietàGiorno()
            {
                Size = FinestraPrincipale.Finestra.Size,
                Visible = false,
            };
            FinestraPrincipale.BackPanel.Controls.Add(FinestraPrincipale.BackPanel.Panel_Giorno);
            RefreshWindow(); FinestraPrincipale.BackPanel.Panel_Giorno.timerPannello.Start();
            */


        }
        

        public void Calcoli_Mese()
        {
            numero_giorni = Date.ContaGiorni(mese, anno);
            eventi_mese.Clear();
            eventi_mese = new List<Eventi>();
            foreach (var evento in eventi)
            {
                if (evento.data[4] == mese && evento.data[5] == anno) eventi_mese.Add(evento);
            }
            progressi_mese.Clear();
            progressi_mese = new List<EvProgressi>();
            foreach (var progresso in progressi)
            {
                if (progresso.data[4] == mese && progresso.data[5] == anno) { progressi_mese.Add(progresso); }
            }


        }
        private void Calcoli_Giorno(int i)
        {
            eventi_giorno.Clear();
            foreach (var evento in eventi_mese)
            {
                if (evento.data[3] == i) eventi_giorno.Add(evento);
            }
        }

        private void Button_precedente_Click(object sender, MouseEventArgs e)
        {
            //if (FinestraPrincipale.BackPanel.altriconti) return;
            if (e.Button == MouseButtons.Left)
            {
                SuspendLayout();
                mese--;
                if (mese == 0) { mese = 12; anno--; }
                eventi_mese.Clear();
                label_mese.Text = Date.GetMesetxt(mese);
                label_anno.Text = Convert.ToString(anno);
                RefreshWindow();
                button_precedente.Size = taglia_grande;
                ResumeLayout();
            }
        }

        private void Button_successivo_Click(object sender, MouseEventArgs e)
        {
            //if (FinestraPrincipale.BackPanel.altriconti) return;
            if (e.Button == MouseButtons.Left)
            {
                mese++;
                if (mese == 13) { mese = 1; anno++; }
                eventi_mese.Clear();
                label_mese.Text = Date.GetMesetxt(mese);
                label_anno.Text = Convert.ToString(anno);
                RefreshWindow();
            }
        }
        private void Button_precedente_MouseLeave(object sender, EventArgs e)
        {
            //if (FinestraPrincipale.BackPanel.altriconti) return;
            button_precedente.Size = taglia_piccola;
            button_precedente.Location = new Point(button_precedente.Location.X + (taglia_grande.Width - taglia_piccola.Width), button_precedente.Location.Y + (taglia_grande.Height - taglia_piccola.Height) / 2);
        }

        private void Button_precedente_MouseEnter(object sender, EventArgs e)
        {
            //if (FinestraPrincipale.BackPanel.altriconti) return;
            button_precedente.Size = taglia_grande;
            button_precedente.Location = new Point(button_precedente.Location.X - (taglia_grande.Width - taglia_piccola.Width), button_precedente.Location.Y - (taglia_grande.Height - taglia_piccola.Height) / 2);
        }

        private void Button_successivo_MouseLeave(object sender, EventArgs e)
        {
            //if (FinestraPrincipale.BackPanel.altriconti) return;
            button_successivo.Size = taglia_piccola;
            button_successivo.Location = new Point(button_successivo.Location.X, button_successivo.Location.Y + 6);
        }


        private void Button_successivo_MouseEnter(object sender, EventArgs e)
        {
            //if (FinestraPrincipale.BackPanel.altriconti) return;
            button_successivo.Size = taglia_grande;
            button_successivo.Location = new Point(button_successivo.Location.X, button_successivo.Location.Y - 6);
        }

        private new void Enter(object sender, EventArgs e)
        {
            Bottoni.index_on = -1;
            Bottoni.Selezione();
        }
        public void Refresh_Indispensabile()
        {
            label_mese.Text = Date.GetMesetxt(mese);
            label_anno.Text = Convert.ToString(anno);
        }
        public static int[] GetGiornoMeseAnno()
        {
            int[] array = new int[3];
            //array[0] = FinestraPrincipale.BackPanel.StandardCalendar.giorno;
            //array[1] = FinestraPrincipale.BackPanel.StandardCalendar.mese;
            //array[2] = FinestraPrincipale.BackPanel.StandardCalendar.anno;
            return array;
        }
        private void TimerF(object sender, EventArgs e)
        {
            //if (resize && ProprietàGiorno.time_to_save == false)
            {
               // resize = false;
               // RefreshWindow();
            }
            if (wait) return;
            if (Program.principale.ClientRectangle.Contains(Program.principale.PointToClient(Cursor.Position)))
            {
                return;
            }
            Bottoni.index_on = -1;
            Bottoni.Selezione();
            wait = true;
        }

        private void MouseIsOverControl(object sender, EventArgs e)
        {
            if (wait) return;
            if (Principale.ClientRectangle.Contains(Principale.PointToClient(Cursor.Position)))
            {
                return;
            }
            Bottoni.index_on = -1;
            Bottoni.Selezione();
            wait = true;
        }
        public void HideButtons()
        {
            foreach (Bottoni button in button) button.BorderStyle = BorderStyle.None;
        }
        public void ShowButtons()
        {
            foreach (Bottoni button in button) button.BorderStyle = BorderStyle.FixedSingle;
        }

        public void ClickNull(object sender, EventArgs e)
        {
            Info.Hide();
            LocationChanging();
        }




        public void LocationChanging()
        {
            RefreshWindow();
            Size = new System.Drawing.Size(Program.principale.Width, Program.principale.Height - Program.principale.Menù.Height - 5);
            Location = new Point(0, Program.principale.Menù.Height);
            if ((double)Program.principale.Size.Width / (double)Program.principale.Size.Height > (double)proporzione_massima) { Program.principale.Width = (int)(Program.principale.Size.Height * proporzione_massima); }
            
            Size = new System.Drawing.Size(Program.principale.Width, Program.principale.Height - Program.principale.Menù.Height);
            int larghezza_forma = Width;
            int altezza_forma = Height;
            larghezza_forma = this.Width;
            altezza_forma = this.Height;
            Info.SetBounds((int)(Convert.ToDouble(larghezza_forma) * 0.68), (int)(Convert.ToDouble(altezza_forma) * 0.0), (int)(Convert.ToDouble(larghezza_forma) * 0.33)-25, (int)(Convert.ToDouble(altezza_forma) * 1) - 40);
            Info.ResizeForm();
            if (Info.Visible)
            {
                Principale.SetBounds((int)(Convert.ToDouble(larghezza_forma) * 0.01), (int)(Convert.ToDouble(altezza_forma) * 0.2), (int)(Convert.ToDouble(larghezza_forma) * 0.65), (int)(Convert.ToDouble(altezza_forma) * 0.75));
                Settimana.SetBounds((int)(Convert.ToDouble(larghezza_forma) * 0.015), (int)(Convert.ToDouble(altezza_forma) * 0.161), (int)(Convert.ToDouble(larghezza_forma) * 0.62), (int)(Convert.ToDouble(altezza_forma) * 0.043));
                for (int i = 0; i < 7; i++)
                {
                    if (i == 0) label[i].Text = "Lun";
                    if (i == 1) label[i].Text = "Mar";
                    if (i == 2) label[i].Text = "Mer";
                    if (i == 3) label[i].Text = "Gio";
                    if (i == 4) label[i].Text = "Ven";
                    if (i == 5) label[i].Text = "Sab";
                    if (i == 6) label[i].Text = "Dom";
                }
            }
            else
            {
                Principale.SetBounds((int)(Convert.ToDouble(larghezza_forma) * 0.01), (int)(Convert.ToDouble(altezza_forma) * 0.2), (int)(Convert.ToDouble(larghezza_forma) * 0.97), (int)(Convert.ToDouble(altezza_forma) * 0.75));
                Settimana.SetBounds((int)(Convert.ToDouble(larghezza_forma) * 0.02), (int)(Convert.ToDouble(altezza_forma) * 0.161), (int)(Convert.ToDouble(larghezza_forma) * 0.92), (int)(Convert.ToDouble(altezza_forma) * 0.043));
                for (int i = 0; i < 7; i++)
                {
                    if (i == 0) label[i].Text = "Lunedì";
                    if (i == 1) label[i].Text = "Martedì";
                    if (i == 2) label[i].Text = "Mercoledì";
                    if (i == 3) label[i].Text = "Giovedì";
                    if (i == 4) label[i].Text = "Venerdì";
                    if (i == 5) label[i].Text = "Sabato";
                    if (i == 6) label[i].Text = "Domenica";
                }
            }
            
            foreach (Bottoni bottone in button) bottone.Refresh_Bottoni();
            int j = -1, spazietto = 1;
            button[0].Location = new System.Drawing.Point(Principale.Width / 7 - button[0].Width, 0);
            for (int i = 1; i < 37; i++)
            {
                if (i % 7 == 0) { j++; button[i].Location = new Point(button[i - 7].Location.X, button[i - 7].Location.Y + button[i - 7].Height + spazietto); }
                else button[i].Location = new System.Drawing.Point(button[i - 1].Location.X + button[i - 1].Width + spazietto, button[i - 1].Location.Y);
            }
            for (int i = 0; i < 7; i++)
            {
                label[i].Location = new System.Drawing.Point(Settimana.Width / 7 * i, 0);
                label[i].Font = new System.Drawing.Font(font1, (int)((double)altezza_forma / 4000 * 30 + 10), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label[i].Size = new Size(Settimana.Width / 7, (int)((double)altezza_forma / 4000 * 60 + 15));
            }
            Info.BringToFront();
            taglia_piccola = new Size((int)(Convert.ToDouble(larghezza_forma) * 0.02), (int)(1000 * 0.03));
            taglia_grande = new Size((int)(Convert.ToDouble(larghezza_forma) * 0.03), (int)(1000 * 0.045));
            button_precedente.SetBounds((int)(Convert.ToDouble(larghezza_forma) * (0.2 - 0.15)), (int)(Convert.ToDouble(altezza_forma) * 0.06 + 6), taglia_piccola.Width, taglia_piccola.Height);
            button_successivo.SetBounds((int)(Convert.ToDouble(larghezza_forma) * (0.7 - 0.15)), (int)(Convert.ToDouble(altezza_forma) * 0.06 + 6), taglia_piccola.Width, taglia_piccola.Height);

            label_mese.SetBounds((int)(Convert.ToDouble(larghezza_forma) * 0.07), (int)(Convert.ToDouble(altezza_forma) * 0.036), (int)(Convert.ToDouble(larghezza_forma) * 0.3), (int)(Convert.ToDouble(altezza_forma) * 0.12));
            label_anno.SetBounds((int)(Convert.ToDouble(larghezza_forma) * 0.40 - 50000 / larghezza_forma), (int)(Convert.ToDouble(altezza_forma) * 0.036), (int)(Convert.ToDouble(larghezza_forma) * 0.2), (int)(Convert.ToDouble(altezza_forma) * 0.12));
            label_mese.Font = new System.Drawing.Font(font1, (int)((double)larghezza_forma * 0.018 + 25), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label_anno.Font = new System.Drawing.Font(font1, (int)((double)larghezza_forma * 0.018 + 25), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
        }
    }
}
