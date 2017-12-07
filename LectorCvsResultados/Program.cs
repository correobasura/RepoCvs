using HtmlAgilityPack;
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
        static Dictionary<int,List<int>> dictionaryHist;

        public static void AnalizarDatosListaDiaActual(string rutaBase, DateTime fecha, int maxTabindex = 0)
        {
            bool historial = false;
            if (maxTabindex == 0)
            {
                maxTabindex = ConsultasClass.ConsultarMaxIndexFecha(fecha.ToString("yyyyMMdd"), contexto);
                historial = true;
            }
            TimeSpan ts = fecha.AddDays(-1) - minFecha;
            //53 % encontrado en consolidado
            int dayofweek = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int percent = ts.Days * AnDataUnGanador.RetornarPercent(dayofweek) / 100;
            List<int> lista = AnDataUnGanador.AnalizarDatosDiaActual(fecha, contexto, maxTabindex, percent);
            string rutaFileTemp = rutaBase + fecha.ToString("yyyyMM") + "\\" + fecha.ToString("yyyyMMdd") + "T.csv";
            IEnumerable<string> lines = File.ReadAllLines(rutaFileTemp);
            Dictionary<int, AnalizedTabIndexDTO> dict = new Dictionary<int, AnalizedTabIndexDTO>();
            List<AgrupadorTotalPercentSpanDTO> listaSpanGral = ConsultasClass.ConsultarPercentTimeSpan(contexto, fecha.ToString("yyyyMMdd"));
            List< AgrupadorTotalPercentSpanDTO> listaSpanDia = ConsultasClass.ConsultarPercentTimeSpan(contexto, fecha.ToString("yyyyMMdd"), 1);
            List<ANALISTINDEXUNG> listaAnalisis = new List<ANALISTINDEXUNG>();
            if (historial)
            {
                int fechanum = Convert.ToInt32(fecha.ToString("yyyyMMdd"));
                listaAnalisis = (from x in contexto.ANALISTINDEXUNG
                 where x.FECHANUM == fechanum
                 select x).OrderBy(x=>x.LINEINDEX).ToList();
            }
            for (int i = 0; i < lista.Count; i++)
            {
                AnalizedTabIndexDTO a = new AnalizedTabIndexDTO();
                a.Lineindex = i + 1;
                if (historial)
                {
                    a.Result = (int)(from x in listaAnalisis where x.LINEINDEX == a.Lineindex select x.RESULT).FirstOrDefault();
                }
                a.Tabindex = lista.ElementAt(i);
                a.UltimoSpan = ConsultasClass.ConsultarUltimoTimeSpan(contexto, a.Tabindex, fecha.ToString("yyyyMMdd")).Spantiempo;
                var allLines = lines.Where(x => x.IndexOf(a.Tabindex.ToString(), StringComparison.OrdinalIgnoreCase) >= 0);
                for (int j = 0; j < allLines.Count(); j++)
                {
                    string lineTemp = allLines.ElementAt(j);
                    if (Convert.ToInt32(lineTemp.Split(';')[0]).CompareTo(a.Tabindex) == 0)
                    {
                        a.TMatch = lineTemp;
                        break;
                    }
                }
                a.RankUltimoSpanGral = (from x in listaSpanGral where x.Span == a.UltimoSpan select x.Rank).FirstOrDefault();
                a.RankUltimoSpanDia = (from x in listaSpanDia where x.Span == a.UltimoSpan select x.Rank).FirstOrDefault();
                dict.Add(i + 1, a);
            }
            var dictvalues = (from entry in dict
                          orderby entry.Value.RankUltimoSpanDia descending,
                          entry.Value.RankUltimoSpanGral descending
                          select entry).ToDictionary(x => x.Key, x => x.Value);
            //if (historial)
            //{
            //    var lineIndex = 1;
            //    foreach (var item in dictvalues)
            //    {
            //        var varData = dictionaryHist[lineIndex];
            //        varData.Add(item.Value.Result);
            //        dictionaryHist[lineIndex] = varData;
            //        lineIndex++;
            //    }
            //}
            EscribirDatosArchivo(dictvalues, "Actual\\Analisis" + fecha.ToString("yyyyMMdd"), rutaBase);
        }

        public static void AnalizarDatosListaDias(string rutaBase)
        {
            Dictionary<string, string> dictValues = new Dictionary<string, string>();
            dictValues.Add("AnConProm", ConstantesConsulta.QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_PROM);
            dictValues.Add("AnConDiaSem", ConstantesConsulta.QUERY_SELECCION_ORDENADA_MAS_VALORES_DIASEM);
            dictValues.Add("AnConDiaSemDiaMes", ConstantesConsulta.QUERY_CONTEO_VALORES_DIA_SEMANA_DIAMES);
            DateTime fechaMinima = DateTime.Today.AddDays(-7);
            //DateTime fechaMinima = DateTime.ParseExact("20170727", "yyyyMMdd", CultureInfo.InvariantCulture);
            List<int> indexDias = new List<int>();
            indexDias.Add(1);
            indexDias.Add(2);
            indexDias.Add(3);
            indexDias.Add(4);
            indexDias.Add(5);
            indexDias.Add(6);
            indexDias.Add(7);
            foreach (var itemDict in dictValues)
            {
                List<AnalisisDatosDTO> listaAnalizada = new List<AnalisisDatosDTO>();
                List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();
                Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
                for (int m = 0;  m < indexDias.Count; m++)
                {
                    var elementoIndex = indexDias.ElementAt(m);
                    List<DateTime> listaFechas = new List<DateTime>();
                    for (var i = fechaMinima; i < DateTime.Today;)
                    {
                        int dayOfWeek = (int)i.DayOfWeek == 0 ? 7 : (int)i.DayOfWeek;
                        if (dayOfWeek == elementoIndex)
                        {
                            listaFechas.Add(i);
                        }
                        i = i.AddDays(1);
                    }
                    if (listaFechas.Count > 0)
                    {
                        for (int k = 5; k < 93; k++)
                        {
                            for (var i = 0; i < listaFechas.Count(); i++)
                            {
                                var fecha = listaFechas.ElementAt(i);
                                TimeSpan ts = fecha.AddDays(-1) - minFecha;
                                int percent = ts.Days * k / 100;
                                listaAnalizada.Add(AnDataUnGanador.AnalizarDatosDiaTemp(fecha, contexto, itemDict.Value, percent));
                            }
                            //EscribirDatosArchivo(listaAnalizada, "AnalisisDatosDepuradosD1", rutaBase);
                            AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
                            a.MaxValue = (from x in listaAnalizada select x.ResultadosPositivos).Max();
                            a.MinValue = (from x in listaAnalizada select x.ResultadosPositivos).Min();
                            a.Porcentaje = k;
                            a.TotalPositivosMuestras = (from x in listaAnalizada select x.ResultadosPositivos).Sum();
                            listaAnalizada.Clear();
                            listaConsolidada.Add(a);
                        }
                        listaConsolidada = (from x in listaConsolidada
                                            orderby x.TotalPositivosMuestras descending, x.MaxValue descending,
                                            x.MinValue descending
                                            select x).ToList();
                        EscribirDatosArchivo(listaConsolidada, itemDict.Key + elementoIndex, rutaBase);
                        listaConsolidada.Clear();
                    }
                }
            }

            ConsolidarResultados();

        }

        private static void ConsolidarResultados()
        {
            List<string> stringBase = new List<string>();
            stringBase.Add("AnConProm");
            stringBase.Add("AnConDiaSem");
            stringBase.Add("AnConDiaSemDiaMes");
            foreach (var item in stringBase)
            {
                for (int i = 1; i <= 7; i++)
                {
                    string rutaActual = rutaBase + "\\" + item + i + ".csv";
                    string rutaConsolidado = rutaBase + "\\Consolidados\\" + item + i + ".csv";
                    IEnumerable<string> linesActual = File.ReadAllLines(rutaActual);
                    IEnumerable<string> linesConsolidado = File.ReadAllLines(rutaConsolidado);
                    List<AgrupadorConsolidadoDTO> listaConsolidada = new List<AgrupadorConsolidadoDTO>();
                    List<AgrupadorConsolidadoDTO> listaActual = new List<AgrupadorConsolidadoDTO>();
                    List<AgrupadorConsolidadoDTO> listaFinal = new List<AgrupadorConsolidadoDTO>();
                    foreach (var itemConsolidado in linesConsolidado)
                    {
                        AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
                        string[] arreglo = itemConsolidado.Split(';');
                        a.TotalPositivosMuestras = Convert.ToInt32(arreglo[0]);
                        a.MaxValue = Convert.ToInt32(arreglo[1]);
                        a.MinValue = Convert.ToInt32(arreglo[2]);
                        a.Porcentaje = Convert.ToInt32(arreglo[3]);
                        listaConsolidada.Add(a);
                    }
                    foreach (var itemActual in linesActual)
                    {
                        AgrupadorConsolidadoDTO a = new AgrupadorConsolidadoDTO();
                        string[] arreglo = itemActual.Split(';');
                        a.TotalPositivosMuestras = Convert.ToInt32(arreglo[0]);
                        a.MaxValue = Convert.ToInt32(arreglo[1]);
                        a.MinValue = Convert.ToInt32(arreglo[2]);
                        a.Porcentaje = Convert.ToInt32(arreglo[3]);
                        listaActual.Add(a);
                    }
                    foreach (var itemConsolidado in listaConsolidada)
                    {
                        var itemActual = (from x in listaActual where x.Porcentaje == itemConsolidado.Porcentaje select x).FirstOrDefault();
                        itemConsolidado.TotalPositivosMuestras += itemActual.TotalPositivosMuestras;
                        itemConsolidado.MaxValue = itemConsolidado.MaxValue >= itemActual.MaxValue ? itemConsolidado.MaxValue : itemActual.MaxValue;
                        itemConsolidado.MinValue = itemConsolidado.MinValue <= itemActual.MinValue ? itemConsolidado.MinValue : itemActual.MinValue;
                    }
                    listaConsolidada = listaConsolidada
                        .OrderByDescending(b => b.TotalPositivosMuestras)
                        .ThenByDescending(x => x.MinValue)
                        .ThenBy(b => b.Porcentaje)
                        .ThenByDescending(x => x.MaxValue)
                        .AsEnumerable().ToList();
                    string rutaFinal = "\\Consolidados\\" + item + i;
                    EscribirDatosArchivo(listaConsolidada, rutaFinal, rutaBase);
                }
            }
        }

        public static void AnalizarUnGanador(string fechaFormat)
        {
            DateTime dt = DateTime.ParseExact(fechaFormat, "yyyyMMdd", CultureInfo.InvariantCulture);
            TimeSpan ts = dt.AddDays(-1) - minFecha;
            int dayofweek = (int)dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;
            //53 % encontrado en consolidado
            int percent = ts.Days * AnDataUnGanador.RetornarPercent(dayofweek) / 100;
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

        public static void EscribirDatosArchivo(Dictionary<int, AnalizedTabIndexDTO> dict, string cad, string rutabase)
        {
            //var data = CalcularNumeros();
            string fic = rutabase + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Value.ToString());
            }
            sw.WriteLine("\n");
            //foreach (var item in data)
            //{
            //    sw.WriteLine(item);
            //}
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
            DateTime fechaMax = DateTime.ParseExact("20170727", "yyyyMMdd", CultureInfo.InvariantCulture);
            for (var i = minFecha; i < fechaMax;)
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
            //contexto = new SisResultEntities();
            ////IngresarDatosAllReload();
            //DateTime fechaMinima = DateTime.Today.AddDays(-1);
            //////DateTime fechaMinima = DateTime.ParseExact("20170727", "yyyyMMdd", CultureInfo.InvariantCulture);
            //for (var i = fechaMinima; i < DateTime.Today;)
            //{
            //    string fechaFormat = i.ToString("yyyyMMdd");
            //    AnalizarTabindexResultados(fechaFormat);
            //    IngresarDatos(fechaFormat);
            //    AnalizarUnGanador(fechaFormat);
            //    i = i.AddDays(1);
            //}
            ////for (int i = -5; i < 0; i++)
            ////{
            ////for (var i = DateTime.Today.AddDays(-16); i < DateTime.Today;)
            ////{
            ////    AnalizarDiaAnterior(i.AddDays(-1));
            ////    i = i.AddDays(1);
            ////}
            ////var laFecha = DateTime.ParseExact("20170322", "yyyyMMdd", CultureInfo.InvariantCulture);
            ////AnDataUnGanador.AnalizarDiaAnteriorUnGanador(DateTime.Today.AddDays(-1), contexto);
            ////AnDataMayorUno.AnalizarDiaAnteriorMayorUno(DateTime.Today.AddDays(-1), contexto);
            ////List<string> filenames = new List<string>();
            ////for (var i = DateTime.Today.AddDays(-30); i < DateTime.Today;)
            ////{
            ////    filenames.Add(i.ToString("yyyyMMdd"));
            ////    i = i.AddDays(1);
            ////}
            ////AnalizarUnGanador(filenames);
            ////AnalizarDatos(rutaBase, DateTime.Today, 202);

            ////SeleccionarValoresAleatorios(rutaBase);
            ////AnalizarUnGanadorLvl1(rutaBase);
            ////AnalizarUnGanadorLvl3(rutaBase);
            ////RevisarTimeSpanDatos();

            ////AnalizarDatosListaDiaActual(rutaBase, 270, DateTime.Today.AddDays(-7));
            ////AnalizarDatosListaDiaActual(rutaBase, 764, DateTime.Today.AddDays(-6));
            ////AnalizarDatosListaDiaActual(rutaBase, 499, DateTime.Today.AddDays(-5));
            ////AnalizarDatosListaDiaActual(rutaBase, 111, DateTime.Today.AddDays(-4));
            ////AnalizarDatosListaDiaActual(rutaBase, 115, DateTime.Today.AddDays(-3));
            ////AnalizarDatosListaDiaActual(rutaBase, 167, DateTime.Today.AddDays(-2));
            //AnalizarDatosListaDiaActual(rutaBase, DateTime.Today.AddDays(-1));



            ////dictionaryHist = new Dictionary<int, List<int>>();
            ////for (int i = 1; i <= 30; i++)
            ////{
            ////    dictionaryHist.Add(i, new List<int>());
            ////}
            ////var laFecha = DateTime.ParseExact("20171010", "yyyyMMdd", CultureInfo.InvariantCulture);
            ////for (var i = laFecha; i < DateTime.Today; i = i.AddDays(1))
            ////{
            ////    AnalizarDatosListaDiaActual(rutaBase, i);
            ////}
            //AnalizarDatosListaDiaActual(rutaBase, DateTime.Today, 195);
            ////EscribirHistoriales();
            ////AnalizarDatosListaDias(rutaBase);
            var laFecha = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
            for (var i = laFecha; i < DateTime.Today;)
            {
                if (File.Exists(@"D:\OneDrive\Estimaciones\FS\" + i.ToString("yyyyMM") + @"\" + i.ToString("yyyyMMdd") + ".html"))
                {
                    LeerHtml(i.ToString("yyyyMM"), i.ToString("yyyyMMdd"));
                }
                else
                {
                    LeerHtml(i.ToString("yyyyMM"), i.ToString("yyyyMMdd") + "F");
                }
                i = i.AddDays(1);
            }
            laFecha = DateTime.ParseExact("20171025", "yyyyMMdd", CultureInfo.InvariantCulture);
            for (var i = laFecha; i < DateTime.Today;)
            {
                if (File.Exists(@"D:\OneDrive\Estimaciones\RC\" + i.ToString("yyyyMM") + @"\" + i.ToString("yyyyMMdd") + ".html"))
                {
                    LeerHtmlRC(i.ToString("yyyyMM"), i.ToString("yyyyMMdd"));
                }
                else
                {
                    LeerHtmlRC(i.ToString("yyyyMM"), i.ToString("yyyyMMdd") + "F");
                }
                i = i.AddDays(1);
            }
        }

        public static void LeerHtmlRC(String fechaMes, String fechaDia)
        {
            string path = @"D:\OneDrive\Estimaciones\RC\" + fechaMes + @"\" + fechaDia + ".html";
            string pathWriteFile = @"D:\OneDrive\Estimaciones\RCO\" + fechaMes + @"\" + fechaDia + ".csv";
            string pathWrite = @"D:\OneDrive\Estimaciones\RCO\" + fechaMes;
            var htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.UTF8;
            var doc = htmlWeb.Load(path);

            var htmlNodes = doc.DocumentNode.SelectNodes("//tr");
            List<HtmlDTO> lista = new List<HtmlDTO>();
            foreach (HtmlNode  item in htmlNodes)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(item.InnerHtml);

                var htmltdNodes = htmlDoc.DocumentNode.SelectNodes("//td");
                HtmlDTO htmlDTO = new HtmlDTO();
                var theNodes = htmltdNodes.ToString();
                StringBuilder sb = new StringBuilder();
                bool esAfter = false;
                for (int i = 0; i < htmltdNodes.Count; i++)
                {
                    var item2 = htmltdNodes.ElementAt(i);
                    var data = item2.InnerText.Replace("&nbsp;", "").Replace("&amp;","&");
                    if(i.Equals(1) && data.ToLower().Contains("tras"))
                    {
                        data = "Finalizado";
                        esAfter = true;
                    }
                    if(i.Equals(3) && esAfter)
                    {
                        var htmlDocTd = new HtmlDocument();
                        htmlDocTd.LoadHtml(item2.InnerHtml);
                        var data2 = htmlDocTd.DocumentNode.SelectNodes("//span");
                        var result = data2.ElementAt(0).InnerText.Replace("&nbsp;", "").Replace("(", "").Replace(")", "");
                        sb.Append(result);
                        sb.Append(";");
                    }
                    else {
                        sb.Append(data);
                        sb.Append(";");
                    }
                }
                string[] datos = sb.ToString().Split(';');
                htmlDTO.Hora = datos[0];
                htmlDTO.Estado = datos[1];
                htmlDTO.Home = datos[2];
                htmlDTO.Result = datos[3];
                htmlDTO.Away = datos[4];
                htmlDTO.Half = datos[5].Replace("&nbsp;", "").Replace("(", "").Replace(")", "").Replace(" ","");
                lista.Add(htmlDTO);
            }
            lista = lista.OrderBy(x => x.Home).ThenBy(x => x.Away).ToList();
            int indexer = 0;
            char letter = lista.ElementAt(0).Home[0];
            int indexLetter = 1;
            foreach (var item in lista)
            {
                item.IndexOrdered = ++indexer;
                if (!item.Home[0].Equals(letter))
                {
                    indexLetter = 1;
                    letter = item.Home[0];
                }
                if (item.Estado.ToLower().Equals("finalizado"))
                {
                    string[] score = item.Result.Split('-');
                    item.GHome = Convert.ToInt32(score[0]);
                    item.GAway = Convert.ToInt32(score[1]);
                    item.DiferenciaG = item.GHome - item.GAway;
                    item.TotalG = item.GHome + item.GAway;
                }
                item.GroupLetter = letter;
                item.GroupIndexLetter = indexLetter++;
            }
            lista = lista.Where(x => x.Estado.ToLower().Equals("finalizado")).ToList();
            EscribirArchivoCsv(lista, pathWriteFile, pathWrite);
        }

        public static void LeerHtml(String fechaMes, String fechaDia)
        {
            string path = @"D:\OneDrive\Estimaciones\FS\" + fechaMes + @"\" + fechaDia + ".html";
            string pathWriteFile = @"D:\OneDrive\Estimaciones\FSO\" + fechaMes + @"\" + fechaDia + ".csv";
            string pathWrite = @"D:\OneDrive\Estimaciones\FSO\" + fechaMes;
            var htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.UTF8;
            var doc = htmlWeb.Load(path);

            var htmlNodes = doc.DocumentNode.SelectNodes("//tr");
            List<HtmlDTO> lista = new List<HtmlDTO>();
            foreach (HtmlNode item in htmlNodes)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(item.InnerHtml);

                var htmltdNodes = htmlDoc.DocumentNode.SelectNodes("//td");
                HtmlDTO htmlDTO = new HtmlDTO();
                var theNodes = htmltdNodes.ToString();
                StringBuilder sb = new StringBuilder();
                bool esAfter = false;
                for (int i = 0; i < htmltdNodes.Count; i++)
                {
                    var item2 = htmltdNodes.ElementAt(i);
                    var data = item2.InnerText.Replace("&nbsp;", "").Replace("&amp;", "&");
                    if (i.Equals(2) && data.ToLower().Contains("after"))
                    {
                        data = "Finished";
                        esAfter = true;
                    }
                    if (i.Equals(4) && esAfter)
                    {
                        var htmlDocTd = new HtmlDocument();
                        htmlDocTd.LoadHtml(item2.InnerHtml);
                        var data2 = htmlDocTd.DocumentNode.SelectNodes("//span");
                        var result = data2.ElementAt(0).InnerText.Replace("&nbsp;", "").Replace("(", "").Replace(")", "");
                        sb.Append(result);
                        sb.Append(";");
                    }
                    else
                    {
                        sb.Append(data);
                        sb.Append(";");
                    }
                }
                string[] datos = sb.ToString().Split(';');
                htmlDTO.Hora = datos[1];
                htmlDTO.Estado = datos[2];
                htmlDTO.Home = datos[3];
                htmlDTO.Result = datos[4];
                htmlDTO.Away = datos[5];
                htmlDTO.Half = datos[6].Replace("&nbsp;", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                lista.Add(htmlDTO);
            }
            lista = lista.OrderBy(x => x.Home).ThenBy(x => x.Away).ToList();
            int indexer = 0;
            char letter = lista.ElementAt(0).Home[0];
            int indexLetter = 1;
            foreach (var item in lista)
            {
                item.IndexOrdered = ++indexer;
                if (!item.Home[0].Equals(letter))
                {
                    indexLetter = 1;
                    letter = item.Home[0];
                }
                if (item.Estado.ToLower().Equals("finished"))
                {
                    string[] score = item.Result.Split('-');
                    item.GHome = Convert.ToInt32(score[0]);
                    item.GAway = Convert.ToInt32(score[1]);
                    item.DiferenciaG = item.GHome - item.GAway;
                    item.TotalG = item.GHome + item.GAway;
                }
                item.GroupLetter = letter;
                item.GroupIndexLetter = indexLetter++;
            }
            lista = lista.Where(x => x.Estado.ToLower().Equals("finished")).ToList();
            EscribirArchivoCsv(lista, pathWriteFile, pathWrite);
        }

        public static void EscribirArchivoCsv(List<HtmlDTO> lista, string pathWriteFile, string pathWrite)
        {
            if (!Directory.Exists(pathWrite))
            {
                Directory.CreateDirectory(pathWrite);
            }
            StreamWriter sw = new StreamWriter(pathWriteFile);
            var i = 0;
            foreach (var item in lista)
            {
                sw.WriteLine(item);
                i++;
            }
            sw.Close();
        }

        public static void EscribirHistoriales()
        {
            string fic = rutaBase + "\\Analisis\\Historiales" + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            var i = 0;
            foreach (var item in dictionaryHist)
            {
                sw.WriteLine(string.Join(";",item.Value));
                i++;
            }
            sw.Close();
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
            }
            File.WriteAllLines(rutaFinal, linesFinal, Encoding.UTF8);
        }

        public static int[] CalcularNumeros()
        {
            int[] numeros = new int[7];
            Random r = new Random();

            int auxiliar = 0;
            int contador = 0;

            for (int i = 0; i < 7; i++)
            {
                auxiliar = r.Next(1, 30);
                bool continuar = false;

                while (!continuar)
                {
                    for (int j = 0; j <= contador; j++)
                    {
                        if (auxiliar == numeros[j])
                        {
                            continuar = true;
                            j = contador;
                        }
                    }

                    if (continuar)
                    {
                        auxiliar = r.Next(1, 30);
                        continuar = false;
                    }
                    else
                    {
                        continuar = true;
                        numeros[contador] = auxiliar;
                        contador++;
                    }
                }
            }

            return numeros;
        }

        //public static void AnalizarUnGanadorLvl2(List<string> filenames)
        //{
        //    for (int i = 0; i < filenames.Count; i++)
        //    {
        //        DateTime dt = DateTime.ParseExact(filenames[i], "yyyyMMdd", CultureInfo.InvariantCulture);
        //        TimeSpan ts = dt - minFecha;
        //        int percent = ts.Days * PercentAnalisis / 100;
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
        //        int percent = ts.Days * PercentAnalisis / 100;
        //        listaAnalizada.Add(RevisionDatos.AnalizarDatosPorDiaSeleccionLvl3(fechaMinLvl1, contexto, percent, 5));
        //        fechaMinLvl1 = fechaMinLvl1.AddDays(1);
        //    }
        //    EscribirDatosArchivo(listaAnalizada, "AnalisisAnalizada2", rutaBase);
        //}
    }

    
}
