public class Prestito
{
    public string InizioPrestito { get; set; }
    public string FinePrestito { get; set; }
    public Utente User { get; set; }
    public Documento Document { get; set; }

    public Prestito(string inizioPrestito, string finePrestito, Utente user, Documento document)
    {
        InizioPrestito = inizioPrestito;
        FinePrestito = finePrestito;
        User = user;
        Document = document;
    }
}