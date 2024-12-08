using TireAndProducer.Library.Models;
using TireAndProducer.Library.Servives;

namespace TireAndProducerTests
{
    public class ProducerServiceUnitTests
    {
        private readonly ProducerService _producerService;
        private readonly TireService _tireService;

        public ProducerServiceUnitTests()
        {
            _tireService = new TireService();
            _producerService = new ProducerService(_tireService);

            var prod1 = new Producer() { Name = "Producent ABC", Class = "premium" };
            var prod2 = new Producer() { Name = "Producent XYZ", Class = "premium" };
            _producerService.Add(prod1);
            _producerService.Add(prod2);

            var tire1 = new Tire() { Size = "16", ProducerId = prod1.Id, TreadName = "AllTerrainPlus" };
            var tire2 = new Tire() { Size = "17", ProducerId = prod1.Id, TreadName = "EcoSaver" };
            var tire3 = new Tire() { Size = "17", ProducerId = prod2.Id, TreadName = "WinterGrip" };
            _tireService.Add(tire1);
            _tireService.Add(tire2);
            _tireService.Add(tire3);
        }

        [Fact]
        public void GetAll_ShouldReturnAllProducers()
        {
            var allProducers = _producerService.GetAll();

            Assert.Equal(2, allProducers.Count());
            Assert.Equal("Producent ABC", allProducers[0].Name);
            Assert.Equal("Producent XYZ", allProducers[1].Name);
            Assert.Equal(1, allProducers[0].Id);
            Assert.Equal(2, allProducers[1].Id);
        }

        [Fact]
        public void Add_ShouldAddProducer()
        {
            var newProducer = new Producer() { Name = "Producent QWE" };

            _producerService.Add(newProducer);
            var allProducers = _producerService.GetAll();

            Assert.Equal(3, allProducers.Count());
            Assert.Equal("Producent QWE", allProducers[2].Name);
        }


        [Fact]
        public void Edit_ShouldEditProducer()
        {
            var producerToEdit =_producerService.GetById(1);

            producerToEdit.Name = "New Producer";
            _producerService.Edit(producerToEdit.Id, producerToEdit);

            var producerWdited = _producerService.GetById(1);

            Assert.Equal("New Producer", producerWdited.Name);
        }

        [Fact]
        public void Delete_ShouldRemoveProducer() 
        {
            var producersBeforeRemove = _producerService.GetAll().Count();
            _producerService.Remove(1);

            var producersAfterRemove = _producerService.GetAll().Count();

            Assert.Equal(producersBeforeRemove, producersAfterRemove + 1);
        }

        [Fact]
        public void Delete_ShouldRemoveTiresOfProducer()
        {
            var tiresBeforeRemove = _tireService.GetAll().Count();
            _producerService.Remove(1);

            var tiresAfterRemove = _tireService.GetAll().Count();

            Assert.Equal(tiresBeforeRemove, tiresAfterRemove + 2);
        }
    }
}
