﻿/*
    HumanShufflingTests.cs

    The unit testing package for LibraryFunctions.cs for HumanShufflingLib

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
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kesyhara.HumanShuffling.LibraryFunctions;

namespace Kesyhara.HumanShuffling.UnitTests
{
    [TestClass]
    public class HumanShufflingTests
    {
        [TestCategory("Mock List")]
        [TestMethod]
        public void GenerateMockListDoesNotReturnNull()
        {
            ICollection<int> testList = GenerateMockListOfIntegers(5);

            Assert.IsNotNull(testList);
        }

        [TestCategory("Mock List")]
        [TestMethod]
        public void GenerateMockListReturnsCorrectListOfIntegers()
        {
            ICollection<int> testList = GenerateMockListOfIntegers(5);

            List<int> correctList = new List<int> { 1, 2, 3, 4, 5 };

            Assert.IsTrue(correctList.SequenceEqual(testList));
        }

        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleDoesNotReturnNull()
        {
            ICollection<int> shuffledList = GenerateMockListOfIntegers(5).MongeanShuffle<int>();

            Assert.IsNotNull(shuffledList);
        }

        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleMaintainsCount()
        {
            ICollection<int> shuffledList = GenerateMockListOfIntegers(5).MongeanShuffle<int>();

            Assert.AreEqual(shuffledList.Count, GenerateMockListOfIntegers(5).Count);
        }

        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleReordersSequence()
        {
            ICollection<int> shuffledList = GenerateMockListOfIntegers(5).MongeanShuffle<int>();

            Assert.IsTrue(!shuffledList.SequenceEqual(GenerateMockListOfIntegers(5)));
        }

        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleGeneratesCorrectSequence()
        {
            ICollection<int> shuffledList = GenerateMockListOfIntegers(5).MongeanShuffle<int>();

            List<int> correctListOrder = new List<int> { 4, 2, 1, 3, 5 };

            Assert.IsTrue(shuffledList.SequenceEqual(correctListOrder));
        }

        public ICollection<int> GenerateMockListOfIntegers(int listLength)
        {
            List<int> returnList = new List<int>();

            for (int i = 1; i <= listLength; i++)
                returnList.Add(i);

            return returnList;
        }
    }
}