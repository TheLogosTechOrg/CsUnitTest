using BankDb;
using BankUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounts
{
    public class CheckingAccount
    {
        private double m_balance = 0;
        private IBankDb m_bankDb = null;
        private AccountInfo m_accountInfo = null;

        public CheckingAccount(string name, double balance)
        {
            this.m_accountInfo = new AccountInfo() { Name = name };
            this.m_balance = balance;
        }

        public CheckingAccount(string name, double balance, IBankDb bankDb) : this(name, balance)
        {
            this.m_bankDb = bankDb;
            this.m_accountInfo.ID = 123;
        }

        public double Balance
        {
            get { return m_balance; }
        }

        public void Withdraw(double amount)
        {
            if (m_balance >= amount)
            {
                m_balance -= amount;
            }
            else
            {
                throw new ArgumentException(amount.ToString(), "Withdrawal exceeds balance!");
            }
        }

        public void WithdrawFromDb(double amount)
        {
            if (m_balance >= amount)
            {
                //m_balance -= amount;
                m_balance = m_bankDb.Withdraw(m_accountInfo.ID, amount);
            }
            else
            {
                throw new ArgumentException(amount.ToString(), "Withdrawal exceeds balance!");
            }
        }

        public DateTime GetTime()
        {
            return System.DateTime.Now;
        }

        public bool IsLeapYear()
        {
            return System.DateTime.IsLeapYear(2015);
        }

        public bool ForceNew()
        {
            Util u = new Util();
            bool result = false;
            bool forceNew = false;
            string file = "test";
            result = u.ToNewOrToUpdate(file, out forceNew);
            return result;
        }

        public static void Main()
        {
            var account = new CheckingAccount("name", 10);
            if(account.ForceNew())
                MessageBox.Show("Yes or No is clicked");
            else
                MessageBox.Show("Cancel is clicked");
        }
    }
}
