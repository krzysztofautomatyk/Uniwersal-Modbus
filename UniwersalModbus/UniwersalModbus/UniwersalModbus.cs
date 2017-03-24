using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniwersalModbus
{
    public partial class UniwersalModbus : Form
    {
        public UniwersalModbus()
        {
            InitializeComponent();
            EventHandler eh = new EventHandler(this.Zamknij);
        }
        public void Zamknij(Object sender, EventArgs e)
        {
            this.Close();
        }
       

      


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (UniwersalModbus.ActiveForm.TopMost == true) 
            {
                this.TopMost = false;
                tSB_zawszeNaWierzchu.Checked = false;
                zawszeNaWierzchuToolStripMenuItem.Checked = false;
            }
            else
            {
                this.TopMost = true;
                tSB_zawszeNaWierzchu.Checked = true;
                zawszeNaWierzchuToolStripMenuItem.Checked = true;
            }
        }

        
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void plikToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
