using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TireAndProducer.Library.Interfaces;
using TireAndProducer.Library.Models;
using TireAndProducerAPI.ViewModels;

namespace TireAndProducerAPI.Controllers
{
    /// <summary>
    /// Controller responsible for managing tires.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TireController : ControllerBase
    {
        private readonly ITireService _tireService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TireController"/> class.
        /// </summary>
        /// <param name="tireService">Service for managing producers.</param>
        /// <param name="mapper">Mapper for mapping models to view models.</param
        public TireController(ITireService tireService, IMapper mapper)
        {
            _tireService = tireService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all tires.
        /// </summary>
        /// <returns>A list of tires.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var tires = _tireService.GetAll();
            var tiresViewModel = _mapper.Map<List<TireViewModel>>(tires);

            return Ok(tiresViewModel);
        }

        /// <summary>
        /// Retrieves a tire by ID.
        /// </summary>
        /// <param name="id">The ID of the tire to retrieve.</param>
        /// <returns>The tire with the specified ID.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tire = _tireService.GetById(id);
            if (tire == null)
            {
                return NotFound(new { Message = $"Tire with ID {id} was not found." });
            }

            var tireViewModel = _mapper.Map<TireViewModel>(tire);

            return Ok(tireViewModel);
        }

        /// <summary>
        /// Adds a new tire.
        /// </summary>
        /// <param name="viewModel">The tire to add.</param>
        /// <returns>Result of the operation.</returns>
        [HttpPost]
        public IActionResult Add(TireViewModel viewModel)
        {
            var tire = _mapper.Map<Tire>(viewModel);
            _tireService.Add(tire);

            return CreatedAtAction(nameof(GetById), new { id = tire.Id }, viewModel); // 201 Created
        }

        /// <summary>
        /// Edits an existing tire.
        /// </summary>
        /// <param name="id">The ID of the tire to edit.</param>
        /// <param name="viewModel">The updated tire data.</param>
        /// <returns>Result of the operation.</returns>
        [HttpPut("{id}")]
        public IActionResult Edit(int id, TireViewModel viewModel)
        {
            var tire = _tireService.GetById(id);
            if (tire == null)
            {
                return NotFound(new { Message = $"Tire with ID {id} was not found." });
            }

            var newProducer = _mapper.Map<Tire>(viewModel);
            _tireService.Edit(id, newProducer);

            return NoContent();
        }

        /// <summary>
        /// Deletes a tire.
        /// </summary>
        /// <param name="id">The ID of the tire to delete.</param>
        /// <returns>Result of the operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tire = _tireService.GetById(id);
            if (tire == null) 
            {
                return NotFound(new { Message = $"Tire with ID {id} was not found." });
            }

            _tireService.Remove(id);

            return NoContent();
        }
    }
}
