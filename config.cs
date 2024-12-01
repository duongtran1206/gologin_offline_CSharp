using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gologin_offline
{
    public partial class config : Form
    {
        private Form1 _form1;
        public config(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            loadDataConfig();
            this.Icon = new Icon("images.ico");
        }
        private void loadDataConfig()
        {
            Form1.config_data config = _form1.getConfigData();
            TokenBox.Text = config.token;
            LocalPathBox.Text = config.local_path;
            ChromeDriverBox.Text = config.chromedriver;
            Proxy6APIBOX.Text = config.proxy_api_key;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(TokenBox.Text == "" || LocalPathBox.Text == "" || ChromeDriverBox.Text == "" || Proxy6APIBOX.Text == "")
            {
                Form5 form5 = new Form5();
                form5.ShowDialog();
                return;
            }
            string currentDirectory = Directory.GetCurrentDirectory();
            string dir_config = Path.Combine(currentDirectory, "config_data.json");
            if (!File.Exists(dir_config))
            {
                return;
            }
            string content = File.ReadAllText(dir_config);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            dictionary["token"] = TokenBox.Text;
            dictionary["local_path"] = LocalPathBox.Text;
            dictionary["chromedriver"] = ChromeDriverBox.Text;
            dictionary["proxy_api_key"] = Proxy6APIBOX.Text;
            string updatedJson = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
            File.WriteAllText(dir_config, updatedJson);
            _form1.sendlog("Execute : Saving config");
        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    // Thiết lập kích thước bo góc
        //    int radius = 30; // Độ cong của góc
        //    GraphicsPath path = new GraphicsPath();

        //    // Tạo hình chữ nhật bo góc
        //    path.StartFigure();
        //    path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
        //    path.AddArc(new Rectangle(this.Width - radius, 0, radius, radius), 270, 90);
        //    path.AddArc(new Rectangle(this.Width - radius, this.Height - radius, radius, radius), 0, 90);
        //    path.AddArc(new Rectangle(0, this.Height - radius, radius, radius), 90, 90);
        //    path.CloseFigure();

        //    // Đặt hình dạng bo góc cho form
        //    this.Region = new Region(path);
        //}
    }
}
