using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankUtil
{
    public class Util
    {
        public bool ToNewOrToUpdate(string file, out bool forceNew)
        {
            DialogResult result = MessageBox.Show(file, file, MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Cancel) // user cancelled
            {
                forceNew = false;
                return false;
            }
            else if (result == DialogResult.Yes)
            {
                forceNew = true;
                return true;
            }
            else // No
            {
                forceNew = false;
                return true;
            }
        }

    }
}
