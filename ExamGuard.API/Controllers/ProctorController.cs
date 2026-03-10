using ExamGuard.API.DTOs;
using ExamGuard.API.Enums;
using ExamGuard.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamGuard.API.Controllers
{
    [ApiController]
    [Route("api/proctor")]
    [Authorize]
    public class ProctorController : ControllerBase
    {
        private readonly IProctorService _proctorService;

        public ProctorController(IProctorService proctorService)
        {
            _proctorService = proctorService;
        }

        [HttpPost("event")]
        [Authorize(Roles = UserRole.Student)]
        public async Task<IActionResult> LogEvent([FromBody] LogEventDto dto)
        {
            var result = await _proctorService.LogEventAsync(dto);
            if (!result) return BadRequest(new { message = "Could not log event." });
            return Ok(new { message = "Event logged." });
        }

        [HttpGet("events/{sessionId}")]
        [Authorize(Roles = $"{UserRole.Instructor},{UserRole.Admin}")]
        public async Task<IActionResult> GetEvents(int sessionId)
        {
            var events = await _proctorService.GetEventsAsync(sessionId);
            return Ok(events);
        }

        [HttpPost("terminate/{sessionId}")]
        [Authorize(Roles = UserRole.Instructor)]
        public async Task<IActionResult> Terminate(int sessionId)
        {
            var result = await _proctorService.TerminateSessionAsync(sessionId);
            if (!result) return NotFound();
            return Ok(new { message = "Session terminated." });
        }
    }
}