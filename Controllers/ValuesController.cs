using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShell.Models;

namespace WebShell.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private CommandsDataBaseContext context;

        public ValuesController(CommandsDataBaseContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        public long Get()
        {
            long? maxSize = context.Commands.Max(u => (long?)u.cmdId);

            if (maxSize == null)
                return 0;
            else
                return maxSize.Value;
        }

        [HttpGet("{id}")]
        public string GetTodoItem([FromRoute] long id)
        {
            if (Get() == 0)
                return "";

            var cmd = context.Commands
                    .Where(с => с.cmdId == id)
                    .FirstOrDefault();

            return cmd.Line;
        }

        [HttpPost]
        public string Post([FromBody]string cmd)
        {
            var c = new Command();
            c.Line = cmd;
            c.cmdId = Get() + 1;

            context.Commands.Add(c);
            context.SaveChanges();

            if (cmd == "clear")
                return "";

            var escapedArgs = cmd.Replace("\"", "\\\"");
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();

            process.WaitForExit();

            string cmdResult = process.StandardOutput.ReadToEnd();
            string cmdError = process.StandardError.ReadToEnd();

            string result = cmdError + "\n" + cmdResult;

            return result;
        }
    }
}
