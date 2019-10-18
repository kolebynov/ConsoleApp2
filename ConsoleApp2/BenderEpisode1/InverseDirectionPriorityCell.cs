using System.Linq;

namespace ConsoleApp2.BenderEpisode1
{
	public class InverseDirectionPriorityCell : Cell
	{
		public override void Apply(Bender.BenderState benderState)
		{
			benderState.DirectionPriority = benderState.DirectionPriority.Reverse().ToList();
		}
	}
}
