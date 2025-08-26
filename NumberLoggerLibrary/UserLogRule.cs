namespace NameLoggerLibrary
{
    /// <summary>
    /// Represents a user-defined logging rule that specifies name pairs and their corresponding division factors.
    /// </summary>
    public record UserLogRule(string FirstName, int FirstNameIndex, string LastName, int LastNameIndex);
}