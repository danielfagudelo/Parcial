namespace Parcial.Web.Shared.Entities
{
    public class Boleta
    {

        public int Id { get; set; }

        public DateTime? FechaUso { get; set; }

        public bool FueUsada { get; set; }

        public string? Porteria { get; set; }

    }
}
