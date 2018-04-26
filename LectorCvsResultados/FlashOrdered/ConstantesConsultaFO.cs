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
            "SELECT COALESCE(MAX(" + ConstantesModel.ID + "), 0)"
            + "FROM " + ConstantesModel.FLASHORDERED+" ";

        public const string QUERY_ULTIMOS_SPAN =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS fechanum, " + ConstantesModel.TABINDEX + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "AND " + ConstantesModel.FECHANUM + "    < {1} {3}"
            + "GROUP BY " + ConstantesModel.TABINDEX + ") "
            + "SELECT f.{2} AS spantiempo, f." + ConstantesModel.TABINDEX + " AS " + ConstantesModel.TABINDEX + ", f." + ConstantesModel.FECHANUM + " AS fechanum "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON r." + ConstantesModel.TABINDEX + "  = f." + ConstantesModel.TABINDEX
            + " AND r.fechanum = f." + ConstantesModel.FECHANUM + "";

        public const string QUERY_ULTIMOS_SPAN_TB_LETTER =
            "WITH registros AS "
            + "(SELECT MAX(" + ConstantesModel.FECHANUM + ") AS " + ConstantesModel.FECHANUM + ", " + ConstantesModel.GROUPLETTER + ", " + ConstantesModel.TABINDEXLETTER + " "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
            + "AND {1} {3} "
            + "GROUP BY " + ConstantesModel.GROUPLETTER + "," + ConstantesModel.TABINDEXLETTER + ") "
            + "SELECT f.{2}, f." + ConstantesModel.FECHANUM + " AS fechanum, f." + ConstantesModel.GROUPLETTER + " AS GroupLetter, f." + ConstantesModel.TABINDEXLETTER + " AS TabindexLetter "
            + "FROM " + ConstantesModel.FLASHORDERED + " f "
            + "INNER JOIN registros r ON r." + ConstantesModel.GROUPLETTER + " = f." + ConstantesModel.GROUPLETTER + " "
            + "AND r." + ConstantesModel.TABINDEXLETTER + " = f." + ConstantesModel.TABINDEXLETTER + " "
            + "AND r." + ConstantesModel.FECHANUM + " = f." + ConstantesModel.FECHANUM + "";

        public const string QUERY_MAX_INDEX_RESULTADOS =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEX + "), 0)"
            + "FROM " + ConstantesModel.FLASHORDERED + " ";

        public const string QUERY_MAX_INDEX_RESULTADOS_GROUP_LETTER =
            "SELECT COALESCE(MAX(" + ConstantesModel.TABINDEXLETTER + "), 0)"
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
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " < {0} "
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

        public const string QUERY_MAX_TABINDEXSEQ_GEN =
            "SELECT MAX(" + ConstantesModel.TABINDEXSEQ + ") AS total, tabindex AS tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.TABINDEX + " <= {0} "
            + "AND "+ConstantesModel.FECHANUM+" <= {1} "
            + "GROUP BY tabindex";

        public const string QUERY_MAX_TABINDEXSEQ_GL =
            "SELECT MAX(" + ConstantesModel.TABINDEXLETTERSEQ + ") AS total, groupletter AS GroupLetter, tabindexletter AS Tabindex "
            + "FROM " + ConstantesModel.FLASHORDERED + " "
            + "WHERE " + ConstantesModel.FECHANUM + " <= {0} AND {1} "
            + "GROUP BY groupletter, tabindexletter";
    }
}
