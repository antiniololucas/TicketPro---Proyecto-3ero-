using BE;

namespace Services
{
    public interface IPublisher
    {
        void AddObserver(IObserver observer);
        void RemoveAllObservers();
        void NotifyAll(EntityIdioma idioma);
    }
}
