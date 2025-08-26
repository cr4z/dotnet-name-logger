namespace NameLoggerLibrary.Tests
{
    public class NameLoggerTests
    {
        #region Core Functionality
        [Fact]
        public void Run_PrintsNumbersAndNames_AsExpected()
        {
            var userLogRules = new List<UserLogRule>
            {
                new("Steven", 3, "Bennett", 3),
                new("Juan", 3, "Gonzales", 3)
            };
            var options = new NameLoggerOptions(6, userLogRules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal("1", result[0]); // plain number
            Assert.Equal("2", result[1]); // plain number
            Assert.Equal("Steven Bennett, Juan Gonzales", result[2]); // i = 3
            Assert.Equal("4", result[3]); // plain number
            Assert.Equal("5", result[4]); // plain number
            Assert.Equal("Steven Bennett, Juan Gonzales", result[5]); // i = 6
        }

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

        [Fact]
        public void Run_WithOverlappingFactors_PrintsAllMatchingNames()
        {
            var userLogRules = new List<UserLogRule>
            {
                new("Steven", 3, "Bennett", 3),
                new("Juan", 3, "Gonzales", 3)
            };
            var options = new NameLoggerOptions(10, userLogRules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal("Steven Bennett, Juan Gonzales", result[2]); // i = 3
            Assert.Equal("Steven Bennett, Juan Gonzales", result[5]); // i = 6
            Assert.Equal("Steven Bennett, Juan Gonzales", result[8]); // i = 9
        }

        #endregion

        #region Input Validation
        [Fact]
        public void Run_WithSameNames_NoDuplication()
        {
            var rules = new List<UserLogRule>
            {
                new("John", 3, "John", 5)  // Same name for first and last
            };
            var options = new NameLoggerOptions(15, rules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal("John John", result[14]); // 15: should show both even if same
        }

        [Fact]
        public void Run_WithEmptyNames_HandlesGracefully()
        {
            var rules = new List<UserLogRule> { new("", 3, "", 5) };
            var options = new NameLoggerOptions(15, rules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal(" ", result[14]); // 15: empty names with space
        }

        #endregion

        #region Bounds Conditions  
        [Fact]
        public void Run_WithMultipleRules_SameNumber()
        {
            var rules = new List<UserLogRule>
            {
                new("John", 3, "Doe", 7),
                new("Jane", 3, "Smith", 11)
            };
            var options = new NameLoggerOptions(12, rules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal("John, Jane", result[2]);  // 3: both first names
            Assert.Equal("John, Jane", result[5]);  // 6: still both first names
            Assert.Equal("John, Jane", result[8]);  // 9: still both first names
            Assert.Equal("Doe", result[6]);         // 7: John's last name
            Assert.Equal("Smith", result[10]);      // 11: Jane's last name
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(1000)]
        public void Run_WithVariousUpperBounds_ReturnsCorrectCount(int upperBounds)
        {
            var options = new NameLoggerOptions(upperBounds, []);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal(upperBounds, result.Count);
        }

        [Fact]
        public void Run_WithZeroUpperBounds_ReturnsEmpty()
        {
            var options = new NameLoggerOptions(0, []);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Empty(result);
        }
        #endregion

        #region Factors Handling
        [Fact]
        public void Run_WithNegativeFactors_TreatsAsPositive()
        {
            var rules = new List<UserLogRule> { new("John", -3, "Doe", 5) };
            var options = new NameLoggerOptions(10, rules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal("John", result[2]); // 3
            Assert.Equal("Doe", result[4]);  // 5
            Assert.Equal("John", result[5]); // 6
            Assert.Equal("John", result[8]); // 9
            Assert.Equal("Doe", result[9]);  // 10
        }

        [Fact]
        public void Run_FactorOfOne_MatchesAllNumbers()
        {
            var rules = new List<UserLogRule> { new("Every", 1, "Number", 1) };
            var options = new NameLoggerOptions(5, rules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.All(result, item => Assert.Equal("Every Number", item));
        }

        [Fact]
        public void Run_WithLargeFactors_WorksCorrectly()
        {
            var rules = new List<UserLogRule> { new("Big", 1000, "Number", 2000) };
            var options = new NameLoggerOptions(2000, rules);
            var logger = new NameLogger(options);

            var result = logger.Run();

            Assert.Equal("Big", result[999]);    // 1000
            Assert.Equal("Big Number", result[1999]); // 2000
        }
        #endregion
    }
}
