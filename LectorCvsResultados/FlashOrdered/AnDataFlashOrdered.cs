﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataFlashOrdered
    {

        static string strGlGen1 = "11,5,6,4,8";
        static string strTiGen1 = "16,13,1,0,4,9,12";
        static string strGlGen2 = "10,12,14,5,7,6,1,3";
        static string strTiGen2 = "8,0,5,10,4,3,1";
        static string strGlGen3 = "13,12,10,11,3,4,7";
        static string strTiGen3 = "12,11,0,3";
        static string strGlGen4 = "13,2,0,6,9,11,7";
        static string strTiGen4 = "0,15,11,7,3,4,11,14,2,12";
        static string strGlGen5 = "12,8,9,2,4";
        static string strTiGen5 = "15,9,8,10,4,10,4,9,14";
        static string strGlGen6 = "5,2,0,8,1";
        static string strTiGen6 = "11,5,8,9,1";
        static string strGlGen7 = "11,2,1,4,5,7,10,12";
        static string strTiGen7 = "11,9,5,2,13";
        static string strGlDiaSem1 = "5";
        static string strGlDiaSem2 = "";
        static string strGlDiaSem3 = "";
        static string strGlDiaSem4 = "";
        static string strGlDiaSem5 = "";
        static string strGlDiaSem6 = "0,1";
        static string strGlDiaSem7 = "";

        private static Dictionary<int, List<int>> dictListGlGen = new Dictionary<int, List<int>>();
        private static Dictionary<int, List<int>> dictListGlDiaSem = new Dictionary<int, List<int>>();
        private static Dictionary<int, List<int>> dictListTiGen = new Dictionary<int, List<int>>();


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
            List<int> listaFechaNum = (from x in lista orderby x.FECHANUM select x.FECHANUM).Distinct().ToList();
            foreach (var fechaNum in listaFechaNum)
            {
                List<FLASHORDERED> listaPersist = lista.Where(x => x.FECHANUM == fechaNum).OrderBy(x => x.ID).ToList();
                string strJoin = ObtenerJoinElementos(listaPersist);
                var elementTemp = listaPersist.FirstOrDefault();
                int maxTabindex = (from x in listaPersist select x.TABINDEX).Max();
                List<AgrupadorFechaNumValor> listaDiaHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum);
                List<AgrupadorFechaNumValor> listaDiaSemHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, elementTemp.DIASEM, ConstantesGenerales.CASO_DIASEM);
                List<AgrupadorFechaNumValor> listaDiaMesHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, elementTemp.DIAMES, ConstantesGenerales.CASO_DIAMES);
                List<AgrupadorFechaNumValor> listaDiaAnioHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, maxTabindex, fechaNum, elementTemp.DIAANIO, ConstantesGenerales.CASO_DIAANIO);
                List<AgrupadorFechaNumValor> listaDiaGlHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin);
                List<AgrupadorFechaNumValor> listaDiaGlSemHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, elementTemp.DIASEM, ConstantesGenerales.CASO_DIASEM);
                List<AgrupadorFechaNumValor> listaDiaGlMesHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, elementTemp.DIAMES, ConstantesGenerales.CASO_DIAMES);
                List<AgrupadorFechaNumValor> listaDiaGlAnioHist = ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, fechaNum, strJoin, elementTemp.DIAANIO, ConstantesGenerales.CASO_DIAANIO);

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
                List<int> listTabindex = (from x in listaPersist select x.TABINDEX).ToList();
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

            //= groupLetter.Equals(string.Empty) ?
            //   ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, tabindex, fechaNum, diaSem, diaMes, diaAnio, caso)
            //   : ConsultasClassFO.ConsultarUltimoTimeSpan(contexto, tabindex, fechaNum, diaSem, diaMes, diaAnio, caso, groupLetter)
            //   ;
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
            string strJoin = ObtenerJoinElementos(listElementosAgregados);
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

        public static List<AgrupadorInfoGeneralDTO> ValidarElementosDia(DateTime fecha, int caso, SisResultEntities contexto)
        {
            Dictionary<int, List<int>> dictListGlGen = new Dictionary<int, List<int>>();
            dictListGlGen.Add(1, strGlGen1.Split(',').Select(Int32.Parse).ToList());
            dictListGlGen.Add(2, strGlGen2.Split(',').Select(Int32.Parse).ToList());
            dictListGlGen.Add(3, strGlGen3.Split(',').Select(Int32.Parse).ToList());
            dictListGlGen.Add(4, strGlGen4.Split(',').Select(Int32.Parse).ToList());
            dictListGlGen.Add(5, strGlGen5.Split(',').Select(Int32.Parse).ToList());
            dictListGlGen.Add(6, strGlGen6.Split(',').Select(Int32.Parse).ToList());
            dictListGlGen.Add(7, strGlGen7.Split(',').Select(Int32.Parse).ToList());

            Dictionary<int, List<int>> dictListGlDiaSem = new Dictionary<int, List<int>>();
            dictListGlDiaSem.Add(1, strGlDiaSem1.Split(',').Select(Int32.Parse).ToList());
            //dictListGlDiaSem.Add(2, strGlDiaSem2.Split(',').Select(Int32.Parse).ToList());
            //dictListGlDiaSem.Add(3, strGlDiaSem3.Split(',').Select(Int32.Parse).ToList());
            //dictListGlDiaSem.Add(4, strGlDiaSem4.Split(',').Select(Int32.Parse).ToList());
            //dictListGlDiaSem.Add(5, strGlDiaSem5.Split(',').Select(Int32.Parse).ToList());
            dictListGlDiaSem.Add(6, strGlDiaSem6.Split(',').Select(Int32.Parse).ToList());
            //dictListGlDiaSem.Add(7, strGlDiaSem7.Split(',').Select(Int32.Parse).ToList());

            Dictionary<int, List<int>> dictListTiGen = new Dictionary<int, List<int>>();
            dictListTiGen.Add(1, strTiGen1.Split(',').Select(Int32.Parse).ToList());
            dictListTiGen.Add(2, strTiGen2.Split(',').Select(Int32.Parse).ToList());
            dictListTiGen.Add(3, strTiGen3.Split(',').Select(Int32.Parse).ToList());
            dictListTiGen.Add(4, strTiGen4.Split(',').Select(Int32.Parse).ToList());
            dictListTiGen.Add(5, strTiGen5.Split(',').Select(Int32.Parse).ToList());
            dictListTiGen.Add(6, strTiGen6.Split(',').Select(Int32.Parse).ToList());
            dictListTiGen.Add(7, strTiGen7.Split(',').Select(Int32.Parse).ToList());

            List<FLASHORDERED> listaHtmlTemp = UtilGeneral.UtilHtml.LeerInfoHtmlTempActual(fecha, caso);
            List<AgrupadorInfoGeneralDTO> listaInfo = new List<AgrupadorInfoGeneralDTO>();
            string strJoin = ObtenerJoinElementos(listaHtmlTemp);
            int maxTabindex = (from x in listaHtmlTemp select x.TABINDEX).Max();
            string fechaFormat = fecha.ToString("yyyyMMdd");
            int dayofweek = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int minRegistros = 220;

            //Obtener los promedios
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexGen = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaSem = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaMes = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxTabindexDiaAnio = ConsultasClassFO.ConsultarPromResultadosMaxTabindex(maxTabindex, fechaFormat, contexto, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabGen = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaSem = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaMes = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            List<AgrupadorTotalTabIndexDTO> listaPromMaxGroupTabDiaAnio = ConsultasClassFO.ConsultarPromResultadosGroupTab(strJoin, fechaFormat, contexto, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);

            //Obtener los máximos tabindex seq
            List<AgrupadorMaxTabIndex> listaMaxTabindexSeq = ConsultasClassFO.ConsultarMaxSeqTabindex(maxTabindex, fechaFormat, contexto);
            List<AgrupadorMaxTabIndex> listaMaxTabindexGL = ConsultasClassFO.ConsultarMaxSeqTabindexGl(fechaFormat, contexto, strJoin);

            //Obtener las fechas máximas de los tabindex
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGen = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaSem = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaMes = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexDiaAnio = ConsultasClassFO.ConsultarMaxFechaAndSpanTabindex(contexto, maxTabindex, fechaFormat, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLGen = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaSem = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaMes = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIAMES, fecha.Day);
            List<AgrupadorFechaNumValor> listaFechasMaxTabindexGLDiaAnio = ConsultasClassFO.ConsultarMaxFechaTabindexAndSpanGl(contexto, fechaFormat, strJoin, ConstantesGenerales.CASO_DIAANIO, fecha.DayOfYear);

            string strJoinTabindex = "(" + ConstantesModel.FECHANUM + " < {0} AND " + ConstantesModel.TABINDEX + " = {1})";
            string strJoinTabindexGl = "(" + ConstantesModel.FECHANUM + " < {0} AND " + ConstantesModel.GROUPLETTER + " = '{1}' AND " + ConstantesModel.TABINDEXLETTER + " = {2})";
            List<string> listStrJoinTabindex = new List<string>();
            List<string> listStrJoinTabindexDiaSem = new List<string>();
            List<string> listStrJoinGlTabindex = new List<string>();
            List<string> listStrJoinGlTabindexDiaSem = new List<string>();

            foreach (var item in listaHtmlTemp)
            {
                AgrupadorInfoGeneralDTO aigDTO = new AgrupadorInfoGeneralDTO();
                int total = (from x in listaMaxTabindexGL where x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER) select x.Total).FirstOrDefault();
                if (total == 0 || total < minRegistros) continue;
                aigDTO.Tabindex = item.TABINDEX;
                aigDTO.Tabindexletter = item.TABINDEXLETTER;
                aigDTO.GroupLetter = item.GROUPLETTER;
                aigDTO.AgrupadorPromMaxTabindexGen = listaPromMaxTabindexGen.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxTabindexDiaSem = listaPromMaxTabindexDiaSem.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxTabindexDiaMes = listaPromMaxTabindexDiaMes.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxTabindexDiaAnio = listaPromMaxTabindexDiaAnio.Where(x => x.Tabindex.Equals(item.TABINDEX)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxGroupTabGen = listaPromMaxGroupTabGen.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxGroupTabDiaSem = listaPromMaxGroupTabDiaSem.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxGroupTabDiaMes = listaPromMaxGroupTabDiaMes.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                aigDTO.AgrupadorPromMaxGroupTabDiaAnio = listaPromMaxGroupTabDiaAnio.Where(x => x.Tabindex.Equals(item.TABINDEXLETTER) && x.GroupLetter.Equals(item.GROUPLETTER)).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGen = (from x in listaFechasMaxTabindexGen where x.Tabindex.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanDiaSem = (from x in listaFechasMaxTabindexDiaSem where x.Tabindex.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanDiaMes = (from x in listaFechasMaxTabindexDiaMes where x.Tabindex.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanDiaAnio = (from x in listaFechasMaxTabindexDiaAnio where x.TabindexLetter.Equals(aigDTO.Tabindex) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGlGen = (from x in listaFechasMaxTabindexGLGen where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGlDiaSem = (from x in listaFechasMaxTabindexGLDiaSem where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGlDiaMes = (from x in listaFechasMaxTabindexGLDiaMes where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();
                aigDTO.AgrUltFechaNumSpanGlDiaAnio = (from x in listaFechasMaxTabindexGLDiaAnio where x.TabindexLetter.Equals(aigDTO.Tabindexletter) && x.GroupLetter.Equals(aigDTO.GroupLetter) select x).FirstOrDefault();
                listStrJoinTabindex.Add(string.Format(strJoinTabindex, aigDTO.AgrUltFechaNumSpanGen.FechaNum, aigDTO.Tabindex));
                listStrJoinGlTabindex.Add(string.Format(strJoinTabindexGl, aigDTO.AgrUltFechaNumSpanGlGen.FechaNum, aigDTO.GroupLetter, aigDTO.Tabindexletter));
                listStrJoinTabindexDiaSem.Add(string.Format(strJoinTabindex, aigDTO.AgrUltFechaNumSpanDiaSem.FechaNum, aigDTO.Tabindex));
                listStrJoinGlTabindexDiaSem.Add(string.Format(strJoinTabindexGl, aigDTO.AgrUltFechaNumSpanGlDiaSem.FechaNum, aigDTO.GroupLetter, aigDTO.Tabindexletter));
                aigDTO.DiferenciaGTemp = item.DIFERENCIAG;
                aigDTO.Home = item.Home;
                aigDTO.Away = item.Away;
                listaInfo.Add(aigDTO);
            }

            string strTabindexJoinTabIndexMaxFechas = string.Join(" OR ", listStrJoinTabindex);
            string strTabindexJoinTabIndexMaxFechasDiaSem = string.Join(" OR ", listStrJoinTabindexDiaSem);
            string strTabindexJoinTabIndexGlMaxFechas = string.Join(" OR ", listStrJoinGlTabindex);
            string strTabindexJoinTabIndexGlMaxFechasDiaSem = string.Join(" OR ", listStrJoinGlTabindexDiaSem);

            //Consulta de los rank
            List<AgrupadorConteosTimeSpanDTO> listaRanksGen = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechas);
            List<AgrupadorConteosTimeSpanDTO> listaRanksDiaSem = ConsultasClassFO.ConsultarRanksTabindex(contexto, strTabindexJoinTabIndexMaxFechasDiaSem, ConstantesGenerales.CASO_DIASEM, dayofweek);
            List<AgrupadorConteosTimeSpanDTO> listaRanksGlGen = ConsultasClassFO.ConsultarRanksGlTabindex(contexto, strTabindexJoinTabIndexGlMaxFechas);
            List<AgrupadorConteosTimeSpanDTO> listaRanksGlDiaSem = ConsultasClassFO.ConsultarRanksGlTabindex(contexto, strTabindexJoinTabIndexGlMaxFechasDiaSem, ConstantesGenerales.CASO_DIASEM, dayofweek);

            List<AgrupadorInfoGeneralDTO> listaFinal = new List<AgrupadorInfoGeneralDTO>();
            List<AgrupadorInfoGeneralDTO> listaFinalDep = new List<AgrupadorInfoGeneralDTO>();
            foreach (var item in listaInfo)
            {
                item.ListRankSpansGlGen = (from x in listaRanksGlGen where x.TabIndexLetter.Equals(item.Tabindexletter) && x.GroupLetter.Equals(item.GroupLetter) select x).ToList();
                if (item.ListRankSpansGlGen.Count == 0) continue;
                item.RankSpanActualGlGen = AsignarInfoRank(item.ListRankSpansGlGen, item.AgrUltFechaNumSpanGlGen.Spantiempo);
                item.MaxRankSpanActualGlGen = AsignarInfoMaxRank(item.ListRankSpansGlGen);
                item.MinSpanGlGen = AsignarInfoMinSpan(item.ListRankSpansGlGen);

                item.ListRankSpansGen = (from x in listaRanksGen where x.TabIndex.Equals(item.Tabindex) select x).ToList();
                if (item.ListRankSpansGen.Count == 0) continue;
                item.RankSpanActualGen = AsignarInfoRank(item.ListRankSpansGen, item.AgrUltFechaNumSpanGen.Spantiempo);
                item.MaxRankSpanActualGen = AsignarInfoMaxRank(item.ListRankSpansGen);
                item.MinSpanTiGen = AsignarInfoMinSpan(item.ListRankSpansGen);

                item.ListRankSpansGlDiaSem = (from x in listaRanksGlDiaSem where x.TabIndexLetter.Equals(item.Tabindexletter) && x.GroupLetter.Equals(item.GroupLetter) select x).ToList();
                if (item.ListRankSpansGlDiaSem.Count == 0) continue;
                item.RankSpanActualGlDiaSem = AsignarInfoRank(item.ListRankSpansGlDiaSem, item.AgrUltFechaNumSpanGlDiaSem.Spantiempo);
                item.MaxRankSpanActualGlDiaSem = AsignarInfoMaxRank(item.ListRankSpansGlDiaSem);
                item.MinSpanGlDiaSem = AsignarInfoMinSpan(item.ListRankSpansGlDiaSem);

                if (dictListGlGen[dayofweek].IndexOf(item.RankSpanActualGlGen) != -1
                    || (dictListGlDiaSem.ContainsKey(dayofweek) 
                        && dictListGlDiaSem[dayofweek].IndexOf(item.RankSpanActualGlDiaSem) != -1)
                    || dictListTiGen[dayofweek].IndexOf(item.RankSpanActualGen) != -1
                    ) continue;

                listaFinal.Add(item);
            }
            foreach (var item in listaFinal)
            {
                item.TipoIncremento = new List<int>();
                if (item.RankSpanActualGlGen != 0 && item.RankSpanActualGlGen >= item.MaxRankSpanActualGlGen)
                {
                    item.Puntuacion++;
                    item.TipoIncremento.Add(1);
                }
                if (item.RankSpanActualGlDiaSem != 0 && item.RankSpanActualGlDiaSem >= item.MaxRankSpanActualGlDiaSem)
                {
                    item.Puntuacion++;
                    item.TipoIncremento.Add(2);
                }
                if (item.RankSpanActualGen != 0 && item.RankSpanActualGen >= item.MaxRankSpanActualGen)
                {
                    item.Puntuacion++;
                    item.TipoIncremento.Add(3);
                }
                if (item.RankSpanActualDiaSem != 0 && item.RankSpanActualDiaSem >= item.MaxRankSpanActualDiaSem)
                {
                    item.Puntuacion++;
                    item.TipoIncremento.Add(4);
                }
                listaFinalDep.Add(item);
            }
            listaFinal = listaFinalDep.OrderBy(x => x.AgrupadorPromMaxGroupTabGen.Rank).Take(50).ToList();
            listaFinal = listaFinal.OrderBy(x => x.AgrupadorPromMaxTabindexGen.Rank).Take(35).ToList();
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

        public static string ObtenerJoinElementos(List<FLASHORDERED> listaElementos)
        {
            List<string> listaDistinctChar = listaElementos.Select(x => x.GROUPLETTER).Distinct().ToList();
            Dictionary<string, int> keyValuePairMaxIndexChar = new Dictionary<string, int>();
            foreach (var item in listaDistinctChar)
            {
                int maxValueIndexChar = (from x in listaElementos
                                         where x.GROUPLETTER.Equals(item)
                                         select x.TABINDEXLETTER).Max();
                keyValuePairMaxIndexChar.Add(item, maxValueIndexChar);
            }
            string agrupador = "({0})";
            string temp = "(" + ConstantesModel.GROUPLETTER + "  = '{0}' AND " + ConstantesModel.TABINDEXLETTER + " <= {1})";
            List<string> listaJoins = new List<string>();
            foreach (var item in keyValuePairMaxIndexChar)
            {
                listaJoins.Add(string.Format(temp, item.Key, item.Value));
            }
            string strJoin = string.Join(" OR ", listaJoins);
            strJoin = string.Format(agrupador, strJoin);
            return strJoin;
        }
    }
}
