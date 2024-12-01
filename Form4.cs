using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace gologin_offline
{
    public partial class Form4 : Form
    {
        private Form1 _form1;
        public Form4(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            load_current_proxy();
            this.Icon = new Icon("images.ico");
        }
        private void load_current_proxy()
        {
            DataGridView dataGridViewFromForm1 = _form1.MyDataGridView;
            DataGridViewRow selectedRow = dataGridViewFromForm1.SelectedRows[0];
            string id_profile = selectedRow.Cells[1].Value.ToString();
            string currentDirectory = Directory.GetCurrentDirectory();
            string dir_proxies = Path.Combine(currentDirectory, "data\\proxy.json");
            if (!File.Exists(dir_proxies))
            {
                return;
            }
            string content = File.ReadAllText(dir_proxies);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(content);
            if (dictionary.ContainsKey(id_profile))
            {
                var values = dictionary[id_profile];
                string type = values["type"];
                string user = values["user"];
                string pass = values["pass"];
                string ip = values["ip"];
                string port = values["port"];
                string display = type + @"://" + user + ":" + pass + @"@" + ip + ":" + port;
                textBox1.Text = display;
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string display = textBox1.Text;
            if(display == "")
            {
                saveProxy("", "", "", "", "");
                return;
            }
            string type = display.Split(@"://")[0];
            if (type == "sock5" || type == "http" || type == "https")
            {
                string data = display.Split(@"://")[1];
                string userpass = display.Split(@"@")[0];
                string ipport = display.Split(@"@")[1];
                string user = userpass.Split(@":")[0];
                string pass = userpass.Split(@":")[1];
                string ip  = ipport.Split(@":")[0];
                string port = ipport.Split(@":")[1];
                if (user=="" || pass=="" ||  ip=="" || port == "")
                {
                    Form5 form5 = new Form5();
                    form5.ShowDialog();
                }
                else
                {
                    saveProxy(type,user,pass,ip,port);
                }
            }
            else
            {
                Form5 form5 = new Form5();
                form5.ShowDialog();
            }
        }
        private void saveProxy(string type, string user, string pass, string ip, string port)
        {
            DataGridView dataGridViewFromForm1 = _form1.MyDataGridView;
            DataGridViewRow selectedRow = dataGridViewFromForm1.SelectedRows[0];
            string id_profile = selectedRow.Cells[1].Value.ToString();
            string currentDirectory = Directory.GetCurrentDirectory();
            string dir_proxies = Path.Combine(currentDirectory, "data\\proxy.json");
            if (!File.Exists(dir_proxies))
            {
                return ;
            }
            string content = File.ReadAllText(dir_proxies);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(content);
            if (dictionary.ContainsKey(id_profile))
            {
                dictionary[id_profile]["type"] = type;
                dictionary[id_profile]["user"] = user;
                dictionary[id_profile]["pass"] = pass;
                dictionary[id_profile]["ip"] = ip;
                dictionary[id_profile]["port"] = port;
            }
            else
            {
                dictionary[id_profile] = new Dictionary<string, string>
            {
                { "type", type },
                { "user", user },
                { "pass", pass },
                { "ip", ip },
                { "port", port }
            };
            }
            string updatedJson = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
            File.WriteAllText(dir_proxies, updatedJson);
            _form1.sendlog("Save profile for id profile " + id_profile + "  with ip : " + ip);
        }
    }
}
