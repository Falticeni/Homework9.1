using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Homework_9._1.Classes
{
    public abstract class Conexiune
    {
        public static SqlConnection sqlConnection { get; } = new SqlConnection("Data Source =.; Initial Catalog = Week9; Integrated Security = True");
    }
}
