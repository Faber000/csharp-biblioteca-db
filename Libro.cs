public class Libro : Documento
{
    public int NumPagine { get; set; }

    public Libro(string Titolo, string Autore, int NumPagine) : base(Titolo, Autore)
    {
        this.NumPagine = NumPagine;
    }
}