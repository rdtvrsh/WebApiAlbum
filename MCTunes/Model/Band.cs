using System.Collections.Generic;

namespace MCTunes.Model
{
    public class Band
    {
        public string Nome { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Componente> Componenti { get; set; }
        public int Id { get; set; }
    }
}
