using System;
using System.IO;
using ConsoleApp2.BenderEpisode1;
using Solution;

namespace ConsoleApp2
{
	public class Program
	{
		public static void Main()
		{
			int[,] field = new int[10,10]
			{{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
				{1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
				{1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
				{1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
				{0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
				{0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
				{0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
			var a = BattleshipField.ValidateBattlefield(field);
		}
	}
}
