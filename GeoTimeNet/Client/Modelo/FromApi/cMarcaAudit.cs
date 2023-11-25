namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cMarcaAudit
    {
        public long IDREGISTRO { get; set; }
        public string IDPLANILLA { get; set; }
        public string IDNUMERO { get; set; }
        public DateTime FECHA { get; set; }
        public string? USUARIO { get; set; }
        public DateTime? FECHA_ORIG { get; set; }
        public DateTime? FECHA_CHG { get; set; }
        public string? HORA_ORIG { get; set; }
        public string? HORA_CHG { get; set; }
        public string? COMENTARIO { get; set; }
    }
}
