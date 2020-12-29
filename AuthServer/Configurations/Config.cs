using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AuthServer.Configurations
{
    public static class Config
    {
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "DA0EDD28-83FE-4BB3-8CDA-E098963C9752",
                    Username = "admin",
                    Password = "admin",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "admin"),
                        new Claim(JwtClaimTypes.WebSite, "https://admin.com"),
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentitySources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "ApiOne",
                    DisplayName = "API #1",
                    Description = "Allow the application to access API #1 on your behalf",
                    Scopes = new List<string> {"api1.read", "api1.write"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())}
                },
                new ApiResource
                {
                    Name = "ApiTwo",
                    DisplayName = "API #2",
                    Description = "Allow the application to access API #2 on your behalf",
                    Scopes = new List<string> {"api2.read", "api2.write"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new()
                {
                    ClientId = "ApiOne",
                    ClientName = "API #1",
                    ClientSecrets = new List<Secret> {new Secret("APISecretKey".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new()
                {
                    ClientId = "ApiTwo",
                    ClientName = "API #2",
                    ClientSecrets = new List<Secret> {new Secret("APISecretKey".Sha256())},
                    RedirectUris = {"http://localhost:5002/signin-oidc"},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1.read",
                        "api2.read",
                        "api2.write"
                    }
                },
                new()
                {
                    ClientId = "WebClient",
                    ClientName = "Web Client",
                    ClientSecrets = new List<Secret> {new Secret("WebSecretKey".Sha256())},
                    
                    RedirectUris = {"https://localhost:5011/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:5011/signout-callback-oidc"},
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1.read", "Read Access to API #1"),
                new ApiScope("api1.write", "Write Access to API #1"),
                new ApiScope("api2.read", "Read Access to API #2"),
                new ApiScope("api2.write", "Write Access to API #2")
            };
        }
    }
}