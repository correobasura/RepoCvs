﻿using LectorCvsResultados.UtilGeneral;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataSelectedInfo
    {
        /// <summary>
        /// Guarda los elementos que se leen de los archivos generados del análisis general
        /// </summary>
        /// <param name="contexto">instancia para la persistencia de los objetos</param>
        /// <param name="maxIdFile">Identificador del máximo archivo generado</param>
        public static void GuardarElementosSelectedInfo(SisResultEntities contexto, DateTime fecha, int valorTotal, List<FLASHORDERED> listaDia)
        {
            //List<FLASHORDERED> listaHtmlTemp = AnDataFlashOrdered.GetListaTemp(fecha, 1, contexto, valorTotal);
            //string strJoin = UtilHtml.ObtenerJoinElementos(listaHtmlTemp);
            //int maxTabindex = (from x in listaHtmlTemp select x.TABINDEX).Max();
            //string fechaFormat = fecha.ToString("yyyyMMdd");
            //int dayofweek = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            //List<AgrupadorInfoGeneralDTO> listaTemp = AnDataFlashOrdered.GetListaInfoAsignn(fecha, 1, contexto, listaHtmlTemp,
            //    dayofweek, fechaFormat, maxTabindex, strJoin);
            //ANDATASELECTEDINFO a;
            //List<ANDATASELECTEDINFO> listInfoAdd = new List<ANDATASELECTEDINFO>();
            //int idInicio = ConsultasClassFO.ConsultarMaxIdActual(contexto, ConstantesGenerales.TBL_SELECTINFO);
            //foreach (var item in listaTemp)
            //{
            //    var data = (from x in listaDia where x.TABINDEX == item.Tabindex select x).FirstOrDefault();
            //    if (data == null) continue;
            //    a = new ANDATASELECTEDINFO();
            //    a.ID = idInicio++;
            //    a.SPANGLGEN = item.AgrUltFechaNumSpanGlGen.Spantiempo;
            //    a.RANKSPANGLGEN = item.RankSpanActualGlGen;
            //    a.SPANGLDIASEM = item.AgrUltFechaNumSpanGlDiaSem.Spantiempo;
            //    a.RANKSPANGLDIASEM = item.RankSpanActualGlDiaSem;
            //    a.SPANGLDIAMES = item.AgrUltFechaNumSpanGlDiaMes.Spantiempo;
            //    a.RANKSPANGLDIAMES = item.RankSpanActualGlDiaMes;
            //    a.SPANTABGEN = item.AgrUltFechaNumSpanGen.Spantiempo;
            //    a.RANKSPANTABGEN = item.RankSpanActualGen;
            //    a.SPANTABDIASEM = item.AgrUltFechaNumSpanDiaSem.Spantiempo;
            //    a.RANKSPANTABDIASEM = item.RankSpanActualDiaSem;
            //    a.SPANTABDIAMES = item.AgrUltFechaNumSpanDiaMes.Spantiempo;
            //    a.RANKSPANTABDIAMES = item.RankSpanActualDiaMes;
            //    a.FECHANUM = data.FECHANUM;
            //    a.DIFERENCIAG = data.DIFERENCIAG;
            //    a.DIASEM = dayofweek;
            //    a.DIAMES = fecha.Day;
            //    listInfoAdd.Add(a);
            //}
            //contexto.ANDATASELECTEDINFO.AddRange(listInfoAdd);
            //contexto.SaveChanges();
        }
    }
}