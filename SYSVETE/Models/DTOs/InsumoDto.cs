using System.Text.Json.Serialization;

namespace SYSVETE.Models.DTOs
{
    public class InsumoDto
    {
        public int? IdTipoInsumo { get; set; }
        public int idPresentacion { get; set; }
        public int idImpuesto { get; set; }
        public string codigo { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public bool Activo { get; set; }
        public bool Borrado { get; set; }
        public ImpuestoDtocs? impuesto { get; set; }
        [JsonIgnore]
        public int? Total { get; set; }


    }
}
