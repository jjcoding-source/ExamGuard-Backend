using ExamGuard.API.DTOs;
using ExamGuard.API.Enums;
using ExamGuard.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExamGuard.API.Controllers
{
    [ApiController]
    [Route("api/sessions")]
    [Authorize]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("start")]
        [Authorize(Roles = UserRole.Student)]
        public async Task<IActionResult> Start([FromBody] StartSessionDto dto)
        {
            var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var session = await _sessionService.StartAsync(dto.ExamId, studentId);
            if (session == null) return BadRequest(new { message = "Could not start session." });
            return Ok(session);
        }

        [HttpPost("submit")]
        [Authorize(Roles = UserRole.Student)]
        public async Task<IActionResult> Submit([FromBody] SubmitSessionDto dto)
        {
            var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var session = await _sessionService.SubmitAsync(dto, studentId);
            if (session == null) return BadRequest(new { message = "Could not submit session." });
            return Ok(session);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null) return NotFound();
            return Ok(session);
        }

        [HttpGet("exam/{examId}")]
        [Authorize(Roles = UserRole.Instructor)]
        public async Task<IActionResult> GetByExam(int examId)
        {
            var sessions = await _sessionService.GetByExamAsync(examId);
            return Ok(sessions);
        }

        [HttpGet("my")]
        [Authorize(Roles = UserRole.Student)]
        public async Task<IActionResult> GetMy()
        {
            var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var sessions = await _sessionService.GetMySessionsAsync(studentId);
            return Ok(sessions);
        }
    }
}