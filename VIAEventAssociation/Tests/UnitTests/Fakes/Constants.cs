using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;

namespace UnitTests.Fakes;

public class Constants
{
    public static readonly DateTime TEST_DATE = new DateTime(2024, 4, 1, 17, 0, 0);
    public static readonly string START_DATE_STRING = TEST_DATE.ToString("yyyy-MM-dd HH:mm:ss");
    public static readonly string END_DATE_STRING = TEST_DATE.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss");

    public static readonly Guest TEST_GUEST = new("e2399bcd-b83b-400f-bfba-2e58cb2b2330","user@test.com", "John", "Doe", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScgIcP58MKd4CpuMNwdTncyXrDwBGmCYXWHA\\u0026usqp=CAU");
    public static readonly Event TEST_EVENT = new("3b1d8789-e982-41b4-9f77-a7459fd6f51e","Title", "Description", TEST_DATE, TEST_DATE.AddHours(2), 30, EventVisibility.Public, EventStatus.Active, new List<Guest>(), new Location("location", 32));
    public static readonly Location TEST_LOCATION = new("7c59adac-5a10-4de9-8783-ea2add07bb65","location", 32);
    public static readonly Creator TEST_CREATOR = new("342eeab8-c912-4286-a9d3-8e0fbab4dbf2","admin", "password");
}