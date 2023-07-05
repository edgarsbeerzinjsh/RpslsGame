using RpslsGame.HelperFunctions;

namespace RpslsGame.Players
{
    public class HumanPlayer : Player
    {
        public int PlayedGames { get; set; } = 0;
        public HumanPlayer(string name) : base(name)
        {
        }

        public override T MakeTurnDecision<T>()
        {
            var playerOption = HumanInteractionForInput<T>();

            T choiseEnum = (T)Enum.Parse(typeof(T), playerOption, true);
           
            return choiseEnum;
        }

        private string HumanInteractionForInput<T>() where T : Enum
        {
            bool validEnumName = false;

            Console.WriteLine("Choose your option:");
            Console.WriteLine("([exit] or [ex] to exit from game)\n");

            foreach (var turnOption in EnumConversions.EnumToArray<T>())
            {
                var firstTwoLetters = EnumConversions.FirstTwoLettersOfEnumToString((T)turnOption);

                Console.WriteLine($"{turnOption} ({turnOption} / {firstTwoLetters} / {(int)turnOption})");
            }

            var input = GetValidInput<T>();

            return input;
        }

        private string GetValidInput<T>() where T : Enum
        {
            bool validEnumName = false;
            var input = "";

            do
            {
                input = Console.ReadLine().Trim().ToLower();
                var inputTwoCharOption = EnumConversions.EnumNameFromFirstTwoLetters<T>(input);

                if (input == "exit" || input == "ex")
                {
                    Console.WriteLine("\nOoh, maybe another time.");
                    Environment.Exit(0);
                }
                else if (EnumConversions.IsStringEnumName<T>(input))
                {
                    validEnumName = true;
                }
                else if (inputTwoCharOption != null)
                {
                    validEnumName = true;
                    input = inputTwoCharOption;
                }
                else if (UserInputValidations.IsInputNumber(input))
                {
                    var numberInput = UserInputValidations
                        .NumberRangeValidation(input, 0, EnumConversions.EnumToArray<T>().Length - 1);
                    input = EnumConversions.NumberToEnumName<T>(numberInput);

                    if (input != null)
                    {
                        validEnumName = true;
                    }
                }
                
                if (!validEnumName)
                {
                    Console.WriteLine("That was not one of options...");
                }

            } while (!validEnumName);

            return input;
        }
    }
}
