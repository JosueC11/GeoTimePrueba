namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cMarcaIncidencia
    {
        public long INDICE { get; set; }
        public string IDPLANILLA { get; set; }
        public string IDNUMERO { get; set; }
        public DateTime FECHA { get; set; }
        public int IDINCIDENCIA { get; set; }
        public long IDREGISTRO { get; set; }
        public string HENTRA { get; set; }
        public string HSALE { get; set; }
        public char? EST_P { get; set; }
        public string? COMENTARIO { get; set; }
        public int? INCIDENCIA_JUST { get; set; }
        public char ESTADO { get; set; }
        public string? C_TIEMPO { get; set; }
        public string? USUARIO { get; set; }
        public DateTime? FECHA_JUST { get; set; }
        public long? IDACC { get; set; }
    }
}
