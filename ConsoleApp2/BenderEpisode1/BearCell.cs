using System;

namespace ConsoleApp2.BenderEpisode1
{
	public class BearCell : Cell
	{
		public override void Apply(Bender.BenderState benderState)
		{
			switch (benderState.BenderMode)
			{
				case BenderMode.Normal:
					benderState.BenderMode = BenderMode.Breaker;
					break;
				case BenderMode.Breaker:
					benderState.BenderMode = BenderMode.Normal;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			base.Apply(benderState);
		}
	}
}
