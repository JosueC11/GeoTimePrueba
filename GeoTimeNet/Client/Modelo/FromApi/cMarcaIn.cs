namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cMarcaIn
    {
        public string? idtarjeta { get; set; }
        public DateTime? fecha { get; set; }
        public string? hora { get; set; }
        public string? idterminal { get; set; }
        public int? tipo { get; set; }
        public string? idplanilla { get; set; }
        public string? idnumero { get; set; }
        public char? estado { get; set; }
        public DateTime? fecha_reg { get; set; }
        public string? long_reg { get; set; }
        public string? lat_reg { get; set; }
        public byte[]? imagen_reg { get; set; }
        public char? gps_cell { get; set; }
        public string? host { get; set; }
        public string? dir_ip { get; set; }
    }
}
