using System.Collections.Generic;

namespace Calculator
{
    internal sealed class SpecializedStack<T>
    {
        // Internal Instance Data
        private readonly IList<T> _store = new List<T>();
        //private int index = 0;

        // .Ctor
        internal SpecializedStack()
        {
        }

        // Properties
        public int Count { get { return _store.Count; } }

        // Methods
        internal void Push(T value)
        {
            _store.Add(value);
        }

        internal T Pick(int index)
        {
            var result = _store[index];

            _store.RemoveAt(index);
            return result;
        }

        internal T Pop()
        {
            return Pick(_store.Count - 1);
        }

        internal T Peek()
        {
            return _store[_store.Count - 1];
        }

        internal void Clear()
        {
            _store.Clear();
        }
    }
}