using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataStructures.Domain.Models
{
    public class Node<T> : IComparable<Node<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Property linking to greater object / value
        /// If null - then a node represents currently greatest object / value
        /// </summary>
        public Node<T>? Next { get; set; }

        public T Value { get; set; }

        /// <summary>
        /// Property linking to smaller object / value
        /// If null - then a node represents currently greatest object / value
        /// </summary>
        public Node<T>? Previous { get; set; }
        public Node(T value, Node<T> next = null, Node<T>? previous = null)
        {
            Value = value;
            if (next != null)
            {
                Next = next;
            }
            if (previous != null)
            {
                Previous = previous;
            }
        }

        public int CompareTo(Node<T>? other)
        {
            if (other is null) return 1;
            else return Value.CompareTo(other.Value);
        }
    }
}
