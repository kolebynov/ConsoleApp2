namespace ConsoleApp2.BenderEpisode1
{
	public class EndCell : Cell
	{
		public override void Apply(Bender.BenderState benderState)
		{
			benderState.IsAlive = false;
		}
	}
}
