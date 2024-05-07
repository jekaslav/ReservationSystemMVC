using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Domain.Enums;
using ReservationSystemMVC.Services.Interfaces;
using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Web.Controllers;

public class ReservationRequestController : Controller
{
    private IReservationRequestService ReservationRequestService { get; }
    
    public ReservationRequestController(IReservationRequestService reservationRequestService)
    {
        ReservationRequestService = reservationRequestService;
    }
    public IActionResult ReservationRequestMethods()
    {
        return View();
    }
    
    [HttpGet("reservations")]
    public async Task<IActionResult> GetAllReservationRequests(CancellationToken cancellationToken)
    {
        var requests = await ReservationRequestService.GetAllRequests(cancellationToken);
        
        return View(requests);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetReservationRequestById(int id, CancellationToken cancellationToken)
    {
        var result = await ReservationRequestService.GetRequestById(id, cancellationToken);
        
        return View(result);
    }
    
    [HttpGet("requests/create")]
    public IActionResult Create()
    {
        return View("CreateReservationRequest");
    }
    
    [HttpPost("requests/create")]
    public async Task<IActionResult> Create(ReservationRequestDto requestDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        await ReservationRequestService.Create(requestDto, cancellationToken);
        return RedirectToAction("ReservationRequestMethods");
    }
    
    [HttpGet("requests/update")]
    public IActionResult Update()
    {
        return View("UpdateReservationRequest");
    }

    [HttpPut("requests/update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReservationRequestDto requestDto, CancellationToken cancellationToken)
    {
        var result = await ReservationRequestService.Update(id, requestDto, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("requests/delete")]
    public IActionResult Delete()
    {
        return View("DeleteReservationRequest");
    }
    
    [HttpDelete("requests/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await ReservationRequestService.Delete(id, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("requests/update/status")]
    public IActionResult UpdateRequestStatus()
    {
        return View("UpdateReservationRequestStatus");
    }
    
    [HttpPut("/requests/update/status")]
    public async Task<IActionResult> UpdateReservationRequestStatus(int chiefId, int reservationRequestId, int newStatus, CancellationToken cancellationToken)
    {
        var result = await ReservationRequestService.UpdateReservationRequestStatus(reservationRequestId, chiefId, (ReservationStatus)newStatus, cancellationToken);
            
        return Ok(result);
    }
}