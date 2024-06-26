namespace CustomDataStructures.Domain.DataStructures
{
    public class QuickPopDataStructure<T> where T : IComparable<T>
    {
        private IComparer<T> Comparer { get; set; }

        public QuickPopDataStructure()
        {
            Comparer = Comparer<T>.Default;
        }

        public QuickPopDataStructure(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        public void Push(T item)
        {
            throw new NotImplementedException();
        }

        public T Pop()
        {
            throw new NotImplementedException();
        }
    }
}