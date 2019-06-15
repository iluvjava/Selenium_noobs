using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithThePython;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine(Program.TestMethod());
        }
    }
}
