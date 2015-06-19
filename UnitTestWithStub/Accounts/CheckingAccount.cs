using BankDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    public class CheckingAccount
    {
        private string name;
        double m_balance = 0;
        IBankDb m_bankDb = null;
        AccountInfo m_accountInfo = null;

        public CheckingAccount(string name, double balance)
        {
            this.name = name;
            this.m_balance = balance;
        }

        public CheckingAccount(string name, double balance, IBankDb bankDb) : this(name, balance)
        {
            this.m_bankDb = bankDb;
            this.m_accountInfo = new AccountInfo() { ID = 123 };
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
    }
}
