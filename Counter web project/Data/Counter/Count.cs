using System;
using System.IO;
using System.Text.Json;

namespace Counter_web_project.Data.Counter
{
    public class Count
    {
        private const string FilePath = "Data/Counter/Count.json";

        public int Value { get; private set; }

        public Count()
        {
            LoadFromJson();
        }

        public void Increment()
        {
            Value++;
            SaveToJson();
        }

        public void Decrement()
        {
            Value--;
            SaveToJson();
        }

        public void Reset()
        {
            Value = 0;
            SaveToJson();
        }

        private void LoadFromJson()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(FilePath);
                    using JsonDocument doc = JsonDocument.Parse(jsonString);
                    Value = doc.RootElement.GetProperty("Count").GetInt32();
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., file not found, JSON parsing errors)
                    Console.WriteLine($"Error loading count: {ex.Message}");
                    Value = 0; // Default value in case of error
                }
            }
            else
            {
                Value = 0; // Default value if the file does not exist
            }
        }

        private void SaveToJson()
        {
            try
            {
                var countData = new { Count = Value };
                string jsonString = JsonSerializer.Serialize(countData);
                File.WriteAllText(FilePath, jsonString);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file write errors)
                Console.WriteLine($"Error saving count: {ex.Message}");
            }
        }
    }
}
