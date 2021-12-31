using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace CyanTabata
{
    public class SottoPanel : Panel
    {
        public int dim = 2;
        public Scheda scheda;
        public Label Formattazione;
        public int indice;
        public MyTextBox Scheda_TXT;
        public List<string> Info_righe = new List<string>();
        public bool saved = true;
        public SottoPanel(Scheda scheda, int indice)
        {
            this.scheda = scheda;
            this.indice = indice;
            dim = scheda.nomi_esercizi.Count;
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.White;
            Formattazione = new Label()
            {
                AutoSize = false,
                Text = "              Formattazione :  Nome_Esercizio   >   Tempo_Esercizio(secondi)   >   (Eventuali)Ripetizioni_Esercizio",
                Font = new Font(Font, FontStyle.Bold),
            };
            Controls.Add(Formattazione);
            //AutoScroll = true;
        }
        
        public void Creazione()
        {
            Scheda_TXT = new MyTextBox()
            {
                Multiline = true,
                AutoSize = true,
                Size = this.Size,
                BorderStyle = BorderStyle.None,
                ScrollBars = RichTextBoxScrollBars.Vertical,
            };
            Controls.Add(Scheda_TXT);
            Scheda_TXT.KeyDown += Scheda_TXT.KeyPressed;
            Scheda_TXT.LostFocus += FocusLost;
            Scheda_TXT.GotFocus += Scheda_TXT.Check;
            Scheda_TXT.TextChanged += Scheda_TXT.Check;
            for(int i=0; i<dim; i++)
            {
                if(i!=0) Scheda_TXT.Text += "\r\n";
                Scheda_TXT.Text += scheda.nomi_esercizi[i].Replace("\n", "").Replace("\r", "");
                Scheda_TXT.Text += "    >   " + scheda.tempo_esercizi[i];
                if(scheda.ripetizioni_esercizi[i]!=0) Scheda_TXT.Text += "    >   " + scheda.ripetizioni_esercizi[i];
            }
        }
        public void FocusLost(object sender, EventArgs e)
        {
            Save_Scheda();
        }

        public void SetInfo(List<string> info)
        {

        }
        
        public void Resize_Elements()
        {
            Scheda_TXT.Location = new Point(50, 14);
            Scheda_TXT.Size = new Size(this.Width-50, this.Height - 20);
            Formattazione.Size = new Size(this.Width, 14);
            Formattazione.Location = new Point(0, 0);

        }
        public void Save_Scheda()
        {
            if (!Pannello1.pronto) return;
            scheda.nomi_esercizi.Clear();
            scheda.tempo_esercizi.Clear();
            scheda.ripetizioni_esercizi.Clear();
            scheda.focus_esercizi.Clear();
            
            string[] righe = Scheda_TXT.Text.Split(new[] { '\n'});
            for (int i = 0; i < righe.Length; i++) righe[i] = righe[i].Replace("\n", "").Replace("\r", "");
            for (int j = 0; j < righe.Length; j++)
            {
                if (Info_righe[j].Contains("Errore") && Info_righe[j]!= "Errore lunghezza nome") continue;
                string stringa = righe[j];
                string[] pezzi = stringa.Split(new[] { '>', }); for (int i = 0; i < pezzi.Length; i++) pezzi[i] = pezzi[i].Replace(">", "");
                if (Info_righe[j].Contains("Errore lunghezza nome")) pezzi[0] = pezzi[0].Substring(0, 31);
                string nome = pezzi[0];  int tempo, rip=0;

                for(int i= pezzi[0].Length-1; i>=0; i--) { if (nome.Substring(i - 1, 1) != " ") { nome = pezzi[0].Substring(0, i+1); break; } }
                if (Info_righe[j].Contains("Riposo")) nome = "Riposo";
                scheda.nomi_esercizi.Add(nome);

                //Console.WriteLine(righe[j]);
                if (int.TryParse(pezzi[1], out tempo)) { scheda.tempo_esercizi.Add(tempo); }

                if (pezzi.Length > 2) { if (int.TryParse(pezzi[2], out rip)) { scheda.ripetizioni_esercizi.Add(rip); } else { scheda.ripetizioni_esercizi.Add(0); } } else { scheda.ripetizioni_esercizi.Add(0); }
                if (rip == 0) scheda.focus_esercizi.Add(Scheda.convenzione_tempo); else scheda.focus_esercizi.Add(Scheda.convenzione_ripetizioni);
            }

            Pannello1.arc.Schede[indice] = scheda;
            saved = true;
            //Console.WriteLine("FINE SAVING");
        }
        
    }
}
