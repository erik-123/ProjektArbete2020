﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPMedAPI.Models.Classes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASPMedAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Post> Post { get; set; } 
        public DbSet<Image> Images { get; set;}

        public DbSet<Profil> Profil { get; set; }

        public DbSet<Vän> Vän { get; set; }

        public DbSet<VänFörfrågan> VänFörfrågningar { get; set; }

        public DbSet<BloggPost> BloggPosts { get; set; }

        public DbSet<UserPost> Posts { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}