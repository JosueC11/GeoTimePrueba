using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cEmpleado
    {
        public string? IdNumero { get; set; }
        public string? IdPlanilla { get; set; }
        public string? Nombre { get; set; }
        public string? Tarjeta { get; set; }
        public string? Identificacion { get; set; }
        public string? IdDepartamento { get; set; }
        public char Estado { get; set; }
        public string IdCCosto { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public string Email { get; set; }
        public int IdGrupo { get; set; }
        public int IdHorario { get; set; }
        public int IdAgrupamiento { get; set; }
        public string Tipo_Marca { get; set; }
        public string global_clave { get; set; }

    }
}
