using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LectorCvsResultados
{
    public class AnDataUnGanador
    {
        /// <summary>
        /// Método que maneja el análisis de los datos para los tabindex y los timespan de los datos para cada tabindex
        /// </summary>
        /// <param name="fechaRevisar">fecha para revisar la información</param>
        /// <param name="contexto">Instancia del contexto para la consulta de datos.</param>
        public static void AnalizarDatosDia(DateTime fechaRevisar, SisResultEntities contexto, int cantidadQuitar = 53)
        {
            string fechaFormat = fechaRevisar.ToString("yyyyMMdd");
            int fechaNum = Convert.ToInt32(fechaFormat);
            int diaSemana = fechaRevisar.DayOfWeek == 0 ? 7 : (int)fechaRevisar.DayOfWeek;
            int diaMes = fechaRevisar.Day;
            int maxIndex = ConsultasClass.ConsultarMaxIndexFecha(fechaFormat, contexto);
            List<int> listaTabIndexDifCero, listaTabIndexDifNoCero;
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados;
            List<AgrupadorTotalTabIndexDTO> listaDatosOpcionados = AnalizarListIndexDatos(contexto, fechaFormat, diaSemana, diaMes, maxIndex, fechaNum, fechaRevisar, out listaTodosResultados, out listaTabIndexDifCero, out listaTabIndexDifNoCero);
            listaDatosOpcionados = (from x in listaDatosOpcionados where x.Apariciones >= cantidadQuitar select x).ToList();
            List<int> listaResultadosMuestra = ObtenerListaResultadosMuestra(contexto, fechaFormat, listaDatosOpcionados, diaSemana, fechaNum);

            for (int j = 0; j < listaResultadosMuestra.Count; j++)
            {
                ANALISTINDEXUNG a = new ANALISTINDEXUNG();
                a.ID = ConsultasClass.ObtenerValorSecuencia(a, contexto);
                int tabindex = listaResultadosMuestra.ElementAt(j);
                int spanTabindex = ConsultasClass.ConsultarUltimoTimeSpan(contexto, tabindex, fechaFormat).Spantiempo;
                a.LINEINDEX = j + 1;
                a.FECHA = fechaRevisar;
                a.FECHANUM = fechaNum;
                a.DIASEMNUM = diaSemana;
                a.DIAMESNUM = diaMes;
                a.MESNUM = fechaRevisar.Month;
                a.ULTIMOTIMESPAN = spanTabindex;
                if (listaTabIndexDifCero.IndexOf(tabindex) != -1)
                {
                    a.RESULT = -1;
                }
                else if (listaTabIndexDifNoCero.IndexOf(tabindex) != -1)
                {
                    a.RESULT = 1;
                }
                else
                {
                    a.RESULT = 0;
                }
                contexto.ANALISTINDEXUNG.Add(a);
            }
            contexto.SaveChanges();
        }

        /// <summary>
        /// Método que maneja el análisis de los datos para los tabindex y los timespan de los datos para cada tabindex
        /// </summary>
        /// <param name="fechaRevisar">fecha para revisar la información</param>
        /// <param name="contexto">Instancia del contexto para la consulta de datos.</param>
        public static List<int> AnalizarDatosDiaActual(DateTime fechaRevisar, SisResultEntities contexto, int maxIndex, int porcentajeDatos)
        {
            string fechaFormat = fechaRevisar.ToString("yyyyMMdd");
            int fechaNum = Convert.ToInt32(fechaFormat);
            int diaSemana = fechaRevisar.DayOfWeek == 0 ? 7 : (int)fechaRevisar.DayOfWeek;
            int diaMes = fechaRevisar.Day;

            List<AgrupadorTotalTabIndexDTO> listaDatosOpcionados = ConsultasClass.ConsultarDatosParaDiaSeleccion(maxIndex, fechaFormat, contexto);
            listaDatosOpcionados = (from x in listaDatosOpcionados where x.Apariciones >= porcentajeDatos select x).ToList();
            return ObtenerListaResultadosMuestra(contexto, fechaFormat, listaDatosOpcionados, diaSemana, fechaNum);
        }

        private static List<int> ObtenerListaResultadosMuestra(SisResultEntities contexto, string fechaFormat, List<AgrupadorTotalTabIndexDTO> listaDatosOpcionados, int diaSemana, int fechaNum)
        {

            List<int> listaResultadosMuestra = new List<int>();
            for (int i = 0; i < listaDatosOpcionados.Count && listaResultadosMuestra.Count < 30; i++)
            {
                int tabindex = listaDatosOpcionados.ElementAt(i).Tabindex;
                IngresarElementosListaSeleccionados(listaResultadosMuestra, contexto, tabindex, fechaFormat, diaSemana, fechaNum);
            }
            return listaResultadosMuestra;
        }

        /// <summary>
        /// Método que maneja el análisis de los datos para los tabindex y los timespan de los datos para cada tabindex
        /// </summary>
        /// <param name="fechaRevisar">fecha para revisar la información</param>
        /// <param name="contexto">Instancia del contexto para la consulta de datos.</param>
        public static AnalisisDatosDTO AnalizarDatosDiaTemp(DateTime fechaRevisar, SisResultEntities contexto, int cantidadQuitar = 10)
        {
            string fechaFormat = fechaRevisar.ToString("yyyyMMdd");
            int fechaNum = Convert.ToInt32(fechaFormat);
            int diaSemana = fechaRevisar.DayOfWeek == 0 ? 7 : (int)fechaRevisar.DayOfWeek;
            int diaMes = fechaRevisar.Day;
            int maxIndex = ConsultasClass.ConsultarMaxIndexFecha(fechaFormat, contexto);
            List<int> listaTabIndexDifCero, listaTabIndexDifNoCero;
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados;
            List<AgrupadorTotalTabIndexDTO> listaDatosOpcionados = AnalizarListIndexDatosTemp(contexto, fechaFormat, diaSemana, diaMes, maxIndex, fechaNum, fechaRevisar, out listaTodosResultados, out listaTabIndexDifCero, out listaTabIndexDifNoCero, cantidadQuitar);
            listaDatosOpcionados = (from x in listaDatosOpcionados where x.Apariciones >= cantidadQuitar select x).ToList();
            return AnalizarSpanDatosDiaTemp(contexto, fechaFormat, diaSemana, diaMes, maxIndex, fechaNum, fechaRevisar, listaTodosResultados, listaTabIndexDifCero, listaTabIndexDifNoCero, listaDatosOpcionados);
        }

        /// <summary>
        /// Método que realiza el análisis del span de los datos
        /// </summary>
        /// <param name="contexto">Instancia del contexto para realizar la consulta</param>
        /// <param name="fechaFormat">Fecha formateada en string</param>
        /// <param name="diaSemana">Día de la semana para el análsis</param>
        /// <param name="diaMes">Día del mes para el análisis</param>
        /// <param name="maxIndex">Máximo tabindex para realizar el análisis</param>
        /// <param name="fechaNum">Fecha en formato número</param>
        /// <param name="fechaRevisar">Fecha para realizar el análisis</param>
        /// <param name="listaTodosResultados">Lista que contiene todos los resultados</param>
        /// <param name="listaTabIndexDifCero">Lista que contiene los valores con diferencia cero</param>
        /// <param name="listaTabIndexDifNoCero">Lista que contiene todos los valores con diferencia distinta de cero</param>
        public static AnalisisDatosDTO AnalizarSpanDatosDiaTemp(SisResultEntities contexto, string fechaFormat, int diaSemana, int diaMes, int maxIndex, int fechaNum, DateTime fechaRevisar,
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados, List<int> listaTabIndexDifCero, List<int> listaTabIndexDifNoCero,
            List<AgrupadorTotalTabIndexDTO> listaDatosOpcionados)
        {
            List<int> listaResultadosMuestra = ObtenerListaResultadosMuestra(contexto, fechaFormat, listaDatosOpcionados, diaSemana, fechaNum);
            //Dictionary<int, int> dictvalues = new Dictionary<int, int>();
            //for (int i = 0; i < listaResultadosMuestra.Count; i++)
            //{
            //    dictvalues.Add(i + 1, listaResultadosMuestra.ElementAt(i));
            //}
            //List<int> listIndexApariciones = ObtenerListIndex(contexto, fechaFormat);
            //listaResultadosMuestra = (from x in dictvalues where listIndexApariciones.Contains(x.Key) select x.Value).ToList();
            AnalisisDatosDTO a = new AnalisisDatosDTO();
            a.TotalDatos = listaResultadosMuestra.Count();
            a.Fecha = fechaFormat;
            for (int i = 0; i < listaResultadosMuestra.Count(); i++)
            {
                int tabindex = listaResultadosMuestra.ElementAt(i);
                if (listaTabIndexDifCero.IndexOf(tabindex) != -1)
                {
                    //int spanActual = ConsultasClass.ConsultarUltimoTimeSpan(contexto, tabindex, fechaFormat);
                    //int spanActualSem = ConsultasClass.ConsultarUltimoTimeSpan(contexto, tabindex, fechaFormat,1);
                    //int spanActualMes = ConsultasClass.ConsultarUltimoTimeSpan(contexto, tabindex, fechaFormat,2);
                    //List<AgrupadorConteosTimeSpanDTO> listaspanActual = ConsultasClass.ConsultarConteoSpanTiempo(contexto, tabindex, fechaFormat);
                    //var elemento = (from x in listaspanActual
                    //                where x.Spantiempo == spanActual
                    //                select x).FirstOrDefault();
                    //List<AgrupadorConteosTimeSpanDTO> listaspanActualSem = ConsultasClass.ConsultarConteoSpanTiempo(contexto, tabindex, fechaFormat,1, diaSemana);
                    //var elementosem = (from x in listaspanActualSem
                    //                where x.Spantiempo == spanActualSem
                    //                select x).FirstOrDefault();
                    //List<AgrupadorConteosTimeSpanDTO> listaspanActualMes = ConsultasClass.ConsultarConteoSpanTiempo(contexto, tabindex, fechaFormat,2, diaMes);
                    //var elementomes = (from x in listaspanActualMes
                    //                   where x.Spantiempo == spanActualMes
                    //                   select x).FirstOrDefault();

                    a.ResultadosNegativos++;
                    //string spanActualStr = elemento != null ? elemento.Rank+"" : "NA";
                    //string spanActualSemStr = elementosem != null ? elementosem.Rank + "" : "NA";
                    //string spanActualMesStr = elementomes != null ? elementomes.Rank + "" : "NA";
                    //a.AnalizedData+= spanActualStr+"|"+ spanActualSemStr+"|"+spanActualMesStr +"|"+ ";";
                }
                else if (listaTabIndexDifNoCero.IndexOf(tabindex) != -1)
                {
                    a.ResultadosPositivos++;
                }
            }
            a.PromedioPositivo = (double)a.ResultadosPositivos / a.TotalDatos;
            a.PromedioNegativo = (double)a.ResultadosNegativos / a.TotalDatos;

            return a;
        }

        public static List<int> ObtenerListIndex(SisResultEntities contexto, string fechaFormat)
        {
            List<AgrupadorTotalTabIndexDTO> listaIndex = ConsultasClass.ObtenerDatosListIndex(fechaFormat, contexto);
            return (from x in listaIndex select x.Lineindex).ToList();
        }

        /// <summary>
        /// Crea una estructura de diccionario para el análisis de datos
        /// </summary>
        /// <returns>Diccionario con claves hasta el maxindex</returns>
        public static Dictionary<int, AgrupadorTimeSpanDTO> ObtenerDiccionarioInicial()
        {
            Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
            for (int i = 1; i <= 765; i++)
            {
                dict.Add(i, new AgrupadorTimeSpanDTO());
                dict[i].ValoresAparicionAcumulada = new List<int>();
            }
            return dict;
        }

        /// <summary>
        /// Método que realiza el análisis de los timespan para cada tabindex
        /// </summary>
        /// <param name="FechaMax">Fecha máxima hasta donde se realiza el análisis</param>
        /// <param name="contexto">Instancia del contexto para la consulta de datos</param>
        /// <param name="listaTodosResultados">Lista con todos los resultados de la fecha a analizar</param>
        /// <returns></returns>
        public static Dictionary<int, AgrupadorTimeSpanDTO> RevisarTimeSpanDatos(DateTime FechaMax, SisResultEntities contexto)
        {

            Dictionary<int, AgrupadorTimeSpanDTO> dict = new Dictionary<int, AgrupadorTimeSpanDTO>();
            var laMin = DateTime.ParseExact("20170202", "yyyyMMdd", CultureInfo.InvariantCulture);
            for (; laMin <= FechaMax;)
            {
                RevisarTimeSpanDatos(contexto, laMin, dict);
                laMin = laMin.AddDays(1);
            }

            foreach (var item in dict)
            {
                item.Value.ValoresAparicionAcumulada = new List<int>();
                var counter = 0;
                foreach (var itemList in item.Value.ValoresAparicion)
                {
                    if (itemList == 1)
                    {
                        if (counter < 0)
                        {
                            item.Value.ValoresAparicionAcumulada.Add(counter);
                            counter = 0;
                        }
                        counter++;
                    }
                    else
                    {
                        if (counter > 0)
                        {
                            item.Value.ValoresAparicionAcumulada.Add(counter);
                            counter = 0;
                        }
                        counter--;

                    }
                }
                item.Value.ValoresAparicionAcumulada.Add(counter);
                item.Value.UltimoEnRachas = counter;

                item.Value.DictRachasAcumuladas = (from elemento in item.Value.ValoresAparicionAcumulada
                                                   group elemento by elemento into g
                                                   select new
                                                   {
                                                       Valor = g.Key,
                                                       Cantidad = g.Count()
                                                   }).ToList().ToDictionary(x => x.Valor, x => x.Cantidad);
                item.Value.DictRachasAcumuladas = (from entry in item.Value.DictRachasAcumuladas orderby entry.Value ascending select entry).ToDictionary(x => x.Key, x => x.Value);

            }
            return dict;
        }

        /// <summary>
        /// Método que realiza el timespan de los datos
        /// </summary>
        /// <param name="fecha">Fecha en la que se realiza el análisis</param>
        /// <param name="dict">Estructura que almacena la información de los timespan</param>
        /// <param name="listaTodosResultados">Lista que contiene todos los resultados</param>
        public static void RevisarTimeSpanDatos(SisResultEntities contexto, DateTime fecha, Dictionary<int, AgrupadorTimeSpanDTO> dict)
        {
            string fechaFormat = fecha.ToString("yyyyMMdd");
            List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultadosFecha = ConsultasClass.ConsultarResultadosDia(fechaFormat, contexto);
            foreach (var item in listaTodosResultadosFecha)
            {
                if (!dict.ContainsKey(item.Tabindex))
                {
                    dict.Add(item.Tabindex, new AgrupadorTimeSpanDTO());
                    dict[item.Tabindex].ValoresAparicion = new List<int>();
                }
                if (item.Diferencia == 0)
                {
                    dict[item.Tabindex].ValoresAparicion.Add(0);
                }
                else
                {
                    dict[item.Tabindex].ValoresAparicion.Add(1);
                }
            }
        }

        /// <summary>
        /// Método que realiza validación de los span de datos con diccionario
        /// (Usado para el reinicio de datos.)
        /// </summary>
        /// <param name="dict">Diccionario con los span de datos</param>
        /// <param name="u">Elemento a adicionar</param>
        /// <param name="tabindex">tabindex para realizar la validación de datos</param>
        /// <param name="diferenciaG">Valor con la diferencia de goles para marcar los span de datos</param>
        /// <param name="contexto">Instancia del contexto para las consultas y guardado de datos</param>
        public static void ValidarSpanDatos(Dictionary<int, AgrupadorTimeSpanDTO> dict, USERRESULTTABLESFS u, int tabindex, int diferenciaG, SisResultEntities contexto)
        {
            int dato = dict[tabindex].ValoresAparicionAcumulada.LastOrDefault();
            if (dato == 0)
            {
                if (diferenciaG == 0)
                {
                    dict[tabindex].ValoresAparicionAcumulada.Add(-1);
                }
                else
                {
                    dict[tabindex].ValoresAparicionAcumulada.Add(1);
                }
                u.SPANTIEMPO = dict[tabindex].ValoresAparicionAcumulada.Last();
                dict[tabindex].UltimoGuardado = u;
            }
            else
            {
                int indexUltimo = dict[tabindex].ValoresAparicionAcumulada.Count - 1;
                if (diferenciaG == 0)
                {
                    USERRESULTTABLESFS uAnt = dict[tabindex].UltimoGuardado;
                    if (dato < 0)
                    {
                        dato--;
                        dict[tabindex].ValoresAparicionAcumulada[indexUltimo] = dato;
                        uAnt.SPANTIEMPO = null;
                        u.SPANTIEMPO = dato;
                    }
                    else
                    {
                        dict[tabindex].ValoresAparicionAcumulada.Add(-1);
                        u.SPANTIEMPO = -1;
                        uAnt.SPANTIEMPO = dato;
                    }
                    contexto.USERRESULTTABLESFS.Add(uAnt);
                }
                else
                {
                    USERRESULTTABLESFS uAnt = dict[tabindex].UltimoGuardado;
                    if (dato > 0)
                    {
                        dato++;
                        dict[tabindex].ValoresAparicionAcumulada[indexUltimo] = dato;
                        uAnt.SPANTIEMPO = null;
                        u.SPANTIEMPO = dato;
                    }
                    else
                    {
                        dict[tabindex].ValoresAparicionAcumulada.Add(1);
                        u.SPANTIEMPO = 1;
                        uAnt.SPANTIEMPO = dato;
                    }
                    contexto.USERRESULTTABLESFS.Add(uAnt);
                }

            }
            contexto.USERRESULTTABLESFS.Add(u);
            dict[tabindex].UltimoGuardado = u;
        }

        /// <summary>
        /// Método que realiza la validación de los span del día anterior, y actualizar los datos si es requerido
        /// y luego asignar los del día acutal
        /// </summary>
        /// <param name="contexto">Instancia del contexto para la consulta de datos</param>
        /// <param name="listTabindex">Lista con los tabindex para realizar la consulta</param>
        /// <param name="listElementosAgregados">Lista con los elementos adicionados, para realizar el análisis</param>
        public static void ValidarSpanDatosDiaAnterior(SisResultEntities contexto, List<decimal?> listTabindex, List<USERRESULTTABLESFS> listElementosAgregados,
            int diasemnum, int diamesnum)
        {
            int maxTabindex = ConsultasClass.ConsultarMaxIndexResultados(contexto);
            maxTabindex = (int)listTabindex.Last() > maxTabindex ? maxTabindex : (int)listTabindex.Last();
            int maxFechaNumIndex;
            List<USERRESULTTABLESFS> listaDatosUltimosSpan;
            for (int r = 0; r < 3; r++)
            {
                switch (r)
                {
                    case 1:
                        maxFechaNumIndex = ConsultasClass.ConsultarMaxFechaTabindex(contexto, maxTabindex, 1, diasemnum);
                        listaDatosUltimosSpan = (from b in contexto.USERRESULTTABLESFS
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANTIEMPOSEM != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 && b.DIASEMNUM == diasemnum
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;
                    case 2:
                        maxFechaNumIndex = ConsultasClass.ConsultarMaxFechaTabindex(contexto, maxTabindex, 2, diamesnum);
                        listaDatosUltimosSpan = (from b in contexto.USERRESULTTABLESFS
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANTIEMPOMES != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 && b.DIAMESNUM == diamesnum
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;
                    default:
                        maxFechaNumIndex = ConsultasClass.ConsultarMaxFechaTabindex(contexto, maxTabindex, 0, 0);
                        listaDatosUltimosSpan = (from b in contexto.USERRESULTTABLESFS
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANTIEMPO != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;
                }

                for (int i = 0; i < listTabindex.Count; i++)
                {
                    int tabIndex = (int)listTabindex.ElementAt(i);
                    var uAnt = listaDatosUltimosSpan.Where(x => x.TABINDEX == tabIndex).FirstOrDefault();
                    var uActual = listElementosAgregados.Where(x => x.TABINDEX == tabIndex).FirstOrDefault();
                    if(uAnt != null)
                    {
                        ValidarSpanColumna(uAnt, uActual, r);
                    }
                    contexto.USERRESULTTABLESFS.Add(uActual);
                }
            }
        }

        private static void ValidarSpanColumna(USERRESULTTABLESFS uAnt, USERRESULTTABLESFS uActual, int casoColumna)
        {
            if (uActual.DIFERENCIAG == 0)
            {
                if (casoColumna == 0)
                {                
                    if (uAnt.SPANTIEMPO < 0)
                    {
                        uActual.SPANTIEMPO = --uAnt.SPANTIEMPO;
                        uAnt.SPANTIEMPO = null;
                    }
                    else
                    {
                        uActual.SPANTIEMPO = -1;

                    }
                }
                else if(casoColumna == 1)
                {
                    if (uAnt.SPANTIEMPOSEM < 0)
                    {
                        uActual.SPANTIEMPOSEM = --uAnt.SPANTIEMPOSEM;
                        uAnt.SPANTIEMPOSEM = null;
                    }
                    else
                    {
                        uActual.SPANTIEMPOSEM = -1;

                    }
                }
                else if(casoColumna == 2)
                {
                    if (uAnt.SPANTIEMPOMES < 0)
                    {
                        uActual.SPANTIEMPOMES = --uAnt.SPANTIEMPOMES;
                        uAnt.SPANTIEMPOMES = null;
                    }
                    else
                    {
                        uActual.SPANTIEMPOMES = -1;

                    }
                }
            }
            else
            {
                if(casoColumna == 0)
                {
                    if (uAnt.SPANTIEMPO > 0)
                    {
                        uActual.SPANTIEMPO = ++uAnt.SPANTIEMPO;
                        uAnt.SPANTIEMPO = null;
                    }
                    else
                    {
                        uActual.SPANTIEMPO = 1;

                    }
                }
                else if(casoColumna == 1)
                {
                    if (uAnt.SPANTIEMPOSEM > 0)
                    {
                        uActual.SPANTIEMPOSEM = ++uAnt.SPANTIEMPOSEM;
                        uAnt.SPANTIEMPOSEM = null;
                    }
                    else
                    {
                        uActual.SPANTIEMPOSEM = 1;

                    }
                }
                else if(casoColumna == 2)
                {
                    if (uAnt.SPANTIEMPOMES > 0)
                    {
                        uActual.SPANTIEMPOMES = ++uAnt.SPANTIEMPOMES;
                        uAnt.SPANTIEMPOMES = null;
                    }
                    else
                    {
                        uActual.SPANTIEMPOMES = 1;

                    }
                }
            }
        }

        /// <summary>
        /// Realiza la validación de los datos para la fecha específica de los que menos aparecen con igualdades
        /// depurando los valores tabindex con mas igualdades
        /// </summary>
        /// <param name="contexto">Instancia del contexto para la consulta</param>
        /// <param name="fechaFormat">Fecha formateada para la consulta</param>
        /// <param name="diaSemana">Día de la semana para la consulta</param>
        /// <param name="diaMes">Día del mes para la consulta</param>
        /// <param name="maxIndex">Máximo tabindex para realizar la verificación</param>
        /// <param name="fechaNum">Valor númerico para la fecha formateada</param>
        /// <param name="fechaRevisar">Fecha en formato Date</param>
        /// <param name="listaTodosResultados">Lista que contiene todos los resultados para consultar</param>
        /// <param name="cantidadQuitar">Cantidad de elementos para quitar de la lista si no ecibe nada, el valor por default es 10</param>
        private static List<AgrupadorTotalTabIndexDTO> AnalizarListIndexDatos(SisResultEntities contexto, string fechaFormat, int diaSemana,
            int diaMes, int maxIndex, int fechaNum, DateTime fechaRevisar,
             out List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados, out List<int> listaTabIndexDifCero, out List<int> listaTabIndexDifNoCero)
        {
            List<AgrupadorTotalTabIndexDTO> listaResultados = ConsultasClass.ConsultarDatosParaDiaSeleccion(maxIndex, fechaFormat, contexto);
            listaTodosResultados = ConsultasClass.ConsultarResultadosDia(fechaFormat, contexto);
            listaTabIndexDifCero = (from x in listaTodosResultados where x.Diferencia == 0 select x.Tabindex).ToList();
            listaTabIndexDifNoCero = (from x in listaTodosResultados where x.Diferencia != 0 select x.Tabindex).ToList();
            return listaResultados;
        }

        /// <summary>
        /// Realiza la validación de los datos para la fecha específica de los que menos aparecen con igualdades
        /// depurando los valores tabindex con mas igualdades
        /// </summary>
        /// <param name="contexto">Instancia del contexto para la consulta</param>
        /// <param name="fechaFormat">Fecha formateada para la consulta</param>
        /// <param name="diaSemana">Día de la semana para la consulta</param>
        /// <param name="diaMes">Día del mes para la consulta</param>
        /// <param name="maxIndex">Máximo tabindex para realizar la verificación</param>
        /// <param name="fechaNum">Valor númerico para la fecha formateada</param>
        /// <param name="fechaRevisar">Fecha en formato Date</param>
        /// <param name="listaTodosResultados">Lista que contiene todos los resultados para consultar</param>
        /// <param name="cantidadQuitar">Cantidad de elementos para quitar de la lista si no ecibe nada, el valor por default es 10</param>
        private static List<AgrupadorTotalTabIndexDTO> AnalizarListIndexDatosTemp(SisResultEntities contexto, string fechaFormat, int diaSemana,
            int diaMes, int maxIndex, int fechaNum, DateTime fechaRevisar,
             out List<AgrupadorTabIndexDiferenciaDTO> listaTodosResultados, out List<int> listaTabIndexDifCero, out List<int> listaTabIndexDifNoCero, int cantidadQuitar = 10)
        {
            List<AgrupadorTotalTabIndexDTO> listaResultados = ConsultasClass.ConsultarDatosParaDiaSeleccion(maxIndex, fechaFormat, contexto);
            listaTodosResultados = ConsultasClass.ConsultarResultadosDia(fechaFormat, contexto);
            //List<AgrupadorTotalTabIndexDTO> listaConteoIgualdadDia = ConsultasClass.ConsultarDatosIgualdadDia(contexto, diaSemana, maxIndex, fechaFormat);
            //List<int> listaIgualdades = (from x in listaConteoIgualdadDia select x.Tabindex).Take(cantidadQuitar).ToList();
            //List<AgrupadorTotalTabIndexDTO> listaResultadosTemp = (from x in listaResultados where !(listaIgualdades.Contains(x.Tabindex)) select x).ToList();
            listaTabIndexDifCero = (from x in listaTodosResultados where x.Diferencia == 0 select x.Tabindex).ToList();
            listaTabIndexDifNoCero = (from x in listaTodosResultados where x.Diferencia != 0 select x.Tabindex).ToList();
            return listaResultados;
        }

        /// <summary>
        /// Método que realiza las validaciones para ingresar los datos de los elementos seleccionados a la lista de index finales
        /// </summary>
        /// <param name="spanActual">SpanActual para realizar la validación</param>
        /// <param name="index">Index de la lista que se está validando</param>
        /// <param name="listaResultadosMuestra">Lista que contiene los elementos seleccionados</param>
        /// <param name="contexto">Instancia del contexto para las consultas</param>
        /// <param name="tabindex">Tabindex validado</param>
        /// <param name="fechaFormat">Fecha sobre la que se realizan las validaciones</param>
        private static void IngresarElementosListaSeleccionados(List<int> listaResultadosMuestra,
            SisResultEntities contexto, int tabindex, string fechaFormat,
            int diasemana, int fechaNum)
        {
            AgrupadorFechaNumValor objSpanActual = ConsultasClass.ConsultarUltimoTimeSpan(contexto, tabindex, fechaFormat);
            AgrupadorFechaNumValor objSpanActualSem = ConsultasClass.ConsultarUltimoTimeSpan(contexto, tabindex, fechaFormat, 1);
            AgrupadorFechaNumValor objSpanActualMes = ConsultasClass.ConsultarUltimoTimeSpan(contexto, tabindex, fechaFormat, 2);
            int total = objSpanActual.Spantiempo + objSpanActualSem.Spantiempo + objSpanActualMes.Spantiempo;
            if (total >= 15) return;

            //DateTime dt = DateTime.ParseExact(fechaFormat, "yyyyMMdd", CultureInfo.InvariantCulture);
            //List<AgrupadorConteosTimeSpanDTO> listaspanActual = ConsultasClass.ConsultarConteoSpanTiempo(contexto, tabindex, fechaFormat);
            //var elemento = (from x in listaspanActual
            //                where x.Spantiempo == spanActual
            //                select x).FirstOrDefault();
            //List<AgrupadorConteosTimeSpanDTO> listaspanActualSem = ConsultasClass.ConsultarConteoSpanTiempo(contexto, tabindex, fechaFormat, 1, diasemana);
            //var elementosem = (from x in listaspanActualSem
            //                   where x.Spantiempo == spanActualSem
            //                   select x).FirstOrDefault();
            //List<AgrupadorConteosTimeSpanDTO> listaspanActualMes = ConsultasClass.ConsultarConteoSpanTiempo(contexto, tabindex, fechaFormat, 2, dt.Day);
            //var elementomes = (from x in listaspanActualMes
            //                   where x.Spantiempo == spanActualMes
            //                   select x).FirstOrDefault();
            //elementomes = elementomes == null ? new AgrupadorConteosTimeSpanDTO() : elementomes;
            //int totalRank = elemento.Rank + elementosem.Rank + elementomes.Rank;
            //if (totalRank <= 3) return;            
            listaResultadosMuestra.Add(tabindex);
        }

        /// <summary>
        /// Método que realiza la validación del spantiempo actual, ya que algunos valores pueden retornar null
        /// </summary>
        /// <param name="fechaRevisar">Fecha para realizar la validación del span</param>
        /// <param name="listspantiempo">Lista que contiene los datos de los spantiempo ordenados descendentemente</param>
        /// <param name="fechaActual">Bandera que sirve para controlar si la validación se hace para la fecha actual
        /// y de esta forma evitar la multiplicación del valor</param>
        /// <returns></returns>
        private static int ObtenerSpanTiempoActual(List<AgrupadorConteosTimeSpanDTO> listspantiempo)
        {
            if (!listspantiempo.ToList().Any())
                return 0;

            AgrupadorConteosTimeSpanDTO act = listspantiempo.ElementAt(0);
            if (act.Spantiempo != null)
            {
                return (int)act.Spantiempo;
            }
            else
            {
                int spanActual = 0;
                int lastElement = 0;
                for (int i = 0; i < listspantiempo.Count; i++)
                {
                    act = listspantiempo.ElementAt(i);
                    if (act.Spantiempo != null)
                    {
                        lastElement = (int)act.Spantiempo;
                        break;
                    }
                    spanActual++;
                }
                if (lastElement > 0)
                {
                    spanActual = spanActual * -1;
                }
                return spanActual;
            }
        }

        /// <summary>
        /// Método que realiza el retorno del valor para el span de tiempo del tabindex
        /// de acuerdo a si la diferencia es cero o no, y dependiendo del último valor del span
        /// </summary>
        /// <param name="tabindex">Tabindex a consultar</param>
        /// <param name="diferenciaG">Valor de la diferencia para validar</param>
        /// <param name="contexto"></param>
        /// <returns></returns>
        internal static int ValidarSpanTiempo(int tabindex, int diferenciaG, SisResultEntities contexto, string fechaNum, int caso)
        {
            int valorSpan = 0;
            AgrupadorFechaNumValor ultimoSpan = ConsultasClass.ConsultarUltimoTimeSpan(contexto, tabindex, fechaNum, caso);
            if (diferenciaG == 0)
            {
                valorSpan = ultimoSpan.Spantiempo >= 0 ? -1 : --ultimoSpan.Spantiempo;
            }
            else
            {
                valorSpan = ultimoSpan.Spantiempo <= 0 ? 1 : ++ultimoSpan.Spantiempo;
            }
            return valorSpan;
        }
    }
}
