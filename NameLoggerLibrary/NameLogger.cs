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
                var namesAtThisLine = new List<string>();
                foreach (var rule in options.UserLogRules)
                {
                    bool firstNameIsAtIndex = i % rule.FirstNameIndex == 0;
                    bool lastNameIsAtIndex = i % rule.LastNameIndex == 0;
                    if (firstNameIsAtIndex || lastNameIsAtIndex)
                    {
                        var name = new NamePair(
                            firstNameIsAtIndex ? rule.FirstName : null,
                            lastNameIsAtIndex ? rule.LastName : null
                        );
                        namesAtThisLine.Add(name.ToString());
                    }
                }

                string msg = i.ToString();
                if (namesAtThisLine.Count > 0)
                {
                    msg = string.Join(", ", namesAtThisLine);
                }

                res.Add(msg);
            }
            return res;
        }
    }

}