namespace ConsoleApp2.BenderEpisode1
{
	public class ChangeDirectionCell : Cell
	{
		private readonly Direction newDirection;

		public ChangeDirectionCell(Direction newDirection)
		{
			this.newDirection = newDirection;
		}

		public override void Apply(Bender.BenderState benderState)
		{
			benderState.CurrentDirection = newDirection;
		}
	}
}
