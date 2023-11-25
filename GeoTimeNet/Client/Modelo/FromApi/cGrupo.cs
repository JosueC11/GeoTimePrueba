using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace GeotimeNet.Client.Modelo.FromApi
{
    public class cGrupo
    {
        public int idgrupo { get; set; }
        public string descripcion { get; set; }
        public string? idcomp { get; set; }
        public string? idplanilla { get; set; }
        public char? estado { get; set; }
        public int idagrupamiento { get; set; }
        public string turno_continuo { get; set; }

    }
}
