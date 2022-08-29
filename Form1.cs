using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.ComponentModel;

namespace Launch
{
    public partial class MainForm : Form
    {
        public string EnginePath = "";
        public string ClientPath = "";
        public MainForm()
        {
            InitializeComponent();
            GetEngineDirectory();
            GetClientDirectory();
        }

        private void GetEngineDirectory()
        {
            RegistryKey CurEngine = Registry.LocalMachine.OpenSubKey("SOFTWARE\\EpicGames\\Unreal Engine\\4.27");
            if (CurEngine != null)
            {
                EnginePath = (string)CurEngine.GetValue("InstalledDirectory") + @"\Engine\Binaries\Win64\UE4Editor.exe";
                CurEngine.Close();
            }
            this.textBox_Engine.Text = EnginePath;
        }

        private void GetClientDirectory()
        {
            string currentDirName = Directory.GetCurrentDirectory();
            ClientPath = Directory.GetParent(currentDirName).ToString() + "\\Client.uproject";
            this.textBox_Client.Text = ClientPath;
        }

        private string GetCMD()
        {
            string FullCmd = ClientPath + " ";
            if (this.checkBox1.Checked)
            {
                FullCmd = FullCmd + "-log ";
            }
            FullCmd = FullCmd + this.textBox_CMD.Text;
            return FullCmd;
        }

        // 编辑器
        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(ClientPath))
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = EnginePath;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.Arguments = GetCMD();
                myProcess.Start();
            }
            else
            {
                MessageBox.Show("Client不存在");
            }
        }

        // 游戏
        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(ClientPath))
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = EnginePath;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.Arguments = GetCMD() + " -game -windowed";
                myProcess.Start();
            }
            else
            {
                MessageBox.Show("Client不存在");
            }
        }
    }
}