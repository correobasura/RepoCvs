using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int? DiaSemNum
        {
            get;
            set;
        }
        public int? Fechanum
        {
            get;
            set;
        }

        public int Rank
        {
            get;
            set;
        }

        public int? Spantiempo
        {
            get;
            set;
        }

        public int Total
        {
            get;
            set;
        }
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
            get;set;
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

        public USERRESULTTABLESFS UltimoGuardado
        {
            get;
            set;
        }

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
        public int Lineindex
        {
            get; set;
        }

        public int Tabindex
        {
            get; set;
        }

        public double Total
        {
            get; set;
        }

        public int Apariciones
        {
            get; set;
        }

        public string GroupLetter
        {
            get;set;
        }
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

    public class HtmlDTO
    {
        public int IndexOrdered
        {
            get; set;
        }
        public string Hora
        {
            get; set;
        }
        public String Estado
        {
            get; set;
        }
        public String Home
        {
            get; set;
        }
        public String Result
        {
            get; set;
        }
        public String Away
        {
            get; set;
        }
        public String Half
        {
            get; set;
        }
        public int GHome
        {
            get; set;
        }
        public int GAway
        {
            get; set;
        }
        public int DiferenciaG
        {
            get; set;
        }
        public int TotalG
        {
            get; set;
        }
        public char GroupLetter
        {
            get; set;
        }
        public int GroupIndexLetter
        {
            get; set;
        }
        public int DiaSem
        {
            get; set;
        }
        public int DiaMes
        {
            get; set;
        }
        public int DiaAnio
        {
            get; set;
        }
        public int? SpanDiarioActual
        {
            get; set;
        }
        public int SpanDiarioHistorico
        {
            get; set;
        }
        public int? SpanSemanaActual
        {
            get; set;
        }
        public int SpanSemanaHistorico
        {
            get; set;
        }
        public int? SpanMesActual
        {
            get; set;
        }
        public int SpanMesHistorico
        {
            get; set;
        }
        public int SpanAnioActual
        {
            get; set;
        }
        public int SpanAnioHistorico
        {
            get; set;
        }
        public DateTime Fecha
        {
            get; set;
        }
        public override string ToString()
        {
            return IndexOrdered + ";" + Hora + ";" + Estado + ";" + Home + ";" + Result
                + ";" + Away + ";" + Half + ";" + GHome + ";" + GAway + ";" + DiferenciaG + ";" + TotalG
                + ";" + GroupLetter + ";" + GroupIndexLetter + ";"
                + SpanDiarioHistorico + ";" + SpanDiarioActual + ";"
                + DiaSem + ";" + SpanSemanaHistorico + ";" + SpanSemanaActual + ";"
                + DiaMes + ";" + SpanMesHistorico + ";" + SpanMesActual + ";"
                + DiaAnio + ";" + SpanAnioHistorico + ";"
                + Fecha.ToString("yyyyMMdd");
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
        public int Tabindexletter { get; set; }
        public string GroupLetter { get; set; }
        public int Tabindex { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexGen { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexDiaSem { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexDiaMes { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromMaxTabindexDiaAnio { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromGroupTabGen { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromGroupTabDiaSem { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromGroupTabDiaMes { get; set; }
        public AgrupadorTotalTabIndexDTO AgrupadorPromGroupTabDiaAnio { get; set; }
        public int MaxFechaNumGen { get; set; }
        public int MaxFechaNumDiaSem { get; set; }
        public int MaxFechaNumDiaMes { get; set; }
        public int MaxFechaNumDiaAnio { get; set; }
        public int MaxFechaNumGlGen { get; set; }
        public int MaxFechaNumGlDiaSem { get; set; }
        public int MaxFechaNumGlDiaMes { get; set; }
        public int MaxFechaNumGlDiaAnio { get; set; }
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

}
