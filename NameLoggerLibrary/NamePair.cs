namespace NameLoggerLibrary
{
    /// <summary>
    /// Represents a pair of names where either or both can be null.
    /// </summary>
    public record NamePair(string? FirstName, string? LastName)
    {
        public override string ToString() =>
            string.Join(" ", new[] { FirstName, LastName }.Where(x => x != null));
    }
}