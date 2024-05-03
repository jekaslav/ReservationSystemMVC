using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReservationSystemMVC.Services.Interfaces;
using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Web.Controllers;

public class ClassroomController : Controller
{
    private IClassroomService ClassroomService { get; }
    
    public ClassroomController(IClassroomService classroomService)
    {
        ClassroomService = classroomService;
    }
    public IActionResult ClassroomMethods()
    {
        return View();
    }
    
    [HttpGet("classrooms")]
    public async Task<IActionResult> GetAllClassrooms(CancellationToken cancellationToken)
    {
        var classrooms = await ClassroomService.GetAllClassrooms(cancellationToken);
        return View(classrooms);
    }

    [HttpGet]
    public async Task<IActionResult> GetClassroomById(int classroomId, CancellationToken cancellationToken)
    {
        var result = await ClassroomService.GetClassroomById(classroomId, cancellationToken);
        return View(result);
    }
    
    [HttpGet("classroom/create")]
    public IActionResult Create()
    {
        return View("CreateClassroom");
    }
    
    [HttpPost("classroom/create")]
    public async Task<IActionResult> Create(ClassroomDto classroomDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await ClassroomService.Create(classroomDto, cancellationToken);
        return RedirectToAction("ClassroomMethods");
    }

    [HttpGet("classrooms/update")]
    public IActionResult Update()
    {
        return View("UpdateClassroom");
    }

    [HttpPut("classrooms/update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClassroomDto classroomDto, CancellationToken cancellationToken)
    {
        var result = await ClassroomService.Update(id, classroomDto, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("classrooms/delete")]
    public IActionResult Delete()
    {
        return View("DeleteClassroom");
    }
    
    [HttpDelete("classrooms/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await ClassroomService.Delete(id, cancellationToken);
        return Ok(result);
    }

}