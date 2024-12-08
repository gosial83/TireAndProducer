using TireAndProducer.Library.Models;

namespace TireAndProducer.Library.Interfaces
{
    public interface IProducerService
    {
        void Add(Producer producer);
        void Remove(int id);
        void Edit(int id, Producer newProducer);
        List<Producer> GetAll();
        Producer GetById(int id);
    }
}
