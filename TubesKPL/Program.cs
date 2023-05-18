// See https://aka.ms/new-console-template for more information
using System;
using TubesKPL;

namespace TubesKPL
{
    class program
    {
        
        public static void Main(string[] args)
        {
            SistemAkun akun = new SistemAkun();
            akun.currentState = SistemAkun.StateAkun.Start;

            Console.WriteLine("Selamat datang.");

            while (akun.currentState != SistemAkun.StateAkun.Keluar)
            {
                Console.WriteLine("Pilih Menu:");
                Console.WriteLine("1. Registrasi");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Keluar");
                Console.WriteLine("Pilih: ");
                int selection = Convert.ToInt32(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        akun.activeTrigger(SistemAkun.Trigger.btnRegistrasi);
                        Console.WriteLine("\nAnda berada di halaman Registrasi");
                        Console.WriteLine("Pilih tipe akun:");
                        Console.WriteLine("1. Pembeli");
                        Console.WriteLine("2. Penjual");
                        Console.WriteLine("3. Cancel");
                        Console.WriteLine("Pilih: ");
                        int regisSelect = Convert.ToInt32(Console.ReadLine());

                        switch (regisSelect)
                        {
                            case 1:
                                akun.activeTrigger(SistemAkun.Trigger.klikPembeli);
                                akun.Register();
                                akun.activeTrigger(SistemAkun.Trigger.Submit);
                                break;
                            case 2:
                                akun.activeTrigger(SistemAkun.Trigger.klikPenjual);
                                akun.Register();
                                akun.activeTrigger(SistemAkun.Trigger.Submit);
                                break;
                            case 3:
                                akun.activeTrigger(SistemAkun.Trigger.Batal);
                                break;
                            default:
                                Console.WriteLine("Pilihan Invalid.");
                                break;
                        }
                        break;

                    case 2:
                        akun.activeTrigger(SistemAkun.Trigger.btnRegistrasi);
                        Console.WriteLine("\nAnda berada di halaman Login");
                        Console.WriteLine("Pilih tipe akun:");
                        Console.WriteLine("1. Pembeli");
                        Console.WriteLine("2. Penjual");
                        Console.WriteLine("3. Cancel");
                        Console.WriteLine("Pilih: ");
                        int loginSelect = Convert.ToInt32(Console.ReadLine());

                        switch (loginSelect)
                        {
                            case 1:
                                akun.activeTrigger(SistemAkun.Trigger.klikPembeli);
                                akun.Login();
                                akun.activeTrigger(SistemAkun.Trigger.Submit);
                                akun.MainScreen();
                                break;
                            case 2:
                                akun.activeTrigger(SistemAkun.Trigger.klikPenjual);
                                akun.Login();
                                akun.activeTrigger(SistemAkun.Trigger.Submit);
                                akun.MainScreen();
                                break;
                            case 3:
                                akun.activeTrigger(SistemAkun.Trigger.Batal) ;
                                break;
                            default:
                                Console.WriteLine("Pilihan Invalid.");
                                break;
                        }
                        break;

                    case 3:
                        akun.activeTrigger(SistemAkun.Trigger.Batal);
                        break;

                    default:
                        Console.WriteLine("Pilihan Invalid.");
                        break;
                }

            }

            Console.WriteLine("Program diberhentikan.");
        }
    }
}