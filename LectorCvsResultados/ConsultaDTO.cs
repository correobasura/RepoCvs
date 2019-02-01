using System;
using System.Collections.Generic;
using System.Text;

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

    public class AgrupadorInfoQuery
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
        public int ValueDiff { get; set; }
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
            //AgrUltFechaNumSpanGen = new AgrupadorInfoQuery();
            //AgrUltFechaNumSpanGen.ValueDiff = 2;
            //AgrUltFechaNumSpanDiaSem = new AgrupadorInfoQuery();
            //AgrUltFechaNumSpanDiaSem.ValueDiff = 2;
            //AgrUltFechaNumSpanDiaMes = new AgrupadorInfoQuery();
            //AgrUltFechaNumSpanDiaMes.ValueDiff = 2;
            //AgrUltFechaNumSpanDiaAnio = new AgrupadorInfoQuery();
            //AgrUltFechaNumSpanDiaAnio.ValueDiff = 2;
            //AgrUltFechaNumSpanMesNum = new AgrupadorInfoQuery();
            //AgrUltFechaNumSpanMesNum.ValueDiff = 2;
            //AgrUltFechaNumSpanDiaSemMesNum = new AgrupadorInfoQuery();
            //AgrUltFechaNumSpanDiaSemMesNum.ValueDiff = 2;
            //AgrUltFechaNumSpanDiaMesMesNum = new AgrupadorInfoQuery();
            //AgrUltFechaNumSpanDiaMesMesNum.ValueDiff = 2;
            DicContadoresGen = new Dictionary<int, int>();
            DicContadoresDiaSem = new Dictionary<int, int>();
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


        public AgrupadorInfoQuery AgrUltFechaNumSpanGen { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanDiaSem { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanDiaMes { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanDiaAnio { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanMesNum { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanDiaSemMesNum { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanDiaMesMesNum { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanGlGen { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanGlDiaSem { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanGlDiaMes { get; set; }
        public AgrupadorInfoQuery AgrUltFechaNumSpanGlDiaAnio { get; set; }
        public List<AgrupadorInfoQuery> ListRankSpansGen { get; set; }
        public List<AgrupadorInfoQuery> ListRankSpansDiaSem { get; set; }
        public List<AgrupadorInfoQuery> ListRankSpansDiaMes { get; set; }
        public List<AgrupadorInfoQuery> ListRankSpansGlGen { get; set; }
        public List<AgrupadorInfoQuery> ListRankSpansGlDiaSem { get; set; }
        public List<AgrupadorInfoQuery> ListRankSpansGlDiaMes { get; set; }
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
        public List<AgrupadorInfoQuery> ListRankSpansGlDiaAnio { get; set; }
        public int RankSpanActualGlDiaAnio { get; set; }
        public int MaxRankSpanActualGlDiaAnio { get; set; }
        public int MinSpanGlDiaAnio { get; set; }
        public int MaxRankSpanActualDiaMes { get; set; }
        public int MinSpanTiDiaMes { get; set; }
        public List<AgrupadorInfoQuery> ListRankSpansDiaAnio { get; set; }
        public int RankSpanActualDiaAnio { get; set; }
        public int MaxRankSpanActualDiaAnio { get; set; }
        public int MinSpanDiaAnio { get; set; }
        public List<int> ListData { get; set; }
        public List<int> ListaInfoPivot { get; set; }
        public AgrupadorInfoQuery BinInfoGral { get; set; }
        public AgrupadorInfoQuery BinInfoTabindex { get; set; }
        public AgrupadorInfoQuery BinInfoGrLet { get; set; }
        public bool Indicador { get; set; }
        public List<AgrupadorInfoQuery> ListDataSpans { get; set; }
        public Dictionary<int, int> DicContadoresGen { get; set;}
        public Dictionary<int, int> DicContadoresDiaSem { get; set; }
        public int ValRefGen { get; set; }
        public int ValRefGlGen { get; set; }
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
        public int EqPositivos { get; set; }
        public int EqNegativos { get; set; }
        public double EqAvgPos { get { return EqPositivos + EqNegativos != 0 ? (EqPositivos * 100000) / (EqPositivos + EqNegativos) : 0; } }
        public double EqAvgNeg { get { return EqPositivos + EqNegativos != 0 ? (EqNegativos * 100000) / (EqPositivos + EqNegativos) : 0; } }
        public double EqAvgPosVal { get { return EqPositivos + EqNegativos != 0 ? EqPositivos * 100 / (EqPositivos + EqNegativos) : 0; } }
        public override string ToString()
        {
            return Positivos + ";" + Negativos + ";" + AvgPos + ";" + AvgNeg + "--" + EqPositivos + ";" + EqNegativos + ";" + EqAvgPos + ";" + EqAvgNeg; ;
        }
    }

    public class InfoReviewDTO
    {
        public int MaxTabindex { get; set; }
        public List<FLASHORDERED> LstResults { get; set; }
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

    public class InfoAnalisisDTO2
    {
        public int Id { get; set; }
        public int P { get; set; }
        public int N { get; set; }
        public double AP { get { return P + N != 0 ? (P * 100000) / (P + N) : 0; } }
        public double AN { get { return P + N != 0 ? (N * 100000) / (P + N) : 0; } }
        public double APVal { get { return P + N != 0 ? P * 100 / (P + N) : 0; } }
        public int EqP { get; set; }
        public int EqN { get; set; }
        public double EqAP { get { return EqP + EqN != 0 ? (EqP * 100000) / (EqP + EqN) : 0; } }
        public double EqAN { get { return EqP + EqN != 0 ? (EqN * 100000) / (EqP + EqN) : 0; } }
        public double EqAPVal { get { return EqP + EqN != 0 ? EqP * 100 / (EqP + EqN) : 0; } }
        public override string ToString()
        {
            return "{\"Id\":\""+Id+"\",\"P\":\""+P+"\",\"N\":\""+N+ "\",\"EqP\":\""+ EqP+ "\",\"EqN\":\""+EqN+"\"}";
        }
    }

    public class InfoAnalisisDTO3
    {
        public int Id { get; set; }
        public List<InfoAnalisisDTO2> Lst { get; set; }
        public override string ToString()
        {
            return "{\"Id\":\"" + Id + "\",\"lst\":["+ string.Join(",", Lst) +"]}";
        }
    }

    public class InfoAnalisisDTO4
    {
        public int Total { get; set; }
        public int Tipo { get; set; }
        public int IndexRank { get; set; }
        public double Prob { get; set; }
        public int Rank { get; set; }
    }

    public class InfoAnalisisDTO5
    {
        public int Tipo { get; set; }
        public InfoAnalisisDTO Value { get; set; }
        public int Fechanum { get; set; }

        public InfoAnalisisDTO5(int tipo, int fechanum)
        {
            this.Tipo = tipo;
            this.Fechanum = fechanum;
            this.Value = new InfoAnalisisDTO();
        }
    }

    public class AgrupadorInfoQuery2
    {
        public int Ghome { get; set; }
        public int Gaway { get; set; }
        public int Totalg { get; set; }
        public int Tabindex { get; set; }
        public int Tabindexletter { get; set; }
        public string Groupletter { get; set; }
    }
}