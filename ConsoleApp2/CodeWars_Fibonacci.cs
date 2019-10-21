using System.Numerics;

public class Fibonacci
{
	public static BigInteger fib(int n)
	{
		if (n == 0)
		{
			return 0;
		}

		if (n == 1)
		{
			return 1;
		}

		BigInteger result = 1;
		BigInteger prev = 0;

		for (int i = 2; i <= n; i++)
		{
			var temp = result;
			result = result + prev;
			prev = temp;
		}

		return result;
	}
}