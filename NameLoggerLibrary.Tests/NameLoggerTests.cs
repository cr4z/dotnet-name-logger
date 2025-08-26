namespace NameLoggerLibrary.Tests
{
    public class NameLoggerTests
    {
        [Fact]
        public void Run_WithNoRules_ReturnsNumbers()
        {
            var options = new NameLoggerOptions(5, []);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal(["1", "2", "3", "4", "5"], result);
        }

        [Fact]
        public void Run_WithSingleRule_BothNamesMatch()
        {
            var rules = new List<UserLogRule> { new("John", 3, "Doe", 5) };
            var options = new NameLoggerOptions(15, rules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal("John", result[2]);     // 3: first name only
            Assert.Equal("Doe", result[4]);      // 5: last name only
            Assert.Equal("John", result[5]);     // 6: first name only
            Assert.Equal("John", result[8]);     // 9: first name only
            Assert.Equal("Doe", result[9]);      // 10: last name only
            Assert.Equal("John", result[11]);    // 12: first name only
            Assert.Equal("John Doe", result[14]); // 15: both names
        }

        [Fact]
        public void Run_WithMultipleRules_ComplexCombination()
        {
            var rules = new List<UserLogRule>
           {
               new("Steven", 3, "Bennett", 5),
               new("Juan", 4, "Gonzales", 6)
           };
            var options = new NameLoggerOptions(30, rules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal("Steven", result[2]);                  // 3
            Assert.Equal("Juan", result[3]);                    // 4
            Assert.Equal("Bennett", result[4]);                 // 5
            Assert.Equal("Steven, Gonzales", result[5]);        // 6
            Assert.Equal("Juan", result[7]);                    // 8
            Assert.Equal("Steven", result[8]);                  // 9
            Assert.Equal("Bennett", result[9]);                 // 10
            Assert.Equal("Steven, Juan Gonzales", result[11]);  // 12
            Assert.Equal("Steven Bennett", result[14]);         // 15
            Assert.Equal("Juan", result[15]);                   // 16
            Assert.Equal("Steven, Gonzales", result[17]);       // 18
            Assert.Equal("Steven, Juan Gonzales", result[23]); // 24
        }
    }
}
