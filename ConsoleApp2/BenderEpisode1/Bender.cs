using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConsoleApp2.BenderEpisode1
{
	public class Bender : IEnumerable<string>
	{
		private readonly Map map;

		public Bender(Map map)
		{
			this.map = map;
		}

		public IEnumerator<string> GetEnumerator()
		{
			var state = new BenderState(map.Clone());
			var directions = new List<string>();

			while (true)
			{
				state.CurrentCell.Apply(state);

				if (state.IsLoop)
				{
					return new List<string> { "LOOP" }.GetEnumerator();
				}

				if (!state.IsAlive)
				{
					return directions.GetEnumerator();
				}

				if (!state.NextCell.CanGo(state))
				{
					state.CurrentDirection = state.DirectionPriority
						.First(direction => state.Map[direction.GetNextPosition(state.CurrentPosition)].CanGo(state));
				}

				state.CurrentPosition = state.NextPosition;
				directions.Add(state.CurrentDirection.Name);
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public class BenderState
		{
			public Point CurrentPosition { get; set; }

			public BenderMode BenderMode { get; set; }

			public Direction CurrentDirection { get; set; }

			public IList<Direction> DirectionPriority { get; set; }

			public bool IsAlive { get; set; } = true;

			public bool IsLoop { get; set; } = false;

			public Map Map { get; }

			public Cell CurrentCell => Map[CurrentPosition];

			public Point NextPosition => CurrentDirection.GetNextPosition(CurrentPosition);

			public Cell NextCell => Map[NextPosition];

			public IList<(Point position, Direction direction)> PreviousSteps { get; } = new List<(Point position, Direction direction)>();

			public BenderState(Map map)
			{
				Map = map;
				CurrentPosition = Map.StartCellPosition;
				DirectionPriority = new List<Direction>
				{
					Direction.South, Direction.East, Direction.North, Direction.West
				};
				CurrentDirection = DirectionPriority[0];
			}
		}
	}
}
