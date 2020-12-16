using IdentityModel.Client;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleView
{
    public class OAuth
    {
        public static async Task<string> GetPasswordToken(string userName, string password)
        {
            HttpClient httpClient = new HttpClient();

            var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:10001");

            var response = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "ConsoleClient",
                ClientSecret = "ConsoleClientSecret",
                Scope = "WebAPI",
                UserName = userName,
                Password = password
            });

            if (response.IsError)
                Console.WriteLine($"error: {response.Error}\nerrorDescription: {response.ErrorDescription}");

            return response.AccessToken;
        }

        public static async Task<string> GetCodeToken()
        {
            HttpClient httpClient = new HttpClient();

            var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:10001");

            var codeUri = "https://localhost:10001/connect/authorize?client_id=ConsoleClient&response_type=code&redirect_uri=https://localhost:5001/authentication/login-callback&code_challenge=5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5&code_challenge_method=S256&scope=WebAPI";
            const string browserPath = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe";
            Process.Start(browserPath, codeUri);

            //var response = await httpClient.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest()
            //{
            //    Address = discoveryDocument.TokenEndpoint,
            //    ClientId = "ConsoleClient",
            //    ClientSecret = "ConsoleClientSecret",
            //    RedirectUri = "https://localhost:8001",
            //    //Code = code
            //});

            //if (response.IsError)
            //    Console.WriteLine($"error: {response.Error}\nerrorDescription: {response.ErrorDescription}");

            //return response.AccessToken;
            return "";
        }
    }
}
