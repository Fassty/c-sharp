using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NezarkaBookstore
{
    class Program
    {
        /// <summary>
        /// Controller initialization and execution
        /// </summary>
        static void Main(string[] args)
        {
            IController program = new Controller();
            program.Run();
        }
    }
}
