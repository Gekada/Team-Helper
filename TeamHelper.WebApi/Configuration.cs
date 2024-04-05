using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace TeamHelper.WebApi
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =
            new List<ApiScope> { new ApiScope("TeamHelperWebApi", "Web Api") };

        public static IEnumerable<IdentityResource> IdentityResources =
            new List<IdentityResource> { new IdentityResources.OpenId(), new IdentityResources.Profile(), new IdentityResources.Email() };

        public static IEnumerable<ApiResource> ApiResources =
            new List<ApiResource> {
                new ApiResource("TeamHelperWebApi", "Web Api", new[] { JwtClaimTypes.Name, JwtClaimTypes.Role }) { Scopes = { "TeamHelperWebApi" } }
            };

        public static IEnumerable<Client> Clients =
            new List<Client> {
                new Client{
                    ClientId = "team-helper-web-app",
                    ClientName = "TeamHelper Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret  = false,
                    RequirePkce = true,
                    RequireConsent = false,
                    AccessTokenLifetime = 360000,
                    AccessTokenType = AccessTokenType.Jwt,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AllowOfflineAccess = true,
                    RedirectUris =
                    {
                        "http://localhost:4200",
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:4200",
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:4200",
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "TeamHelperWebApi"
                    },
                    AllowAccessTokensViaBrowser = true,
                },
                //new Client{
                //    ClientId = "team-helper-iot",
                //    ClientName = "TeamHelper Iot",
                //    AllowedGrantTypes = GrantTypes.DeviceFlow,
                //    RequireClientSecret  = false,
                //    RequirePkce = true,
                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        "TeamHelperWebApi"
                //    },
                //    AllowAccessTokensViaBrowser = true,
                //}
            };
    }
}