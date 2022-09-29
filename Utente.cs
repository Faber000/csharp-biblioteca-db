public class Utente
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }

    public Utente(string nome, string cognome, string email, string password, string phone)
    {
        Nome = nome;
        Cognome = cognome;
        Email = email;
        Password = password;
        Phone = phone;
    }
}
