// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4.Models;

namespace ClientCredentials.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    //客户端Id
                    ClientId = "apiClient",
                    //客户端密码
                    ClientSecrets = {new Secret("apiSecret".Sha256())},
                    //客户端授权类型，ClientCredentials:客户端凭证方式
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //允许访问的作用域
                    AllowedScopes = {"secret_api.access"}
                }
            };

        public static IEnumerable<ApiResource> GetApis =>
            new ApiResource[]
            {
                // secret_api:标识名称
                // Secret Api：显示名称，可以自定义
                // Scopes: 资源拥有的访问作用域
                new ApiResource
                {
                    Name = "secret_api",
                    DisplayName = "Secret Api",
                    Scopes = {"secret_api.access"}
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes =>
            new ApiScope[]
            {
                new ApiScope("secret_api.access", "Secret Api Access Scope")
            };
    }
}