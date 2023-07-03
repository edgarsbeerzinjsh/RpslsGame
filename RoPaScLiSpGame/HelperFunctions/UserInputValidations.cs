namespace RpslsGame.HelperFunctions
{
    public static class UserInputValidations
    {
        public static bool IsInputNumber(string playerInput)
        {
            return Int32.TryParse(playerInput, out _);
        }

        public static string NumberRangeValidation(string number, int min, int max)
        {
            if (Int32.TryParse(number, out int inputNumber))
            {
                if (inputNumber >= min && inputNumber <= max)
                {
                    return number;
                }
            };

            return null;
        }
    }
}
