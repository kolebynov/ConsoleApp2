using System.Linq;

namespace ConsoleApp2.BenderEpisode1
{
	public class InverseDirectionPriorityCell : Cell
	{
		public override void Apply(Bender.BenderStateMachine benderStateMachine)
		{
			benderStateMachine.DirectionPriority = benderStateMachine.DirectionPriority.Reverse().ToList();
		}
	}
}
