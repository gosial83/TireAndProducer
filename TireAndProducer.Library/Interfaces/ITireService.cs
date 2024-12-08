using TireAndProducer.Library.Models;

namespace TireAndProducer.Library.Interfaces
{
    public interface ITireService
    {
        void Add(Tire tire);
        void Remove(int id);
        void Edit(int id, Tire newTire);
        List<Tire> GetAll();
        Tire GetById(int id);
    }
}
