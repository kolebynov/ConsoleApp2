using System;
using System.IO;

namespace ConsoleApp2
{
	public class Program
	{
		public static void Main()
		{
			Console.SetIn(new StringReader(
@"4 4 1
0 1
0 2
1 3
2 3
3
0
2"));
			SkynetRevolutionEpisode1.Run();
		}
	}
}
