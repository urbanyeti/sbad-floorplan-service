using System;
namespace SBad.Map
{
    public interface IRoomService
    {
        FloorRoom GenerateRoom(RoomPlan roomType);
    }
}
