using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShell.Models;

namespace WebShell
{
    public class CommandsDataBaseContext : DbContext
    {
        public CommandsDataBaseContext(DbContextOptions<CommandsDataBaseContext> options) : base(options)
        {

        }
        public DbSet<Command> Commands {get; set;}
    }
}