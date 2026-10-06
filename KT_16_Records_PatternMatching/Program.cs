using System;
using System.Globalization;

namespace KT_16_Records_PatternMatching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("  КТ №16: Записи и паттерн-матчинг (Вариант 1)   ");
            Console.WriteLine("==================================================\n");

            // 1. Автоматическая проверка по контрольным ключам преподавателя
            Console.WriteLine("=== 1. Проверка проверочных ключей ===");
            RunTeacherTests();

            // 2. Интерактивный ручной ввод с обработкой ошибок
            Console.WriteLine("\n=== 2. Ручной ввод фигуры с клавиатуры ===");
            while (true)
            {
                object userShape = ReadShapeFromConsole();
                string result = Classify(userShape);
                Console.WriteLine($"\n[Результат классификации]: \"{result}\"\n");

                Console.Write("Хотите ввести ещё одну фигуру? (y/n): ");
                string choice = Console.ReadLine()?.Trim().ToLower();
                if (choice != "y" && choice != "yes" && choice != "да")
                {
                    break;
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nПрограмма завершена.");
        }

        /// <summary>
        /// Классификатор фигур через switch-выражение.
        /// Порядок веток строго от специфичных к общим.
        /// </summary>
        public static string Classify(object shape) => shape switch
        {
            // 1. Вложенный свойственный паттерн (окружность в центре (0,0))
            Circle { Center: { X: 0, Y: 0 } } => "окружность в начале координат",

            // 2. Свойственный паттерн (радиус равен 0)
            Circle { Radius: 0 } => "вырожденная окружность (точка)",

            // 3. Общий паттерн для окружности
            Circle c => $"строка с радиусом {c.Radius}",

            // 4. Тип-паттерн + when (верхняя левая точка совпадает с нижней правой)
            Rectangle r when r.TopLeft == r.BottomRight => "вырожденный прямоугольник (точка)",

            // 5. Общий паттерн для прямоугольника
            Rectangle r => $"строка с размерами {Math.Abs(r.BottomRight.X - r.TopLeft.X)}x{Math.Abs(r.BottomRight.Y - r.TopLeft.Y)}",

            // 6. Заменяющий паттерн (fallback)
            _ => "неизвестная фигура"
        };

        private static void RunTeacherTests()
        {
            object[] testCases = new object[]
            {
                new Circle(new Point(0, 0), 5),
                new Circle(new Point(3, 4), 0),
                new Circle(new Point(3, 4), 5),
                new Rectangle(new Point(1, 1), new Point(1, 1)),
                new Rectangle(new Point(0, 0), new Point(4, 3)),
                "Неизвестный объект"
            };

            foreach (var test in testCases)
            {
                Console.WriteLine($"Вход: {test}");
                Console.WriteLine($"Ожидание/Результат: \"{Classify(test)}\"");
                Console.WriteLine(new string('-', 40));
            }
        }

        #region Ручной ввод с обработкой исключений (try-catch)

        static object ReadShapeFromConsole()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Выберите тип фигуры для создания:");
                    Console.WriteLine("1 — Окружность (Circle)");
                    Console.WriteLine("2 — Прямоугольник (Rectangle)");
                    Console.Write("Ваш выбор (1 или 2): ");

                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        Console.WriteLine("\n-- Создание окружности --");
                        double x = ReadDoubleFromConsole("Введите X центра: ");
                        double y = ReadDoubleFromConsole("Введите Y центра: ");
                        double radius = ReadDoubleFromConsole("Введите радиус (>= 0): ");

                        if (radius < 0)
                        {
                            throw new ArgumentException("Радиус не может быть отрицательным!");
                        }

                        return new Circle(new Point(x, y), radius);
                    }
                    else if (choice == "2")
                    {
                        Console.WriteLine("\n-- Создание прямоугольника --");
                        double x1 = ReadDoubleFromConsole("Введите X первой точки (TopLeft): ");
                        double y1 = ReadDoubleFromConsole("Введите Y первой точки (TopLeft): ");
                        double x2 = ReadDoubleFromConsole("Введите X второй точки (BottomRight): ");
                        double y2 = ReadDoubleFromConsole("Введите Y второй точки (BottomRight): ");

                        return new Rectangle(new Point(x1, y1), new Point(x2, y2));
                    }
                    else
                    {
                        throw new FormatException("Необходимо ввести 1 или 2.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"[Ошибка формата]: {ex.Message} Попробуйте снова.\n");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Ошибка ввода]: {ex.Message} Попробуйте снова.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Ошибка]: {ex.Message} Попробуйте снова.\n");
                }
            }
        }

        static double ReadDoubleFromConsole(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    string input = Console.ReadLine()?.Replace(',', '.');

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new ArgumentException("Строка не может быть пустой!");
                    }

                    if (!double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                    {
                        throw new FormatException($"Значение '{input}' не является числом.");
                    }

                    return result;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"[Ошибка формата]: {ex.Message} Попробуйте снова.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Ошибка ввода]: {ex.Message} Попробуйте снова.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Ошибка]: {ex.Message} Попробуйте снова.");
                }
            }
        }

        #endregion
    }
}