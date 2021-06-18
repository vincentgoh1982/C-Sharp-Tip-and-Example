namespace PolymorphismInterface
{
    public class Message
    {
        public delegate void MessageDelegate(float Btyes);
        public static MessageDelegate messageDelegate;
        // Constructor that takes no arguments:
        public Message()
        {
            fileSize = 0.0f;
        }

        // Constructor that takes one argument:
        public Message(float videobtyes)
        {
            fileSize = videobtyes;
            messageDelegate(fileSize);
        }

        // Auto-implemented readonly property:
        public float fileSize { get; }
    }
}
