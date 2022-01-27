// importerer system lib for noen c# standardfunksjoner
using System;
// importerer Generic fra system for å lage lister
using System.Collections.Generic;

// Namespace er bjarnekunderegister
namespace Bjarnekunderegister
{
    // Lager klasse
    class Program
    {
        // Lager liste av lister for å lagre alle kundene
        // Lager en variabel som holder telling på hvilket kundenummer de nye kundene skall ha
        static int kundenummer = 0;

        // lager Hovedfunksjonen
        static void Main(string[] args)
        {
            // Lager en variebel som bestemmer om while loopen skal kjøre eller stoppe ut fra valgene i Hovedmeny
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://bondz:123QWEr@cluster0.uterx.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            bool running = true;
            while (running)
            {
                // Henter svar fra funksjonen hovedmeny
                string awnser = Hovedmeny();

                // Sjekker om svar er 0
                if(awnser == "0")
                {
                    // skifter running variablen til false så loopen stopper og programmet stopper
                    running = false;

                // sjekker om svaret er 1
                } else if(awnser == "1")
                {
                    // Kjører en funksjon som printer ut en liste av alle kundene 
                    ListKunder();

                // Sjekker om svaret er 2
                } else if(awnser == "2")
                {
                    // Kjører en funskjon som åpner en administrasjonsmeny
                    Administrer();

                // Hvis det blir skrevet noe annet enn 0,1 og 2 kjører loopen videre og spør på nytt om svar fra hovedmeny
                }
                else
                {
                    // Ikke gjør noe bare fortsett
                    continue;
                }
            }
        }

        // Lager funskjon Hovedmeny for å spørre om hva brukeren vil gjøre
        static string Hovedmeny()
        {
            // Rydder all text fra konsollen
            Console.Clear();
            // Printer en velkommen melding og lager en linje mellom svaralternativene og velkommen for at det skal se bedre ut
            Console.WriteLine("Velkommen til CRM for Bjarnes Fiskefarse AS\n");
            // Svaralt for Vis alle kunder
            Console.WriteLine("1: Vis alle kunder");
            // svaralt for administrer en kunde
            Console.WriteLine("2: Administrer en kunde");
            // svaralt for å avslutte programmet
            Console.WriteLine("0: Avslutt");

            // returnerer svaret til Mainfunksjonen
            return Console.ReadLine();
        }

        // lager funksjon for å administrere kunde
        static void Administrer()
        {
            // Henter svar fra adminmeny funksjon
            string awnser = Adminmeny();

            // hvis svar er 0 så kjører den return, siden funksjonen er en void skal den ikke returnere noe men den stopper funskjonen og går tilbake til Hovedmenyen.
            if(awnser == "0")
            {
                return;

            // Hvis svar er 1 kjører den funksjonen Registrerkunde
            } else if(awnser == "1")
            {
                RegistrerKunde();
            // Hvis svar er 2 kjører den funksjonen SlettKunde
            } else if(awnser == "2")
            {
                SlettKunde();
            // hvis svaret som er gitt ikke er 0,1 eller 2 så returnerer den og går går tilbake til hovedmeny
            } else
            {
                return;
            }
        }

        // Lager funksjon SlettKunde();
        static void SlettKunde()
        {
            // Sletter all text i konsollen
            Console.Clear();
            // spør om kundenummer
            Console.WriteLine("Skriv inn kundenummer: ");
            // Henter et svar
            string awnser = Console.ReadLine();
            // for hver Liste med kundeinfo i Kunderegister listen
            foreach(List<string> kunde in Kunderegister)
            {
                // hvis kunden har samme kundenummer som svaret brukeren ga sletter den kunden fra kunderegister og stopper for loopen
                if (kunde[0] == awnser)
                {
                    Kunderegister.Remove(kunde);
                    return;
                // Hvis den ikke finner en match går den bare videre og printer At den ikke kunne finne kunden
                } else
                {
                    continue;
                }
            }
            Console.WriteLine("Kunne ikke finne Kunden i kunderegisteret.\nTrykk Enter...");
            Console.ReadLine();
        }

        // lager Registrer kunde funksjon
        static void RegistrerKunde()
        {
            // lager en liste med info for kunde
            List<string> kunde = new List<string>();
            // Spørr om Fornavn, Etternavn, Adresse, Postnummer, Poststed, Telefonnummer
            Console.Clear();
            kundenummer++;
            kunde.Add((kundenummer).ToString());
            Console.Clear();
            Console.WriteLine("Fornavn: ");
            kunde.Add(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Etternavn: ");
            kunde.Add(Console.ReadLine()); ;
            Console.Clear();
            Console.WriteLine("Adresse: ");
            kunde.Add(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Postnummer: ");
            kunde.Add(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Poststed: ");
            kunde.Add(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Telefonnummer: ");
            kunde.Add(Console.ReadLine());
            Console.Clear();
            // sjekker om kundenavn har innhold og hvis den ikke har det spør den om kundeinfoen igjen og returerer for å avslutte funksjonen så den ikke fortsetter etterpå igjen
            if(kunde[1] == null)
            {
                RegistrerKunde();
                return;

            // hvis den har innhold så kjører den videre
            } else
            {
                
            }
            // legger til kunde listen i Kunderegisteret og printer at kunde med kundenummer er registrert og venter til brukeren trykker neter til den går videre
            Kunderegister.Add(kunde);
            Console.WriteLine($"Kunde med kundenummer: {kunde[0]} er registrert.\nTrykk Enter...");
            Console.ReadLine();
        }


        // lager funksjon for adminmeny
        static string Adminmeny()
        {
            // Sletter text i konsoll
            Console.Clear();
            // Svaralt 1 for registrering av ny kunde
            Console.WriteLine("1: Registrer ny kunde");
            // svaralt 2 for sletting av kunde
            Console.WriteLine("2: Slett kunde");
            // svaralt 0 For å gå tilbake til hovedmeny
            Console.WriteLine("0: Tilbake til hovedmeny");

            // returnerer svar
            return Console.ReadLine();
        }

        // Lager funksjon for listing av kunder
        static void ListKunder()
        {
            // sletter text i konsoll
            Console.Clear();
            // for hver liste"kunde" i kunderegister så printer den all infoen om den kunden
            foreach(List<string> kunde in Kunderegister)
            {
                Console.WriteLine($"Kundenummer: {kunde[0]}, Fornavn: {kunde[1]}, Etternavn: {kunde[2]}, Adresse: {kunde[3]}, Postnummer: {kunde[4]}, Poststed: {kunde[5]}, Telefonnummer: {kunde[6]}.");
            }
            // venter på at brukeren skal trykke enter før programmet går videre.
            Console.WriteLine("\nTrykk Enter...");
            Console.ReadLine();
        }
    }
}
