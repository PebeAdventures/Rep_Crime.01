using Commons.DTO;
using Microsoft.AspNetCore.Mvc;
using Rep_Crime._01_LawEnforcement.API.Factories;
using Rep_Crime._01_LawEnforcement.API.Models;
using Rep_Crime._01_LawEnforcement.API.Models.DTO;
using Rep_Crime._01_LawEnforcement.API.Services.Interface;

namespace Rep_Crime._01_LawEnforcement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LawEnforcementController : ControllerBase
    {

        private readonly ILawEnforcementService _lawEnforcementService;

        public LawEnforcementController(ILawEnforcementService lawEnforcementService)
        {
            _lawEnforcementService = lawEnforcementService;
        }

        [HttpGet]
        [Route("/getAll")]
        public async Task<List<LawEnforcement>> GetAllLawEnforcement() =>
        await _lawEnforcementService.GetAllLawEnforcement();

        [HttpGet]
        [Route("/geLawEnforcementtById")]
        public async Task<ActionResult<LawEnforcement?>> GetLawEnforcementById(string id)
        {
            var lawEnforcement = await _lawEnforcementService.GetLawEnforcementById(id);

            if (lawEnforcement is null)
            {
                return NotFound("A record with the specified ID was not found: " + id);
            }

            return lawEnforcement;
        }

        [HttpPost]
        [Route("/addNewLawEnforcement")]
        public async Task<IActionResult> AddNewLawEnforcement(NewLawEnforcementDTO lawEnforcementDTO)
        {
            LawEnforcementRequest lawEnforcementRequest = new LawEnforcementRequest(new List<AssignedCrimeEvent>(), lawEnforcementDTO.Rank);
            await _lawEnforcementService.AddLawEnforcementToBase(lawEnforcementRequest);

            return Ok();
        }

        [HttpDelete]
        [Route("/deleteLawEnforcementById")]
        public async Task<IActionResult> DeleteEnforcementById(string id)
        {
            var lawEnforcement = await _lawEnforcementService.GetLawEnforcementById(id);

            if (lawEnforcement is null)
            {
                return NotFound("A record with the specified ID was not found: " + id);
            }

            await _lawEnforcementService.DeleteLawEnforcement(id);

            return NoContent();
        }

        [HttpPost]
        [Route("/addNewAssignedCrimeToChosedEnforcementById")]
        public async Task<IActionResult> AddNewAssignedCrimeToLawEnforcementById(string crimeEventId, string publicId)
        {
            await _lawEnforcementService.AddNewAssignedCrimeToChosedLawEnforcement(new AssignedCrimeEvent() { CrimeEventId = crimeEventId }, publicId);

            return Ok();
        }

        [HttpPost]
        [Route("/addNewAssignedCrimeToMostAccessibleLawEnforcement")]
        public async Task<IActionResult> AddNewAssignedCrimeToMostAccessibleLawEnforcement(CrimeEventIdDTO crimeEventId)
        {
            var publicVarEnforcementId = await _lawEnforcementService.AddNewAssignedCrimeToMostAccessibleLawEnforcement(new AssignedCrimeEvent() { CrimeEventId = crimeEventId.EventId });

            return Ok(publicVarEnforcementId);

        }

        [HttpGet]
        [Route("/getAllAssignedCrimeFromChosedLawEnforcemen")]
        public async Task<ActionResult<List<AssignedCrimeEvent>>> GetAllAssignedCrimeFromChosedLawEnforcement(string publicId)
        {
            if (publicId == "")
            {
                return BadRequest("Empty provided id");
            }
            var assignedCrimes = await _lawEnforcementService.GetAllAssignedCrimeFromChosedLawEnforcement(publicId);

            return assignedCrimes;
        }

        [HttpPost]
        [Route("/changeCrimeEventStatus")]
        public async Task<IActionResult> ChangeCrimeEventStatus(string newCrimeEventStatus, AssignedCrimeEvent assignedCrimeEvent)
        {

            var respond = await _lawEnforcementService.UpdateAssignedCrimeStatus(newCrimeEventStatus, assignedCrimeEvent);

            return Ok(respond);

        }

        [HttpPost]
        [Route("/getCrimeEventDetailsById")]
        public async Task<ActionResult<CrimeEventDetailsDTO>> GetCrimeEventDetailsByCrimeEventPublicId(string publicId)
        {
            CrimeEventDetailsDTO result = await _lawEnforcementService.GetCrimeEventDetailsByCrimeEventPublicId(publicId);

            return Ok(result);
        }
    }
}