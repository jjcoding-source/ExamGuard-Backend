using ExamGuard.API.DTOs;
using ExamGuard.API.Enums;
using ExamGuard.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExamGuard.API.Controllers
{
    [ApiController]
    [Route("api/exams")]
    [Authorize]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(ClaimTypes.Role)!.Value;
            var exams = await _examService.GetAllAsync(userId, role);
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exam = await _examService.GetByIdAsync(id);
            if (exam == null) return NotFound();
            return Ok(exam);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Instructor)]
        public async Task<IActionResult> Create([FromBody] CreateExamDto dto)
        {
            var instructorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var exam = await _examService.CreateAsync(dto, instructorId);
            return CreatedAtAction(nameof(GetById), new { id = exam.Id }, exam);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Instructor)]
        public async Task<IActionResult> Update(int id, [FromBody] CreateExamDto dto)
        {
            var instructorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var exam = await _examService.UpdateAsync(id, dto, instructorId);
            if (exam == null) return NotFound();
            return Ok(exam);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Instructor)]
        public async Task<IActionResult> Delete(int id)
        {
            var instructorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _examService.DeleteAsync(id, instructorId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}