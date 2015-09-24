using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accounts;
using BankDb.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using BankUtil;

namespace AccountsTest
{
    [TestClass]
    public class CheckingAccountTest
    {
        [TestMethod]
        // Shim a static property getter
        public void GetTime_WithShim()
        {
            double currentBalance = 10.0;
            DateTime expected = new DateTime(2000, 1, 1);
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return new DateTime(2000, 1, 1); };
                CheckingAccount account = new CheckingAccount("JohnDoe", currentBalance);
                DateTime result = account.GetTime();
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        // Shim a static method with parameter
        public void IsLeapYear_WithShim()
        {
            double currentBalance = 10.0;
            bool expected = false;
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.IsLeapYearInt32 = (int year) => { return false; };
                CheckingAccount account = new CheckingAccount("JohnDoe", currentBalance);
                bool result = account.IsLeapYear();
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        // Shim an instance method with no parameter
        // NOTE An instance of the shimmed class need to be passed in as the argument
        public void ShowMessage_WithShim()
        {
            double currentBalance = 10.0;
            bool expected = true;
            using (ShimsContext.Create())
            {
                BankUtil.Fakes.ShimUtil.AllInstances.ShowMessage =
                    (Util util) =>
                    {   // suppress the message box that needs to be closed by the user
                        return;
                    };
                CheckingAccount account = new CheckingAccount("JohnDoe", currentBalance);
                bool result = account.ShowMessage();
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        // Shim an instance method with normal and out parameters
        // NOTE An instance of the shimmed class need to be passed in as the first argument
        public void ForceNew_WithShim()
        {
            double currentBalance = 10.0;
            bool expected = true;
            using (ShimsContext.Create())
            {
                BankUtil.Fakes.ShimUtil.AllInstances.ToNewOrToUpdateStringBooleanOut = 
                    (Util util, string file, out bool forceNew) => 
                    {   // simulate the Yes button being clicked
                        forceNew = true; 
                        return true; 
                    };
                CheckingAccount account = new CheckingAccount("JohnDoe", currentBalance);
                bool result = account.ForceNew();
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Withdraw_ValidAmount_ChangesBalance_WithStub() // using stub
        {
            // arrange
            double currentBalance = 10.0;
            double withdrawal = 1.0;
            double expected = 9.0;

            //var account = new CheckingAccount("JohnDoe", currentBalance);

            StubIBankDb fakeBankDb = new BankDb.Fakes.StubIBankDb();
            fakeBankDb.WithdrawInt32Double = (id, amount) => 9.0;
            // same as: fakeBankDb.WithdrawInt32Double = (id, amount) => { return 9.0; };

            var account = new CheckingAccount("JohnDoe", currentBalance, fakeBankDb);

            // act
            account.WithdrawFromDb(withdrawal);
            double actual = account.Balance;

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Withdraw_ValidAmount_ChangesBalance()
        {
            // arrange
            double currentBalance = 10.0;
            double withdrawal = 1.0;
            double expected = 9.0;

            var account = new CheckingAccount("JohnDoe", currentBalance);

            // act
            account.Withdraw(withdrawal);
            double actual = account.Balance;

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Withdraw_AmountMoreThanBalance_Throws()
        {
            // arrange
            var account = new CheckingAccount("John Doe", 10.0);
            // act
            account.Withdraw(11.0);
            //account.Withdraw(1.0); // test error
            // assert is handled by the ExpectedException
        }
    }
}
