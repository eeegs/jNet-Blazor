using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace jNet.Data
{
    public class AccountingDb: DbContext
    {
        public AccountingDb([NotNullAttribute] DbContextOptions options) : base(options)
        {
            this.Entry(Data.Model.Account.RootAccount).State = EntityState.Unchanged;
        }

        //public DbSet<Model.Association> Associations => Set<Model.Association>();
        //public DbSet<Model.Person> People => Set<Model.Person>();

        public DbSet<Model.Account> Accounts => Set<Model.Account>();
        public DbSet<Model.Entry> Entries => Set<Model.Entry>();
        public DbSet<Model.Transaction> Transactions => Set<Model.Transaction>();
        public DbSet<Model.CompanyDetail> CompanyDetails => Set<Model.CompanyDetail>();
        public DbSet<Model.Business> Businesses => Set<Model.Business>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Model.Account).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
