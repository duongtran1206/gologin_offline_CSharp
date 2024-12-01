using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gologin_offline
{
    public partial class listprofilename : Form
    {
        public listprofilename()
        {
            InitializeComponent();
        }

        private void Submit_buton_Click(object sender, EventArgs e)
        {
            string[] list_profile_name = listnameprofileBox.Text.Split("\n");
            return;
        }
    }
}
