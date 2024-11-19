using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BAExamApp.Business.Interfaces.Services;
using BAExamApp.Dtos.ApiDtos.StudentExamApiDtos;
using BAExamApp.Business.ApiServices.Interfaces;
using BAExamApp.Dtos.ApiDtos.ExamApiDtos;
using BAExamApp.DataAccess.Contexts;
using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static Azure.Core.HttpHeader;

namespace BAExamApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentApiService _studentApiService;
        private readonly IStudentExamApiService _studentExamApiService;
        private readonly IRegisterCodeApiService _registerCodeApiService;

        public StudentController(IStudentApiService studentService, IStudentExamApiService studentExamService, IRegisterCodeApiService registerCodeApiService)
        {
            _studentApiService = studentService;
            _studentExamApiService = studentExamService;
            _registerCodeApiService = registerCodeApiService;
        }

        [HttpGet("GetStudentExamResults/{email}")]
        public async Task<IActionResult> GetStudentExamResults(string email, string code)
        {

            if (await _registerCodeApiService.IsRegisterCodeActiveAsync(code))
            {
                try
                {
                    var studentResult = await _studentApiService.GetAStudentByEmail(email);
                    if (studentResult == null || studentResult.Data == null)
                    {
                        return NotFound("User not found.");
                    }

                    var student = studentResult.Data;
                    var studentEmail = student.Email;

                    if (string.IsNullOrEmpty(studentEmail))
                    {
                        return NotFound("User email address not found.");
                    }

                    var studentExamsResult = await _studentExamApiService.GetStudentExamsResults(email);
                    if (studentExamsResult == null || studentExamsResult.Data == null)
                    {
                        return NotFound("Exam results not found.");
                    }

                    var studentExams = studentExamsResult.Data;

                    var examResultsDto = studentExams.Select(exam => new ExamResultDto
                    {
                        ExamName = exam.Exam.Name,
                        Score = exam.Score
                    }).ToList();

                    return Ok(examResultsDto);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
                }
            }
            else
            {
                return NotFound("Code is not active");
            }
        }
    }
}

