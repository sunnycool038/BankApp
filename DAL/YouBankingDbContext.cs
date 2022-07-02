using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectTemplate.Models;

namespace ProjectTemplate.DAL
{
    public class YouBankingDbContext:DbContext
    {
        public YouBankingDbContext(DbContextOptions<YouBankingDbContext> options):base(options)
        {
            
        } 

        public DbSet<Account> Account { get; set; } = null!;

        public DbSet<Transaction> Transaction { get; set; } = null!;
        
    }
}