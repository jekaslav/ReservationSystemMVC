using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReservationSystemMVC.Services.Interfaces;
using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Web.Controllers;

public class ChiefController : Controller
{
    private IChiefService ChiefService { get; }
    
    public ChiefController(IChiefService chiefService)
    {
        ChiefService = chiefService;
    }
    public IActionResult ChiefMethods()
    {
        return View();
    }
    
    [HttpGet("chiefs")]
    public async Task<IActionResult> GetAllChiefs(CancellationToken cancellationToken)
    {
        var chiefs = await ChiefService.GetAllChiefs(cancellationToken);
        return View(chiefs);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetChiefById(int chiefId, CancellationToken cancellationToken)
    {
        var result = await ChiefService.GetChiefById(chiefId, cancellationToken);
        return View(result);
    }
    
    [HttpGet("chiefs/create")]
    public IActionResult Create()
    {
        return View("CreateChief");
    }
    
    [HttpPost("chiefs/create")]
    public async Task<IActionResult> Create(ChiefDto chiefDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        await ChiefService.Create(chiefDto, cancellationToken);
        return RedirectToAction("ChiefMethods");
    }
    
    [HttpGet("chiefs/update")]
    public IActionResult Update()
    {
        return View("UpdateChief");
    }

    [HttpPut("chiefs/update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ChiefDto chiefDto, CancellationToken cancellationToken)
    {
        var result = await ChiefService.Update(id, chiefDto, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("chiefs/delete")]
    public IActionResult Delete()
    {
        return View("DeleteChief");
    }
    
    [HttpDelete("chiefs/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await ChiefService.Delete(id, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("chief/takecontrol")]
    public IActionResult TakeControl()
    {
        return View("TakeControl");
    }

    [HttpPost("chief/takecontrol")]
    public async Task<IActionResult> TakeControl(int classroomId, int chiefId, CancellationToken cancellationToken)
    {
        var result = await ChiefService.TakeControl(classroomId, chiefId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("chief/releasecontrol")]
    public IActionResult ReleaseControl()
    {
        return View("ReleaseControl");
    }

    [HttpPost("chief/releasecontrol")]
    public async Task<IActionResult> ReleaseControl(int classroomId, int chiefId, CancellationToken cancellationToken)
    {
        var result = await ChiefService.ReleaseControl(classroomId, chiefId, cancellationToken);
        return Ok(result);
    }
}