﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConsoleApp2.BenderEpisode1
{
	public class Map : IEnumerable<Cell>, ICloneable
	{
		private readonly Cell[,] cells;

		public Map(Cell[,] cells)
		{
			this.cells = cells;
			StartCellPosition = Find(x => x is StartCell);
			SoftObstacleCellCount = this.Count(x => x is SoftObstacleCell);
		}

		public Point StartCellPosition { get; }

		public int SoftObstacleCellCount { get; private set; }

		public Cell this[Point position]
		{
			get => cells[position.Y, position.X];
			set
			{
				if (cells[position.Y, position.X] is SoftObstacleCell && !(value is SoftObstacleCell))
				{
					SoftObstacleCellCount--;
				}

				cells[position.Y, position.X] = value;
			}
		}

		public IEnumerator<Cell> GetEnumerator()
		{
			for (int i = 0; i < cells.GetLength(0); i++)
			{
				for (int j = 0; j < cells.GetLength(1); j++)
				{
					yield return cells[i, j];
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Point Find(Predicate<Cell> predicate)
		{
			for (int i = 0; i < cells.GetLength(0); i++)
			{
				for (int j = 0; j < cells.GetLength(1); j++)
				{
					if (predicate(cells[i, j]))
					{
						return new Point(j, i);
					}
				}
			}

			return new Point(-1, -1);
		}

		public static Map Parse(string[] stringMap)
		{
			var cells = new Cell[stringMap.Length, stringMap[0].Length];
			for (int i = 0; i < cells.GetLength(0); i++)
			{
				Console.Error.WriteLine(stringMap[i]);
				for (int j = 0; j < cells.GetLength(1); j++)
				{
					cells[i, j] = Cell.GetCell(stringMap[i][j]);
				}
			}

			return new Map(cells);
		}

		public Map Clone()
		{
			return new Map((Cell[,])cells.Clone());
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
	}
}
