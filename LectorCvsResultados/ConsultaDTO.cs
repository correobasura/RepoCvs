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

        public int Spantiempo
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

        public int Total
        {
            get;set;
        }

        public int Apariciones
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
            get;set;
        }
        public string UltimoParecido
        {
            get;
            set;
        }
        public override string ToString()
        {
            return +TotalDatos + ";" + ResultadosPositivos + ";"+ PromedioPositivo + ";"+ ResultadosNegativos+";"+ PromedioNegativo+";"+ MinimoApariciones+";"+ Fecha+";"+ UltimoParecido+";"+AnalizedData;
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
            get;set;
        }

        public override string ToString()
        {
            return Result+";"+Lineindex + ";" + Tabindex + ";"+UltimoSpan+";"+ RankUltimoSpanDia + ";" + RankUltimoSpanGral + ";" + TMatch + ";";
        }
    }

    public class HtmlDTO
    {
        public int IndexOrdered
        {
            get;set;
        }
        public string Hora
        {
            get;set;
        }
        public String Estado
        {
            get;set;
        }
        public String Home
        {
            get;set;
        }
        public String Result
        {
            get;set;
        }
        public String Away
        {
            get;set;
        }
        public String Half
        {
            get;set;
        }
    }
}
