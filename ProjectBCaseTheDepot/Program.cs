using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ProjectBCaseTheDepot
{
    class Program
    {
        static void Main()
        {
            int Pagina = 0;
            while (Pagina != -1)
            {
                switch (Pagina)
                {
                    case 0:
                        Pagina = StartMenuDepot();
                        break;
                    case 1:
                        Pagina = BezoekerMenu();
                        break;
                    case 2:
                        Pagina = GidsMenu();
                        break;
                }
            }
        }

        private static int StartMenuDepot()
        {
            while (true)
            {
                LeegPagina();
                Console.WriteLine("Het Depot Boijmans Van Beuningen\n\n[1] Bezoeker\n[2] Gids");

                string invoergebruiker = Console.ReadLine();
                switch (invoergebruiker)
                {
                    case "1":
                        return 1;
                    case "2":
                        return 2;
                    default:
                        Console.WriteLine("Invoer onjuist, selecteer een van bovenstaande opties a.u.b.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static int BezoekerMenu()
        {
            while (true)
            {
                LeegPagina();
                Console.WriteLine("Bezoeker\n\n[1] om een reservering te maken \n[0] om terug te gaan ");

                string invoerbezoeker = Console.ReadLine();
                switch (invoerbezoeker)
                {
                    case "0":
                        return 0;

                    case "1":
                        LeegPagina();
                        var dateNu = DateTime.Now;
                        Console.WriteLine("Huidige datum: " + dateNu + "\n");

                        var bezoekerslimit11uur = 13;
                        var bezoekerslimit12uur = 13;
                        var bezoekerslimit13uur = 13;
                        var bezoekerslimit14uur = 13;
                        var bezoekerslimit15uur = 13;
                        var bezoekerslimit16uur = 13;
                        var bezoekerslimit17uur = 13;

                        var Vandaag = DateTime.Today;
                        var reservering11uur = Vandaag.AddHours(11);
                        var reservering12uur = Vandaag.AddHours(12);
                        var reservering13uur = Vandaag.AddHours(13);
                        var reservering14uur = Vandaag.AddHours(14);
                        var reservering15uur = Vandaag.AddHours(15);
                        var reservering16uur = Vandaag.AddHours(16);
                        var reservering17uur = Vandaag.AddHours(17);


                        var huidigelijst = File.ReadAllText(@"reservering.Json");
                        var Reserveringenvoorcount = JsonConvert.DeserializeObject<List<Reservering>>(huidigelijst);

                        
                        foreach (var Reservering in Reserveringenvoorcount)
                        {
                            if (Reservering.tijd == reservering11uur)
                            {
                                --bezoekerslimit11uur;
                            }
                            else if (Reservering.tijd == reservering12uur)
                            {
                                --bezoekerslimit12uur;
                            }
                            else if (Reservering.tijd == reservering13uur)
                            {
                                --bezoekerslimit13uur;
                            }
                            else if (Reservering.tijd == reservering14uur)
                            {
                                --bezoekerslimit14uur;
                            }
                            else if (Reservering.tijd == reservering15uur)
                            {
                                --bezoekerslimit15uur;
                            }
                            else if (Reservering.tijd == reservering16uur)
                            {
                                --bezoekerslimit16uur;
                            }
                            else if (Reservering.tijd == reservering17uur)
                            {
                                --bezoekerslimit17uur;
                            }
                        }
                        
                        Console.WriteLine("[1] Rondleiding van 11:00" + " || plekken vrij: " + bezoekerslimit11uur);
                        Console.WriteLine("[2] Rondleiding van 12:00" + " || plekken vrij: " + bezoekerslimit12uur);
                        Console.WriteLine("[3] Rondleiding van 13:00" + " || plekken vrij: " + bezoekerslimit13uur);
                        Console.WriteLine("[4] Rondleiding van 14:00" + " || plekken vrij: " + bezoekerslimit14uur);
                        Console.WriteLine("[5] Rondleiding van 15:00" + " || plekken vrij: " + bezoekerslimit15uur);
                        Console.WriteLine("[6] Rondleiding van 16:00" + " || plekken vrij: " + bezoekerslimit16uur);
                        Console.WriteLine("[7] Rondleiding van 17:00" + " || plekken vrij: " + bezoekerslimit17uur);

                        List<Reservering> LijstVanReserveringen = new List<Reservering>();

                        Console.Write("\n\nSelecteer (getal) de Rondleiding naar keuze: ");
                        int num1 = Convert.ToInt32(Console.ReadLine());

                        LeegPagina();
                        Console.WriteLine("Door u geselecteerd : " + GeselecteerdeTijd(num1));
                        Console.WriteLine("\nVoer uw unieke ticket code in: ");

                        int Acode = Convert.ToInt32(Console.ReadLine());
                        int returnvalue = (CodeControle(Acode));

                        //// json 
                        var LijstReserveringenJson = File.ReadAllText(@"reservering.Json");
                        LijstVanReserveringen = JsonConvert.DeserializeObject<List<Reservering>>(LijstReserveringenJson);

                        if (returnvalue == 0)
                        {
                            Reservering reserveringmuseum = (new Reservering(Acode, GeselecteerdeTijd(num1)));
                            LijstVanReserveringen.Add(reserveringmuseum);

                            string NieuweLijstVanReserveringen = JsonConvert.SerializeObject(LijstVanReserveringen);
                            File.WriteAllText(@"reservering.Json", NieuweLijstVanReserveringen);
                            LeegPagina();

                            Console.WriteLine("Opgeslagen, toets [Enter] om terug te gaan.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Code onjuist, toets [Enter] om terug te gaan !");
                            Console.ReadLine();
                        }
                        break;

                    default:
                        Console.WriteLine("Invoer onjuist, selecteer een van bovenstaande opties a.u.b.");
                        Console.ReadLine();
                        break;

                }
            }
        }

        private static int GidsMenu()                                           /// Gidsmenu 
        {
            while (true)
            {
                LeegPagina(); // functie rondleiding starten als bepaalde tijd
                Console.WriteLine("Gids\n\n[0] om terug te gaan naar het start menu\n[1] om een rondleiding te starten \n[10] om een overzicht te zien van de reserveringen");
                string invoergids = Console.ReadLine();
                switch (invoergids)
                {
                    case "1":
                        LeegPagina();
                        codecontrole2(11);
                        break;

                    case "2":
                        LeegPagina();
                        codecontrole2(12);
                        break;

                    case "3":
                        LeegPagina();
                        codecontrole2(13);
                        break;

                    case "4":
                        LeegPagina();
                        codecontrole2(14);
                        break;

                    case "5":
                        LeegPagina();
                        codecontrole2(15);
                        break;

                    case "6":
                        LeegPagina();
                        codecontrole2(16);
                        break;

                    case "7":
                        LeegPagina();
                        codecontrole2(17);
                        break;

                    case "10":
                        LeegPagina();
                        LijstVanAlleReserveringen();

                        break;
                    case "0":
                        return 0;
                    default:
                        Console.WriteLine("Invoer onjuist, selecteer een van bovenstaande opties a.u.b.");
                        Console.ReadLine();
                        break;
                }
            }
        }


        static void LeegPagina()
        {
            Console.Clear();
        }

        static void codecontrole2(int tijd)
        {
            var Vandaag = DateTime.Today;
            var reservering = Vandaag.AddHours(tijd);

            string stringjson1 = File.ReadAllText(@"reservering.Json");
            var LijstVanReserveringen1 = JsonConvert.DeserializeObject<List<Reservering>>(stringjson1);

            Console.WriteLine("reservering:" + reservering);
            Console.WriteLine("vul jouw code in");
            int aCode = Convert.ToInt32(Console.ReadLine());

            int indexcode = LijstVanReserveringen1.FindIndex(Reservering => Reservering.code == aCode);
            int indextijd = LijstVanReserveringen1.FindIndex(Reservering => Reservering.tijd == reservering);

            if (indexcode >= 0)
            {
                Console.WriteLine("controleren...");

                var yeahtijd = LijstVanReserveringen1[indexcode].tijd;
                if (yeahtijd == reservering)
                {
                    Console.WriteLine("Je mag een labjas");
                }
                else
                {
                    Console.WriteLine("Je mag geen labjas, je hebt voor een andere tijd gereserveerd.");
                }
            }
            if (indexcode < 0)
            {
                Console.WriteLine("je mag geen labjas");
            }
            Console.ReadLine();
        }

        static int CodeControle(int code)
        {
            int answer = code % 17;

            if (answer == 0)
            {
                return 0;
            }
            else
                return answer;
        }

        static DateTime GeselecteerdeTijd(int tijdOptie)
        {
            DateTime Tijdvak;

            var Vandaag = DateTime.Today;
            var reservering11uur = Vandaag.AddHours(11);
            var reservering12uur = Vandaag.AddHours(12);
            var reservering13uur = Vandaag.AddHours(13);
            var reservering14uur = Vandaag.AddHours(14);
            var reservering15uur = Vandaag.AddHours(15);
            var reservering16uur = Vandaag.AddHours(16);
            var reservering17uur = Vandaag.AddHours(17);

            switch (tijdOptie)
            {
                case 1:
                    Tijdvak = reservering11uur;
                    break;
                case 2:
                    Tijdvak = reservering12uur;
                    break;
                case 3:
                    Tijdvak = reservering13uur;
                    break;
                case 4:
                    Tijdvak = reservering14uur;
                    break;
                case 5:
                    Tijdvak = reservering15uur;
                    break;
                case 6:
                    Tijdvak = reservering16uur;
                    break;
                case 7:
                    Tijdvak = reservering17uur;
                    break;
                default:
                    Tijdvak = Vandaag;
                    break;
            }
            return Tijdvak;
        }

        public static void LijstVanAlleReserveringen()
        {
            Console.WriteLine("Lijst van alle reserveringen");

            string stringjson2 = File.ReadAllText(@"reservering.Json");
            var LijstVanReserveringen2 = JsonConvert.DeserializeObject<List<Reservering>>(stringjson2);

            foreach (var Reservering in LijstVanReserveringen2)
            {
                Console.WriteLine(Reservering.code);
                Console.WriteLine(Reservering.tijd);
                Console.WriteLine("--------------");
            }

            Console.WriteLine("[0] om terug te gaan");
            Console.ReadLine();
        }

        public static void WeergaveRondleidingen()
        {
   
        }
    }
}


