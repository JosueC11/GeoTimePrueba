namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cMarcaProceso
    {
        public long idregistro { get; set; }
        public string idplanilla { get; set; }
        public string idnumero { get; set; }
        public DateTime fecha_entra { get; set; }
        public DateTime fecha_sale { get; set; }
        public string hora_entra { get; set; }
        public string hora_sale { get; set; }
        public int? idturno { get; set; }
        public string? ORDC { get; set; }
        public string? EXTC { get; set; }
        public string? ORDT { get; set; }
        public string? EXTT { get; set; }
        public string? TC1 { get; set; }
        public string? TC2 { get; set; }
        public string? TC3 { get; set; }
        public string? TC4 { get; set; }
        public string? TC5 { get; set; }
        public int? CON_1 { get; set; }
        public int? CON_2 { get; set; }
        public int? CON_3 { get; set; }
        public int? CON_4 { get; set; }
        public int? CON_5 { get; set; }
        public string? ID1 { get; set; }
        public string? ID2 { get; set; }
        public string? ID3 { get; set; }
        public string? FD1 { get; set; }
        public string? FD2 { get; set; }
        public string? FD3 { get; set; }
        public string? TOD { get; set; }
        public string? TED { get; set; }
        public string? TDD { get; set; }
        public string? TIEMPO_CALC_JORN { get; set; }
        public string? TDC { get; set; }
        public char estado { get; set; }
        public char? estado_inc { get; set; }
        public string? manticipo { get; set; }
        public string? mtardia { get; set; }
        public char proyectado { get; set; }
        public long? reg_sale { get; set; }
    }
}
