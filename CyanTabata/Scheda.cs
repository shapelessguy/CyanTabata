using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyanTabata
{
    public class Scheda
    {
        public int index;
        public string nome_scheda;
        public List<string> nomi_esercizi = new List<string>();
        public List<int> tempo_esercizi = new List<int>();
        public List<string> focus_esercizi = new List<string>();
        public List<int> ripetizioni_esercizi = new List<int>();
        public static string convenzione_tempo = "time";
        public static string convenzione_ripetizioni = "rip";
        public Scheda(List<string> lista, int index)
        {
            this.index = index;
            nome_scheda = lista[0];
            int reiterazione=0;
            foreach(string stringa in lista)
            {
                List<int> segni = new List<int>();
                for(int i=0; i<stringa.Length; i++)
                {
                    if (stringa.Substring(i, 1) == "'") segni.Add(i);
                }
                if (reiterazione != 0)
                {
                    //Console.WriteLine(stringa);
                    nomi_esercizi.Add(stringa.Substring(segni[0]+1, segni[1] - segni[0]-1));
                    tempo_esercizi.Add(Convert.ToInt32(stringa.Substring(segni[2]+1, segni[3] - segni[2]-1)));
                    if (segni.Count > 4) { focus_esercizi.Add(stringa.Substring(segni[5] - 3, 3)); ripetizioni_esercizi.Add(Convert.ToInt32(stringa.Substring(segni[4] + 1, segni[5] - segni[4] - 4))); }
                    else { focus_esercizi.Add(convenzione_tempo); ripetizioni_esercizi.Add(0); }
                }

                reiterazione++;
            }

            //Console.WriteLine( "\nCreazione di :   '" + nome_scheda + "'");
            //for (int i = 0; i < nomi_esercizi.Count; i++) Console.WriteLine(nomi_esercizi[i] + "     " + tempo_esercizi[i] + "    " + ripetizioni_esercizi[i] + "->" + focus_esercizi[i]);
        }
    }
}
