

using DigichList.Core.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Digichlist.Tests.TestData
{
    public class UsersTestData
    {
        public static List<User> GetUsersTestData() =>
            new List<User>{
                new User
                {
                    Id = 1, 
                    TelegramId = 55555, 
                    FirstName = "John",
                    LastName = "williams", 
                    IsRegistered = true
                },
                new User
                {
                    Id = 2, 
                    TelegramId = 44444, 
                    FirstName = "Matthew",
                    LastName = "Paul", 
                    IsRegistered = true
                },
                new User
                {
                    Id = 3,
                    TelegramId = 33333,
                    FirstName = "Drake",
                    LastName = "Parker",
                    IsRegistered = false
                },
            };
    }
}
