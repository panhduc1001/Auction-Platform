using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB201
{
    public class AuctionHouse
    {
        private List<Customer> customers;
        private List<Item> items;
        private Customer loggedInUser;

        // Check whether the user is logged in or not
        private bool LoggedIn
        {
            get
            {
                return (loggedInUser != null);
            }
        }

        public AuctionHouse()
        {
            customers = new List<Customer>();
            items = new List<Item>();
        }

        // Main part of the program 
        static void Main(string[] args)
        {
            AuctionHouse auctionSystem = new AuctionHouse();
            auctionSystem.Process();
        }

        private void Process()
        {
            MainMenu();
            MainContent();
        }

        // Display the main menu 
        private void MainMenu()
        {
            const int REGISTER = 0, SIGN_UP = 1, EXIT = 2;

            bool running = true;

            while (running)
            {
                int option = UserInterface.GetOption("Please select one of the following",
                    "Register as a new Client", "Login as existing Client", "Exit");

                switch (option)
                {
                    case REGISTER:
                        userRegister();
                        break;
                    case SIGN_UP:
                        userLogIn();
                        break;
                    case EXIT:
                        exitMenu();
                        running = false;
                        break;
                    default: break;
                }
            }
        }

        // Display the main content 
        public void MainContent()
        {
            Console.WriteLine();

            const int REGISTER_ITEM = 0, LIST_ITEM = 1, SEARCH = 2, BID = 3, LIST_BID = 4, SELL = 5, LOG_OUT = 6;

            bool running = true;

            while (running)
            {
                if (LoggedIn)
                {
                    int option = UserInterface.GetOption("Please select one of the following",
                    "Register item for sale", "List my items", "Search for items", "Place a bid on an item", "List bids received for my items", "Sell one of my item to highest bidder", "Logout");

                    switch (option)
                    {
                        case REGISTER_ITEM:
                            itemRegister();
                            break;
                        case LIST_ITEM:
                            itemList();
                            break;
                        case SEARCH:
                            itemSearch();
                            break;
                        case BID:
                            placeItemBid();
                            break;
                        case LIST_BID:
                            listBids();
                            break;
                        case SELL:
                            sellItem();
                            break;
                        case LOG_OUT:
                            exitContent();
                            break;
                        default: break;
                    }
                }

                else
                {
                    MainMenu();
                }
            }
        }

        // Actions in the menu 

        // Registration
        private void userRegister()
        {
            string userName = UserInterface.GetInput("Full name");
            string userAddr = UserInterface.GetInput("Address");
            string userMail = UserInterface.GetInput("Email");
            string userPwd = "";

            // Ensure user's email is unique 
            if (customers.Find(m1 => m1.UserMail == userMail) != null)
            {
                UserInterface.Error("This email is already taken");
                return;
            }

            bool pwd_check = false;

            // Confirm password
            while (!pwd_check)
            {
                userPwd = UserInterface.GetPassword("Password");
                string pwd_reconfirm = UserInterface.GetPassword("Confirm Password");
                if (userPwd == pwd_reconfirm)
                {
                    pwd_check = true;
                }
                else
                {
                    UserInterface.Error("Sorry, your passwords don't match");
                }
            }

            customers.Add(new Customer(userName, userMail, userPwd));

            Console.WriteLine($"{userName} registered successfully");
            UserInterface.Message("--------------------");
        }

        // Log-in session 
        private void userLogIn()
        {
            string userMailInput = UserInterface.GetInput("Email");
            string userPwdInput = UserInterface.GetPassword("Password");
            Customer customer = customers.Find(m2 => m2.UserMail == userMailInput);

            if (customer == null)
            {
                UserInterface.Error("Email not found");
            }

            else
            {
                loggedInUser = customer.Verify(userMailInput, userPwdInput);
                MainContent();
            }
        }

        private void exitMenu()
        {
            Console.WriteLine("Goodbye!");
            UserInterface.Message("--------------------");
            System.Environment.Exit(0);
        }

        // Regiter an item 
        private void itemRegister()
        {
            Item newItem = loggedInUser.regNewItem(1);
            items.Add(newItem);

            Console.WriteLine($"{newItem} registered successfully");
            UserInterface.Message("--------------------");
        }

        // List items 
        private void itemList()
        {
            List<Item> listOfItems = Item.sortItems(loggedInUser, items);
            Console.WriteLine();
            UserInterface.DisplayOption($"All items owned by {loggedInUser}", listOfItems, "You do not currently have any properties listed");
        }

        // Search for items 
        private void itemSearch()
        {
            Console.WriteLine();
            string ItemType = UserInterface.GetInput("Item type");
            Console.WriteLine();
            UserInterface.DisplayOption("Item found", Item.sortItems(ItemType, items));
        }

        // Place a bid
        private void placeItemBid()
        {
            List<Item> sortedItems = items.Except(Item.sortItems(loggedInUser, items)).ToList();

            if (sortedItems.Count == 0)
            {
                UserInterface.Error("No items available");
                return;
            }

            Item itemChose = UserInterface.ChooseFromList(sortedItems);
            UserInterface.Message(loggedInUser.placeItemBid(itemChose));
        }

        // List bids for my items 
        private void listBids()
        {
            Item itemChose = UserInterface.ChooseFromList(Item.sortItems(loggedInUser, items));
            itemChose.listBids();
        }

        // Sell my item to highest bidder 
        private void sellItem()
        {
            Console.WriteLine(); 
            UserInterface.Message("Which item would you want to sell?");
            Item itemChose = UserInterface.ChooseFromList(Item.sortItems(loggedInUser, items));

            if (itemChose != null)
            {
                itemChose.transferItem();
            }
        }

        // Logout 
        private void exitContent()
        {
            UserInterface.Message($"{loggedInUser} Logged out successfully!");
            Console.WriteLine("--------------------");
            Console.WriteLine();

            loggedInUser = null;
        }
    }
}
