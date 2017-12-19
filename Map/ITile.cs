using System;
namespace SBad.Map
{
	public interface ITile
	{
		Point Point { get; set; }
		int X { get; }
		int Y { get; } 
		int? Cost { get; set; }
		string Notes { get; set; }
		ITile Shift(Point origin);
	}
}
