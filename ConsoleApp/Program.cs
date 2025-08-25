using NumberLoggerLibrary;

NumberLoggerOptions options = new(100);
NumberLogger logger = new(options);

List<string> logs = logger.Run();

Console.WriteLine(logs); // LINQ enumerate over

