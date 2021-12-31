using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanTabata
{
    public class Eventi
    {
        public string scheda = null;
        public string nome = null;
        public string data_txt = null;
        public int valore = 1;
        public int[] data;
        public long datacode = 0;

        public Eventi()
        {

        }

        public void Load()
        {
            
        }
        

        /*public static List<Eventi> Order_data(List<Eventi> eventi)
        {
            
            foreach (Eventi evento in eventi) { evento.datacode = Date.Codifica(evento.data); evento.datacode_modifica = Date.Codifica(evento.data_modifica); }
            eventi = eventi.OrderBy(o => o.datacode).ToList();

            List<List<Eventi>> superlista = new List<List<Eventi>>();
            int j = -1;
            if (eventi.Count > 0)
            {
                superlista.Add(new List<Eventi>());
                superlista[0].Add(eventi[0]);
                j++;
                for (int i = 1; i < eventi.Count; i++)
                {
                    if (eventi[i].datacode != eventi[i - 1].datacode) { superlista.Add(new List<Eventi>()); j++; }
                    superlista[j].Add(eventi[i]);
                }
            }
            for (int i = 0; i < superlista.Count; i++)
            {
                superlista[i] = superlista[i].OrderBy(o => o.datacode_modifica).ToList();
            }

            List<Eventi> eventi_out = new List<Eventi>();
            for (int m = 0; m < j + 1; m++)
            {
                for (int i = 0; i < superlista[m].Count; i++)
                {
                    eventi_out.Add(superlista[m][i]);
                }
            }
            return eventi_out;
            

        }*/

        /////////////////////////////////////////////////////////////////////////  Caricamento Eventi (System, System.IO)
        public static List<Eventi> GetEventi(string path)
        {
            List<Eventi> eventi = new List<Eventi>();
            
            foreach (string file in Directory.GetFiles(path))
            {
                string[] righe = File.ReadAllLines(file);
                string nome, scheda="";
                nome = righe[0].Substring(8);
                if (righe.Length > 2) { for (int i = 1; i < righe.Length - 2; i++) { if (i > 1) scheda += "\n";  scheda += righe[i].Substring(1, righe[i].Length-2).Replace("''", "  >  "); } }
                string file_name = file.Substring(file.LastIndexOf(@"\") + 1);
                file_name = file_name.Substring(0, file_name.Length - 4);

                long file_datacode = Convert.ToInt64(file_name);
                
                eventi.Add(new Eventi() { datacode = file_datacode, data = Date.Decodifica((uint)file_datacode), nome = nome, scheda = scheda,});
                eventi[eventi.Count - 1].Load();
            }

            return eventi;
        }
    }
}
