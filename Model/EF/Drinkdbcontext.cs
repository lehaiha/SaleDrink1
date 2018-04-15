namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Drinkdbcontext : DbContext
    {
        public Drinkdbcontext()
            : base("name=Drinkdbcontext")
        {
        }

        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<AnnouncementUser> AnnouncementUsers { get; set; }
        public virtual DbSet<AppRole> AppRoles { get; set; }
        public virtual DbSet<AppUserClaim> AppUserClaims { get; set; }
        public virtual DbSet<AppUserLogin> AppUserLogins { get; set; }
        public virtual DbSet<AppUserRole> AppUserRoles { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<ContactDetail> ContactDetails { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PartnerLogo> PartnerLogoes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductQuantity> ProductQuantities { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsShip> ProductsShip { get; set; }

        public virtual DbSet<ProjectProduct> ProjectProducts { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<SupportOnline> SupportOnlines { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<UserAdministrator> UserAdministrators { get; set; }
        public virtual DbSet<UserBusiness> UserBusinesses { get; set; }
        public virtual DbSet<UserCategory> UserCategories { get; set; }
        public virtual DbSet<UserGrantPermission> UserGrantPermissions { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserPost> UserPosts { get; set; }
        public virtual DbSet<VisitorStatistic> VisitorStatistics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>()
                .HasMany(e => e.AppUserRoles)
                .WithOptional(e => e.AppRole)
                .HasForeignKey(e => e.IdentityRole_Id);

            modelBuilder.Entity<AppRole>()
                .HasMany(e => e.Permissions)
                .WithOptional(e => e.AppRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.Announcements)
                .WithOptional(e => e.AppUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.AnnouncementUsers)
                .WithRequired(e => e.AppUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.AppUserClaims)
                .WithOptional(e => e.AppUser)
                .HasForeignKey(e => e.AppUser_Id);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.AppUserLogins)
                .WithOptional(e => e.AppUser)
                .HasForeignKey(e => e.AppUser_Id);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.AppUserRoles)
                .WithOptional(e => e.AppUser)
                .HasForeignKey(e => e.AppUser_Id);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.AppUser)
                .HasForeignKey(e => e.CustomerId);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.OrderDetails)
                .WithOptional(e => e.Color)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Function>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Function>()
                .Property(e => e.ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<Function>()
                .HasMany(e => e.Functions1)
                .WithOptional(e => e.Function1)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Page>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Permission>()
                .Property(e => e.FunctionId)
                .IsUnicode(false);

            modelBuilder.Entity<PostCategory>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<PostCategory>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.PostCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Post>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("PostTags").MapLeftKey("PostID").MapRightKey("TagID"));

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Tags1)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("ProductTags").MapLeftKey("ProductID").MapRightKey("TagID"));

            modelBuilder.Entity<Size>()
                .HasMany(e => e.OrderDetails)
                .WithOptional(e => e.Size)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SystemConfig>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<UserAdministrator>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserAdministrator>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserAdministrator>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserAdministrator>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<UserBusiness>()
                .Property(e => e.BusinessId)
                .IsUnicode(false);

            modelBuilder.Entity<UserPermission>()
                .Property(e => e.PermissionName)
                .IsUnicode(false);

            modelBuilder.Entity<UserPermission>()
                .Property(e => e.BusinessId)
                .IsUnicode(false);
        }
    }
}
