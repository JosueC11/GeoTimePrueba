namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cPhHorarioTurno
    {
        public int IDHORARIO { get; set; }
        public int? ID_DIA { get; set; }
        public int? T_1 { get; set; }
        public int? T_2 { get; set; }
        public int? T_3 { get; set; }
        public int? T_4 { get; set; }
        public int? T_5 { get; set; }

        public cPhHorario? cPh_Horarios { get; set; }
    }
}
