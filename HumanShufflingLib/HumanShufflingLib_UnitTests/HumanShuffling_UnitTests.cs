/*
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
using Kesyhara.HumanShuffling.UtilityFunctions;
using MyUtils = Kesyhara.HumanShuffling.UtilityFunctions.Utils;

namespace Kesyhara.HumanShuffling.UnitTests
{
    [TestClass]
    public class HumanShufflingTests
    {
        #region Mock List
        [TestCategory("Mock List")]
        [TestMethod]
        public void GenerateMockListDoesNotReturnNull()
        {
            ICollection<int> testList = MyUtils.GenerateListOfIntegers(5);

            Assert.IsNotNull(testList);
        }

        [TestCategory("Mock List")]
        [TestMethod]
        public void GenerateMockListReturnsCorrectListOfIntegers()
        {
            ICollection<int> testList = MyUtils.GenerateListOfIntegers(5);

            List<int> correctList = new List<int> { 1, 2, 3, 4, 5 };

            Assert.IsTrue(correctList.SequenceEqual(testList));
        }
        #endregion

        #region Mongean
        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleDoesNotReturnNull()
        {
            ICollection<int> shuffledList = MyUtils.GenerateListOfIntegers(5).MongeanShuffle<int>();

            Assert.IsNotNull(shuffledList);
        }

        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleMaintainsCount()
        {
            ICollection<int> shuffledList = MyUtils.GenerateListOfIntegers(5).MongeanShuffle<int>();

            Assert.AreEqual(shuffledList.Count, MyUtils.GenerateListOfIntegers(5).Count);
        }

        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleReordersSequence()
        {
            ICollection<int> shuffledList = MyUtils.GenerateListOfIntegers(5).MongeanShuffle<int>();

            Assert.IsTrue(!shuffledList.SequenceEqual(MyUtils.GenerateListOfIntegers(5)));
        }

        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleGeneratesCorrectSequenceOnOneIteration()
        {
            ICollection<int> shuffledList = MyUtils.GenerateListOfIntegers(5).MongeanShuffle<int>();

            List<int> correctListOrder = new List<int> { 4, 2, 1, 3, 5 };

            Assert.IsTrue(shuffledList.SequenceEqual(correctListOrder));
        }

        [TestCategory("Mongean")]
        [TestMethod]
        public void MongeanShuffleGeneratesCorrectSequenceOnTwoIterations()
        {
            ICollection<int> shuffledList = MyUtils.GenerateListOfIntegers(5).MongeanShuffle<int>(2);

            List<int> correctListOrder = new List<int> { 3, 2, 4, 1, 5 };

            Assert.IsTrue(shuffledList.SequenceEqual(correctListOrder));
        }
        #endregion

        #region Riffle
        [TestCategory("Riffle")]
        [TestMethod]
        public void RiffleShuffleDoesNotReturnNull()
        {
            ICollection<int> testList = MyUtils.GenerateListOfIntegers(5).RiffleShuffle<int>();

            Assert.IsNotNull(testList);
        }

        [TestCategory("Riffle")]
        [TestMethod]
        public void RiffleShuffleMaintainsCount()
        {
            ICollection<int> testList = MyUtils.GenerateListOfIntegers(5).RiffleShuffle<int>();

            Assert.AreEqual(testList.Count, MyUtils.GenerateListOfIntegers(5).Count);
        }

        [TestCategory("Riffle")]
        [TestMethod]
        public void RiffleShuffleReordersSequence()
        {
            ICollection<int> testList = MyUtils.GenerateListOfIntegers(5).RiffleShuffle<int>();

            Assert.IsTrue(!testList.SequenceEqual(MyUtils.GenerateListOfIntegers(5)));
        }
        #endregion

        #region Overhand
        [TestCategory("Overhand")]
        [TestMethod]
        public void OverhandShuffleDoesNotReturnNull()
        {
            ICollection<int> testList = MyUtils.GenerateListOfIntegers(52).ApplyOverhandShuffle<int>();

            Assert.IsNotNull(testList);
        }

        [TestCategory("Overhand")]
        [TestMethod]
        public void OverhandShuffleMaintainsCount()
        {
            ICollection<int> testList = MyUtils.GenerateListOfIntegers(52).ApplyOverhandShuffle<int>();

            Assert.AreEqual(testList.Count, MyUtils.GenerateListOfIntegers(52).Count);
        }

        [TestCategory("Overhand")]
        [TestMethod]
        public void OverhandShuffleReordersSequence()
        {
            ICollection<int> testList = MyUtils.GenerateListOfIntegers(52).ApplyOverhandShuffle<int>();

            Assert.IsTrue(!testList.SequenceEqual(MyUtils.GenerateListOfIntegers(52)));
        }
        #endregion
    }
}
