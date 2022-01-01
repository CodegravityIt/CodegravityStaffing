//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.SqlServer;
//using System.Configuration;
//using Microsoft.Extensions.Configuration;

//namespace WebApi_New.Models
//{
//    public class IncentiveContext: DbContext
//    {
//        public IncentiveContext(DbContextOptions options) : base(options) { }
//        public IConfiguration Configuration { get; }
//        public DbSet<cg_Incentivedetils> Incentive
//        {
//            get;
//            set;
//        }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("CodeGravityDB"));
//            }
//        }
//    }
//}
