// importerer system lib for noen c# standardfunksjoner
using System;
// importerer Generic fra system for å lage lister
using System.Collections.Generic;
using MongoDB.Bson;
// importerer mongoDB driver
using MongoDB.Driver;

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
            bool running = true;
            while (running)
            {
                // Henter svar fra funksjonen hovedmeny
                string awnser = Hovedmeny();

                // Sjekker om svar er 0
                if (awnser == "0")
                {
                    // skifter running variablen til false så loopen stopper og programmet stopper
                    running = false;

                    // sjekker om svaret er 1
                }
                else if (awnser == "1")
                {
                    // Kjører en funksjon som printer ut en liste av alle kundene 
                    ListKunder();

                    // Sjekker om svaret er 2
                }
                else if (awnser == "2")
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
            if (awnser == "0")
            {
                return;

                // Hvis svar er 1 kjører den funksjonen Registrerkunde
            }
            else if (awnser == "1")
            {
                RegistrerKunde();
                // Hvis svar er 2 kjører den funksjonen SlettKunde
            }
            else if (awnser == "2")
            {
                SlettKunde();
                // hvis svaret som er gitt ikke er 0,1 eller 2 så returnerer den og går går tilbake til hovedmeny
            }
            else
            {
                return;
            }
        }

        // Lager funksjon SlettKunde();
        static void SlettKunde()
        {
            var client = Get_Database();
            var db = client.GetDatabase("db_bondz_ansatt");
            var col = db.GetCollection<BsonDocument>("kunderegister");
            var ansatt = col.Find(new BsonDocument()).ToList();
            // Sletter all text i konsollen
            Console.Clear();
            // spør om kundenummer
            Console.WriteLine("Skriv inn kundenummer: ");
            // Henter et svar
            string awnser = Console.ReadLine();
            // for hver Liste med kundeinfo i Kunderegister listen
            foreach (BsonDocument kunde in ansatt)
            {
                // hvis kunden har samme kundenummer som svaret brukeren ga sletter den kunden fra kunderegister og stopper for loopen
                if (kunde[0] == awnser)
                {
                    ansatt.Remove(kunde);
                    return;
                    // Hvis den ikke finner en match går den bare videre og printer At den ikke kunne finne kunden
                }
                else
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
            var client = Get_Database();
            var db = client.GetDatabase("db_bondz_ansatt");
            var col = db.GetCollection<BsonDocument>("kunderegister");
            // lager en liste med info for kunde
            Dictionary<string, string> kunde = new Dictionary<string, string>();
            // Spørr om Fornavn, Etternavn, Adresse, Postnummer, Poststed, Telefonnummer
            Console.Clear();
            kundenummer++;
            string kundenr = kundenummer.ToString();
            Console.Clear();
            Console.WriteLine("Fornavn: ");
            string fornavn = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Etternavn: ");
            string etternavn = Console.ReadLine(); ;
            Console.Clear();
            Console.WriteLine("Adresse: ");
            string adresse = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Postnummer: ");
            string postnr = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Poststed: ");
            string poststed = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Telefonnummer: ");
            string tlf = Console.ReadLine();
            Console.Clear();
            // sjekker om kundenavn har innhold og hvis den ikke har det spør den om kundeinfoen igjen og returerer for å avslutte funksjonen så den ikke fortsetter etterpå igjen
            if (fornavn == null)
            {
                RegistrerKunde();
                return;

                // hvis den har innhold så kjører den videre
            }
            else
            {
                var document = new BsonDocument
                {
                    { "kundenr", kundenr },
                    { "fornavn", fornavn },
                    { "etternavn", etternavn },
                    { "adresse", adresse },
                    { "postnummer", postnr },
                    { "poststed", poststed },
                    { "telefonnummer", tlf }

                };
                col.InsertOne(document);
            }
            // legger til kunde listen i Kunderegisteret og printer at kunde med kundenummer er registrert og venter til brukeren trykker neter til den går videre
            Console.WriteLine($"Kunde er registrert.\nTrykk Enter...");
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
            var client = Get_Database();
            var db = client.GetDatabase("db_bondz_ansatt");
            var col = db.GetCollection<BsonDocument>("kunderegister");
            var ansatte = col.Find(new BsonDocument()).ToList();
            // sletter text i konsoll
            Console.Clear();
            // for hver liste"kunde" i kunderegister så printer den all infoen om den kunden
            foreach (BsonDocument doc in ansatte)
            {
                Console.WriteLine(doc.ToString());
            }
            // venter på at brukeren skal trykke enter før programmet går videre.
            Console.WriteLine("\nTrykk Enter...");
            Console.ReadLine();
        }

        static MongoClient Get_Database()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://bondz:123QWEr@cluster0.uterx.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var client = new MongoClient(settings);

            return client;
        }
    }
}