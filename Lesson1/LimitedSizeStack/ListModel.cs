using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit { get; }
        private LimitedSizeStack<Tuple<char, TItem, int>> Stack { get; }

        public ListModel(int limit)
        {
            Limit = limit;
            Items = new List<TItem>();
            Stack = new LimitedSizeStack<Tuple<char, TItem, int>>(Limit);
        }

        public void AddItem(TItem item)
        {
            Stack.Push(new Tuple<char, TItem, int>('+', item, 0));
            Items.Add(item);
        }

        public void RemoveItem(int index)
        {
            Stack.Push(new Tuple<char, TItem, int>('-', Items[index], index));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return Stack.Count > 0;
        }

        public void Undo()
        {
            if (!CanUndo())
                return;

            var lastAction = Stack.Pop();
            switch (lastAction.Item1)
            {
                case '+':
                    Items.Remove(lastAction.Item2);
                    break;
                case '-':
                    Items.Insert(lastAction.Item3, lastAction.Item2);
                    break;
            }
        }
    }
}
