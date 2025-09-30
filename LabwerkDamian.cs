Console.WriteLine("WARNING, NO STOCK FOUND, PLEASE ADD STOCK OR TOTAL FAILURE");
int moneyonecount = 0; int moneytwocount = 0; int howmuchelements = 1; Item[] tracker = new Item[100];
start:
Console.WriteLine("Hello, welcome to Cat Intertainment Vending Machine");
Console.WriteLine("Please only input correct values or total failure will be imminent");
Console.WriteLine("Please, choose an option:");
Console.WriteLine("1 - See stock");
Console.WriteLine("2 - Admin mode");
int option = Convert.ToInt32(Console.ReadLine());
if (option == 2)
{
    Console.WriteLine("Enter passcode");
    string passtry = Console.ReadLine();
    if (passtry == "Vulpes Inculta")
    {
        Console.WriteLine("Access granted");
        adminmode:
        Console.WriteLine("Choose option:");
        Console.WriteLine("1 - Retrieve money");
        Console.WriteLine("2 - Change stock");
        Console.WriteLine("3 - Exit");
        int optiontwo = Convert.ToInt32(Console.ReadLine());
        if (optiontwo == 1)
        {
            Console.WriteLine("Latch to coin-bank unlocked");
            goto adminmode;
        }
        else if (optiontwo == 2)
        {
            Console.WriteLine("Add or edit stock? (1 - add, 2 - edit, 3 - exit)");
            int optionthree = Convert.ToInt32(Console.ReadLine());
            if (optionthree == 1)
            {
                Console.WriteLine("In three separate lines: Name, Price, Amount");
                string tempname = Console.ReadLine();
                int tempprice = Convert.ToInt32(Console.ReadLine());
                int tempamount = Convert.ToInt32(Console.ReadLine());
                tracker[howmuchelements] = new Item(tempname, tempprice, tempamount);
                Console.WriteLine($"ID {howmuchelements} added, add it to paper list.");
                howmuchelements++;
                goto adminmode;
            }
            else if (optionthree == 2)
            {
                Console.WriteLine("Which ID from paper list to edit?");
                int ID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("In three separate lines: Name, Price, Amount");
                string tempname = Console.ReadLine();
                int tempprice = Convert.ToInt32(Console.ReadLine());
                int tempamount = Convert.ToInt32(Console.ReadLine());
                tracker[ID].editName(tempname);
                tracker[ID].editPrice(tempprice);
                tracker[ID].editAmount(tempamount);
                goto adminmode;
            }
            else
            {
                optionthree = 0;
                goto adminmode;
            }
        }
        else
        {
            optiontwo = 0;
            goto start;
        }
    }
    else
    {
        Console.WriteLine("Wrong password");
        goto start;
    }
}
else
{
    shopcycle:
    Console.WriteLine("Here is the stock:");
    for (int i = 1; i < howmuchelements; i++)
    {
        string temp = tracker[i].printoutstats();
        Console.WriteLine(temp);
        temp = "";
    }
    Console.WriteLine("1 - Buy item, 2 - Insert Coins (We only accept one-coins and ten-coins), 3 - Return coins, 4 - Exit.");
    int optionbuy = Convert.ToInt32(Console.ReadLine());
    if (optionbuy == 1)
    {
        Console.WriteLine("Which number do you need?");
        int selected = Convert.ToInt32(Console.ReadLine());
        if ((moneyonecount + moneytwocount * 10) >= tracker[selected].returnprice() && tracker[selected].returnamount() >= 1)
        {
            tracker[selected].soldone();
            int tempprice = tracker[selected].returnprice();
            if (moneyonecount >= tempprice)
            {
                moneyonecount = moneyonecount - tempprice;
            }
            else
            {
                while (moneytwocount > 0 && tempprice >= 10)
                {
                    tempprice = tempprice - 10;
                    moneytwocount--;
                }
                while (moneyonecount > 0 && tempprice >= 1)
                {
                    tempprice--;
                    moneyonecount--;
                }
            }
            tempprice = 0;
            goto shopcycle;
        }
        else
        {
            Console.WriteLine($"Not enough money or stock. You have: {moneyonecount + moneytwocount * 10}");
            optionbuy = 0;
            goto shopcycle;
        }
    }
    if (optionbuy == 2)
    {
        Console.WriteLine("Which coins to add? 1 or 10");
        int optioncoin = Convert.ToInt32(Console.ReadLine());
        if (optioncoin == 1)
        {
            Console.WriteLine("How many?");
            int amount = Convert.ToInt32(Console.ReadLine());
            moneyonecount = moneyonecount + amount;
            goto shopcycle;
        }
        if (optioncoin == 10)
        {
            Console.WriteLine("How many?");
            int amount = Convert.ToInt32(Console.ReadLine());
            moneytwocount = moneytwocount + amount;
            goto shopcycle;
        }
        else
        {
            optionbuy = 0;
            goto shopcycle;
        }
    }
    if (optionbuy == 3)
    {
        Console.WriteLine($"Dispensed {moneyonecount} one-coins and {moneytwocount} two-coins");
        moneyonecount = 0; moneytwocount = 0;
    }
    else
    {
        goto start;
    }
}

public class Item
{
    public Item (string name, int price, int amount)
    {
        Name = name;
        Price = price;
        Amount = amount;

    }
    public void editName (string otherName)
    {
        Name = otherName;
    }
    public void editPrice(int otherPrice)
    {
        Price = otherPrice;
    }
    public void editAmount(int otherAmount)
    {
        Amount = otherAmount;
    }
    public void soldone()
    {
        Console.WriteLine($"Dispensed {Name}");
        Amount--;
    }
    public string printoutstats()
    {
        string toreturn;
        toreturn = Name + "|  Price = " + Price + "| Amount = " + Amount;
        return toreturn;
    }
    public int returnprice()
    {
        return Price;
    }
    public int returnamount()
    {
        return Amount;
    }

    public string Name { get; set; }
    public int  Price { get; set; }
    public int Amount { get; set; }
}
