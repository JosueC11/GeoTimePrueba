namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cPhPlanilla
    {
        public string idplanilla { get; set; }
        public string planilla { get; set; }
        public string? nom_conector { get; set; }
        public char? tipo_planilla { get; set; }
        public char? c_ext { get; set; }
        public char? c_inci { get; set; }
        public char? c_adic { get; set; }
        public char? m_desc { get; set; }
        public char proyecta { get; set; }
        public int? dia_inicio { get; set; }
        public char? auto_proceso { get; set; }
        public char? tipo_dist { get; set; }
        public string? est_nomina { get; set; }
        public char? ext_per_ant { get; set; }
        public char? ext_det { get; set; }
        public char? agrup_salida { get; set; }
        public char? tipo_adic { get; set; }
        public int? nivel_aprob_ext { get; set; }
    }
}
