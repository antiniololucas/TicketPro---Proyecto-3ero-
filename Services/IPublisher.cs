using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPublisher
    {
        void AddObserver(IObserver observer);
        void RemoveAllObservers();
        void NotifyAll(EntityIdioma idioma);
    }
}
