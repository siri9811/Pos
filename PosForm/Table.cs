using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosForm
{
    public class Table:Order
    {
        public int SeatNumber { get; set; }
        public int sum { get; set; }

        public Table(int seatNumber)
        {
            SeatNumber = seatNumber;
            sum = 0;
        }
        
        public int Sum()
        {
            sum = Friedsum + Redsum + Soysum + Kkanpungsum + Tangsusum + Onionsum + Frenchsum + Gizzardsum + Sojusum + Beersum + SmallDrinksum + BigDrinksum;
            return sum;
        }
        
    }
}
