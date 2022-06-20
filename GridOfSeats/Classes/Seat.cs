using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SeatAnalyzer
{
    public class Seat
    {

        public Point Point { get; set; }
        public char state { get; set; }
        public List<Point> AdjacentSeatsPosition { get; set; }

        private List<Point> _adjacentPoints = new List<Point>()
        { new Point(1,0),
          new Point(-1,0),
          new Point(0,1),
          new Point(0,-1),
          new Point(1,1),
          new Point(-1,-1),
          new Point(1,-1),
          new Point(-1, 1)
        };

        public Seat()
        {
            AdjacentSeatsPosition = new List<Point>();
        }

        public Seat(char state, int x, int y, int maxRow, int maxCol)
        {
            this.state = state;
            this.Point = new Point(x, y);
            AdjacentSeatsPosition = new List<Point>();
            AddAdjacentPoints(x, y, maxRow, maxCol);  
        }

        public void AddAdjacentPoints( int row, int col, int maxRow, int maxCol)
        {

            if ((row >= maxRow) || (col >= maxCol))
                throw new ArgumentOutOfRangeException("Out of maximum Range Error!");
                
            if ((row < 0) || (col < 0))
                throw new IndexOutOfRangeException("Row and Column can't be less than zero!");

              

            foreach (var point in _adjacentPoints)
            {
                //calculate adjacent points
                var pointX = row + point.X;
                var pointY = col + point.Y;

                if (IsValidAdjacentNode(pointX, pointY, maxRow, maxCol))
                {
                    AdjacentSeatsPosition.Add(new Point(pointX, pointY));
                }



            }
        }

        public List<char> GetAdjacentSeats(char[,] gridData)
        {
            List<char> result = new List<char>();

            foreach(var point in AdjacentSeatsPosition)
            {
                result.Add(gridData[point.X, point.Y]);
            }

            return result;
        }

        public bool IsValidAdjacentNode(int pointX, int pointY, int maxRow, int maxCol)
        {
            return ((pointX >= 0 && pointY >= 0) && (pointX < maxRow && pointY < maxCol)) ? true : false;
        }
    }


    

    internal enum SeatStatus
    {
        Filled = '#',
        Empty = 'E',
        Constant = '.'
    }
}
