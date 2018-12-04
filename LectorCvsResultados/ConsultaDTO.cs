using System;
using System.Collections.Generic;

namespace LectorCvsResultados
{
    public class AgrupadorConsolidadoDTO
    {
        public int MaxValue
        {
            get;
            set;
        }

        public int MinValue
        {
            get;
            set;
        }

        public int Porcentaje
        {
            get;
            set;
        }

        public int TotalPositivosMuestras
        {
            get;
            set;
        }

        public override string ToString()
        {
            return TotalPositivosMuestras + ";" + MaxValue + ";" + MinValue + ";" + Porcentaje;
        }
    }

    public class AgrupadorConteosTimeSpanDTO
    {
        public int DiaSemNum { get; set; }
        public int Fechanum { get; set; }
        public int Rank { get; set; }
        public int Spantiempo { get; set; }
        public int Total { get; set; }
        public int TabIndex { get; set; }
        public int TabIndexLetter { get; set; }
        public string GroupLetter { get; set; }
        public int Percent { get; set; }
        public double Prob { get; set; }
        public int TotalGen { get; set; }
        public int TotalDif { get; set; }
        public string Key { get; set; }
    }

    public class AgrupadorFechaNumValor
    {
        public int FechaNum
        {
            get; set;
        }

        public int Tabindex
        {
            get; set;
        }

        public int TabindexLetter
        {
            get; set;
        }

        public int Spantiempo
        {
            get; set;
        }

        public string GroupLetter
        {
            get; set;
        }
    }

    public class AgrupadorTabIndexDiferenciaDTO
    {
        public int Diferencia
        {
            get; set;
        }

        public int Tabindex
        {
            get; set;
        }
    }

    public class AgrupadorTimeSpanDTO
    {
        public Dictionary<int, int> DictRachasAcumuladas
        {
            get;
            set;
        }

        public int UltimoEnRachas
        {
            get;
            set;
        }

        //public USERRESULTTABLESFS UltimoGuardado
        //{
        //    get;
        //    set;
        //}

        public List<int> ValoresAparicion
        {
            get;
            set;
        }

        public List<int> ValoresAparicionAcumulada
        {
            get;
            set;
        }
    }

    public class AgrupadorTotalTabIndexDTO
    {
        public int Lineindex { get; set; }
        public int Tabindex { get; set; }
        public double Total { get; set; }
        public int Apariciones { get; set; }
        public string GroupLetter { get; set; }
        public int Rank { get; set; }
    }

    public class AgrupadorTotalPercentSpanDTO
    {
        public double Total
        {
            get; set;
        }

        public int Span
        {
            get; set;
        }

        public int Rank
        {
            get; set;
        }
    }

    public class AnalisisDatosDTO
    {
        public string AnalizedData
        {
            get;
            set;
        }

        public string Fecha
        {
            get; set;
        }

        public int MinimoApariciones
        {
            get; set;
        }

        public double PromedioNegativo
        {
            get; set;
        }

        public double PromedioPositivo
        {
            get; set;
        }

        public int ResultadosNegativos
        {
            get; set;
        }

        public int ResultadosPositivos
        {
            get; set;
        }

        public int TotalDatos
        {
            get; set;
        }

        public string UltimoParecido
        {
            get;
            set;
        }

        public override string ToString()
        {
            return +TotalDatos + ";" + ResultadosPositivos + ";" + PromedioPositivo + ";" + ResultadosNegativos + ";" + PromedioNegativo + ";" + MinimoApariciones + ";" + Fecha + ";" + UltimoParecido + ";" + AnalizedData;
        }
    }

    public class AnalizedTabIndexDTO
    {
        public int Lineindex
        {
            get; set;
        }

        public int Tabindex
        {
            get; set;
        }

        public int UltimoSpan
        {
            get; set;
        }

        public int RankUltimoSpanDia
        {
            get; set;
        }

        public int RankUltimoSpanGral
        {
            get; set;
        }

        public string TMatch
        {
            get; set;
        }

        public int Result
        {
            get; set;
        }

        public override string ToString()
        {
            return Result + ";" + Lineindex + ";" + Tabindex + ";" + UltimoSpan + ";" + RankUltimoSpanDia + ";" + RankUltimoSpanGral + ";" + TMatch + ";";
        }
    }

    

    public class AgrupadorMaxFechasTGDTO
    {
        public decimal Id
        {
            get; set;
        }

        public int Fechanum
        {
            get; set;
        }

        public int Tabindexletter
        {
            get; set;
        }

        public string GroupLetter
        {
            get; set;
        }
    }

    public class AgrupadorInfoGeneralDTO
    {
        public AgrupadorInfoGeneralDTO()
        {
            ListData = new List<int>();
        }
        public int Tabindexletter { get; set; }
        public string GroupLetter { get; set; }
        public int Tabindex { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexGen { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexDiaSem { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexDiaMes { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexDiaAnio { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexMesNum { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexDiaSemMesNum { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexDiaMesMesNum { get; set; }


        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxGroupTabGen { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxGroupTabDiaSem { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxGroupTabDiaMes { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxGroupTabDiaAnio { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxGroupTabMesNum { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxGroupTabDiaSemMesNum { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxGroupTabDiaMesMesNum { get; set; }


        public AgrupadorFechaNumValor AgrUltFechaNumSpanGen { get; set; }
        public AgrupadorFechaNumValor AgrUltFechaNumSpanDiaSem { get; set; }
        public AgrupadorFechaNumValor AgrUltFechaNumSpanDiaMes { get; set; }
        public AgrupadorFechaNumValor AgrUltFechaNumSpanDiaAnio { get; set; }
        public AgrupadorFechaNumValor AgrUltFechaNumSpanGlGen { get; set; }
        public AgrupadorFechaNumValor AgrUltFechaNumSpanGlDiaSem { get; set; }
        public AgrupadorFechaNumValor AgrUltFechaNumSpanGlDiaMes { get; set; }
        public AgrupadorFechaNumValor AgrUltFechaNumSpanGlDiaAnio { get; set; }
        public List<AgrupadorConteosTimeSpanDTO> ListRankSpansGen { get; set; }
        public List<AgrupadorConteosTimeSpanDTO> ListRankSpansDiaSem { get; set; }
        public List<AgrupadorConteosTimeSpanDTO> ListRankSpansDiaMes { get; set; }
        public List<AgrupadorConteosTimeSpanDTO> ListRankSpansGlGen { get; set; }
        public List<AgrupadorConteosTimeSpanDTO> ListRankSpansGlDiaSem { get; set; }
        public List<AgrupadorConteosTimeSpanDTO> ListRankSpansGlDiaMes { get; set; }
        public int RankSpanActualGen { get; set; }
        public int RankSpanActualDiaSem { get; set; }
        public int RankSpanActualDiaMes { get; set; }
        public int RankSpanActualGlGen { get; set; }
        public int RankSpanActualGlDiaSem { get; set; }
        public int RankSpanActualGlDiaMes { get; set; }
        public int MaxRankSpanActualGen { get; set; }
        public int MaxRankSpanActualDiaSem { get; set; }
        public int MaxRankSpanActualGlGen { get; set; }
        public int MaxRankSpanActualGlDiaSem { get; set; }
        public int MaxRankSpanActualGlDiaMes { get; set; }
        public int DiferenciaGTemp { get; set; }
        public int MinSpanGlGen { get; set; }
        public int MinSpanGlDiaSem { get; set; }
        public int MinSpanGlDiaMes { get; set; }
        public int MinSpanTiGen { get; set; }
        public int MinSpanTiDiaSem { get; set; }

        public string Home { get; set; }
        public string Away { get; set; }

        public int Puntuacion { get; set; }
        public List<int> TipoIncremento { get; set; }
        public int Percent { get; set; }
        public int PosTemp { get; set; }
        public int TipoOrden { get; set; }
        public List<AgrupadorConteosTimeSpanDTO> ListRankSpansGlDiaAnio { get; set; }
        public int RankSpanActualGlDiaAnio { get; set; }
        public int MaxRankSpanActualGlDiaAnio { get; set; }
        public int MinSpanGlDiaAnio { get; set; }
        public int MaxRankSpanActualDiaMes { get; set; }
        public int MinSpanTiDiaMes { get; set; }
        public List<AgrupadorConteosTimeSpanDTO> ListRankSpansDiaAnio { get; set; }
        public int RankSpanActualDiaAnio { get; set; }
        public int MaxRankSpanActualDiaAnio { get; set; }
        public int MinSpanDiaAnio { get; set; }
        public List<int> ListData { get; set; }
        public List<int> ListaInfoPivot { get; set; }
    }

    public class AgrupadorMaxTabIndex
    {
        public int Total
        {
            get; set;
        }

        public int Tabindex
        {
            get; set;
        }

        public string GroupLetter
        {
            get; set;
        }
    }

    public class InfoAnalisisDTO
    {
        public int Positivos { get; set; }
        public int Negativos { get; set; }
        public double AvgPos { get { return Positivos + Negativos != 0 ? (Positivos * 100000) / (Positivos + Negativos) : 0; } }
        public double AvgNeg { get { return Positivos + Negativos != 0 ? (Negativos * 100000) / (Positivos + Negativos) : 0; } }
        public double AvgPosVal { get { return Positivos + Negativos != 0 ? Positivos * 100 / (Positivos + Negativos) : 0; } }
        public override string ToString()
        {
            return Positivos + ";" + Negativos + ";" + AvgPos + ";" + AvgNeg;
        }
    }
}