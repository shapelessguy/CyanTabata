using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public partial class Form1 : Form
    {
        public Pannello1 panel_principale;
        public Pannello2 panel_schede;
        public Pannello3 panel_cronologia;
        public Pannello4 panel_statistiche;
        public MenuStrip Menù;
        private ToolStripMenuItem Assistente;
        private ToolStripMenuItem Schede;
        private ToolStripMenuItem Cronologia;
        private ToolStripMenuItem Statistiche;
        private ToolStripMenuItem Impostazioni_;
        public static Size actSize = new Size(0, 0);
        public Form1()
        {
            Program.principale = this;
            //Definizione Menù
            Menù = new MenuStrip()
            {
                GripStyle = ToolStripGripStyle.Visible,
                BackColor = Color.LightGray,
                ImageScalingSize = new System.Drawing.Size(24, 24),
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(2435, 33),
            };
            Controls.Add(Menù);
            Assistente = new ToolStripMenuItem() { Size = new System.Drawing.Size(90, 33), Text = "Assistente", Image = new Bitmap((System.Drawing.Image)(Properties.Resources.ResourceManager.GetObject("Peso"))), };
            Schede = new ToolStripMenuItem() { Size = new System.Drawing.Size(90, 33), Text = "Schede", Image = new Bitmap((System.Drawing.Image)(Properties.Resources.ResourceManager.GetObject("Documenti"))), };
            Cronologia = new ToolStripMenuItem() { Size = new System.Drawing.Size(90, 33), Text = "Cronologia", Image = new Bitmap((System.Drawing.Image)(Properties.Resources.ResourceManager.GetObject("Calendar"))), };
            Statistiche = new ToolStripMenuItem() { Size = new System.Drawing.Size(90, 33), Text = "Statistiche", Image = new Bitmap((System.Drawing.Image)(Properties.Resources.ResourceManager.GetObject("Statistics"))), };
            Impostazioni_ = new ToolStripMenuItem() { Size = new System.Drawing.Size(90, 33), Text = "Impostazioni", Image = new Bitmap((System.Drawing.Image)(Properties.Resources.ResourceManager.GetObject("Ingranaggio"))), };
            Menù.Items.AddRange(new ToolStripItem[] { Assistente, Schede, Cronologia, Statistiche, Impostazioni_ });
            Assistente.Click += AssistenteClick;
            Schede.Click += SchedeClick;
            Cronologia.Click += CronologiaClick;
            Statistiche.Click += StatisticheClick;
            Impostazioni_.Click += ImpostazioniClick;
            Schede.DropDownItems.AddRange(new ToolStripItem[] { });

            InitializeComponent();

            panel_principale = new Pannello1();
            panel_principale.Size = new Size(this.Width, this.Height - 20);

            panel_schede = new Pannello2();
            panel_cronologia = new Pannello3();
            panel_statistiche = new Pannello4();
            ResizeEnd += new System.EventHandler(LocationChanging);
            ClientSize = new System.Drawing.Size(1084, 661);
            Controls.Add(panel_principale);
            Controls.Add(panel_schede);
            Controls.Add(panel_cronologia);
            Controls.Add(panel_statistiche);
            LocationChanging();
            Timer resizeTimer = new Timer() { Enabled = true, Interval = 100 };
            resizeTimer.Tick += (o, e) =>
            {
                if (Size != actSize) LocationChanging();
            };
        }
        private void LocationChanging(object sender, EventArgs e)
        {
            LocationChanging();
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
                if (m.Msg == 0x0112)
                {
                    if (m.WParam == new IntPtr(0xF030) || m.WParam == new IntPtr(0xF032))
                    {
                        LocationChanging();
                    }
                }
                if (m.Msg == 0x0112)
                {
                    if (m.WParam == new IntPtr(0xF120) || m.WParam == new IntPtr(0xF122))
                    {
                        LocationChanging();
                    }
                }
            }
            catch (Exception) { Close(); }
        }
        public void LocationChanging()
        {
            actSize = Size;
            panel_principale.Size = new Size(this.Width, this.Height - 20);
            panel_schede.Size = new Size(this.Width, this.Height - 20);
            panel_cronologia.Size = new Size(this.Width, this.Height - 20);
            panel_statistiche.Size = new Size(this.Width, this.Height - 20);
            Menù.Size = new Size(Width, (int)(Height * 0.02));
            panel_principale.Location = new Point(0, 0);
            panel_cronologia.Location = new Point(0, 0);
            panel_statistiche.Location = new Point(0, 0);
            panel_schede.Location = new Point(0, 0);
            panel_principale.LocationChanging();
            panel_schede.LocationChanging();
            panel_cronologia.LocationChanging();
            panel_statistiche.LocationChanging();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (SottoPanel panel in panel_schede.panel_schede.sottopannelli)
            {
                if(!panel.saved) panel.Save_Scheda(); 
            }
            Program.Saving();
        }

        private void ImpostazioniClick(object sender, EventArgs e)
        {
            if (!Impostazioni.imp_on)
            {
                new Impostazioni();
            }
            else { Impostazioni.impostazioni.BringToFront(); Impostazioni.impostazioni.Focus(); }
        }
        private void AssistenteClick(object sender, EventArgs e)
        {
            //if (!panel_principale.Visible)
            {
                NascondiTutto();
                panel_principale.Visible = true;
            }
        }
        private void SchedeClick(object sender, EventArgs e)
        {
            //if (!panel_schede.Visible)
            {
                NascondiTutto();
                panel_schede.Visible = true;
                panel_principale.InizializzaNull();
            }
        }
        private void CronologiaClick(object sender, EventArgs e)
        {
            //if (!panel_cronologia.Visible)
            {
                NascondiTutto();
                panel_cronologia.Visible = true;
                panel_principale.InizializzaNull();
            }
        }
        private void StatisticheClick(object sender, EventArgs e)
        {
            //if (!panel_cronologia.Visible)
            {
                NascondiTutto();
                panel_statistiche.Aggiorna();
                panel_statistiche.Visible = true;
                panel_principale.InizializzaNull();
            }
        }

        private void NascondiTutto()
        {
            panel_principale.Visible = false;
            panel_schede.Visible = false;
            panel_cronologia.Visible = false;
            panel_statistiche.Visible = false;
        }
    }
}
