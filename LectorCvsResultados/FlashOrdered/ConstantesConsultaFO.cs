using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectorCvsResultados.FlashOrdered
{
    public class ConstantesConsultaFO
    {
        public const string QUERY_MAX_ID_ACTUAL =
                    "SELECT MAX(id) "
                    + "FROM flashordered ";

        public const string QUERY_ULTIMO_SPAN =
            "SELECT {2} AS Spantiempo, fechanum AS FechaNum "
            + "FROM flashordered "
            + "WHERE tabindex = {0} "
            + "AND fechanum = (SELECT MAX(fechanum) "
            + "FROM flashordered "
            + "WHERE tabindex = {0} "
            + "AND fechanum < {1} "
            + "{3}) {3}";

        public const string QUERY_MAX_INDEX_RESULTADOS =
            "SELECT MAX(tabindex) "
            + "FROM flashordered ";

        public const string QUERY_MAX_FECHA_TABINDEX =
            "SELECT MAX(fechaNum) AS FechaNum, tabindex AS tabindex "
            + "FROM flashordered "
            + "WHERE tabindex <= {0} "
            + "{1} "
            + "GROUP BY tabindex ORDER BY 1";
    }
}
