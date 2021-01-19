using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using App;

namespace CustomerTests
{
    [TestClass]
    public class CustomerAppTests
    {
        [TestMethod]
        public void CustomerAdd_WithValidInputs()
        {
            var customerAddResult = new CustomerRepository();
            bool expected = true;
            bool actual = customerAddResult.AddCustomer("Divya", "Kotagiri", "test@test.com", new DateTime(1990, 10, 17), 1);
            customerAddResult.AddCustomer("Divya", "Kotagiri", "test@test.com", new DateTime(1990, 10, 17), 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CustomerAdd_EmailValidationCheck()
        {

            var customerAddResult = new CustomerRepository();
            var result = customerAddResult.AddCustomer("Divya", "Mukkawar", "testtest.com", new DateTime(1990, 10, 17), 1);
            Assert.ThrowsException<System.Exception>(() => result);
        }

        [TestMethod]
        public void CustomerAdd_FirstNameValidationCheck()
        {

            var customerAddResult = new CustomerRepository();
            var result = customerAddResult.AddCustomer("", "Mukkawar", "test@test.com", new DateTime(1990, 10, 17), 1);
            Assert.ThrowsException<System.Exception>(() => result);
        }

        [TestMethod]
        public void CustomerAdd_LastNameValidationCheck()
        {

            var customerAddResult = new CustomerRepository();
            var result = customerAddResult.AddCustomer("Divya", "", "test@test.com", new DateTime(1990, 10, 17), 1);
            Assert.ThrowsException<System.Exception>(() => result);
        }

        [TestMethod]
        public void CustomerAdd_AllValidationCheck()
        {

            var customerAddResult = new CustomerRepository();
            var result = customerAddResult.AddCustomer("", "", "testtest.com", new DateTime(1990, 10, 17), 1);
            Assert.ThrowsException<System.Exception>(() => result);
        }
    }
}
