using System.Collections.Generic;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        public int Count => LinkedList.Count;
        public int Limit { get; }
        private LinkedList<T> LinkedList { get; }

        public LimitedSizeStack(int limit)
        {
            Limit = limit;
            LinkedList = new LinkedList<T>();
        }

        public void Push(T item)
        {
            if (LinkedList.Count == Limit)
                LinkedList.RemoveFirst();
            LinkedList.AddLast(item);
        }

        public T Pop()
        {
            var item = LinkedList.Last.Value;
            LinkedList.RemoveLast();
            return item;
        }
    }
}
