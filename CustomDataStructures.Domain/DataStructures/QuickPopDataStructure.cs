using CustomDataStructures.Domain.Models;
using CustomDataStructures.DTOs;

namespace CustomDataStructures.Domain.DataStructures
{
    public class QuickPopDataStructure<T> where T : IComparable<T>
    {
        private QuickPopNode<T>? Greatest { get; set; }
        private static readonly object _lock = new object();

        public async Task Push(T newItem)
        {
            lock (_lock)
            {
                var newNode = new QuickPopNode<T>(newItem);
                if (Greatest is null)
                {
                    Greatest = newNode;
                }
                else
                {
                    switch (Greatest.CompareTo(newNode))
                    {
                        case -1:
                            Greatest.Next = newNode;
                            newNode.Previous = Greatest;
                            Greatest = newNode;
                            break;
                        case 0:
                            Greatest.Next = newNode;
                            newNode.Previous = Greatest;
                            Greatest = newNode;
                            break;
                        default:
                            // Greatest is bigger than item
                            var keepSearching = true;
                            var currentLookUp = Greatest.Previous; // in case there's only one element in the structure
                            if (currentLookUp is null)
                            {
                                Greatest.Previous = newNode;
                                newNode.Next = Greatest;
                            }
                            else
                            {
                                do
                                {
                                    // Compare all of the elements in the data set, one after another.
                                    var compareResult = currentLookUp.CompareTo(newNode);
                                    if (compareResult == -1 || compareResult == 0)
                                    {
                                        // I assume that currentLookUp has been added earlier, so it has to contain value inside Next prop.
                                        newNode.Next = currentLookUp.Next;
                                        newNode.Previous = currentLookUp;
                                        currentLookUp.Next.Previous = newNode;
                                        currentLookUp.Next = newNode;
                                        keepSearching = false;
                                    }
                                    else
                                    {
                                        // currentLookUp is greater or equal to the newItem, keep searching
                                        // jeśli currentLookUp.Previous is null, wtedy umieść na koniec.
                                        if (currentLookUp.Previous is null)
                                        {
                                            currentLookUp.Previous = newNode;
                                            newNode.Next = currentLookUp;
                                            keepSearching = false;
                                        }
                                        else
                                        {
                                            currentLookUp = currentLookUp.Previous;
                                        }
                                    }
                                } while (keepSearching);
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns greatest object stored in this DataStructure
        /// </summary>
        /// <returns>If DataStructure is empty, then returns null, otherwise - returns greatest object stored</returns>
        public async Task<T?> Pop()
        {
            lock (_lock)
            {
                if (Greatest is null) return default; // empty collection
                var result = Greatest.Value;
                Greatest = Greatest.Previous is null ? default : Greatest.Previous;
                if (Greatest is not null) Greatest.Next = default;
                return result;
            }
        }
    }
}