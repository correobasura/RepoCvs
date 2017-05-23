﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    public class AgrupadorTotalTabIndexDTO
    {
        public int Total
        {
            get;set;
        }

        public int Tabindex
        {
            get;set;
        }

        public int Lineindex
        {
            get; set;
        }
    }

    public class AgrupadorTabIndexDiferenciaDTO
    {
        public int Tabindex
        {
            get; set;
        }

        public int Diferencia
        {
            get; set;
        }
    }

    public class AgrupadorFechaNumTabindex
    {
        public int Tabindex
        {
            get; set;
        }

        public int FechaNum
        {
            get; set;
        }
    }

    public class AnalisisDatosDTO
    {
        public int TotalDatos
        {
            get;set;
        }

        public int ResultadosPositivos
        {
            get;set;
        }

        public int ResultadosNegativos
        {
            get;set;
        }

        public double PromedioPositivo
        {
            get;set;
        }

        public double PromedioNegativo
        {
            get;set;
        }

        public int MinimoApariciones
        {
            get; set;
        }

        public string Fecha
        {
            get;set;
        }

        public string UltimoParecido
        {
            get;
            set;
        }

        public string AnalizedData
        {
            get;
            set;
        }

        public override string ToString()
        {
            return +TotalDatos + ";" + ResultadosPositivos + ";"+ PromedioPositivo + ";"+ ResultadosNegativos+";"+ PromedioNegativo+";"+ MinimoApariciones+";"+ Fecha+";"+ UltimoParecido+";"+AnalizedData;
        }
    }

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

        public int TotalPositivosMuestras
        {
            get;
            set;
        }

        public int Porcentaje
        {
            get;
            set;
        }

        public override string ToString()
        {
            return TotalPositivosMuestras + ";" + MaxValue + ";" + MinValue+";"+ Porcentaje;
        }
    }

    public class AgrupadorTimeSpanDTO
    {
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

        public int UltimoEnRachas
        {
            get;
            set;
        }

        public Dictionary<int,int> DictRachasAcumuladas
        {
            get;
            set;
        }

        public USERRESULTTABLESFS UltimoGuardado
        {
            get;
            set;
        }
    }

    public class AgrupadorConteosTimeSpanDTO
    {
        public int Total
        {
            get;
            set;
        }

        public int? Spantiempo
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
    }
    }
