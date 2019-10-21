using System;
using System.Collections.Generic;
using System.Linq;

public class SkynetRevolutionEpisode1
{
	public static void Run()
	{
		var inputs = Console.ReadLine().Split(' ');
		int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
		int L = int.Parse(inputs[1]); // the number of links
		int E = int.Parse(inputs[2]); // the number of exit gateways
		var network = new SkynetNetwork(N);
		for (int i = 0; i < L; i++)
		{
			inputs = Console.ReadLine().Split(' ');
			network.AddLink(int.Parse(inputs[0]), int.Parse(inputs[1]));
		}
		for (int i = 0; i < E; i++)
		{
			network.AddExitGateway(int.Parse(Console.ReadLine()));
		}

		// game loop
		while (true)
		{
			int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

			var shortestPath = network.ExitGateways
				.Select(exit => network.GetShortestPath(SI, exit))
				.Aggregate((IReadOnlyList<int>)null, (prev, curr) => prev?.Count < curr.Count ? prev : curr);

			Console.WriteLine($"{shortestPath[0]} {shortestPath[1]}");
		}
	}
}

public class SkynetNetwork
{
	private readonly List<Node> exitGateways = new List<Node>();
	private readonly Node[] nodes;

	public IEnumerable<int> ExitGateways => exitGateways.Select(x => x.Index);

	public SkynetNetwork(int nodesCount)
	{
		nodes = Enumerable.Range(0, nodesCount).Select(x => new Node(x)).ToArray();
	}

	public void AddLink(int firstNodeIndex, int secondNodeIndex)
	{
		nodes[firstNodeIndex].ConnectedNodes.Add(nodes[secondNodeIndex]);
		nodes[secondNodeIndex].ConnectedNodes.Add(nodes[firstNodeIndex]);
	}

	public void AddExitGateway(int gatewayNodeIndex)
	{
		exitGateways.Add(nodes[gatewayNodeIndex]);
	}

	public IReadOnlyList<int> GetShortestPath(int nodeFromIndex, int nodeToIndex)
	{
		var nodeFrom = nodes[nodeFromIndex];
		var nodeTo = nodes[nodeToIndex];
		var visitedNodes = new HashSet<Node>
		{
			nodeFrom
		};
		var paths = new List<IReadOnlyList<Node>>
		{
			new List<Node> { nodeFrom }
		};
		Console.Error.WriteLine($"Starting find shortest path. From: {nodeFrom}, To: {nodeTo}");

		while (true)
		{
			foreach (var currentPath in paths.Select(x => x).ToArray())
			{
				Console.Error.WriteLine($"Current path: {string.Join(',', currentPath.Select(x => x.Index))}");
				var lastNode = currentPath.Last();

				foreach (var connectedNode in lastNode.ConnectedNodes)
				{
					if (visitedNodes.Contains(connectedNode))
					{
						continue;
					}

					Console.Error.WriteLine($"Connected node: {connectedNode}");
					visitedNodes.Add(connectedNode);

					if (connectedNode == nodeTo)
					{
						var path = currentPath.Select(x => x.Index).Concat(new[] { connectedNode.Index }).ToArray();
						Console.Error.WriteLine($"Path found: {string.Join(',', path)}");
						return path;
					}

					paths.Add(new List<Node>(currentPath)
					{
						connectedNode
					});
				}

				paths.Remove(currentPath);
			}
		}
	}

	private readonly struct Link
	{
		public readonly int FirstNodeIndex;
		public readonly int SecondNodeIndex;

		public Link(int firstNodeIndex, int secondNodeIndex)
		{
			FirstNodeIndex = firstNodeIndex;
			SecondNodeIndex = secondNodeIndex;
		}
	}

	private class Node
	{
		public readonly int Index;
		public readonly List<Node> ConnectedNodes = new List<Node>();

		public Node(int index)
		{
			Index = index;
		}

		public override bool Equals(object obj) => Equals((Node)obj);

		public override int GetHashCode() => Index;

		public override string ToString() => $"Index: {Index}";

		public static bool operator ==(Node left, Node right) => Equals(left, right);

		public static bool operator !=(Node left, Node right) => !Equals(left, right);

		private bool Equals(Node other)
		{
			return Index == other.Index;
		}
	}
}