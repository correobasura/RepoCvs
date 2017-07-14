using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    class Program
    {
        private static SisResultEntities contexto;
        static DateTime minFecha = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
        static string rutaBase = @"D:\OneDrive\Estimaciones\FS\";

        public static void AnalizarDatosListaDiaActual(string rutaBase)
        {
            var fecha = DateTime.Today;
            TimeSpan ts = fecha - minFecha;
            //53 % encontrado en consolidado
            int percent = ts.Days * 30 / 100;
            List<int> lista = AnDataUnGanador.AnalizarDatosDiaActual(fecha, contexto, 89, percent);
            EscribirDatosArchivo(lista, "AnalisisActual" + fecha.ToString("yyyyMMdd"), rutaBase);
        }

        public static void AnalizarDatosListaDias(string rutaBase)
        {
            List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
            List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();
            //Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
            //for (int k = 5; k < 94; k++)
            //{
                for (var i = DateTime.Today.AddDays(-15); i < DateTime.Today;)
                {

                    TimeSpan ts = i - minFecha;
                    //87 % encontrado en consolidado
                    int percent = ts.Days * 30 / 100;
                    listaAnalizada.Add(AnDataUnGanador.AnalizarDatosDiaTemp(i, contexto, percent));
                    i = i.AddDays(1);
                }
            EscribirDatosArchivo(listaAnalizada, "AnalisisDatosDepurados", rutaBase);
            //    AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
            //    a.MaxValue = (from x in listaAnalizada select x.ResultadosPositivos).Max();
            //    a.MinValue = (from x in listaAnalizada select x.ResultadosPositivos).Min();
            //    a.Porcentaje = k;
            //    a.TotalPositivosMuestras = (from x in listaAnalizada select x.ResultadosPositivos).Sum();
            //    listaAnalizada.Clear();
            //    listaConsolidada.Add(a);
            //}
            //EscribirDatosArchivo(listaConsolidada, "AnalisisConsolidado", rutaBase);

        }

        public static void AnalizarUnGanador(string fechaFormat)
        {
            DateTime dt = DateTime.ParseExact(fechaFormat, "yyyyMMdd", CultureInfo.InvariantCulture);
            TimeSpan ts = dt - minFecha;
            //53 % encontrado en consolidado
            int percent = ts.Days * 30 / 100;
            AnDataUnGanador.AnalizarDatosDia(dt, contexto, percent);
        }

        public static void EscribirDatosArchivo(List<AgrupadorConsolidadoDTO> listaSeleccionados, string cad, string rutabase)
        {

            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            var i = 0;
            foreach (var item in listaSeleccionados)
            {
                sw.WriteLine(item.ToString());
                i++;
            }
            sw.Close();
        }

        public static void EscribirDatosArchivo(List<int> listaSeleccionados, string cad, string rutabase)
        {

            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in listaSeleccionados)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }

        public static void EscribirDatosArchivo(List<AnalisisDatosDTO> listaSeleccionados, string cad, string rutabase)
        {

            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in listaSeleccionados)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }

        public static void EscribirDatosArchivo(Dictionary<int, AgrupadorTimeSpanDTO> dict, string cad, string rutabase)
        {

            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Key + ";" + item.Value.ToString());
            }
            sw.Close();
        }

        private static void IngresarDatosAllReload()
        {
            string line;
            List<string> filenames = new List<string>();
            //DateTime fechaMinima = DateTime.Today.AddDays(-36);
            //DateTime fechaMax = DateTime.ParseExact("20170301", "yyyyMMdd", CultureInfo.InvariantCulture);
            for (var i = minFecha; i < DateTime.Today;)
            {
                filenames.Add(i.ToString("yyyyMMdd"));
                i = i.AddDays(1);
            }
            Dictionary<int, AgrupadorTimeSpanDTO> dict = AnDataUnGanador.ObtenerDiccionarioInicial();
            //filenames.Add("20170621");
            StreamReader fileReader;
            List<decimal?> listTabindex = new List<decimal?>();
            List<USERRESULTTABLESFS> listElementosAgregados = new List<USERRESULTTABLESFS>();

            for (int i = 0; i < filenames.Count; i++)
            {
                DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
                string rutaTemp = rutaBase + dt.ToString("yyyyMM") + "\\" + filenames[i] + ".csv";
                fileReader = new StreamReader(rutaTemp);
                while ((line = fileReader.ReadLine()) != null)
                {
                    string[] arreglo = line.Split(';');
                    USERRESULTTABLESFS u = new USERRESULTTABLESFS();
                    u.ID = ConsultasClass.ObtenerValorSecuencia(u, contexto);
                    u.FECHA = dt;
                    int tabindex = Convert.ToInt32(arreglo[0]);
                    int diferenciaG = Convert.ToInt32(arreglo[9]);
                    int laFechaNum = Convert.ToInt32(dt.ToString("yyyyMMdd"));
                    u.TABINDEX = tabindex;
                    u.HORA = arreglo[1];
                    u.MARCADOR = arreglo[4];
                    u.GLOCAL = Convert.ToInt32(arreglo[7]);
                    u.GVISITANTE = Convert.ToInt32(arreglo[8]);
                    u.DIFERENCIAG = diferenciaG;
                    u.TOTALG = Convert.ToInt32(arreglo[10]);
                    u.FECHANUM = laFechaNum;
                    u.MESNUM = dt.Month;
                    u.DIAMESNUM = dt.Day;
                    u.DIASEMNUM = dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;
                    //Cuando se reinician todos los datos es necesario ejecutar la actualización de los tabindexseq y spantiempo hist
                    //por que se hacen consultas a la base de datos y no se han confirmado los cambios, por lo que siempre va a retornar 1 0 -1
                    //u.TABINDEXSEQ = ConsultasClass.ConsultarNextTabindexSeq(contexto, tabindex);
                    //u.SPANTIEMPOHIST = AnDataUnGanador.ValidarSpanTiempo(tabindex, diferenciaG, contexto, dt.ToString("yyyyMMdd"));
                    AnDataUnGanador.ValidarSpanDatos(dict, u, tabindex, diferenciaG, contexto);
                    listTabindex.Add(tabindex);
                    listElementosAgregados.Add(u);
                }
            }
            //AnDataUnGanador.ValidarSpanDatosDiaAnterior(contexto, listTabindex, listElementosAgregados);
            contexto.SaveChanges();
            //AnalizarUnGanador(filenames);
        }

        private static void IngresarDatos(string dateFileName)
        {
            string line;
            StreamReader fileReader;
            List<decimal?> listTabindex = new List<decimal?>();
            List<USERRESULTTABLESFS> listElementosAgregados = new List<USERRESULTTABLESFS>();
            DateTime dt = DateTime.ParseExact(dateFileName, "yyyyMMdd", CultureInfo.InvariantCulture);
            string rutaTemp = rutaBase + dt.ToString("yyyyMM") + "\\" + dateFileName + ".csv";
            fileReader = new StreamReader(rutaTemp);
            int diamesnum = dt.Day;
            int diasemnum = dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] arreglo = line.Split(';');
                USERRESULTTABLESFS u = new USERRESULTTABLESFS();
                u.ID = ConsultasClass.ObtenerValorSecuencia(u, contexto);
                u.FECHA = dt;
                int tabindex = Convert.ToInt32(arreglo[0]);
                int diferenciaG = Convert.ToInt32(arreglo[9]);
                int laFechaNum = Convert.ToInt32(dt.ToString("yyyyMMdd"));
                u.TABINDEX = tabindex;
                u.HORA = arreglo[1];
                u.MARCADOR = arreglo[4];
                u.GLOCAL = Convert.ToInt32(arreglo[7]);
                u.GVISITANTE = Convert.ToInt32(arreglo[8]);
                u.DIFERENCIAG = diferenciaG;
                u.TOTALG = Convert.ToInt32(arreglo[10]);
                u.FECHANUM = laFechaNum;
                u.MESNUM = dt.Month;
                u.DIAMESNUM = diamesnum;
                u.DIASEMNUM = diasemnum;
                u.TABINDEXSEQ = ConsultasClass.ConsultarNextTabindexSeq(contexto, tabindex);
                u.SPANTIEMPOHIST = AnDataUnGanador.ValidarSpanTiempo(tabindex, diferenciaG, contexto, dt.ToString("yyyyMMdd"), 0);
                u.SPANTIEMPOSEMHIST = AnDataUnGanador.ValidarSpanTiempo(tabindex, diferenciaG, contexto, dt.ToString("yyyyMMdd"), 1);
                u.SPANTIEMPOMESHIST = AnDataUnGanador.ValidarSpanTiempo(tabindex, diferenciaG, contexto, dt.ToString("yyyyMMdd"), 2);
                listTabindex.Add(tabindex);
                listElementosAgregados.Add(u);
            }
            AnDataUnGanador.ValidarSpanDatosDiaAnterior(contexto, listTabindex, listElementosAgregados, diasemnum, diamesnum);
            contexto.SaveChanges();
        }

        static void Main(string[] args)
        {
            contexto = new SisResultEntities();
            //IngresarDatosAllReload();
            //DateTime fechaMinima = DateTime.Today.AddDays(-15);
            //for (var i = fechaMinima; i < DateTime.Today;)
            //{
            //    string fechaFormat = i.ToString("yyyyMMdd");
            //    //AnalizarTabindexResultados(fechaFormat);
            //    //IngresarDatos(fechaFormat);
            //    AnalizarUnGanador(fechaFormat);
            //    i = i.AddDays(1);
            //}
            //for (int i = -5; i < 0; i++)
            //{
            //for (var i = DateTime.Today.AddDays(-16); i < DateTime.Today;)
            //{
            //    AnalizarDiaAnterior(i.AddDays(-1));
            //    i = i.AddDays(1);
            //}
            //var laFecha = DateTime.ParseExact("20170322", "yyyyMMdd", CultureInfo.InvariantCulture);
            //AnDataUnGanador.AnalizarDiaAnteriorUnGanador(DateTime.Today.AddDays(-1), contexto);
            //AnDataMayorUno.AnalizarDiaAnteriorMayorUno(DateTime.Today.AddDays(-1), contexto);
            //List<string> filenames = new List<string>();
            //for (var i = DateTime.Today.AddDays(-30); i < DateTime.Today;)
            //{
            //    filenames.Add(i.ToString("yyyyMMdd"));
            //    i = i.AddDays(1);
            //}
            //AnalizarUnGanador(filenames);
            //AnalizarDatos(rutaBase, DateTime.Today, 202);

            AnalizarDatosListaDias(rutaBase);

            //SeleccionarValoresAleatorios(rutaBase);
            //AnalizarUnGanadorLvl1(rutaBase);
            //AnalizarUnGanadorLvl3(rutaBase);
            //RevisarTimeSpanDatos();

            AnalizarDatosListaDiaActual(rutaBase);
        }

        private static void AnalizarTabindexResultados(string filename)
        {
            StreamReader fileReader;
            DateTime dt = DateTime.ParseExact(filename, "yyyyMMdd", CultureInfo.InvariantCulture);
            string rutaFileTemp = rutaBase + dt.ToString("yyyyMM") + "\\" + filename + "T.csv";
            string rutaFileFinal = rutaBase + dt.ToString("yyyyMM") + "\\" + filename + "F.csv";
            string rutaFinal = rutaBase + dt.ToString("yyyyMM") + "\\" + filename + ".csv";
            IEnumerable<string> lines = File.ReadAllLines(rutaFileFinal);
            List<string> linesFinal = new List<string>();
            fileReader = new StreamReader(rutaFileTemp);
            String line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] arreglo = line.Split(';');
                string eHome = arreglo[3].Trim();
                string eVisitor = arreglo[5].Trim();
                var matchHome = lines.Where(x => x.IndexOf(eHome, StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault();
                var matchVisitor = lines.Where(x => x.IndexOf(eHome, StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault();
                if (matchHome != null && matchVisitor != null && matchHome.Equals(matchVisitor))
                {
                    var arrayFinal = matchHome.Split(';');
                    var arrayPush = new string[11];
                    arrayPush[0] = arreglo[0];
                    arrayPush[1] = arreglo[1];
                    arrayPush[3] = arreglo[3];
                    arrayPush[4] = arrayFinal[4];
                    arrayPush[5] = arreglo[5];
                    arrayPush[7] = arrayFinal[7];
                    arrayPush[8] = arrayFinal[8];
                    arrayPush[9] = arrayFinal[9];
                    arrayPush[10] = arrayFinal[10];

                    linesFinal.Add(String.Join(";", arrayPush));
                }
                else
                {
                    var algo = "revisar";
                }
            }
            File.WriteAllLines(rutaFinal, linesFinal, Encoding.UTF8);
        }

        //public static void AnalizarUnGanadorLvl2(List<string> filenames)
        //{
        //    for (int i = 0; i < filenames.Count; i++)
        //    {
        //        DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
        //        TimeSpan ts = dt - minFecha;
        //        int percent = ts.Days * 80 / 100;
        //        AnDataUnGanador.AnalizarDatosPorDiaSeleccionLvl2(dt, contexto, percent);
        //    }
        //}

        //public static void AnalizarUnGanadorLvl3(string rutaBase)
        //{
        //    List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
        //    DateTime fechaMinLvl1 = DateTime.ParseExact("20170408", "yyyyMMdd", CultureInfo.InvariantCulture);
        //    DateTime minFecha = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
        //    for (; fechaMinLvl1 < DateTime.Today;)
        //    {
        //        TimeSpan ts = fechaMinLvl1 - minFecha;
        //        int percent = ts.Days * 80 / 100;
        //        listaAnalizada.Add(RevisionDatos.AnalizarDatosPorDiaSeleccionLvl3(fechaMinLvl1, contexto, percent, 5));
        //        fechaMinLvl1 = fechaMinLvl1.AddDays(1);
        //    }
        //    EscribirDatosArchivo(listaAnalizada, "AnalisisAnalizada2", rutaBase);
        //}
    }
}
