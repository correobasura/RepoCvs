﻿namespace LectorCvsResultados
{
    public class ConstantesConsulta
    {
        public const string QUERY_ACUMULADO_DATOS_IGUALDAD_DIA_MES =
                    "SELECT COUNT(1)/fn_cantidad_reg_index_diaS(tabindex,{0},{2}) AS total, tabindex AS tabindex "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag = 0 "
                    + "AND diasemnum = {0} "
                    + "AND tabindex <= {1} "
                    + "AND FECHANUM < {2} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_COUNT_SPANTIEMPOS =
                    "SELECT COUNT(1) AS total, {2} AS spantiempo, RANK () OVER (ORDER BY COUNT(1) DESC) AS rank "
                    + "FROM userresulttablesfs "
                    + "WHERE tabindex = {0} "
                    + "AND fechanum < {1} "
                    + "AND {2} IS NOT NULL "
                    + "{3} "
                    + "GROUP BY {2} "
                    + "ORDER BY 3";

        public const string QUERY_COUNT_SPANTIEMPOS_DIA_SEM =
                    "SELECT COUNT(1) AS total, {1} AS spantiempo, RANK () OVER (ORDER BY COUNT(1) DESC) AS rank "
                    + "FROM userresulttablesfs "
                    + "WHERE fechanum < {0} "
                    + "AND {1} IS NOT NULL "
                    + "{2} "
                    + "GROUP BY {1} "
                    + "ORDER BY 3";

        public const string QUERY_MAX_FECHA_TABINDEX =
                    "SELECT MAX(fechaNum) AS FechaNum, tabindex AS tabindex "
                    + "FROM Userresulttablesfs "
                    + "WHERE tabindex <= {0} "
                    + "{1} "
                    + "GROUP BY tabindex ORDER BY 1";

        public const string QUERY_MAX_INDEX_FECHA =
                    "SELECT MAX(tabindex) "
                    + "FROM Userresulttablesfs "
                    + "WHERE fechaNum = {0}";

        public const string QUERY_MAX_INDEX_RESULTADOS =
                    "SELECT MAX(tabindex) "
                    + "FROM Userresulttablesfs ";

        public const string QUERY_RESULTADOS_DIA =
                    "SELECT tabindex AS TabIndex, Diferenciag AS Diferencia "
                    + "FROM userResultTablesFs "
                    + "WHERE fechaNum = {0} "
                    + "ORDER BY tabindex";

        public const string QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_PROM =
                    "SELECT COUNT(1)/ MAX(tabindexseq) AS total, tabindex AS tabindex, MAX(tabindexseq) AS apariciones "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag != 0 "
                    + "AND fechanum < {0} "
                    + "AND tabindex <= {1} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_SELECCION_ORDENADA_MAS_VALORES_DIASEM =
                    "SELECT COUNT(1)/ MAX(tabindexseq) AS total, tabindex AS tabindex, MAX(tabindexseq) AS apariciones "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag != 0 "
                    + "AND fechanum < {0} "
                    + "AND tabindex <= {1} "
                    + "AND diasemnum = {2} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_CONTEO_VALORES_DIA_SEMANA_DIAMES =
                    "SELECT SUM(a.total) AS total, a.tabindex AS tabindex, SUM(a.apariciones) AS apariciones FROM "
                    + "(SELECT COUNT(1)/ MAX(tabindexseq) AS total, tabindex AS tabindex, MAX(tabindexseq) AS apariciones "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag != 0 "
                    + "AND fechanum < {0} "
                    + "AND tabindex <= {1} "
                    + "AND diasemnum = {2} "
                    + "GROUP BY tabindex "
                    + "UNION ALL "
                    + "SELECT COUNT(1)/ MAX(tabindexseq) AS total, tabindex AS tabindex, MAX(tabindexseq) AS apariciones "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag != 0 "
                    + "AND fechanum < {0} "
                    + "AND tabindex <= {1} "
                    + "AND diamesnum = {3} "
                    + "GROUP BY tabindex ) a "
                    + "GROUP BY a.tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_ULTIMO_SPAN_TIEMPO =
                    "SELECT spantiempo, fechanum "
                    + "FROM userresulttablesfs "
                    + "WHERE tabindex = {0} "
                    + "AND fechanum < {1} "
                    + "ORDER BY fechanum DESC";

        public const string QUERY_DATOS_IGUALDAD_DIA =
                    "SELECT COUNT(1)/fn_cantidad_reg_index_diaS(tabindex,{0},{2}) AS total, tabindex AS tabindex "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag = 0 "
                    + "AND diasemnum = {0} "
                    + "AND tabindex <= {1} "
                    + "AND FECHANUM < {2} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_ULTIMO_SPAN =
                    "SELECT {2} AS Spantiempo, fechanum AS FechaNum "
                    + "FROM userresulttablesfs "
                    + "WHERE tabindex = {0} "
                    + "AND fechanum = (SELECT MAX(fechanum) "
                    + "FROM userresulttablesfs "
                    + "WHERE tabindex = {0} "
                    + "AND fechanum < {1} "
                    + "{3}) {3}";

        public const string QUERY_NEXT_TABINDEX_SEQ =
                    "SELECT COALESCE(MAX(tabindexseq),0) "
                    + "FROM Userresulttablesfs "
                    + "WHERE tabindex = {0}";

        public const string QUERY_COUNT_SPANTIEMPOS_DIASEM =
                    "SELECT COUNT(1) AS total, spantiempo AS spantiempo, RANK () OVER (ORDER BY COUNT(1) DESC) AS rank "
                    + "FROM userresulttablesfs "
                    + "WHERE diasemnum = {0} "
                    + "AND fechanum < {1} "
                    + "AND spantiempo IS NOT NULL "
                    + "GROUP BY spantiempo "
                    + "ORDER BY 3";

        public const string QUERY_COUNT_IGUALDADES_TABINDEX_DIA =
                    "SELECT COUNT(1)/fn_cantidad_reg_index_dias({0},{1},{2}) "
                    + "FROM userresulttablesfs "
                    + "WHERE tabindex = {0} "
                    + "AND fechanum < {2} "
                    + "AND diasemnum = {1} "
                    + "AND diferenciag = 0";

        public const string QUERY_COUNT_LIST_INDEX =
                    "SELECT COUNT(1) AS Total, lineindex AS Lineindex "
                    + "FROM analistindexung "
                    + "WHERE result = 1 "
                    + "AND fechanum < {0} "
                    + "AND diasemnum = {1} "
                    + "GROUP BY lineindex "
                    + "ORDER BY 1 desc, 2";

        public const string QUERY_PERCENT_SPAN =
            "SELECT b.cuenta/a.cuenta AS Total, a.timespan AS Span, RANK () OVER (ORDER BY b.cuenta/a.cuenta DESC) AS rank "
            + "FROM "
            + "(SELECT count(1) AS cuenta, ULTIMOTIMESPAN AS timespan "
            + "FROM ANALISTINDEXUNG "
            + "WHERE fechanum < {0} "
            + "{1}"
            + "GROUP BY ULTIMOTIMESPAN) a, "
            + "(SELECT count(1) AS cuenta, ULTIMOTIMESPAN AS timespan "
            + "FROM ANALISTINDEXUNG "
            + "WHERE fechanum < {0} "
            + "AND result = -1 "
            + "{1} "
            + "GROUP BY ULTIMOTIMESPAN) b "
            + "WHERE a.timespan = b.timespan "
            + "ORDER BY 1 DESC";

        public const string QUERY_COUNT_GRP_TAB_TOTAL =
        "SELECT COUNT(CASE WHEN diferenciag = 0 THEN 1 END)/COUNT(1) AS PROB, "
+ "COUNT(CASE WHEN diferenciag = 0 THEN 1 END) AS CERO, "
+ "COUNT(1) AS TOTAL, groupletter, tabindexletter "
+ "FROM flashordered "
+ "WHERE fechanum IN ("
+ "SELECT fechanum "
+ "FROM totalesdiagroptab "
+ "WHERE groupletter = {0} AND total = {1}) "
+ "AND groupletter = {0} "
+ "GROUP BY groupletter, tabindexletter";
    }
}