using System;

namespace ConsoleApp2.BenderEpisode1
{
	public class BenderRunner
	{
		public static void Run()
		{
			string[] inputs = Console.ReadLine().Split(' ');
			int L = int.Parse(inputs[0]);
			int C = int.Parse(inputs[1]);
			var stringMap = new string[L];
			for (int i = 0; i < L; i++)
			{
				stringMap[i] = Console.ReadLine();
			}
			
			var bender = new Bender(Map.Parse(stringMap));
			foreach (var direction in bender)
			{
				Console.WriteLine(direction);
			}
		}
	}
}
