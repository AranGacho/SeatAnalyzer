using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using SeatAnalyzer.Classes;

namespace SeatAnalyzer
{
    public class SeatingSystem
    {

        private char[,] _seatslayout;
        private IFileReader _fileReader;
        public List<Seat> Seats{ get; set; }
        public bool IsStable { get; set; } = true;
        public int SimulationCounter { get; set; }

        public SeatingSystem(string inputDataPath, IFileReader fileReader = null )
        {
            _fileReader = fileReader ?? new FileReader();
            Seats = new List<Seat>();
            MapSeatsLayout(_fileReader.ReadFile(inputDataPath));
        }



        public void Simulate()
        {
            try
            {
                UpdateSeatStatus();
                UpdateSeats();
            }
            catch(Exception ex)
            {
                Console.WriteLine(@"Error: {0}", ex.Message);
            }
            

            SimulationCounter++;
        }

        private void MapSeatsLayout(string [] seats)
        {
            var maxRow = seats.Length;
            var maxColumn = seats[0].Length;

            this._seatslayout = new char[maxRow, maxColumn];    

            for(int row = 0; row < maxRow; row++)
            {
                var seatPerColumn = seats[row].ToCharArray();
                for(int col = 0; col< maxColumn; col++)
                {
                    this._seatslayout[row, col] = seatPerColumn[col];

                    Seats.Add(new Seat(seatPerColumn[col], row, col, _seatslayout.GetLength(0), _seatslayout.GetLength(1)));

                }
            }
        }

        private void UpdateSeatStatus()
        {
            IsStable = true;

            foreach (var seat in Seats)
            {
                var adjSeats = seat.GetAdjacentSeats(_seatslayout);

                switch (seat.state)
                { 
                    case (char) SeatStatus.Filled:
                        if (adjSeats.Where(s => s == (char)SeatStatus.Filled).Count() >= 4)
                        {
                            seat.state = (char)SeatStatus.Empty;
                            IsStable = false;
                        }
                        break;
                    case (char)SeatStatus.Empty:
                        if (adjSeats.Contains((char)SeatStatus.Filled) == false)
                        {
                            seat.state = (char)SeatStatus.Filled;
                            IsStable = false;
                        }
                        break;
                    default:   
                        break;
                }

            }
        }

        private void UpdateSeats()
        {
            foreach(var seat in Seats)
            {
                this._seatslayout[seat.Point.X, seat.Point.Y] = seat.state;
            }
        }
        

        //public void PrintGrid()
        //{
        //    for (int curRow = 0; curRow < _seatslayout.GetLength(0); curRow++)
        //    {
        //        for (int curCol = 0; curCol < _seatslayout.GetLength(1); curCol++)
        //        {
        //            Console.WriteLine(_seatslayout[curRow, curCol] + "");
        //        }
        //        Console.WriteLine();

        //    }
        //    Console.WriteLine("\n");
        //}


        public int CountSeatsFilled()
        {

            return Seats.Where(s => s.state == (char)SeatStatus.Filled).Select(s => s).Count(); 
        }

    }
}
