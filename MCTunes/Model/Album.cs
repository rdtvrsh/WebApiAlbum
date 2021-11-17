using System.Collections.Generic;

namespace MCTunes.Model
{
    public class Album
    {
        public string Titolo { get; set; }
        public IList<Brano> Brani { get; set; }
        public int Anno { get; set; }
        public string Genere { get; set; }
        public int Id { get; set; }
        public int Band_Id { get; set; }
        public Album()
        {
            Brani = new List<Brano>(); 
        }
    }
}
