using CalculatorLibrary;

namespace MicrosoftCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            bool endApp = false;
            
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                // Declare variables and set to empty.
                double cleanNum1;
                double cleanNum2;

                if (calculator.CalculationList.Any())
                {
                    // Ask the user to type the first number.
                    cleanNum1 = GetInput("Type a number, and then press Enter: ", calculator.CalculationList);
                    cleanNum2 = GetInput("Type another number, and then press Enter: ", calculator.CalculationList);
                }
                else
                {
                    Console.Write("Type a number, and then press Enter: ");
                    cleanNum1 = CleanNum();
                    Console.Write("Type another number, and then press Enter: ");
                    cleanNum2 = CleanNum();
                }
                
                ShowMenu();

                string op = Console.ReadLine();

                try
                {
                    var result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        calculator.IncreaseCount();
                        calculator.CalculationList.Add(result);
                        ClearList(calculator.CalculationList);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            
            calculator.Finish();
            return;
        }

        private static void ClearList(List<double> list)
        {
            Console.Write("\nDo you want to clear your list? (Y/y) ");
            if (GetApproval())
            {
                list.Clear();
            }
        }
        private static double GetInput(string message, List<double> list)
        {
            Console.WriteLine("Do you want to take from list (Y/y): ");
            if (GetApproval())
            {
                return GetListItem(message, list);
            } 
            
            Console.Write(message); 
            return CleanNum();
        }

        private static bool GetApproval() => (Console.ReadLine() == "y" || Console.ReadLine() == "Y");

        private static void ShowMenu()
        {
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");
        }

        private static double GetListItem(string message, List<double> calculatorCalculationList)
        {
            int indexInput;
            Console.Write(message);
            foreach(var item in calculatorCalculationList)
            {
                Console.Write($"{item} -  ");
            }
            Console.Write("Enter index: ");
            while (!int.TryParse(Console.ReadLine(), out indexInput) && calculatorCalculationList.Contains(calculatorCalculationList[indexInput]))
            {
                Console.WriteLine("item doesn't exist in list. Enter index again");
            }

            return calculatorCalculationList[indexInput];
        }
        private static double CleanNum()
        {
            double cleanNum;
            while (!double.TryParse(Console.ReadLine(), out cleanNum))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
            }

            return cleanNum;
        }
    }
}