using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    static class Program
    {
        static public Form1 principale;
        static public string path = @"C:\ProgramData\Cyan\Tabata";
        static public string nome_file = @"Data";
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateDir();
            Application.Run(principale = new Form1());
        }

        public static void CreateDir()
        {
            if (!Directory.Exists(@"C:\ProgramData\Cyan")) Directory.CreateDirectory(@"C:\ProgramData\Cyan");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path + @"\Cronologia");
                Directory.CreateDirectory(path + @"\Progressi");
            }
            else
            {
                if (!Directory.Exists(path + @"\Cronologia")) Directory.CreateDirectory(path + @"\Cronologia");
                if (!Directory.Exists(path + @"\Progressi")) Directory.CreateDirectory(path + @"\Progressi");
            }
        }
        public static void Saving()
        {
            using (StreamWriter sw = File.CreateText(path + @"\" + nome_file + ".txt"))
            {
                sw.WriteLine("Video_path:" + Archivio.video_path);
                string spart = "false";
                if(Archivio.spartano) spart = "true";
                sw.WriteLine("Spartano:" + spart);
                foreach (Scheda scheda in Pannello1.arc.Schede)
                {
                    sw.Write("*Scheda "); sw.WriteLine(scheda.nome_scheda);
                    for(int i=0; i<scheda.nomi_esercizi.Count; i++)
                    {
                        sw.Write("'" + scheda.nomi_esercizi[i] + "'");
                        sw.Write("'" + scheda.tempo_esercizi[i] + "'");
                        if(scheda.ripetizioni_esercizi[i]>0) sw.Write("'" + scheda.ripetizioni_esercizi[i] + "rip'");
                        sw.WriteLine("");
                    }
                    sw.WriteLine("*");
                    sw.WriteLine("");
                }
                sw.WriteLine("");
                /*
                sw.WriteLine("*Storico :");
                foreach (string stringa in Pannello1.arc.lista_date)
                {
                    sw.WriteLine(stringa);
                }
                sw.WriteLine("*");
                sw.WriteLine("");
                foreach (string stringa in Pannello1.arc.fondo)
                {
                    sw.WriteLine(stringa);
                }*/


            }
        }


    }
}
