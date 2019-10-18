using System;
using System.Drawing;

namespace ConsoleApp2.BenderEpisode1
{
	public class Direction
	{
		private readonly Func<Point, Point> nextPositionFunc;

		public static Direction North = new Direction("NORTH", p => new Point(p.X, p.Y - 1));
		public static Direction South = new Direction("SOUTH", p => new Point(p.X, p.Y + 1));
		public static Direction West = new Direction("WEST", p => new Point(p.X - 1, p.Y));
		public static Direction East = new Direction("EAST", p => new Point(p.X + 1, p.Y));

		public string Name { get; }

		public Point GetNextPosition(Point currentPosition) => nextPositionFunc(currentPosition);

		public override string ToString() => Name;

		private Direction(string name, Func<Point, Point> nextPositionFunc)
		{
			Name = name;
			this.nextPositionFunc = nextPositionFunc;
		}
	}
}
