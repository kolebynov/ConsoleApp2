using System;

namespace ConsoleApp2.BenderEpisode1
{
	public class BearCell : Cell
	{
		public override void Apply(Bender.BenderStateMachine benderStateMachine)
		{
			switch (benderStateMachine.BenderMode)
			{
				case BenderMode.Normal:
					benderStateMachine.BenderMode = BenderMode.Breaker;
					break;
				case BenderMode.Breaker:
					benderStateMachine.BenderMode = BenderMode.Normal;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
