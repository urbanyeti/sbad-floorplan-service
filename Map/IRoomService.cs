using System;
namespace SBad.Map
{
	public interface IRoomService
	{
        IFloorRoom GenerateRoom(RoomPlan roomType);
	}
}
