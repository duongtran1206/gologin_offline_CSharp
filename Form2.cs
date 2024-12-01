using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
namespace gologin_offline
{
    public partial class Form2 : Form
    {
        private StreamWriter logWriter;
        private Form1 _form1;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.Icon = new Icon("images.ico");
            _form1 = form1;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.config_data config = getConfigData();
                if (config == null)
                {
                    return;
                }
                if (dataGridView2.SelectedRows.Count == 0)
                {
                    return;
                }
                string pc_name = Environment.UserName;
                foreach (DataGridViewRow selectedRow in dataGridView2.SelectedRows)
                {
                    string id_profile = selectedRow.Cells[1].Value.ToString();
                    StartProfileApi(id_profile);
                    System.Threading.Thread.Sleep(5000);
                    string sourceFolder = Path.Combine(
                    @"C:\Users", pc_name, @"AppData\Local\Temp\GoLogin\profiles\", id_profile);
                    string destFolder = config.local_path;
                    CopyFolder(sourceFolder, destFolder);
                    System.Threading.Thread.Sleep(1000);
                    StopProfileApi(id_profile);
                    MessageBox.Show($"Successfully copied {sourceFolder} to {destFolder}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _form1.sendlog("Copied All Profiles to local");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CopyFolder(string src, string dest)
        {
            try
            {
                // Get the folder name from the source path
                string folderName = Path.GetFileName(src);
                string destPath = Path.Combine(dest, folderName);

                // Create the destination directory if it doesn't exist
                Directory.CreateDirectory(destPath);

                // Copy all files from source to destination
                foreach (var file in Directory.GetFiles(src))
                {
                    try
                    {
                        string fileName = Path.GetFileName(file);
                        if (file.Contains(@"\Default\Network\Cookies") || file.Contains(@"\Default\Network\Cookies-journal") || file.Contains(@"\Default\Sessions\"))
                        {
                            continue;
                        }
                        string destFile = Path.Combine(destPath, fileName);
                        File.Copy(file, destFile, overwrite: true);
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show($"IO Error copying file {file}: {ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (UnauthorizedAccessException authEx)
                    {
                        MessageBox.Show($"Access denied copying file {file}: {authEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to copy file {file}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Recursively copy all subdirectories
                foreach (var directory in Directory.GetDirectories(src))
                {
                    try
                    {
                        CopyFolder(directory, destPath);
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show($"IO Error copying directory {directory}: {ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (UnauthorizedAccessException authEx)
                    {
                        MessageBox.Show($"Access denied copying directory {directory}: {authEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to copy directory {directory}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //MessageBox.Show($"Successfully copied {src} to {destPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"IO Error during copy operation: {ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException authEx)
            {
                MessageBox.Show($"Access denied during copy operation: {authEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow selectedRow in dataGridView2.SelectedRows)
            {
                string id_profile = selectedRow.Cells[1].Value.ToString();
                StartProfileApi(id_profile);
            }
            _form1.sendlog("Started Selected Profiles");
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow selectedRow in dataGridView2.SelectedRows)
            {
                string id_profile = selectedRow.Cells[1].Value.ToString();
                StopProfileApi(id_profile);
            }
            _form1.sendlog("Stop All Profiles");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.config_data config = getConfigData();
            GetListProfileAsync(config.token);
            _form1.sendlog("Get List Profile Online");
        }

        private async void StopProfileApi(string id_profile)
        {
            try
            {
                Form1.config_data config = getConfigData();
                if (config == null)
                {
                    return;
                }
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:36912/browser/stop-profile");
                string profileId = id_profile;
                var content = new StringContent($@"{{
                   ""profileId"": ""{profileId}"",
                   ""sync"": true
                   }}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                sendlog(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                sendlog(ex.ToString());
            }
        }
        private async void StartProfileApi(string id_profile)
        {
            try
            {
                Form1.config_data config = getConfigData();
                if (config == null)
                {
                    return;
                }
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:36912/browser/start-profile");
                string profileId = id_profile;
                var content = new StringContent($@"{{
                   ""profileId"": ""{profileId}"",
                   ""sync"": true
                   }}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                sendlog(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                sendlog(ex.ToString());
            }
        }
        private async void GetListProfileAsync(string token)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Profile Name", typeof(string));
                table.Columns.Add("ID", typeof(string));
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://api.gologin.com/browser/v2");
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var content = new StringContent(string.Empty);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    using (JsonDocument doc = JsonDocument.Parse(responseBody))
                    {
                        var root = doc.RootElement;
                        foreach (var profile in root.GetProperty("profiles").EnumerateArray())
                        {
                            var name = profile.GetProperty("name").GetString();
                            var id = profile.GetProperty("id").GetString();
                            table.Rows.Add(name, id);
                        }
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = table;
                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        int totalWidth = dataGridView2.Width;
                        dataGridView2.Columns[0].Width = (int)(totalWidth * 0.30);
                        dataGridView2.Columns[1].Width = (int)(totalWidth * 0.75);
                        dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP request errors
                MessageBox.Show($"HTTP request error: {httpEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON parsing errors
                MessageBox.Show($"JSON parsing error: {jsonEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle any other errors
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private Form1.config_data getConfigData()
        {
            try
            {
                string jsonFilePath = "Form1.config_data.json";
                string jsonString = File.ReadAllText(jsonFilePath);
                Form1.config_data config = JsonSerializer.Deserialize<Form1.config_data>(jsonString);
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
                return null;
            }
        }
        private void sendlog(string str)
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
                Console.WriteLine($"{currentTime.ToString("HH:mm:ss")} : " + str + " | Update pop-up");
                logWriter.Flush();
                logWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing log file: {ex.Message}");
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

        private void DeletedProfile_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow selectedRow in dataGridView2.SelectedRows)
            {
                string id_profile = selectedRow.Cells[1].Value.ToString();
                DeleteProfileAPI(id_profile);
            }
        }
        public async void DeleteProfileAPI(string id_profile)
        {
            try
            {
                Form1.config_data config = getConfigData();
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, "https://api.gologin.com/browser/" + id_profile);
                request.Headers.Add("Authorization", "Bearer " + config.token);
                var content = new StringContent(string.Empty);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"HTTP request error: {httpEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"JSON parsing error: {jsonEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _form1.sendlog("Deleted selected Profiles " + id_profile);
        }
    }

}
