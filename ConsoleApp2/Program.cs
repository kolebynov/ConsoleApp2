using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConsoleApp2
{
	public class SnailSolution
	{
		public static void Main()
		{
			var res = Snail(new int[1][]
			{
				new int[0]
			});
		}

		public static int[] Snail(int[][] array)
		{
			return new SnailStateMachine(array).ToArray();
		}

		public class SnailStateMachine : IEnumerable<int>
		{
			private readonly int[][] sourceArray;

			public SnailStateMachine(int[][] sourceArray)
			{
				this.sourceArray = sourceArray;
			}

			public IEnumerator<int> GetEnumerator()
			{
				if (sourceArray[0].Length == 0)
				{
					yield break;
				}

				var state = new State(sourceArray.Length);

				while (true)
				{
					// Возвращаем текущий элемент
					yield return sourceArray[state.CurrentPosition.Y][state.CurrentPosition.X];

					// Получаем след позицию из тек направления (право)
					var nextPosition = state.CurrentDirection.GetNextPosition(state.CurrentPosition);
					if (state.CurrentDirection.IsOnBoundLine(nextPosition)) // Новая позиция находится на границе, нужно менять направление
					{
						var nextDirection = state.CurrentDirection.NextDirection;

						// Новая позиция из нового направления (вниз)
						nextPosition = nextDirection.GetNextPosition(state.CurrentPosition);

						// Новая позиция находится на границе нового направления, дальше пути нет, выход из функции
						if (nextDirection.IsOnBoundLine(nextPosition))
						{
							yield break;
						}

						// Сужение границы для предыдущего направления (вверх)
						state.CurrentDirection.PrevDirection.DecreaseBoundLine();
						state.CurrentDirection = nextDirection;
					}

					state.CurrentPosition = nextPosition;
				}
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			private class State
			{
				public Direction CurrentDirection;
				public Point CurrentPosition;

				public State(int dimension)
				{
					var left = new Left(dimension);
					var right = new Right(dimension);
					var up = new Up(dimension);
					var down = new Down(dimension)
					{
						NextDirection = left,
						PrevDirection = right
					};
					right.NextDirection = down;
					right.PrevDirection = up;
					up.NextDirection = right;
					up.PrevDirection = left;
					left.NextDirection = up;
					left.PrevDirection = down;

					CurrentDirection = right;
				}
			}

			private abstract class Direction
			{
				private int boundLine;

				public Direction NextDirection { get; set; }

				public Direction PrevDirection { get; set; }

				public abstract Point GetNextPosition(Point currentPosition);

				public void DecreaseBoundLine()
				{
					boundLine = DecreaseBoundLine(boundLine);
				}

				public bool IsOnBoundLine(Point position)
				{
					return IsOnBoundLine(boundLine, position);
				}

				protected Direction(int dimension)
				{
					boundLine = CalculateBoundLine(dimension);
				}

				protected abstract int CalculateBoundLine(int dimension);

				protected abstract int DecreaseBoundLine(int boundLine);

				protected abstract bool IsOnBoundLine(int boundLine, Point position);
			}

			private class Up : Direction
			{
				public Up(int width) : base(width)
				{
				}

				public override Point GetNextPosition(Point currentPosition)
				{
					return new Point(currentPosition.X, currentPosition.Y - 1);
				}

				protected override int CalculateBoundLine(int dimension)
				{
					return -1;
				}

				protected override int DecreaseBoundLine(int boundLine)
				{
					return boundLine + 1;
				}

				protected override bool IsOnBoundLine(int boundLine, Point position)
				{
					return position.Y == boundLine;
				}
			}

			private class Down : Direction
			{
				public Down(int width) : base(width)
				{
				}

				public override Point GetNextPosition(Point currentPosition)
				{
					return new Point(currentPosition.X, currentPosition.Y + 1);
				}

				protected override int CalculateBoundLine(int dimension)
				{
					return dimension;
				}

				protected override int DecreaseBoundLine(int boundLine)
				{
					return boundLine - 1;
				}

				protected override bool IsOnBoundLine(int boundLine, Point position)
				{
					return position.Y == boundLine;
				}
			}

			private class Left : Direction
			{
				public Left(int width) : base(width)
				{
				}

				public override Point GetNextPosition(Point currentPosition)
				{
					return new Point(currentPosition.X - 1, currentPosition.Y);
				}

				protected override int CalculateBoundLine(int dimension)
				{
					return -1;
				}

				protected override int DecreaseBoundLine(int boundLine)
				{
					return boundLine + 1;
				}

				protected override bool IsOnBoundLine(int boundLine, Point position)
				{
					return position.X == boundLine;
				}
			}

			private class Right : Direction
			{
				public Right(int width) : base(width)
				{
				}

				public override Point GetNextPosition(Point currentPosition)
				{
					return new Point(currentPosition.X + 1, currentPosition.Y);
				}

				protected override int CalculateBoundLine(int dimension)
				{
					return dimension;
				}

				protected override int DecreaseBoundLine(int boundLine)
				{
					return boundLine - 1;
				}

				protected override bool IsOnBoundLine(int boundLine, Point position)
				{
					return position.X == boundLine;
				}
			}
		}
	}
}
