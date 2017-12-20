using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.Map.Navigation
{
	public class PathNotFound : Exception
	{
		public PathNotFound()
		{
		}

		public PathNotFound(string message) : base(message)
		{
		}

		public PathNotFound(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}
