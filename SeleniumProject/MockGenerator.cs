using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace RandomUserConsoleApp
{
    public class Program
    {
        private const string BaseUrl = "https://randomuser.me/api/";

        public static UserData GetRandomUserDataFromApi()
        {
            using (var client = new HttpClient())
            {
                var userEndpoint = $"{BaseUrl}";

                var response = client.GetFromJsonAsync<UserApiResponse>(userEndpoint).Result;

                if (response != null)
                {
                    var user = response.Results[0];

                    return new UserData
                    {
                        Name = $"{user.Name.First} {user.Name.Last}",
                        City = user.Location.City
                    };
                }
                return null;
            }
        }
    }

    public class UserApiResponse
    {
        public User[] Results { get; set; }
    }

    public class User
    {
        public UserName Name { get; set; }
        public UserLocation Location { get; set; }
    }

    public class UserName
    {
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class UserLocation
    {
        public string City { get; set; }
    }

    public class UserData
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}
