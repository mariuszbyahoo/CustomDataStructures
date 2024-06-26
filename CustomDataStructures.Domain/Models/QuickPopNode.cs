using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataStructures.Domain.Models
{
    public class QuickPopNode<T> : IComparable<QuickPopNode<T>> where T : class, IComparable<T>
    {
        /// <summary>
        /// Property linking to greater object / value
        /// If null - then a node represents currently greatest object / value
        /// </summary>
        public QuickPopNode<T>? Next { get; set; }

        public T Value { get; set; }

        /// <summary>
        /// Property linking to smaller object / value
        /// If null - then a node represents currently greatest object / value
        /// </summary>
        public QuickPopNode<T>? Previous { get; set; }
        public QuickPopNode(QuickPopNode<T> next = null, QuickPopNode<T>? previous = null)
        {
            if (next != null)
            {
                Next = next;
            }
            if (previous != null)
            {
                Previous = previous;
            }
        }

        public int CompareTo(QuickPopNode<T>? other)
        {
            if (other is null) return 1;
            else return Value.CompareTo(other.Value);
        }

        /* Jeśli to tak zrobię, wtedy datastructure będzie miał tylko referencję do największego z nich 
         * - wtedy od strzała będzie mógł wyciągnąć największą liczbę, a dodawanie będzie się odbywało poprzez
         * porównanie po kolei od największego do najmniejszego, aby wykonać push
         * można będzie wywołać tę metodę na największym, wtedy po prostu porówna się on z argumentem
         * i jeśli jest mniejszy od nowego największego, to się zmieni po prostu referencję w QuickPopDataStructure na nowy obiekt i tyle*/
    }
}
