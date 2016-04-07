using ConsoleAppBase;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSGraphCLI.Commands
{
    [ConsoleCommand("token")]
    public static class Token
    {
        private readonly static string AuthorityUrl = "https://login.microsoftonline.com/{0}";
        private readonly static string[] MandatoryCommandParameters = { "tenantid", "clientid", "clientsecretvarname", "redirecturi", "resource" };

        [ConsoleCommandDefaultMethod]
        public static void PrintHelp()
        {
            Console.WriteLine("Generates an OAuth token with Azure Active Directory for use with the passed-in resources!");
        }
                           

        [ConsoleCommand("getapptoken")]
        public static void GetAppToken(string tenantId, string clientId, string secretEnvVar, string resource)
        {
            // Validate Parameters and prepare some other variables needed
            var tenantAuthUrl = string.Format(AuthorityUrl, tenantId);
            var clientSecret = System.Environment.GetEnvironmentVariable(secretEnvVar);
            if(clientSecret == null)
            {
                throw new ArgumentException($"Environment variable passed in via 'secretEnvVar'-argument with name {secretEnvVar} does not exist!");
            }

            // Print some user state / info
            Console.WriteLine($"Acquiring token via tenant '{tenantId}'!");
            Console.WriteLine($"  with client ID {clientId}");
            Console.WriteLine($"  via authority URL {tenantAuthUrl}");
            Console.WriteLine($"  for resource {resource}");

            // Start the authentication process with ADAL
            var authResult = default(AuthenticationResult);
            var authContext = new AuthenticationContext(tenantAuthUrl);
            authContext.TokenCache.Clear();
            var credential = new ClientCredential(clientId, clientSecret);
            authResult = authContext.AcquireToken(resource, credential);

            Console.WriteLine("-----");
            Console.WriteLine($"Expires: {authResult.ExpiresOn.ToLocalTime()}");
            Console.WriteLine(authResult.CreateAuthorizationHeader());
            Console.WriteLine("-----");
            Console.WriteLine();

        }
    }
}
