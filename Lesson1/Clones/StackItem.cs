namespace Clones
{
    public class StackItem<T>
    {
        public T Value { get; }
        public StackItem<T> Previous { get; }

        public StackItem(T value, StackItem<T> previous)
        {
            Value = value;
            Previous = previous;
        }
    }
}
