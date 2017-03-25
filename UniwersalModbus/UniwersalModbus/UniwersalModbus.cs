using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

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
#region Metoda szukająca portu COM
        // Metoda szukająca port COM

        
        public void ZnajdzPortComWKomputerze(object sender, EventArgs e)
        {
            Hashtable PortNames = new Hashtable();
            string[] ports = SerialPort.GetPortNames();

            string st = toolStripComboBoxPortCOM.Text;
            
            toolStripComboBoxPortCOM.Items.Clear();

            if (ports.Length == 0)
            {
                toolStripComboBoxPortCOM.Items.Add("ERROR: Nie znalazłem portu COM w Twoim PC :(");
            }
            else
            {               
                PortNames = BuildPortNameHash(ports);
               
                foreach (String s in PortNames.Keys)
                {
                    toolStripComboBoxPortCOM.Items.Add(PortNames[s] + ": " + s);
                }
            }            
        }
        // koniec metody

        //Wczytujemy nazwę i obrabiamy ją
        Hashtable BuildPortNameHash(string[] oPortsToMap)
        {
            Hashtable oReturnTable = new Hashtable();
            MineRegistryForPortName("SYSTEM\\CurrentControlSet\\Enum", oReturnTable, oPortsToMap);
            return oReturnTable;
        }

        void MineRegistryForPortName(string strStartKey, Hashtable oTargetMap, string[] oPortNamesToMatch)
        {
            if (oTargetMap.Count >= oPortNamesToMatch.Length)
                return;
            RegistryKey oCurrentKey = Registry.LocalMachine;

            try
            {
                oCurrentKey = oCurrentKey.OpenSubKey(strStartKey);

                string[] oSubKeyNames = oCurrentKey.GetSubKeyNames();
                if (((IList<string>)oSubKeyNames).Contains("Device Parameters") && strStartKey != "SYSTEM\\CurrentControlSet\\Enum")
                {
                    object oPortNameValue = Registry.GetValue("HKEY_LOCAL_MACHINE\\" + strStartKey + "\\Device Parameters", "PortName", null);

                    if (oPortNameValue == null || ((IList<string>)oPortNamesToMatch).Contains(oPortNameValue.ToString()) == false)
                        return;
                    object oFriendlyName = Registry.GetValue("HKEY_LOCAL_MACHINE\\" + strStartKey, "FriendlyName", null);

                    string strFriendlyName = "N/A";

                    if (oFriendlyName != null)
                        strFriendlyName = oFriendlyName.ToString();
                    if (strFriendlyName.Contains(oPortNameValue.ToString()) == false)
                        strFriendlyName = string.Format("{0} ({1})", strFriendlyName, oPortNameValue);
                    oTargetMap[strFriendlyName] = oPortNameValue;
                     
                }
                else
                {
                    foreach (string strSubKey in oSubKeyNames)
                        MineRegistryForPortName(strStartKey + "\\" + strSubKey, oTargetMap, oPortNamesToMatch);
                }
            }
            catch
            {

            }
        }
        #endregion koniec metody

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

         

        private void toolStripComboBoxPortCOM_Leave(object sender, EventArgs e)
        {
            string commm = toolStripComboBoxPortCOM.Text;
            int srednikpolozenie = commm.IndexOf(":");
            commm = commm.Substring(0, srednikpolozenie);
            toolStripComboBoxPortCOM.Text = commm;
        }

        private void UniwersalModbus_Load(object sender, EventArgs e)
        {

        }

        private void toolStripComboBoxPortCOM_DropDownClosed(object sender, EventArgs e)
        {

            string commm = toolStripComboBoxPortCOM.SelectedItem.ToString();
            
            int srednikpolozenie = commm.IndexOf(":");
             
            if (srednikpolozenie > 0)
            {
                commm = commm.Substring(0, srednikpolozenie);
                toolStripComboBoxPortCOM.Text = commm;
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
