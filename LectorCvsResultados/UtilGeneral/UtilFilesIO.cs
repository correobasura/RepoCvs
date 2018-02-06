using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.UtilGeneral
{
    public class UtilFilesIO
    {
        static string rutaBase = @"D:\OneDrive\Estimaciones\FS\";

        public static void EscribirArchivoCsv(List<FLASHORDERED> lista)
        {
            string fic = rutaBase + "\\Analisis\\DiaTemp" + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in lista)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }
    }
}
