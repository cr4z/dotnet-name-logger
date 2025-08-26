using NameLoggerLibrary;

List<UserLogRule> userLogRules = [
    new UserLogRule("Steven", 3, "Bennett", 3),
    new UserLogRule("Juan", 4, "Gonzales", 7)
];

NameLoggerOptions options = new(100, userLogRules);
NameLogger logger = new(options);

List<string> logs = logger.Run();

Console.WriteLine(Helpers.GetPrintable(logs));