using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProgramAddressGenerator;

namespace AddressTests
{
    [TestClass]
    public class TestAddressClass
    {
        [TestMethod]
        public void test1GenerateIPv4()
        {
            string input = "00001000000000000000100000001010";
            string expected = "8.0.8.10";
            AddressGenerator a = new AddressGenerator(input);
            string actual = a.generateIPv4();
            Assert.AreEqual(expected, actual, "#1 Correct test");
        }
        [TestMethod]
        public void test1GenerateSubnetMask()
        {
            //contiene anche test di IpClass()
            string input = "00001000000000000000100000001010";
            string expected = "255.0.0.0";
            AddressGenerator a = new AddressGenerator(input);
            string actual = a.generateSubnet();
            Assert.AreEqual(expected, actual, "#1 Correct test");
        }

    }
}
