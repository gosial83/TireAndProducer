using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TireAndProducer.Library.Interfaces;
using TireAndProducer.Library.Models;
using TireAndProducerAPI.ViewModels;

namespace TireAndProducerAPI.Controllers
{
    /// <summary>
    /// Controller responsible for managing producers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _producerService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerController"/> class.
        /// </summary>
        /// <param name="producerService">Service for managing producers.</param>
        /// <param name="mapper">Mapper for mapping models to view models.</param>
        public ProducerController(IProducerService producerService, IMapper mapper)
        {
            _producerService = producerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all producers.
        /// </summary>
        /// <returns>A list of producers.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var producers = _producerService.GetAll();
            var producersViewModel = _mapper.Map<List<ProducerViewModel>>(producers);

            return Ok(producersViewModel);
        }

        /// <summary>
        /// Retrieves a producer by ID.
        /// </summary>
        /// <param name="id">The ID of the producer to retrieve.</param>
        /// <returns>The producer with the specified ID.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var producer = _producerService.GetById(id);
            if (producer == null) 
            {
                return NotFound(new { Message = $"Producer with ID {id} was not found." });
            }

            var producerViewModel = _mapper.Map<ProducerViewModel>(producer);

            return Ok(producerViewModel);
        }

        /// <summary>
        /// Adds a new producer.
        /// </summary>
        /// <param name="viewModel">The producer to add.</param>
        /// <returns>Result of the operation.</returns>
        [HttpPost]
        public IActionResult Add(ProducerViewModel viewModel)
        {
            var producer = _mapper.Map<Producer>(viewModel);
            _producerService.Add(producer);

            return CreatedAtAction(nameof(GetById), new { id = producer.Id }, viewModel); // 201 Created
        }

        /// <summary>
        /// Edits an existing producer.
        /// </summary>
        /// <param name="id">The ID of the producer to edit.</param>
        /// <param name="viewModel">The updated producer data.</param>
        /// <returns>Result of the operation.</returns>
        [HttpPut("{id}")]
        public IActionResult Edit(int id, ProducerViewModel viewModel)
        {
            var producer = _producerService.GetById(id);
            if (producer == null)
            {
                return NotFound(new { Message = $"Producer with ID {id} was not found." });
            }

            var newProducer = _mapper.Map<Producer>(viewModel);
            _producerService.Edit(id, newProducer);

            var producerViewModel = _mapper.Map<ProducerViewModel>(newProducer);

            return Ok(producerViewModel);
        }

        /// <summary>
        /// Deletes a producer.
        /// </summary>
        /// <param name="id">The ID of the producer to delete.</param>
        /// <returns>Result of the operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producer = _producerService.GetById(id);
            if (producer == null)
            {
                return NotFound(new { Message = $"Producer with ID {id} was not found." });
            }

            _producerService.Remove(id);

            return NoContent();
        }
    }
}
