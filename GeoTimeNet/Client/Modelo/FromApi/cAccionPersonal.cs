using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cAccionPersonal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 IdRegistro { get; set; }
        public String? IdPlanilla { get; set; }
        public String? IdNumero { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public int IdIncidencia { get; set; }
        public char Estado { get; set; }        
        public int? IdAccion { get; set; }
        public String? Comentario { get; set; }
        public int Dias { get; set; }
        public string? Usuario { get; set; }
        public DateTime Fecha_Just { get; set; }
        public string? Dias_Apl { get; set; }
        public long? SolicitudId { get; set; }

        [NotMapped]
        public string? Nom_Conector { get; set; }


    }
}