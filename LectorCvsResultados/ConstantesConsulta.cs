using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    public class ConstantesConsulta
    {
        public const string QUERY_ACUMULADO_DATOS_IGUALDAD_DIA_MES =
                    "SELECT SUM(a.total) AS total, a.tabindex AS tabindex FROM("
                    + "SELECT COUNT(1)/fn_cantidad_reg_index_diaS(tabindex,{0},{2}) AS total, tabindex AS tabindex "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag = 0 "
                    + "AND diasemnum = {0} "
                    + "AND tabindex <= {1} "
                    + "AND FECHANUM < {2} "
                    + "GROUP BY tabindex "
                    + "UNION ALL "
                    + "SELECT COUNT(1)/fn_cantidad_reg_index_diaM(tabindex,{3},{2}) AS total, tabindex AS tabindex "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag = 0 "
                    + "AND diaMesNum = {3} "
                    + "AND tabindex <= {1} "
                    + "AND FECHANUM < {2} "
                    + "GROUP BY tabindex "
                    + ") a "
                    + "GROUP BY a.tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_COUNT_SPANTIEMPOS =
                    "SELECT COUNT(1) AS total, spantiempo AS spantiempo, RANK () OVER (ORDER BY COUNT(1) DESC) AS rank "
                    + "FROM userresulttablesfs "
                    + "WHERE tabindex = {0} "
                    + "AND fechanum < {1} "
                    + "GROUP BY spantiempo "
                    + "ORDER BY 3";

        public const string QUERY_DATOS_MAS_VALORES_LVL1 = "SELECT COUNT(1) AS total, lineindex AS lineindex "
                    + "FROM analistindexung "
                    + "WHERE result IN (1,0) "
                    + "AND fechaNum < {0} "
                    + "GROUP BY lineindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_DATOS_MAS_VALORES_LVL2 = "SELECT COUNT(1) AS total, lineindex AS lineindex "
                    + "FROM analistindexunglv2 "
                    + "WHERE result IN (1,0) "
                    + "AND fechaNum < {0} "
                    + "GROUP BY lineindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_MAX_FECHA_TABINDEX =
                    "SELECT MAX(fechaNum) AS FechaNum, tabindex AS tabindex "
                    + "FROM Userresulttablesfs "
                    + "WHERE tabindex <= {0} "
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

        public const string QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA =
                    "SELECT COUNT(1) AS total, tabindex AS tabindex "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag != 0 "
                    + "AND fechaNum < {0} "
                    + "AND fn_cantidad_reg_index_fs(tabindex,{0}) > {2} "
                    + "AND tabindex <= {1} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_BETWEEN =
                    "SELECT COUNT(1) AS total, tabindex AS tabindex "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag != 0 "
                    + "AND fechaNum < {0} "
                    + "AND fn_cantidad_reg_index_fs(tabindex,{0}) BETWEEN {2} AND {3} "
                    + "AND tabindex <= {1} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC";

        public const string QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_PROM =
                    "SELECT COUNT(1)/fn_cantidad_reg_index_fs(tabindex,{0}) AS total, tabindex AS tabindex "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag != 0 "
                    + "AND fechaNum < {0} "
                    + "AND tabindex <= {1} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";
        public const string QUERY_ULTIMO_SPAN_TIEMPO =
                    "SELECT spantiempo, fechanum "
                    + "FROM userresulttablesfs "
                    + "WHERE tabindex = {0} "
                    + "AND fechanum < {1} "
                    + "AND spantiempo IS NOT NULL "
                    + "ORDER BY fechanum DESC";
    }
}
