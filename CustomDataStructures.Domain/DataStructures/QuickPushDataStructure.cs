﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataStructures.Domain.DataStructures
{
    public class QuickPushDataStructure<T> where T : IComparable<T>
    {
        private IComparer<T> Comparer { get; set; }

        public QuickPushDataStructure()
        {
            Comparer = Comparer<T>.Default;
        }
        public QuickPushDataStructure(IComparer<T> comparer)
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