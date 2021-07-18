using System;

namespace jNet.Temporal
{
	public abstract class EdgeBase : Base
	{
		public string BackCategory { get; init; }

		protected EdgeBase(string backCategory) : base("Edge")
		{
			BackCategory = backCategory;
		}
		protected EdgeBase() : base("Edge")
		{
			BackCategory = Category;
		}
	}

	public class Edge : EdgeBase
	{
		public Edge(Node from, Node to, bool bidirectional = false) : base()
		{
			From = from;
			To = to;
			Bidirectional = bidirectional;
		}

		public Edge(Node from, Node to, string backwardCategory) : base(backwardCategory)
		{
			From = from;
			To = to;
			Bidirectional = true;
		}

		public bool Bidirectional { get; init; }
		public Node From { get; init; }
		public Node To { get; init; }
	}
}
