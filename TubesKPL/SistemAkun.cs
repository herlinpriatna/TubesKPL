using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
            new Transition(StateAkun.Start, StateAkun.Registrasi, Trigger.btnRegistrasi ),
            new Transition(StateAkun.Start, StateAkun.Login, Trigger.btnLogin),
              new Transition(StateAkun.Start, StateAkun.Keluar, Trigger.Batal),

              //registrasi
              new Transition(StateAkun.Registrasi, StateAkun.RegistrasiPembeli, Trigger.klikPembeli),
              new Transition(StateAkun.Registrasi, StateAkun.RegistrasiPenjual, Trigger.klikPenjual),
              new Transition(StateAkun.Registrasi, StateAkun.Keluar, Trigger.Batal),

              new Transition(StateAkun.RegistrasiPembeli, StateAkun.Start, Trigger.Submit),
              new Transition(StateAkun.RegistrasiPembeli, StateAkun.Registrasi, Trigger.Batal),

              new Transition(StateAkun.RegistrasiPembeli, StateAkun.Start, Trigger.Submit),
              new Transition(StateAkun.RegistrasiPembeli, StateAkun.Registrasi, Trigger.Batal),

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
                currentState == SistemAkun.StateAkun.RegistrasiPenjual, "Invalid state");

            if (currentState == SistemAkun.StateAkun.RegistrasiPembeli)
            {
                Console.WriteLine("--Registrasi Pembeli--");
            }
            else if (currentState == SistemAkun.StateAkun.RegistrasiPenjual)
            {
                Console.WriteLine("--Registrasi Penjual--");
            }

            Console.WriteLine("Masukkan Nama (maks: 25 huruf, tidak ada spasi)");
            string Name = Console.ReadLine();
            // Cek panjang username dan apakah terdapat spasi pada username
            if (Name.Length > 25 || Name.Contains(" "))
            {
                Console.WriteLine("Nama yang dimasukkan tidak valid");
                return;
            }

            Console.WriteLine("Masukkan Password (maks: 8 karakter, tidak ada spasi)");

            string password = Console.ReadLine();

            // Cek panjang password dan apakah terdapat spasi pada password
            if (password.Length > 8 || password.Contains(" "))
            {
                Console.WriteLine("Password yang dimasukkan tidak valid");
                return;
            }
        };
        
    
    /*
    internal class SistemAkun
    {
         public SistemAkun() { }
         public enum State
          {
              Start, Login, Registrasi,
              RegistrasiPembeli, LoginPembeli, TampilanPembeli,
              RegistrasiPenjual, LoginPenjual, TampilanPenjual,
              End

          };
          public enum Trigger { klikPembeli, klikPenjual, btnRegistrasi, btnLogin, Submit, Batal };

          class transisi
          {
              public State prevState;
              public State nextState;
              public Trigger trigger;

              public transisi(State prevState, State nextState, Trigger trigger)
              {
                  this.prevState = prevState;
                  this.nextState = nextState;
                  this.trigger = trigger;
              }
          }


          transisi[] transisiArr =
              {
              //start
              new transisi(State.Start, State.Registrasi, Trigger.btnRegistrasi),
              new transisi(State.Start, State.Login, Trigger.btnLogin),
              new transisi(State.Start, State.End, Trigger.Batal),

              //registrasi
              new transisi(State.Registrasi, State.RegistrasiPembeli, Trigger.klikPembeli),
              new transisi(State.Registrasi, State.RegistrasiPenjual, Trigger.klikPenjual),
              new transisi(State.Registrasi, State.End, Trigger.Batal),

              new transisi(State.RegistrasiPembeli, State.Start, Trigger.Submit),
              new transisi(State.RegistrasiPembeli, State.Registrasi, Trigger.Batal),

              new transisi(State.RegistrasiPembeli, State.Start, Trigger.Submit),
              new transisi(State.RegistrasiPembeli, State.Registrasi, Trigger.Batal),

              //login
              new transisi(State.Login, State.LoginPembeli, Trigger.klikPembeli),
              new transisi(State.Login, State.LoginPenjual, Trigger.klikPenjual),
              new transisi(State.Login, State.End, Trigger.Batal),

              new transisi(State.LoginPembeli, State.TampilanPembeli, Trigger.Submit),
              new transisi(State.LoginPembeli, State.Login, Trigger.Batal),

              new transisi(State.LoginPenjual, State.TampilanPenjual, Trigger.Submit),
              new transisi(State.LoginPenjual, State.Login, Trigger.Batal),

              // Bagian MainScreen
              new transisi(State.TampilanPembeli, State.Start, Trigger.Batal),
              new transisi(State.TampilanPenjual, State.Start, Trigger.Batal)
          };

              public State currentState;

              public State getNextState(State prevState, Trigger trigger)
              {
                  State nextState = prevState;
                  for (int i = 0; i < transisiArr.Length; i++)
                  {
                      if (transisiArr[i].prevState == prevState && transisiArr[i].trigger == trigger)
                      {
                          nextState = transisiArr[i].nextState;
                      }
                  }
                  return nextState;
              }
              public void activeTrigger(Trigger trigger)
              {
                  State nextState = getNextState(currentState, trigger);
                  currentState = nextState;
              }

              public void Register()
              {
                  //Preconditions
                  Debug.Assert(currentState == SistemAkun.State.RegistrasiPembeli ||
                      currentState == SistemAkun.State.RegistrasiPenjual, "Invalid state");

                  if (currentState == SistemAkun.State.RegistrasiPembeli)
                  {
                      Console.WriteLine("--Registrasi Pembeli--");
                  }
                  else if (currentState == SistemAkun.State.RegistrasiPenjual)
                  {
                      Console.WriteLine("--Registrasi Penjual--");
                  }

                  Console.WriteLine("Masukkan Nama (maks: 25 huruf, tidak ada spasi)");
                  string Name = Console.ReadLine();
                  // Cek panjang username dan apakah terdapat spasi pada username
                  if (Name.Length > 25 || Name.Contains(" "))
                  {
                      Console.WriteLine("Nama yang dimasukkan tidak valid");
                      return;
                  }

                  Console.WriteLine("Masukkan Password (maks: 8 karakter, tidak ada spasi)");

                  string password = Console.ReadLine();

                  // Cek panjang password dan apakah terdapat spasi pada password
                  if (password.Length > 8 || password.Contains(" "))
                  {
                      Console.WriteLine("Password yang dimasukkan tidak valid");
                      return;
                  }

                  // Memasukkan data ke dengan teknik Runtime Config
                  string tipe_akun = "";
                  if (currentState == State.RegistrasiPembeli)
                  {
                      tipe_akun = "Pembeli";
                  }
                  else if (currentState == State.RegistrasiPenjual)
                  {
                      tipe_akun = "Penjual";
                  }

                  Config config = new Config(tipe_akun, Name, password);
                  Akun acc = new Akun();
                  acc.config = config;
                  acc.WriteConfigFile();

                  Console.WriteLine("\nRegistrasi berhasil!");
                  Console.WriteLine("Kembali ke halaman Registrasi/Login");
              }

              public void Login()
              {
                  //Preconditions
                  //Debug.Assert(currentState == AccountSystem.State.PembeliLogin ||
                  //  currentState == AccountSystem.State.PenjualLogin, "Invalid state");

                  String tipe_akun = "";
                  if (currentState == SistemAkun.State.LoginPembeli)
                  {
                      Console.WriteLine("--Login Pembeli--");
                      tipe_akun = "Pembeli";
                  }
                  else if (currentState == SistemAkun.State.LoginPenjual)
                  {
                      Console.WriteLine("--Login Penjual--");
                      tipe_akun = "Penjual";
                  }

                  Console.Write("Nama: ");
                  string Name = Console.ReadLine();

                  Console.Write("Password: ");
                  string password = Console.ReadLine();

                  // Pengecekan tipe akun, username, password
                  Akun acc = new Akun();
                  Config config = acc.ReadConfigFile();

                  if (tipe_akun == config.tipeAkun && Name == config.username && password == config.password)
                  {
                      Console.WriteLine("Login berhasil!");
                  }
                  else
                  {
                      Console.WriteLine("Login gagal, Name atau password salah");
                  }
              }

              public void MainScreen()
              {
                  //Preconditions
                  //Debug.Assert(currentState == AccountSystem.State.PembeliScreen ||
                  //  currentState == AccountSystem.State.PenjualScreen, "Invalid state");

                  Akun acc = new Akun();
                  Config config = acc.ReadConfigFile();

                  if (currentState == SistemAkun.State.TampilanPembeli)
                  {
                      Console.WriteLine("Selamat datang, pembeli " + config.username + "!");
                  }
                  else if (currentState == SistemAkun.State.TampilanPenjual)
                  {
                      Console.WriteLine("Selamat datang, Penjual " + config.username + "!");
                  }

                  Console.WriteLine("Menu: ");
                  Console.WriteLine("1. Logout");
                  string menuMain = Console.ReadLine();

                  if (menuMain == "1")
                  {
                      Console.WriteLine("Logout. Kembali ke halaman Registrasi/Login");
                      activeTrigger(Trigger.Batal);
                  }
              }
          }*/
}

