using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accounts;
using BankDb.Fakes;

namespace AccountsTest
{
    [TestClass]
    public class CheckingAccountTest
    {
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
        public void Withdraw_ValidAmount_ChangesBalance_Stub() // using stub
        {
            // arrange
            double currentBalance = 10.0;
            double withdrawal = 1.0;
            double expected = 9.0;

            //var account = new CheckingAccount("JohnDoe", currentBalance);

            StubIBankDb fakeBankDb = new BankDb.Fakes.StubIBankDb();
            fakeBankDb.WithdrawInt32Double = (id, amount) => 9.0;
            // same as: fakeBankDb.WithdrawInt32Double = (id, amount) => { return 9.0; };

            CheckingAccount account = new CheckingAccount("JohnDoe", currentBalance, fakeBankDb);

            // act
            account.WithdrawFromDb(withdrawal);
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
