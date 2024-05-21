using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Todolist.Data
{
    public class ApiDbContext : IdentityDbContext<AppUser>
    {
        public virtual DbSet<Category> Category {get; set;}
        public virtual DbSet<Priority> Priority {get; set;}
        public virtual DbSet<Role> Role {get; set;}
        public virtual DbSet<Task> Task {get; set;}
        public virtual DbSet<User> User {get; set;}
        // public virtual DbSet<AspNetUsers> AspNetUsers {get; set;}
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurare relație Many-to-One între User și Task
            // modelBuilder.Entity<Task>()
            //     .HasOne(t => t.User)
            //     .WithMany(u => u.Tasks)
            //     .HasForeignKey(t => t.AppUserId)
            //     .OnDelete(DeleteBehavior.Restrict); // Poate fi schimbat în funcție de necesități (Cascade, SetNull etc.)

            

            // Configure one-to-many relationship between Task and Category
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Category)            // Each Task has one Category
                .WithMany(c => c.Tasks)              // Each Category can have many Tasks
                .HasForeignKey(t => t.CategoryID);  // Foreign key property in Task referring to CategoryID


            // Configurare relație Many-to-One între Task și Priority
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Priority)
                .WithMany()
                .HasForeignKey(t => t.PriorityID)
                .OnDelete(DeleteBehavior.Restrict); // Poate fi schimbat în funcție de necesități (Cascade, SetNull etc.)


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

/*
Pentru a configura relațiile între aceste entități în baza de date, putem urmări următoarele scheme:

One-to-Many Relationship:

Relația dintre User și Task: Un utilizator poate avea mai multe sarcini, dar o sarcină aparține unui singur utilizator.
Relația dintre Role și User: Un rol poate fi asociat cu mai mulți utilizatori, dar un utilizator are un singur rol.

Many-to-Many Relationship:

Relația dintre Task și Category: O sarcină poate fi asociată cu mai multe categorii și, în același timp, o categorie poate fi asociată cu mai multe sarcini.
Relația dintre Task și Priority: O sarcină poate avea o prioritate, dar o prioritate poate fi asociată cu mai multe sarcini.

Aceste configurări folosesc Fluent API pentru a defini modul în care entitățile sunt legate între ele în baza de date. Acest lucru oferă un nivel mai mare de 
control asupra modului în care sunt gestionate relațiile și comportamentul de ștergere/de actualizare între entități.
*/