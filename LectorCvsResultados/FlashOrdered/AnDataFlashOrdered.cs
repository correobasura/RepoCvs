using LectorCvsResultados.UtilGeneral;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            List<string> listDistinctGrouLetter = lista.Select(x => x.GROUPLETTER).Distinct().ToList();
            foreach (var itemLetter in listDistinctGrouLetter)
            {
                List<int> listaDistinctIndex2 = lista.Where(x => x.GROUPLETTER.Equals(itemLetter)).Select(x => x.TABINDEXLETTER).Distinct().ToList();
                foreach (var item in listaDistinctIndex2)
                {
                    int indexActual = 0;
                    var listaItems = (from x in lista
                                      where x.TABINDEXLETTER.Equals(item)
                      && x.GROUPLETTER.Equals(itemLetter)
                                      select x).OrderBy(x => x.FECHA);
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
                        item2.SPANTIGLDIAHIST = indexActual;
                    }
                    for (int i = 1; i <= 7; i++)
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
                            itemDia.SPANTIGLSEMHIST = indexActual;
                        }
                    }
                    for (int i = 1; i <= 31; i++)
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
                            itemDia.SPANTIGLMESHIST = indexActual;
                        }
                    }

                    listaItems = (from x in lista
                                  where x.TABINDEXLETTER.Equals(item)
                                  && x.GROUPLETTER.Equals(itemLetter)
                                  select x).OrderByDescending(x => x.FECHA);
                    bool asignar = true;
                    foreach (var item2 in listaItems)
                    {
                        if (asignar)
                        {
                            item2.SPANTIGLDIAACT = item2.SPANTIGLDIAHIST;
                        }
                        else
                        {
                            item2.SPANTIGLDIAACT = null;
                        }
                        asignar = item2.SPANTIGLDIAHIST == 1 || item2.SPANTIGLDIAHIST == -1;
                    }
                    asignar = true;
                    for (int i = 1; i <= 7; i++)
                    {
                        var listaItemsDia = (from x in listaItems
                                             where x.DIASEM.Equals(i)
                                             && x.GROUPLETTER.Equals(itemLetter)
                                             select x);
                        foreach (var itemDia in listaItemsDia)
                        {
                            if (asignar)
                            {
                                itemDia.SPANTIGLSEMACT = itemDia.SPANTIGLSEMHIST;
                            }
                            else
                            {
                                itemDia.SPANTIGLSEMACT = null;
                            }
                            asignar = itemDia.SPANTIGLSEMHIST == 1 || itemDia.SPANTIGLSEMHIST == -1;
                        }
                    }
                    asignar = true;
                    for (int i = 1; i <= 31; i++)
                    {
                        var listaItemsDia = (from x in listaItems
                                             where x.DIAMES.Equals(i)
                                             && x.GROUPLETTER.Equals(itemLetter)
                                             select x);
                        foreach (var itemDia in listaItemsDia)
                        {
                            if (asignar)
                            {
                                itemDia.SPANTIGLMESACT = itemDia.SPANTIGLMESHIST;
                            }
                            else
                            {
                                itemDia.SPANTIGLMESACT = null;
                            }
                            asignar = itemDia.SPANTIGLMESHIST == 1 || itemDia.SPANTIGLMESHIST == -1;
                        }
                    }
                }
            }

            List<int> listaDistinctIndex = lista.Select(x => x.TABINDEX).Distinct().ToList();
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
                    item2.SPANTIDIAHIST = indexActual;
                }
                for (int i = 1; i <= 7; i++)
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
                        itemDia.SPANTISEMHIST = indexActual;
                    }
                }
                for (int i = 1; i <= 31; i++)
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
                        itemDia.SPANTIMESHIST = indexActual;
                    }
                }

                listaItems = (from x in lista where x.TABINDEX.Equals(item) select x).OrderByDescending(x => x.FECHA);
                bool asignar = true;
                foreach (var item2 in listaItems)
                {
                    if (asignar)
                    {
                        item2.SPANTIDIAACT = item2.SPANTIDIAHIST;
                    }
                    else
                    {
                        item2.SPANTIDIAACT = null;
                    }
                    asignar = item2.SPANTIDIAHIST == 1 || item2.SPANTIDIAHIST == -1;
                }
                asignar = true;
                for (int i = 1; i <= 7; i++)
                {
                    var listaItemsDia = (from x in listaItems where x.DIASEM.Equals(i) select x);
                    foreach (var itemDia in listaItemsDia)
                    {
                        if (asignar)
                        {
                            itemDia.SPANTISEMACT = itemDia.SPANTISEMHIST;
                        }
                        else
                        {
                            itemDia.SPANTISEMACT = null;
                        }
                        asignar = itemDia.SPANTISEMHIST == 1 || itemDia.SPANTISEMHIST == -1;
                    }
                }
                asignar = true;
                for (int i = 1; i <= 31; i++)
                {
                    var listaItemsDia = (from x in listaItems where x.DIAMES.Equals(i) select x);
                    foreach (var itemDia in listaItemsDia)
                    {
                        if (asignar)
                        {
                            itemDia.SPANTIMESACT = itemDia.SPANTIMESHIST;
                        }
                        else
                        {
                            itemDia.SPANTIMESACT = null;
                        }
                        asignar = itemDia.SPANTIMESHIST == 1 || itemDia.SPANTIMESHIST == -1;
                    }
                }
            }
            lista = lista.OrderBy(x => x.ID).ToList();
            List<FLASHORDERED> listaTemp = (from x in lista where x.GROUPLETTER.Equals("B") && x.TABINDEXLETTER == 1 select x).ToList();
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
                string filePath = @"D:\temp2\file" + i + ".csv";
                StreamReader fileReader = new StreamReader(filePath);
                String line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    FLASHORDERED fs = JsonConvert.DeserializeObject<FLASHORDERED>(line);
                    lista.Add(fs);
                }
                contexto.FLASHORDERED.AddRange(lista);
                contexto.SaveChanges();
                lista.Clear();
            }
        }

        /// <summary>
        /// Método que realiza el guardado y la validación de los datos actuales, junto con los
        /// span para cada uno de las columnas de span
        /// </summary>
        /// <param name="lista">Elementos a persistir</param>
        /// <param name="contexto">Instancia del contexto</param>
        public static void InsertarElementosActuales(List<FLASHORDERED> lista, SisResultEntities contexto)
        {
            TOTALESDIA td;
            string strJoin;
            int maxTabindex;
            List<AgrupadorFechaNumValor> listaDiaHist;
            List<AgrupadorFechaNumValor> listaDiaSemHist;
            List<AgrupadorFechaNumValor> listaDiaMesHist;
            List<AgrupadorFechaNumValor> listaDiaAnioHist;
            List<AgrupadorFechaNumValor> listaDiaGlHist;
            List<AgrupadorFechaNumValor> listaDiaGlSemHist;
            List<AgrupadorFechaNumValor> listaDiaGlMesHist;
            List<AgrupadorFechaNumValor> listaDiaGlAnioHist;
            List<int> listTabindex;
            FLASHORDERED elementTemp;
            List<int> listaFechaNum = (from x in lista orderby x.FECHANUM select x.FECHANUM).Distinct().ToList();
            foreach (var fechaNum in listaFechaNum)
            {
                List<FLASHORDERED> listaPersist = lista.Where(x => x.FECHANUM == fechaNum).OrderBy(x => x.ID).ToList();
                td = new TOTALESDIA();
                td.ID = fechaNum;
                td.TOTAL = listaPersist.Count;
                contexto.TOTALESDIA.Add(td);
                strJoin = UtilHtml.ObtenerJoinElementos(listaPersist);
                elementTemp = listaPersist.FirstOrDefault();
                maxTabindex = (from x in listaPersist select x.TABINDEX).Max();
                listaDiaHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum);
                listaDiaSemHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, elementTemp.DIASEM, ConstantesGenerales.CASO_DIASEM);
                listaDiaMesHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, elementTemp.DIAMES, ConstantesGenerales.CASO_DIAMES);
                listaDiaAnioHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, elementTemp.DIAANIO, ConstantesGenerales.CASO_DIAANIO);
                listaDiaGlHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin);
                listaDiaGlSemHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, elementTemp.DIASEM, ConstantesGenerales.CASO_DIASEM);
                listaDiaGlMesHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, elementTemp.DIAMES, ConstantesGenerales.CASO_DIAMES);
                listaDiaGlAnioHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, elementTemp.DIAANIO, ConstantesGenerales.CASO_DIAANIO);

                foreach (var obj in listaPersist)
                {
                    obj.TABINDEXSEQ = ConsultasClassFO.ConsultarNextTabindexSeq(contexto, obj.TABINDEX);
                    obj.TABINDEXLETTERSEQ = ConsultasClassFO.ConsultarNextTabindexLetterSeq(contexto, obj.GROUPLETTER, obj.TABINDEXLETTER);
                    obj.SPANTIDIAHIST = ValidarSpanTiempo(obj.TABINDEX, obj.DIFERENCIAG, (from x in listaDiaHist where x.Tabindex.Equals(obj.TABINDEX) select x).FirstOrDefault());
                    obj.SPANTISEMHIST = ValidarSpanTiempo(obj.TABINDEX, obj.DIFERENCIAG, (from x in listaDiaSemHist where x.Tabindex.Equals(obj.TABINDEX) select x).FirstOrDefault());
                    obj.SPANTIMESHIST = ValidarSpanTiempo(obj.TABINDEX, obj.DIFERENCIAG, (from x in listaDiaMesHist where x.Tabindex.Equals(obj.TABINDEX) select x).FirstOrDefault());
                    obj.SPANTIANIHIST = ValidarSpanTiempo(obj.TABINDEX, obj.DIFERENCIAG, (from x in listaDiaAnioHist where x.Tabindex.Equals(obj.TABINDEX) select x).FirstOrDefault());
                    obj.SPANTIGLDIAHIST = ValidarSpanTiempo(obj.TABINDEXLETTER, obj.DIFERENCIAG, (from x in listaDiaGlHist where x.TabindexLetter.Equals(obj.TABINDEXLETTER) && x.GroupLetter.Equals(obj.GROUPLETTER) select x).FirstOrDefault());
                    obj.SPANTIGLSEMHIST = ValidarSpanTiempo(obj.TABINDEXLETTER, obj.DIFERENCIAG, (from x in listaDiaGlSemHist where x.TabindexLetter.Equals(obj.TABINDEXLETTER) && x.GroupLetter.Equals(obj.GROUPLETTER) select x).FirstOrDefault());
                    obj.SPANTIGLMESHIST = ValidarSpanTiempo(obj.TABINDEXLETTER, obj.DIFERENCIAG, (from x in listaDiaGlMesHist where x.TabindexLetter.Equals(obj.TABINDEXLETTER) && x.GroupLetter.Equals(obj.GROUPLETTER) select x).FirstOrDefault());
                    obj.SPANTIGLANIHIST = ValidarSpanTiempo(obj.TABINDEXLETTER, obj.DIFERENCIAG, (from x in listaDiaGlAnioHist where x.TabindexLetter.Equals(obj.TABINDEXLETTER) && x.GroupLetter.Equals(obj.GROUPLETTER) select x).FirstOrDefault());
                }
                contexto.FLASHORDERED.AddRange(listaPersist);
                listTabindex = (from x in listaPersist select x.TABINDEX).ToList();
                ValidarSpanDatosAnterior(contexto, listaPersist, listTabindex);
                ValidarSpanDatosAnterior(contexto, listaPersist);
                contexto.SaveChanges();
                lista.RemoveAll(x => x.FECHANUM == fechaNum);
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
        private static int ValidarSpanTiempo(int tabindex, int diferenciaG, AgrupadorFechaNumValor a)
        {
            int valorSpan = 0;
            AgrupadorFechaNumValor ultimoSpan = a != null ? a : new AgrupadorFechaNumValor();
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
        /// <param name="listTabindex">Lista con los tabindex ingresados</param>
        public static void ValidarSpanDatosAnterior(SisResultEntities contexto, List<FLASHORDERED> listElementosAgregados, List<int> listTabindex)
        {
            int maxTabindex = ConsultasClassFO.ConsultarMaxTabIndexResultados(contexto);
            maxTabindex = listTabindex.Max() > maxTabindex ? maxTabindex : listTabindex.Max();
            int maxFechaNumIndex;
            var elementTemp = listElementosAgregados.ElementAt(0);
            List<FLASHORDERED> listaDatosUltimosSpan;
            for (int r = 0; r <= 3; r++)
            {
                switch (r)
                {
                    case 1:
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex, ConstantesGenerales.CASO_DIASEM, elementTemp.DIASEM);
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANTISEMACT != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 && b.DIASEM == elementTemp.DIASEM
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;

                    case 2:
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex, ConstantesGenerales.CASO_DIAMES, elementTemp.DIAMES);
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANTIMESACT != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 && b.DIAMES == elementTemp.DIAMES
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;

                    case 3:
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex, ConstantesGenerales.CASO_DIAANIO, elementTemp.DIAANIO);
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANTIANIACT != null
                                                 && b.FECHANUM >= maxFechaNumIndex
                                                 && b.DIAANIO == elementTemp.DIAANIO
                                                 select b).OrderByDescending(b => b.FECHANUM).ThenBy(x => x.TABINDEX).AsEnumerable().ToList()
                             .GroupBy(p => p.TABINDEX).Select(g => g.First()).ToList();
                        break;

                    default:
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex);
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listTabindex.Contains(b.TABINDEX)
                                                 && b.SPANTIDIAACT != null
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
                    else
                    {
                        switch (r)
                        {
                            case 1:
                                uActual.SPANTISEMACT = uActual.SPANTISEMHIST;
                                break;

                            case 2:
                                uActual.SPANTIMESACT = uActual.SPANTIMESHIST;
                                break;

                            case 3:
                                uActual.SPANTIANIACT = uActual.SPANTIANIHIST;
                                break;

                            default:
                                uActual.SPANTIDIAACT = uActual.SPANTIDIAHIST;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que realiza la validación de los span del día anterior, y actualizar los datos si es requerido
        /// y luego asignar los del día acutal
        /// </summary>
        /// <param name="contexto">Instancia del contexto para la consulta de datos</param>
        /// <param name="listElementosAgregados">Lista con los elementos adicionados, para realizar el análisis</param>
        /// <param name="listTabindex">Lista con los tabindex ingresados</param>
        public static void ValidarSpanDatosAnterior(SisResultEntities contexto, List<FLASHORDERED> listElementosAgregados)
        {
            string strJoin = UtilHtml.ObtenerJoinElementos(listElementosAgregados);
            var elementTemp = listElementosAgregados.ElementAt(0);
            List<FLASHORDERED> listaDatosUltimosSpan = new List<FLASHORDERED>();
            List<AgrupadorMaxFechasTGDTO> listaFechas = new List<AgrupadorMaxFechasTGDTO>();
            List<decimal> listaIds = new List<decimal>();
            for (int r = 4; r <= 7; r++)
            {
                switch (r)
                {
                    case 4:
                        listaFechas = ConsultasClassFO.ConsultarMaxFechasTabGroup(contexto, strJoin, ConstantesGenerales.CASO_DIASEM, elementTemp.DIASEM);
                        listaIds = (from x in listaFechas
                                    select x.Id).ToList();
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listaIds.Contains(b.ID)
                                                 select b).ToList();
                        break;

                    case 5:
                        listaFechas = ConsultasClassFO.ConsultarMaxFechasTabGroup(contexto, strJoin, ConstantesGenerales.CASO_DIAMES, elementTemp.DIAMES);
                        listaIds = (from x in listaFechas
                                    select x.Id).ToList();
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listaIds.Contains(b.ID)
                                                 select b).ToList();
                        break;

                    case 6:
                        listaFechas = ConsultasClassFO.ConsultarMaxFechasTabGroup(contexto, strJoin, ConstantesGenerales.CASO_DIAANIO, elementTemp.DIAANIO);
                        listaIds = (from x in listaFechas
                                    select x.Id).ToList();
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listaIds.Contains(b.ID)
                                                 select b).ToList();
                        break;

                    default:
                        listaFechas = ConsultasClassFO.ConsultarMaxFechasTabGroup(contexto, strJoin);
                        listaIds = (from x in listaFechas
                                    select x.Id).ToList();
                        listaDatosUltimosSpan = (from b in contexto.FLASHORDERED
                                                 where listaIds.Contains(b.ID)
                                                 select b).ToList();
                        break;
                }
                for (int i = 0; i < listElementosAgregados.Count; i++)
                {
                    var uActual = listElementosAgregados.ElementAt(i);
                    var uAnt = listaDatosUltimosSpan
                        .Where(x => x.GROUPLETTER.Equals(uActual.GROUPLETTER)
                        && x.TABINDEXLETTER.Equals(uActual.TABINDEXLETTER)).FirstOrDefault();
                    if (uAnt != null)
                    {
                        ValidarSpanColumna(uAnt, uActual, r);
                    }
                    else
                    {
                        switch (r)
                        {
                            case 4:
                                uActual.SPANTIGLSEMACT = uActual.SPANTIGLSEMHIST;
                                break;

                            case 5:
                                uActual.SPANTIGLMESACT = uActual.SPANTIGLMESHIST;
                                break;

                            case 6:
                                uActual.SPANTIGLANIACT = uActual.SPANTIGLANIHIST;
                                break;

                            default:
                                uActual.SPANTIGLDIAACT = uActual.SPANTIGLDIAHIST;
                                break;
                        }
                    }
                }
            }
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
                    if (uAnt.SPANTIDIAACT == null)
                    {
                        uAnt.SPANTIDIAACT = uAnt.SPANTIDIAHIST;
                    }
                    if (uAnt.SPANTIDIAACT < 0)
                    {
                        uActual.SPANTIDIAACT = --uAnt.SPANTIDIAACT;
                        uAnt.SPANTIDIAACT = null;
                    }
                    else
                    {
                        uActual.SPANTIDIAACT = -1;
                    }
                }
                else if (casoColumna == 1)
                {
                    if (uAnt.SPANTISEMACT == null)
                    {
                        uAnt.SPANTISEMACT = uAnt.SPANTISEMHIST;
                    }
                    if (uAnt.SPANTISEMACT < 0)
                    {
                        uActual.SPANTISEMACT = --uAnt.SPANTISEMACT;
                        uAnt.SPANTISEMACT = null;
                    }
                    else
                    {
                        uActual.SPANTISEMACT = -1;
                    }
                }
                else if (casoColumna == 2)
                {
                    if (uAnt.SPANTIMESACT == null)
                    {
                        uAnt.SPANTIMESACT = uAnt.SPANTIMESHIST;
                    }
                    if (uAnt.SPANTIMESACT < 0)
                    {
                        uActual.SPANTIMESACT = --uAnt.SPANTIMESACT;
                        uAnt.SPANTIMESACT = null;
                    }
                    else
                    {
                        uActual.SPANTIMESACT = -1;
                    }
                }
                else if (casoColumna == 3)
                {
                    if (uAnt.SPANTIANIACT == null)
                    {
                        uAnt.SPANTIANIACT = uAnt.SPANTIANIHIST;
                    }
                    if (uAnt.SPANTIANIACT < 0)
                    {
                        uActual.SPANTIANIACT = --uAnt.SPANTIANIACT;
                        uAnt.SPANTIANIACT = null;
                    }
                    else
                    {
                        uActual.SPANTIANIACT = -1;
                    }
                }
                else if (casoColumna == 4)
                {
                    if (uAnt.SPANTIGLSEMACT == null)
                    {
                        uAnt.SPANTIGLSEMACT = uAnt.SPANTIGLSEMHIST;
                    }
                    if (uAnt.SPANTIGLSEMACT < 0)
                    {
                        uActual.SPANTIGLSEMACT = --uAnt.SPANTIGLSEMACT;
                        uAnt.SPANTIGLSEMACT = null;
                    }
                    else
                    {
                        uActual.SPANTIGLSEMACT = -1;
                    }
                }
                else if (casoColumna == 5)
                {
                    if (uAnt.SPANTIGLMESACT == null)
                    {
                        uAnt.SPANTIGLMESACT = uAnt.SPANTIGLMESHIST;
                    }
                    if (uAnt.SPANTIGLMESACT < 0)
                    {
                        uActual.SPANTIGLMESACT = --uAnt.SPANTIGLMESACT;
                        uAnt.SPANTIGLMESACT = null;
                    }
                    else
                    {
                        uActual.SPANTIGLMESACT = -1;
                    }
                }
                else if (casoColumna == 6)
                {
                    if (uAnt.SPANTIGLANIACT == null)
                    {
                        uAnt.SPANTIGLANIACT = uAnt.SPANTIGLANIHIST;
                    }
                    if (uAnt.SPANTIGLANIACT < 0)
                    {
                        uActual.SPANTIGLANIACT = --uAnt.SPANTIGLANIACT;
                        uAnt.SPANTIGLANIACT = null;
                    }
                    else
                    {
                        uActual.SPANTIGLANIACT = -1;
                    }
                }
                else if (casoColumna == 7)
                {
                    if (uAnt.SPANTIGLDIAACT == null)
                    {
                        uAnt.SPANTIGLDIAACT = uAnt.SPANTIGLDIAHIST;
                    }
                    if (uAnt.SPANTIGLDIAACT < 0)
                    {
                        uActual.SPANTIGLDIAACT = --uAnt.SPANTIGLDIAACT;
                        uAnt.SPANTIGLDIAACT = null;
                    }
                    else
                    {
                        uActual.SPANTIGLDIAACT = -1;
                    }
                }
            }
            else
            {
                if (casoColumna == 0)
                {
                    if (uAnt.SPANTIDIAACT == null)
                    {
                        uAnt.SPANTIDIAACT = uAnt.SPANTIDIAHIST;
                    }
                    if (uAnt.SPANTIDIAACT > 0)
                    {
                        uActual.SPANTIDIAACT = ++uAnt.SPANTIDIAACT;
                        uAnt.SPANTIDIAACT = null;
                    }
                    else
                    {
                        uActual.SPANTIDIAACT = 1;
                    }
                }
                else if (casoColumna == 1)
                {
                    if (uAnt.SPANTISEMACT == null)
                    {
                        uAnt.SPANTISEMACT = uAnt.SPANTISEMHIST;
                    }
                    if (uAnt.SPANTISEMACT > 0)
                    {
                        uActual.SPANTISEMACT = ++uAnt.SPANTISEMACT;
                        uAnt.SPANTISEMACT = null;
                    }
                    else
                    {
                        uActual.SPANTISEMACT = 1;
                    }
                }
                else if (casoColumna == 2)
                {
                    if (uAnt.SPANTIMESACT == null)
                    {
                        uAnt.SPANTIMESACT = uAnt.SPANTIMESHIST;
                    }
                    if (uAnt.SPANTIMESACT > 0)
                    {
                        uActual.SPANTIMESACT = ++uAnt.SPANTIMESACT;
                        uAnt.SPANTIMESACT = null;
                    }
                    else
                    {
                        uActual.SPANTIMESACT = 1;
                    }
                }
                else if (casoColumna == 3)
                {
                    if (uAnt.SPANTIANIACT == null)
                    {
                        uAnt.SPANTIANIACT = uAnt.SPANTIANIHIST;
                    }
                    if (uAnt.SPANTIANIACT > 0)
                    {
                        uActual.SPANTIANIACT = ++uAnt.SPANTIANIACT;
                        uAnt.SPANTIANIACT = null;
                    }
                    else
                    {
                        uActual.SPANTIANIACT = 1;
                    }
                }
                else if (casoColumna == 4)
                {
                    if (uAnt.SPANTIGLSEMACT == null)
                    {
                        uAnt.SPANTIGLSEMACT = uAnt.SPANTIGLSEMHIST;
                    }
                    if (uAnt.SPANTIGLSEMACT > 0)
                    {
                        uActual.SPANTIGLSEMACT = ++uAnt.SPANTIGLSEMACT;
                        uAnt.SPANTIGLSEMACT = null;
                    }
                    else
                    {
                        uActual.SPANTIGLSEMACT = 1;
                    }
                }
                else if (casoColumna == 5)
                {
                    if (uAnt.SPANTIGLMESACT == null)
                    {
                        uAnt.SPANTIGLMESACT = uAnt.SPANTIGLMESHIST;
                    }
                    if (uAnt.SPANTIGLMESACT > 0)
                    {
                        uActual.SPANTIGLMESACT = ++uAnt.SPANTIGLMESACT;
                        uAnt.SPANTIGLMESACT = null;
                    }
                    else
                    {
                        uActual.SPANTIGLMESACT = 1;
                    }
                }
                else if (casoColumna == 6)
                {
                    if (uAnt.SPANTIGLANIACT == null)
                    {
                        uAnt.SPANTIGLANIACT = uAnt.SPANTIGLANIHIST;
                    }
                    if (uAnt.SPANTIGLANIACT > 0)
                    {
                        uActual.SPANTIGLANIACT = ++uAnt.SPANTIGLANIACT;
                        uAnt.SPANTIGLANIACT = null;
                    }
                    else
                    {
                        uActual.SPANTIGLANIACT = 1;
                    }
                }
                else if (casoColumna == 7)
                {
                    if (uAnt.SPANTIGLDIAACT == null)
                    {
                        uAnt.SPANTIGLDIAACT = uAnt.SPANTIGLDIAHIST;
                    }
                    if (uAnt.SPANTIGLDIAACT > 0)
                    {
                        uActual.SPANTIGLDIAACT = ++uAnt.SPANTIGLDIAACT;
                        uAnt.SPANTIGLDIAACT = null;
                    }
                    else
                    {
                        uActual.SPANTIGLDIAACT = 1;
                    }
                }
            }
        }

        public static List<AgrupadorInfoGeneralDTO> ValidarElementosDia(DateTime fecha, int caso,
            SisResultEntities contexto, List<FLASHORDERED> listaHtmlTemp, int percent)
        {
            List<string> lstaStrs = new List<string>();

            lstaStrs.Add("4-12");
            lstaStrs.Add("9-12");
            lstaStrs.Add("17-8");
            lstaStrs.Add("10-6");
            lstaStrs.Add("14-4");
            lstaStrs.Add("11-7");
            lstaStrs.Add("13-0");
            lstaStrs.Add("6-8");
            lstaStrs.Add("8-12");
            lstaStrs.Add("15-6");
            lstaStrs.Add("14-6");
            lstaStrs.Add("7-10");
            lstaStrs.Add("7-11");
            lstaStrs.Add("14-8");
            lstaStrs.Add("13-4");
            lstaStrs.Add("14-10");
            lstaStrs.Add("0-10");
            lstaStrs.Add("0-8");
            lstaStrs.Add("6-13");
            lstaStrs.Add("6-12");
            lstaStrs.Add("5-13");
            lstaStrs.Add("6-15");
            lstaStrs.Add("3-16");
            lstaStrs.Add("10-8");
            lstaStrs.Add("4-15");
            lstaStrs.Add("17-5");
            lstaStrs.Add("14-11");
            lstaStrs.Add("0-12");
            lstaStrs.Add("10-11");
            lstaStrs.Add("17-4");
            lstaStrs.Add("3-15");
            lstaStrs.Add("6-11");
            lstaStrs.Add("13-12");
            lstaStrs.Add("9-13");
            lstaStrs.Add("8-13");
            lstaStrs.Add("9-11");
            lstaStrs.Add("15-4");
            lstaStrs.Add("0-13");
            lstaStrs.Add("11-12");
            lstaStrs.Add("15-2");
            lstaStrs.Add("13-10");
            lstaStrs.Add("0-11");
            lstaStrs.Add("8-15");
            lstaStrs.Add("1-10");
            lstaStrs.Add("10-3");
            lstaStrs.Add("0-6");

            List<AgrupadorInfoGeneralDTO> listaInfo = GetListaInfoAsignn(fecha, caso, contexto, listaHtmlTemp);

            //listaInfo = FilterElementosTabindex(contexto, lstTabIndex, fechaFormat, listaInfo, strJoin);
            //DateTime fechaMin = fecha.AddDays(percent);
            //listaInfo = listaInfo.OrderBy(x => x.AgrupadorPromMaxTabindexGen.Rank).ToList();
            //List<AgrupadorConteosTimeSpanDTO> listPercentsTab = ConsultasClassFO.ConsultarPercent(contexto, fechaFormat, dayofweek, fechaMin.ToString("yyyyMMdd"), ConstantesGenerales.ORDEN_TABINDEX_GEN, 0);
            //listaInfo = GetListOrdered(fecha, contexto, listaInfo, listPercentsTab, listaHtmlTemp.Count);

            /*listaInfo = FilterElementosTabindexGl(contexto, lstTabIndex, fechaFormat, listaInfo, strJoin, percent);
            DateTime fechaMin = fecha.AddDays(percent);
            listaInfo = listaInfo.OrderBy(x => x.AgrupadorPromMaxGroupTabGen.Rank).ToList();
            List<AgrupadorConteosTimeSpanDTO> listPercentsTab = ConsultasClassFO.ConsultarPercent(contexto, fechaFormat, dayofweek, fechaMin.ToString("yyyyMMdd"), ConstantesGenerales.ORDEN_GROUP_LETTER_GEN, 0);
            listaInfo = GetListOrdered(fecha, contexto, listaInfo, listPercentsTab, listaHtmlTemp.Count);*/

            List<AgrupadorInfoGeneralDTO> listaFiltered = new List<AgrupadorInfoGeneralDTO>();
            string strVar;
            foreach (var item in listaInfo)
            {
                strVar = item.RankSpanActualGlGen + "-" + item.RankSpanActualGen;
                if (lstaStrs.IndexOf(strVar) != -1)
                {
                    listaFiltered.Add(item);
                }
            }
            return listaFiltered;
        }

        public static List<AgrupadorInfoGeneralDTO> GetListaInfoAsignn(DateTime fecha, int caso,
            SisResultEntities contexto, List<FLASHORDERED> listaHtmlTemp)
        {
            List<AgrupadorInfoGeneralDTO> listaInfo = new List<AgrupadorInfoGeneralDTO>();
            string strJoin = UtilHtml.ObtenerJoinElementos(listaHtmlTemp);
            int maxTabindex = (from x in listaHtmlTemp select x.TABINDEX).Max();
            string fechaFormat = fecha.ToString("yyyyMMdd");
            int dayofweek = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;

            //Obtener los promedios
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexGen = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaSem = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaMes = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaAnio = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabGen = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaSem = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaMes = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaAnio = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);

            //Obtener las fechas máximas de los tabindex
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGen = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaSem = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaMes = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaAnio = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLGen = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaSem = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaMes = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaAnio = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);

            string strJoinTabindex = "(" + ConstantesModel.FECHANUM + " < {0} AND " + ConstantesModel.TABINDEX + " = {1})";
            string strJoinTabindexGl = "(" + ConstantesModel.FECHANUM + " < {0} AND " + ConstantesModel.GROUPLETTER + " = '{1}' AND " + ConstantesModel.TABINDEXLETTER + " = {2})";
            List<string> listStrJoinTabindex = new List<string>();
            List<string> listStrJoinTabindexDiaSem = new List<string>();
            List<string> listStrJoinTabindexDiaMes = new List<string>();
            List<string> listStrJoinGlTabindex = new List<string>();
            List<string> listStrJoinGlTabindexDiaSem = new List<string>();
            List<string> listStrJoinGlTabindexDiaMes = new List<string>();

            foreach (var item in listaHtmlTemp)
            {
                AgrupadorInfoGeneralDTO aigDTO = new AgrupadorInfoGeneralDTO();
                aigDTO.Tabindex = item.TABINDEX;
                aigDTO.Tabindexletter = item.TABINDEXLETTER;
                aigDTO.GroupLetter = item.GROUPLETTER;
                aigDTO.DiferenciaGTemp = item.DIFERENCIAG;
                aigDTO.Home = item.Home;
                aigDTO.Away = item.Away;

                aigDTO.AgrupadorPromMaxTabindexGen = listaPromMaxTabindexGen.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxTabindexDiaSem = listaPromMaxTabindexDiaSem.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxTabindexDiaMes = listaPromMaxTabindexDiaMes.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxTabindexDiaAnio = listaPromMaxTabindexDiaAnio.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxGroupTabGen = listaPromMaxGroupTabGen.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxGroupTabDiaSem = listaPromMaxGroupTabDiaSem.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxGroupTabDiaMes = listaPromMaxGroupTabDiaMes.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxGroupTabDiaAnio = listaPromMaxGroupTabDiaAnio.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGen = (from x in listaFechasMaxTabindexGen where x.Tabindex.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanDiaSem = (from x in listaFechasMaxTabindexDiaSem where x.Tabindex.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanDiaMes = (from x in listaFechasMaxTabindexDiaMes where x.Tabindex.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                //aigDTO.AgrUltFechaNumSpanDiaAnio = (from x in listaFechasMaxTabindexDiaAnio where x.TabindexLetter.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGlGen = (from x in listaFechasMaxTabindexGLGen where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGlDiaSem = (from x in listaFechasMaxTabindexGLDiaSem where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGlDiaMes = (from x in listaFechasMaxTabindexGLDiaMes where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();
                //aigDTO.AgrUltFechaNumSpanGlDiaAnio = (from x in listaFechasMaxTabindexGLDiaAnio where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();

                if (aigDTO.AgrUltFechaNumSpanGlGen != null)
                {
                    listStrJoinGlTabindex.Add(string.Format(strJoinTabindexGl, aigDTO.AgrUltFechaNumSpanGlGen.FechaNum, aigDTO.GroupLetter, aigDTO.Tabindexletter));
                }
                if (aigDTO.AgrUltFechaNumSpanGlDiaSem != null)
                {
                    listStrJoinGlTabindexDiaSem.Add(string.Format(strJoinTabindexGl, aigDTO.AgrUltFechaNumSpanGlDiaSem.FechaNum, aigDTO.GroupLetter, aigDTO.Tabindexletter));
                }
                if (aigDTO.AgrUltFechaNumSpanGlDiaMes != null)
                {
                    listStrJoinGlTabindexDiaMes.Add(string.Format(strJoinTabindexGl, aigDTO.AgrUltFechaNumSpanGlDiaMes.FechaNum, aigDTO.GroupLetter, aigDTO.Tabindexletter));
                }

                if (aigDTO.AgrUltFechaNumSpanGen != null)
                {
                    listStrJoinTabindex.Add(string.Format(strJoinTabindex, aigDTO.AgrUltFechaNumSpanGen.FechaNum, aigDTO.Tabindex));
                }
                if (aigDTO.AgrUltFechaNumSpanDiaSem != null)
                {
                    listStrJoinTabindexDiaSem.Add(string.Format(strJoinTabindex, aigDTO.AgrUltFechaNumSpanDiaSem.FechaNum, aigDTO.Tabindex));
                }
                if (aigDTO.AgrUltFechaNumSpanDiaMes != null)
                {
                    listStrJoinTabindexDiaMes.Add(string.Format(strJoinTabindex, aigDTO.AgrUltFechaNumSpanDiaMes.FechaNum, aigDTO.Tabindex));
                }

                listaInfo.Add(aigDTO);
            }

            string strTabindexJoinTabIndexMaxFechas = string.Join(" OR ", listStrJoinTabindex);
            string strTabindexJoinTabIndexMaxFechasDiaSem = string.Join(" OR ", listStrJoinTabindexDiaSem);
            string strTabindexJoinTabIndexMaxFechasDiaMes = string.Join(" OR ", listStrJoinTabindexDiaMes);
            string strTabindexJoinTabIndexGlMaxFechas = string.Join(" OR ", listStrJoinGlTabindex);
            string strTabindexJoinTabIndexGlMaxFechasDiaSem = string.Join(" OR ", listStrJoinGlTabindexDiaSem);
            string strTabindexJoinTabIndexGlMaxFechasDiaMes = string.Join(" OR ", listStrJoinGlTabindexDiaMes);

            //Consulta de los rank
            List<AgrupadorConteosTimeSpanDTO> listaRanksGen = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechas);
            List<AgrupadorConteosTimeSpanDTO> listaRanksDiaSem = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechasDiaSem, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorConteosTimeSpanDTO> listaRanksDiaMes = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechasDiaMes, ConstantesGenerales.CASO_DIAMES, dayofweek);
            List<AgrupadorConteosTimeSpanDTO> listaRanksGlGen = ConsultasClassFO.ConsultarRanksGlTabindex(contexto, strTabindexJoinTabIndexGlMaxFechas);
            List<AgrupadorConteosTimeSpanDTO> listaRanksGlDiaSem = ConsultasClassFO.ConsultarRanksGlTabindex(contexto, strTabindexJoinTabIndexGlMaxFechasDiaSem, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorConteosTimeSpanDTO> listaRanksGlDiaMes = ConsultasClassFO.ConsultarRanksGlTabindex(contexto, strTabindexJoinTabIndexGlMaxFechasDiaMes, ConstantesGenerales.CASO_DIAMES, fecha.Day);

            foreach (var item in listaInfo)
            {
                if (item.AgrUltFechaNumSpanGlGen != null)
                {
                    item.ListRankSpansGlGen = (from x in listaRanksGlGen where x.TabIndexLetter.Equals(item.Tabindexletter) && x.GroupLetter.Equals(item.GroupLetter) select x).ToList();
                    item.RankSpanActualGlGen = AsignarInfoRank(item.ListRankSpansGlGen, item.AgrUltFechaNumSpanGlGen.Spantiempo);
                    if (item.RankSpanActualGlGen != 0)
                    {
                        item.MaxRankSpanActualGlGen = AsignarInfoMaxRank(item.ListRankSpansGlGen);
                        item.MinSpanGlGen = AsignarInfoMinSpan(item.ListRankSpansGlGen);
                    }
                }
                else
                {
                    item.AgrUltFechaNumSpanGlGen = new AgrupadorFechaNumValor();
                }

                if (item.AgrUltFechaNumSpanGlDiaSem != null)
                {
                    item.ListRankSpansGlDiaSem = (from x in listaRanksGlDiaSem where x.TabIndexLetter.Equals(item.Tabindexletter) && x.GroupLetter.Equals(item.GroupLetter) select x).ToList();
                    item.RankSpanActualGlDiaSem = AsignarInfoRank(item.ListRankSpansGlDiaSem, item.AgrUltFechaNumSpanGlDiaSem.Spantiempo);
                    if (item.RankSpanActualGlDiaSem != 0)
                    {
                        item.MaxRankSpanActualGlDiaSem = AsignarInfoMaxRank(item.ListRankSpansGlDiaSem);
                        item.MinSpanGlDiaSem = AsignarInfoMinSpan(item.ListRankSpansGlDiaSem);
                    }
                }
                else
                {
                    item.AgrUltFechaNumSpanGlDiaSem = new AgrupadorFechaNumValor();
                }

                if (item.AgrUltFechaNumSpanGlDiaMes != null)
                {
                    item.ListRankSpansGlDiaMes = (from x in listaRanksGlDiaMes where x.TabIndexLetter.Equals(item.Tabindexletter) && x.GroupLetter.Equals(item.GroupLetter) select x).ToList();
                    item.RankSpanActualGlDiaMes = AsignarInfoRank(item.ListRankSpansGlDiaMes, item.AgrUltFechaNumSpanGlDiaMes.Spantiempo);
                    if (item.RankSpanActualGlDiaMes != 0)
                    {
                        item.MaxRankSpanActualGlDiaMes = AsignarInfoMaxRank(item.ListRankSpansGlDiaMes);
                        item.MinSpanGlDiaMes = AsignarInfoMinSpan(item.ListRankSpansGlDiaMes);
                    }
                }
                else
                {
                    item.AgrUltFechaNumSpanGlDiaMes = new AgrupadorFechaNumValor();
                }

                if (item.AgrUltFechaNumSpanGen != null)
                {
                    item.ListRankSpansGen = (from x in listaRanksGen where x.TabIndex.Equals(item.Tabindex) select x).ToList();
                    item.RankSpanActualGen = AsignarInfoRank(item.ListRankSpansGen, item.AgrUltFechaNumSpanGen.Spantiempo);
                    if (item.RankSpanActualGen != 0)
                    {
                        item.MaxRankSpanActualGen = AsignarInfoMaxRank(item.ListRankSpansGen);
                        item.MinSpanTiGen = AsignarInfoMinSpan(item.ListRankSpansGen);
                    }
                }
                else
                {
                    item.AgrUltFechaNumSpanGen = new AgrupadorFechaNumValor();
                }

                if (item.AgrUltFechaNumSpanDiaSem != null)
                {
                    item.ListRankSpansDiaSem = (from x in listaRanksDiaSem where x.TabIndex.Equals(item.Tabindex) select x).ToList();
                    item.RankSpanActualDiaSem = AsignarInfoRank(item.ListRankSpansDiaSem, item.AgrUltFechaNumSpanDiaSem.Spantiempo);
                    if (item.RankSpanActualDiaSem != 0)
                    {
                        item.MaxRankSpanActualGlDiaSem = AsignarInfoMaxRank(item.ListRankSpansDiaSem);
                        item.MinSpanGlDiaSem = AsignarInfoMinSpan(item.ListRankSpansDiaSem);
                    }
                }
                else
                {
                    item.AgrUltFechaNumSpanDiaSem = new AgrupadorFechaNumValor();
                }

                if (item.AgrUltFechaNumSpanDiaMes != null)
                {
                    item.ListRankSpansDiaMes = (from x in listaRanksDiaMes where x.TabIndex.Equals(item.Tabindex) select x).ToList();
                    item.RankSpanActualDiaMes = AsignarInfoRank(item.ListRankSpansDiaMes, item.AgrUltFechaNumSpanDiaMes.Spantiempo);
                    if (item.RankSpanActualDiaMes != 0)
                    {
                        item.MaxRankSpanActualGlDiaMes = AsignarInfoMaxRank(item.ListRankSpansDiaMes);
                        item.MinSpanGlDiaMes = AsignarInfoMinSpan(item.ListRankSpansDiaMes);
                    }
                }
                else
                {
                    item.AgrUltFechaNumSpanDiaMes = new AgrupadorFechaNumValor();
                }
            }
            return listaInfo;
        }

        private static List<AgrupadorInfoGeneralDTO> FilterElementosTabindex(SisResultEntities contexto,
            int maxTabindex, string fechaFormat, List<AgrupadorInfoGeneralDTO> listaInfo, string strJoin, int percent = 100)
        {
            List<AgrupadorInfoGeneralDTO> listaFiltered = new List<AgrupadorInfoGeneralDTO>();
            List<int> listTabindex = (from x in listaInfo select x.Tabindex).ToList();
            //int rank = 0;
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexGen = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto);
            listaPromMaxTabindexGen = (from x in listaPromMaxTabindexGen where listTabindex.IndexOf(x.Tabindex) != -1 select x).OrderBy(x => x.Rank).ToList();
            //int totalElementos = listaInfo.Count;
            //int percentRemove = (percent * totalElementos) / 100;
            foreach (var itemProm in listaPromMaxTabindexGen)
            {
                var item = (from x in listaInfo where x.Tabindex.Equals(itemProm.Tabindex) select x).FirstOrDefault();
                item.AgrupadorPromMaxTabindexGen = itemProm;
                //rank++;
                //if (rank > percentRemove)
                //{
                //    continue;
                //}
                listaFiltered.Add(item);
            }
            return listaFiltered;
        }

        private static List<AgrupadorInfoGeneralDTO> FilterElementosTabindexGl(SisResultEntities contexto,
            int maxTabindex, string fechaFormat, List<AgrupadorInfoGeneralDTO> listaInfo, string strJoin, int percent = 99)
        {
            List<AgrupadorInfoGeneralDTO> listaFiltered = new List<AgrupadorInfoGeneralDTO>();
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabGen = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTab = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto);
            //int totalElementos = listaInfo.Count;
            //int rank = 0;
            //int percentRemove = (percent * totalElementos) / 100;
            //foreach (var item in listaPromMaxGroupTab)
            //{
            //    var data = (from x in listaInfo where x.Tabindexletter.Equals(item.Tabindex) && x.GroupLetter.Equals(item.GroupLetter) select x).FirstOrDefault();
            //    if (data == null) continue;
            //    listaPromMaxGroupTabGen.Add(item);
            //}
            foreach (var itemProm in listaPromMaxGroupTabGen)
            {
                var item = (from x in listaInfo
                            where x.Tabindexletter.Equals(itemProm.Tabindex)
        && x.GroupLetter.Equals(itemProm.GroupLetter)
                            select x).FirstOrDefault();
                item.AgrupadorPromMaxGroupTabGen = itemProm;
                //if (++rank > percentRemove)
                //{
                //    continue;
                //}
                listaFiltered.Add(item);
            }
            return listaFiltered;
        }

        public static List<FLASHORDERED> GetListaTemp(DateTime fecha, int casoResource, SisResultEntities contexto, int valorMinimo)
        {
            List<FLASHORDERED> listaHtmlFinal = new List<FLASHORDERED>();
            List<FLASHORDERED> listaHtmlTemp = UtilGeneral.UtilHtml.LeerInfoHtmlTempActual(fecha, casoResource);
            string fechaFormat = fecha.ToString("yyyyMMdd");
            string strJoin = UtilHtml.ObtenerJoinElementos(listaHtmlTemp);
            List<AgrupadorMaxTabIndex> listaMaxTabindexGL = ConsultasClassFO.ConsultarMaxSeqTabindexGl(fechaFormat, contexto, strJoin);
            foreach (var item in listaHtmlTemp)
            {
                int totalGl = (from x in listaMaxTabindexGL
                               where x.Tabindex.Equals(item.TABINDEXLETTER)
                                && x.GroupLetter.Equals(item.GROUPLETTER)
                               select x.Total).FirstOrDefault();
                if (totalGl < valorMinimo) continue;
                listaHtmlFinal.Add(item);
            }
            return listaHtmlFinal;
        }

        public static void InsertarElementosPercent(List<FLASHORDERED> listaFin, SisResultEntities contexto, int minRegistros)
        {
            List<FLASHORDERED> listaHtmlTemp;
            List<AgrupadorInfoGeneralDTO> lstAigDto;
            List<AgrupadorInfoGeneralDTO> listaInfoTemp;
            string strJoin;
            int maxTabindex;
            string fechaFormat;
            int dayofweek;
            int diaMes;
            int mesNum;
            int diaAnio;
            int fechaNum;
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexGen;
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabGen;
            AgrupadorInfoGeneralDTO aigDTO;
            List<ANDATAPERCENTUNG> listaFinal;
            int totalElementos;
            int idInicioPer = ConsultasClassFO.ConsultarMaxIdActual(contexto, ConstantesGenerales.TBL_PERCENT);
            List<DateTime> listFechas = (from x in listaFin select x.FECHA).Distinct().ToList();
            foreach (var itemFecha in listFechas)
            {
                listaHtmlTemp = GetListaTemp(itemFecha, 1, contexto, minRegistros);
                lstAigDto = new List<AgrupadorInfoGeneralDTO>();
                listaInfoTemp = new List<AgrupadorInfoGeneralDTO>();
                strJoin = UtilHtml.ObtenerJoinElementos(listaHtmlTemp);
                maxTabindex = (from x in listaHtmlTemp select x.TABINDEX).Max();
                fechaFormat = itemFecha.ToString("yyyyMMdd");
                dayofweek = (int)itemFecha.DayOfWeek == 0 ? 7 : (int)itemFecha.DayOfWeek;
                diaMes = itemFecha.Day;
                mesNum = itemFecha.Month;
                diaAnio = itemFecha.DayOfYear;
                fechaNum = Convert.ToInt32(itemFecha.ToString("yyyyMMdd"));

                //Obtener los promedios
                listaPromMaxTabindexGen = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto);
                listaPromMaxGroupTabGen = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto);

                foreach (var item in listaHtmlTemp)
                {
                    aigDTO = new AgrupadorInfoGeneralDTO();
                    aigDTO.Tabindex = item.TABINDEX;
                    aigDTO.Tabindexletter = item.TABINDEXLETTER;
                    aigDTO.GroupLetter = item.GROUPLETTER;
                    aigDTO.AgrupadorPromMaxTabindexGen = listaPromMaxTabindexGen.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                    if (aigDTO.AgrupadorPromMaxTabindexGen == null)
                    {
                        aigDTO.AgrupadorPromMaxTabindexGen = new AgrupadorTotalTabIndexDTO();
                        aigDTO.AgrupadorPromMaxTabindexGen.Rank = 5000;
                    }
                    aigDTO.AgrupadorPromMaxGroupTabGen = listaPromMaxGroupTabGen.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                    if (aigDTO.AgrupadorPromMaxGroupTabGen == null)
                    {
                        aigDTO.AgrupadorPromMaxGroupTabGen = new AgrupadorTotalTabIndexDTO();
                        aigDTO.AgrupadorPromMaxGroupTabGen.Rank = 5000;
                    }
                    lstAigDto.Add(aigDTO);
                }
                totalElementos = lstAigDto.Count;

                listaInfoTemp = SortListElements(listaFin, lstAigDto, itemFecha, ConstantesGenerales.ORDEN_GROUP_LETTER_GEN);
                listaFinal = new List<ANDATAPERCENTUNG>();
                listaFinal.AddRange(AddElementosLista(listaInfoTemp, dayofweek, diaMes, mesNum, diaAnio, fechaNum,
                    totalElementos, idInicioPer, itemFecha, ConstantesGenerales.ORDEN_GROUP_LETTER_GEN));
                idInicioPer = (int)(from x in listaFinal select x.ID).Max() + 1;
                listaInfoTemp = SortListElements(listaFin, lstAigDto, itemFecha, ConstantesGenerales.ORDEN_TABINDEX_GEN);
                listaFinal.AddRange(AddElementosLista(listaInfoTemp, dayofweek, diaMes, mesNum, diaAnio, fechaNum,
                    totalElementos, idInicioPer, itemFecha, ConstantesGenerales.ORDEN_TABINDEX_GEN));
                idInicioPer = (int)(from x in listaFinal select x.ID).Max() + 1;
                contexto.ANDATAPERCENTUNG.AddRange(listaFinal);
                contexto.SaveChanges();
                listaFin.RemoveAll(x => x.FECHA.Equals(itemFecha));
            }
        }

        private static List<AgrupadorInfoGeneralDTO> SortListElements(List<FLASHORDERED> listaFin, List<AgrupadorInfoGeneralDTO> listToOrder, DateTime itemFecha,
            int caseOrden)
        {
            List<AgrupadorInfoGeneralDTO> listaInfoSorted = new List<AgrupadorInfoGeneralDTO>();
            int pos = 1;
            switch (caseOrden)
            {
                case ConstantesGenerales.ORDEN_GROUP_LETTER_GEN:
                    listToOrder = listToOrder.OrderBy(x => x.AgrupadorPromMaxGroupTabGen.Rank).ToList();
                    break;

                case ConstantesGenerales.ORDEN_TABINDEX_GEN:
                    listToOrder = listToOrder.OrderBy(x => x.AgrupadorPromMaxTabindexGen.Rank).ToList();
                    break;
            }
            foreach (var item in listToOrder)
            {
                var data = (from x in listaFin
                            where x.TABINDEX == item.Tabindex
                            && x.FECHA.Equals(itemFecha)
                            select x).FirstOrDefault();
                item.PosTemp = pos++;
                if (data == null) continue;
                item.DiferenciaGTemp = data.DIFERENCIAG;
                item.Home = item.Home;
                item.Away = item.Away;
                listaInfoSorted.Add(item);
            }

            return listaInfoSorted;
        }

        private static List<ANDATAPERCENTUNG> AddElementosLista(List<AgrupadorInfoGeneralDTO> listInfo, int dayofweek, int diaMes, int mesNum, int diaAnio, int fechaNum,
            int totalElementos, int idInicioPer, DateTime itemFecha, int tipoOrden)
        {
            List<ANDATAPERCENTUNG> listaFinal = new List<ANDATAPERCENTUNG>();
            foreach (var item in listInfo)
            {
                ANDATAPERCENTUNG apInfo = new ANDATAPERCENTUNG();
                apInfo.ID = idInicioPer++;
                apInfo.FECHA = itemFecha;
                apInfo.FECHANUM = fechaNum;
                apInfo.DIFERENCIAG = item.DiferenciaGTemp;
                apInfo.DIASEM = dayofweek;
                apInfo.DIAMES = diaMes;
                apInfo.DIAANIO = diaAnio;
                apInfo.MESNUM = mesNum;
                double value = (double)(item.PosTemp * 100) / totalElementos;
                int posPercent = (int)Math.Ceiling(value);
                apInfo.PERCENT = posPercent;
                apInfo.TIPOORDEN = tipoOrden;
                listaFinal.Add(apInfo);
            }
            return listaFinal;
        }

        private static List<AgrupadorInfoGeneralDTO> GetListOrdered(DateTime fecha, SisResultEntities contexto,
            List<AgrupadorInfoGeneralDTO> listaInfo,
            List<AgrupadorConteosTimeSpanDTO> listPercent, int totalDia)
        {
            int pos = 1;
            List<AgrupadorInfoGeneralDTO> listaFinal = new List<AgrupadorInfoGeneralDTO>();
            foreach (var aigDTO in listaInfo)
            {
                aigDTO.PosTemp = pos++;
                double value = (double)(aigDTO.PosTemp * 100) / totalDia;
                int posPercent = (int)Math.Ceiling(value);
                var data = (from x in listPercent where x.Percent.Equals(posPercent) select x).FirstOrDefault();
                if (data != null && data.Rank <= 5) continue;
                listaFinal.Add(aigDTO);
            }
            return listaFinal;
        }

        public static int AsignarInfoRank(List<AgrupadorConteosTimeSpanDTO> lista, int spanActual)
        {
            return (from x in lista where x.Spantiempo.Equals(spanActual) select x.Rank).FirstOrDefault();
        }

        public static int AsignarInfoMaxRank(List<AgrupadorConteosTimeSpanDTO> lista)
        {
            return (from x in lista select x.Rank).Max();
        }

        public static int AsignarInfoMinSpan(List<AgrupadorConteosTimeSpanDTO> lista)
        {
            return (from x in lista select x.Spantiempo).Min();
        }
    }
}