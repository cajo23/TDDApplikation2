// See https://aka.ms/new-console-template for more information
//IDatabase Interface för databasoperationer 
public interface IDatabase
{
    //Metoder
    void AddUser(User user);
    void RemoveUser(int userId);
    User GetUser(int userId);
}
