using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using m = jNet.Data.Model;

namespace jNet.Data
{
	public class AccountingDb : DbContext
	{
		private long businessId = default;

		public long BusinessId
		{
			get => businessId;
			set
			{
				Debug.Assert(businessId == default, "Changed BusinessId after it was initially set");
				if (businessId != default)
				{
					// someone is trying to change it, so change it to default so they can't hack stuff
					value = default;
				}
				businessId = value;
			}
		}
		public AccountingDb([NotNull] DbContextOptions options) : base(options)
		{
			// Add the default objects to the context and mark them as unchanged.
			// They will already exist in the DB from its initial deployment.
			// This way we can use them in code without loading them from the db.
			// See each object's IEntityTypeConfiguration<T>.Configure() method.
			Entry(m.Account.Default).State = EntityState.Unchanged;
			Entry(m.Business.Default).State = EntityState.Unchanged;
			Entry(m.CompanyDetail.Default).State = EntityState.Unchanged;
			Entry(m.Entity.Default).State = EntityState.Unchanged;
		}

		//public DbSet<Model.Association> Associations => Set<Model.Association>();
		//public DbSet<Model.Person> People => Set<Model.Person>();

		public DbSet<Model.Business> Businesses => Set<Model.Business>();
		public DbSet<m.Account> Accounts => Set<m.Account>();
		public DbSet<m.CompanyDetail> CompanyDetails => Set<m.CompanyDetail>();
		public DbSet<m.Entry> Entries => Set<m.Entry>();
		public DbSet<m.Transaction> Transactions => Set<m.Transaction>();
		public DbSet<m.Supplier> Suppliers => Set<m.Supplier>();
		public DbSet<m.Customer> Customers => Set<m.Customer>();
		public DbSet<m.Entity> Entities => Set<m.Entity>();
		public DbSet<m.Employee> Employees => Set<m.Employee>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Debugger.Launch();
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(Model.Account).Assembly);

			// filter out the default
			modelBuilder.Entity<m.Entity>().HasQueryFilter(q => q.Id > 0);
			modelBuilder.Entity<m.CompanyDetail>().HasQueryFilter(q => q.Id > 0);
			modelBuilder.Entity<m.Business>().HasQueryFilter(q => q.Id > 0);

			// The rest of the table can't have BusinessId == 0, therefore it must be set to something in the context
			modelBuilder.Entity<m.Account>().HasQueryFilter(q => q.BusinessId > 0 && q.BusinessId == businessId);//.HasOne<m.Business>().WithMany().IsRequired();
			modelBuilder.Entity<m.Customer>().HasQueryFilter(q => q.BusinessId > 0 && q.BusinessId == businessId);//.HasOne<m.Business>().WithMany().IsRequired();
			modelBuilder.Entity<m.Supplier>().HasQueryFilter(q => q.BusinessId > 0 && q.BusinessId == businessId);//.HasOne<m.Business>().WithMany().IsRequired();
			modelBuilder.Entity<m.Transaction>().HasQueryFilter(q => q.BusinessId > 0 && q.BusinessId == businessId);//.HasOne<m.Business>().WithMany().IsRequired();
			modelBuilder.Entity<m.Entry>().HasQueryFilter(q => q.BusinessId > 0 && q.BusinessId == businessId);//.HasOne<m.Business>().WithMany().IsRequired();
			modelBuilder.Entity<m.Employee>().HasQueryFilter(q => q.BusinessId > 0 && q.BusinessId == businessId);//.HasOne<m.Business>().WithMany().IsRequired();
		}

		public override int SaveChanges()
		{
			ChangeTracker.DetectChanges();

			var entities = ChangeTracker.Entries<m.BaseData2>().Where(e => e.State == EntityState.Added);
			foreach (var e in entities)
			{
				e.CurrentValues[nameof(m.BaseData2.BusinessId)] = businessId;
			}

			return base.SaveChanges();
		}
	}

	public class AccountingDbFactory : IDesignTimeDbContextFactory<AccountingDb>
	{
		AccountingDb IDesignTimeDbContextFactory<AccountingDb>.CreateDbContext(string[] args)
		{
			var o = new DbContextOptionsBuilder<AccountingDb>()
				.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
				.Options;

			return new AccountingDb(o);
		}
	}
}
