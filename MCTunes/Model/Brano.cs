using System;
using System.Linq;
using System.Threading.Tasks;

namespace MCTunes.Model
{
    public class Brano
    {
        public string Titolo { get; set; }
        public decimal Durata { get; set; }
        public int Anno { get; set; }
        public int Album_Id { get; set; }
        public int Id { get; set; }
    }
}
