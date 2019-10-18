using System;

namespace ConsoleApp2.BenderEpisode1
{
	public class HardObstacleCell : Cell
	{
		public override void Apply(Bender.BenderStateMachine benderStateMachine)
		{
			throw new InvalidOperationException("Bender can't be on this cell");
		}

		public override bool CanGo(Bender.BenderStateMachine benderStateMachine) => false;
	}
}
