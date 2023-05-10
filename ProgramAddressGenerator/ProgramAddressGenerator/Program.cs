using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ProgramAddressGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = "00001000000000000000100000001010";
                AddressGenerator a = new AddressGenerator(input);
            Console.WriteLine(a.generateSubnet());
        }
    }
}
