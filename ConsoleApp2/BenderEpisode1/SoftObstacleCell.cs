using System;

namespace ConsoleApp2.BenderEpisode1
{
	public class SoftObstacleCell : Cell
	{
		public override void Apply(Bender.BenderState benderState)
		{
			if (benderState.BenderMode == BenderMode.Breaker)
			{
				benderState.Map[benderState.CurrentPosition] = new EmptyCell();
			}
			else
			{
				throw new InvalidOperationException("Bender can't be on this cell in Normal mode");
			}
		}

		public override bool CanGo(Bender.BenderState benderState) => benderState.BenderMode == BenderMode.Breaker;
	}
}
