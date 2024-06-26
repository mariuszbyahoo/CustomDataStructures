using CustomDataStructures.Domain.Models;

namespace CustomDataStructures.Domain.DataStructures
{
    public class QuickPopDataStructure<T> where T : class, IComparable<T>
    {
        /// <summary>
        /// Is this necessary?
        /// </summary>
        private IComparer<T> Comparer { get; set; }
        private QuickPopNode<T>? Greatest { get; set; }

        public QuickPopDataStructure()
        {
            // HACK is this necessary?
            Comparer = Comparer<T>.Default;
        }

        public QuickPopDataStructure(IComparer<T> comparer)
        {
            // HACK is this necessary?
            Comparer = comparer;
        }

        public void Push(QuickPopNode<T> item)
        {
            if(Greatest is null)
            {
                Greatest = item;
            }
            else
            {
                switch (Greatest.CompareTo(item))
                {
                    case -1:
                        Greatest.Next = item;
                        Greatest = item;
                        break;
                    case 0:
                        Greatest.Next = item;
                        Greatest = item;
                        break;
                    default:

                        /* Greatest jest większy 
                         *
                         *
                         */
                        break;
                }
            }
            /* Jeśli to tak zrobię, wtedy datastructure będzie miał tylko referencję do największego z nich 
             * - wtedy od strzała będzie mógł wyciągnąć największą liczbę, a dodawanie będzie się odbywało poprzez
             * porównanie po kolei od największego do najmniejszego, aby wykonać push
             * można będzie wywołać tę metodę na największym, wtedy po prostu porówna się on z argumentem
             * i jeśli jest mniejszy od nowego największego, to się zmieni po prostu referencję w QuickPopDataStructure na nowy obiekt i tyle */
            throw new NotImplementedException();

        }

        /// <summary>
        /// Returns greatest object stored in this DataStructure
        /// </summary>
        /// <returns>If DataStructure is empty, then returns null, otherwise - returns greatest object stored</returns>
        public T? Pop()
        {
            return Greatest?.Value;
        }
    }
}