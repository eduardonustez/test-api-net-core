using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.WebApi.Model;

namespace Test.WebApi.Data
{
    public class ContextoBase:DbContext
    {
        public ContextoBase(DbContextOptions<ContextoBase> options):base(options)
        {
            
        }
        public DbSet<Usuario> usuarios { get; set; }
    }
}
