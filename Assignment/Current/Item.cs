using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB201
{
    public abstract class Item
    {
        // Field & Properties
        protected Customer ItemOwner;
        protected string ItemType;
        protected string ItemDes;
        protected List<Bid> Bids { get; set; }

        public Item(Customer itemOwner, string itemType, string itemDes)
        {
            this.ItemOwner = itemOwner;
            this.ItemType = itemType;
            this.ItemDes = itemDes;
            Bids = new List<Bid>();
        }

        public static List<Item> sortItems(Customer ItemOwner, List<Item> items)     // overloaded method with parameter customer
        {
            List<Item> moreItem = new List<Item>();

            foreach (Item subItem in items)
            {
                if (subItem.ItemOwner == ItemOwner) moreItem.Add(subItem);
            }
            return moreItem;
        }

        public static List<Item> sortItems(string ItemType, List<Item> items)    // overloaded method with parameter postcode string
        {
            List<Item> moreItem = new List<Item>();

            foreach (Item subItem in items)
            {
                if (subItem.ItemType == ItemType)
                {
                    moreItem.Add(subItem);
                }
            }
            return moreItem;
        }

        public abstract void listBids();

        public abstract int salesTax(int price);

        public int getHighestBid()
        {
            // Check if item has bids 
            if (Bids.Count > 0)
            {
                return Bids.Max(r => r.ItemBid);
            }
            else
            {
                return 0;
            }
        }

        public void addBid(Customer customer, int itemBid)
        {
            Bids.Add(new Bid(customer, itemBid));
        }

        public void transferItem()
        {
            int price = getHighestBid();
            if (price == 0)
            {
                UserInterface.Error("There are no bids for this item");
                return;
            }

            Bid highestBid = Bids.Find(x => x.ItemBid == price);
            Customer newOwner = highestBid.Bidder;
            ItemOwner = newOwner;

            Bids = new List<Bid>();
            UserInterface.Message($"{this} successfully sold to {this.ItemOwner}");
            UserInterface.Message($"Tax payable ${salesTax(price)}");
        }
    }

    class HomeDeliveredItem : Item
    {
        private int ItemCost;

        public HomeDeliveredItem(Customer itemOwner, string itemType, string itemDes, int itemCost) : base(itemOwner, itemType, itemDes)
        {
            this.ItemCost = itemCost;
        }

        public override void listBids()
        {
            UserInterface.DisplayOption($"All bids for {this.ItemType}", Bids, "No bids for this item have been received");
        }

        public override string ToString()
        {
            return $"{ItemType}, {ItemDes}, price: ${ItemCost}";
        }

        public override int salesTax(int price)
        {
            Console.WriteLine("Auction house receives $20");
            // 15% plus additional 5$
            return (int)Math.Round(price * 0.15 + 5);

        }
    }


    class CollectItem : Item
    {
        private int AuctionCharge;

        public CollectItem(Customer itemOwner, string itemType, string itemDes, int itemCost, int auctionCharge) : base(itemOwner, itemType, itemDes)
        {
            this.AuctionCharge = auctionCharge;
        }

        public override void listBids()
        {
            UserInterface.DisplayOption($"All bids for {this.ItemType} (Home delivered only)", Bids, "No bids for this item have been received");
        }

        public override string ToString()
        {
            return $"{ItemType}, {ItemDes}, price: ${AuctionCharge}";
        }

        public override int salesTax(int price)
        {
            Console.WriteLine("Auction house receives $10");
            // 15% 
            return (int)Math.Round(price * 0.15);

        }
    }
}