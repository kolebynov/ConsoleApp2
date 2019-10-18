using System;
using System.IO;
using ConsoleApp2.BenderEpisode1;

namespace ConsoleApp2
{
	public class Program
	{
		public static void Main()
		{
			Console.SetIn(new StreamReader(@"..\..\..\BenderEpisode1\BenderTest.txt"));
			BenderRunner.Run();
		}
	}
}
