namespace SYSVETE.Models
{
    public class Presentacion
    {
        public int IdPresentacion { get; set; }
        public int? IdUnidad { get; set; }
        public decimal CantidadPresentacion { get; set; }
        public string Descripcion { get; set; } = null;
        public bool Activo { get; set; }
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public virtual UnidadMedida IdUnidadNavigation { get; set; } = null!;


    }
}
