using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleApp2.BenderEpisode1
{
	public class Bender : IEnumerable<string>
	{
		private readonly Map map;

		public Bender(Map map)
		{
			this.map = map;
		}

		public IEnumerator<string> GetEnumerator() => new BenderStateMachine(map);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public class BenderStateMachine : IEnumerator<string>
		{
			public Point CurrentPosition { get; set; }

			public string Current => CurrentDirection.Name;

			object IEnumerator.Current => Current;

			public BenderMode BenderMode { get; set; }

			public Direction CurrentDirection { get; set; }

			public IList<Direction> DirectionPriority { get; set; }

			public bool IsAlive { get; set; } = true;

			public Map Map { get; }

			public BenderStateMachine(Map map)
			{
				Map = map;
				Reset();
			}

			public bool MoveNext()
			{
				Map[CurrentPosition].Apply(this);
				var nextPosition = CurrentDirection.GetNextPosition(CurrentPosition);
				var nextCell = Map[nextPosition];
				if (!nextCell.CanGo(this))
				{
					CurrentDirection =
						DirectionPriority[(DirectionPriority.IndexOf(CurrentDirection) + 1) % DirectionPriority.Count];
					nextPosition = CurrentDirection.GetNextPosition(CurrentPosition);
				}

				return IsAlive;
			}

			public void Reset()
			{
				CurrentPosition = Map.StartCellPosition;
				DirectionPriority = new List<Direction>
				{
					Direction.South, Direction.East, Direction.North, Direction.West
				};
				CurrentDirection = DirectionPriority[0];
			}

			public void Dispose()
			{
			}
		}
	}
}
