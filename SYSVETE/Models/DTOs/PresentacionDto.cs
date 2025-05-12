namespace SYSVETE.Models.DTOs
{
    public class PresentacionDto
    {
        public int IdPresentacion { get; set; }
        public int? IdUnidad { get; set; }
        public decimal CantidadPresentacion { get; set; }
        public string Descripcion { get; set; } = null;
        public bool Activo { get; set; }
        public bool Borrado { get; set; }

    }
}
