using System;
namespace SBad.Map
{
	public interface ITile
	{
		Location Point { get; set; }
		int X { get; }
		int Y { get; } 
		int? Cost { get; set; }
		string Notes { get; set; }
		ITile Shift(Location origin);
	}
}
