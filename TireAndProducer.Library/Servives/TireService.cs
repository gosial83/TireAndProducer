using TireAndProducer.Library.Interfaces;
using TireAndProducer.Library.Models;

namespace TireAndProducer.Library.Servives
{
    public class TireService : ITireService
    {
        private Dictionary<int, Tire> _tires = new Dictionary<int, Tire>();

        public void Add(Tire tire)
        {
            var nextId = _tires.Count() + 1;
            tire.Id = nextId;
            _tires.Add(nextId, tire);
        }

        public void Edit(int id, Tire newTire)
        {
            _tires[id] = newTire;
        }

        public List<Tire> GetAll()
        {
            return _tires.Values.ToList();
        }

        public Tire? GetById(int id)
        {
            return _tires.ContainsKey(id) ? _tires[id] : null;
        }

        public void Remove(int id)
        {
            _tires.Remove(id);
        }
    }
}
