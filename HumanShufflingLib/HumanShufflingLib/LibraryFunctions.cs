/*
    LibraryFunctions.cs

    Provides a number of ICollection extension methods that simulate
    the various methods of shuffling that humans use with decks of cards.
    
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
using System.Text;
using System.Threading.Tasks;

using Kesyhara.HumanShuffling.Utils;

namespace Kesyhara.HumanShuffling.LibraryFunctions
{
    public static class HumanShuffling_LibraryFunctions
    {
        public static ICollection<T> MongeanShuffle<T>(this ICollection<T> collectionToShuffle, int iterationCount = 1)
        {
            ICollection<T> listToShuffle = HandleMultipleIterations<T>(HumanShuffling_LibraryFunctions.MongeanShuffle<T>, collectionToShuffle, iterationCount);

            LinkedList<T> returnList;
            Queue<T> dealingList;

            SetupMongeanShuffle(out returnList, out dealingList, listToShuffle);

            while (!dealingList.IsEmpty())
                HandleMongeanStep(returnList, dealingList);

            return returnList;
        }

        private static void SetupMongeanShuffle<T>(out LinkedList<T> theReturnList, out Queue<T> queueToDealFrom, ICollection<T> listToDealFrom)
        {
            theReturnList = new LinkedList<T>();
            queueToDealFrom = new Queue<T>(listToDealFrom);
            theReturnList.AddFirst(queueToDealFrom.Dequeue());
        }

        private static void HandleMongeanStep<T>(LinkedList<T> targetList, Queue<T> dealingList)
        {
            if (!targetList.CountIsEven())
                targetList.AddFirst(dealingList.Dequeue());
            else
                targetList.AddLast(dealingList.Dequeue());
        }

        private static ICollection<T> HandleMultipleIterations<T>(Func<ICollection<T>, int, ICollection<T>> shuffleToApply, ICollection<T> collectionToShuffle, int iterationsRemaining)
        {
            ICollection<T> returnCollection = collectionToShuffle;

            if (iterationsRemaining > 1)
            {
                returnCollection = HandleMultipleIterations(shuffleToApply, collectionToShuffle, iterationsRemaining - 1);
                returnCollection = shuffleToApply(collectionToShuffle, 1);
            }

            return returnCollection;
        }
    }
}
