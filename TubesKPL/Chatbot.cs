using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TubesKPL
{
    public class ChatConfig
    {
        public string stok_produk { get; set; }
        public string pembayaran { get; set; }
        public string pengiriman { get; set; }

        public ChatConfig() { }
        public ChatConfig(string stok_produk, string pembayaran, string pengiriman)
        {
            this.stok_produk = stok_produk;
            this.pembayaran = pembayaran;
            this.pengiriman = pengiriman;
        }
    }

    public class ChatBot
    {
        public ChatConfig chatConfig;
        public const string fileLocation = @"./chat_config.json";

        public ChatBot() 
        {
            try
            {
                ReadConfigFile();
            }
            catch
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }

        public ChatConfig ReadConfigFile()
        {
            string configJsonData = File.ReadAllText(fileLocation);
            chatConfig = JsonSerializer.Deserialize<ChatConfig>(configJsonData);
            return chatConfig;
        }

        public void SetDefault()
        {
           chatConfig = new ChatConfig("stok_produk", "pembayaran", "pengiriman");

        }

        public void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(chatConfig, options);
            File.WriteAllText(fileLocation, jsonString);
        }
    }
}
