using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Solution
{
	public class BattleshipField
	{
		private static List<Ship> ships = new List<Ship>();
		private static int[,] field;

		public static bool ValidateBattlefield(int[,] field)
		{
			BattleshipField.field = field;
			for (int y = 0; y < field.GetLength(0); y++)
			{
				for (int x = 0; x < field.GetLength(1); x++)
				{
					if (field[y, x] == 1)
					{
						if (IsDiagonalNearWithShips(new Point(x, y)))
						{
							return false;
						}

						var ship = FindSuitableShip(new Point(x, y));
						if (ship == null)
						{
							ship = new Ship();
							ships.Add(ship);
						}

						ship.Points.Add(new Point(x, y));
					}
				}
			}

			return ships.Count(s => s.Points.Count == 4) == 1
				&& ships.Count(s => s.Points.Count == 3) == 2
				&& ships.Count(s => s.Points.Count == 2) == 3
				&& ships.Count(s => s.Points.Count == 1) == 4
				&& ships.Count(s => s.Points.Count > 4) == 0;
		}

		private static bool IsDiagonalNearWithShips(Point point) =>
			FindShipByPoints(new[]
			{
				new Point(point.X - 1, point.Y - 1),
				new Point(point.X + 1, point.Y - 1),
				new Point(point.X - 1, point.Y + 1),
				new Point(point.X + 1, point.Y + 1)
			}) != null;

		private static Ship FindSuitableShip(Point point) =>
			FindShipByPoints(new[]
			{
				new Point(point.X - 1, point.Y),
				new Point(point.X + 1, point.Y),
				new Point(point.X, point.Y - 1),
				new Point(point.X, point.Y + 1)
			});

		private static Ship FindShipByPoints(Point[] points) =>
			ships.FirstOrDefault(x => points.Any(p => x.Points.Contains(p)));

	}

	public class Ship
	{
		public List<Point> Points { get; set; } = new List<Point>();
	}
}