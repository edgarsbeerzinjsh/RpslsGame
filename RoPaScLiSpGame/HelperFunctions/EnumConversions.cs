namespace RpslsGame.HelperFunctions
{
    public static class EnumConversions
    {
        public static string FirstTwoLettersOfEnumToString<T>(T yourEnum) where T : Enum
        {
            return yourEnum.ToString().Substring(0, 2).ToLower();
        }

        public static string? EnumNameFromFirstTwoLetters<T>(string firstTwoLetters) where T : Enum
        {
            foreach (var turnOption in EnumToArray<T>())
            {
                if (firstTwoLetters.ToLower() == FirstTwoLettersOfEnumToString((T)turnOption).ToLower())
                {
                    return turnOption.ToString();
                }
            }

            return null;
        }

        public static bool IsStringEnumName<T>(string possibleEnum) where T : Enum
        {
            foreach (var turnOption in EnumToArray<T>())
            {
                if (possibleEnum.ToLower() == turnOption.ToString().ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        public static string? NumberToEnumName<T>(string number) where T : Enum
        {
            if (int.TryParse(number, out int result))
            {
                if (Enum.IsDefined(typeof(T), result))
                {
                    return Enum.GetName(typeof(T), result);
                }
            }

            return null;
        }

        public static Array EnumToArray<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T));
        }
    }
}
