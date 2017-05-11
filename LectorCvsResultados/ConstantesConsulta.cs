using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados
{
    public class ConstantesConsulta
    {
        public const string QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA =
                    "SELECT   COUNT(1) AS total, tabindex AS tabindex "
                    + "FROM     userresulttablesfs "
                    + "WHERE    diferenciag != 0 "
                    + "AND      fechaNum < {0} "
                    + "AND      fn_cantidad_reg_index_fs(tabindex,{0}) > {2} "
                    + "AND      tabindex <= {1} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_PROM =
                    "SELECT   COUNT(1)/fn_cantidad_reg_index_fs(tabindex,{0}) AS total, tabindex AS tabindex, {2} "
                    + "FROM     userresulttablesfs "
                    + "WHERE    diferenciag != 0 "
                    + "AND      fechaNum < {0} "
                    + "AND      tabindex <= {1} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";

        public const string QUERY_SELECCION_ORDENADA_MAS_VALORES_FECHA_BETWEEN =
                    "SELECT   COUNT(1) AS total, tabindex AS tabindex "
                    + "FROM     userresulttablesfs "
                    + "WHERE    diferenciag != 0 "
                    + "AND      fechaNum < {0} "
                    + "AND      fn_cantidad_reg_index_fs(tabindex,{0}) BETWEEN {2} AND {3} "
                    + "AND      tabindex <= {1} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC";

        public const string QUERY_MAX_INDEX_FECHA = 
                    "SELECT MAX(tabindex) "
                    + "FROM Userresulttablesfs "
                    + "WHERE fechaNum = {0}";

        public const string QUERY_TODOS_RESULTADOS_DIA = 
                    "SELECT tabindex AS TabIndex, Diferenciag AS Diferencia "
                    + "FROM userResultTablesFs "
                    + "WHERE fechaNum = {0}";

        public const string QUERY_DATOS_IGUALDAD_DIA =
                    "SELECT COUNT(1)/fn_cantidad_reg_index_diaS(tabindex,{0},{2}) AS total, tabindex AS tabindex "
                    + "FROM userresulttablesfs "
                    + "WHERE diferenciag = 0 "
                    + "AND diasemnum = {0} "
                    + "AND tabindex <= {1} "
                    + "AND FECHANUM < {2} "
                    + "GROUP BY tabindex "
                    + "ORDER BY 1 DESC, 2";

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
    }
}
