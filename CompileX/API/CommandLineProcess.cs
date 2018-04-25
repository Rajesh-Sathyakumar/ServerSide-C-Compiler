using System.Collections.Generic;
using System.Diagnostics;

namespace CompileX.API
{
    public class CommandLineProcess
    {
        private Process CurrentProcess { get; set; }

        private int WaitingTimeInMs { get; set; }

        public CommandLineProcess(string fileName, string workingDirectory, int waitingTimeInMs)
        {
            CurrentProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = fileName,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory
            };
            CurrentProcess.StartInfo = startInfo;
            WaitingTimeInMs = waitingTimeInMs;
        }

        public void StartProcess()
        {
            CurrentProcess.Start();
        }

        public void InputCommand(string command)
        {
            CurrentProcess.StandardInput.WriteLine(command);
        }

        public void Wait()
        {
            CurrentProcess.WaitForExit(WaitingTimeInMs);
        }

        public void InputFlushAndClose()
        {
            CurrentProcess.StandardInput.Flush();
            CurrentProcess.StandardInput.Close();
        }

        public IEnumerable<string> ReadError()
        {
            List<string> output = new List<string>();

            while (!CurrentProcess.StandardError.EndOfStream)
            {
                output.Add(CurrentProcess.StandardError.ReadLine());
            }
            return output;
        }

        public IEnumerable<string> ReadOutput()
        {
            List<string> output = new List<string>();

            while (!CurrentProcess.StandardOutput.EndOfStream)
            {
                output.Add(CurrentProcess.StandardOutput.ReadLine());
            }
            return output;
        }

    }
}