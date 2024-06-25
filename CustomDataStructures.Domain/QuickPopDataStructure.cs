namespace CustomDataStructures.Domain
{
    public class QuickPopDataStructure<T> where T : IComparable<T>
    {
        private IComparer<T> Comparer { get; set; }

        // HACK TODO: Is it allowed to create only one constructor which will enforce the user to specify, what IComparer<T> will be used?
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