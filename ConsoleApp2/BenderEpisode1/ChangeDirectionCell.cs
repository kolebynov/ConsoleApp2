namespace ConsoleApp2.BenderEpisode1
{
	public class ChangeDirectionCell : Cell
	{
		private readonly Direction newDirection;

		public ChangeDirectionCell(Direction newDirection)
		{
			this.newDirection = newDirection;
		}

		public override void Apply(Bender.BenderStateMachine benderStateMachine)
		{
			benderStateMachine.CurrentDirection = newDirection;
		}
	}
}
