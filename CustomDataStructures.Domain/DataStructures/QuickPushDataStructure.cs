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
        private IComparer<T> Comparer { get; set; }

        public Node<T>? Current { get; set; }

        public QuickPushDataStructure()
        {
            Comparer = Comparer<T>.Default;
        }
        public QuickPushDataStructure(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

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
                        if (compRes == -1 || compRes == 0)
                        {
                            // currentLookup is smaller than biggest
                            currentLookup = currentLookup.Previous;
                        }
                        else if (compRes == 1)
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

                //Rewind();

                var res = biggest;

                if (biggest.Previous is not null)
                {
                    biggest.Previous.Next = biggest.Next; // tu się gubi bo Ann nie ma poprzednika, więc wszystko się pierdoli.
                    if (biggest.Equals(Current))
                    {
                        Current = biggest.Next is not null ? biggest.Next : biggest.Previous;
                    }
                }
                if (biggest.Next is not null)
                {
                    biggest.Next.Previous = biggest.Previous;
                }

                //Current = biggest.Previous is not null ? biggest.Previous : biggest.Next; // po dodaniu tej linii kasuje właściwie wszystkie wszystkie wpisy ale nie chce w ogóle sortować

                Console.WriteLine(Current);
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

        //public async Task<T> Pop()
        //{
        //    lock (_lock)
        //    {
        //        Node<T> greatest = Current;
        //        Node<T> currentLookup = Current;
        //        bool keepSearching = true; // is there any other left to lookup

        //        do
        //        {
        //            #region loop

        //            // Problem polega na tym że dochodzi do samego końca i wtedy nie widzi pozostałych już

        //            // I want to store reference to the greatest one, and if loop reaches an end of the data structure - then proceed to point 2, 
        //            // 1. identify greatest one:

        //            // 1.1. Mark Current as greatest one
        //            // 1.2. Check is Current null
        //                // 1.2.1. if yes => return default
        //                // 1.2.2. if not => Check is Current.Previous null
        //                    // 1.2.3. if yes => check is Current greater or equal than currentLookup
        //                    //              here loop should exit
        //                    // 1.2.4. if not => check is Current.Previous greater or equal than Current
        //                        // 1.2.5. if yes => assign Current.Previous to greatest
        //                        // 1.2.6. if not => assign Current.Previous to currentLookup
        //                        // 1.2.7. keepSearching = true;
        //            if(currentLookup is null) return default(T);
        //            else
        //            {
        //                if (currentLookup.Previous is null) // only one element in data structure
        //                {
        //                    keepSearching = false;
        //                }
        //                else
        //                {
        //                    if (currentLookup.CompareTo(greatest) == 1)
        //                    {
        //                        greatest = currentLookup;
        //                    }
        //                    // W przypadku duplikatu, czyli 6 6 powinien odpuścić sobie pierwszą 6 i zająć się następną currentLookup przypisaną poniżej, a ta już powinna mieć
        //                    // referencję do czego innego (9 bodajże)
        //                    currentLookup = currentLookup.Previous;
        //                }
        //            }
        //        } while (keepSearching);
                                
        //        // rewind the tape to very beginning
        //        if(greatest.Previous is null)
        //        {
        //            var isNext = greatest.Next is not null;
        //            do
        //            {
        //                if (Current?.Next is not null)
        //                {
        //                    currentLookup = currentLookup.Next;
        //                }
        //                else isNext = false;
        //            } while (isNext);
        //        }
        //        else if (greatest.Next is null)
        //        {
        //            var isPrevious = greatest.Previous is not null;
        //            do
        //            {
        //                if (currentLookup.Previous is not null)
        //                {
        //                    currentLookup = currentLookup.Previous;
        //                }
        //                else isPrevious = false;
        //            } while (isPrevious);
        //        }
        //        // Jeśli największy będzie przedostatni, to wszystko się spieprzy bo wtedy gubi referencje do pozostałych.
        //        if (greatest.Previous is not null)
        //        {
        //            greatest.Previous.Next = greatest.Next; // tu się gubi bo Ann nie ma poprzednika, więc wszystko się pierdoli.
        //        }
        //        if (greatest.Next is not null)
        //        {
        //            greatest.Next.Previous = greatest.Previous;
        //        }

        //        return greatest.Value;


        //        #endregion
        //        // 2. update the references in previous and next ones of the greatest one (make it to point null as next)
        //        // 3. Mark the previous of the greatest one as Current
        //        // 4. return the greatest one (it should be gone from the data structure now).

        //        //if (currentLookup is null) return default; // empty data structure, nothing to investigate.

        //        //greatest = currentLookup;
        //        //if (currentLookup.Previous is null) // no other values in dataStructure.
        //        //{

        //        //    keepSearching = false; // stop the loop
        //        //    return greatest.Value; // return the value
        //        //}
        //        //else
        //        //{
        //        //    var comparismentResult = greatest.CompareTo(currentLookup);
        //        //    if (comparismentResult > 0 ) // greatest bigger than currentLookup
        //        //    {
        //        //        currentLookup = currentLookup.Previous;
        //        //    }
        //        //    else // greatest smaller or equal to currentLookup
        //        //    {
        //        //        greatest = currentLookup;
        //        //    }
        //        //    currentLookup = currentLookup.Previous;
        //        //}
        //    }
        //}
    }
}
