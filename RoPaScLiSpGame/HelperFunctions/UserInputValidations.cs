namespace RpslsGame.HelperFunctions
{
    public static class UserInputValidations
    {
        public static bool IsInputNumber(string playerInput)
        {
            return int.TryParse(playerInput, out _);
        }

        public static string NumberRangeValidation(string number, int min, int max)
        {
            if (int.TryParse(number, out var inputNumber))
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
