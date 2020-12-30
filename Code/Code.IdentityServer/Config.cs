// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Code.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("secret_api.access", "Access Secret API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientId = "apiClient",
                    ClientName = "ApiClient for Code",
                    RequirePkce = false,
                    //客户端密码
                    ClientSecrets = {new Secret("apiSecret".Sha256())},
                    //显示允许确认页面
                    RequireConsent = false,
                    //客户端授权类型，Code:授权码模式
                    AllowedGrantTypes = GrantTypes.Code,
                    //允许登录后重定向的地址列表，可以有多个
                    RedirectUris = {"https://localhost:5002/auth.html"},
                    //允许访问的资源
                    AllowedScopes = {"secret_api.access"}
                }
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                // secret_api:标识名称
                // Secret Api：显示名称，可以自定义
                // Scopes: 资源拥有的访问作用域
                new ApiResource
                {
                    Name = "secret_api",
                    DisplayName = "Secret Api",
                    Scopes = {"secret_api.access"},
                    UserClaims =
                    {
                        ClaimTypes.Role,
                        "Department" // contain customer claim
                    }
                }
            };

        public static List<TestUser> Users =>
            new List<TestUser>
            {
                new TestUser()
                {
                    Username = "admin",
                    Password = "admin",
                    SubjectId = "4A2EC065-0A07-4D17-BA71-A9AA040959F4",
                    Claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, "admin"), // admin
                        new Claim("Department", "Shanghai") // contain customer claim
                    }
                },
                new TestUser()
                {
                    Username = "apiUserGuest",
                    Password = "apiUserPassword",
                    SubjectId = "E293B8CE-9C62-4533-A8A9-7B63DEA53534",
                    Claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, "guest") // guest
                    }
                }
            };
    };
}