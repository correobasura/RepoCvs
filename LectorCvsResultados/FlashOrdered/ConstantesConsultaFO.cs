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
            "SELECT MAX("+ ConstantesModel.ID +") "
            + "FROM " + ConstantesModel.FLASHORDERED+" ";

        public const string QUERY_ULTIMO_SPAN =
            "SELECT {2} AS Spantiempo, " + ConstantesModel.FECHANUM + " AS FechaNum "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " = {0} "
            + "AND " + ConstantesModel.FECHANUM + " = (SELECT MAX(" + ConstantesModel.FECHANUM + ") "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " = {0} "
            + "AND " + ConstantesModel.FECHANUM + " < {1} "
            + "{3}) {3}";

        public const string QUERY_ULTIMO_SPAN_TB_LETTER =
            "SELECT {2} AS Spantiempo, " + ConstantesModel.FECHANUM + " AS FechaNum "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE tabIndexLetter = {0} {3} "
            + "AND fechanum = (SELECT MAX(" + ConstantesModel.FECHANUM + ") "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE tabIndexLetter = {0} "
            + "AND " + ConstantesModel.FECHANUM + " < {1} "
            + "{3})";

        public const string QUERY_MAX_INDEX_RESULTADOS =
            "SELECT MAX(" + ConstantesModel.TABINDEX + ") "
            + "FROM " + ConstantesModel.FLASHORDERED + " ";

        public const string QUERY_MAX_INDEX_RESULTADOS_GROUP_LETTER =
            "SELECT MAX(" + ConstantesModel.TABINDEXLETTER + ") "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.GROUPLETTER + " = '{0}'";

        public const string QUERY_MAX_FECHA_TABINDEX =
            "SELECT MAX(" + ConstantesModel.FECHANUM + ") AS FechaNum, " + ConstantesModel.TABINDEX + " AS tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "{1} "
            + "GROUP BY " + ConstantesModel.TABINDEX + " ORDER BY 1";

        public const string QUERY_MAX_FECHA_TABINDEX_GL =
            "SELECT MAX(" + ConstantesModel.FECHANUM + ") AS FechaNum, " + ConstantesModel.TABINDEXLETTER + " AS tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE {0} "
            + "AND " + ConstantesModel.TABINDEXLETTER + " <= {1} "
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + " ORDER BY 1";

        public const string QUERY_NEXT_TABINDEX_SEQ =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXSEQ + "),0) "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " = {0}";

        public const string QUERY_NEXT_TABINDEXLETTER_SEQ =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXLETTERSEQ + "),0) "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.GROUPLETTER + " = '{0}' "
            + "AND " + ConstantesModel.TABINDEXLETTER + " = {1}";

        public const string QUERY_MAXFECHAS_TABANDGROUPLETTER =
            "WITH registros AS ( "
            + "SELECT max(" + ConstantesModel.FECHANUM + ") AS fechanum, " + ConstantesModel.TABINDEXLETTER + " AS Tabindexletter, " + ConstantesModel.GROUPLETTER + " AS Groupletter "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE {0} {1} "
            + "GROUP BY " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.GROUPLETTER + " ) "
            + "SELECT f.id AS id, r.* "
            + "FROM " + ConstantesModel.FLASHORDERED + " f, registros r "
            + "WHERE r.fechanum  = f.fechanum "
            + "AND r.tabindexletter = f.tabindexletter "
            + "AND r.groupletter = f.groupletter ";

        public const string QUERY_PROM_RESULTS_INTO_TOTALTABINDEX =
            "SELECT a.cuenta/b.cuenta AS total, a.tabindex, b.cuenta AS Apariciones "
            + "FROM (SELECT COUNT(1) AS cuenta, " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.DIFERENCIAG + " != 0 "
            + "AND " + ConstantesModel.FECHANUM + " < {0} "
            + "AND " + ConstantesModel.TABINDEX + " < = {1} {2} "
            + "GROUP BY " + ConstantesModel.TABINDEX + " "
            + ") a, "
            + "(SELECT COUNT(1) AS cuenta, " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND " + ConstantesModel.TABINDEX + " < = {1} {2} "
            + "GROUP BY " + ConstantesModel.TABINDEX + ") b "
            + "WHERE a.tabindex = b.tabindex";

        public const string QUERY_PROM_RESULTS_INTO_TOTALTABINDEX_GROUPANDTAB =
            "WITH registros AS ( "
            + "SELECT " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + ", " + ConstantesModel.DIFERENCIAG + " "
            + "FROM flAShordered "
            + "WHERE fechanum < {0} "
            + "AND {1} {2}"
            + ") "
            + "SELECT a.cuenta/b.cuenta AS total, a.groupletter AS groupletter, a.tabindexletter AS tabindex, b.cuenta AS Apariciones "
            + "FROM ( "
            + "SELECT count(1) AS cuenta, " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " "
            + "FROM registros "
            + "WHERE diferenciag != 0 "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + ")a, "
            + "(SELECT count(1) AS cuenta, " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " "
            + "FROM registros "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + ")b "
            + "WHERE a.groupletter = b.groupletter "
            + "AND a.tabindexletter = b.tabindexletter";
    }
}
