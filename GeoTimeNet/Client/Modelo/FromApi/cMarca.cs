namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cMarca
    {
        public long registro { get; set; }
        public string? idplanilla { get; set; }
        public string? idnumero { get; set; }
        public DateTime fecha { get; set; }
        public string? hora { get; set; }
        public int tipo { get; set; }
        public string? idterminal { get; set; }
        public string? estado { get; set; }
        public DateTime? fecha_reg { get; set; }
        public string? long_reg { get; set; }
        public string? lat_reg { get; set; }
        public string? imagen_reg { get; set; }
        public char? gps_cell { get; set; }
        public string? img_verif { get; set; }
        public DateTime? fecha_hora { get; set; }
        public string? dir_ip { get; set; }
    }
}
