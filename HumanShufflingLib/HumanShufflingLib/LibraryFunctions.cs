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

using Kesyhara.HumanShuffling.UtilityFunctions;

using MyUtils = Kesyhara.HumanShuffling.UtilityFunctions.Utils;

namespace Kesyhara.HumanShuffling.LibraryFunctions
{
    public static class HumanShuffling_LibraryFunctions
    {
        #region Mongean
        public static ICollection<T> ApplyMongeanShuffle<T>(this ICollection<T> collectionToShuffle, int iterationCount = 1)
        {
            ICollection<T> shuffledCollection = new List<T>(collectionToShuffle);

            for (int iterationsLeft = iterationCount; iterationsLeft > 0; iterationsLeft--)
                shuffledCollection = MongeanShuffle(shuffledCollection);

            return shuffledCollection;
        }

        public static ICollection<T> MongeanShuffle<T>(ICollection<T> collectionToShuffle)
        {
            LinkedList<T> returnList;
            Queue<T> dealingQueue;

            SetupMongeanShuffle(out returnList, out dealingQueue, collectionToShuffle);

            while (!dealingQueue.IsEmpty())
                MongeanStep(returnList, dealingQueue);

            return returnList;
        }

        private static void SetupMongeanShuffle<T>(out LinkedList<T> targetList, out Queue<T> sourceQueue, ICollection<T> sourceList)
        {
            targetList = new LinkedList<T>();
            sourceQueue = new Queue<T>(sourceList);
            targetList.AddFirst(sourceQueue.Dequeue());
        }

        private static void MongeanStep<T>(LinkedList<T> targetList, Queue<T> sourceList)
        {
            if (!targetList.CountIsEven())
                targetList.AddFirst(sourceList.Dequeue());
            else
                targetList.AddLast(sourceList.Dequeue());
        }
        #endregion

        #region Riffle
        public static ICollection<T> ApplyRiffleShuffle<T>(this ICollection<T> collectionToShuffle, int iterationCount = 1)
        {
            ICollection<T> shuffledCollection = new List<T>(collectionToShuffle);

            for (int iterationsLeft = iterationCount; iterationsLeft > 0; iterationsLeft--)
                shuffledCollection = RiffleShuffle(shuffledCollection);

            return shuffledCollection;
        }

        private static ICollection<T> RiffleShuffle<T>(ICollection<T> collectionToShuffle)
        {
            ICollection<T> targetList = new List<T>(collectionToShuffle.Count);
            Stack<T> topHalf, botHalf;

            SetupRiffleShuffle(out topHalf, out botHalf, collectionToShuffle);

            CallRiffleFunctionsInOrder(topHalf, botHalf, targetList);

            return targetList;
        }

        private static void SetupRiffleShuffle<T>(out Stack<T> topHalf, out Stack<T> botHalf, ICollection<T> sourceCollection)
        {
            ICollection<T> firstList, secondList;

            MyUtils.SplitCollectionInHalf<T>(sourceCollection, out firstList, out secondList);

            topHalf = new Stack<T>(firstList);
            botHalf = new Stack<T>(secondList);
        }

        private static void CallRiffleFunctionsInOrder<T>(Stack<T> topHalf, Stack<T> botHalf, ICollection<T> targetList)
        {
            RiffleLoop(topHalf, botHalf, targetList);

            RiffleFinalize(topHalf, botHalf, targetList);
        }

        private static void RiffleLoop<T>(Stack<T> topHalf, Stack<T> botHalf, ICollection<T> targetList)
        {
            while (!topHalf.IsEmpty() && !botHalf.IsEmpty())
                RiffleStep(topHalf, botHalf, targetList);
        }

        private static void RiffleStep<T>(Stack<T> topHalf, Stack<T> botHalf, ICollection<T> targetList)
        {
            Random rng = new Random();

            if (MyUtils.IntegerIsEven(rng.Next()))
                targetList.Add(topHalf.Pop());
            else
                targetList.Add(botHalf.Pop());
        }

        private static void RiffleFinalize<T>(Stack<T> topHalf, Stack<T> botHalf, ICollection<T> targetList)
        {
            Stack<T> stackToEmpty = DetermineEmptyStack(topHalf, botHalf);

            while (!stackToEmpty.IsEmpty())
                targetList.Add(stackToEmpty.Pop());
        }

        private static Stack<T> DetermineEmptyStack<T>(Stack<T> firstStack, Stack<T> secondStack)
        {
            Stack<T> returnValue;

            if (firstStack.IsEmpty())
                returnValue = secondStack;
            else
                returnValue = firstStack;

            return returnValue;
        }
        #endregion

        #region Overhand
        public static ICollection<T> ApplyOverhandShuffle<T>(this ICollection<T> collectionToShuffle, int iterationCount = 1)
        {
            ICollection<T> shuffledCollection = new List<T>(collectionToShuffle);

            for (int iterationsLeft = iterationCount; iterationsLeft > 0; iterationsLeft--)
                shuffledCollection = OverhandShuffle(shuffledCollection);

            return shuffledCollection;
        }

        private static ICollection<T> OverhandShuffle<T> (ICollection<T> collectionToShuffle)
        {
            ICollection<T> listToReadFrom = new List<T>(collectionToShuffle), listToReturn = new List<T>(collectionToShuffle.Count);
            Random ranGen = new Random();
            
            while (!listToReadFrom.IsEmpty())
                OverhandStep<T>(ref listToReadFrom, ref listToReturn, CalculateGaussianSliceSize(collectionToShuffle.Count, ranGen));

            return listToReturn;
        }

        private static void OverhandStep<T>(ref ICollection<T> sourceList, ref ICollection<T> targetList, int sliceSize)
        {
            targetList = new List<T>(sourceList.Take(sliceSize).Concat(targetList));
            sourceList = new List<T>(sourceList.Skip(sliceSize));
        }

        private static int CalculateGaussianSliceSize(int originalListSize, Random ranGen)
        {
            double gaussianVariable = ranGen.NextGaussian(0.5, 0.2);
            int gaussianSliceSize = (int) (originalListSize * Math.Abs(gaussianVariable) / 2);
            return gaussianSliceSize;
        }
        #endregion

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
