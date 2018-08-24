namespace LectorCvsResultados
{
    public class AnDataMayorDos
    {
        //public static void AnalizarDiaAnteriorMayorDos(DateTime fechaRevisar, SisResultEntities contexto)
        //{
        //    string fechaFormat = fechaRevisar.ToString("dd/MM/yyyy");
        //    int diaSemana = (int)fechaRevisar.DayOfWeek == 0 ? 7 : (int)fechaRevisar.DayOfWeek;
        //    int maxIndex = ConsultasClass.ConsultarMaxIndex(fechaFormat, contexto);
        //    List<ConsultaDTO> listaObtenida = ConsultasClass.ConsultarListaMayorDos(maxIndex, fechaFormat, diaSemana, contexto);
        //    List<int> listaTabIndexDiCero = ConsultasClass.ConsultarIndexTotalMenoIgualDos(fechaFormat, contexto);
        //    List<int> listaTabIndex = ConsultasClass.ConsultarTabIndexFecha(fechaFormat, contexto);
        //    for (int j = 0; j < listaObtenida.Count; j++)
        //    {
        //        var elemento = listaObtenida.ElementAt(j);
        //        ANALISISLISTINDEXMAYORUNO a = new ANALISISLISTINDEXMAYORUNO();
        //        a.ID = ConsultasClass.ObtenerValorSecuencia(a, contexto);
        //        a.LINEINDEX = j + 1;
        //        a.FECHA = fechaRevisar;
        //        if (listaTabIndex.IndexOf(elemento.Tabindex) == -1)
        //        {
        //            a.RESULT = 0;
        //        }
        //        else if (listaTabIndexDiCero.IndexOf(elemento.Tabindex) == -1)
        //        {
        //            a.RESULT = 1;
        //        }
        //        else
        //        {
        //            a.RESULT = -1;
        //        }
        //        contexto.ANALISISLISTINDEXMAYORUNO.Add(a);
        //    }
        //    contexto.SaveChanges();
        //}

        //public static List<AgrupadorDictionaryDTO> SeleccionarValoresMayorUno(string rutaBase, DateTime fechaRevisar, int maxIndex, SisResultEntities contexto)
        //{
        //    string fechaFormat = fechaRevisar.ToString("dd/MM/yyyy");
        //    int diaSemana = (int)fechaRevisar.DayOfWeek == 0 ? 7 : (int)fechaRevisar.DayOfWeek;
        //    /*Cambiar por maxindex del día*/
        //    List<ConsultaDTO> listaObtenida = ConsultasClass.ConsultarListaMayorDos(maxIndex, fechaFormat, diaSemana, contexto);
        //    List<AgrupadorDictionaryDTO> listaSeleccionar = ConsultasClass.ConsultarDatosSeleccionarMayorUno(listaObtenida.Count, fechaFormat, diaSemana, contexto);
        //    List<AgrupadorDictionaryDTO> listaSeleccionadosConsultaOrdenada = new List<AgrupadorDictionaryDTO>();
        //    foreach (var item in listaSeleccionar)
        //    {
        //        AgrupadorDictionaryDTO a = new AgrupadorDictionaryDTO();
        //        var elementoListaObtenida = listaObtenida.ElementAt(item.ListLineindex - 1);
        //        a.SumPorcentaje = elementoListaObtenida.SumPorcentaje;
        //        a.Tabindex = elementoListaObtenida.Tabindex;
        //        a.SumPorcentajeLine = item.SumPorcentaje;
        //        a.ListLineindex = item.ListLineindex;
        //        listaSeleccionadosConsultaOrdenada.Add(a);
        //    }
        //    return listaSeleccionadosConsultaOrdenada;
        //    //EscribirDatosArchivo(listaSeleccionadosConsultaOrdenada, fechaRevisar.ToString("yyyyMMdd") + "AnaSelUnGanador", rutaBase);
        //}
    }
}