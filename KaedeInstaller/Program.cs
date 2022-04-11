using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Threading;

namespace KaedeInstaller
{
    internal class Program
    {
        static WebClient webclient = new WebClient();

        static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KaedeExecutor";
        static readonly string exe = AppData + "\\Kaede Executor.exe";
        static readonly string zip = AppData + "\\Kaede Executor.Zip";

        static readonly string API = "http://kaedeport.tk:443/API/All";

        static void Main(string[] args)
        {
            Process Executor = new Process
            {
                StartInfo = new ProcessStartInfo(exe)
                {
                    UseShellExecute = true,
                    WorkingDirectory = AppData
                },
            };

            var StatusAPI = webclient.DownloadString(API);
            Api StatusResponse = JsonConvert.DeserializeObject<Api>(StatusAPI);

            if (!Directory.Exists(AppData))
            {
                Directory.CreateDirectory(AppData);
            }

            Logs.Warn("Please disable antivirus or whitelist Kaede Executor before use or it might cause issues.");
            Logs.Info("Welcome to Kaede Executor Launcher!");

            foreach (Process process in Process.GetProcessesByName("Kaede Executor"))
            {
                Logs.Info("Kaede Executor was forced closed!");
                process.Kill();
            }

            if (File.Exists(exe))
            {
                Logs.Check("Checking for updates!");
                if (GetFileVer())
                {
                    try
                    {
                        webclient.Proxy = null;
                        webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                        webclient.DownloadFileAsync(new Uri(StatusResponse.KEzip), zip);
                        while (webclient.IsBusy)
                            Thread.Sleep(1000);
                        if (File.Exists(zip))
                        {
                            Logs.Zip("Extracting Files!");
                            ZipArchiveHelper.ExtractToDirectory(zip, AppData, true);
                            Logs.Zip("Extracted Files!");

                            Thread.Sleep(1000);

                            Logs.Info("Finished! Launching Kaede Executor...");
                            Executor.Start();
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                        }
                        else
                        {
                            Logs.Error("ERROR: File not downloaded!");
                            Console.ReadLine();
                        }
                    }
                    catch (Exception arg)
                    {
                        Logs.FatalError("ERROR: " + arg);
                        Console.Read();
                    }
                }
                else
                {
                    Logs.Info("Launching Kaede Executor...");
                    Executor.Start();

                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
            }
            else
            {
                try
                {
                    webclient.Proxy = null;
                    webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    webclient.DownloadFileAsync(new Uri(StatusResponse.KEzip), zip);
                    while (webclient.IsBusy)
                        Thread.Sleep(1000);
                    if (File.Exists(zip))
                    {
                        Logs.Zip("Extracting Files!");
                        ZipArchiveHelper.ExtractToDirectory(zip, AppData, true);
                        Logs.Zip("Extracted Files!");

                        Thread.Sleep(1000);
                        Logs.Info("Finished! Launching Kaede Executor...");
                        Executor.Start();

                        Thread.Sleep(3000);
                        Environment.Exit(0);
                    }
                    else
                    {
                        Logs.Error("ERROR: File not downloaded!");
                        Console.ReadLine();
                    }
                }
                catch (Exception arg)
                {
                    Logs.FatalError("ERROR: " + arg);
                    Console.Read();
                }
            }
        }

        private static int counter;
        public static string totalfix;
        private static void ProgressChanged(object obj, DownloadProgressChangedEventArgs e)
        {

            var Loger = $" {e.ProgressPercentage}% of 100%, {((e.BytesReceived / 1024f) / 1024f).ToString("#0.##")}MB of {((e.TotalBytesToReceive / 1024f) / 1024f).ToString("#0.##")}MB.";

            totalfix = $"{((e.TotalBytesToReceive / 1024f) / 1024f).ToString("#0.##")}MB.";

            counter++;

            if (counter % 65 == 0)
            {
                Logs.Download(Loger);
            }
        }

        private static void Completed(object obj, AsyncCompletedEventArgs e)
        {
            var Loger = $" 100% of 100%, {totalfix}MB of {totalfix}MB.";
            Logs.Download(Loger);
        }

        private static bool GetFileVer()
        {
            if (File.Exists(exe))
            {
                FileVersionInfo fileVersionInfo = null;
                try
                {
                    fileVersionInfo = FileVersionInfo.GetVersionInfo(exe);
                }
                catch
                {
                    Logs.Error("ERROR: Unknown!");
                }
                string text = string.Format("{0}.{1}.{2}.{3}", new object[]
                {
                    fileVersionInfo.FileMajorPart,
                    fileVersionInfo.FileMinorPart,
                    fileVersionInfo.FileBuildPart,
                    fileVersionInfo.FilePrivatePart,
                });
                bool need = CheckUpdate(text);
                return need;
            }
            return true;
        }

        private static bool CheckUpdate(string fv)
        {
            foreach (Process process in Process.GetProcessesByName(exe))
            {
                process.Kill();
            }

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    var StatusAPI = webclient.DownloadString(API);
                    Api StatusResponse = JsonConvert.DeserializeObject<Api>(StatusAPI);
                    webClient.Proxy = null;
                    string text = StatusResponse.version;
                    Logs.Update($"Newest version: {text}");
                    if (!text.Contains(fv))
                    {
                        Logs.Update("Update available!");
                        return true;
                    }
                    else
                    {
                        Logs.Update("You are up to date!");
                        return false;
                    }
                }
                catch
                {
                    Logs.Error("ERROR: Unknown!");
                }
            }
            return true;
        }


    }
}
