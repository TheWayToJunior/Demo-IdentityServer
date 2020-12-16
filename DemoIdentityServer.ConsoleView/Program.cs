using IdentityModel.Client;
using System;
using System.Net.Http;

namespace ConsoleView
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter UserName: ");
            var userName = Console.ReadLine();

            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            string token = OAuth.GetPasswordToken(userName, password)
                .Result;

           // string token = OAuth.GetCodeToken().Result;

            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(token);

            var response = httpClient.GetAsync("https://localhost:8001/WeatherForecast/Get")
                .Result;

            if (!response.IsSuccessStatusCode)
                Console.WriteLine($"StatusCode: {response.StatusCode}");
            else
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

        }
    }
}
