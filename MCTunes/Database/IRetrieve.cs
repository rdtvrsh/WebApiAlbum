using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCTunes.Database
{
    public interface IRetrieve<T>
    {
        T Get(int id);
        IEnumerable<T> GetAllByBand(int idband);
    }
}
