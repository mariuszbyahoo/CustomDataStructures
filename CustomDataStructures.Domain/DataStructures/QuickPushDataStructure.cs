using CustomDataStructures.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataStructures.Domain.DataStructures
{
    public class QuickPushDataStructure<T> where T : IComparable<T>
    {
        private static readonly object _lock = new object();
        public Node<T>? Current { get; set; }

        public async Task Push(T item)
        {
            lock (_lock)
            {
                var newNode = new Node<T>(item);
                if (Current is null) Current = newNode;
                else
                {
                    Current.Next = newNode;
                    newNode.Previous = Current;
                    Current = newNode;
                }
            }
        }

        public async Task<T> Pop()
        {
            lock (_lock)
            {
                if (Current is null) return default;
                var biggest = Current;
                var keepLooking = true;
                var currentLookup = Current?.Previous;
                while (keepLooking)
                {
                    if (Current?.Previous is null && Current?.Next is not null)
                    {
                        Rewind();
                    }
                    if (currentLookup is not null)
                    {
                        var compRes = currentLookup.CompareTo(biggest);
                        if (compRes <= 0)
                        {
                            // currentLookup is smaller or equal to biggest
                            currentLookup = currentLookup.Previous;
                        }
                        else 
                        {
                            // Found bigger number
                            biggest = currentLookup;
                        }
                    }
                    else
                    {
                        keepLooking = false;
                    }
                }

                var res = biggest;

                if (biggest.Previous is not null)
                {
                    biggest.Previous.Next = biggest.Next;
                    if (biggest.Equals(Current))
                    {
                        Current = biggest.Next is not null ? biggest.Next : biggest.Previous;
                    }
                }
                if (biggest.Next is not null)
                {
                    biggest.Next.Previous = biggest.Previous;
                }

                if (biggest.Next is null && biggest.Previous is null)
                {
                    // last element being popped
                    Current = default;
                }
                return res.Value;
            }
        }

        private void Rewind()
        {
            var keepRewinding = true;
            while (keepRewinding)
            {
                if (Current?.Next is not null)
                {
                    Current = Current.Next;
                }
                else keepRewinding = false;
            }
        }
    }
}
