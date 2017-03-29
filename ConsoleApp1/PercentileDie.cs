using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public struct PercentileDie
	{
		public int Value { get; }
		public int Multiplier { get; }

		public PercentileDie(int multiplier, int value)
		{
			this.Value = value;
			this.Multiplier = multiplier;
		}
	}
}
