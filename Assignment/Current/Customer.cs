using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB201
{
    public class Customer
    {
        private string userMail;
        public string UserMail { get { return userMail; } }
        private string UserName;
        private Password UserPwd;

        public Customer(string userName, string userMail, string userPwd)
        {
            this.UserName = userName;
            this.userMail = userMail;
            this.UserPwd = new Password(userPwd);
        }

        public Customer Verify(string userMailInput, string userPwdInput)
        {
            bool truePwd = UserPwd.pwdCheck(userPwdInput);
            bool trueMail = (userMailInput == userMail);

            if (trueMail && truePwd)
            {
                Console.WriteLine($"Welcome {UserName} ({UserMail})");
                UserInterface.Message("--------------------");
                return this;
            }

            else
            {
                UserInterface.Error("Your email/password is not correct.");
                return null;
            }
        }

        public Item regNewItem(int type)
        {
            string itemType = UserInterface.GetInput("Type");
            string itemDes = UserInterface.GetInput("Description");

            switch (type)
            {
                case 1:
                    int itemCost = UserInterface.GetInt("Initial Bid ($)");
                    return new HomeDeliveredItem(this, itemType, itemDes, itemCost);

                default:
                    return null;
            }
        }

        public string placeItemBid(Item item)
        {
            int minBid = item.getHighestBid();
            int itemBid = UserInterface.GetInt("Enter bid ($)");
            string itemDeliType = UserInterface.GetInput("Home Delivery");

            if (itemBid < minBid)
            {
                return "Bid cannot be placed as it is less than the minimum amount";
            }

            //if (itemDeliType == "true")
            //{

            //}

            //else
            //{

            //}

            item.addBid(this, itemBid);
            return $"{UserName} ({UserMail}) successfully bid {itemBid} on {item}";
        }

        public override string ToString()
        {
            return $"{UserName} ({UserMail})";
        }
    }
}