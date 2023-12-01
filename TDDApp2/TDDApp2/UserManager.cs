// See https://aka.ms/new-console-template for more information
// UserManager klass  som använder IDatabase-beroende
public class UserManager
{
    // private readonly, private gör den endast är tillgänglig i UserManager-klassen
    // readonly, för att skrivskydda så värdet ej ändras
    // lagrar referensen till IDatabase-objektet
    private readonly IDatabase _database;

    // Konstruktor för UserManager tar IDatabase-objektet som parameter
    public UserManager(IDatabase database)
    {
        _database = database; //Tilldelar mottaget IDatabase-objektet till den privata varibalen _database
    }

    public void AddUser(User user)
    {
        _database.AddUser(user);//Lägga till användare med IDatabase-objektet med hjälp av user parametern
    }

    public void RemoveUser(int userId)
    {
        _database.RemoveUser(userId); //Ta bort användare med IDatabase-objekt med hjälp av userID parametern
    }

    public User GetUser(int userId)
    {
        return _database.GetUser(userId); //Använder IDatabase-objektet för att hämta en användare,
                                          //baserat på den mottagna userID-parametern och returnerar användaren
    }
}