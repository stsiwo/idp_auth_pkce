﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> 
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("catalog_api", "sts catalog api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {

                new Client
                {
                    ClientId = "spa.react",
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    RequireClientSecret = true,

                    RedirectUris = { "http://localhost:8080/callback" },
                    PostLogoutRedirectUris = { "http://localhost:8080" },
                    AllowedCorsOrigins = { "http://localhost:8080" },

                    ClientSecrets =
                    {
                        // fix here. don't expose secret key here. instead define in appsetting.
                        // #REFACTOR
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "catalog_api"
                    }
                }
            };
//            return new[]
//            {
//                // client credentials flow client
//                new Client
//                {
//                    ClientId = "client",
//                    ClientName = "Client Credentials Client",
//
//                    AllowedGrantTypes = GrantTypes.ClientCredentials,
//                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
//
//                    AllowedScopes = { "api1" }
//                },
//
//                // MVC client using hybrid flow
//                new Client
//                {
//                    ClientId = "mvc",
//                    ClientName = "MVC Client",
//
//                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
//                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
//
//                    RedirectUris = { "http://localhost:5001/signin-oidc" },
//                    FrontChannelLogoutUri = "http://localhost:5001/signout-oidc",
//                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },
//
//                    AllowOfflineAccess = true,
//                    AllowedScopes = { "openid", "profile", "api1" }
//                },
//
//                // SPA client using implicit flow
//                new Client
//                {
//                    ClientId = "spa",
//                    ClientName = "SPA Client",
//                    ClientUri = "http://identityserver.io",
//
//                    AllowedGrantTypes = GrantTypes.Implicit,
//                    AllowAccessTokensViaBrowser = true,
//
//                    RedirectUris =
//                    {
//                        "http://localhost:5002/index.html",
//                        "http://localhost:5002/callback.html",
//                        "http://localhost:5002/silent.html",
//                        "http://localhost:5002/popup.html",
//                    },
//
//                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
//                    AllowedCorsOrigins = { "http://localhost:5002" },
//
//                    AllowedScopes = { "openid", "profile", "api1" }
//                }
//            };
        }
    }
}