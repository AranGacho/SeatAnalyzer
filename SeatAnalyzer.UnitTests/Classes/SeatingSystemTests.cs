using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using SeatAnalyzer.Classes;
using Moq;
namespace SeatAnalyzer.UnitTests.Classes
{
    public class SeatingSystemTests
    {
        private SeatingSystem _seatingSystem;

        [SetUp]
        public void Setup()
        {
            
            string inputData = @"Input\GridOfSeatsTest.txt";

            var mFileReader = new Mock<IFileReader>();
            mFileReader.Setup(fr => fr.ReadFile(inputData)).Returns(File.ReadAllLines(inputData));
            _seatingSystem = new SeatingSystem(inputData, mFileReader.Object);
        }

        [Test]
        
        public void Simulate_WhenCalled_IncrementSimulationCounter()
        {


            _seatingSystem.Simulate();

            Assert.That(_seatingSystem.SimulationCounter, Is.Not.EqualTo(0));


        }

        [Test]
        public void CountSeatsFilled_WhenCalled_ReturnNumberOfSeatsFilled()
        {
            _seatingSystem.Simulate();
            

            Assert.That(_seatingSystem.CountSeatsFilled, Is.EqualTo(37));
        }
    }
}
