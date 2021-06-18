public class DVD
{
    public delegate void DVDDelegate(string Title);
    public static DVDDelegate dvdDelegate;

    // Constructor that takes no arguments:
    public DVD()
    {
        Title = "unknown";
    }

    // Constructor that takes one argument:
    public DVD(string title)
    {
        Title = title;
        dvdDelegate(Title);
    }

    // Auto-implemented readonly property:
    public string Title { get; }

    // Method that overrides the base class (System.Object) implementation.
    public override string ToString()
    {
        return Title;
    }
}
