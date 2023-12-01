using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
//importerat Moq för att skapa mock-objekt
//Importerat NUnit.Framework för att kunna testa

namespace Användarkonton.Test
{
    [TestFixture] //Markerar klassen innehåller testmetoder
    public class UserManagerTest
    {
        [Test] //Markerar en testmetod

        //Test för AddUser
        public void AddUser_ValidUser_CallsAddUserInDatabase()
        {
            //Arrange - förbereder testet
            var mockDatabase = new Mock<IDatabase>(); //Skapar (Simulerad) Mockat-objekt av IDatabase
            var userManager = new UserManager(mockDatabase.Object); //Skapar en instans av UserManager för att testa interaktion med simulering av IDatabase.
                                                                    //Använder mockat-objekt för att isolera testet från verklig databasåtkomst.
            var user = new User { UserId = 1, UserName = "Alicia" }; //Skapar användaren

            //Act - Utför testet
            userManager.AddUser(user); //Anropar userManager-metoden för att lägga till användaren

            //Assert - Validerar testresultatet
            mockDatabase.Verify(a => a.AddUser(user), Times.Once); //Kontrollerar att IDatabase-metoden AddUser anropas en gång.
                                                                   //mockDatabase.Verify() är metod för att verifiera att viss metod i det mockade-objeketet anropats.
                                                                   //(a => a.AddUser(user)) specificerar vilken metod som förväntas anropas från mmockat objekt dvs AddUser.
                                                                   //Times.Once för att den ska anropas en gång
        }

        [Test]

        //Testar att RemoveUser anropas med rätt Användar-Id
        public void RemoveUser_ExistingUserId_CallsDatabaseRemoveUser()
        {
            //Arrange
            var mockDatabase = new Mock<IDatabase>();
            var userManager = new UserManager(mockDatabase.Object);
            int userId = 1; //Definierar användar - Id

            //Act
            userManager.RemoveUser(userId);

            //Assert
            mockDatabase.Verify(a => a.RemoveUser(userId), Times.Once); //Verifierar att IDatabase - metoden RemoveUser anropas en gång med userId
        }

        [Test]

        //Test för GetUser när användare finns i databasen
        public void GetUser_ExistingUserId_ReturnExpectedUser()
        {
            //Arrange
            var mockDatabase = new Mock<IDatabase>();
            var userManager = new UserManager(mockDatabase.Object);
            int userId = 1; //Definierar användar - Id
            var expectedUser = new User { UserId = userId, UserName = "Alicia" }; //Skapar användare

            mockDatabase.Setup(a => a.GetUser(userId)).Returns(expectedUser); //Setup eftersom jag förväntar mig en return
                                                                              //Simulerar beteendet av IDatabase-objektet.
                                                                              //Säkerställer att den returnerar användaren
                                                                              //När GetUser anropas med userId ska mocken returnera expectedUser

            //Act
            var retrievedUser = userManager.GetUser(userId); //Anropar UserManager - metoden för att hämta användaren baserat på Id

            //Assert
            mockDatabase.Verify(a => a.GetUser(userId), Times.Once); //Verifierar om metoden GetUser anropas en gång med userId
            Assert.AreEqual(expectedUser, retrievedUser); //Jämför förväntad expectedUser med den hämtade användaren
                                                          //retrivedUser för att säkerställa att de blir lika
        }
    }
}