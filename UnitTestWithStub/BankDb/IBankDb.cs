using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDb
{
    public interface IBankDb
    {
        double Withdraw(int id, double amount);
        bool WithdrawWithOut(int id, double amount, out double balance);
    }
}
