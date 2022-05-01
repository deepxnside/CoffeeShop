using IdentityServer4.Models;

namespace IdentityServer;

public class Config
{
    public static IEnumerable<IdentityResource> IdentityResource =>
        new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }

        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[] {new ApiScope("Api.read"), new ApiScope("Api.write"),};

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("Api")
            {
                Scopes = new List<string> {"Api.read", "Api.write"},
                ApiSecrets = new List<Secret> {new Secret("LMAO".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "new",
                ClientName = "Client Credentials Client",
                ClientSecrets = {new Secret("LMFAO".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"APi.read", "Api.write"}
            },
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("LMFAO".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = {"openid", "profile", "CoffeeAPI.read"},
                RedirectUris = {"https://localhost:5444/signin-oidc"},
                FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},
                AllowOfflineAccess = false,
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
        };
}