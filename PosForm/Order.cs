using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosForm
{
    public class Order
    {
        
        public int FriedChicken {  get; set; }
        public int FriedCount = 1;
        public int RedChicken { get; set; }
        public int RedCount = 1;
        public int SoyChicken { get; set; }
        public int SoyCount = 1;
        public int KkanpungChicken { get; set; }
        public int KkanpungCount = 1;
        public int TangsuChicken { get; set; }
        public int TangsuCount = 1;
        public int OnionChicken { get; set; }
        public int OnionCount = 1;
        public int FrenchFries { get; set; }
        public int FrenchCount = 1;
        public int GizzardFries { get; set; }
        public int GizzardCount = 1;
        public int Soju { get; set; }
        public int SojuCount = 1;

        public int Beer { get; set; }
        public int BeerCount = 1;

        public int SmallDrink { get; set; }
        public int SmallDrinkCount = 1;    
        public int BigDrink { get; set; }
        public int BigDrinkCount = 1;

        //메뉴들 주문횟수
        public int Friedsum;
        public int Redsum;
        public int Soysum;
        public int Kkanpungsum;
        public int Tangsusum;
        public int Onionsum;
        public int Frenchsum;
        public int Gizzardsum;
        public int Sojusum;
        public int Beersum;
        public int SmallDrinksum;
        public int BigDrinksum;
        public int FriedSum()
        {
            Friedsum = FriedChicken * FriedCount;
            return Friedsum;
        }
        public int RedSum()
        {
            Redsum = RedChicken * RedCount;
            return Redsum;
        }
        public int SoySum()
        {
            Soysum = SoyChicken * SoyCount;
            return Soysum;
        }
        public int KkanpungSum()
        {
            Kkanpungsum = KkanpungChicken * KkanpungCount;
            return Kkanpungsum;
        }
        public int TangsuSum()
        {
            Tangsusum = TangsuChicken * TangsuCount;
            return Tangsusum;
        }
        public int OnionSum()
        {
            Onionsum = OnionChicken * OnionCount;
            return Onionsum;
        }
        public int FrenchSum()
        {
            Frenchsum = FrenchFries * FrenchCount;
            return Frenchsum;
        }
        public int GizzardSum()
        {
            Gizzardsum = GizzardFries * GizzardCount;
            return Gizzardsum;
        }
        public int SojuSum()
        {
           Sojusum = Soju * SojuCount;
            return Sojusum;
        }
        public int BeerSum()
        {
            Beersum = Beer * BeerCount;
            return Beersum; 
        }
        public int SmallDrinkSum()
        {
           SmallDrinksum = SmallDrink * SmallDrinkCount;
            return SmallDrinksum;
        }
        public int BigDrinkSum()
        {
            BigDrinksum = BigDrink * BigDrinkCount;
            return BigDrinksum;
        }



    }
}
