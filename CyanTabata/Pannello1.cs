using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CyanTabata
{
    public class Pannello1 : Panel
    {

        private CircularProgressBar.CircularProgressBar circularProgressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label10;

        static bool decimale = false;
        static public Archivio arc;
        public Timer timer, timer_ora;
        public Scheda scheda;
        public Pannello_Schede panel_schede;
        public List<int> steps = new List<int>();
        public int steps_tot = 0;
        int massima_iterazione;
        int iterazione_progresso;
        int index;
        int[] data_fine; int[] data_attuale;
        static public bool pronto = false;

        static public readonly List<Stream> Audio = new List<Stream>()
        {
            Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, Properties.Resources._5,
        Properties.Resources._6 ,Properties.Resources._7 ,Properties.Resources._8 ,Properties.Resources._9 ,Properties.Resources._10 ,
        Properties.Resources._11 ,Properties.Resources._12 ,Properties.Resources._13 ,Properties.Resources._14 ,Properties.Resources._15 ,
        Properties.Resources._16 ,Properties.Resources._17 ,Properties.Resources._18 ,Properties.Resources._19 ,Properties.Resources._20
        };
        static public readonly List<Stream> Audio_soft = new List<Stream>()
        {
            Properties.Resources._1_soft, Properties.Resources._2_soft, Properties.Resources._3_soft, Properties.Resources._4_soft, Properties.Resources._5_soft,
        Properties.Resources._6_soft ,Properties.Resources._7_soft ,Properties.Resources._8_soft ,Properties.Resources._9_soft ,Properties.Resources._10_soft ,
        Properties.Resources._11_soft ,Properties.Resources._12_soft ,Properties.Resources._13_soft ,Properties.Resources._14_soft ,Properties.Resources._15_soft ,
        Properties.Resources._16_soft ,Properties.Resources._17_soft ,Properties.Resources._18_soft ,Properties.Resources._19_soft ,Properties.Resources._20_soft
        };

        static public readonly Stream Nullo = Properties.Resources.nullo;
        static public System.Media.SoundPlayer nullo_ = new System.Media.SoundPlayer(Nullo);

        static public readonly Stream Partenza = Properties.Resources.partenza;
        static public System.Media.SoundPlayer Partenza_ = new System.Media.SoundPlayer(Partenza);

        static public readonly Stream Riposo = Properties.Resources.riposo;
        static public System.Media.SoundPlayer Riposo_ = new System.Media.SoundPlayer(Riposo);

        static public readonly Stream Traguardo = Properties.Resources.champion;
        static public System.Media.SoundPlayer Traguardo_ = new System.Media.SoundPlayer(Traguardo);

        static public readonly Stream Traguardo1s = Properties.Resources.t1;
        static public System.Media.SoundPlayer Traguardo1 = new System.Media.SoundPlayer(Traguardo1s);

        static public readonly Stream Traguardo2s = Properties.Resources.t2;
        static public System.Media.SoundPlayer Traguardo2 = new System.Media.SoundPlayer(Traguardo2s);

        static public readonly Stream Traguardo3s = Properties.Resources.t3;
        static public System.Media.SoundPlayer Traguardo3 = new System.Media.SoundPlayer(Traguardo3s);

        static public readonly Stream Traguardo4s = Properties.Resources.t4;
        static public System.Media.SoundPlayer Traguardo4 = new System.Media.SoundPlayer(Traguardo4s);

        static public readonly Stream Traguardo5s = Properties.Resources.t5;
        static public System.Media.SoundPlayer Traguardo5 = new System.Media.SoundPlayer(Traguardo5s);

        static public readonly Stream Traguardo10s = Properties.Resources.t10;
        static public System.Media.SoundPlayer Traguardo10 = new System.Media.SoundPlayer(Traguardo10s);

        static public readonly Stream Traguardo20s = Properties.Resources.t20;
        static public System.Media.SoundPlayer Traguardo20 = new System.Media.SoundPlayer(Traguardo20s);

        static public readonly Stream Traguardo30s = Properties.Resources.t30;
        static public System.Media.SoundPlayer Traguardo30 = new System.Media.SoundPlayer(Traguardo30s);


        static public readonly Stream Nullo_soft = Properties.Resources.nullo_soft;
        static public System.Media.SoundPlayer nullo__soft = new System.Media.SoundPlayer(Nullo_soft);

        static public readonly Stream Partenza_soft = Properties.Resources.partenza_soft;
        static public System.Media.SoundPlayer Partenza__soft = new System.Media.SoundPlayer(Partenza_soft);

        static public readonly Stream Riposo_soft = Properties.Resources.riposo_soft;
        static public System.Media.SoundPlayer Riposo__soft = new System.Media.SoundPlayer(Riposo_soft);

        static public readonly Stream Traguardo_soft = Properties.Resources.champion_soft;
        static public System.Media.SoundPlayer Traguardo__soft = new System.Media.SoundPlayer(Traguardo_soft);

        static public readonly Stream Traguardo1s_soft = Properties.Resources.t1_soft;
        static public System.Media.SoundPlayer Traguardo1_soft = new System.Media.SoundPlayer(Traguardo1s_soft);

        static public readonly Stream Traguardo2s_soft = Properties.Resources.t2_soft;
        static public System.Media.SoundPlayer Traguardo2_soft = new System.Media.SoundPlayer(Traguardo2s_soft);

        static public readonly Stream Traguardo3s_soft = Properties.Resources.t3_soft;
        static public System.Media.SoundPlayer Traguardo3_soft = new System.Media.SoundPlayer(Traguardo3s_soft);

        static public readonly Stream Traguardo4s_soft = Properties.Resources.t4_soft;
        static public System.Media.SoundPlayer Traguardo4_soft = new System.Media.SoundPlayer(Traguardo4s_soft);

        static public readonly Stream Traguardo5s_soft = Properties.Resources.t5_soft;
        static public System.Media.SoundPlayer Traguardo5_soft = new System.Media.SoundPlayer(Traguardo5s_soft);

        static public readonly Stream Traguardo10s_soft = Properties.Resources.t10_soft;
        static public System.Media.SoundPlayer Traguardo10_soft = new System.Media.SoundPlayer(Traguardo10s_soft);

        static public readonly Stream Traguardo20s_soft = Properties.Resources.t20_soft;
        static public System.Media.SoundPlayer Traguardo20_soft = new System.Media.SoundPlayer(Traguardo20s_soft);

        static public readonly Stream Traguardo30s_soft = Properties.Resources.t30_soft;
        static public System.Media.SoundPlayer Traguardo30_soft = new System.Media.SoundPlayer(Traguardo30s_soft);


        static public List<System.Media.SoundPlayer> Sounds = new List<System.Media.SoundPlayer>();
        static public List<System.Media.SoundPlayer> Sounds_soft = new List<System.Media.SoundPlayer>();
        public Pannello1()
        {
            for (int i = 0; i < 20; i++) Sounds.Add(new System.Media.SoundPlayer(Audio[i]));
            for (int i = 0; i < 20; i++) Sounds_soft.Add(new System.Media.SoundPlayer(Audio_soft[i]));
            Change_Color(false);
            Initialize_Components();
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.circularProgressBar1);

            KeyDown += new System.Windows.Forms.KeyEventHandler(Hotkeys);
            Click += Focuss;
            pictureBox1.Controls.Add(label7);
            label7.Location = new Point(0, label7.Height);
            arc = new Archivio();
            timer_ora = new Timer()
            {
                Interval = 1000,
                Enabled = true,
            };
            timer_ora.Tick += Timer_ora;
            timer = new Timer()
            {
                Interval = 100,
                Enabled = true,
            };
            InizializzaNull();
            Aggiorna_PannelloSchede();
            foreach (Control controllo in Controls) { controllo.KeyDown += new System.Windows.Forms.KeyEventHandler(Hotkeys); controllo.Click += Focuss; }
            listBox1.DoubleClick += DoppioClick;
        }
        public void Aggiorna_PannelloSchede()
        {
            Controls.Remove(panel_schede);
            panel_schede = new Pannello_Schede(arc.Schede, 1);
            foreach (Control controllo in panel_schede.Controls) controllo.KeyDown += new System.Windows.Forms.KeyEventHandler(Hotkeys);
            Controls.Add(panel_schede);
            if(pronto) LocationChanging();
            Pannello3.eventi.Clear();
            Pannello3.eventi = Eventi.GetEventi(Program.path + @"\Cronologia\");
            Pannello3.progressi.Clear();
            Pannello3.progressi = EvProgressi.GetProgressi(Program.path + @"\Progressi\");
            new Input();
        }
        public void InizializzaNull()
        {
            label1.Text = "Cyan";
            label2.Text = "";
            label3.Text = "";
            label5.Text = "";
            label6.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            Finished(false);
            listBox1.Items.Clear();
            circularProgressBar1.Text = "Tabata";
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            timer.Tick -= Progresso;
        }
        private void Initialize_Components()
        {
            this.circularProgressBar1 = new CircularProgressBar.CircularProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // circularProgressBar1
            // 
            this.circularProgressBar1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.ExponentialEaseOut;
            this.circularProgressBar1.AnimationSpeed = 0;
            this.circularProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.circularProgressBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 99.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularProgressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.circularProgressBar1.InnerColor = System.Drawing.SystemColors.ButtonFace;
            this.circularProgressBar1.InnerMargin = 2;
            this.circularProgressBar1.InnerWidth = -1;
            this.circularProgressBar1.Location = new System.Drawing.Point(594, 59);
            this.circularProgressBar1.MarqueeAnimationSpeed = 2000;
            this.circularProgressBar1.Name = "circularProgressBar1";
            this.circularProgressBar1.OuterColor = System.Drawing.Color.Gray;
            this.circularProgressBar1.OuterMargin = 0;
            this.circularProgressBar1.OuterWidth = 0;
            this.circularProgressBar1.ProgressColor = System.Drawing.Color.Red;
            this.circularProgressBar1.ProgressWidth = 100;
            this.circularProgressBar1.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.circularProgressBar1.Size = new System.Drawing.Size(1656, 1298);
            this.circularProgressBar1.StartAngle = 270;
            this.circularProgressBar1.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar1.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.circularProgressBar1.SubscriptText = "";
            this.circularProgressBar1.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar1.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.circularProgressBar1.SuperscriptText = "";
            this.circularProgressBar1.TabIndex = 0;
            this.circularProgressBar1.Text = "circularProgressBar1";
            this.circularProgressBar1.TextMargin = new System.Windows.Forms.Padding(8, 80, 0, 0);
            this.circularProgressBar1.Value = 100;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(868, 362);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1115, 264);
            this.label1.TabIndex = 1;
            this.label1.Text = "Esercizio super complicato Cali";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1197, 1389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 60);
            this.button1.TabIndex = 2;
            this.button1.TabStop = false;
            this.button1.Text = "<<";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Precedente);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1530, 1389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 60);
            this.button2.TabIndex = 3;
            this.button2.TabStop = false;
            this.button2.Text = ">>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Successivo);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1362, 1389);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 60);
            this.button3.TabIndex = 4;
            this.button3.TabStop = false;
            this.button3.Text = "Play";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Pausa);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 54F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2177, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(659, 199);
            this.label2.TabIndex = 5;
            this.label2.Text = "Esercizio super complicato Cali";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 36F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(2235, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(582, 92);
            this.label3.TabIndex = 6;
            this.label3.Text = "A seguire :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 36;
            this.listBox1.Location = new System.Drawing.Point(16, 458);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox1.Size = new System.Drawing.Size(533, 904);
            this.listBox1.TabIndex = 7;
            this.listBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(522, 82);
            this.label4.TabIndex = 8;
            this.label4.Text = "Serie";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Font = new System.Drawing.Font("Palatino Linotype", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2306, 1265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(479, 92);
            this.label5.TabIndex = 9;
            this.label5.Text = "Ora attuale";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Font = new System.Drawing.Font("Palatino Linotype", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2306, 1357);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(479, 92);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ora Fine";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(2458, 507);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(183, 732);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(2458, 624);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(182, 615);
            this.label7.TabIndex = 12;
            this.label7.Location = new Point(0, 0);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Palatino Linotype", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2399, 362);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(312, 142);
            this.label8.TabIndex = 13;
            this.label8.Text = "Labelllllllll 4 minuti 30 secondi";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Font = new System.Drawing.Font("Palatino Linotype", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 1367);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(556, 92);
            this.label9.TabIndex = 14;
            this.label9.Text = "Durata totale: ";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label10.Font = new System.Drawing.Font("Palatino Linotype", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 362);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(537, 82);
            this.label10.TabIndex = 15;
            this.label10.Text = "Scheda...";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }
        private void DoppioClick(object sender, EventArgs e)
        {
            nullo_.Play();
            for (int i = 0; i < listBox1.Items.Count; i++) if (listBox1.SelectedItem == listBox1.Items[i]) index = i;
            Calcola_Percentuale();
            Calcola_Tempo_fine();
            Aggiorna_ListBox(index);
            Change_Color(true);
            if (listBox1.SelectionMode == SelectionMode.One) SelectFromListbox(index);
            massima_iterazione = scheda.tempo_esercizi[index] * (int)(1000 / timer.Interval);
            iterazione_progresso = -5;
            circularProgressBar1.Value = 0;
            double passo = (double)(massima_iterazione - 5) / (double)scheda.ripetizioni_esercizi[index];
            steps.Clear();
            for (int i = 0; i < scheda.ripetizioni_esercizi[index]; i++) steps.Add((int)(i * passo) + 2);
            steps_tot = scheda.ripetizioni_esercizi[index];
            if (scheda.focus_esercizi[index] == Scheda.convenzione_ripetizioni) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.ripetizioni_esercizi[index] + " rip";
            else if (scheda.focus_esercizi[index] == Scheda.convenzione_tempo) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.tempo_esercizi[index] + " sec";
            if (scheda.nomi_esercizi.Count > index + 1) label2.Text = scheda.nomi_esercizi[index + 1]; else label2.Text = "Fine";
            circularProgressBar1.Text = FormatoStandard((double)(1 - ((double)iterazione_progresso / (double)massima_iterazione)) * scheda.tempo_esercizi[index]);
            FontChanging();
        }
        private void Focuss(object sender, EventArgs e)
        {
            listBox1.Focus();
        }

        public void StartScheda(Scheda scheda)
        {
            this.scheda = scheda;
            timer.Enabled = true;
            timer_ora.Enabled = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            listBox1.Items.Clear();
            Aggiorna_ListBox(-1);
            Calcola_Tempo_fine();
            Change_Color(true);
            label7.Location = new Point(0, 0);
            int somma = 0;
            for (int i = 0; i < scheda.tempo_esercizi.Count; i++) somma += scheda.tempo_esercizi[i];
            somma = somma / 60;
            if (somma == 0) label9.Text = "Durata complessiva: < 1 min";
            else label9.Text = "Durata complessiva: " + somma + " min";
            steps.Clear();
            index = 0;
            iterazione_progresso = -5;
            button3.Text = "Pause";
            label3.Text = "A seguire :";
            if (scheda.focus_esercizi[0] == Scheda.convenzione_ripetizioni) label1.Text = scheda.nomi_esercizi[0] + " x" + scheda.ripetizioni_esercizi[0] + " rip";
            else if (scheda.focus_esercizi[0] == Scheda.convenzione_tempo) label1.Text = scheda.nomi_esercizi[0] + " x" + scheda.tempo_esercizi[0] + " sec";
            if (scheda.nomi_esercizi.Count > 1) label2.Text = scheda.nomi_esercizi[1]; else label2.Text = "Fine";
            circularProgressBar1.Text = FormatoStandard(scheda.tempo_esercizi[0]);
            circularProgressBar1.Value = 0;
            massima_iterazione = scheda.tempo_esercizi[0] * (int)(1000 / timer.Interval);
            timer.Tick += Progresso;

            double passo = (double)(massima_iterazione - 5) / (double)scheda.ripetizioni_esercizi[0];
            for (int i = 0; i < scheda.ripetizioni_esercizi[0]; i++) steps.Add((int)(i * passo) + 2);
            steps_tot = scheda.ripetizioni_esercizi[0];

        }

        private void Timer_ora(object sender, EventArgs e)
        {
            data_attuale = new int[] { DateTime.Now.Second, DateTime.Now.Minute, DateTime.Now.Hour, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year };
            string text1 = "", text2 = "";
            if (data_attuale[1] < 10) text2 = "0";
            if (data_attuale[2] < 10) text1 = "0";
            label5.Text = Convert.ToString("Orario " + text1 + Convert.ToString(data_attuale[2]) + ":" + text2 + Convert.ToString(data_attuale[1]));
            if (iterazione_progresso > 0) Calcola_Percentuale();
            if (label6.Text != "") Calcola_Tempo_fine();
            pronto = true;
        }
        private void Calcola_Percentuale()
        {
            if (circularProgressBar1.Text != "Finished") label8.Text = "Tempo rimanente:\n" + Date.Periodo_DataData(data_attuale, data_fine);
            int val = 0, somma = 0;
            for (int i = 0; i < scheda.tempo_esercizi.Count; i++) somma += scheda.tempo_esercizi[i];
            for (int i = 0; i < index; i++) val += scheda.tempo_esercizi[i];
            val += (int)((double)iterazione_progresso / 10) + 1;
            double frazione = 1 - (double)val / (double)somma;
            label7.Location = new Point(0, (int)(label7.Height * (1 - frazione)));
        }
        private void Calcola_Tempo_fine()
        {
            int somma = 0;
            for (int i = index; i < scheda.tempo_esercizi.Count; i++) if (i >= index) somma += scheda.tempo_esercizi[i];
            data_fine = Date.DataIncrement(data_attuale, (int)(somma * 1.09) + index - iterazione_progresso / 10);
            string text1 = "", text2 = "";
            if (data_fine[1] < 10) text2 = "0";
            if (data_fine[2] < 10) text1 = "0";
            label6.Text = Convert.ToString("Termine " + text1 + Convert.ToString(data_fine[2]) + ":" + text2 + Convert.ToString(data_fine[1]));
        }

        public void Progresso(object sender, EventArgs e)
        {
            if (iterazione_progresso < 0) { iterazione_progresso++; return; }
            if (iterazione_progresso == 0) Change_Color(true);
            if (iterazione_progresso == 1 && massima_iterazione > 60) if (scheda.nomi_esercizi[index].ToLower() == "riposo" || scheda.nomi_esercizi[index].ToLower() == "rest") if (Archivio.spartano) Riposo_.Play(); else Riposo__soft.Play();
            if (iterazione_progresso == massima_iterazione - 302 && massima_iterazione > 350 && scheda.focus_esercizi[index] == Scheda.convenzione_tempo && scheda.nomi_esercizi[index].ToLower() != "riposo" && scheda.nomi_esercizi[index].ToLower() != "rest") if (Archivio.spartano) Traguardo30.Play(); else Traguardo30_soft.Play();
            if (iterazione_progresso == massima_iterazione - 202 && massima_iterazione > 250 && scheda.focus_esercizi[index] == Scheda.convenzione_tempo && scheda.nomi_esercizi[index].ToLower() != "riposo" && scheda.nomi_esercizi[index].ToLower() != "rest") if (Archivio.spartano) Traguardo20.Play(); else Traguardo20_soft.Play();
            if (iterazione_progresso == massima_iterazione - 102 && massima_iterazione > 150 && scheda.focus_esercizi[index] == Scheda.convenzione_tempo && scheda.nomi_esercizi[index].ToLower() != "riposo" && scheda.nomi_esercizi[index].ToLower() != "rest") if (Archivio.spartano) Traguardo10.Play(); else Traguardo10_soft.Play();
            if (iterazione_progresso == massima_iterazione - 52 && massima_iterazione > 80 && scheda.focus_esercizi[index] == Scheda.convenzione_tempo) if (Archivio.spartano) Traguardo5.Play(); else Traguardo5_soft.Play();
            if (iterazione_progresso == massima_iterazione - 42 && massima_iterazione > 70 && scheda.focus_esercizi[index] == Scheda.convenzione_tempo) if (Archivio.spartano) Traguardo4.Play(); else Traguardo4_soft.Play();
            if (iterazione_progresso == massima_iterazione - 32 && massima_iterazione > 60 && scheda.focus_esercizi[index] == Scheda.convenzione_tempo) if (Archivio.spartano) Traguardo3.Play(); else Traguardo3_soft.Play();
            if (iterazione_progresso == massima_iterazione - 22 && massima_iterazione > 60 && scheda.focus_esercizi[index] == Scheda.convenzione_tempo) if (Archivio.spartano) Traguardo2.Play(); else Traguardo2_soft.Play();
            if (iterazione_progresso == massima_iterazione - 14 && massima_iterazione > 60 && scheda.focus_esercizi[index] == Scheda.convenzione_tempo) if (Archivio.spartano) Traguardo1.Play(); else Traguardo1_soft.Play();
            if (iterazione_progresso == massima_iterazione - 0) { try { if (scheda.nomi_esercizi[index + 1].ToLower() == "riposo" || scheda.nomi_esercizi[index + 1].ToLower() == "rest") { if (Archivio.spartano) Traguardo_.Play(); else Traguardo__soft.Play(); } else { if (Archivio.spartano) Partenza_.Play(); else Partenza__soft.Play(); } } catch (Exception) { if (Archivio.spartano) Traguardo_.Play(); else Traguardo__soft.Play(); } }
            if (steps.Contains(iterazione_progresso) && scheda.focus_esercizi[index] == Scheda.convenzione_ripetizioni) { if (steps_tot <= 20 && steps_tot > 0) { if (Archivio.spartano) Sounds[steps_tot - 1].Play(); else Sounds_soft[steps_tot - 1].Play(); } steps_tot--; }
            if (iterazione_progresso == massima_iterazione + 4)
            {
                steps.Clear();
                steps_tot = 0;
                timer.Tick -= Progresso;
                iterazione_progresso = -5;
                circularProgressBar1.Value = 0;
                FontChanging();
                if (index != scheda.nomi_esercizi.Count - 1)
                {
                    index++;
                    massima_iterazione = scheda.tempo_esercizi[index] * (int)(1000 / timer.Interval);
                    double passo = (double)(massima_iterazione - 5) / (double)scheda.ripetizioni_esercizi[index];
                    for (int i = 0; i < scheda.ripetizioni_esercizi[index]; i++) steps.Add((int)(i * passo) + 2);
                    steps_tot = scheda.ripetizioni_esercizi[index];
                    timer.Tick += Progresso;
                }
            }
            iterazione_progresso++;

            if (iterazione_progresso >= massima_iterazione + 2 && iterazione_progresso <= massima_iterazione + 4)
            {
                if (index + 1 == scheda.nomi_esercizi.Count)
                {
                    label1.Text = "Tabata";
                    label2.Text = "";
                    label6.Text = "";
                    label8.Text = "";
                    circularProgressBar1.Text = "Finished";
                    SaveCronologia();
                    Change_Color(false);
                    FontChanging();
                    label7.Location = new Point(0, label7.Height);
                    label7.Location = new Point(0, 0);
                    iterazione_progresso = -5;
                    timer.Tick -= Progresso;
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    listBox1.ClearSelected();
                    Aggiorna_ListBox(-1);
                }
                else
                {
                    Aggiorna_ListBox(index + 1);
                    if (listBox1.SelectionMode == SelectionMode.One)
                    {
                        SelectFromListbox(index + 1);
                    }
                    if (scheda.focus_esercizi[index + 1] == Scheda.convenzione_ripetizioni) label1.Text = scheda.nomi_esercizi[index + 1] + " x" + scheda.ripetizioni_esercizi[index + 1] + " rip";
                    else if (scheda.focus_esercizi[index + 1] == Scheda.convenzione_tempo) label1.Text = scheda.nomi_esercizi[index + 1] + " x" + scheda.tempo_esercizi[index + 1] + " sec";
                    FontChanging();
                    circularProgressBar1.Text = FormatoStandard(scheda.tempo_esercizi[index + 1]);
                }
            }
            else if (iterazione_progresso >= massima_iterazione && iterazione_progresso <= massima_iterazione + 2)
            {
                if (scheda.focus_esercizi[index] == Scheda.convenzione_ripetizioni) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.ripetizioni_esercizi[index] + " rip";
                else if (scheda.focus_esercizi[index] == Scheda.convenzione_tempo) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.tempo_esercizi[index] + " sec";
                circularProgressBar1.Text = FormatoStandard(0);
            }
            else
            {
                if (scheda.focus_esercizi[index] == Scheda.convenzione_ripetizioni) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.ripetizioni_esercizi[index] + " rip";
                else if (scheda.focus_esercizi[index] == Scheda.convenzione_tempo) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.tempo_esercizi[index] + " sec";
                if (scheda.nomi_esercizi.Count > index + 1) label2.Text = scheda.nomi_esercizi[index + 1]; else label2.Text = "Fine";
                circularProgressBar1.Text = FormatoStandard((double)(1 - ((double)(iterazione_progresso) / (double)massima_iterazione)) * scheda.tempo_esercizi[index]);
                if (circularProgressBar1.Value == 0) circularProgressBar1.Value = 1;
            }

            if (iterazione_progresso >= massima_iterazione || iterazione_progresso < 0) circularProgressBar1.Value = 100;
            else { circularProgressBar1.Value = (int)((double)(iterazione_progresso) / (double)massima_iterazione * 100); }
        }

        public void SelectFromListbox(int index)
        {
            listBox1.SetSelected(index, true);
            int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
            listBox1.TopIndex = listBox1.SelectedIndex - visibleItems/2;
        }
        public void Precedente()
        {
            nullo_.Play();
            if (index == 0) return;
            index--;
            Calcola_Percentuale();
            Calcola_Tempo_fine();
            Aggiorna_ListBox(index);
            Change_Color(true);
            if (listBox1.SelectionMode == SelectionMode.One) SelectFromListbox(index);
            massima_iterazione = scheda.tempo_esercizi[index] * (int)(1000 / timer.Interval);
            iterazione_progresso = -5;
            circularProgressBar1.Value = 0;
            double passo = (double)(massima_iterazione - 5) / (double)scheda.ripetizioni_esercizi[index];
            steps.Clear();
            for (int i = 0; i < scheda.ripetizioni_esercizi[index]; i++) steps.Add((int)(i * passo) + 2);
            steps_tot = scheda.ripetizioni_esercizi[index];
            if (scheda.focus_esercizi[index] == Scheda.convenzione_ripetizioni) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.ripetizioni_esercizi[index] + " rip";
            else if (scheda.focus_esercizi[index] == Scheda.convenzione_tempo) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.tempo_esercizi[index] + " sec";
            if (scheda.nomi_esercizi.Count > index + 1) label2.Text = scheda.nomi_esercizi[index + 1]; else label2.Text = "Fine";
            circularProgressBar1.Text = FormatoStandard((double)(1 - ((double)(iterazione_progresso) / (double)massima_iterazione)) * scheda.tempo_esercizi[index]);
            FontChanging();
        }
        public void Successivo()
        {
            nullo_.Play();
            if (index == scheda.nomi_esercizi.Count - 1)
            {
                Finished(true);
                return;
            }
            index++;
            Calcola_Percentuale();
            Calcola_Tempo_fine();
            Aggiorna_ListBox(index);
            Change_Color(true);
            if (listBox1.SelectionMode == SelectionMode.One) SelectFromListbox(index);
            massima_iterazione = scheda.tempo_esercizi[index] * (int)(1000 / timer.Interval);
            iterazione_progresso = -5;
            circularProgressBar1.Value = 0;
            double passo = (double)(massima_iterazione - 5) / (double)scheda.ripetizioni_esercizi[index];
            steps.Clear();
            for (int i = 0; i < scheda.ripetizioni_esercizi[index]; i++) steps.Add((int)(i * passo) + 2);
            steps_tot = scheda.ripetizioni_esercizi[index];
            if (scheda.focus_esercizi[index] == Scheda.convenzione_ripetizioni) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.ripetizioni_esercizi[index] + " rip";
            else if (scheda.focus_esercizi[index] == Scheda.convenzione_tempo) label1.Text = scheda.nomi_esercizi[index] + " x" + scheda.tempo_esercizi[index] + " sec";
            if (scheda.nomi_esercizi.Count > index + 1) label2.Text = scheda.nomi_esercizi[index + 1]; else label2.Text = "Fine";
            circularProgressBar1.Text = FormatoStandard((double)(1 - ((double)iterazione_progresso / (double)massima_iterazione)) * scheda.tempo_esercizi[index]);
            FontChanging();
        }

        private void Finished(bool real)
        {
            steps.Clear();
            steps_tot = 0;
            timer.Tick -= Progresso;
            iterazione_progresso = -5;
            circularProgressBar1.Value = 100;
            label1.Text = "Tabata";
            label2.Text = "";
            circularProgressBar1.Text = "Finished";
            label7.Location = new Point(0, label7.Height);
            label7.Location = new Point(0, 0);
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            listBox1.ClearSelected();
            try { Aggiorna_ListBox(-1); } catch (Exception) { };
            try { Change_Color(false); } catch (Exception) { };
            try { FontChanging(); } catch (Exception) { };
            if (real)
            {
                try
                {
                    SaveCronologia();
                }
                catch (Exception) { }
            }
        }
        void SaveCronologia()
        {
            using (StreamWriter ssw = File.CreateText(Program.path + @"\Cronologia\" + Convert.ToString(Date.Codifica(data_attuale)) + ".txt"))
            {
                ssw.Write("*Scheda "); ssw.WriteLine(scheda.nome_scheda);
                for (int i = 0; i < scheda.nomi_esercizi.Count; i++)
                {
                    ssw.Write("'" + scheda.nomi_esercizi[i] + "'");
                    ssw.Write("'" + scheda.tempo_esercizi[i] + "'");
                    if (scheda.ripetizioni_esercizi[i] > 0) ssw.Write("'" + scheda.ripetizioni_esercizi[i] + "rip'");
                    ssw.WriteLine("");
                }
                ssw.WriteLine("*");
                ssw.WriteLine("");
            }
            Pannello3.eventi.Clear();
            Pannello3.eventi = Eventi.GetEventi(Program.path + @"\Cronologia\");
            Program.principale.panel_cronologia.RefreshWindow();
        }
        public static string FormatoStandard(double numero)
        {
            if (!decimale) return Convert.ToString((int)numero);
            string numero_str = "";
            int num_cifre = ((int)numero).ToString().Length;
            if (numero >= 1000)
            {
                numero_str = Convert.ToString((int)numero);
                if (numero_str.Contains(",")) { } else numero_str += ".0";
                return numero_str.Replace(',', '.');
            }
            if (numero < 0.1) return "0.0";
            numero_str = Convert.ToString(numero);
            if (numero_str.IndexOf(',') == -1)
            {
                if (numero_str.Contains(",")) { } else numero_str += ".0";
                return numero_str.Replace(',', '.');
            }
            else
            {
                numero_str = numero_str.Substring(0, numero_str.IndexOf(',') + 2).Replace(',', '.');
            }
            if (numero_str.Contains(".")) { } else numero_str += ".0";
            return numero_str.Replace(',', '.');
        }

        private void Aggiorna_ListBox(int index)
        {
            listBox1.SelectionMode = SelectionMode.One;
            if (scheda == null || scheda.nomi_esercizi == null) return;
            for (int i = 0; i < scheda.nomi_esercizi.Count; i++)
            {
                if (listBox1.Items.Count < i + 1) listBox1.Items.Add("");

                string ausiliare = "";
                if (scheda.focus_esercizi[i] == Scheda.convenzione_ripetizioni) ausiliare += " x" + scheda.ripetizioni_esercizi[i] + " rip";
                else ausiliare += " - " + scheda.tempo_esercizi[i] + " sec";

                if (i == index) { listBox1.Items[i] = "\u2192   " + scheda.nomi_esercizi[i] + ausiliare; listBox1.SelectedItem = listBox1.Items[i]; }
                else listBox1.Items[i] = "     " + scheda.nomi_esercizi[i] + ausiliare;
            }
        }

        public void Hotkeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && button3.Visible) { Successivo(); e.SuppressKeyPress = true; }
            if (e.KeyCode == Keys.Left && button1.Visible) { Precedente(); e.SuppressKeyPress = true; }
            if (e.KeyCode == Keys.Space && button2.Visible) { Pausa(sender, e); e.SuppressKeyPress = true; }
        }
        private void Precedente(object sender, EventArgs e)
        {
            Precedente();
        }

        private void Successivo(object sender, EventArgs e)
        {
            Successivo();
        }

        private void Change_Color(bool reale)
        {
            try
            {
                if (circularProgressBar1 == null) return;
                if (!reale) { circularProgressBar1.ProgressColor = Color.Red; return; }
                if (scheda.focus_esercizi[index] == Scheda.convenzione_tempo) circularProgressBar1.ProgressColor = Color.DarkOrange;
                else if (scheda.focus_esercizi[index] == Scheda.convenzione_ripetizioni) circularProgressBar1.ProgressColor = Color.Red;
                if (scheda.nomi_esercizi[index].ToLower() == "riposo" || scheda.nomi_esercizi[index].ToLower() == "rest") circularProgressBar1.ProgressColor = Color.Green;
            }
            catch (Exception) { }
        }


        private void Pausa(object sender, EventArgs e)
        {
            nullo_.Play();
            int somma = 0;
            for (int i = index; i < scheda.tempo_esercizi.Count; i++) somma += scheda.tempo_esercizi[i];
            somma -= iterazione_progresso / 10;
            if (button3.Text == "Play") { timer.Enabled = true; button3.Text = "Pause"; data_fine = Date.DataIncrement(data_attuale, ((int)(somma * 1.09) + scheda.tempo_esercizi.Count - index)); }
            else { timer.Enabled = false; button3.Text = "Play"; }
        }
        private void SaveData()
        {
            arc.lista_date.Add(Convert.ToString(Date.Codifica(data_attuale)) + " " + scheda.nome_scheda);
        }


        public void LocationChanging()
        {
            label4.Location = new Point((int)(Width * 0.006), (int)(Height * 0.006) + 33);
            label4.Size = new Size((int)(Width * 0.18), (int)(Height * 0.06));
            panel_schede.Location = new Point((int)(Width * 0.006), (int)(label4.Location.Y * 2 + label4.Height));
            panel_schede.Size = new Size((int)(Width * 0.18), (int)(Height * 0.2));
            panel_schede.Resize_Elements();
            label10.Location = new Point((int)(Width * 0.006), (int)(panel_schede.Location.Y + panel_schede.Height + Height * 0.002 + 10));
            label10.Size = new Size((int)(Width * 0.18), (int)(Height * 0.06));
            label9.Location = new Point((int)(Width * 0.006), (int)(Height * 0.94) - 20);
            label9.Size = new Size((int)(Width * 0.26), (int)((Height - label9.Location.Y) - 20));
            listBox1.Location = new Point((int)(Width * 0.006), (int)(label10.Location.Y + label10.Height + 4));
            listBox1.Size = new Size((int)(Width * 0.18), (int)(label9.Location.Y - listBox1.Location.Y));
            label3.Location = new Point((int)(Width * 0.77), (int)(Height * 0.006)+33);
            label3.Size = new Size((int)(Width * 0.2), (int)(Height * 0.06));
            circularProgressBar1.Location = new Point((int)(label4.Location.X * 2 + label4.Width), (int)(Height * 0.006) + 33);
            circularProgressBar1.Size = new Size((int)((Width - label4.Location.X * 2 - label4.Width - label3.Width) * 0.98), (int)(Height * 0.9 - 20) - 33);
            label1.Size = new Size((int)(Width * 0.5 - 250), (int)(Height * 0.2));
            label1.Location = new Point((int)(circularProgressBar1.Location.X + circularProgressBar1.Width / 2 - label1.Width / 2), (int)(circularProgressBar1.Location.Y + circularProgressBar1.Height * 0.31));
            button1.Size = new Size((int)(Width * 0.04), (int)(Height * 0.05));
            button2.Size = button1.Size; button3.Size = button1.Size;
            button3.Location = new Point((int)(circularProgressBar1.Location.X + circularProgressBar1.Width / 2 - button1.Width / 2), (int)(Height * 0.92 - 20));
            button1.Location = new Point((int)(button3.Location.X - button1.Width - Width * 0.01), (int)(button3.Location.Y));
            button2.Location = new Point((int)(button3.Location.X + button3.Width + Width * 0.01), (int)(button3.Location.Y));
            label2.Location = new Point((int)(Width - label3.Width * 1.3), (int)(label3.Location.Y + label3.Height));
            label2.Size = new Size((int)(Width - label2.Location.X - 10), (int)(Height * 0.13));
            label8.Location = new Point((int)(Width * 0.86), (int)(label2.Location.Y + label2.Height + Height * 0.01));
            label8.Size = new Size((int)(Width * 0.07), (int)(Height * 0.12));
            label5.Size = new Size((int)(Width * 0.21), (int)((Height * 0.05)));
            label6.Size = label5.Size;
            label6.Location = new Point((int)(Width - label6.Width), (int)(Height * 0.95 - label6.Height - 20));
            label5.Location = new Point((int)(label6.Location.X), (int)(label6.Location.Y - label6.Height));
            pictureBox1.Location = new Point((int)(label8.Location.X + label8.Width / 2 - Width * 0.05), (int)(label8.Location.Y + label8.Height + Height * 0.005));
            pictureBox1.Size = new Size((int)(Width * 0.1), (int)((label5.Location.Y - pictureBox1.Location.Y) * 0.95));
            label7.Size = pictureBox1.Size;
            label7.Location = new Point(0, 0);
            label4.Font = new Font("Arial", (int)(Height * 0.03 + 3), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Font = new Font("Arial", (int)(Height * 0.015 + 3), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Font = new Font("Arial", (int)(Height * 0.07 - 25), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Font = new Font("Arial", (int)(Height * 0.02), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Font = label5.Font;
            label8.Font = new Font("Arial", (int)(Height * 0.014), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Font = new Font("Arial", (int)(Height * 0.02), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.Font = new Font("Arial", (int)(Height * 0.02 - 5), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listBox1.Font = new Font("Arial", (int)(Width * 0.0076 - 2), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button1.Font = new Font("Arial", (int)(Height * 0.01), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            circularProgressBar1.Font = new Font("Arial", (int)(Width * 0.03) + 30, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            circularProgressBar1.TextMargin = new Padding(0, (int)(Height * 0.1), 0, 0);
            button2.Font = button1.Font;
            button3.Font = button1.Font;
            panel_schede.Resize_Elements();
            FontChanging();

        }
        private void FontChanging()
        {
            int size1 = (int)(Height * 0.07 - 20);
            int size2 = (int)(Height * 0.04 - 10);
            if (size1 < 8) size1 = 8;
            if (size2 < 8) size2 = 8;
            if (label1.Text.Length < 28) label1.Font = new Font("Arial", size1, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            else label1.Font = new Font("Arial", (int)(Height * 0.05 - 15), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (label2.Text.Length < 28) label2.Font = new Font("Arial", size2, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            else label2.Font = new Font("Arial", (int)(Height * 0.03 - 5), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
