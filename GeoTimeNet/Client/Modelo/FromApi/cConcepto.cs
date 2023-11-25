using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cConcepto
    {
        public int id { get; set; }
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
        public int tipo_j { get; set; }
        public int tipo_h { get; set; }
        public int columnar { get; set; }
        public string? nominaeq { get; set; }
        public int factor { get; set; }
        public int tolerancia { get; set; }
        public char ordinario { get; set; }
        public char autorizado { get; set; }
        public char? transferir { get; set; }
        public char adicional { get; set; }
        public char? tipo_ext_alm { get; set; }
        public char? muestra_resumen { get; set; }

    }
}
