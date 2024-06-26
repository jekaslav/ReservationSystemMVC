using System.Runtime.Serialization;

namespace ReservationSystemMVC.Domain.Enums;

public enum ReservationStatus
{
    [EnumMember(Value = "Pending")]
    Pending = 1,
    [EnumMember(Value = "Approved")]
    Approved = 2,
    [EnumMember(Value = "Denied")]
    Denied = 3
}