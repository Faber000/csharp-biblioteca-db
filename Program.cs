using System;
using System.Data.SqlClient;

string stringaDiConnessione = "Data Source=localhost;Initial Catalog=db-biblioteca;Integrated Security=True";
SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione);

try
{
    connessioneSql.Open();
} catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
} finally
{
    connessioneSql.Close();
}

// lista di oggetti Documento
List<Documento> ListaDocumenti = new List<Documento>();

// lista di oggetti Prestito
List<Prestito> ListaPrestito = new List<Prestito>();

// lista di oggetti Utente
List<Utente> ListaUtente = new List<Utente>();

// inizializzo successo del prestito
bool success = false;

// opzione scelta dal menu
string option;

do
{
    Console.WriteLine("Inserire 1 per aggiungere un documento");
    Console.WriteLine("Inserire 2 per noleggiare un documento");
    Console.WriteLine("Inserire 3 per cercare un prestito");
    Console.WriteLine("Inserire 4 cercare un documento");
    Console.WriteLine("Inserire 5 riconsegnare un documento");

    Console.WriteLine("-1 per terminare");

    option = Console.ReadLine();

    switch (option)
    {
        case "1":
            InserisciDocumento();
            break;
        case "2":
            EffettuaPrestito();
            break;
        case "3":
            RicercaPrestito();
            break;
        case "4":
            RicercaDocumento();
            break;
        case "5":
            RiconsegnaDocumento();
            break;
        default:
            break;
    }

} while (option != "-1");

// funzione per riconsegnare un documento
void RiconsegnaDocumento()
{
    int index = -1;
    int cont = 0;

    Console.WriteLine("inserisci il tuo cognome");
    string cognome = Console.ReadLine();

    Console.WriteLine("Hai in prestito i seguenti documenti: ");

    // cerco una corrispondenza con il cognome 
    foreach (Prestito prestito in ListaPrestito)
    {
        if (prestito.User.Cognome == cognome)
        {
            Console.WriteLine("\r\n");
            Console.WriteLine(prestito.Document.Titolo + " noleggiato il " + prestito.InizioPrestito);
            Console.WriteLine("\r\n");
        }
    }

    Console.WriteLine("Quale documento vuoi restituire?");
    string titolo = Console.ReadLine();

    // cerco una corrispondenza con il titolo
    foreach (Prestito prestito in ListaPrestito)
    {
        if (prestito.Document.Titolo == titolo)
        {
            // ricavo l'indice dell'elemento da rimuovere
            index = cont;
        }
        cont++;
    }
    if (index != -1)
    {
        // rimuovo l'elemento a indice index
        ListaPrestito.RemoveAt(index);
        Console.WriteLine("\r\n");
        Console.WriteLine("Restituzione avvenuta con successo");
        Console.WriteLine("\r\n");

    }
    else
    {
        Console.WriteLine("\r\n");
        Console.WriteLine("documento non trovato");
        Console.WriteLine("\r\n");
    }
}

// funzione per cercare un documento per nome/codice
void RicercaDocumento()
{
    // titolo documento
    string titolo;

    // id documento
    int id;

    // opzione di ricerca
    string option;

    // prestito trovato
    bool found = false;

    Console.WriteLine("Cercare tramite titolo o Id? T/I");

    option = Console.ReadLine();

    switch (option)
    {
        // ricerca tramite titolo
        case "T":

            Console.WriteLine("Quale documento vuoi ricercare?");

            titolo = Console.ReadLine();

            // cerco una corrispondenza del titolo
            foreach (Documento documento in ListaDocumenti)
            {
                if (documento.Titolo == titolo)
                {
                    Console.WriteLine("\r\n");
                    Console.WriteLine("Il documento " + documento.Titolo + " è stato scritto da " + documento.Autore);

                    foreach (Prestito prestito in ListaPrestito)
                    {
                        if (prestito.Document.Titolo == titolo)
                        {
                            found = true;
                            Console.WriteLine("\r\n");
                            Console.WriteLine("Il documento " + titolo + " è attalmente noleggiato da " + prestito.User.Nome + " " + prestito.User.Cognome);
                            Console.WriteLine("\r\n");
                        }
                    }
                    if (found == false)
                    {
                        Console.WriteLine("\r\n");
                        Console.WriteLine("Il documento è disponibile");
                        Console.WriteLine("\r\n");
                    }
                }
            }
            break;

        // ricerca tramite Id
        case "I":
            Console.WriteLine("Quale documento vuoi ricercare?");

            id = Convert.ToInt32(Console.ReadLine());

            // cerco una corrispondenza del codice
            foreach (Documento documento in ListaDocumenti)
            {
                if (documento.Id == id)
                {
                    Console.WriteLine("\r\n");
                    Console.WriteLine("il documento " + documento.Titolo + " è stato scritto da " + documento.Autore);

                    foreach (Prestito prestito in ListaPrestito)
                    {
                        if (prestito.Document.Id == id)
                        {
                            found = true;
                            Console.WriteLine("\r\n");
                            Console.WriteLine("Il documento " + prestito.Document.Titolo + " è attalmente noleggiato da " + prestito.User.Nome + " " + prestito.User.Cognome);
                            Console.WriteLine("\r\n");
                        }
                    }
                    if (found == false)
                    {
                        Console.WriteLine("\r\n");
                        Console.WriteLine("Il documento è disponibile");
                        Console.WriteLine("\r\n");
                    }
                }
            }
            break;

        default:
            Console.WriteLine("Inserisci un' opzione esistente");
            RicercaDocumento();
            break;
    }
}

// funzione per cercare un prestito
void RicercaPrestito()
{
    // prestito trovato
    bool found = false;

    Console.WriteLine("inserisci il cognome dell'utente");

    string cognome = Console.ReadLine();

    // cerco una corrispondenza con il cognome
    foreach (Prestito prestito in ListaPrestito)
    {
        if (prestito.User.Cognome == cognome)
        {
            found = true;
            Console.WriteLine("\r\n");
            Console.WriteLine("informazioni prestito ");
            Console.WriteLine("documento in prestito: " + prestito.Document.Titolo);
            Console.WriteLine("noleggiato il: " + prestito.InizioPrestito);
            Console.WriteLine("\r\n");

        }
    }

    if (found == false)
    {
        Console.WriteLine("Nessun prestito trovato");
    }

}

// funzione per inserire un documento
void InserisciDocumento()
{
    Console.WriteLine("Salve, per effettuare l'inserimento di un documento inserisca le seguenti informazioni");

    Console.WriteLine("Libro o Dvd? L/D");
    string option = Console.ReadLine();

    Console.WriteLine("inserisci il titolo");
    string titolo = Console.ReadLine();

    Console.WriteLine("inserisci l'autore");
    string autore = Console.ReadLine();

    switch (option)
    {
        // inserimento libro
        case "L":
            Console.WriteLine("inserisci il numero di pagine");
            int numPagine = Convert.ToInt32(Console.ReadLine());
            Libro libro = new Libro(titolo, autore, numPagine);
            ListaDocumenti.Add(libro);
            break;

        // inserimento Dvd
        case "D":
            Console.WriteLine("inserisci la durata");
            int durata = Convert.ToInt32(Console.ReadLine());
            Dvd dvd = new Dvd(titolo, autore, durata);
            ListaDocumenti.Add(dvd);
            break;

        default:
            Console.WriteLine("Inserisci un'opzione esistente");
            InserisciDocumento();
            break;
    }
}

// funzione per effettura il prestito di un documento
void EffettuaPrestito()
{
    bool success = false;

    Console.WriteLine("Salve, per effettuare il prestito inserisca le seguenti informazioni:");

    Console.WriteLine("inserisci il tuo nome");
    string nome = Console.ReadLine();

    Console.WriteLine("inserisci il tuo cognome");
    string cognome = Console.ReadLine();

    Console.WriteLine("inserisci la tua email");
    string email = Console.ReadLine();

    Console.WriteLine("inserisci la tua password");
    string password = Console.ReadLine();

    Console.WriteLine("inserisci un recapito telefonico");
    string phone = Console.ReadLine();

    Utente utente = new Utente(nome, cognome, email, password, phone);

    ListaUtente.Add(utente);

    Console.WriteLine("che documento vorrebbe leggere? (codice o titolo)");

    string titolo = Console.ReadLine();

    // cerco una corrispondenza con il titolo
    foreach (Documento documento in ListaDocumenti)
    {
        if (documento.Titolo == titolo)
        {
            if (documento.IsRented != true)
            {
                documento.IsRented = true;

                success = true;

                DateTime thisDay = DateTime.Today;

                Prestito prestito = new Prestito(thisDay.ToString("d"), "30/09/2022", utente, documento);

                ListaPrestito.Add(prestito);

                Console.WriteLine("\r\n");
                Console.WriteLine("Il signor " + prestito.User.Cognome + " ha effettuato il noleggio del documento " + prestito.Document.Titolo);
                Console.WriteLine("\r\n");
            }

            else
            {
                Console.WriteLine("\r\n");
                Console.WriteLine("il documento è già in prestito");
                Console.WriteLine("\r\n");
            }
        }
    }

    if (success == false)
    {
        Console.WriteLine("\r\n");
        Console.WriteLine("il documento non è presente in biblioteca");
        Console.WriteLine("\r\n");
    }
}
