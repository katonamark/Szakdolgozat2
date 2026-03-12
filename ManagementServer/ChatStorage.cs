using ManagementServer;
using System.Text.Json;

namespace ManagementServer
{
    public static class ChatStorage
    {
        private static readonly string FilePath = "chat_history.json";

        public static List<ChatRecord> LoadMessages()
        {
            if (!File.Exists(FilePath))
                return new List<ChatRecord>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<ChatRecord>>(json) ?? new List<ChatRecord>();
        }

        public static void SaveMessages(List<ChatRecord> messages)
        {
            var json = JsonSerializer.Serialize(messages, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FilePath, json);
        }

        public static void AddMessage(ChatRecord record)
        {
            var messages = LoadMessages();
            messages.Add(record);
            SaveMessages(messages);
        }

        public static List<ChatRecord> GetConversation(string agentName)
        {
            var messages = LoadMessages();

            return messages
                .Where(m => m.TargetAgent == agentName)
                .OrderBy(m => m.Timestamp)
                .ToList();
        }
    }
}

