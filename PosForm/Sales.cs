using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosForm
{
    public class Sales
    {
        public int friedsum {  get; set; }
        public int redsum { get; set; }
        public int soysum {  get; set; }
        public int kkanpungsum { get; set; } 
        public int tangsusum {  get; set; }
        public int onionsum { get; set; }

        public int frenchsum { get; set; }
        public int gizzardsum { get; set; }
        public int sojusum { get; set; }
        public int beersum { get; set; }
        public int smalldrinksum { get; set; }
        public int bigdrinksum { get; set; }
        public int amount { get; set; }
        public int Amount() 
        {
            amount = friedsum + redsum + soysum + kkanpungsum + tangsusum + onionsum + frenchsum + gizzardsum + sojusum  + beersum + smalldrinksum + bigdrinksum ;
            return amount;
        }
    }
}
