using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace TubesKPL
{
    public class Akun
    {
        public Config config;
        public const string fileLocation = @"./akun_config.json";
        public Akun() {
            try
            {
                ReadConfigFile();
            }
            catch
            {
                SetDefault();
                WriteConfigFile();
            }
        
        }

        public Config ReadConfigFile()
        {
            string hasilBaca = File.ReadAllText(fileLocation);
            config = JsonSerializer.Deserialize<Config>(hasilBaca);
            return config;
        }

        public void SetDefault()
        {
            config = new Config("Pembeli", "username", "password");
        }
        public void WriteConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(config, options);
            File.WriteAllText(fileLocation, jsonString);
        }

    }

    public class Config
    {
        public string tipe_akun { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Config() { }
        public Config(string tipe_akun, string username, string password) { 
            this.tipe_akun = tipe_akun;
            this.username = username;
            this.password = password;
        }

    }

    
}