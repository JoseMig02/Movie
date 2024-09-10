namespace MovieServer.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string Clasificacion { get; set; }
        public bool EnCartelera { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string ImagenUrl { get; set; }
        public string Sinopsis { get; set; }
        public string TrailerUrl { get; set; }
        public string AudioPromocionalUrl { get; set; }

    }
}
