// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Test;

namespace Password.IdentityServer
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
                new ApiScope("secret_api.access", "Secret Api Access Scope")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientId = "apiClient",
                    ClientSecrets = {new Secret("apiSecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        "secret_api.access",
                        //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                    //设置AccessToken过期时间(60秒)
                    AccessTokenLifetime = 60,
                    //如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    AllowOfflineAccess = true,
                    //RefreshToken的最长生命周期,默认30天
                    AbsoluteRefreshTokenLifetime = 2592000,
                    //刷新令牌时，将刷新RefreshToken的生命周期
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    //刷新令牌的生命周期。
                    SlidingRefreshTokenLifetime = 3600,
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

        public static List<TestUser> GetUsers =>
            new List<TestUser>
            {
                new TestUser()
                {
                    Username = "apiUser",
                    Password = "apiUserPassword",
                    SubjectId = "4A2EC065-0A07-4D17-BA71-A9AA040959F4",
                    Claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, "admin"), // admin role
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
                        new Claim(ClaimTypes.Role, "guest") // guest role
                    }
                }
            };
    };
}