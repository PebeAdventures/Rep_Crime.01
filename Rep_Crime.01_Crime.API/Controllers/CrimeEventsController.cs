using Commons.DTO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Rep_Crime._01_Crime.API.Factories;
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
        [Route("/getAll")]
        public async Task<List<CrimeEvent?>> GetAllCrimeEvents() =>
        await _crimeEventService.GetAllEvents();

        [HttpGet]
        [Route("/getById")]
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
        [Route("/addNewCrimeEvent")]
        public async Task<IActionResult> Post(NewCrimeEventDTO newCrimeEventDTO)
        {
            EventType eventType = CreateEventTypeFromDTO(newCrimeEventDTO.EventType);
            if (eventType == EventType.WRONG_TYPE)
            {
                return BadRequest("Wrong EventType.");
            }

            CrimeEventRequest crimeEventRequest = new CrimeEventRequest(
               eventType,
               newCrimeEventDTO.Description,
               newCrimeEventDTO.PlaceOfEvent,
               newCrimeEventDTO.ReportingPersonEmail,
               EventStatus.WAITING);
            await _crimeEventService.CreateEventAsync(crimeEventRequest);

            return Ok();
        }

        private EventType CreateEventTypeFromDTO(string eventType)
        {
            EventType result;
            if (EventType.TryParse(eventType, out result))
            {
                switch (result)
                {
                    case EventType.INDECENT_BEHAVIOR:
                        return EventType.INDECENT_BEHAVIOR;
                    case EventType.THEFT:
                        return EventType.THEFT;
                    case EventType.ASSAULT:
                        return EventType.ASSAULT;
                    case EventType.CAR_ACCIDENT:
                        return EventType.CAR_ACCIDENT;
                    default:
                        return EventType.WRONG_TYPE;
                }

            }
            else
            {
                return EventType.WRONG_TYPE;
            }
        }

        [HttpPut]
        [Route("/updateCrimeEventStatus")]
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

        [HttpPost]
        [Route("/updateCrimeEventStatusByLawEnforcement")]
        public async Task<IActionResult> UpdateByLawEnforcement(CrimeEventChangeStatusDTO crimeEventChangeStatusDTO)
        {
            EventStatus eventStatus = CreateEventStatusFromDTO(crimeEventChangeStatusDTO);
            if (eventStatus == EventStatus.WRONG_STATUS)
            {
                return BadRequest("Wrong EventStatus.");
            }
            var crimeEvent = await _crimeEventService.GetCrimeEventByPublicId(crimeEventChangeStatusDTO.PublicCrimeId);

            if (crimeEvent is null)
            {
                return NotFound("A record with the specified ID was not found: " + crimeEventChangeStatusDTO.PublicCrimeId);
            }
            if (eventStatus == crimeEvent.EventStatus)
            {
                return BadRequest("The record already contains the currently given status");
            }

            crimeEvent.EventStatus = eventStatus;

            await _crimeEventService.UpdateCrimeEventStatus(crimeEvent.Id, crimeEvent);

            return NoContent();
        }

        private EventStatus CreateEventStatusFromDTO(CrimeEventChangeStatusDTO crimeEventChangeStatusDTO)
        {
            EventStatus result;
            if (EventType.TryParse(crimeEventChangeStatusDTO.CrimeStatus, out result))
            {
                switch (result)
                {
                    case EventStatus.WAITING:
                        return EventStatus.WAITING;
                    case EventStatus.FINISHED:
                        return EventStatus.FINISHED;
                    case EventStatus.DECLINED:
                        return EventStatus.DECLINED;
                    default:
                        return EventStatus.WRONG_STATUS;
                }

            }
            else
            {
                return EventStatus.WRONG_STATUS;
            }
        }

        [HttpDelete]
        [Route("/deleteCrimeEventById")]
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

        [HttpPost]
        [Route("/getCrimeEventDetailsForLawEnforcement")]
        public async Task<IActionResult> GetCrimeEventDetailsForLawEnforcement(CrimeEventDetailsDTO crimeEventDetailsDTO)
        {
            var result = await _crimeEventService.GetCrimeEventForLawEnforcement(crimeEventDetailsDTO);
            return Ok(result);
        }
    }
}