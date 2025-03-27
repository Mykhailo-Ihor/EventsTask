using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsTask
{
    //Клас сонця, основний генератор подій
    public class Sun 
    {
        //Події сонця використовують FlowerEventArgs, щоб передати список квітів
        public event EventHandler<FlowerEventArgs> SunRise;
        public event EventHandler<FlowerEventArgs> Morning;
        public event EventHandler<FlowerEventArgs> MidDay;
        public event EventHandler<FlowerEventArgs> Evening;
        public event EventHandler<FlowerEventArgs> SunSet;

        public void OnSunRise(List<Flower> flowers)
        {
            if (SunRise != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----------Схід сонця----------");
                Console.ResetColor();
                SunRise(this, new FlowerEventArgs(flowers));
            }
        }

        public void OnMorning(List<Flower> flowers)
        {
            if (Morning != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----------Ранок----------");
                Console.ResetColor();
                Morning(this, new FlowerEventArgs(flowers));
            }
        }

        public void OnMidDay(List<Flower> flowers)
        {
            if (MidDay != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----------Полудень----------");
                Console.ResetColor();
                MidDay(this, new FlowerEventArgs(flowers));
            }
        }

        public void OnEvening(List<Flower> flowers)
        {
            if (Evening != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("----------Вечір----------");
                Console.ResetColor();
                Evening(this, new FlowerEventArgs(flowers));
            }
        }

        public void OnSunSet(List<Flower> flowers)
        {
            if (SunSet != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("----------Захід сонця----------");
                Console.ResetColor();
                SunSet(this, new FlowerEventArgs(flowers));
            }
        }
    }
    
    // Тип аргумента події для передачі списку квітів
    public class FlowerEventArgs : EventArgs
    {
        public List<Flower> Flowers { get; }
        public FlowerEventArgs(List<Flower> flowers)
        {
            Flowers = flowers;
        }
    }

    // Абстрактний клас квітки, містить всі необхідні поля методи для опрацювання подій
    // Також має власну подію Withered, яка повідомляє що квітка зів'яла
    public abstract class Flower
    {
        public string Name { get; set; }
        protected int Flowering_period { get; init; } // Поле для кількості днів цвітіння
        protected int DaysAlive = 0; // Лічильник днів цвітіння квітки
        protected readonly ConsoleColor color;
        protected bool isOpen = false;
        public event EventHandler Withered;

        public Flower(string name, int flowering_period, ConsoleColor color)
        {
            Name = name;
            Flowering_period = flowering_period;
            this.color = color;
        }
        public abstract void OnSunRise(object sender, FlowerEventArgs e);
        public abstract void OnMorning(object sender, FlowerEventArgs e);
        public abstract void OnMidDay(object sender, FlowerEventArgs e);
        public abstract void OnEvening(object sender, FlowerEventArgs e);
        public abstract void OnSunSet(object sender, FlowerEventArgs e);
        protected void OnWithered()
        {
            Withered(this, EventArgs.Empty);
        }
        public bool IsOpen() => isOpen;


    }
    //Клас денної квітки, перевизначає всі методи для опрацювання подій 
    public class DayFlower : Flower
    {
        public DayFlower(string name, int flowering_period, ConsoleColor color)
            : base(name, flowering_period, color)
        {
        }

        public override void OnSunRise(object sender, FlowerEventArgs e)
        { 
            // Перевірка чи квітка ще не зів'яла
            if (DaysAlive < Flowering_period) 
            {
                ++DaysAlive;
                Console.ForegroundColor = color;
                Console.WriteLine("Денна квітка {0} починає цвісти", Name);
                Console.ResetColor();
                isOpen = true; // Денна квітка відкривається зранку
            }
            else
            {
                OnWithered();
            }

        }

        public override void OnMorning(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Денна квітка {0} цвіте", Name);
            Console.ResetColor();
        }

        public override void OnMidDay(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Денна квітка {0} цвіте", Name);
            Console.ResetColor();
        }

        public override void OnEvening(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Денна квітка {0} закривається", Name);
            Console.ResetColor();
            isOpen = false; // І закривається ввечері
        }

        public override void OnSunSet(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Денна квітка {0} закрита", Name);
            Console.ResetColor();
        }

    }

    //Клас нічної квітки, перевизначає всі методи для опрацювання подій
    public class NightFlower : Flower
    {
        public NightFlower(string name, int flowering_period, ConsoleColor color)
            : base(name, flowering_period, color)
        {
        }

        public override void OnSunRise(object sender, FlowerEventArgs e)
        {
            // Перевірка чи квітка ще не зів'яла
            if (DaysAlive < Flowering_period)
            {
                ++DaysAlive;
                Console.ForegroundColor = color;
                Console.WriteLine("Нічна квітка {0} закривається", Name);
                Console.ResetColor();
                isOpen = false;// Нічна квітка закривається зранку
            }
            else
            {
                OnWithered();
            }
        }

        public override void OnMorning(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Нічна квітка {0} закрита", Name);
            Console.ResetColor();
        }

        public override void OnMidDay(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Нічна квітка {0} закрита", Name);
            Console.ResetColor();
        }

        public override void OnEvening(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Нічна квітка {0} починає цвісти", Name);
            Console.ResetColor();
            isOpen = true; // І відкривається ввечері
        }

        public override void OnSunSet(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Нічна квітка {0} цвіте", Name);
            Console.ResetColor();
        }
    }
    // Абстракний клас комахи, містить всі необхідні поля методи для опрацювання подій
    public abstract class Insect
    {
        protected ConsoleColor color;
        protected List<Flower> visitedFlowers = new List<Flower>(); // Список відвіданих квітів
        protected const int FlowersToVisitPerTime = 1; // Кількість квітів, які можна відвідати за один раз
        protected bool hasFlowersToVisit = true; // Поле прапорець для перевірки наявності квітів для відвідування

        public abstract void OnSunRise(object sender, FlowerEventArgs e);
        public abstract void OnMorning(object sender, FlowerEventArgs e);
        public abstract void OnMidDay(object sender, FlowerEventArgs e);
        public abstract void OnEvening(object sender, FlowerEventArgs e);
        public abstract void OnSunSet(object sender, FlowerEventArgs e);
        protected abstract void VisitFlowers(List<Flower> flowers, bool visitAllRemaining = false);
       
    }
    //Клас бджоли наслідує клас комахи, перевизначає всі методи для опрацювання подій
    public class Bee : Insect
    {
        public Bee()
        {
            color = ConsoleColor.Yellow;
        }

        public override void OnSunRise(object sender, FlowerEventArgs e)
        {
            hasFlowersToVisit = e.Flowers.Any(f => f is DayFlower); // Перевірка наявності денних квітів

            visitedFlowers.Clear(); // Очищення списку відвіданих квітів після минулого дня

            Console.ForegroundColor = color;
            // Якщо немає денних квітів то вони всі зів'яли
            if (!hasFlowersToVisit)
            {
                Console.WriteLine("Бджола прокинулась, але всі денні квіти зів'яли - немає з чого збирати нектар");
            }
            // В іншому випадку можна починатм збирати нектар
            else
            {
                Console.WriteLine("Бджола прокинулась і починає збирати нектар");
                VisitFlowers(e.Flowers);
            }
            Console.ResetColor();
        }

        public override void OnMorning(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            if (hasFlowersToVisit)
            {
                Console.WriteLine("Бджола продовжує збирати нектар");
                VisitFlowers(e.Flowers);
            }
            else
            {
                Console.WriteLine("Бджола просто літає навколо");
            }
            Console.ResetColor();

        }

        public override void OnMidDay(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            if (hasFlowersToVisit)
            {
                Console.WriteLine("Бджола завершує збір нектару");
                VisitFlowers(e.Flowers, true);// Якщо є ще квіти, то відвідує всі що залишились  
            }
            else
            {
                Console.WriteLine("Бджола просто літає навколо");
            }
            Console.ResetColor();

        }

        public override void OnEvening(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Бджола повертається до вулика");
            Console.ResetColor();
        }

        public override void OnSunSet(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Бджола спить");
            Console.ResetColor();
        }

        protected override void VisitFlowers(List<Flower> flowers, bool visitAllRemaining = false)
        {
            var dayFlowers = flowers.OfType<DayFlower>()
                .Where(f => f.IsOpen() && !visitedFlowers.Contains(f))
                .ToList(); // Отримання списку відкритих денних квітів, які ще не відвідувались

            // Визначаємо кількість квітів, які потрібно відвідати залежно від умови visitAllRemaining
            int flowersToVisit = visitAllRemaining ? dayFlowers.Count : Math.Min(FlowersToVisitPerTime, dayFlowers.Count); 

            for (int i = 0; i < flowersToVisit; i++)
            {
                var flower = dayFlowers[i];
                Console.ForegroundColor = color;
                Console.WriteLine($"Бджола відвідує {flower.Name} і збирає нектар");
                Console.ResetColor();
                visitedFlowers.Add(flower);
            }
            dayFlowers = flowers.OfType<DayFlower>()
                .Where(f => f.IsOpen() && !visitedFlowers.Contains(f))
                .ToList(); // Знову перевіряємо чи залишились ще квіти для відвідування

            // Якщо квітів не залишилось, то бджола відвідала всі квіти
            if (!dayFlowers.Any())
            {
                Console.ForegroundColor = color;
                Console.WriteLine("Бджола відвіла всі денні квітки сьогодні!");
                Console.ResetColor();
                hasFlowersToVisit = false;
            }
        }
    }

    // Клас нічного метелика, логіка аналогічна класу бджоли тільки період активності відбувається ввечері
    public class NightButterFly: Insect
    {
        public NightButterFly()
        {
            color = ConsoleColor.DarkBlue;
        }

        public override void OnSunRise(object sender, FlowerEventArgs e)
        {
            hasFlowersToVisit = e.Flowers.Any(f => f is NightFlower);
            visitedFlowers.Clear();

            Console.ForegroundColor = color;
            Console.WriteLine("Нічний метелик спить");
            Console.ResetColor();
        }

        public override void OnMorning(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Нічний метелик спить");
            Console.ResetColor();
        }

        public override void OnMidDay(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Нічний метелик спить");
            Console.ResetColor();
        }

        public override void OnEvening(object sender, FlowerEventArgs e)
        {
            hasFlowersToVisit = e.Flowers.Any(f => f is NightFlower);

            Console.ForegroundColor = color;
            if (!hasFlowersToVisit)
            {
                Console.WriteLine("Нічний метелик прокинувся, але всі нічні квіти зів'яли");
            }
            else
            {
                Console.WriteLine("Нічний метелик прокинувся і починає збирати нектар");
                VisitFlowers(e.Flowers);
            }
            Console.ResetColor();
        }

        public override void OnSunSet(object sender, FlowerEventArgs e)
        {
            if (!hasFlowersToVisit)
            {
                Console.ForegroundColor = color;
                Console.WriteLine("Нічний метелик просто літає навколо");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = color;
            Console.WriteLine("Нічний метелик завершує збір нектару");
            VisitFlowers(e.Flowers, true);
            Console.ResetColor();
        }

        protected override void VisitFlowers(List<Flower> flowers, bool visitAllRemaining = false)
        {
            var nightFlowers = flowers.OfType<NightFlower>()
                .Where(f => f.IsOpen() && !visitedFlowers.Contains(f))
                .ToList();

            int flowersToVisit = visitAllRemaining ? nightFlowers.Count : Math.Min(FlowersToVisitPerTime, nightFlowers.Count);

            for (int i = 0; i < flowersToVisit; i++)
            {
                var flower = nightFlowers[i];
                Console.ForegroundColor = color;
                Console.WriteLine($"Нічний метелик відвідує {flower.Name} і збирає нектар");
                Console.ResetColor();
                visitedFlowers.Add(flower);
            }

            nightFlowers = flowers.OfType<NightFlower>()
               .Where(f => f.IsOpen() && !visitedFlowers.Contains(f))
               .ToList();

            if (!nightFlowers.Any())
            {
                Console.ForegroundColor = color;
                Console.WriteLine("Нічний метелик відвідав всі нічні квітки сьогодні!");
                Console.ResetColor();
                hasFlowersToVisit = false;  
            }

        }
    }
    // Клас дівчинки
    public class Girl 
    {
        private static readonly Random random = new Random(); // поле для випадкового вибору дій з квітами
        private ConsoleColor color = ConsoleColor.Magenta;
        private int dayCounter = 0; // лічильник днів для визначення вихідних
        private Dictionary<int, string> actions = new Dictionary<int, string> // словник дій дівчинки з квітами
        {
            { 0, "оглядає квітки" },
            { 1, "робить селфі з квітами" },
            { 2, "вдихає пахощі квітів" }
        };

        public void OnSunRise(object sender, FlowerEventArgs e)
        {
            ++dayCounter;
            Console.ForegroundColor = color;
            Console.WriteLine("Дівчинка прокидається і готується до нового дня");
            Console.ResetColor();
        }

        public void OnMorning(object sender, FlowerEventArgs e)
        {
            bool isWeekend = dayCounter % 7 == 6 || dayCounter % 7 == 0;
            if (isWeekend)
            {
                InteractWithFlowers(e.Flowers, isWeekend);
            }
            else
            {
                Console.ForegroundColor = color;
                Console.WriteLine("Дівчинка йде до школи");
                Console.ResetColor();
            }
        }

        public void OnMidDay(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Дівчинка обідає і відпочиває");
            Console.ResetColor();
        }

        public void OnEvening(object sender, FlowerEventArgs e)
        {
            bool isWeekend = dayCounter % 7 == 6 || dayCounter % 7 == 0;
            if (!isWeekend)
            {
                InteractWithFlowers(e.Flowers, isWeekend);
            }
            else
            {
                Console.ForegroundColor = color;
                Console.WriteLine("Дівчинка гуляє з друзями");
                Console.ResetColor();
            }
        }

        public void OnSunSet(object sender, FlowerEventArgs e)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Дівчинка готується до сну");
            Console.ResetColor();
        }

        private void InteractWithFlowers(List<Flower> flowers, bool isWeekend)
        {
            // Випадково вибираємо дію
            int actionIndex = random.Next(actions.Count); 
            string action = actions[actionIndex];

            // Вибираємо квіти відповідно до дня тижня
            var targetFlowers = flowers.Where(f => (isWeekend && f is DayFlower) || (!isWeekend && f is NightFlower)).ToList();

            // Якщо квіти є, то дівчинка взаємодіє з ними
            if (targetFlowers.Any())
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"Дівчинка {action}: {string.Join(", ", targetFlowers.Select(f => f.Name))}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = color;
                Console.WriteLine("Дівчинка шукає квіти, але їх немає");
                Console.ResetColor();
            }
        }
    }
    // Клас сад, відповідає за підписку на події,видалення зів'ялих квітів та симуляцію дня
    public class Garden
    {
        private List<Flower> _flowers = new List<Flower>();
        private Sun _sun = new Sun();

        public void AddFlower(Flower flower)
        {
            _flowers.Add(flower);
            _sun.SunRise += flower.OnSunRise;
            _sun.Morning += flower.OnMorning;
            _sun.MidDay += flower.OnMidDay;
            _sun.Evening += flower.OnEvening;
            _sun.SunSet += flower.OnSunSet;
            flower.Withered += OnWithered;
        }

        public void AddFlowers(List<Flower> flowers)
        {
            foreach (var flower in flowers)
            {
                AddFlower(flower);
            }
        }

        public void AddBee(Bee bee)
        {
            _sun.SunRise += bee.OnSunRise;
            _sun.Morning += bee.OnMorning;
            _sun.MidDay += bee.OnMidDay;
            _sun.Evening += bee.OnEvening;
            _sun.SunSet += bee.OnSunSet;
        }

        public void AddNightButterFly(NightButterFly nightButterFly)
        {
            _sun.SunRise += nightButterFly.OnSunRise;
            _sun.Morning += nightButterFly.OnMorning;
            _sun.MidDay += nightButterFly.OnMidDay;
            _sun.Evening += nightButterFly.OnEvening;
            _sun.SunSet += nightButterFly.OnSunSet;
        }

        public void addGirl(Girl girl)
        {
            _sun.SunRise += girl.OnSunRise;
            _sun.Morning += girl.OnMorning;
            _sun.MidDay += girl.OnMidDay;
            _sun.Evening += girl.OnEvening;
            _sun.SunSet += girl.OnSunSet;
        }
        private void OnWithered(object sender, EventArgs e)
        {
            if (sender is Flower flower)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Квітка {flower.Name} зів'яла :(");
                Console.ResetColor();

                _flowers.Remove(flower);

                _sun.SunRise -= flower.OnSunRise;
                _sun.Morning -= flower.OnMorning;
                _sun.MidDay -= flower.OnMidDay;
                _sun.Evening -= flower.OnEvening;
                _sun.SunSet -= flower.OnSunSet;
            }
        }

        public void SimulateDay()
        {
            _sun.OnSunRise(_flowers);
            _sun.OnMorning(_flowers);
            _sun.OnMidDay(_flowers);
            _sun.OnEvening(_flowers);
            _sun.OnSunSet(_flowers);
        }
    }
}