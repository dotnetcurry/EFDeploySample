using System.Data.Entity;
using System;

namespace EfDeploySample.Models
{
    public class EfDeploySampleContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer
        //      (new System.Data.Entity.
        //          DropCreateDatabaseIfModelChanges<EfDeploySample.Models.EfDeploySampleContext>());

        string _connectionName;
        string _schemaName;
        public EfDeploySampleContext(string connectionName, string schemaName)
            : base(connectionName)
        {
            if (string.IsNullOrEmpty(connectionName) ||
                string.IsNullOrEmpty(schemaName))
            {
                throw new ArgumentNullException(@"connectionName and schemaName are mandatory");
            }
            _connectionName = connectionName;
            _schemaName = schemaName;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EfDeploySampleContext>
                 (new System.Data.Entity.
                     DropCreateDatabaseIfModelChanges<EfDeploySampleContext>());

            modelBuilder.Entity<BlogPost>().ToTable("BlogPosts", _schemaName);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<BlogComment> BlogComments { get; set; }
    }
}
