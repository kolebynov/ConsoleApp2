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
			var state = new BenderState(map);

			while (true)
			{
				state.Map[state.CurrentPosition].Apply(state);
				if (!state.IsAlive)
				{
					yield break;
				}

				var nextPosition = state.CurrentDirection.GetNextPosition(state.CurrentPosition);
				var nextCell = state.Map[nextPosition];
				if (!nextCell.CanGo(state))
				{
					state.CurrentDirection = state.DirectionPriority
						.First(x => state.Map[x.GetNextPosition(state.CurrentPosition)].CanGo(state));
					nextPosition = state.CurrentDirection.GetNextPosition(state.CurrentPosition);
				}

				state.CurrentPosition = nextPosition;
				yield return state.CurrentDirection.Name;
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

			public Map Map { get; }

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
