using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit { get; }
        private LimitedSizeStack<Tuple<TItem, int>> Stack { get; }

        public ListModel(int limit)
        {
            Limit = limit;
            Items = new List<TItem>();
            Stack = new LimitedSizeStack<Tuple<TItem, int>>(Limit);
        }

        public void AddItem(TItem item)
        {
            Stack.Push(new Tuple<TItem, int>(item, -1));
            Items.Add(item);
        }

        public void RemoveItem(int index)
        {
            Stack.Push(new Tuple<TItem, int>(Items[index], index));
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
            var index = lastAction.Item2;
            var item = lastAction.Item1;

            if (index < 0)
                Items.Remove(item);
            else
                Items.Insert(index, item);
        }
    }
}
