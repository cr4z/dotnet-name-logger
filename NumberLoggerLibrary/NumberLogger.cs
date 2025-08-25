namespace NumberLoggerLibrary
{
    public class NumberLoggerOptions(int upperBounds)
    {
        public int UpperBounds { get; set; } = upperBounds;
    }


    public class NumberLogger(NumberLoggerOptions options)
    {
        public int UpperBounds { get; set; } = options.UpperBounds;

        public List<string> Run()
        {
            List<string> res = [];
            int logs = 0;

            while (logs < UpperBounds)
            {
                logs++;
                string msg = logs.ToString();

                if ((logs % 3) == 0)
                {
                    msg = "Steven";
                }
                if ((logs % 5) == 0)
                {
                    msg = "Bennett";
                }
                if ((logs % 3 == 0) && (logs % 5 == 0))
                {
                    msg = "Steven Bennett";
                }

                res.Add(msg);
            }

            return res;
        }
    }
}