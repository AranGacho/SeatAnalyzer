using System;
using System.IO;

namespace SeatAnalyzer
{
    public class SeatAnalyzer
    {
 
        
        static void Main(string[] args)
        {

            string inputData = @"Input\GridOfSeatsInput.txt";


            Console.WriteLine(@"Number of Seats filled: {0}", GetNumberOfOccupiedSeats(inputData));

            Console.ReadLine();
        }

        public static int GetNumberOfOccupiedSeats(string inputData)
        {
            SeatingSystem seatStatus = new SeatingSystem(inputData);

            do
            {
                seatStatus.Simulate();
            } while (seatStatus.IsStable == false);

            return seatStatus.CountSeatsFilled();
        }


    }


}
