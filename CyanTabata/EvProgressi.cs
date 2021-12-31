using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class EvProgressi
    {
        public string scheda = null;
        public string scheda_saving = null;
        public string nome = null;
        public string data_txt = null;
        public int valore = 1;
        public int[] data;
        public string datacode;
        
        public string[] misure_txt = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
        public string[] misure = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };

        public string peso="";
        public string impegno="";
        public string kcal = "";
        public string multimedia="";
        public string esercizio_preferito="";
        public string serie="";
        public string ripetizioni="";
        public string tempo="";
        public string carico="";

        public EvProgressi()
        {

        }
        private void Load()
        {
            string[] righe = scheda_saving.Split('\n');
            int indice = 0;
            foreach (string stringa in righe)
            {
                if (stringa.Length >= 6) if (stringa.Substring(0, 6) == "Peso: ") {peso = stringa.Substring(6, stringa.Length - 6); }
                if (stringa.Length >= 9) if (stringa.Substring(0, 9) == "Impegno: ") impegno = stringa.Substring(9, stringa.Length - 9);
                if (stringa.Length >= 21) if (stringa.Substring(0, 21) == "Calorie giornaliere: ") kcal = stringa.Substring(21, stringa.Length - 21);
                if (stringa.Length >= 12) if (stringa.Substring(0, 12) == "Multimedia: ") multimedia = stringa.Substring(12, stringa.Length - 12);
                if (stringa.Length >= 21) if (stringa.Substring(0, 21) == "Esercizio preferito: ") esercizio_preferito = stringa.Substring(21, stringa.Length - 21);
                if (stringa.Length >= 7) if (stringa.Substring(0, 7) == "Serie: ") serie = stringa.Substring(7, stringa.Length - 7);
                if (stringa.Length >= 13) if (stringa.Substring(0, 13) == "Ripetizioni: ") ripetizioni = stringa.Substring(13, stringa.Length - 13);
                if (stringa.Length >= 10) if (stringa.Substring(0, 10) == "Tempo(s): ") tempo = stringa.Substring(10, stringa.Length - 10);
                if (stringa.Length >= 8) if (stringa.Substring(0, 8) == "Carico: ") carico = stringa.Substring(8, stringa.Length - 8);
                
                if (stringa.Contains("/separ/")){
                    misure_txt[indice] = stringa.Substring(0, stringa.IndexOf("/separ/"));
                    misure[indice] = stringa.Substring(stringa.IndexOf("/separ/")+7);
                    indice++;
                }
            }

            Componi_Scheda();
            Componi_Scheda_Saving();
        }
        public void Componi_Scheda()
        {
            Ordina_misure();
            scheda = "\n";
            if (peso != "") scheda += "Peso: " + peso + "kg\n";
            if (impegno != "") scheda += "Impegno: " + impegno + "/10\n";
            if (kcal != "") scheda += "Calorie giornaliere: " + kcal + "kcal\n";
            if (multimedia != "") scheda += "Multimedia: " + multimedia + "\n";
            if (esercizio_preferito != "")
            {
                scheda += "\nEsercizio di riferimento: \n- " + esercizio_preferito + " -\n";
                if (serie != "") scheda += " ->Serie: " + serie + "\n";
                if (ripetizioni != "") scheda += " ->Ripetizioni: " + ripetizioni + "\n";
                if (tempo != "") scheda += " ->Tempo: " + tempo + "s\n";
                if (carico != "") scheda += " ->Carico: " + carico + "kg\n";
            }

            bool inizio = false;
            for (int i = 0; i < misure_txt.Length; i++)
            {
                if (misure[i] != "") { if (!inizio) { scheda += "\n"; inizio = true; } scheda += misure_txt[i] + misure[i] + "cm\n"; }
            }
        }
        void Componi_Scheda_Saving()
        {
            scheda_saving = "";
            if (peso != "") scheda_saving += "Peso: " + peso + "\n";
            if (impegno != "") scheda_saving += "Impegno: " + impegno + "\n";
            if (kcal != "") scheda_saving += "Calorie giornaliere: " + kcal + "\n";
            if (multimedia != "") scheda_saving += "Multimedia: " + multimedia + "\n";
            if (esercizio_preferito != "")
            {
                scheda_saving += "Esercizio preferito: " + esercizio_preferito + "\n";
                if (serie != "") scheda_saving += "Serie: " + serie + "\n";
                if (ripetizioni != "") scheda_saving += "Ripetizioni: " + ripetizioni + "\n";
                if (tempo != "") scheda_saving += "Tempo(s): " + tempo + "\n";
                if (carico != "") scheda_saving += "Carico: " + carico + "\n";
            }
            
            for (int i = 0; i < misure_txt.Length; i++)
            {
                if (misure[i] != "") { scheda_saving += misure_txt[i] + "/separ/" + misure[i] + "\n"; }
            }
        }
        void Ordina_misure()
        {
            List<int> indici = new List<int>();
            for(int i=0;i<misure_txt.Length; i++)
            {
                if (misure_txt[i] != "") indici.Add(i);
            }
            string[] ausiliare_misure_txt = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
            string[] ausiliare_misure = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
            for (int i=0; i<indici.Count; i++)
            {
                ausiliare_misure_txt[i] = misure_txt[indici[i]];
                ausiliare_misure[i] = misure[indici[i]];
            }
            misure_txt = ausiliare_misure_txt;
            misure = ausiliare_misure;
        }

        public void Save()
        {
            Componi_Scheda_Saving();
            
            
            using (StreamWriter sw = File.CreateText(Program.path + @"\Progressi\" + datacode + ".txt"))
            {
                sw.Write(scheda_saving);
            }
        }
        static public void Compila(int[] data, EvProgressi progresso)
        {
            new Compila(data, progresso);
        }

        public static List<EvProgressi> GetProgressi(string path)
        {
            List<EvProgressi> progressi = new List<EvProgressi>();

            foreach (string file in Directory.GetFiles(path))
            {
                string[] righe = File.ReadAllLines(file);
                string scheda_saving = "";
                for (int i = 0; i < righe.Length; i++) { if (i > 0) scheda_saving += "\n"; scheda_saving += righe[i]; } 
                string file_name = file.Substring(file.LastIndexOf(@"\") + 1);
                file_name = file_name.Substring(0, file_name.Length - 4);

                int[] data = new int[] {0, 0, 0, Convert.ToInt32(file_name.Substring(6, 2)), Convert.ToInt32(file_name.Substring(4, 2)), Convert.ToInt32(file_name.Substring(0, 4)) };

                progressi.Add(new EvProgressi() {data = data, scheda_saving = scheda_saving, datacode = file_name});
                progressi[progressi.Count - 1].Load();
                //Console.WriteLine(progressi[progressi.Count-1].scheda);
            }

            return progressi;
        }
    }
}
