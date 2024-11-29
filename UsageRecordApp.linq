<Query Kind="Program" />

namespace UsageRecordApp
{
    // Клас 1: для опису запису відомості
    class UsageRecord
    {
        // Поля для початкових даних
        public string Department { get; }
        public double PlannedHours { get; }
        public double ActualHours { get; }

        // Конструктор з параметрами
        public UsageRecord(string department, double plannedHours, double actualHours)
        {
            Department = department;
            PlannedHours = plannedHours;
            ActualHours = actualHours;
        }

        // Метод для обчислення відхилення у годинах
        public double DeviationInHours() => PlannedHours - ActualHours;

        // Метод для обчислення відхилення у відсотках
        public double DeviationInPercent()
        {
            return PlannedHours != 0 ? DeviationInHours() * 100 / PlannedHours : 0;
        }
    }

    // Клас 2: містить статичні методи, включаючи main(), методи підрахунку підсумкових даних і виведення
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть кількість записів:");
            int recordCount;
            while (!int.TryParse(Console.ReadLine(), out recordCount) || recordCount <= 0)
            {
                Console.WriteLine("Будь ласка, введіть коректне число записів.");
            }

            UsageRecord[] records = new UsageRecord[recordCount];

            for (int i = 0; i < recordCount; i++)
            {
                Console.WriteLine($"\nЗапис {i + 1}");
                Console.Write("Введіть назву кафедри: ");
                string department = Console.ReadLine();

                double plannedHours = GetDoubleInput("Введіть планові години: ");
                double actualHours = GetDoubleInput("Введіть фактичні години: ");

                records[i] = new UsageRecord(department, plannedHours, actualHours);
            }

            PrintTable(records);
            PrintSummary(records);
        }

        // Метод для безпечного вводу числа з консолі
        static double GetDoubleInput(string prompt)
        {
            double value;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out value) || value < 0)
            {
                Console.WriteLine("Будь ласка, введіть коректне число.");
            }
            return value;
        }

        // Метод для виведення таблиці
        static void PrintTable(UsageRecord[] records)
        {
            Console.WriteLine("\n+----------------+----------------+----------------+----------------+----------------+");
            Console.WriteLine("|    Кафедра     | Планові години | Фактичні години | Відхилення, год | Відхилення, % |");
            Console.WriteLine("+----------------+----------------+----------------+----------------+----------------+");

            foreach (var record in records)
            {
                Console.WriteLine($"| {record.Department,-14} | {record.PlannedHours,14:F2} | {record.ActualHours,14:F2} | {record.DeviationInHours(),14:F2} | {record.DeviationInPercent(),14:F2} |");
            }

            Console.WriteLine("+----------------+----------------+----------------+----------------+----------------+");
        }

        // Метод для підрахунку
        static void PrintSummary(UsageRecord[] records)
        {
            double totalPlannedHours = 0;
            double totalActualHours = 0;
            double totalDeviationInHours = 0;
            double totalDeviationInPercent = 0;

            foreach (var record in records)
            {
                totalPlannedHours += record.PlannedHours;
                totalActualHours += record.ActualHours;
                totalDeviationInHours += record.DeviationInHours();
                totalDeviationInPercent += record.DeviationInPercent();
            }

            Console.WriteLine($"| {"Підсумок",14} | {totalPlannedHours,14:F2} | {totalActualHours,14:F2} | {totalDeviationInHours,14:F2} | {totalDeviationInPercent / records.Length,14:F2} |");
            Console.WriteLine("+----------------+----------------+----------------+----------------+----------------+");
			Console.WriteLine("Good day, isn't today?Yes of course");
        }
    }
}
