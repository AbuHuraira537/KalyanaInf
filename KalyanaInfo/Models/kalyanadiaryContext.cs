using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KalyanaInfo.Models
{
    public partial class kalyanadiaryContext : DbContext
    {
        public kalyanadiaryContext()
        {
        }

        public kalyanadiaryContext(DbContextOptions<kalyanadiaryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<Fiqqah> Fiqqah { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Mobile> Mobile { get; set; }
        public virtual DbSet<Mosque> Mosque { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Profession> Profession { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<SchoolType> SchoolType { get; set; }
        public virtual DbSet<Shope> Shope { get; set; }
        public virtual DbSet<ShopeTypes> ShopeTypes { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Video> Video { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=den1.mssql8.gear.host;Database=kalyanadiary;User ID=kalyanadiary; Password=Xv4ZV?Uh4T-M;Trusted_Connection=False;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("education");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(10);

                entity.Property(e => e.Upto)
                    .IsRequired()
                    .HasColumnName("upto")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("family");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Fiqqah>(entity =>
            {
                entity.ToTable("fiqqah");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasColumnName("gender_name")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Mobile>(entity =>
            {
                entity.ToTable("mobile");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HasMobile)
                    .IsRequired()
                    .HasColumnName("has_mobile")
                    .HasMaxLength(10);

                entity.Property(e => e.MobileType)
                    .IsRequired()
                    .HasColumnName("mobile_type")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Mosque>(entity =>
            {
                entity.ToTable("mosque");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuildDate)
                    .HasColumnName("build_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.Fiqqah).HasColumnName("fiqqah");

                entity.Property(e => e.ImamMasjid)
                    .IsRequired()
                    .HasColumnName("imam_masjid")
                    .HasMaxLength(50);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(50);

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasColumnName("maintainer")
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnName("mobile")
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.FiqqahNavigation)
                    .WithMany(p => p.Mosque)
                    .HasForeignKey(d => d.Fiqqah)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mosque_fiqqah");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Mosque)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mosque_person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.About)
                    .IsRequired()
                    .HasColumnName("about")
                    .HasMaxLength(500);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("datetime");

                entity.Property(e => e.Education).HasColumnName("education");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Family).HasColumnName("family");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.HasMobile).HasColumnName("has_mobile");

                entity.Property(e => e.Hobby)
                    .IsRequired()
                    .HasColumnName("hobby")
                    .HasMaxLength(50);

                entity.Property(e => e.IdCardOrBForm)
                    .IsRequired()
                    .HasColumnName("id_card_or_b_form")
                    .HasMaxLength(20);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasMaxLength(50);

                entity.Property(e => e.Married)
                    .IsRequired()
                    .HasColumnName("married")
                    .HasMaxLength(10);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnName("mobile")
                    .HasMaxLength(20);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Profession).HasColumnName("profession");

                entity.Property(e => e.SonOrDaughterOf)
                    .IsRequired()
                    .HasColumnName("son_or_daughter_of")
                    .HasMaxLength(50);

                entity.Property(e => e.Vehicle).HasColumnName("vehicle");

                entity.HasOne(d => d.EducationNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.Education)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_education");

                entity.HasOne(d => d.FamilyNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.Family)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_family");

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_gender");

                entity.HasOne(d => d.HasMobileNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.HasMobile)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_mobile");

                entity.HasOne(d => d.ProfessionNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.Profession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_profession");

                entity.HasOne(d => d.VehicleNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.Vehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_vehicle");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.Images)
                    .IsRequired()
                    .HasColumnName("images")
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostType)
                    .IsRequired()
                    .HasColumnName("post_type")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_person");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.ToTable("profession");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("school");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Images)
                    .IsRequired()
                    .HasColumnName("images")
                    .HasMaxLength(500);

                entity.Property(e => e.LongDescription)
                    .IsRequired()
                    .HasColumnName("long_description")
                    .HasMaxLength(500);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnName("mobile")
                    .HasMaxLength(20);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Principal)
                    .IsRequired()
                    .HasColumnName("principal")
                    .HasMaxLength(50);

                entity.Property(e => e.PrincipalPicture)
                    .IsRequired()
                    .HasColumnName("principal_picture")
                    .HasMaxLength(50);

                entity.Property(e => e.ShortDescription)
                    .IsRequired()
                    .HasColumnName("short_description")
                    .HasMaxLength(200);

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_school_school_type");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_school_person");
            });

            modelBuilder.Entity<SchoolType>(entity =>
            {
                entity.ToTable("school_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasColumnName("type_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Shope>(entity =>
            {
                entity.ToTable("shope");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.Images)
                    .IsRequired()
                    .HasColumnName("images")
                    .HasMaxLength(500);

                entity.Property(e => e.LongDescription)
                    .IsRequired()
                    .HasColumnName("long_description")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.ShopeType).HasColumnName("shope_type");

                entity.Property(e => e.ShortDescription)
                    .IsRequired()
                    .HasColumnName("short_description")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ShopeTypeNavigation)
                    .WithMany(p => p.Shope)
                    .HasForeignKey(d => d.ShopeType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_shope_shope_types");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Shope)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_shope_person");
            });

            modelBuilder.Entity<ShopeTypes>(entity =>
            {
                entity.ToTable("shope_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ShopeName)
                    .IsRequired()
                    .HasColumnName("shope_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_log");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserIp)
                    .HasColumnName("user_ip")
                    .HasMaxLength(20);

                entity.Property(e => e.UserLoginTime)
                    .HasColumnName("user_login_time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_log_person");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("vehicle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("video");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(50);

                entity.Property(e => e.UploadDate)
                    .HasColumnName("upload_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Video)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_video_person");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
