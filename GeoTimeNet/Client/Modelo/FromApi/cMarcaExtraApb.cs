namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cMarcaExtraApb
    {
        public long idregistro { get; set; }
        public string idplanilla { get; set; }
        public string idnumero { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string cantidad { get; set; }
        public string? comentario { get; set; }
        public char estado { get; set; }
        public string? usuario { get; set; }
        public string? ccosto { get; set; }
        public char? aprob_nivel1 { get; set; }
        public char? aprob_nivel2 { get; set; }
        public char? aprob_nivel3 { get; set; }
        public string? usuario_aprob_nivel1 { get; set; }
        public string? usuario_aprob_nivel2 { get; set; }
        public string? usuario_aprob_nivel3 { get; set; }
        public string? comentario_aprob_nivel1 { get; set; }
        public string? comentario_aprob_nivel2 { get; set; }
        public string? comentario_aprob_nivel3 { get; set; }
        public DateTime? fecha_aprob_nivel1 { get; set; }
        public DateTime? fecha_aprob_nivel2 { get; set; }
        public DateTime? fecha_aprob_nivel3 { get; set; }
        public string? cantidad_aprob_nivel1 { get; set; }
        public string? cantidad_aprob_nivel2 { get; set; }
        public string? cantidad_aprob_nivel3 { get; set; }

    }
}
