using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIssue
{
    public class Connections
    {

        public static string get()
        {
            return AppSettings.connection_string;
        }
    }
}
