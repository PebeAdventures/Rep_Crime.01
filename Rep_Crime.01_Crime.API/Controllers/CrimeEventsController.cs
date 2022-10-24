using Microsoft.AspNetCore.Mvc;
using Rep_Crime._01_Crime.API.Models;
using Rep_Crime._01_Crime.API.Services;

namespace Rep_Crime._01_Crime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrimeEventsController : ControllerBase
    {
        private readonly CrimeEventService _crimeEventService;

        private readonly ILogger<CrimeEventsController> _logger;

        public CrimeEventsController(ILogger<CrimeEventsController> logger, CrimeEventService crimeEventService)
        {
            _logger = logger;
            _crimeEventService = crimeEventService;
        }

        [HttpGet]
        [Route("/getAll)")]
        public async Task<List<CrimeEvent?>> GetAllCrimeEvents() =>
        await _crimeEventService.GetAllEvents();

        [HttpGet]
        [Route("/getById)")]
        public async Task<ActionResult<CrimeEvent?>> GetCrimeEventById(string id)
        {
            var crimeEvent = await _crimeEventService.GetCrimeEventById(id);

            if (crimeEvent is null)
            {
                return NotFound("A record with the specified ID was not found: " + id);
            }

            return crimeEvent;
        }

        [HttpPost]
        [Route("/addNewCrimeEvent)")]
        public async Task<IActionResult> Post(CrimeEvent crimeEvent)
        {
            await _crimeEventService.CreateEventAsync(crimeEvent);

            return CreatedAtAction(nameof(GetCrimeEventById), new { id = crimeEvent.Id }, crimeEvent);
        }
        [HttpPut]
        [Route("/updateCrimeEventStatus)")]
        public async Task<IActionResult> Update(string id, EventStatus eventStatus)
        {
            var crimeEvent = await _crimeEventService.GetCrimeEventById(id);

            if (crimeEvent is null)
            {
                return NotFound("A record with the specified ID was not found: " + id);
            }
            if (eventStatus == crimeEvent.EventStatus)
            {
                return BadRequest("The record already contains the currently given status");
            }

            crimeEvent.EventStatus = eventStatus;

            await _crimeEventService.UpdateCrimeEventStatus(id, crimeEvent);

            return NoContent();
        }

        [HttpDelete]
        [Route("/deleteCrimeEventById)")]
        public async Task<IActionResult> Delete(string id)
        {
            var crimeEvent = await _crimeEventService.GetCrimeEventById(id);

            if (crimeEvent is null)
            {
                return NotFound("A record with the specified ID was not found: " + id);
            }

            await _crimeEventService.RemoveAsync(id);

            return NoContent();
        }
    }
}