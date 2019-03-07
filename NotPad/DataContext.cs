using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotPad
{
    public class DataContext : DbContext
    {
        public DataContext() : base(@"Data Source=HPELITEBOOK850G;Database=notepad;Trusted_Connection=True;")
        {

        }

        public DbSet<Note> Notes { get;set; }
        public DbSet<User> Users { get; set; }
    }
}
