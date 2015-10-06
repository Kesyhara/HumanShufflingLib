/*
    Utils.cs

    Contains a number of utility functions designed to make the
    author's life easier and his code more readable.
    
    Copyright 2015 Kenneth Syharath

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kesyhara.HumanShuffling.Utils
{
    public static class Utils
    {
        public static Boolean CountIsEven<T>(this ICollection<T> collection)
        {
            return (collection.Count % 2) == 0;
        }

        public static Boolean CountIsEven<T>(this Queue<T> queue)
        {
            return (queue.Count % 2) == 0;
        }

        public static Boolean IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static Boolean IsEmpty<T>(this Queue<T> queue)
        {
            return queue.Count == 0;
        }

        public static Boolean IsEmpty<T>(this Stack<T> stack)
        {
            return stack.Count == 0;
        }

        public static Boolean IntegerIsEven(int arg)
        {
            return arg % 2 == 0;
        }

        public static void SplitCollectionInHalf<T>(ICollection<T> collectionToSplit,
            out ICollection<T> firstHalf, out ICollection<T> secondHalf)
        {
            int midpoint = collectionToSplit.Count / 2;

            if (!collectionToSplit.CountIsEven())
                midpoint++; //We add one in the odd case to ensure we capture all elements.

            firstHalf = new List<T>(collectionToSplit.Take(midpoint));
            secondHalf = new List<T>(collectionToSplit.Skip(midpoint).Take(midpoint));
        }
    }
}
