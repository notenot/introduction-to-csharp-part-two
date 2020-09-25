using System;
using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
	public class NotGreedyPathFinder : IPathFinder
	{
		public List<Point> FindPathToCompleteGoal(State state)
		{
            // var chestList = state.Chests
            MakePermutations(new int[state.Chests.Count], 0);
            return new List<Point>();
		}

        private static void Evaluate(int[] permutation)
        {
            int price = 0;
            for (int i = 0; i < permutation.Length; i++)
                price += prices[permutation[i], permutation[(i + 1) % permutation.Length]];
            foreach (var e in permutation)
                Console.Write(e + " ");
            Console.Write(price);
            Console.WriteLine();
        }

        private static void MakePermutations(int[] permutation, int position)
        {
            if (position == permutation.Length)
            {
                Evaluate(permutation);
                return;
            }

            for (var i = 0; i < permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, i, 0, position);
                if (index != -1)
                    continue;
                permutation[position] = i;
                MakePermutations(permutation, position + 1);
            }
        }
    }
}