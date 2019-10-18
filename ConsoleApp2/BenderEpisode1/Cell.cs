using System;
using System.Linq;

namespace ConsoleApp2.BenderEpisode1
{
	public abstract class Cell
	{
		private int visitedCount;

		public virtual bool CanGo(Bender.BenderState benderState) => true;

		public virtual void Apply(Bender.BenderState benderState)
		{
			visitedCount++;

			if (visitedCount == 3)
			{
				var reversed = benderState.PreviousSteps.Reverse();
				var firstLoop = reversed.TakeWhile(x => !x.Equals((benderState.CurrentPosition, benderState.CurrentDirection)));
				var secondLoop = reversed
					.SkipWhile(x => !x.Equals((benderState.CurrentPosition, benderState.CurrentDirection)))
					.Skip(1)
					.TakeWhile(x => !x.Equals((benderState.CurrentPosition, benderState.CurrentDirection)));
				benderState.IsLoop = firstLoop.SequenceEqual(secondLoop);
			}

			benderState.PreviousSteps.Add((benderState.CurrentPosition, benderState.CurrentDirection));
		}

		public static Cell GetCell(char charCell)
		{
			switch (charCell)
			{
				case '@':
					return new StartCell();
				case '$':
					return new EndCell();
				case '#':
					return new HardObstacleCell();
				case 'X':
					return new SoftObstacleCell();
				case 'S':
					return new ChangeDirectionCell(Direction.South);
				case 'N':
					return new ChangeDirectionCell(Direction.North);
				case 'W':
					return new ChangeDirectionCell(Direction.West);
				case 'E':
					return new ChangeDirectionCell(Direction.East);
				case 'I':
					return new InverseDirectionPriorityCell();
				case 'B':
					return new BearCell();
				case 'T':
					return new TeleportCell();
				case ' ':
					return new EmptyCell();
				default:
					throw new ArgumentException("Invalid char", nameof(charCell));
			}
		}
	}
}
