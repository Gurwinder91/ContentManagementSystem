namespace ContentManagementSystem.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;

    public interface ICMSDBEntities
    {

    }
    public partial class CMSDBEntities : DbContext, ICMSDBEntities
    {
        public CMSDBEntities()
            : base("name=CMSDBEntities")
        {
        }

        public virtual DbSet<CMSAuthor> CMSAuthors { get; set; }
        public virtual DbSet<CMSEmailSubscribe> CMSEmailSubscribes { get; set; }
        public virtual DbSet<CMSNotFoundQuestion> CMSNotFoundQuestions { get; set; }
        public virtual DbSet<CMSQuestion> CMSQuestions { get; set; }
        public virtual DbSet<CMSRole> CMSRoles { get; set; }
        public virtual DbSet<CMSTechnology> CMSTechnologies { get; set; }
        public virtual DbSet<CMSUser> CMSUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CMSRole>()
                .HasMany(e => e.CMSUsers)
                .WithMany(e => e.CMSRoles)
                .Map(m => m.ToTable("CMSUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<CMSTechnology>()
                .HasMany(e => e.CMSQuestions)
                .WithRequired(e => e.CMSTechnology)
                .HasForeignKey(e => e.TechnologyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMSUser>()
                .HasMany(e => e.CMSAuthors)
                .WithOptional(e => e.CMSUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<CMSUser>()
                .HasMany(e => e.CMSQuestions)
                .WithOptional(e => e.CMSUser)
                .HasForeignKey(e => e.UploadedBy);
        }
    }
}
