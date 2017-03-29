namespace ConsoleApp1
{
	public class Point
	{
		public Point(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		public double X { get; }
		public double Y { get; }

		public void Deconstruct(out double x, out double y)
		{
			x = this.X;
			y = this.Y;
		}
	}
}
