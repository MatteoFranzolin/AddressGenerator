using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace ProgramAddressGenerator
{
    public class AddressGenerator : IAddress
    {
        string bit_ip;
        IPAddress ipAddress;
        public AddressGenerator(string bit_ip)
        {
            this.bit_ip = bit_ip;
        }

        /// <summary>
        /// Metodo che genera un indirizzo IP
        /// </summary>
        /// <returns>Stringa contente indirizzo IP</returns>
        /// <exception cref="Exception"></exception>
        public string generateIPv4()
        {
            if (!ControlloBitInput(bit_ip))
                throw new Exception("Bit inseriti errati");
            bool[] bool_ip = ConvertBitToBool(bit_ip);
            byte[] ip = BoolArrayToByteArray(bool_ip);

            ipAddress = new IPAddress(ip);

            return ipAddress.ToString();
        }

        /// <summary>
        /// Metodo che genera una subnet mask
        /// </summary>
        /// <returns>Stringa contente subnet mask</returns>
        public string generateSubnet()
        {
            generateIPv4();
            string subnet = "";
            char classe_ip = IpClass(ipAddress);
            switch (classe_ip)
            {
                case 'a': subnet = "255.0.0.0"; break;
                case 'b': subnet = "255.255.0.0"; break;
                case 'c': subnet = "255.255.255.0"; break;
                case 'd':
                case 'e': subnet = "Subnet non associata a questa classe"; break;
            }
            return subnet;
        }

        /// <summary>
        /// Dato in input un indirizzo IP ritorna la sua classe di appartenenza
        /// </summary>
        /// <returns></returns>
        private char IpClass(IPAddress ipAddress)
        {
            byte[] addressBytes = ipAddress.GetAddressBytes();
            char classe;

            if (addressBytes[0] >= 1 && addressBytes[0] <= 126)
            {
                classe = 'a';
            }
            else if (addressBytes[0] >= 128 && addressBytes[0] <= 191)
            {
                classe = 'b';
            }
            else if (addressBytes[0] >= 192 && addressBytes[0] <= 223)
            {
                classe = 'c';
            }
            else if (addressBytes[0] >= 224 && addressBytes[0] <= 239)
            {
                classe = 'd';
            }
            else
                classe = 'e';
            return classe;
        }

        /// <summary>
        /// Metodo che controlla, data in input una sequenza di bit, la validità dei bit e la lunghezza (32)
        /// </summary>
        /// <param name="bit">Stringa contenente una sequenza di bit</param>
        /// <returns></returns>
        private bool ControlloBitInput(string bit)
        {
            return bit.Length == 32 && bit.All(c => c == '0' || c == '1');
        }

        /// <summary>
        /// Converte una stringa di bit in un array di bool
        /// </summary>
        /// <param name="bit"></param>
        private bool[] ConvertBitToBool(string bit)
        {
            bool[] octet_bool = new bool[32];
            for (int i = 0; i < bit.Length; i++)
            {
                octet_bool[i] = bit[i] == '1';
            }
            return octet_bool;
        }

        /// <summary>
        /// Converte un array di bool in un byte
        /// </summary>
        /// <param name="bn">Array di bool da convertire</param>
        /// <returns>Byte</returns>
        /// <exception cref="Exception"></exception>
        private byte BoolToByte(bool[] bn)
        {
            if (bn.Length > 8) //se la lunghezza del vettore è maggiore di 8 non è possibile convertirlo a byte
            {
                throw new Exception("Valore non accettabile");
            }
            byte valore = 0;
            byte supporto;

            for (int i = 0; i < bn.Length; i++)
            {
                supporto = 1;
                if (bn[i])
                {
                    /*j viene impostato a 1 perché, impostandolo a 0, viene eseguita una potenza del 2 in più
                    e non viene più gestita dal valore byte, settandolo automaticamente a 0*/
                    for (int j = 1; j < bn.Length - i; j++)
                        supporto *= 2;

                    valore += supporto;
                }
            }
            return valore;
        }

        /// <summary>
        /// Converte un array di bool in un array di byte
        /// </summary>
        /// <param name="boolArray">Array di bool da convertire</param>
        /// <returns>Array di byte</returns>
        private byte[] BoolArrayToByteArray(bool[] boolArray)
        {
            byte[] ip = new byte[4];
            bool[] bools;
            for (int i = 0; i < ip.Length; i++)
            {
                bools = new bool[] { boolArray[i * 8], boolArray[i * 8 + 1], boolArray[i * 8 + 2], boolArray[i * 8 + 3], boolArray[i * 8 + 4], boolArray[i * 8 + 5], boolArray[i * 8 + 6], boolArray[i * 8 + 7] };
                ip[i] = BoolToByte(bools);
            }
            return ip;
        }
    }
    interface IAddress
    {
        string generateIPv4();
        string generateSubnet();
    }
}
