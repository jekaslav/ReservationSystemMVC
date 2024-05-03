using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ReservationSystemMVC.Domain.Enums;

namespace ReservationSystemMVC.Web.Models;

public class ReservationRequestDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ClassroomId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
        
    [JsonConverter(typeof(StringEnumConverter))]
    public ReservationStatus Status { get; set; }
}