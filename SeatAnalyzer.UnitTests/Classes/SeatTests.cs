using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using System.IO;
namespace SeatAnalyzer.UnitTests.Classes
{
   
    public class SeatTests
    {
        [TestFixture]
        public class SeatAddAdjacentPointsTests
        {
            private Seat _seat;

            [SetUp]
            public void Setup()
            {
                _seat = new Seat();
            }

            [Test]
            [Category("AddAdjacentPoint")]
            [TestCase(1, 2)]
            [TestCase(3, 3)]
            public void AddAjacentPoints_AddingPostionOfAdjacentSeats_ListShouldNotBeEmpty(int row, int col)
            {
                _seat.AdjacentSeatsPosition.Add(new Point(row, col));

                Assert.That(_seat.AdjacentSeatsPosition, Is.Not.Empty);
            }

            [Test]
            [Category("AddAdjacentPoint")]
            [TestCase(1, 1, 0, 0)]
            [TestCase(10, 10, 10, 11)]
            public void AddAdjacentPoint_ArgumentIsOutOfRange_ThrowArgumentOutOfRangeException(int row, int col, int maxRow, int maxCol)
            {
                var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _seat.AddAdjacentPoints(row, col, maxRow, maxCol));
                Assert.That(ex.Message, Does.Contain("Out of maximum Range Error!"));
            }

            [Test]
            [Category("AddAdjacentPoint")]
            [TestCase(-1, -1, 2, 2)]
            [TestCase(1, -1, 2, 2)]
            [TestCase(-1, 1, 2, 2)]
            public void AddAdjacentPoint_InvalidValueRowAndColumn_ThrowsIndexOutOfRangeException(int row, int col, int maxRow, int maxCol)
            {
                var ex = Assert.Throws<IndexOutOfRangeException>(() => _seat.AddAdjacentPoints(row, col, maxRow, maxCol));
                Assert.That(ex.Message, Does.Contain("Row and Column can't be less than zero!"));
            }
        }

        [TestFixture]
        public class SeatGetAjacentSeatsTests
        {
            private Seat _seat;

            [SetUp]
            public void Setup()
            {
                _seat = new Seat();
            }

            [Test]
            public void GetAdjacentSeats_WhenCalled_ReturnListOfAdjacentSeats()
            {
                char[,] inputData = { { '#', '.', '#' }, { '.', '#', 'E' }, { 'E', '#', '#' } };
                List<char> expectedResult = new List<char> { '#', '.', '#', '.', 'E', 'E', '#', '#' };

                _seat.AdjacentSeatsPosition.Add(new Point(0, 0));
                _seat.AdjacentSeatsPosition.Add(new Point(0, 1));
                _seat.AdjacentSeatsPosition.Add(new Point(0, 2));
                _seat.AdjacentSeatsPosition.Add(new Point(1, 0));
                _seat.AdjacentSeatsPosition.Add(new Point(1, 2));
                _seat.AdjacentSeatsPosition.Add(new Point(2, 0));
                _seat.AdjacentSeatsPosition.Add(new Point(2, 1));
                _seat.AdjacentSeatsPosition.Add(new Point(2, 2));


                var result = _seat.GetAdjacentSeats(inputData);

                Assert.That(expectedResult, Is.EquivalentTo(result));
            }

            [Test]
            public void GetAdjacentSeats_HasValue_ReturnNumberOfItems()
            {
                char[,] inputData = { { '#', '.', '#' }, { '.', '#', 'E' }, { 'E', '#', '#' } };

                _seat.AdjacentSeatsPosition.Add(new Point(0, 0));
                _seat.AdjacentSeatsPosition.Add(new Point(2, 2));



                var result = _seat.GetAdjacentSeats(inputData);

                Assert.That(result.Count, Is.EqualTo(2));

            }

        }


        [TestFixture]
        public class SeatIsValidAdjacentNode
        {
            private Seat _seat;

            [SetUp]
            public void Setup()
            {
                _seat = new Seat();
            }

            [Test]
            [TestCase(0, 0, 2, 2)]
            [TestCase(0, 1, 2, 2)]
            [TestCase(1, 0, 2, 2)]
            public void IsValidAdjacentNode_Valid_ReturnTrue(int row, int col, int maxRow, int maxCol)
            {
                var result = _seat.IsValidAdjacentNode(row, col, maxRow, maxCol);

                Assert.That(result, Is.EqualTo(true));
            }

            [Test]
            [TestCase(-1, -1, 2, 2)]
            [TestCase(2, 2, 2, 2)]
            [TestCase(2, 3, 2, 2)]
            [TestCase(3, 2, 2, 2)]
            [TestCase(3, 3, 2, 2)]
            public void IsValidAdjacentNode_Invalid_ReturnFalse(int row, int col, int maxRow, int maxCol)
            {
                var result = _seat.IsValidAdjacentNode(row, col, maxRow, maxCol);

                Assert.That(result, Is.EqualTo(false));
            }
        }
    }
    
}
