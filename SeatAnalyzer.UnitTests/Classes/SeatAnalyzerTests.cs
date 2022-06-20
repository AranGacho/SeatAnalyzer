using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SeatAnalyzer.UnitTests.Classes
{
    [TestFixture]
    public class SeatAnalyzerTests
    {
        [Test]
        [TestCase(@"Input\GridOfSeatsInput.txt")]
        public void GetNumberOfOccupiedSeats_WhenStabilized_ReturnNumberOfSitsFilled(string path)
        {
            const int expectedResult = 2338;

            var result = SeatAnalyzer.GetNumberOfOccupiedSeats(path);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
