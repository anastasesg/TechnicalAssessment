namespace Technical_Assessment.Application.Common
{
    public class MessageSplitter
    {
        public List<string> Split(string message, int size)
        {
            List<string> messages = new();
            for (int i = 0; i < message.Length; i += size)
            {
                if (message.Length - i >= size) messages.Add(message.Substring(i, size));
                else messages.Add(message[i..]);
            }
            return messages;
        }
    }
}