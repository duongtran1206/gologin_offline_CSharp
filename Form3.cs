using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.AxHost;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Net;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;


namespace gologin_offline
{
    public partial class Form3 : Form
    {
        private Form1 _form1;

        public Form3(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            this.Icon = new Icon("images.ico");
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            Form1.config_data config = _form1.getConfigData();
            string pc_name = Environment.UserName;
            int column = -1;
            int row = -1;
            string selectedcol = comboBox1.SelectedItem as string;
            string selectedrow = comboBox2.SelectedItem as string;
            if (!int.TryParse(selectedcol, out column) || !int.TryParse(selectedrow, out row))
            {
                return;
            }

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height - 5;
            int frameWidth = screenWidth / column;
            int frameHeight = screenHeight / row;
            int profileWidth = frameWidth - 20;
            int profileHeight = frameHeight - 20;
            int startX = 0;
            int startY = 0;
            int cntX = 1;

            try
            {
                DataGridView dataGridViewFromForm1 = _form1.MyDataGridView;
                if (dataGridViewFromForm1.SelectedRows.Count == 0)
                {
                    return;
                }

                var tasks = new List<Task>();

                foreach (DataGridViewRow selectedRow in dataGridViewFromForm1.SelectedRows)
                {
                    int currentCntX = cntX;
                    int currentStartY = startY;
                    string id = selectedRow.Cells[1].Value.ToString();
                    string versionChrome = getVersionChrome(id);
                    int port = GetRandomPort();
                    string chromePath = $@"C:\Users\{pc_name}\.gologin\browser\orbita-browser-{versionChrome}\chrome.exe";
                    _form1.VerRunning.Add(versionChrome);
                    string arguments = $"--user-data-dir=\"{config.local_path}\\{id}\" --profile-directory=\"Default\" --remote-debugging-port={port}";
                    int newStartX = (currentCntX - 1) * frameWidth;
                    _form1.sendlog("Open profile id : " + id + "  at positon : " + newStartX + ":" + currentStartY + " with the size " + profileWidth+":"+profileHeight);
                    tasks.Add(Task.Run(() =>
                    {
                        

                        try
                        {
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
                            System.Threading.Thread.Sleep(5000);
                            ReconnectDriver(versionChrome, port, profileWidth, profileHeight, newStartX, currentStartY);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to start Chrome or reconnect driver: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));

                    if (cntX == column)
                    {
                        cntX = 1;
                        startY += frameHeight;
                    }
                    else
                    {
                        cntX++;
                    }
                }
                await Task.WhenAll(tasks);
                _form1.sendlog("Executed : Done open Profiles" );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public void ReconnectDriver(string versionChrome, int port, int windowWidth, int windowHeight, int posX, int posY)
        {
            Form1.config_data config = _form1.getConfigData();
            string chromedriver_path = Path.Combine(config.chromedriver, versionChrome, "chromedriver.exe");
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.DebuggerAddress = $"127.0.0.1:{port}";
            chromeOptions.AddArgument("--force-device-scale-factor=0.5");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(chromedriver_path);
            chromeOptions.AddArgument($"--window-size={windowWidth},{windowHeight}");
            chromeOptions.AddArgument($"--window-position={posX},{posY}");
            service.HideCommandPromptWindow = true;
            try
            {
                using (IWebDriver driver = new ChromeDriver(service, chromeOptions))
                {
                    driver.Manage().Window.Size = new System.Drawing.Size(windowWidth, windowHeight);
                    driver.Manage().Window.Position = new System.Drawing.Point(posX, posY);
                    //getIframe(driver);
                    driver.Quit();
                }
            }
            catch (Exception ex)
            {
                _form1.sendlog(ex.Message);
            }

        }
        
        public void getIframe(IWebDriver Mdriver)
        {
            IWebDriver driver = Mdriver;
            driver.Navigate().GoToUrl("https://web.telegram.org/a/#7249432100");
            Thread.Sleep(5000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement buttonElement = wait.Until(driver =>
            {
                var element = driver.FindElement(By.ClassName("bot-menu-text"));
                return (element.Displayed && element.Enabled) ? element : null;  
            });
            int timeoutInSeconds = 10;
            int intervalInMilliseconds = 500;
            buttonElement.Click();
            try
            {
                // Wait for the modal dialog to appear
                bool modalVisible = false;
                DateTime endTime = DateTime.Now.AddSeconds(timeoutInSeconds);

                while (DateTime.Now < endTime)
                {
                    try
                    {
                        var modalElement = driver.FindElement(By.ClassName("modal-content"));
                        if (modalElement.Displayed)
                        {
                            modalVisible = true;
                            break; // Exit loop if the modal is found
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        // Modal is not yet present, wait before retrying
                    }
                    Thread.Sleep(intervalInMilliseconds);
                }

                if (modalVisible)
                {
                    // Check for the Confirm button inside the modal dialog
                    var confirmButton = driver.FindElement(By.XPath("//div[contains(@class, 'modal-content')]//button[contains(text(), 'Confirm')]"));

                    // If the Confirm button exists, click it
                    if (confirmButton.Displayed)
                    {
                        confirmButton.Click();
                       // _form1.sendlog("Confirm button clicked.");
                    }
                    else
                    {
                       // _form1.sendlog("Confirm button is not displayed.");
                    }
                }
                else
                {
                   // _form1.sendlog("Modal dialog did not appear within the timeout period.");
                }
            }
            catch (NoSuchElementException)
            {
                
            }
            buttonElement.Click();
            Thread.Sleep(2000);
            Actions actions = new Actions(driver);
            actions.SendKeys(OpenQA.Selenium.Keys.Escape).Perform();
            Thread.Sleep(2000);
            buttonElement = wait.Until(driver =>
            {
                var element = driver.FindElement(By.ClassName("bot-menu-text"));
                return (element.Displayed && element.Enabled) ? element : null;  
            });
            buttonElement.Click();
            Thread.Sleep(5000);
            _form1.clickToCoordinate(413, 269, Mdriver);
            Thread.Sleep(3000);
            _form1.clickToCoordinate(413, 269, Mdriver);
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> elementsWithSrc = driver.FindElements(By.XPath("//*[@src]"));
            var elementsContainingNotpx = elementsWithSrc
            .Where(e => e.GetAttribute("src").Contains("notpx"))
            .ToList();

            // Print the 'src' of elements that match
            foreach (var element in elementsContainingNotpx)
            {
                //_form1.sendlog("Element found with 'src': " + element.GetAttribute("src"));
            }
        }

        private string getVersionChrome(String id_profile)
        {
            string version = "127";
            Form1.config_data config = _form1.getConfigData();
            string profile_path = Path.Combine(config.local_path, id_profile, "Default", "Preferences");
            string jsonString = File.ReadAllText(profile_path);
            Preference _preference = JsonSerializer.Deserialize<Preference>(jsonString);
            Extens data = _preference.extensions;
            if (data == null)
            {
                return version;
            }
            string ver = data.last_chrome_version;
            string[] array = ver.Split('.');
            return array[0];
        }
        private void configArgument(int bottom, int left, bool maximized, int right, int top, string id_profile)
        {
            Form1.config_data config = _form1.getConfigData();
            string profile_path = Path.Combine(config.local_path, id_profile, "Default", "Preferences");

            string jsonString = File.ReadAllText(profile_path);
            JObject json = JObject.Parse(jsonString);
            var windowPlacement = json["browser"]?["window_placement"];
            if (windowPlacement != null)
            {
                windowPlacement["bottom"] = bottom;
                windowPlacement["left"] = left;
                windowPlacement["maximized"] = maximized;
                windowPlacement["right"] = right;
                windowPlacement["top"] = top;
            }
            File.WriteAllText(profile_path, json.ToString(Newtonsoft.Json.Formatting.Indented));


        }
    }
    public class Window_Placement
    {
        public int bottom { get; set; }
        public int left { get; set; }
        public bool maximized { get; set; }
        public int right { get; set; }
        public int top { get; set; }
    }
    public class Browser
    {
        public Window_Placement window_placement { get; set; }
    }
    public class Preference
    {
        public Browser browser { get; set; }
        public Extens extensions { get; set; }
    }
    public class Extens
    {
        public string last_chrome_version { get; set; }
    }
}
