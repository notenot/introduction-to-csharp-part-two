using System;
using System.Collections.Generic;

namespace Clones
{
	public class CloneVersionSystem : ICloneVersionSystem
    {
        private List<Clone> clones;

        public CloneVersionSystem()
        {
            clones = new List<Clone> { new Clone() };
        }

		public string Execute(string query)
        {
            var args = query.Split();
            var id = Convert.ToInt32(args[1]) - 1;

            switch (args[0])
            {
                case "learn":
                    clones[id].Learn(args[2]);
                    break;
                case "rollback":
                    clones[id].Rollback();
                    break;
                case "relearn":
                    clones[id].Relearn();
                    break;
                case "clone":
                    clones.Add(new Clone(clones[id]));
                    break;
                case "check":
                    return clones[id].Check();
                default:
                    throw new ArgumentException();
            }
			return null;
		}
	}
}
