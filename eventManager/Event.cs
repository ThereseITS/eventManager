using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventManager
{

    internal class Event
    {
        static int eventIdCounter = 1; // Note that a static variable is shared between all class members.

// private class members

        int _eventNumber;
        string _name;
        string _eventType;
        decimal _price;
        int _capacity;
        int _seatsSold;

// Properties
        public string Name { get { return _name; } }
        public string EventType
        { 
            
            get { return _eventType; }

            set
            {
                if ((value.ToLower() != "comedy")&&(value.ToLower()!="drama")&& (value.ToLower() != "concert") && (value.ToLower() != "sporting") )
                {
                    throw new ArgumentException("Invalid event type");
                }
                else
                {
                    _eventType = value;
                }
            }
        }
    
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid Price");
                }
                else
                {
                    _price = value;
                }
            }
        }
        public int Capacity{  
            
            get { return _capacity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Capacity should be greater than or equal to 0");
                }
                else
                {
                    _capacity = value;
                }
            }
        }
        public int SeatsSold {  get { return _seatsSold; } } // this value can only be changed by the BuySeats method below.

        // Constructors
        public Event(string name, string eventType, decimal price, int capacity)
        {
            _name = name;
            EventType = eventType;// use property setters to check input
            Price = price;
            Capacity = capacity;
            _seatsSold = 0;
            _eventNumber = eventIdCounter++;
        }
        
        public Event(string name, string eventType, decimal price, int capacity, int seatsSold):this(name, eventType,price,capacity) // calls earlier constructor.
        {
            if (_seatsSold >= 0)
            {
                _seatsSold = seatsSold;
            }
            else
            {
                throw new ArgumentException("Seats sold should not be less than 0");
            }
     
        }

        //Methods

        /// <summary>
        /// Privides a classification for the price.
        /// </summary>
        /// <returns>This method returns a string  classification for the price - high, low or medium</returns>
        public string GetPriceClassification()
        {
            string classification = "";

            switch(_price)
            {
                case decimal p when ((p>=0m) && (p<50m)): classification = "Low";break;
                case decimal p when ((p >= 50m) && (p < 100m)): classification = "Medium"; break;
                case decimal p when (p >= 100m): classification = "High"; break;
                default: classification = "UNKNOWN"; break;
            }

            return classification;
        }

        /// <summary>
        /// Caculates the percentage seats sold.
        /// </summary>
        /// <returns>This method returns a double -percentage seats sold - as a value between 0 and 1.</returns>
        /// 
        public double CalculatePercentageSeatsSold()
        {
            if (_capacity > 0)
            {
                return (double)_seatsSold / (double)_capacity;
            }
            return 0;
        }
/// <summary>
/// This method returns a formatted string.
/// </summary>
/// <returns></returns>
        public override string ToString()
        {
            return $" {_eventNumber,-10} {_name, -15} {_eventType,-10} {_price,-10:C2} {GetPriceClassification(),-10} {_capacity,-10} {_seatsSold,-10} {CalculatePercentageSeatsSold(),-10:P0} sold";

        }
    }
}
