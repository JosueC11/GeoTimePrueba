namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cPhUsuario
    {
        public int IDUSUARIO { get; set; }
        public string? PLANILLAS { get; set; }
        public int? NIVEL { get; set; }
        public string? GRUPOS { get; set; }
        public char? ESTADO { get; set; }
        public string? FPERIODO { get; set; }
        public string? FPLANILLA { get; set; }
        public string? TURNOS { get; set; }
        public char ORDEN_EMP { get; set; }
        public char TIPO_EDT { get; set; }
        public char FILT_PRGT { get; set; }
        public int? NIVEL_APROB_EXT { get; set; }
    }
}
