using ConsoleAppBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSGraphCLI.Commands
{
    [ConsoleCommand("mail")]
    public class Mail
    {
        [ConsoleCommand("list")]
        public static void ListEmails()
        {
            return;
        }
    }
}
