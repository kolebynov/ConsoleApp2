using System;

namespace ConsoleApp2.BenderEpisode1
{
	public class SoftObstacleCell : Cell
	{
		public override void Apply(Bender.BenderStateMachine benderStateMachine)
		{
			if (benderStateMachine.BenderMode == BenderMode.Breaker)
			{
				benderStateMachine.Map[benderStateMachine.CurrentPosition] = EmptyCell;
			}
			else
			{
				throw new InvalidOperationException("Bender can't be on this cell in Normal mode");
			}
		}

		public override bool CanGo(Bender.BenderStateMachine benderStateMachine) => benderStateMachine.BenderMode == BenderMode.Breaker;
	}
}
