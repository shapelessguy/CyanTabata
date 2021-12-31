using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyanTabata
{
    public class Archivio
    {
        static public string video_path = "";
        static public bool spartano = false;
        public List<Scheda> Schede = new List<Scheda>();
        public List<string> fondo = new List<string>();
        public List<string> lista_date = new List<string>();
        public List<List<string>> lista_schede = new List<List<string>>();
        public Archivio()
        {
            List<string> readText = new List<string>();
            if (!Directory.Exists(Program.path)) Directory.CreateDirectory(Program.path);
            if (!File.Exists(Program.path + @"\" + Program.nome_file + ".txt"))
            {
                using (StreamWriter sw = File.CreateText(Program.path + @"\" + Program.nome_file + ".txt"))
                {
                    sw.Write(CyanTabata.Properties.Resources.Data);
                }
            }

            foreach (string stringa in File.ReadAllLines(Program.path + @"\" + Program.nome_file + ".txt")) readText.Add(stringa);
            int ind_scheda = 0;
            bool lettura = false;
            bool lettura_date = false;
            for (int i = 0; i < readText.Count; i++)
            {
                if (readText[i].Length > 10) if (readText[i].Substring(0, 11) == "Video_path:") { video_path = readText[i].Substring(11); if (video_path.Length<2) { video_path = @"C:\";} }
                if (readText[i].Length > 8) if (readText[i].Substring(0, 9) == "Spartano:") { if (readText[i].Substring(9) == "true") spartano = true; else spartano = false; }
                if (lettura && readText[i].Substring(0, 1) == "*")
                {
                    lettura = false;
                    ind_scheda++;
                    continue;
                }
                if (lettura_date && readText[i].Substring(0, 1) == "*")
                {
                    lettura_date = false;
                    continue;
                }
                if (lettura)
                {
                    lista_schede[ind_scheda].Add(readText[i]);
                    continue;
                }
                /*
                if (lettura_date)
                {
                    lista_date.Add(readText[i]);
                    continue;
                }
                */
                if (readText[i].Length > 1) if (readText[i].Substring(0, 1) != "*" && !lettura)
                    {
                        fondo.Add(readText[i]);
                    }
                if (readText[i].Length > 6) if (readText[i].Substring(1, 6) == "Scheda")
                    {
                        lista_schede.Add(new List<string>());
                        lista_schede[ind_scheda].Add(readText[i].Substring(8, readText[i].Length - 8));
                        lettura = true;
                    }
                /*
                if (readText[i].Length > 7) if (readText[i].Substring(1, 7) == "Storico")
                    {
                        lettura_date = true;
                    }
                    */
            }
            int index = 0;
            foreach (List<string> lista in lista_schede) { Schede.Add(new Scheda(lista, index)); index++; }
        }
    }
}
