namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cMarcaMovTurno
    {
        public long idregistro { get; set; }
        public string idplanilla { get; set; }
        public string idnumero { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public int turno { get; set; }
        public char estado { get; set; }
        public string? usuario { get; set; }
        public DateTime? fecha_reg { get; set; }
    }
}
