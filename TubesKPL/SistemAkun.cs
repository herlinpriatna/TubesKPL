using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TubesKPL
{
    internal class SistemAkun
    {
        public enum StateAkun
        {
            Start, Login, Registrasi, RegistrasiPembeli, LoginPembeli, TampilanPembeli, RegistrasiPenjual, LoginPenjual, TampilanPenjual, Keluar
        }

        public enum Trigger
        {
            klikPembeli, klikPenjual, btnRegistrasi, btnLogin, Submit, Batal
        }

        class Transition
        {
            public StateAkun prevState;
            public StateAkun nextState;
            public Trigger trigger;

            public Transition(StateAkun prevState, StateAkun nextState, Trigger trigger)
            {
                this.prevState = prevState;
                this.nextState = nextState;
                this.trigger = trigger;
            }
        }

        Transition[] transitions =
        {
            new Transition(StateAkun.Start, StateAkun.Registrasi, Trigger.btnRegistrasi),
            new Transition(StateAkun.Start, StateAkun.Login, Trigger.btnLogin),
            new Transition(StateAkun.Start, StateAkun.Keluar, Trigger.Batal),

            //registrasi
            new Transition(StateAkun.Registrasi, StateAkun.RegistrasiPembeli, Trigger.klikPembeli),
            new Transition(StateAkun.Registrasi, StateAkun.RegistrasiPenjual, Trigger.klikPenjual),
            new Transition(StateAkun.Registrasi, StateAkun.Keluar, Trigger.Batal),

            new Transition(StateAkun.RegistrasiPembeli, StateAkun.Start, Trigger.Submit),
            new Transition(StateAkun.RegistrasiPembeli, StateAkun.Registrasi, Trigger.Batal),

            new Transition(StateAkun.RegistrasiPenjual, StateAkun.Start, Trigger.Submit),
            new Transition(StateAkun.RegistrasiPenjual, StateAkun.Registrasi, Trigger.Batal),

            //login
            new Transition(StateAkun.Login, StateAkun.LoginPembeli, Trigger.klikPembeli),
            new Transition(StateAkun.Login, StateAkun.LoginPenjual, Trigger.klikPenjual),
            new Transition(StateAkun.Login, StateAkun.Keluar, Trigger.Batal),

            new Transition(StateAkun.LoginPembeli, StateAkun.TampilanPembeli, Trigger.Submit),
            new Transition(StateAkun.LoginPembeli, StateAkun.Login, Trigger.Batal),

            new Transition(StateAkun.LoginPenjual, StateAkun.TampilanPenjual, Trigger.Submit),
            new Transition(StateAkun.LoginPenjual, StateAkun.Login, Trigger.Batal),

            // Bagian MainScreen
            new Transition(StateAkun.TampilanPembeli, StateAkun.Start, Trigger.Batal),
            new Transition(StateAkun.TampilanPenjual, StateAkun.Start, Trigger.Batal)
        };

        public StateAkun currentState;
        public StateAkun getNextState(StateAkun prevState, Trigger trigger)
        {
            StateAkun nextState = prevState;
            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].prevState == prevState && transitions[i].trigger == trigger)
                {
                    nextState = transitions[i].nextState;
                }
            }
            return nextState;
        }

        public void activeTrigger(Trigger trigger)
        {
            StateAkun nextState = getNextState(currentState, trigger);
            currentState = nextState;
        }

        public void Register()
        {
            //Preconditions
            Debug.Assert(currentState == SistemAkun.StateAkun.RegistrasiPembeli ||
                currentState == SistemAkun.StateAkun.RegistrasiPenjual, "Invalid State");

            if (currentState == SistemAkun.StateAkun.RegistrasiPembeli)
            {
                Console.WriteLine("--Registrasi Pembeli--");
            }
            else
            {
                Console.WriteLine("--Registrasi Penjual--");
            }
            Console.WriteLine("Username (maks: 20 huruf, tidak ada spasi)");
            Console.Write("Input: ");
            string username = Console.ReadLine();
            // Cek panjang username dan apakah terdapat spasi pada username
            if (username.Length > 20 || username.Contains(" "))
            {
                Console.WriteLine("Username tidak valid");
                return;
            }
            Console.WriteLine("Password (maks: 16 karakter, tidak ada spasi)");
            Console.Write("Input: ");
            string password = Console.ReadLine();

            // Cek panjang password dan apakah terdapat spasi pada password
            if (password.Length > 16 || password.Contains(" "))
            {
                Console.WriteLine("Password tidak valid");
                return;
            }

            // Memasukkan data ke dengan teknik Runtime Config
            string tipe_akun = "";
            if (currentState == StateAkun.RegistrasiPembeli)
            {
                tipe_akun = "Pembeli";
            }
            else if (currentState == StateAkun.RegistrasiPenjual)
            {
                tipe_akun = "Penjual";
            }
            Config config = new Config(tipe_akun, username, password);
            Akun acc = new Akun();
            acc.config = config;
            acc.WriteConfigFile();

            Console.WriteLine("\nRegistrasi berhasil!");
            Console.WriteLine("Kembali ke halaman Registrasi/Login");
        }
        public void Login()
        {
            //Preconditions
            Debug.Assert(currentState == SistemAkun.StateAkun.LoginPembeli || currentState == SistemAkun.StateAkun.LoginPenjual, "Invalid State");
            String tipe_akun = "";
            if (currentState == SistemAkun.StateAkun.LoginPembeli)
            {
                Console.WriteLine("--Login Pembeli--");
                tipe_akun = "Pembeli";
            }
            else if (currentState == SistemAkun.StateAkun.LoginPenjual)
            {
                Console.WriteLine("--Login Penjual--");
                tipe_akun = "Penjual";
            }
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            // Pengecekan tipe akun, username, password
            Akun acc = new Akun();
            Config config = acc.ReadConfigFile();

            if (tipe_akun == config.tipe_akun && username == config.username && password == config.password)
            {
                Console.WriteLine("Login berhasil!\n");
                activeTrigger(Trigger.Submit);
            }
            else
            {
                Console.WriteLine("Login gagal, username atau password salah");
                Console.WriteLine("Kembali ke layar Login.\n");
                activeTrigger(Trigger.Batal);
            }
        }
        public void MainScreen()
        {
            //Preconditions
            Debug.Assert(currentState == SistemAkun.StateAkun.TampilanPembeli ||
                currentState == SistemAkun.StateAkun.TampilanPenjual, "Invalid state");

            Akun acc = new Akun();
            Config config = acc.ReadConfigFile();

            if (currentState == SistemAkun.StateAkun.TampilanPembeli)
            {
                Console.WriteLine("Selamat datang, pembeli " + config.username + "!");
            }
            else if (currentState == SistemAkun.StateAkun.TampilanPenjual)
            {
                Console.WriteLine("Selamat datang, tenant " + config.username + "!");
            }

            int menuMain = 0;

            while (currentState != StateAkun.Start)
            {
                switch (menuMain)
                {
                    case 0:
                        Console.WriteLine("Menu: ");
                        Console.WriteLine("1. Logout");
                        try
                        {
                            menuMain = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Input invalid. Tolong masukkan inputan angka.\n");
                        }
                        break;
                    case 1:
                        Console.WriteLine("Logout. Kembali ke halaman Registrasi/Login");
                        activeTrigger(Trigger.Batal);
                        break;
                    default:
                        Console.WriteLine("Input invalid.");
                        menuMain = 0;
                        break;
                }
            }
        }
    }

}



           