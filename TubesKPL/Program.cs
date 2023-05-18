namespace TubesKPL
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ChatBot bot = new ChatBot();
            ChatConfig config = new ChatConfig();

            Console.WriteLine("------ChatBot Otomatis------");
            Console.WriteLine("Apa yang ingin Anda tanyakan?");
            Console.WriteLine("1. Stok Produk");
            Console.WriteLine("2. Metode Pembayaran");
            Console.WriteLine("3. Metode Pengiriman");
            Console.WriteLine("Pilih salah satu : ");

            string ketik = Console.ReadLine();
            if(ketik== "1")
            {
                bot.ReadConfigFile();
            } else if (ketik == "2")
            {
                Console.WriteLine(config.pembayaran);
            } else if(ketik == "3")
            {
                Console.WriteLine(config.pengiriman);
            } else
            {
                Console.WriteLine("Pilihan tidak tersedia");
            }
        }
    }
}