using System.Drawing;

namespace ConsoleApp2.BenderEpisode1
{
	public class Map
	{
		private readonly Cell[,] cells;

		public Map(Cell[,] cells)
		{
			this.cells = cells;
			for (int i = 0; i < cells.GetLength(0); i++)
			{
				for (int j = 0; j < cells.GetLength(1); j++)
				{
					if (cells[i, j] is StartCell)
					{
						StartCellPosition = new Point(j, i);
						break;
					}
				}

				if (StartCellPosition != default)
				{
					break;
				}
			}
		}

		public Point StartCellPosition { get; }

		public int Width => cells.GetLength(1);

		public int Height => cells.GetLength(0);

		public Cell this[Point position]
		{
			get => cells[position.Y, position.X];
			set => cells[position.Y, position.X] = value;
		}

		public static Map Parse(string[] stringMap)
		{
			var cells = new Cell[stringMap.Length, stringMap[0].Length];
			for (int i = 0; i < cells.GetLength(0); i++)
			{
				for (int j = 0; j < cells.GetLength(1); j++)
				{
					cells[i, j] = Cell.GetCell(stringMap[i][j]);
				}
			}

			return new Map(cells);
		}
	}
}
