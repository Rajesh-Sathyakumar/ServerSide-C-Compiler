using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Http;
using CompileX.API;

namespace CompileX.Controllers
{
    public class CompileXapiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IEnumerable<string> Post([FromBody]IDictionary<string,List<string>> value)
        {
            string codeFilePath = "E:\\Rajesh\\Project Files\\C Sharp\\RMK\\CodeCollege\\CompileX\\TestPrograms\\";
            string codeFileName = "P1.c";
            string executableFileName = "P1.exe";
            string commandLineArgs = string.Empty;
            int waitingTimeForEachProcess = 2000;
            int consoleOutputStartLine = 6;
            int consoleOutputRemovalLength = 8;
            CommandLineProcess cmd = new CommandLineProcess("cmd.exe", codeFilePath, waitingTimeForEachProcess);
            IEnumerable<string> output;

            foreach (var args in value["commandLineArgs"])
            {
                commandLineArgs += (args + " ");
            }

            if (File.Exists(codeFilePath+executableFileName))
            {
                File.Delete(codeFilePath + executableFileName);
            }

            FileManager.CreateFile(codeFilePath + codeFileName, value["code"]);

            cmd.StartProcess();
            cmd.InputCommand("gcc \""+ codeFilePath + codeFileName + "\" -o \"" + codeFilePath + executableFileName + "\"");
            cmd.Wait();
            if (File.Exists(codeFilePath + executableFileName) == false)
            {
                cmd.InputFlushAndClose();
                List<string> errorList = cmd.ReadError().ToList();
                for (int i=0;i < errorList.Count();i++)
                {
                    errorList[i] = errorList[i].Replace(codeFilePath, "").Replace(codeFileName,"CompileX.c");
                }
                output = errorList;
            }
            else
            {
                
                cmd.InputCommand(executableFileName + " " + commandLineArgs);

                foreach (var input in value["inputString"])
                {
                    cmd.InputCommand(input);
                }

                cmd.InputFlushAndClose();
                var programResult = cmd.ReadOutput();
                IEnumerable<string> result = programResult as IList<string> ?? programResult.ToList();

                output = result.ToList();
                //output = result.ToList().GetRange(consoleOutputStartLine, result.Count()- consoleOutputRemovalLength);
            }
            return output;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}