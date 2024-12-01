using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Interactions;
using System.Net.Sockets;
using System.Net;

namespace gologin_offline
{
    public partial class Form1 : Form
    {
        private StreamWriter logWriter;
        public DataGridView MyDataGridView => dataGridView1;

        public HashSet<String> VerRunning;
        public static bool updateNow = false;

        public string key = "rCHxBaNSrn";
        public Form1()
        {
            InitializeComponent();
            VerRunning = new HashSet<String>();
            this.Icon = new Icon("images.ico");
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.VirtualMode = true;
            jHashID.Text = key;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void button1_click(object sender, EventArgs e)
        {
            sendlog("Execute : Update Profile");
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sendlog("Execute: Get Local Data Profile...");
            List<string> test = getLocalProfile();
            if (test == null || test.Count == 0)
            {
                sendlog("No data for Local Profiles");
                return;
            }
            DataTable table = new DataTable();
            table.Columns.Add("Profile Name", typeof(string));
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Proxy", typeof(string));
            table.Columns.Add("Size(GB)", typeof(string));
            foreach (string datas in test)
            {
                string[] data = datas.Split(":");
                if (data.Length >= 4)
                {
                    table.Rows.Add(data[0], data[1], data[2], data[3]);
                }
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            int totalWidth = dataGridView1.Width;
            dataGridView1.Columns[0].Width = (int)(totalWidth * 0.20);
            dataGridView1.Columns[1].Width = (int)(totalWidth * 0.35);
            dataGridView1.Columns[2].Width = (int)(totalWidth * 0.35);
            dataGridView1.Columns[3].Width = (int)(totalWidth * 0.10);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            sendlog("Loaded All Local Profiles");
        }


        private List<string> getLocalProfile()
        {
            List<string> list = new List<string>();
            config_data config = getConfigData();
            if (config != null)
            {
                try
                {
                    string[] directories = Directory.GetDirectories(config.local_path);
                    string id_profile = "";
                    string name = "";
                    string size = "";
                    string data = "";
                    foreach (string directory in directories)
                    {
                        id_profile = directory.Replace(config.local_path, "").Replace("\\", "");
                        if (id_profile.Length != 24 || id_profile.Contains("_"))
                        {
                            continue;
                        }
                        long directorySizeBytes = GetDirectorySize(directory);
                        double directorySizeGB = BytesToGigabytes(directorySizeBytes);
                        name = getNameProfile(directory);
                        size = $"{directorySizeGB:F2}";
                        if (getinfoProxy(id_profile) == null)
                        {
                            data = name + ":" + id_profile + ":null" + ":" + size;
                        }
                        else
                        {
                            data = name + ":" + id_profile + ":" + getinfoProxy(id_profile) + ":" + size;
                        }

                        list.Add(data);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accessing directories: {ex.Message}");
                    return list;
                }
            }
            return list;
        }
        private string getinfoProxy(string id)
        {
            //config_data config = getConfigData();
            string currentDirectory = Directory.GetCurrentDirectory();
            string dir_proxies = Path.Combine(currentDirectory, "data\\proxy.json");
            if (!File.Exists(dir_proxies))
            {
                return null;
            }
            string content = File.ReadAllText(dir_proxies);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(content);
            if (dictionary.ContainsKey(id))
            {
                var values = dictionary[id];
                string ip = values["ip"];
                return ip;
            }
            return null;
        }
        static string ExtractJsonString(string jsContent)
        {
            int startIndex = jsContent.IndexOf("{", jsContent.IndexOf("var config =")) + 1;
            int endIndex = jsContent.LastIndexOf("}");
            string jsonString = jsContent.Substring(startIndex, endIndex - startIndex);
            jsonString = jsonString.TrimEnd(';');
            jsonString = jsonString.Trim();
            return jsonString;
        }
        static long GetDirectorySize(string directoryPath)
        {
            long size = 0;
            string[] files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                size += fileInfo.Length;
            }

            return size;
        }

        static double BytesToGigabytes(long bytes)
        {
            return bytes / (1024.0 * 1024.0 * 1024.0);
        }
        private string getNameProfile(string dir)
        {
            string name = null;
            string userData = dir;
            string filePath = Path.Combine(userData, "Default", "Preferences");

            try
            {
                string jsonString = File.ReadAllText(filePath);

                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = doc.RootElement;
                    //JsonElement gologinElement = root.GetProperty("gologin");
                    if (root.TryGetProperty("gologin", out JsonElement gologinElement))
                    {
                        name = gologinElement.GetProperty("name").GetString();
                    }
                    else
                    {
                        name = "null";
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or deserializing file: {ex.Message}");
            }
            return name;
        }

        public void sendlog(string str)
        {
            string logDirectory = "log";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                MessageBox.Show("Log directory created.");
            }

            DateTime currentDate = DateTime.Today;
            string logFilePath = Path.Combine(logDirectory, currentDate.ToString("yyyy_MM_dd") + ".txt");
            DateTime currentTime = DateTime.Now;
            try
            {
                logWriter = new StreamWriter(logFilePath, append: true);
                Console.SetOut(logWriter);
                Console.WriteLine($"{currentTime.ToString("HH:mm:ss")} : " + str);
                LogsBox.AppendText($"{currentTime.ToString("HH:mm:ss")} : " + str + "\n");
                logWriter.Flush();
                logWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing log file: {ex.Message}");
            }
        }
        public config_data getConfigData()
        {
            try
            {
                string jsonFilePath = "config_data.json";
                string jsonString = File.ReadAllText(jsonFilePath);
                config_data config = System.Text.Json.JsonSerializer.Deserialize<config_data>(jsonString);
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
                return null;
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (logWriter != null)
            {

                logWriter.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendlog("Execute : Config Columns and Rows");
            DataGridView dataGridViewFromForm1 = dataGridView1;
            if (dataGridViewFromForm1.SelectedRows.Count == 0)
            {
                return;
            }
            Form3 form3 = new Form3(this);
            form3.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string pc_name = Environment.UserName;
            sendlog("Execute : Stop All Profiles");
            foreach (string version in VerRunning)
            {
                string targetPath = @"C:\Users\" + pc_name + @"\.gologin\browser\orbita-browser-" + version + @"\chrome.exe";
                var processes = Process.GetProcessesByName("chrome");

                foreach (var process in processes)
                {
                    try
                    {
                        string processPath = GetProcessPath(process.Id);
                        if (processPath.Equals(targetPath, StringComparison.OrdinalIgnoreCase))
                        {
                            process.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

        }
        static string GetProcessPath(int processId)
        {
            try
            {
                using (var process = Process.GetProcessById(processId))
                {
                    return process.MainModule.FileName;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


        private void EditProxy_Click(object sender, EventArgs e)
        {
            sendlog("Execute : Edit Proxy");
            DataGridView dataGridViewFromForm1 = dataGridView1;
            if (dataGridViewFromForm1.SelectedRows.Count != 1)
            {
                return;
            }
            Form4 form4 = new Form4(this);
            form4.ShowDialog();
        }

        private void config_Click(object sender, EventArgs e)
        {
            config formconfig = new config(this);
            formconfig.ShowDialog();
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            DataGridView dataGridViewFromForm1 = dataGridView1;
            foreach (DataGridViewRow selectedRow in dataGridViewFromForm1.SelectedRows)
            {
                config_data config = getConfigData();
                string id_profile = selectedRow.Cells[1].Value.ToString();
                string profile_default = Path.Combine(config.local_path, id_profile, "Default");
                string profile_cache = Path.Combine(profile_default, "Cache");
                string profile_codecache = Path.Combine(profile_default, @"Code Cache");
                DeleteAllFilesAndSubfolders(profile_cache);
                DeleteAllFilesAndSubfolders(profile_codecache);
            }
            sendlog("Execute : Clear selected profiles");

        }
        private void DeleteAllFilesAndSubfolders(string targetDirectory)
        {
            if (Directory.Exists(targetDirectory))
            {
                // Delete all files in the directory
                foreach (string file in Directory.GetFiles(targetDirectory))
                {
                    File.Delete(file);
                }

                // Delete all subdirectories and their contents
                foreach (string subDirectory in Directory.GetDirectories(targetDirectory))
                {
                    Directory.Delete(subDirectory, true); // true to delete recursively
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sendlog("Delete function do not active now !");
        }

        private void creatProfile_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listprofilename form = new listprofilename();
            form.ShowDialog();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            //ChromeOptions options = new ChromeOptions();
            //ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            //string userDataDir = @"D:\multithread\Thread1";

            //string profileName = "Profile 5"; // Replace with your profile name

            //options.AddArgument($"--user-data-dir={userDataDir}");
            //options.AddArgument($"--profile-directory={profileName}");
            //options.AddArgument("--start-maximized");
            //service.HideCommandPromptWindow = true;
            //using (IWebDriver driver = new ChromeDriver(service,options))
            //{
            //    driver.Navigate().GoToUrl("https://www.google.com");
            //    IWebElement searchBox = driver.FindElement(By.Name("q"));
            //    searchBox.SendKeys("Selenium WebDriver with Profile");
            //    searchBox.Submit();
            //    Thread.Sleep(3000);
            //    int xCoord = 314;
            //    int yCoord = 627;
            //    clickToCoordinate(xCoord, yCoord, driver);
            //    Thread.Sleep(10000);
            //}
            string userDataDir = @"D:\multithread\Thread1";

            string profileName = "Profile 5"; // Replace with your profile name
            InitDriver(userDataDir, profileName, 9000);
        }
        public IWebDriver InitDriver(string userdir, string profileName, int debugPort)
        {
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            options.AddArgument($"--user-data-dir={userdir}");
            options.AddArgument($"--profile-directory={profileName}");
            //options.AddArgument("--start-maximized");
            options.AddArgument($"--remote-debugging-port={debugPort.ToString()}");
            service.HideCommandPromptWindow = true;
            IWebDriver driver = new ChromeDriver(service, options);
            return driver;
        }

        public void clickToCoordinate(int x, int y, IWebDriver Mdriver)
        {
            Actions actions = new Actions(Mdriver);
            actions.MoveByOffset(x, y).Click().Perform();
        }

        private async void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string pc_name = Environment.UserName;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                config_data config = getConfigData();
                string id = row.Cells[1].Value.ToString();
                string versionChrome = getVersionChrome(id);
                int port = GetRandomPort();
                string chromePath = $@"C:\Users\{pc_name}\.gologin\browser\orbita-browser-{versionChrome}\chrome.exe";
                string arguments = $"--user-data-dir=\"{config.local_path}\\{id}\" --profile-directory=\"Default\" --remote-debugging-port={port}";
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = chromePath,
                    Arguments = arguments,
                    UseShellExecute = false
                };
                Process chromeProcess = new Process
                {
                    StartInfo = startInfo
                };
                chromeProcess.Start();
            }
        }
        public int GetRandomPort(int minPort = 9000, int maxPort = 9999)
        {
            Random random = new Random();
            int port = random.Next(minPort, maxPort);
            while (!IsPortAvailable(port))
            {
                port = random.Next(minPort, maxPort);
            }

            return port;
        }
        private bool IsPortAvailable(int port)
        {
            bool isAvailable = true;

            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Loopback, port);
                listener.Start();
            }
            catch (SocketException)
            {
                isAvailable = false;
            }
            finally
            {
                listener?.Stop();
            }

            return isAvailable;
        }
        private string getVersionChrome(String id_profile)
        {
            string version = "127";
            config_data config = getConfigData();
            string profile_path = Path.Combine(config.local_path, id_profile, "Default", "Preferences");
            string jsonString = File.ReadAllText(profile_path);
            Preference _preference = System.Text.Json.JsonSerializer.Deserialize<Preference>(jsonString);
            Extens data = _preference.extensions;
            if (data == null)
            {
                return version;
            }
            string ver = data.last_chrome_version;
            string[] array = ver.Split('.');
            return array[0];
        }

        public class config_data
        {
            public string token { get; set; }
            public string local_path { get; set; }
            public string chromedriver { get; set; }
            public string proxy_api_key { get; set; }
        }

        public class profile_local_data
        {
            public string profile_name { get; set; }
            public string ID { get; set; }
            public string size { get; set; }
            public string proxy { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private async void InitializeAsync()
        {
            if (updateNow)
            {
                jUpdatebutton.BackColor = Color.Red;
                jUpdatebutton.Text = "Updating";
                string UpdateUrl = "https://github.com/duongtranbka/update/raw/refs/heads/main/gologin_offline.dll";
                string DllPath = Environment.CurrentDirectory + "\\gologin_offline.dll";
                string TempUpdaterPath = Environment.CurrentDirectory + "\\updater.exe";
                using (HttpClient client = new HttpClient())
                {
                    string url = "https://raw.githubusercontent.com/duongtranbka/update/refs/heads/main/update.json";
                    try
                    {
                        string json = await client.GetStringAsync(url);

                        JObject jsonObject = JObject.Parse(json);
                        var jsonData = (JObject)jsonObject["go_offline"];
                        foreach (var property in jsonData)
                        {
                            UpdateUrl = property.Value.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                string tempDllPath = Environment.CurrentDirectory + "\\gologin_offline_new.dll";
                using (var client = new WebClient())
                {
                    client.DownloadFile(UpdateUrl, tempDllPath);
                }
                string args = $"\"{DllPath}\" \"{tempDllPath}\" \"{Application.ExecutablePath}\"";
                Process.Start(TempUpdaterPath, args);
                Application.Exit();
            }
            bool isUpdateAvailable = await checkforUpdate();
            if (isUpdateAvailable)
            {
                jUpdatebutton.BackColor = Color.Green;
                jUpdatebutton.Text = "Click for Update";
                updateNow = true;
            }
        }
        public async Task<bool> checkforUpdate()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://raw.githubusercontent.com/duongtranbka/update/refs/heads/main/update.json";
                try
                {
                    string json = await client.GetStringAsync(url);

                    //var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);

                    JObject jsonObject = JObject.Parse(json);
                    var jsonData = (JObject)jsonObject["go_offline"];
                    foreach (var property in jsonData)
                    {
                        if (property.Key == key)
                        {
                            return false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return true;
        }
        private void jUpdatebutton_Click(object sender, EventArgs e)
        {
            InitializeAsync();
        }
    }
}
