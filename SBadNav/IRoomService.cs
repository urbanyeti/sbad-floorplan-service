using System;
namespace SBad.Nav
{
	public interface IRoomService<T> where T : IFloorRoom
	{
        T GenerateRoom(RoomPlan roomType);
	}
}
