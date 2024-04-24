using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SQSV_lab_3.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=BookDB")
        {
        }

        public virtual DbSet<BOOK> BOOKs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
