using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.FlashOrdered
{
    public class AnDataInfoPosRank
    {
        public static void AddInfoPosRank(SisResultEntities contexto, DateTime fechaMin, DateTime laFechaMax)
        {
            List<AgrupadorInfoGeneralDTO> listaTemp;
            InfoAnalisisDTO info = new InfoAnalisisDTO();
            List<FLASHORDERED> listaHtmlTemp;
            List<FLASHORDERED> listaDia;
            int idInicio = ConsultasClassFO.ConsultarMaxIdActual(contexto, ConstantesGenerales.TBL_POSRANK);
            List<ANDATAINFOPOSRANK> lstDataPersist = new List<ANDATAINFOPOSRANK>();
            for (var i = fechaMin; i < laFechaMax; i = i.AddDays(1))
            {
                lstDataPersist.Clear();
                listaHtmlTemp = UtilGeneral.UtilHtml.LeerInfoHtmlTempActual(i, 1);
                listaTemp = AnDataFlashOrdered.ValidarElementosDia(i, 1, contexto, listaHtmlTemp);
                listaDia = UtilGeneral.UtilHtml.LeerInfoHtml(i, 1);
                for (int rank = 1; rank <= 5; rank++)
                {
                    AgrupadorInfoGeneralDTO aigDTOTabindexGen = (from x in listaTemp where x.AgrupadorPromMaxTabindexGen != null && x.AgrupadorPromMaxTabindexGen.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOTabindexDiaSem = (from x in listaTemp where x.AgrupadorPromMaxTabindexDiaSem != null && x.AgrupadorPromMaxTabindexDiaSem.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOTabindexDiaMes = (from x in listaTemp where x.AgrupadorPromMaxTabindexDiaMes != null && x.AgrupadorPromMaxTabindexDiaMes.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOTabindexDiaAnio = (from x in listaTemp where x.AgrupadorPromMaxTabindexDiaAnio != null && x.AgrupadorPromMaxTabindexDiaAnio.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOTabindexMesNum = (from x in listaTemp where x.AgrupadorPromMaxTabindexMesNum != null && x.AgrupadorPromMaxTabindexMesNum.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOGroupTabGen = (from x in listaTemp where x.AgrupadorPromMaxGroupTabGen != null && x.AgrupadorPromMaxGroupTabGen.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOGroupTabDiaSem = (from x in listaTemp where x.AgrupadorPromMaxGroupTabDiaSem != null && x.AgrupadorPromMaxGroupTabDiaSem.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOGroupTabDiaMes = (from x in listaTemp where x.AgrupadorPromMaxGroupTabDiaMes != null && x.AgrupadorPromMaxGroupTabDiaMes.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOGroupTabDiaAnio = (from x in listaTemp where x.AgrupadorPromMaxGroupTabDiaAnio != null && x.AgrupadorPromMaxGroupTabDiaAnio.Rank == rank select x).FirstOrDefault();
                    AgrupadorInfoGeneralDTO aigDTOGroupTabMesNum = (from x in listaTemp where x.AgrupadorPromMaxGroupTabMesNum != null && x.AgrupadorPromMaxGroupTabMesNum.Rank == rank select x).FirstOrDefault();
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_TAB_GEN, aigDTOTabindexGen, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_TAB_DIASEM, aigDTOTabindexDiaSem, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_TAB_DIAMES, aigDTOTabindexDiaMes, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_TAB_DIAANIO, aigDTOTabindexDiaAnio, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_TAB_MESNUM, aigDTOTabindexMesNum, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_GR_TAB_GEN, aigDTOGroupTabGen, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_GR_TAB_DIASEM, aigDTOGroupTabDiaSem, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_GR_TAB_DIAMES, aigDTOGroupTabDiaMes, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_GR_TAB_DIAANIO, aigDTOGroupTabDiaAnio, contexto, idInicio, i, rank, lstDataPersist);
                    idInicio = GetValueInfoRank(listaDia, ConstantesGenerales.CASO_GR_TAB_MESNUM, aigDTOGroupTabMesNum, contexto, idInicio, i, rank, lstDataPersist);
                }
                contexto.ANDATAINFOPOSRANK.AddRange(lstDataPersist);
                contexto.SaveChanges();
            }
        }


        public static int GetValueInfoRank(List<FLASHORDERED> listaDia, int tipo, AgrupadorInfoGeneralDTO aigDTO, SisResultEntities contexto, int idInicio, DateTime fecha, int indexRank
            , List<ANDATAINFOPOSRANK> lstDataPersist)
        {
            int dayofweek = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
            int fechaNum = Convert.ToInt32(fecha.ToString("yyyyMMdd"));
            if (aigDTO != null)
            {

                var data = (from x in listaDia where x.TABINDEX == aigDTO.Tabindex select x).FirstOrDefault();
                if (data != null)
                {
                    ANDATAINFOPOSRANK a = new ANDATAINFOPOSRANK();
                    ANDATAINFOPOSRANK infoHist = contexto.ANDATAINFOPOSRANK
                            .Where(x => x.TIPO == tipo && x.INDEXRANK == indexRank)
                            .OrderByDescending(x => x.FECHANUM).FirstOrDefault();
                    a.FECHANUM = fechaNum;
                    a.DIASEM = dayofweek;
                    a.DIAMES = fecha.Day;
                    a.DIAANIO = fecha.DayOfYear;
                    a.DIFERENCIAG = data.DIFERENCIAG;
                    a.ID = idInicio++;
                    a.INDEXRANK = indexRank;
                    a.TABINDEX = aigDTO.Tabindex;
                    a.TIPO = tipo;
                    a.GROUPLETTER = aigDTO.GroupLetter;
                    a.TABINDEXLETTER = aigDTO.Tabindexletter;
                    if (data.DIFERENCIAG == 0)
                    {
                        if (infoHist.HISTACUM > 0)
                        {
                            a.HISTACUM = -1;
                        }
                        else
                        {
                            a.HISTACUM = --infoHist.HISTACUM;
                            infoHist.HISTACUM = null;
                        }
                    }
                    else
                    {
                        if (infoHist.HISTACUM < 0)
                        {
                            a.HISTACUM = 1;
                        }
                        else
                        {
                            a.HISTACUM = ++infoHist.HISTACUM;
                            infoHist.HISTACUM = null;
                        }
                    }
                    lstDataPersist.Add(a);
                }
            }
            return idInicio;
        }

        public static List<AgrupadorInfoGeneralDTO> ValidarListaElementos(SisResultEntities contexto, DateTime fechaMin, DateTime laFechaMax,

            List<AgrupadorInfoGeneralDTO> listaTemp, int caso)
        {
            List<AgrupadorInfoGeneralDTO> listaTempFinal = new List<AgrupadorInfoGeneralDTO>();
            InfoAnalisisDTO info = new InfoAnalisisDTO();
            List<ANDATAINFOPOSRANK> lstDataPersist = new List<ANDATAINFOPOSRANK>();
            string fechanum = laFechaMax.ToString("yyyyMMdd");
            string fechaminnum = fechaMin.ToString("yyyyMMdd");
            int dayofweek = (int)laFechaMax.DayOfWeek == 0 ? 7 : (int)laFechaMax.DayOfWeek;
            string filtro;
            switch (caso)
            {
                case 1:
                    filtro = ConsultasClassFO.GetFiltro(ConstantesGenerales.CASO_DIASEM, dayofweek);
                    break;
                case 2:
                    filtro = ConsultasClassFO.GetFiltro(ConstantesGenerales.CASO_DIAMES, laFechaMax.Day);
                    break;
                case 3:
                    filtro = ConsultasClassFO.GetFiltro(ConstantesGenerales.CASO_DIASEM, dayofweek) + ConsultasClassFO.GetFiltro(ConstantesGenerales.CASO_DIAMES, laFechaMax.Day);
                    break;
                case 4:
                    filtro = ConsultasClassFO.GetFiltro(ConstantesGenerales.CASO_MESNUM, laFechaMax.Month);
                    break;
                case 5:
                    filtro = ConsultasClassFO.GetFiltro(ConstantesGenerales.CASO_MESNUM, laFechaMax.Month) + ConsultasClassFO.GetFiltro(ConstantesGenerales.CASO_DIASEM, dayofweek);
                    break;
                default:
                    filtro = "";
                    break;
            }

            string query = "SELECT COUNT(1) AS total, tipo AS tipo, indexrank As indexRank, "
+ "COUNT(CASE WHEN diferenciag != 0 THEN 1 END)/COUNT(1) AS Prob, "
+ "RANK () OVER(ORDER BY COUNT(CASE WHEN diferenciag != 0 THEN 1 END)/COUNT(1) DESC, COUNT(1) DESC) AS Rank "
+ "FROM andatainfoposrank "
+ "WHERE fechanum < {0} "
+ "{1} "
+ "AND fechanum >= {2} "
+ "GROUP BY tipo, indexrank";

            List<InfoAnalisisDTO4> lst = ConsultasClassFO.getInfoRanks(contexto, string.Format(query, fechanum, filtro, fechaminnum));
            AgrupadorInfoGeneralDTO aigDTO;
            for (int i = 0; i < listaTemp.Count && listaTempFinal.Count < 5 && i < lst.Count; i++)
            {
                InfoAnalisisDTO4 objDto = lst.ElementAt(i);
                switch (objDto.Tipo)
                {
                    case ConstantesGenerales.CASO_TAB_GEN:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxTabindexGen != null && x.AgrupadorPromMaxTabindexGen.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_TAB_DIASEM:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxTabindexDiaSem != null && x.AgrupadorPromMaxTabindexDiaSem.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_TAB_DIAMES:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxTabindexDiaMes != null && x.AgrupadorPromMaxTabindexDiaMes.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_TAB_DIAANIO:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxTabindexDiaAnio != null && x.AgrupadorPromMaxTabindexDiaAnio.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_TAB_MESNUM:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxTabindexMesNum != null && x.AgrupadorPromMaxTabindexMesNum.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_GR_TAB_GEN:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxGroupTabGen != null && x.AgrupadorPromMaxGroupTabGen.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_GR_TAB_DIASEM:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxGroupTabDiaSem != null && x.AgrupadorPromMaxGroupTabDiaSem.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_GR_TAB_DIAMES:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxGroupTabDiaMes != null && x.AgrupadorPromMaxGroupTabDiaMes.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_GR_TAB_DIAANIO:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxGroupTabDiaAnio != null && x.AgrupadorPromMaxGroupTabDiaAnio.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    case ConstantesGenerales.CASO_GR_TAB_MESNUM:
                        aigDTO = (from x in listaTemp where x.AgrupadorPromMaxGroupTabMesNum != null && x.AgrupadorPromMaxGroupTabMesNum.Rank == objDto.IndexRank select x).FirstOrDefault();
                        break;
                    default:
                        aigDTO = new AgrupadorInfoGeneralDTO();
                        break;

                }
                var data = (from x in listaTempFinal where x.Tabindex == aigDTO.Tabindex select x).FirstOrDefault();
                if (data == null)
                {
                    listaTempFinal.Add(aigDTO);
                }
            }

            return listaTempFinal;
        }
    }
}
