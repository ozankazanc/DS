using DotRas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeVPN.Connector
{
    public class VPNConnector
    {

        private static string WinDir = Environment.GetFolderPath(Environment.SpecialFolder.System);

        private string rasDialFileName;

        private readonly string serverAddress;

        private readonly string connectionName;

        private readonly string userName;

        private readonly string passWord;

        private readonly Protocol protocol;

        private static readonly string allUserPhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

        public string RasDialFileName
        {
            get
            {
                return rasDialFileName;
            }
            set
            {
                if (File.Exists(value))
                {
                    rasDialFileName = value;
                }

                throw new FileNotFoundException();
            }
        }

        public bool IsActive
        {
            get
            {
                Process process = new Process();
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.FileName = "cmd.exe";
                process.Start();
                process.StandardInput.WriteLine("ipconfig");
                process.StandardInput.WriteLine("exit");
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                if (text.Contains("0.0.0.0"))
                {
                    return true;
                }

                return false;
            }
        }

        public RasDevice RasDevice
        {
            get
            {
                string name = Enum.GetName(typeof(Protocol), protocol);
                RasDevice rasDevice = RasDevice.GetDevices().FirstOrDefault((RasDevice c) => c.Name.Contains(name));
                if (rasDevice == null)
                {
                    throw new Exception("No device found.");
                }

                return rasDevice;
            }
        }

        public RasVpnStrategy RasVpnStrategy
        {
            get
            {
                if (protocol == Protocol.SSTP)
                {
                    return RasVpnStrategy.SstpFirst;
                }

                return RasVpnStrategy.IkeV2First;
            }
        }

        public VPNConnector(string serverAddress, string userName, string passWord)
            : this(serverAddress, serverAddress, userName, passWord, Protocol.SSTP)
        {
        }

        public VPNConnector(string serverAddress, string connectionName, string userName, string passWord)
            : this(serverAddress, connectionName, userName, passWord, Protocol.SSTP)
        {
        }

        public VPNConnector(string serverAddress, string connectionName, string userName, string passWord, Protocol protocol)
        {
            this.serverAddress = serverAddress;
            this.connectionName = connectionName;
            this.userName = userName;
            this.passWord = passWord;
            this.protocol = protocol;
            rasDialFileName = Path.Combine(WinDir, "rasdial.exe");
        }

        public bool WaitUntilActive(int timeOut = 10)
        {
            for (int i = 0; i < timeOut; i++)
            {
                if (!IsActive)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                return true;
            }

            return false;
        }

        public bool WaitUntilInActive(int timeOut = 10)
        {
            for (int i = 0; i < timeOut; i++)
            {
                if (IsActive)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                return true;
            }

            return false;
        }

        public bool TryConnect()
        {
            try
            {
                string arguments = $"{connectionName} {userName} {passWord}";
                ProcessStartInfo processStartInfo = new ProcessStartInfo(rasDialFileName, arguments);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                Debug.Assert(condition: false, ex.ToString());
            }

            WaitUntilActive();
            if (IsActive)
            {
                return true;
            }

            return false;
        }

        public bool TryDisconnect()
        {
            try
            {
                string arguments = $"{connectionName} /d";
                ProcessStartInfo processStartInfo = new ProcessStartInfo(rasDialFileName, arguments);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                Debug.Assert(condition: false, ex.ToString());
            }

            WaitUntilInActive();
            if (!IsActive)
            {
                return true;
            }

            return false;
        }

        public void CreateOrUpdate()
        {
            RasDialer rasDialer = new RasDialer();
            RasPhoneBook rasPhoneBook = new RasPhoneBook();
            rasPhoneBook.Open(openUserPhoneBook: true);
            if (rasPhoneBook.Entries.Contains(connectionName))
            {
                rasPhoneBook.Entries[connectionName].PhoneNumber = connectionName;
                rasPhoneBook.Entries[connectionName].VpnStrategy = RasVpnStrategy;
                rasPhoneBook.Entries[connectionName].Device = RasDevice;
                rasPhoneBook.Entries[connectionName].Update();
            }
            else
            {
                RasEntry item = RasEntry.CreateVpnEntry(connectionName, serverAddress, RasVpnStrategy, RasDevice);
                rasPhoneBook.Entries.Add(item);
                rasDialer.EntryName = connectionName;
                rasDialer.PhoneBookPath = allUserPhoneBookPath;
            }
        }

        public void TryDelete()
        {
            using (new RasDialer())
            {
                RasPhoneBook rasPhoneBook = new RasPhoneBook();
                rasPhoneBook.Open(openUserPhoneBook: true);
                if (rasPhoneBook.Entries.Contains(connectionName))
                {
                    TryDisconnect();
                    WaitUntilInActive();
                    rasPhoneBook.Entries.Remove(connectionName);
                }
            }
        }
    }
    public enum Protocol
    {
        SSTP,
        IKEV2,
        PPTP,
        L2TP
    }
}
