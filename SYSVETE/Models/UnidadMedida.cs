using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace SYSVETE.Models
{
    public class UnidadMedida
    {
        public UnidadMedida()
        {
            Presentaciones = new HashSet<Presentacion>();
        }
        public int IdUnidad { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Abreviatura { get; set; } = null!;
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        [JsonIgnore]
        public virtual ICollection<Presentacion> Presentaciones { get; set; }

    }
}
