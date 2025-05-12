namespace SYSVETE.Models
{
    public class Tarjeta
    {
        public int NúmeroTarjeta  { get; set; } 
        public string Banco { get; set; } = null;
        public string Titular { get; set; }
    }
}
