﻿using CustomDataStructures.Domain.Models;

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

        public void Push(QuickPopNode<T> newItem)
        {
            if(Greatest is null)
            {
                Greatest = newItem;
            }
            else
            {
                switch (Greatest.CompareTo(newItem))
                {
                    case -1:
                        Greatest.Next = newItem;
                        Greatest = newItem;
                        break;
                    case 0:
                        Greatest.Next = newItem;
                        Greatest = newItem;
                        break;
                    default:
                        // Greatest is bigger than item
                        var keepSearching = true;
                        var currentLookUp = Greatest.Previous; // in case there's only one element in the structure
                        if (currentLookUp is null)
                        {
                            Greatest.Previous = newItem;
                            newItem.Next = Greatest;
                        }
                        else
                        {
                            do
                            {
                                // Compare all of the elements in the data set, one after another. 
                                var isLookupSmaller = currentLookUp.CompareTo(newItem) < 0;
                                if (isLookupSmaller)
                                {
                                    // I assume that currentLookUp has been added earlier, so it has to contain value inside Next prop.
                                    newItem.Next = currentLookUp.Next;
                                    newItem.Previous = currentLookUp.Previous;
                                    currentLookUp.Next.Previous = newItem; 
                                    currentLookUp.Next = newItem;
                                    keepSearching = false;
                                }
                                else
                                {
                                    // currentLookUp is greater or equal to the newItem, keep searching
                                    currentLookUp = currentLookUp.Previous;
                                }
                            } while (keepSearching);
                        }
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