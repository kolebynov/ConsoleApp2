namespace ConsoleApp2.BenderEpisode1
{
	public class TeleportCell : Cell
	{
		public override void Apply(Bender.BenderState benderState)
		{
			benderState.CurrentPosition = benderState.Map.Find(x => x is TeleportCell && x != this);
		}
	}
}
