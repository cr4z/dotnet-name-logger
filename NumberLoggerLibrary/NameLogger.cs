namespace NameLoggerLibrary
{
    /// <summary>
    /// Configuration options for NameLogger operations.
    /// </summary>
    /// <param name="upperBounds">The upper limit for number generation.</param>
    /// <param name="userLogRules">List of names with specified factors at which names should render.</param>
    public class NameLoggerOptions(int upperBounds, List<UserLogRule> userLogRules)
    {
        public int UpperBounds { get; set; } = upperBounds;
        public List<UserLogRule> UserLogRules { get; set; } = userLogRules ?? [];
    }

    /// <summary>
    /// Processes numbers from 1 to UpperBounds, applying user-defined rules to replace numbers with names based on divisibility factors.
    /// </summary>
    /// <param name="options">Configuration containing upper bounds and name rules to apply.</param>
    public class NameLogger(NameLoggerOptions options)
    {
        public int UpperBounds { get; set; } = options.UpperBounds;

        public List<string> Run()
        {
            List<string> res = [];
            int i = 0;
            while (i < UpperBounds)
            {
                i++;
                var matchingRulePairs = new List<string>();

                foreach (var rule in options.UserLogRules)
                {
                    bool firstNameMatches = i % rule.FirstNameIndex == 0;
                    bool lastNameMatches = i % rule.LastNameIndex == 0;

                    if (firstNameMatches || lastNameMatches)
                    {
                        var nameParts = new List<string>();
                        if (firstNameMatches) nameParts.Add(rule.FirstName);
                        if (lastNameMatches) nameParts.Add(rule.LastName);

                        matchingRulePairs.Add(string.Join(" ", nameParts));
                    }
                }

                string msg = matchingRulePairs.Count > 0
                    ? string.Join(", ", matchingRulePairs)
                    : i.ToString();

                res.Add(msg);
            }
            return res;
        }
    }

}