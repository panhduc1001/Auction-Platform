using System;
using System.Collections.Generic;
using System.Text;

namespace CAB201
{
    public class Bid
    {
        public Customer Bidder {get; set;}
        public int ItemBid {get; set;}

        public Bid(Customer bidder, int itemBid)
        {
            this.Bidder = bidder; 
            this.ItemBid = itemBid; 
        }

        public override string ToString()
        {
            return $"{Bidder}: ${ItemBid}"; 
        }
    }
}