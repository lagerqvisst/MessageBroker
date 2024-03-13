//TEST LAYER FOR API CALLS / DB SAVES ETC.

using BusinessLayer;
using DataLayer;
using Models;

Create create = new Create();
Context context = new Context();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

User user = await Create.GenerateRandomUser();
context.Users.Add(user);
context.SaveChanges();

Console.WriteLine($"Förnamn: {user.FirstName}, Efternamn {user.LastName} ");