
namespace UDP_Asynchronous_Chat
{
    public enum PACKET_TYPE
    {
        DISCOVERY = 0, CONFIRMATION, TEXT, IMAGE
    }
    public class ChatPacket
    {
        public string Message { get; set; }
        public PACKET_TYPE PacketType { get; set; }

        public byte[] RawData { get; set; }

    }
}
