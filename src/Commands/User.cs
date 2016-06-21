using ConsoleAppBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MSGraphCLI.Commands
{
    [ConsoleCommand("user")]
    public static class User
    {
        private const string REQUESTURL_TEMPLATE = "https://graph.microsoft.com/beta/users/{0}";

        [ConsoleCommand("get")]
        public static void Get(string upn, string tokenEnvVariable = "MSGRAPHCLI_TOKEN")
        {
            // Check the needed parameter
            if (string.IsNullOrEmpty(upn))
            {
                throw new ArgumentException("Missing parameter 'upn' (userPrincipalName) for operation!");
            }
            if (string.IsNullOrEmpty(tokenEnvVariable))
            {
                throw new ArgumentException("Missing parameter 'tokenEnvVariable' for operation!");
            }

            // Check if the environment variable is available 
            var token = System.Environment.GetEnvironmentVariable(tokenEnvVariable);
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException($"The environment variable {tokenEnvVariable} is not set with a valid OAuth Bearer Token!");
            }

            // Now configure the HttpClient for the call against the graph API
            var finalUrl = string.Format(REQUESTURL_TEMPLATE, upn);
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add(
                "Authorization",
                token
            );

            // Execute the request and output the response
            var response = httpClient.GetAsync(finalUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseContent);
            }
            else
            {
                throw new Exception($"Request failed with http error code {response.StatusCode}!!");
            }
        }
    }
}
