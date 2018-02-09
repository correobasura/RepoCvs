using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataFlashOrdered
    {
        /// <summary>
        /// Valida los elementos ingresados desde cero
        /// </summary>
        /// <param name="lista">elementos a revisar</param>
        /// <returns>Lista con datos actualizados de los span analizados</returns>
        public static List<FLASHORDERED> AnalizarGeneral(List<FLASHORDERED> lista)
        {
            List<int?> listaDistinctIndex = lista.Select(x => x.TABINDEX).Distinct().ToList();
            foreach (var item in listaDistinctIndex)
            {
                int indexActual = 0;
                var listaItems = (from x in lista where x.TABINDEX.Equals(item) select x).OrderBy(x => x.FECHA);
                foreach (var item2 in listaItems)
                {
                    if (item2.DIFERENCIAG == 0)
                    {
                        if (indexActual < 0)
                        {
                            indexActual--;
                        }
                        else
                        {
                            indexActual = -1;
                        }
                    }
                    else
                    {
                        if (indexActual > 0)
                        {
                            indexActual++;
                        }
                        else
                        {
                            indexActual = 1;
                        }
                    }
                    item2.SPANDIARIOHISTORICO = indexActual;
                }
                for (decimal i = 1; i <= 7; i++)
                {
                    indexActual = 0;
                    var listaItemsDia = (from x in listaItems where x.DIASEM.Equals(i) select x);
                    foreach (var itemDia in listaItemsDia)
                    {
                        if (itemDia.DIFERENCIAG == 0)
                        {
                            if (indexActual < 0)
                            {
                                indexActual--;
                            }
                            else
                            {
                                indexActual = -1;
                            }
                        }
                        else
                        {
                            if (indexActual > 0)
                            {
                                indexActual++;
                            }
                            else
                            {
                                indexActual = 1;
                            }
                        }
                        itemDia.SPANSEMANAHISTORICO = indexActual;
                    }
                }
                for (decimal i = 1; i <= 31; i++)
                {
                    indexActual = 0;
                    var listaItemsDiaMes = (from x in listaItems where x.DIAMES.Equals(i) select x);
                    foreach (var itemDia in listaItemsDiaMes)
                    {
                        if (itemDia.DIFERENCIAG == 0)
                        {
                            if (indexActual < 0)
                            {
                                indexActual--;
                            }
                            else
                            {
                                indexActual = -1;
                            }
                        }
                        else
                        {
                            if (indexActual > 0)
                            {
                                indexActual++;
                            }
                            else
                            {
                                indexActual = 1;
                            }
                        }
                        itemDia.SPANMESHISTORICO = indexActual;
                    }
                }

                listaItems = (from x in lista where x.TABINDEX.Equals(item) select x).OrderByDescending(x => x.FECHA);
                bool asignar = true;
                foreach (var item2 in listaItems)
                {
                    if (asignar)
                    {
                        item2.SPANDIARIOACTUAL = item2.SPANDIARIOHISTORICO;
                    }
                    else
                    {
                        item2.SPANDIARIOACTUAL = null;
                    }
                    asignar = item2.SPANDIARIOHISTORICO == 1 || item2.SPANDIARIOHISTORICO == -1;
                }
                asignar = true;
                for (decimal i = 1; i <= 7; i++)
                {
                    var listaItemsDia = (from x in listaItems where x.DIASEM.Equals(i) select x);
                    foreach (var itemDia in listaItemsDia)
                    {
                        if (asignar)
                        {
                            itemDia.SPANSEMANAACTUAL = itemDia.SPANSEMANAHISTORICO;
                        }
                        else
                        {
                            itemDia.SPANSEMANAACTUAL = null;
                        }
                        asignar = itemDia.SPANSEMANAHISTORICO == 1 || itemDia.SPANSEMANAHISTORICO == -1;
                    }
                }
                asignar = true;
                for (decimal i = 1; i <= 31; i++)
                {
                    var listaItemsDia = (from x in listaItems where x.DIAMES.Equals(i) select x);
                    foreach (var itemDia in listaItemsDia)
                    {
                        if (asignar)
                        {
                            itemDia.SPANMESACTUAL = itemDia.SPANMESHISTORICO;
                        }
                        else
                        {
                            itemDia.SPANMESACTUAL = null;
                        }
                        asignar = itemDia.SPANMESHISTORICO == 1 || itemDia.SPANMESHISTORICO == -1;
                    }
                }
            }
            lista = lista.OrderBy(x => x.ID).ToList();
            //GuardarElementosGeneral(contexto, lista);
            return lista;
        }

        /// <summary>
        /// Guarda los elementos que se leen de los archivos generados del análisis general
        /// </summary>
        /// <param name="contexto">instancia para la persistencia de los objetos</param>
        /// <param name="maxIdFile">Identificador del máximo archivo generado</param>
        public static void GuardarElementosGeneral(SisResultEntities contexto, int maxIdFile)
        {
            List<FLASHORDERED> lista = new List<FLASHORDERED>();
            for (int i = 1; i < maxIdFile; i++)
            {
                string filePath = @"D:\temp" + i + ".csv";
                StreamReader fileReader = new StreamReader(filePath);
                String line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    FLASHORDERED fs = JsonConvert.DeserializeObject<FLASHORDERED>(line);
                    //if (fs.GROUPLETTER.Length > 1)
                    //{
                    //    var data3 = "";
                    //}
                    lista.Add(fs);
                }
                //var data = (from x in LT where x.GROUPLETTER.Length != 1 select x);
                //var data2 = "";
                contexto.FLASHORDERED.AddRange(lista);
                contexto.SaveChanges();
                lista.Clear();
            }
           //var groups = lista.GroupBy(info => info.ID)
           //     .Select(group => new {
           //         Metric = group.Key,
           //         Count = group.Count()
           //     })
           //     .Where(x => x.Count > 1);
           // if (groups.Count() != 0)
           // {
           //     var stop = "";
           // }


        }

        /// <summary>
        /// Método que realiza el guardado y la validación de los datos actuales, junto con los 
        /// span para cada uno de las columnas de span
        /// </summary>
        /// <param name="lista">Elementos a persistir</param>
        /// <param name="contexto">Instancia del contexto</param>
        public static void InsertarElementosActuales(List<FLASHORDERED> lista, SisResultEntities contexto)
        {
            List<int?> listaFechaNum = (from x in lista orderby x.FECHANUM select x.FECHANUM).Distinct().ToList();
            foreach (var fechaNum in listaFechaNum)
            {
                List<FLASHORDERED> listaPersist = lista.Where(x => x.FECHANUM == fechaNum).OrderBy(x=>x.ID).ToList();
                foreach (var obj in listaPersist)
                {
                    obj.TABINDEXSEQ = ConsultasClassFO.ConsultarNextTabindexSeq(contexto, obj.TABINDEX);
                    obj.TABINDEXLETTERSEQ = ConsultasClassFO.ConsultarNextTabindexLetterSeq(contexto, obj.GROUPLETTER, obj.TABINDEXLETTER);
                    obj.SPANDIARIOHISTORICO = ValidarSpanTiempo((int)obj.TABINDEX, (int)obj.DIFERENCIAG, contexto, (int)obj.FECHANUM, 0, (int)obj.DIASEM, (int)obj.DIAMES, (int)obj.DIAANIO);
                    obj.SPANSEMANAHISTORICO = ValidarSpanTiempo((int)obj.TABINDEX, (int)obj.DIFERENCIAG, contexto, (int)obj.FECHANUM, 1, (int)obj.DIASEM, (int)obj.DIAMES, (int)obj.DIAANIO);
                    obj.SPANMESHISTORICO = ValidarSpanTiempo((int)obj.TABINDEX, (int)obj.DIFERENCIAG, contexto, (int)obj.FECHANUM, 2, (int)obj.DIASEM, (int)obj.DIAMES, (int)obj.DIAANIO);
                    obj.SPANANIOHISTORICO = ValidarSpanTiempo((int)obj.TABINDEX, (int)obj.DIFERENCIAG, contexto, (int)obj.FECHANUM, 3, (int)obj.DIASEM, (int)obj.DIAMES, (int)obj.DIAANIO);

                }
                contexto.FLASHORDERED.AddRange(listaPersist);
                contexto.SaveChanges();
                ValidarSpanDatosAnterior(contexto, listaPersist);
            }
        }

        /// <summary>
        /// Método que realiza el retorno del valor para el span de tiempo del tabindex
        /// de acuerdo a si la diferencia es cero o no, y dependiendo del último valor del span
        /// </summary>
        /// <param name="tabindex">Tabindex a consultar</param>
        /// <param name="diferenciaG">Valor de la diferencia para validar</param>
        /// <param name="contexto"></param>
        /// <param name="fechaNum">Fecha de la validación</param>
        /// <param name="caso">Caso para validar la columna </param>
        /// <param name="diaSem">Dia sem</param>
        /// <param name="diaMes">Dia mes</param>
        /// <param name="diaAnio">Dia anio</param>
        /// <returns>Valor para asignar al span</returns>
        private static int ValidarSpanTiempo(int tabindex, int diferenciaG, SisResultEntities contexto, int fechaNum, int caso,
            int diaSem, int diaMes, int diaAnio)
        {
            int valorSpan = 0;
            AgrupadorFechaNumValor ultimoSpan = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, tabindex, fechaNum, diaSem, diaMes, diaAnio, caso);
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

        /// <summary>
        /// Método que realiza la validación de los span del día anterior, y actualizar los datos si es requerido
        /// y luego asignar los del día acutal
        /// </summary>
        /// <param name="contexto">Instancia del contexto para la consulta de datos</param>
        /// <param name="listElementosAgregados">Lista con los elementos adicionados, para realizar el análisis</param>
        public static void ValidarSpanDatosAnterior(SisResultEntities contexto, List<FLASHORDERED> listElementosAgregados)
        {
            int maxTabindex = ConsultasClassFO.ConsultarMaxIndexResultados(contexto);
            List<int?> listTabindex = (from x in listElementosAgregados select x.TABINDEX).ToList();
            maxTabindex = (int)listTabindex.Max()> maxTabindex ? maxTabindex : (int)listTabindex.Max();
            int maxFechaNumIndex;
            var elementTemp = listElementosAgregados.ElementAt(0);
            List<FLASHORDERED> listaDatosUltimosSpan;
            for (int r = 0; r <= 3; r++)
            {
                switch (r)
                {
                    case 1:
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex, 1, (int)elementTemp.DIASEM);
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANSEMANAACTUAL != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 && b.DIASEM == elementTemp.DIASEM
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;
                    case 2:
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex, 2, (int)elementTemp.DIAMES);
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANMESACTUAL != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 && b.DIAMES == elementTemp.DIAMES
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;
                    case 3:
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex, 3, (int)elementTemp.DIAANIO);
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANANIOACTUAL != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 && b.DIAANIO == elementTemp.DIAANIO
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;
                    default:
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex, 0, 0);
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANDIARIOACTUAL != null
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
                    if (uAnt != null)
                    {
                        ValidarSpanColumna(uAnt, uActual, r);
                    }
                }
            }
            contexto.SaveChanges();
        }

        /// <summary>
        /// Método que realiza la validación del span de acuerdo al caso de la columna
        /// </summary>
        /// <param name="uAnt">Elemento de la última referencia</param>
        /// <param name="uActual">Elemento actual</param>
        /// <param name="casoColumna">Caso para asignar el valor a la columna</param>
        private static void ValidarSpanColumna(FLASHORDERED uAnt, FLASHORDERED uActual, int casoColumna)
        {
            if (uActual.DIFERENCIAG == 0)
            {
                if (casoColumna == 0)
                {
                    if (uAnt.SPANDIARIOACTUAL < 0)
                    {
                        uActual.SPANDIARIOACTUAL = --uAnt.SPANDIARIOACTUAL;
                        uAnt.SPANDIARIOACTUAL = null;
                    }
                    else
                    {
                        uActual.SPANDIARIOACTUAL = -1;

                    }
                }
                else if (casoColumna == 1)
                {
                    if (uAnt.SPANSEMANAACTUAL < 0)
                    {
                        uActual.SPANSEMANAACTUAL = --uAnt.SPANSEMANAACTUAL;
                        uAnt.SPANSEMANAACTUAL = null;
                    }
                    else
                    {
                        uActual.SPANSEMANAACTUAL = -1;

                    }
                }
                else if (casoColumna == 2)
                {
                    if (uAnt.SPANMESACTUAL < 0)
                    {
                        uActual.SPANMESACTUAL = --uAnt.SPANMESACTUAL;
                        uAnt.SPANMESACTUAL = null;
                    }
                    else
                    {
                        uActual.SPANMESACTUAL = -1;

                    }
                }
                else if (casoColumna == 3)
                {
                    if (uAnt.SPANANIOACTUAL < 0)
                    {
                        uActual.SPANANIOACTUAL = --uAnt.SPANANIOACTUAL;
                        uAnt.SPANANIOACTUAL = null;
                    }
                    else
                    {
                        uActual.SPANANIOACTUAL = -1;

                    }
                }
            }
            else
            {
                if (casoColumna == 0)
                {
                    if (uAnt.SPANDIARIOACTUAL > 0)
                    {
                        uActual.SPANDIARIOACTUAL = ++uAnt.SPANDIARIOACTUAL;
                        uAnt.SPANDIARIOACTUAL = null;
                    }
                    else
                    {
                        uActual.SPANDIARIOACTUAL = 1;

                    }
                }
                else if (casoColumna == 1)
                {
                    if (uAnt.SPANSEMANAACTUAL > 0)
                    {
                        uActual.SPANSEMANAACTUAL = ++uAnt.SPANSEMANAACTUAL;
                        uAnt.SPANSEMANAACTUAL = null;
                    }
                    else
                    {
                        uActual.SPANSEMANAACTUAL = 1;

                    }
                }
                else if (casoColumna == 2)
                {
                    if (uAnt.SPANMESACTUAL > 0)
                    {
                        uActual.SPANMESACTUAL = ++uAnt.SPANMESACTUAL;
                        uAnt.SPANMESACTUAL = null;
                    }
                    else
                    {
                        uActual.SPANMESACTUAL = 1;

                    }
                }
                else if (casoColumna == 3)
                {
                    if (uAnt.SPANANIOACTUAL > 0)
                    {
                        uActual.SPANANIOACTUAL = ++uAnt.SPANANIOACTUAL;
                        uAnt.SPANANIOACTUAL = null;
                    }
                    else
                    {
                        uActual.SPANANIOACTUAL = 1;

                    }
                }
            }
        }

        public static void ValidarElementosDia(DateTime fecha, int caso)
        {
            List<FLASHORDERED> listaHtmlTemp = UtilGeneral.UtilHtml.LeerInfoHtmlTempActual(fecha, caso);
            List<string> listaDistinctChar = listaHtmlTemp.Select(x => x.GROUPLETTER).Distinct().ToList();
            Dictionary<string, int> keyValuePairMaxIndexChar = new Dictionary<string, int>();
            foreach (var item in listaDistinctChar)
            {
                int maxValueIndexChar = (int)(from x in listaHtmlTemp
                                         where x.GROUPLETTER.Equals(item)
                                         select x.TABINDEXLETTER).Max();
                keyValuePairMaxIndexChar.Add(item, maxValueIndexChar);
            }
            string agrupador = "({0})";
            string temp = "(groupletter  = '{0}' AND tabindexletter <= {1})";
            List<string> listaJoins = new List<string>();
            foreach (var item in keyValuePairMaxIndexChar)
            {
                listaJoins.Add(string.Format(temp, item.Key, item.Value));
            }
            string strJoin = string.Join(" OR ", listaJoins);
            strJoin = string.Format(agrupador, strJoin);
            UtilGeneral.UtilFilesIO.EscribirArchivoCsv(listaHtmlTemp);
        }
    }
}
