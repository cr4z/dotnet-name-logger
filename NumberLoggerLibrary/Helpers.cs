using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameLoggerLibrary
{
    public static class Helpers
    {
        /// <summary>
        /// Cross-platform method that converts a list of strings into a single
        /// string with each item on a new line for logging purposes.
        /// </summary>
        public static string GetPrintable(List<string> items) => string.Join(Environment.NewLine, items);
    }
}
