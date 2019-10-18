using System;

namespace ConsoleApp2.BenderEpisode1
{
	public abstract class Cell
	{
		public virtual bool CanGo(Bender.BenderState benderState) => true;

		public virtual void Apply(Bender.BenderState benderState)
		{
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
