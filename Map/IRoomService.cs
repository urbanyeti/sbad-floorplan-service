using System;
namespace SBad.Map
{
	public interface IRoomService<T> where T : IFloorRoom
	{
        T GenerateRoom(RoomPlan roomType);
	}
}
