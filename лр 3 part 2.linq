<Query Kind="Program" />

namespace UsageRecordApp
{
    // Клас частина: описує окремий запис відомості
    class Record
    {
        // Поля для початкових даних
        public string Department { get; }
        public double PlannedHours { get; }
        public double ActualHours { get; }

        // Конструктор з параметрами
        public Record(string department, double plannedHours, double actualHours)
        {
            Department = department;
            PlannedHours = plannedHours;
            ActualHours = actualHours;
        }

        // Метод для обчислення відхилення у годинах
        public double DeviationInHours() => PlannedHours - ActualHours;

        // Метод для обчислення відхилення у відсотках
        public double DeviationInPercent() => PlannedHours != 0 ? DeviationInHours() * 100 / PlannedHours : 0;
    }

    // Клас ціле: описує відомість із безліччю записів
    class Report
    {
        // Масив об'єктів Record, що представляють окремі записи
        private Record[] records;

        // Конструктор, який приймає кількість записів і створює відповідний масив
        public Report(int recordCount)
        {
            records = new Record[recordCount];
        }

        // Метод для додавання запису у відомість
        public void AddRecord(int index, string department, double plannedHours, double actualHours)
        {
            if (index >= 0 && index < records.Length)
            {
                records[index] = new Record(department, plannedHours, actualHours);
            }
        }

        // Метод для виведення даних у вигляді таблиці
        public void PrintTable()
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

        // Метод для підрахунку і виведення підсумкових даних
        public void PrintSummary()
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
        }
    }

    // Клас програми з методом Main
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

            // Створення об'єкта Report (відомість)
            Report report = new Report(recordCount);

            // Введення даних для кожного запису і додавання їх у відомість
            for (int i = 0; i < recordCount; i++)
            {
                Console.WriteLine($"\nЗапис {i + 1}");
                Console.Write("Введіть назву кафедри: ");
                string department = Console.ReadLine();

                double plannedHours = GetDoubleInput("Введіть планові години: ");
                double actualHours = GetDoubleInput("Введіть фактичні години: ");

                report.AddRecord(i, department, plannedHours, actualHours);
            }

            // Виведення таблиці і підсумкових даних
            report.PrintTable();
            report.PrintSummary();
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
    }
}
