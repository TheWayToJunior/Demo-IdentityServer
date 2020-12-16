using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace DemoIdentityServer.Server
{
    internal class IdentityServerConfiguration
    {
        internal static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "BlazorClient",
                    AllowedGrantTypes = GrantTypes.Code, // CodeAndClientCredentials
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "WebAPI"

                    },
                    RedirectUris = { "https://localhost:5001/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:5001/authentication/logout-callback" },
                    AllowedCorsOrigins = { "https://localhost:5001" }
                },

                new Client
                {
                    ClientName = "Web Client",
                    AlwaysSendClientClaims = true,
                    ClientId = "WebClient",
                    ClientSecrets = { new Secret("TopSecretClientSecret".Sha256()) },
                    RequireClientSecret = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        "WebAPI",
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = { "https://localhost:8001" },
                },

                new Client
                {
                    ClientName = "Console Client",
                    AlwaysSendClientClaims = true,
                    ClientId = "ConsoleClient",
                    ClientSecrets = { new Secret("ConsoleClientSecret".Sha256()) },
                    RequireClientSecret = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // ResourceOwnerPassword Code
                    AllowedScopes = { "WebAPI" },
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    //RedirectUris = { "https://localhost:5001/authentication/login-callback" },
                }
            };

        internal static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "WebAPI",
                    DisplayName = "Web API",
                    ApiSecrets =
                    {
                        new Secret("TopSecret".Sha256()),
                    },
                    Scopes = { "WebAPI" }
                },
            };

        internal static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Profile(),
            };

        internal static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("WebAPI"),
            };
    }
}