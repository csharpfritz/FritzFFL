using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fritz.FFL.Data;

namespace Fritz.FFL.Import
{
    class Program
    {
        static void Main(string[] args)
        {

            var fileToLoad = args[0];

            var rdr = new System.IO.StreamReader(fileToLoad);
            var repo = new PlayerRepository();
            var import = new ImportHandler(repo);
            while(!rdr.EndOfStream)
            {
                var thisLine = rdr.ReadLine();
                import.AddPlayer(thisLine, args[1]);
            }

            rdr.Close();
            rdr.Dispose();

        }
    }
}
