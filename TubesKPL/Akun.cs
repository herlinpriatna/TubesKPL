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
        public AkunConfig akun;
        private const string fileLocation = @"D:\About Telkom University\Semester 4\Konstruksi Perangkat Lunak\TubesKPL\TubesKPL\akun_config.json";
        
        public Akun()
        {
            try
            {
                ReadConfigFile();
            }
            catch
            {
                WriteConfigFile();
            }
        }

        private AkunConfig ReadConfigFile()
        {
            string hasilBaca = File.ReadAllText(fileLocation);
            akun = JsonSerializer.Deserialize<AkunConfig>(hasilBaca);
            return akun;
        }

        private void WriteConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string teksTulis = JsonSerializer.Serialize(akun, options);
            File.WriteAllText(fileLocation, teksTulis);
        }
    }

    public class AkunConfig
    {
        public List<Account> Pembeli {  get; set; }
        public List<Account> Penjual { get; set; }

        public class Account
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public AkunConfig() { }
        public AkunConfig(List<Account> pembeli, List<Account> penjual)
        {
            this.Pembeli = pembeli;
            this.Penjual = penjual;
        }
    }
    
    
    
    
    
    
    
    
    
    
    /*public class Config
    {
        public string tipeAkun { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Config() { }
        public Config(string tipeAkun, string username, string password)
        {
            this.tipeAkun = tipeAkun;
            this.username = username;
            this.password = password;
        }

    }
    public class Akun
    {
        public Config config;
        public const string filePath = @"D:\About Telkom University\Semester 4\Konstruksi Perangkat Lunak\TubesKPL\TubesKPL\akun_config.json";

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
            string configJsonData = File.ReadAllText(filePath);
            config = JsonSerializer.Deserialize<Config>(configJsonData);
            return config;
        }

        private void SetDefault()
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
            File.WriteAllText(filePath, jsonString);
        }
    }
    */
}