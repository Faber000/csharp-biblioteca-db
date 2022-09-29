public class Dvd : Documento
{
    public int Durata { get; set; }

    public Dvd(string Titolo, string Autore, int Durata) : base(Titolo, Autore)
    {
        this.Durata = Durata;
    }
}