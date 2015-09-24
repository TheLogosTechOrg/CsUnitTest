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
        /// <summary>
        /// Shows a dialog which does not need user interaction. 
        /// </summary>
        public void ShowMessage()
        {
            MessageBox.Show("test");
        }

        /// <summary>
        /// Shows a dialog which needs user interaction. Involves out parameter. 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="forceNew"></param>
        /// <returns></returns>
        public bool ToNewOrToUpdate(string file, out bool forceNew)
        {
            DialogResult result = MessageBox.Show(file, file, MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Cancel) // Cancel
            {
                forceNew = false;
                return false; // cancel
            }
            else if (result == DialogResult.Yes) // Yes
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
