using EventsTask;
using System.Text;

class Program
{
    public enum DayNames { Понеділок, Вівторок, Середа, Четвер, Пятниця, Субота, Неділя };
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        List<Flower> flowers = new List<Flower>
        {
                new DayFlower("Ромашка", 15 , ConsoleColor.White),
                new DayFlower("Мак", 10, ConsoleColor.Red),
                new DayFlower("Тюльпан", 13, ConsoleColor.DarkRed),
                new NightFlower("Енотера", 12, ConsoleColor.DarkYellow),
                new NightFlower("Матіола", 14, ConsoleColor.Gray)
        };
        Bee bee = new Bee();
        NightButterFly nightButterFly = new NightButterFly();
        Girl girl = new Girl();
        Garden garden = new Garden();
        garden.AddFlowers(flowers);
        garden.AddBee(bee);
        garden.AddNightButterFly(nightButterFly);
        garden.addGirl(girl);
        for (int i = 0; i < 16; i++)
        {
            DayNames day = (DayNames)(i % 7);
            Console.WriteLine($"\n--- День {i + 1} - {day} ---");
            garden.SimulateDay();
            Console.ReadKey();
        }
    }
}
