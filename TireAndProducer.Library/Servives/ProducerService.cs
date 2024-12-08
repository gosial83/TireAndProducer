using TireAndProducer.Library.Interfaces;
using TireAndProducer.Library.Models;

namespace TireAndProducer.Library.Servives
{
    public class ProducerService : IProducerService
    {
        private Dictionary<int, Producer> _producers = new Dictionary<int, Producer>();
        private readonly ITireService _tireService;

        public ProducerService(ITireService tireService)
        {
            _tireService = tireService;
        }

        public void Add(Producer producer)
        {
            var nextId = _producers.Count + 1;
            producer.Id = nextId;
            _producers.Add(nextId, producer);
        }

        public void Edit(int id, Producer newProducer)
        {
            _producers[id] = newProducer;
        }

        public List<Producer> GetAll()
        {
            return _producers.Values.ToList();
        }

        public Producer? GetById(int id)
        {
            return _producers.ContainsKey(id) ? _producers[id] : null;
        }

        public void Remove(int id)
        {
            List<Tire> tires = _tireService.GetAll();
            var tiresToRemove = tires.Where(x => x.ProducerId == id);

            foreach (var tire in tiresToRemove) 
            {
                _tireService.Remove(tire.Id);
            }

            _producers.Remove(id);
        }
    }
}
