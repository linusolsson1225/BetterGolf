using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace BetterGolf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.TestConnection();

        }
    }
}
