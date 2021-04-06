using DemoSprintXamarin.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSprintXamarin.Services
{
    class UserService
    {
        FirebaseClient client;

        public UserService()
        {
            client = new FirebaseClient("https://demosprintxamarin-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> IsUserExists(string name)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>())
                .Where(u => u.Object.Username == name)
                .FirstOrDefault();

            return (user != null);

        }

        public async Task<bool> ResgisterUser(string name, string passwd)
        {
            if(await IsUserExists(name) == false)
            {
                await client.Child("Users")
                .PostAsync(new User()
                {
                    Username = name,
                    Password = passwd
                });

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginUser(string name, string passwd)
        {
            var user = (await client.Child("Users")
                        .OnceAsync<User>())
                        .Where(u => u.Object.Username == name)
                        .Where(u => u.Object.Password == passwd)
                        .FirstOrDefault();

            return (user != null);
        }

    }
}
