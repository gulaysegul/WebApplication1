﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database1Entities : DbContext
    {
        public Database1Entities()
            : base("name=Database1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Gruplar> Gruplar { get; set; }
        public virtual DbSet<Katilimcilar> Katilimcilar { get; set; }
        public virtual DbSet<Mesajlar> Mesajlar { get; set; }
        public virtual DbSet<Sohbetler> Sohbetler { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
