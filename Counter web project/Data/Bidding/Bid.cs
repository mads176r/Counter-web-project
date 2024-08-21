using System.Text.Json;
using System.IO;

namespace Counter_web_project.Data.Bidding
{
    public class Bid
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        // Parameterless constructor
        public Bid()
        {
        }

        // Constructor with parameters
        public Bid(string name, double amount)
        {
            Name = name;
            Amount = amount;
        }

        public void CreateBid(Bid newBid)
        {
            var bids = LoadFromJson();
            bids.Add(newBid);
            SaveToJson(bids);
        }

        public List<Bid> GetBids()
        {
            List<Bid> bids = new List<Bid>();
            bids = LoadFromJson();

            bids.Sort((x, y) => y.Amount.CompareTo(x.Amount));

            return bids;
        }

        private void SaveToJson(List<Bid> bids)
        {
            try
            {
                string filePath = GetFilePath();
                string jsonString = JsonSerializer.Serialize(bids);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error saving bids: {ex.Message}");
            }
        }

        private List<Bid> LoadFromJson()
        {
            List<Bid> bids = new List<Bid>();
            try
            {
                string filePath = GetFilePath();
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);
                    bids = JsonSerializer.Deserialize<List<Bid>>(jsonString) ?? new List<Bid>();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error loading bids: {ex.Message}");
            }

            return bids;
        }

        private static string GetFilePath()
        {
            // Adjust this path based on your application's root directory or configuration
            return Path.Combine("Data", "Bidding", "Bids.json");
        }

        public override string ToString()
        {
            return $"{Name} - ${Amount:F2}";
        }
    }
}
