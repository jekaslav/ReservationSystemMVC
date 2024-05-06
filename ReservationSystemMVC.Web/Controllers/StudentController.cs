using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Services.Interfaces;
using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Web.Controllers;

public class StudentController : Controller
{
    private IStudentService StudentService { get; }
    
    public StudentController(IStudentService studentService)
    {
        StudentService = studentService;
    }
    
    public IActionResult StudentMethods()
    {
        return View();
    }
    
    [HttpGet("student")]
    public async Task<IActionResult> GetAllStudents(CancellationToken cancellationToken)
    {
        var students = await StudentService.GetAllStudents(cancellationToken);
        return View(students);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetStudentById(int studentId, CancellationToken cancellationToken)
    {
        var result = await StudentService.GetStudentById(studentId, cancellationToken);
        return View(result);
    }
    
    [HttpGet("student/create")]
    public IActionResult Create()
    {
        return View("CreateStudent");
    }
    
    [HttpPost("student/create")]
    public async Task<IActionResult> Create(StudentDto studentDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        await StudentService.Create(studentDto, cancellationToken);
        return RedirectToAction("StudentMethods");
    }

    [HttpGet("student/update")]
    public IActionResult Update()
    {
        return View("UpdateStudent");
    }

    [HttpPut("student/update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentDto studentDto, CancellationToken cancellationToken)
    {
        var result = await StudentService.Update(id, studentDto, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("student/delete")]
    public IActionResult Delete()
    {
        return View("DeleteStudent");
    }
    
    [HttpDelete("student/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await StudentService.Delete(id, cancellationToken);
        return Ok(result);
    }
}