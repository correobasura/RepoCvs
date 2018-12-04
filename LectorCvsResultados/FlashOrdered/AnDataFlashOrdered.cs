using LectorCvsResultados.UtilGeneral;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataFlashOrdered
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
            List<AgrupadorTotalTabIndexDTO> lstTabindexMax;
            List<AgrupadorTotalTabIndexDTO> lstTabindexMaxGroup;
            List<AgrupadorTotalTabIndexDTO> lstTabindexMaxComp;
            List<int> listTabindex;
            List<int> listIdsCompetition;
            FLASHORDERED elementTemp;
            List<int> listaFechaNum = (from x in lista orderby x.FECHANUM select x.FECHANUM).Distinct().ToList();
            List<FLASHORDERED> listaPersist;
            foreach (var fechaNum in listaFechaNum)
            {
                listaPersist = lista.Where(x => x.FECHANUM == fechaNum).OrderBy(x => x.ID).ToList();
                td = new TOTALESDIA();
                td.ID = fechaNum;
                maxTabindex = (from x in listaPersist select x.TABINDEX).Max();
                td.TOTAL = maxTabindex;
                contexto.TOTALESDIA.Add(td);
                contexto.TOTALESDIAGROPTAB.AddRange(GetValuesGroupTabTotales(listaPersist, fechaNum));
                listaPersist = listaPersist.Where(x => x.Estado.Equals("FP")).ToList();
                strJoin = UtilHtml.ObtenerJoinElementos(listaPersist);

                elementTemp = listaPersist.FirstOrDefault();
                listaDiaHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, ConstantesGenerales.CASO_DEFAULT);
                listaDiaSemHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, ConstantesGenerales.CASO_DIASEM, elementTemp.DIASEM);
                listaDiaMesHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, ConstantesGenerales.CASO_DIAMES, elementTemp.DIAMES);
                listaDiaAnioHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, ConstantesGenerales.CASO_DIAANIO, elementTemp.DIAANIO);
                listaDiaGlHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, ConstantesGenerales.CASO_DEFAULT);
                listaDiaGlSemHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, ConstantesGenerales.CASO_DIASEM, elementTemp.DIASEM);
                listaDiaGlMesHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, ConstantesGenerales.CASO_DIAMES, elementTemp.DIAMES);
                listaDiaGlAnioHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, ConstantesGenerales.CASO_DIAANIO, elementTemp.DIAANIO);
                lstTabindexMax = ConsultasClassFO.ConsultarNextTabindexSeq(contexto, maxTabindex);
                listIdsCompetition = (from x in listaPersist select x.IDCOMPETITION).Distinct().ToList();
                lstTabindexMaxGroup = ConsultasClassFO.ConsultarNextTabindexLetterSeq(contexto, strJoin);
                lstTabindexMaxComp = ConsultasClassFO.ConsultarNextTabCompSeq(contexto, listIdsCompetition);

                foreach (var obj in listaPersist)
                {
                    //obj.TABINDEXSEQ = ConsultasClassFO.ConsultarNextTabindexSeq(contexto, obj.TABINDEX);
                    //obj.TABINDEXLETTERSEQ = ConsultasClassFO.ConsultarNextTabindexLetterSeq(contexto, obj.GROUPLETTER, obj.TABINDEXLETTER);
                    obj.TABINDEXSEQ = (from x in lstTabindexMax where x.Tabindex.Equals(obj.TABINDEX) select x.Apariciones).FirstOrDefault() + 1;
                    obj.TABINDEXLETTERSEQ = (from x in lstTabindexMaxGroup where x.Tabindex.Equals(obj.TABINDEXLETTER) && x.GroupLetter.Equals(obj.GROUPLETTER) select x.Apariciones).FirstOrDefault() + 1;
                    obj.TABINDEXCOMPETSEQ = (from x in lstTabindexMaxComp where x.Lineindex.Equals(obj.IDCOMPETITION) && x.Tabindex.Equals(obj.TABINDEXCOMPETITION) select x.Apariciones).FirstOrDefault() + 1;
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

        private static IEnumerable<TOTALESDIAGROPTAB> GetValuesGroupTabTotales(List<FLASHORDERED> listaPersist, int fechanum)
        {
            List<TOTALESDIAGROPTAB> lstTotalesGroupTab = new List<TOTALESDIAGROPTAB>();
            List<string> lstGroupLetter = (from x in listaPersist select x.GROUPLETTER).Distinct().ToList();
            TOTALESDIAGROPTAB t;
            foreach (var item in lstGroupLetter)
            {
                t = new TOTALESDIAGROPTAB();
                t.FECHANUM = fechanum;
                t.GROUPLETTER = item;
                t.TOTAL = (from x in listaPersist where x.GROUPLETTER.Equals(item) select x.TABINDEXLETTER).Max();
                lstTotalesGroupTab.Add(t);
            }
            return lstTotalesGroupTab;
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
            _log.Info(MethodBase.GetCurrentMethod().Name);
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
                        maxFechaNumIndex = ConsultasClassFO.ConsultarMaxFechaTabindex(contexto, maxTabindex, ConstantesGenerales.CASO_DEFAULT);
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
            _log.Info(MethodBase.GetCurrentMethod().Name);
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
                        listaFechas = ConsultasClassFO.ConsultarMaxFechasTabGroup(contexto, strJoin, ConstantesGenerales.CASO_DEFAULT);
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
            SisResultEntities contexto, List<FLASHORDERED> listaHtmlTemp, int minDias)
        {
            string strJoin = UtilHtml.ObtenerJoinElementos(listaHtmlTemp);
            int maxTabindex = (from x in listaHtmlTemp select x.TABINDEX).Max();
            string fechaFormat = fecha.ToString("yyyyMMdd");
            int dayofweek = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            //List<AgrupadorInfoGeneralDTO> listaInfo = new List<AgrupadorInfoGeneralDTO>();
            List<AgrupadorInfoGeneralDTO> listaInfo = GetListaInfoAsignn(fecha, caso, contexto, listaHtmlTemp,
                dayofweek, fechaFormat, maxTabindex, strJoin);
            //IncrementInfoPointsPercent(fecha, contexto, listaHtmlTemp, listaInfo, fechaFormat, dayofweek, maxTabindex, strJoin);
            //ViewInfoRank(fecha, contexto, listaInfo, fechaFormat, dayofweek, maxTabindex, strJoin, minDias);
            //ConsultarProbSelectedInfo

            //string fechaMinformat = "fechanum > " + fecha.AddDays(valor).ToString("yyyyMMdd") + " AND";
            //List<AgrupadorConteosTimeSpanDTO> listSelectedInfo
            //    = ConsultasClassFO.ConsultarProbSelectedInfo(contexto,
            //    ConstantesModel.SPANGLGEN, ConstantesModel.RANKSPANGLGEN, fechaFormat);
            //foreach (var aigDTO in listaInfo)
            //{
            //    var data = (from x in listSelectedInfo
            //                where x.Spantiempo.Equals(aigDTO.AgrUltFechaNumSpanGlGen.Spantiempo)
            //                && x.Rank.Equals(aigDTO.RankSpanActualGlGen)
            //                select x).FirstOrDefault();
            //    if (data != null && data.Prob >= 0.91)
            //    {
            //        aigDTO.Puntuacion++;
            //    }
            //}
            //listSelectedInfo = ConsultasClassFO.ConsultarProbSelectedInfo(contexto,
            //    ConstantesModel.SPANTABGEN, ConstantesModel.RANKSPANTABGEN, fechaFormat, fechaMinformat);

            //foreach (var aigDTO in listaInfo)
            //{
            //    var data = (from x in listSelectedInfo
            //                where x.Spantiempo.Equals(aigDTO.AgrUltFechaNumSpanGen.Spantiempo)
            //                && x.Rank.Equals(aigDTO.RankSpanActualGen)
            //                select x).FirstOrDefault();
            //    if (data != null && data.Prob >= 0.9)
            //    {
            //        aigDTO.Puntuacion++;
            //    }
            //}

            //return listaInfo.Where(x => x.Puntuacion > 0).ToList();
            return listaInfo;
        }

        private static void ViewInfoRank(DateTime fecha, SisResultEntities contexto, List<AgrupadorInfoGeneralDTO> listaInfo, string fechaFormat, int dayofweek, int maxTabindex, string strJoin
            , int minDias)
        {
            var laFechaMin = fecha.AddDays(minDias);//Para Diasem
            //List<AgrupadorConteosTimeSpanDTO> listaRankGralDiaSem = ConsultasClassFO.ConsultarProbRankInfo(contexto, ConstantesGenerales.KEY_RANK_GRAL, ConstantesGenerales.CASO_DIASEM, fechaFormat, dayofweek, "");
            ////var laFechaMin = fecha.AddDays(minDias);
            //////laFechaMin = fecha.AddDays(-28);
            //List<AgrupadorConteosTimeSpanDTO> listaRankGralMes = ConsultasClassFO.ConsultarProbRankInfo(contexto, ConstantesGenerales.KEY_RANK_GRAL, ConstantesGenerales.CASO_DIAMES, fechaFormat, fecha.Day, "");
            List<AgrupadorConteosTimeSpanDTO> listaInfoBin = ConsultasClassFO.ConsultarProbRankInfo(contexto, ConstantesGenerales.KEY_RANK_GRAL, 0, fechaFormat, fecha.Day, laFechaMin.ToString("yyyyMMdd"));
            foreach (var item in listaInfo)
            {
                string key = string.Join("", item.ListData);
                //var dataDiaSem = (from x in listaRankGralDiaSem where x.Key.Equals(key) select x).FirstOrDefault();
                //var dataDiaMes = (from x in listaRankGralMes where x.Key.Equals(key) select x).FirstOrDefault();
                var dataDiaGral = (from x in listaInfoBin where x.Key.Equals(key) select x).FirstOrDefault();
                //if (dataDiaSem != null)
                //{
                //    item.Puntuacion++;
                //}
                //if (dataDiaMes != null)
                //{
                //    item.Puntuacion++;
                //}
                if (dataDiaGral != null)
                {
                    item.Puntuacion++;
                }
            }
        }

        private static void IncrementSelectedInfo(double valor, List<AgrupadorInfoGeneralDTO> listaInfo, List<AgrupadorConteosTimeSpanDTO> listSelectedInfo)
        {
            foreach (var aigDTO in listaInfo)
            {
                var data = (from x in listSelectedInfo
                            where x.Spantiempo.Equals(aigDTO.AgrUltFechaNumSpanGlGen.Spantiempo)
                            && x.Rank.Equals(aigDTO.RankSpanActualGlGen)
                            select x).FirstOrDefault();
                if (data != null && data.Prob >= valor)
                {
                    aigDTO.Puntuacion++;
                }
            }
        }

        private static void IncrementInfoPointsPercent(DateTime fecha, SisResultEntities contexto, List<FLASHORDERED> listaHtmlTemp, List<AgrupadorInfoGeneralDTO> listaInfo, string fechaformat, int dayofweek, int maxTabindex, string strJoin)
        {
            //int total;
            //double minRank = 0.85;
            //foreach (var item in listaInfo)
            //{
            //    total = 0;

            //    total += GetListOrdered(item.AgrupadorPromMaxTabindexGen, item, minRank);
            //    total += GetListOrdered(item.AgrupadorPromMaxTabindexDiaSem, item, minRank);
            //    //total += GetListOrdered(item.AgrupadorPromMaxTabindexDiaMes, item, minRank);
            //    //total += GetListOrdered(item.AgrupadorPromMaxTabindexDiaAnio, item, minRank);
            //    //total += GetListOrdered(item.AgrupadorPromMaxTabindexMesNum, item, minRank);
            //    //total += GetListOrdered(item.AgrupadorPromMaxGroupTabGen, item, minRank);
            //    //total += GetListOrdered(item.AgrupadorPromMaxGroupTabDiaSem, item, minRank);
            //    //total += GetListOrdered(item.AgrupadorPromMaxGroupTabDiaMes, item, minRank);
            //    //total += GetListOrdered(item.AgrupadorPromMaxGroupTabDiaAnio, item, minRank);
            //    //total += GetListOrdered(item.AgrupadorPromMaxGroupTabMesNum, item, minRank);
            //    //if (total>=8) item.Puntuacion++;
            //    item.Puntuacion = total;
            //}
            //List<AgrupadorTotalTabIndexDTO> listaInfoAgrupadorTi = ConsultasClassFO.ConsultarAgrupadorDiaMesDia(contexto, fechaformat, fecha.Month, fecha.Day, maxTabindex);
            //List<AgrupadorTotalTabIndexDTO> listaInfoAgrupadorGl = ConsultasClassFO.ConsultarAgrupadorDiaMesDiaGl(contexto, fechaformat, fecha.Month, fecha.Day, strJoin);
            //foreach (var item in listaInfo)
            //{
            //    var dataTi = (from x in listaInfoAgrupadorTi where x.Tabindex.Equals(item.Tabindex) select x).FirstOrDefault();
            //    if (dataTi != null)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    var dataGl = (from x in listaInfoAgrupadorGl where x.Tabindex.Equals(item.Tabindexletter) && x.GroupLetter.Equals(item.GroupLetter) select x).FirstOrDefault();
            //    if (dataGl != null)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    if (item.RankSpanActualGen <= item.MinSpanGlGen)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    if (item.RankSpanActualDiaSem <= item.MinSpanGlDiaSem)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    if (item.RankSpanActualDiaMes <= item.MinSpanGlDiaMes)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    if (item.RankSpanActualGlGen <= item.MinSpanTiGen)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    if (item.RankSpanActualGlDiaSem <= item.MinSpanTiDiaSem)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    if (item.RankSpanActualGlDiaMes <= item.MinSpanGlDiaAnio)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    if (item.RankSpanActualGlDiaAnio <= item.MinSpanTiDiaMes)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //    if (item.RankSpanActualDiaAnio <= item.MinSpanDiaAnio)
            //    {
            //        item.ListData.Add(1);
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //}
        }

        public static List<AgrupadorInfoGeneralDTO> GetListaInfoAsignn(DateTime fecha, int caso,
            SisResultEntities contexto, List<FLASHORDERED> listaHtmlTemp, int dayofweek, string fechaFormat,
            int maxTabindex, string strJoin)
        {
            List<AgrupadorInfoGeneralDTO> listaInfo = new List<AgrupadorInfoGeneralDTO>();

            //Obtener los promedios
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexGen = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DEFAULT);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaSem = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIASEM, dayofweek);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaMes = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaAnio = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexMesNum = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_MESNUM, fecha.Month);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaSemMesNum = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIASEM_MESNUM, dayofweek, fecha.Month);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaMesMesNum = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIAMES_MESNUM, fecha.Day, fecha.Month);

            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabGen = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DEFAULT);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaSem = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIASEM, dayofweek);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaMes = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaAnio = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabMesNum = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_MESNUM, fecha.Month);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaSemMesNum = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIASEM_MESNUM, dayofweek, fecha.Month);
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaMesMesNum = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIAMES_MESNUM, fecha.Day, fecha.Month);

            ////Obtener las fechas máximas de los tabindex
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexGen = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DEFAULT);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaSem = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIASEM, dayofweek);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaMes = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaAnio = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLGen = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DEFAULT);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaSem = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIASEM, dayofweek);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaMes = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaAnio = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);

            //string strJoinTabindex = "(" + ConstantesModel.FECHANUM + " < {0} AND " + ConstantesModel.TABINDEX + " = {1})";
            //string strJoinTabindexGl = "(" + ConstantesModel.FECHANUM + " < {0} AND " + ConstantesModel.GROUPLETTER + " = '{1}' AND " + ConstantesModel.TABINDEXLETTER + " = {2})";
            //List<string> listStrJoinTabindex = new List<string>();
            //List<string> listStrJoinTabindexDiaSem = new List<string>();
            //List<string> listStrJoinTabindexDiaMes = new List<string>();
            //List<string> listStrJoinTabindexDiaAnio = new List<string>();
            //List<string> listStrJoinGlTabindex = new List<string>();
            //List<string> listStrJoinGlTabindexDiaSem = new List<string>();
            //List<string> listStrJoinGlTabindexDiaMes = new List<string>();
            //List<string> listStrJoinGlTabindexDiaAnio = new List<string>();
            //double minProb = 0.85;

            foreach (var item in listaHtmlTemp)
            {
                AgrupadorInfoGeneralDTO aigDTO = new AgrupadorInfoGeneralDTO();
                aigDTO.Tabindex = item.TABINDEX;
                aigDTO.Tabindexletter = item.TABINDEXLETTER;
                aigDTO.GroupLetter = item.GROUPLETTER;
                aigDTO.DiferenciaGTemp = item.DIFERENCIAG;
                aigDTO.Home = item.Home;
                aigDTO.Away = item.Away;

                //aigDTO.AgrupadorPromMaxTabindexGen = listaPromMaxTabindexGen.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxTabindexDiaSem = listaPromMaxTabindexDiaSem.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxTabindexDiaMes = listaPromMaxTabindexDiaMes.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxTabindexDiaAnio = listaPromMaxTabindexDiaAnio.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxTabindexMesNum = listaPromMaxTabindexMesNum.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxTabindexDiaSemMesNum = listaPromMaxTabindexDiaSemMesNum.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxTabindexDiaMesMesNum = listaPromMaxTabindexDiaMesMesNum.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();

                //aigDTO.AgrupadorPromMaxGroupTabGen = listaPromMaxGroupTabGen.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxGroupTabDiaSem = listaPromMaxGroupTabDiaSem.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxGroupTabDiaMes = listaPromMaxGroupTabDiaMes.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxGroupTabDiaAnio = listaPromMaxGroupTabDiaAnio.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxGroupTabMesNum = listaPromMaxGroupTabMesNum.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxGroupTabDiaSemMesNum = listaPromMaxGroupTabDiaSemMesNum.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                //aigDTO.AgrupadorPromMaxGroupTabDiaMesMesNum = listaPromMaxGroupTabDiaMesMesNum.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();


                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxTabindexGen, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxTabindexDiaSem, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxTabindexDiaMes, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxTabindexDiaAnio, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxTabindexMesNum, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxTabindexDiaSemMesNum, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxTabindexDiaMesMesNum, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxGroupTabGen, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxGroupTabDiaSem, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxGroupTabDiaMes, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxGroupTabDiaAnio, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxGroupTabMesNum, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxGroupTabDiaSemMesNum, minProb));
                //aigDTO.ListData.Add(CompareValues(aigDTO.AgrupadorPromMaxGroupTabDiaMesMesNum, minProb));
                
                //aigDTO.AgrUltFechaNumSpanGen = AddInfoString(strJoinTabindex, listStrJoinTabindex, aigDTO, listaFechasMaxTabindexGen, 1);
                //aigDTO.AgrUltFechaNumSpanDiaSem = AddInfoString(strJoinTabindex, listStrJoinTabindexDiaSem, aigDTO, listaFechasMaxTabindexDiaSem, 1);
                //aigDTO.AgrUltFechaNumSpanDiaMes = AddInfoString(strJoinTabindex, listStrJoinTabindexDiaMes, aigDTO, listaFechasMaxTabindexDiaMes, 1);
                //aigDTO.AgrUltFechaNumSpanDiaAnio = AddInfoString(strJoinTabindex, listStrJoinTabindexDiaAnio, aigDTO, listaFechasMaxTabindexDiaAnio, 1);
                //aigDTO.AgrUltFechaNumSpanGlGen = AddInfoString(strJoinTabindexGl, listStrJoinGlTabindex, aigDTO, listaFechasMaxTabindexGLGen, 2);
                //aigDTO.AgrUltFechaNumSpanGlDiaSem = AddInfoString(strJoinTabindexGl, listStrJoinGlTabindexDiaSem, aigDTO, listaFechasMaxTabindexGLDiaSem, 2);
                //aigDTO.AgrUltFechaNumSpanGlDiaMes = AddInfoString(strJoinTabindexGl, listStrJoinGlTabindexDiaMes, aigDTO, listaFechasMaxTabindexGLDiaMes, 2);
                //aigDTO.AgrUltFechaNumSpanGlDiaAnio = AddInfoString(strJoinTabindexGl, listStrJoinGlTabindexDiaAnio, aigDTO, listaFechasMaxTabindexGLDiaAnio, 2);

                listaInfo.Add(aigDTO);
            }

            //string strTabindexJoinTabIndexMaxFechas = string.Join(" OR ", listStrJoinTabindex);
            //string strTabindexJoinTabIndexMaxFechasDiaSem = string.Join(" OR ", listStrJoinTabindexDiaSem);
            //string strTabindexJoinTabIndexMaxFechasDiaMes = string.Join(" OR ", listStrJoinTabindexDiaMes);
            //string strTabindexJoinTabIndexMaxFechasDiaAnio = string.Join(" OR ", listStrJoinTabindexDiaAnio);
            //string strTabindexJoinTabIndexGlMaxFechas = string.Join(" OR ", listStrJoinGlTabindex);
            //string strTabindexJoinTabIndexGlMaxFechasDiaSem = string.Join(" OR ", listStrJoinGlTabindexDiaSem);
            //string strTabindexJoinTabIndexGlMaxFechasDiaMes = string.Join(" OR ", listStrJoinGlTabindexDiaMes);
            //string strTabindexJoinTabIndexGlMaxFechasDiaAnio = string.Join(" OR ", listStrJoinGlTabindexDiaAnio);

            ////Consulta de los rank
            //List<AgrupadorConteosTimeSpanDTO> listaRanksGen = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechas, ConstantesGenerales.CASO_DEFAULT);
            //List<AgrupadorConteosTimeSpanDTO> listaRanksDiaSem = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechasDiaSem, ConstantesGenerales.CASO_DIASEM, dayofweek);
            //List<AgrupadorConteosTimeSpanDTO> listaRanksDiaMes = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechasDiaMes, ConstantesGenerales.CASO_DIAMES, dayofweek);
            //List<AgrupadorConteosTimeSpanDTO> listaRanksDiaAnio = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechasDiaAnio, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);
            //List<AgrupadorConteosTimeSpanDTO> listaRanksGlGen = ConsultasClassFO.ConsultarValoresPivotTab(contexto, strTabindexJoinTabIndexGlMaxFechas, ConstantesGenerales.CASO_DEFAULT);
            //List<AgrupadorConteosTimeSpanDTO> listaRanksGlDiaSem = ConsultasClassFO.ConsultarValoresPivotTab(contexto, strTabindexJoinTabIndexGlMaxFechasDiaSem, ConstantesGenerales.CASO_DIASEM, dayofweek);
            //List<AgrupadorConteosTimeSpanDTO> listaRanksGlDiaMes = ConsultasClassFO.ConsultarValoresPivotTab(contexto, strTabindexJoinTabIndexGlMaxFechasDiaMes, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            //List<AgrupadorConteosTimeSpanDTO> listaRanksGlDiaAnio = ConsultasClassFO.ConsultarValoresPivotTab(contexto, strTabindexJoinTabIndexGlMaxFechasDiaAnio, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);

            //foreach (var item in listaInfo)
            //{
            //    item.ListRankSpansGen = GetListSpanRank(listaRanksGen, item, 2);
            //    item.RankSpanActualGen = AsignarInfoRank(item.ListRankSpansGen, item.AgrUltFechaNumSpanGen.Spantiempo);
            //    if (item.RankSpanActualGen != 0)
            //    {
            //        item.MaxRankSpanActualGen = AsignarInfoMaxRank(item.ListRankSpansGen);
            //        item.MinSpanTiGen = AsignarInfoMinSpan(item.ListRankSpansGen);
            //        item.ListData.Add(AddInfoMinSpanListData(item.RankSpanActualGen));
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }

            //    item.ListRankSpansDiaSem = GetListSpanRank(listaRanksDiaSem, item, 2);
            //    item.RankSpanActualDiaSem = AsignarInfoRank(item.ListRankSpansDiaSem, item.AgrUltFechaNumSpanDiaSem.Spantiempo);
            //    if (item.RankSpanActualDiaSem != 0)
            //    {
            //        item.MaxRankSpanActualDiaSem = AsignarInfoMaxRank(item.ListRankSpansDiaSem);
            //        item.MinSpanTiDiaSem = AsignarInfoMinSpan(item.ListRankSpansDiaSem);
            //        item.ListData.Add(AddInfoMinSpanListData(item.RankSpanActualDiaSem));
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }

            //    item.ListRankSpansDiaMes = GetListSpanRank(listaRanksDiaMes, item, 2);
            //    item.RankSpanActualDiaMes = AsignarInfoRank(item.ListRankSpansDiaMes, item.AgrUltFechaNumSpanDiaMes.Spantiempo);
            //    if (item.RankSpanActualDiaMes != 0)
            //    {
            //        item.MaxRankSpanActualDiaMes = AsignarInfoMaxRank(item.ListRankSpansDiaMes);
            //        item.MinSpanTiDiaMes = AsignarInfoMinSpan(item.ListRankSpansDiaMes);
            //        item.ListData.Add(AddInfoMinSpanListData(item.RankSpanActualDiaMes));
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }

            //    item.ListRankSpansDiaAnio = GetListSpanRank(listaRanksDiaAnio, item, 2);
            //    item.RankSpanActualDiaAnio = AsignarInfoRank(item.ListRankSpansDiaAnio, item.AgrUltFechaNumSpanDiaAnio.Spantiempo);
            //    if (item.RankSpanActualDiaAnio != 0)
            //    {
            //        item.MaxRankSpanActualDiaAnio = AsignarInfoMaxRank(item.ListRankSpansDiaAnio);
            //        item.MinSpanDiaAnio = AsignarInfoMinSpan(item.ListRankSpansDiaAnio);
            //        item.ListData.Add(AddInfoMinSpanListData(item.RankSpanActualDiaAnio));
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }

            //    item.ListRankSpansGlGen = GetListSpanRank(listaRanksGlGen, item, 1);
            //    item.RankSpanActualGlGen = AsignarInfoRank(item.ListRankSpansGlGen, item.AgrUltFechaNumSpanGlGen.Spantiempo);
            //    if (item.RankSpanActualGlGen != 0)
            //    {
            //        item.MaxRankSpanActualGlGen = AsignarInfoMaxRank(item.ListRankSpansGlGen);
            //        item.MinSpanGlGen = AsignarInfoMinSpan(item.ListRankSpansGlGen);
            //        item.ListData.Add(AddInfoMinSpanListData(item.RankSpanActualGlGen));
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }

            //    item.ListRankSpansGlDiaSem = GetListSpanRank(listaRanksGlDiaSem, item, 1);
            //    item.RankSpanActualGlDiaSem = AsignarInfoRank(item.ListRankSpansGlDiaSem, item.AgrUltFechaNumSpanGlDiaSem.Spantiempo);
            //    if (item.RankSpanActualGlDiaSem != 0)
            //    {
            //        item.MaxRankSpanActualGlDiaSem = AsignarInfoMaxRank(item.ListRankSpansGlDiaSem);
            //        item.MinSpanGlDiaSem = AsignarInfoMinSpan(item.ListRankSpansGlDiaSem);
            //        item.ListData.Add(AddInfoMinSpanListData(item.RankSpanActualGlDiaSem));
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }

            //    item.ListRankSpansGlDiaMes = GetListSpanRank(listaRanksGlDiaMes, item, 1);
            //    item.RankSpanActualGlDiaMes = AsignarInfoRank(item.ListRankSpansGlDiaMes, item.AgrUltFechaNumSpanGlDiaMes.Spantiempo);
            //    if (item.RankSpanActualGlDiaMes != 0)
            //    {
            //        item.MaxRankSpanActualGlDiaMes = AsignarInfoMaxRank(item.ListRankSpansGlDiaMes);
            //        item.MinSpanGlDiaMes = AsignarInfoMinSpan(item.ListRankSpansGlDiaMes);
            //        item.ListData.Add(AddInfoMinSpanListData(item.RankSpanActualGlDiaMes));
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }

            //    item.ListRankSpansGlDiaAnio = GetListSpanRank(listaRanksGlDiaAnio, item, 1);
            //    item.RankSpanActualGlDiaAnio = AsignarInfoRank(item.ListRankSpansGlDiaAnio, item.AgrUltFechaNumSpanGlDiaAnio.Spantiempo);
            //    if (item.RankSpanActualGlDiaAnio != 0)
            //    {
            //        item.MaxRankSpanActualGlDiaAnio = AsignarInfoMaxRank(item.ListRankSpansGlDiaAnio);
            //        item.MinSpanGlDiaAnio = AsignarInfoMinSpan(item.ListRankSpansGlDiaAnio);
            //        item.ListData.Add(AddInfoMinSpanListData(item.RankSpanActualGlDiaAnio));
            //    }
            //    else
            //    {
            //        item.ListData.Add(0);
            //    }
            //}

            //List<object[]> lstIndexDias =
                ConsultasClassFO.ConsultarValoresPivotTab(contexto, maxTabindex);
            List<string> lstGroupLetter = (from x in listaInfo select x.GroupLetter).Distinct().ToList();
            List<string> lstStrQueries = new List<string>();
            foreach (var item in lstGroupLetter)
            {
                int total = (from x in listaInfo where x.GroupLetter.Equals(item) select x.Tabindexletter).Max();
                lstStrQueries.Add(string.Format(ConstantesConsultaFO.QUERY_COUNT_GRP_TAB_TOTAL, item, total, fechaFormat));
            }
            ConsultasClassFO.ConsultarValoresProbGroupTabTotal(contexto, string.Join(" UNION ALL ", lstStrQueries));
            return listaInfo;
        }

        private static int AddInfoMinSpanListData(int rankActual)
        {
            if (rankActual> 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private static AgrupadorFechaNumValor AddInfoString(string strJoin, List<string> lstStringJoin, AgrupadorInfoGeneralDTO aigDTO,
            List<AgrupadorFechaNumValor> lstAgrupadores, int caso)
        {
            AgrupadorFechaNumValor infoUltFecha;
            if(caso == 1)
            {
                infoUltFecha = (from x in lstAgrupadores where x.Tabindex.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                if (infoUltFecha != null)
                {
                    lstStringJoin.Add(string.Format(strJoin, infoUltFecha.FechaNum, aigDTO.Tabindex));
                }
                else
                {
                    infoUltFecha = new AgrupadorFechaNumValor();
                }
            }
            else
            {
                infoUltFecha = (from x in lstAgrupadores where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();
                if (infoUltFecha != null)
                {
                    lstStringJoin.Add(string.Format(strJoin, infoUltFecha.FechaNum, aigDTO.GroupLetter, aigDTO.Tabindexletter));
                }
                else
                {
                    infoUltFecha = new AgrupadorFechaNumValor();
                }
            }
            
            return infoUltFecha;
        }

        private static int CompareValues(AgrupadorTotalTabIndexDTO objInfo, double totalProb)
        {
            return objInfo != null && objInfo.Total >= totalProb ? 1 : 0;
        }

        private static List<AgrupadorConteosTimeSpanDTO> GetListSpanRank(List<AgrupadorConteosTimeSpanDTO> lista, AgrupadorInfoGeneralDTO item, int caso)
        {
            if (caso == 1)
            {
                return (from x in lista where x.TabIndexLetter.Equals(item.Tabindexletter) && x.GroupLetter.Equals(item.GroupLetter) select x).ToList();
            }
            else
            {
                return (from x in lista where x.TabIndex.Equals(item.Tabindex) select x).ToList();
            }
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
            //List<FLASHORDERED> listaHtmlTemp;
            //List<AgrupadorInfoGeneralDTO> lstAigDto;
            //List<AgrupadorInfoGeneralDTO> listaInfoTemp;
            //string strJoin;
            //int maxTabindex;
            //string fechaFormat;
            //int dayofweek;
            //int diaMes;
            //int mesNum;
            //int diaAnio;
            //int fechaNum;
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexGen;
            //List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabGen;
            //AgrupadorInfoGeneralDTO aigDTO;
            //List<ANDATAPERCENTUNG> listaFinal;
            //int totalElementos;
            //int idInicioPer = ConsultasClassFO.ConsultarMaxIdActual(contexto, ConstantesGenerales.TBL_PERCENT);
            //List<DateTime> listFechas = (from x in listaFin select x.FECHA).Distinct().ToList();
            //foreach (var itemFecha in listFechas)
            //{
            //    listaHtmlTemp = GetListaTemp(itemFecha, 1, contexto, minRegistros);
            //    lstAigDto = new List<AgrupadorInfoGeneralDTO>();
            //    listaInfoTemp = new List<AgrupadorInfoGeneralDTO>();
            //    strJoin = UtilHtml.ObtenerJoinElementos(listaHtmlTemp);
            //    maxTabindex = (from x in listaHtmlTemp select x.TABINDEX).Max();
            //    fechaFormat = itemFecha.ToString("yyyyMMdd");
            //    dayofweek = (int)itemFecha.DayOfWeek == 0 ? 7 : (int)itemFecha.DayOfWeek;
            //    diaMes = itemFecha.Day;
            //    mesNum = itemFecha.Month;
            //    diaAnio = itemFecha.DayOfYear;
            //    fechaNum = Convert.ToInt32(itemFecha.ToString("yyyyMMdd"));

            //    //Obtener los promedios
            //    listaPromMaxTabindexGen = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto);
            //    listaPromMaxGroupTabGen = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto);

            //    foreach (var item in listaHtmlTemp)
            //    {
            //        aigDTO = new AgrupadorInfoGeneralDTO();
            //        aigDTO.Tabindex = item.TABINDEX;
            //        aigDTO.Tabindexletter = item.TABINDEXLETTER;
            //        aigDTO.GroupLetter = item.GROUPLETTER;
            //        aigDTO.AgrupadorPromMaxTabindexGen = listaPromMaxTabindexGen.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
            //        if (aigDTO.AgrupadorPromMaxTabindexGen == null)
            //        {
            //            aigDTO.AgrupadorPromMaxTabindexGen = new AgrupadorTotalTabIndexDTO();
            //            aigDTO.AgrupadorPromMaxTabindexGen.Rank = 5000;
            //        }
            //        aigDTO.AgrupadorPromMaxGroupTabGen = listaPromMaxGroupTabGen.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
            //        if (aigDTO.AgrupadorPromMaxGroupTabGen == null)
            //        {
            //            aigDTO.AgrupadorPromMaxGroupTabGen = new AgrupadorTotalTabIndexDTO();
            //            aigDTO.AgrupadorPromMaxGroupTabGen.Rank = 5000;
            //        }
            //        lstAigDto.Add(aigDTO);
            //    }
            //    totalElementos = lstAigDto.Count;

            //    listaInfoTemp = SortListElements(listaFin, lstAigDto, itemFecha, ConstantesGenerales.ORDEN_GROUP_LETTER_GEN);
            //    listaFinal = new List<ANDATAPERCENTUNG>();
            //    listaFinal.AddRange(AddElementosLista(listaInfoTemp, dayofweek, diaMes, mesNum, diaAnio, fechaNum,
            //        totalElementos, idInicioPer, itemFecha, ConstantesGenerales.ORDEN_GROUP_LETTER_GEN));
            //    idInicioPer = (int)(from x in listaFinal select x.ID).Max() + 1;
            //    listaInfoTemp = SortListElements(listaFin, lstAigDto, itemFecha, ConstantesGenerales.ORDEN_TABINDEX_GEN);
            //    listaFinal.AddRange(AddElementosLista(listaInfoTemp, dayofweek, diaMes, mesNum, diaAnio, fechaNum,
            //        totalElementos, idInicioPer, itemFecha, ConstantesGenerales.ORDEN_TABINDEX_GEN));
            //    idInicioPer = (int)(from x in listaFinal select x.ID).Max() + 1;
            //    contexto.ANDATAPERCENTUNG.AddRange(listaFinal);
            //    contexto.SaveChanges();
            //    listaFin.RemoveAll(x => x.FECHA.Equals(itemFecha));
            //}
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

        //private static List<ANDATAPERCENTUNG> AddElementosLista(List<AgrupadorInfoGeneralDTO> listInfo, int dayofweek, int diaMes, int mesNum, int diaAnio, int fechaNum,
        //    int totalElementos, int idInicioPer, DateTime itemFecha, int tipoOrden)
        //{
        //    List<ANDATAPERCENTUNG> listaFinal = new List<ANDATAPERCENTUNG>();
        //    foreach (var item in listInfo)
        //    {
        //        ANDATAPERCENTUNG apInfo = new ANDATAPERCENTUNG();
        //        apInfo.ID = idInicioPer++;
        //        apInfo.FECHA = itemFecha;
        //        apInfo.FECHANUM = fechaNum;
        //        apInfo.DIFERENCIAG = item.DiferenciaGTemp;
        //        apInfo.DIASEM = dayofweek;
        //        apInfo.DIAMES = diaMes;
        //        apInfo.DIAANIO = diaAnio;
        //        apInfo.MESNUM = mesNum;
        //        double value = (double)(item.PosTemp * 100) / totalElementos;
        //        int posPercent = (int)Math.Ceiling(value);
        //        apInfo.PERCENT = posPercent;
        //        apInfo.TIPOORDEN = tipoOrden;
        //        listaFinal.Add(apInfo);
        //    }
        //    return listaFinal;
        //}

        private static int GetListOrdered(AgrupadorTotalTabIndexDTO objDto, AgrupadorInfoGeneralDTO objAsign, double rank)
        {
            if (objDto == null)
            {
                objDto = new AgrupadorTotalTabIndexDTO();
                objDto.Rank = 1000;
                objDto.Total = 0.5;
            }
            return objDto.Total >= rank ? 1 : 0;
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