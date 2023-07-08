namespace RoPaScLiSp.HelperFunctions
{
    public static class UserInputValidations
    {
        public static bool IsNumberInRangeValidation(int number, int min, int max)
        {
            return number >= min && number <= max;
        }
    }
}
