public class Program
{
    public class House
    {
        public double price;
        public string name;
        public House() { }
        public House(string name, double price)
        {
            this.price = price;
            this.name = name;

        }
        public virtual void Display()
        {
            Console.WriteLine("Price of {0} house is {1}", name, price);
        }
    }
    public class SmartHouse : House
    {
        public int star;
        public SmartHouse(string name, double price, int star)
        {
            this.name = name;
            this.price = price;
            this.star = star;
        }
        public override void Display()
        {
            base.Display();
            Console.WriteLine("Star Of {0} is : {1}", name, star);
        }
    }

    public class Apartment : House
    {
        public string nameOfArea;
        public Apartment(string name, double price, string nameOfArea)
        {
            this.name = name;
            this.nameOfArea = nameOfArea;
            this.price = price;
        }
        static public void GetNameOfArea()
        {
            Console.WriteLine("This is Apartment");
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("This {0} located in {1} Area", name, nameOfArea);
        }

    }
    static void Main()
    {
        House house = new House("NormalHouse", 300.11);
        house.Display();
        SmartHouse smartHouse = new SmartHouse("SmartHouse", 500.5, 5);
        smartHouse.Display();
        Apartment apartment = new Apartment("Apartment", 150.5, "KhuA-HaDong");
        apartment.Display();
    }
}