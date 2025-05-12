using System.Text.Json.Serialization;

namespace SYSVETE.Models.DTOs
{
    public class ImpuestoDtocs
    {
        public int idImpuesto { get; set; }
        public string Descripcion { get; set; } = null;
        public decimal Valor { get; set; }
   
    }
}
