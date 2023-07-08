namespace RoPaScLiSp.GameLogic
{
    public enum DecisionOptions
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }

    public static class GameRoundDecision
    {
        public const string AWins = "AWins";
        public const string BWins = "BWins";
        public const string Draw = "Draw";
        public static readonly int DecisionOptionCount = Enum.GetValues(typeof(DecisionOptions)).Length;
        public static readonly List<string> DecisionOptionNames = Enum.GetNames(typeof(DecisionOptions)).ToList();

        public static bool IsTurnDecisionValid(string decision)
        {
            return DecisionOptionNames.Contains(decision, StringComparer.OrdinalIgnoreCase);
        }

        public static string TurnWinner(string aOption, string bOption)
        {


            if (!Enum.TryParse(aOption, true, out DecisionOptions aDecision)
                || !Enum.TryParse(bOption, true, out DecisionOptions bDecision))
            {
                return "Invalid input parameters";
            }

            if (aDecision == bDecision)
            {
                return Draw;
            }

            switch (aDecision)
            {
                case DecisionOptions.Rock:
                    return (bDecision == DecisionOptions.Scissors || bDecision == DecisionOptions.Lizard)
                        ? AWins
                        : BWins;

                case DecisionOptions.Paper:
                    return (bDecision == DecisionOptions.Rock || bDecision == DecisionOptions.Spock)
                        ? AWins
                        : BWins;


                case DecisionOptions.Scissors:
                    return (bDecision == DecisionOptions.Paper || bDecision == DecisionOptions.Lizard)
                        ? AWins
                        : BWins;

                case DecisionOptions.Lizard:
                    return (bDecision == DecisionOptions.Paper || bDecision == DecisionOptions.Spock)
                        ? AWins
                        : BWins;

                case DecisionOptions.Spock:
                    return (bDecision == DecisionOptions.Rock || bDecision == DecisionOptions.Scissors)
                        ? AWins
                        : BWins;
            }

            return "Game logic not written correctly. You found bug in code";
        }
    }
}
