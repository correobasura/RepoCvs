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
            var fecha = DateTime.Today.AddDays(-1);
            List<int> lista = AnDataUnGanador.AnalizarDatosDiaActual(fecha, contexto, 134);
            EscribirDatosArchivo(lista, "AnalisisActual" + fecha.ToString("yyyyMMdd"), rutaBase);
        }

        public static void AnalizarDatosListaDias(string rutaBase)
        {
            List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
            List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();
            Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
            listaAnalizada = new List<AnalisisDatosDTO>();
            //for (int k = 4; k < 25; k++)
            //{
                for (var i = DateTime.Today.AddDays(-15); i < DateTime.Today;)
                {
                    TimeSpan ts = i - minFecha;
                    listaAnalizada.Add(AnDataUnGanador.AnalizarDatosDiaTemp(i, contexto));
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

        public static void AnalizarUnGanador(List<string> filenames)
        {
            for (int i = 0; i < filenames.Count; i++)
            {
                DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
                AnDataUnGanador.AnalizarDatosDia(dt, contexto);
            }
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

        private static void IngresarDatos(string rutaBase)
        {
            string line;
            List<string> filenames = new List<string>();
            DateTime fechaMinima = DateTime.ParseExact("20170503", "yyyyMMdd", CultureInfo.InvariantCulture);
            ////////DateTime fechaMax = DateTime.ParseExact("20170301", "yyyyMMdd", CultureInfo.InvariantCulture);
            for (var i = fechaMinima; i < DateTime.Today;)
            {
                filenames.Add(i.ToString("yyyyMMdd"));
                i = i.AddDays(1);
            }
            //Dictionary<int, AgrupadorTimeSpanDTO> dict = AnDataUnGanador.ObtenerDiccionarioInicial();
            //filenames.Add("20170513");
            //filenames.Add("20170524");
            //StreamReader fileReader;
            //List<decimal?> listTabindex = new List<decimal?>();
            //List<USERRESULTTABLESFS> listElementosAgregados = new List<USERRESULTTABLESFS>();

            //for (int i = 0; i < filenames.Count; i++)
            //{
            //    DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
            //    string rutaTemp = rutaBase + dt.ToString("yyyyMM") + "\\" + filenames[i] + ".csv";
            //    fileReader = new StreamReader(rutaTemp);
            //    while ((line = fileReader.ReadLine()) != null)
            //    {
            //        string[] arreglo = line.Split(';');
            //        USERRESULTTABLESFS u = new USERRESULTTABLESFS();
            //        u.ID = ConsultasClass.ObtenerValorSecuencia(u, contexto);
            //        u.FECHA = dt;
            //        int tabindex = Convert.ToInt32(arreglo[0]);
            //        int diferenciaG = Convert.ToInt32(arreglo[9]);
            //        int laFechaNum = Convert.ToInt32(dt.ToString("yyyyMMdd"));
            //        u.TABINDEX = tabindex;
            //        u.HORA = arreglo[1];
            //        u.MARCADOR = arreglo[4];
            //        u.GLOCAL = Convert.ToInt32(arreglo[7]);
            //        u.GVISITANTE = Convert.ToInt32(arreglo[8]);
            //        u.DIFERENCIAG = diferenciaG;
            //        u.TOTALG = Convert.ToInt32(arreglo[10]);
            //        u.FECHANUM = laFechaNum;
            //        u.MESNUM = dt.Month;
            //        u.DIAMESNUM = dt.Day;
            //        u.DIASEMNUM = dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;
            //        //AnDataUnGanador.ValidarSpanDatos(dict, u, tabindex, diferenciaG, contexto);
            //        listTabindex.Add(tabindex);
            //        listElementosAgregados.Add(u);
            //    }
            //}
            //AnDataUnGanador.ValidarSpanDatosDiaAnterior(contexto, listTabindex, listElementosAgregados);
            //contexto.SaveChanges();
            AnalizarUnGanador(filenames);
            //AnalizarUnGanadorLvl2(filenames);
        }

        static void Main(string[] args)
        {
            contexto = new SisResultEntities();
            //IngresarDatos(rutaBase);

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
            //AnalizarDatosListaDiasBetween(rutaBase);
            //AnalizarUnGanadorLvl1(rutaBase);
            //AnalizarUnGanadorLvl3(rutaBase);
            //RevisarTimeSpanDatos();

            //AnalizarDatosListaDiaActual(rutaBase);
        }
        //public static void AnalizarDatosListaDiasBetween(string rutaBase)
        //{
        //    List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
        //    List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();

        //    listaAnalizada = new List<AnalisisDatosDTO>();
        //        for (var i = DateTime.Today.AddDays(-15); i < DateTime.Today;)
        //        {
        //            TimeSpan ts = i - minFecha;
        //            int percentMenor = ts.Days * 60 / 100;
        //            int percentMayor = ts.Days * 79 / 100;
        //            //int percent = 80;
        //            listaAnalizada.Add(RevisionDatos.AnalizarDatosPorDiaSeleccionBetween(i, contexto, 5, percentMenor, percentMayor));
        //            i = i.AddDays(1);
        //    }
        //    EscribirDatosArchivo(listaAnalizada, "AnalisisAnalizada", rutaBase);
        //    AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
        //        a.MaxValue = (from x in listaAnalizada select x.ResultadosPositivos).Max();
        //        a.MinValue = (from x in listaAnalizada select x.ResultadosPositivos).Min();
        //        //a.Porcentaje = j;
        //        a.TotalPositivosMuestras = (from x in listaAnalizada select x.ResultadosPositivos).Sum();
        //        listaAnalizada.Clear();
        //        listaConsolidada.Add(a);
        //    EscribirDatosArchivo(listaConsolidada, "AnalisisDiaBetween" + 5, rutaBase);
        //}
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
