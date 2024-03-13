using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Models;
using DataLayer;

namespace BusinessLayer
{
    public class Create
    {

        public static async Task<User> GenerateRandomUser()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync("https://randomuser.me/api/");
                    response.EnsureSuccessStatusCode(); // kastar ett undantag om felkod returneras

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(jsonString);

                    // Hämta förnamn och efternamn från JSON-svaret
                    var firstName = jsonObject["results"][0]["name"]["first"].ToString();
                    var lastName = jsonObject["results"][0]["name"]["last"].ToString();

                    User user = new User(firstName, lastName); 


                    return user;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Det uppstod en HTTP-fel: {e.Message}");
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ett fel uppstod: {e.Message}"); 
                    return null;
                }
            }

            
        }

        public static void SaveNewUserToDb(User user)
        {
            using (var context = new Context())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
