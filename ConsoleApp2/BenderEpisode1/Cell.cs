using System;

namespace ConsoleApp2.BenderEpisode1
{
	public abstract class Cell
	{
		protected static readonly StartCell StartCell = new StartCell();
		protected static readonly EndCell EndCell = new EndCell();
		protected static readonly HardObstacleCell HardObstacleCell = new HardObstacleCell();
		protected static readonly SoftObstacleCell SoftObstacleCell = new SoftObstacleCell();
		protected static readonly ChangeDirectionCell SouthChangeDirectionCell = new ChangeDirectionCell();
		protected static readonly ChangeDirectionCell NorthChangeDirectionCell = new ChangeDirectionCell();
		protected static readonly ChangeDirectionCell WestChangeDirectionCell = new ChangeDirectionCell();
		protected static readonly ChangeDirectionCell EastChangeDirectionCell = new ChangeDirectionCell();
		protected static readonly InverseDirectionPriorityCell InverseDirectionPriorityCell = new InverseDirectionPriorityCell();
		protected static readonly BearCell BearCell = new BearCell();
		protected static readonly TeleportCell TeleportCell = new TeleportCell();
		protected static readonly EmptyCell EmptyCell = new EmptyCell();

		public virtual bool CanGo(Bender.BenderStateMachine benderStateMachine) => true;

		public virtual void Apply(Bender.BenderStateMachine benderStateMachine)
		{
		}

		public static Cell GetCell(char charCell)
		{
			switch (charCell)
			{
				case '@':
					return StartCell;
				case '$':
					return EndCell;
				case '#':
					return HardObstacleCell;
				case 'X':
					return SoftObstacleCell;
				case 'S':
					return SouthChangeDirectionCell;
				case 'N':
					return NorthChangeDirectionCell;
				case 'W':
					return WestChangeDirectionCell;
				case 'E':
					return EastChangeDirectionCell;
				case 'I':
					return InverseDirectionPriorityCell;
				case 'B':
					return BearCell;
				case 'T':
					return TeleportCell;
				case ' ':
					return EmptyCell;
				default:
					throw new ArgumentException("Invalid char", nameof(charCell));
			}
		}
	}
}
