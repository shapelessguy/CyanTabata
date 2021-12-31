using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public partial class Compila : Form
    {
        private int[] data;
        bool compilato = false;
        EvProgressi progresso = new EvProgressi();
        Timer timer;
        public Compila(int[] data, EvProgressi progresso)
        {
            InitializeComponent();
            Text = "Compila Scheda Progressi  ->  " + data[0] + " / " + data[1] + " / " + data[2];
            Show();
            this.data = data;
            FormClosing += Save;
            string giorno = "";
            if (data[0] < 10) giorno += "0";
            string mese = "";
            if (data[1] < 10) mese += "0";
            giorno += data[0].ToString();
            mese += data[1].ToString();
            this.progresso.datacode = data[2].ToString() + mese + giorno;
            label9.Text = "None";

            if (progresso != null) LoadAttributi(progresso);

            timer = new Timer()
            {
                Enabled = true,
                Interval = 200,
            };
            timer.Tick += Check;
        }

        void LoadAttributi(EvProgressi progresso)
        {
            textBox1.Text = progresso.peso;
            textBox2.Text = progresso.impegno;
            textBox26.Text = progresso.kcal;
            label9.Text = progresso.multimedia;
            if (label9.Text == "") label9.Text = "Choose a multimedial file";
            textBox3.Text = progresso.esercizio_preferito;
            textBox4.Text = progresso.serie;
            textBox5.Text = progresso.ripetizioni;
            textBox6.Text = progresso.carico;
            textBox7.Text = progresso.tempo;
            List<int> indici = new List<int>();
            List<int> indici_mancanti = new List<int>();
            for (int i = 0; i < progresso.misure_txt.Length; i++)
            {
                if (progresso.misure_txt[i] == label16.Text) { textBox8.Text = progresso.misure[i]; indici.Add(i); }
                if (progresso.misure_txt[i] == label17.Text) { textBox11.Text = progresso.misure[i]; indici.Add(i); }
                if (progresso.misure_txt[i] == label18.Text) { textBox12.Text = progresso.misure[i]; indici.Add(i); }
                if (progresso.misure_txt[i] == label19.Text) { textBox13.Text = progresso.misure[i]; indici.Add(i); }
                if (progresso.misure_txt[i] == label22.Text) { textBox16.Text = progresso.misure[i]; indici.Add(i); }
                if (progresso.misure_txt[i] == label23.Text) { textBox17.Text = progresso.misure[i]; indici.Add(i); }
            }
            int ind_max =0;
            for (int i = 0; i < progresso.misure_txt.Length; i++) { if (progresso.misure_txt[i]!="") ind_max++; }
            for (int i = 0; i < ind_max; i++) { if (!indici.Contains(i)) indici_mancanti.Add(i); }
            List<TextBox> nomi_misure = new List<TextBox>()
            {
                textBox9,textBox10,textBox22,textBox23,textBox24,textBox25,
            };
            List<TextBox> valori_misure = new List<TextBox>()
            {
                textBox15,textBox14,textBox21,textBox20,textBox19,textBox18,
            };
            for (int i=0; i<indici_mancanti.Count; i++)
            {
                if (progresso.misure_txt[indici_mancanti[i]].Length > 1)
                {
                    nomi_misure[i].Text = progresso.misure_txt[indici_mancanti[i]].Substring(0, progresso.misure_txt[indici_mancanti[i]].Length - 2);
                    valori_misure[i].Text = progresso.misure[indici_mancanti[i]];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            compilato = true;
            button1.Focus();
            Check(sender, e);
            Close();
            this.Dispose();
        }

        private void Save(object sender, EventArgs e)
        {
            if (compilato)
            {
                progresso.peso = textBox1.Text;
                progresso.impegno = textBox2.Text;
                progresso.kcal = textBox26.Text;
                if(label9.Text!="None") progresso.multimedia = label9.Text;
                progresso.esercizio_preferito = textBox3.Text;
                progresso.serie = textBox4.Text;
                progresso.ripetizioni = textBox5.Text;
                progresso.carico = textBox6.Text;
                progresso.tempo = textBox7.Text;
                progresso.misure = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
                progresso.misure_txt = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
                progresso.misure_txt[0] = label16.Text;
                progresso.misure_txt[1] = label17.Text;
                progresso.misure_txt[2] = label18.Text;
                progresso.misure_txt[3] = label19.Text;
                progresso.misure_txt[4] = label22.Text;
                progresso.misure_txt[5] = label23.Text;
                progresso.misure_txt[6] = textBox9.Text;
                progresso.misure_txt[7] = textBox10.Text;
                progresso.misure_txt[8] = textBox22.Text;
                progresso.misure_txt[9] = textBox23.Text;
                progresso.misure_txt[10] = textBox24.Text;
                progresso.misure_txt[11] = textBox25.Text;
                for (int i = 6; i < 12; i++) if (progresso.misure_txt[i] != "") progresso.misure_txt[i] += ": ";
                progresso.misure[0] = textBox8.Text;
                progresso.misure[1] = textBox11.Text;
                progresso.misure[2] = textBox12.Text;
                progresso.misure[3] = textBox13.Text;
                progresso.misure[4] = textBox16.Text;
                progresso.misure[5] = textBox17.Text;
                progresso.misure[6] = textBox15.Text;
                progresso.misure[7] = textBox14.Text;
                progresso.misure[8] = textBox21.Text;
                progresso.misure[9] = textBox20.Text;
                progresso.misure[10] = textBox19.Text;
                progresso.misure[11] = textBox18.Text;


                progresso.Save();
                progresso.Componi_Scheda();
                Pannello3.progressi.Clear();
                Pannello3.progressi = EvProgressi.GetProgressi(Program.path + @"\Progressi\");
                Program.principale.panel_cronologia.RefreshWindow();
                Program.principale.panel_cronologia.Info.AggiornaProgresso(progresso);
            }
        }
        void Check(object sender, EventArgs e)
        {
            if (textBox3.Text != "") { textBox4.ReadOnly = false; textBox5.ReadOnly = false; textBox6.ReadOnly = false; textBox7.ReadOnly = false; }
            else { textBox4.ReadOnly = true; textBox5.ReadOnly = true; textBox6.ReadOnly = true; textBox7.ReadOnly = true; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; }
            if (textBox9.Text != "") { textBox15.ReadOnly = false; } else { textBox15.ReadOnly = true; textBox15.Text = ""; }
            if (textBox10.Text != "") { textBox14.ReadOnly = false; } else { textBox14.ReadOnly = true; textBox14.Text = ""; }
            if (textBox22.Text != "") { textBox21.ReadOnly = false;  } else { textBox21.ReadOnly = true; textBox21.Text = ""; }
            if (textBox23.Text != "") { textBox20.ReadOnly = false;} else { textBox20.ReadOnly = true; textBox20.Text = ""; }
            if (textBox24.Text != "") { textBox19.ReadOnly = false;} else { textBox19.ReadOnly = true; textBox19.Text = ""; }
            if (textBox25.Text != "") { textBox18.ReadOnly = false; } else { textBox18.ReadOnly = true; textBox18.Text = ""; }

            float result; int resulti;
            if (textBox1.Text.Length > 5) { textBox1.Text = textBox1.Text.Substring(0, 5); textBox1.SelectionStart = textBox1.Text.Length; }
            if (!textBox1.Focused) if(float.TryParse(textBox1.Text.Replace(",", "."), out result)) textBox1.Text = textBox1.Text.Replace(",", "."); else { textBox1.Text = ""; }

            if (textBox2.Text.Length > 2) { textBox2.Text = textBox2.Text.Substring(0, 2); textBox2.SelectionStart = textBox2.Text.Length; }
            if (!textBox2.Focused) if (int.TryParse(textBox2.Text, out resulti)) { if (resulti > 10) textBox2.Text = ""; } else { textBox2.Text = ""; }

            if (textBox26.Text.Length > 5) { textBox26.Text = textBox26.Text.Substring(0, 5); textBox26.SelectionStart = textBox26.Text.Length; }
            if (!textBox26.Focused) if (int.TryParse(textBox26.Text, out resulti)) { } else { textBox26.Text = ""; }

            if (textBox3.Text.Length > 32) { textBox3.Text = textBox3.Text.Substring(0, 32); textBox3.SelectionStart = textBox3.Text.Length; }

            if (textBox4.Text.Length > 2) { textBox4.Text = textBox4.Text.Substring(0, 2); textBox4.SelectionStart = textBox4.Text.Length; }
            if (!textBox4.Focused) if (int.TryParse(textBox4.Text, out resulti)) { } else { textBox4.Text = ""; }

            if (textBox5.Text.Length > 3) { textBox5.Text = textBox5.Text.Substring(0, 3); textBox5.SelectionStart = textBox5.Text.Length; }
            if (!textBox5.Focused) if (int.TryParse(textBox5.Text, out resulti)) { } else { textBox5.Text = ""; }

            if (textBox6.Text.Length > 5) { textBox6.Text = textBox6.Text.Substring(0, 5); textBox6.SelectionStart = textBox6.Text.Length; }
            if (!textBox6.Focused) if (float.TryParse(textBox6.Text.Replace(",", "."), out result)) textBox6.Text = textBox6.Text.Replace(",", "."); else { textBox6.Text = ""; }

            if (textBox7.Text.Length > 3) { textBox7.Text = textBox7.Text.Substring(0, 3); textBox7.SelectionStart = textBox7.Text.Length; }
            if (!textBox7.Focused) if (int.TryParse(textBox7.Text, out resulti)) { } else { textBox7.Text = ""; }

            List<TextBox> lista = new List<TextBox>();
            lista.Add(textBox9);
            lista.Add(textBox10);
            lista.Add(textBox22);
            lista.Add(textBox23);
            lista.Add(textBox24);
            lista.Add(textBox25);

            foreach(TextBox text in lista)
            {
                if (text.Text.Length > 12) { text.Text = text.Text.Substring(0, 12); text.SelectionStart = text.Text.Length; }
            }
            

            lista.Clear();
            lista.Add(textBox8);
            lista.Add(textBox11);
            lista.Add(textBox12);
            lista.Add(textBox13);
            lista.Add(textBox16);
            lista.Add(textBox17);
            lista.Add(textBox15);
            lista.Add(textBox14);
            lista.Add(textBox21);
            lista.Add(textBox20);
            lista.Add(textBox19);
            lista.Add(textBox18);
            foreach(TextBox text in lista)
            {
                if (text.Text.Length > 5) text.Text = text.Text.Substring(0, 5);
                if (!text.Focused) if (float.TryParse(text.Text.Replace(",", "."), out result)) text.Text = text.Text.Replace(",", "."); else { text.Text = ""; }
            }
        }

        private OpenFileDialog folderBrowserDialog1;
        private void label9_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new OpenFileDialog();
            try { folderBrowserDialog1.InitialDirectory = Archivio.video_path; } catch (Exception) { MessageBox.Show("Seleziona in impostazioni una cartella di file multimediali valida"); return; }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string namesafe = folderBrowserDialog1.SafeFileName;
                    string path = folderBrowserDialog1.FileName.Substring(0, folderBrowserDialog1.FileName.Length - namesafe.Length - 1);
                    if (path != Archivio.video_path) { MessageBox.Show("Seleziona un file multimediale nella cartella di default"); return; }
                    label9.Text = folderBrowserDialog1.SafeFileName;
                }
                catch (Exception) { MessageBox.Show("Non è possibile selezionare questo file a causa di mancata autorizzazione"); }
            }
        }
        
    }
}
