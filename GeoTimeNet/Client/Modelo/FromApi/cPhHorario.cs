namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cPhHorario
    {
        public int IDHORARIO { get; set; }
        public string? DESCRIPCION { get; set; }

        public IEnumerable<cPhHorarioTurno>? cPh_HorarioTurno { get; set; }
    }
}
