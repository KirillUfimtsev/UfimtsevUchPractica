﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UfimtsevUchPractica.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UchPracticaEntities : DbContext
    {
        public static UchPracticaEntities _context;

        public static UchPracticaEntities GetContext()
        {
            if (_context == null)
            {
                _context= new UchPracticaEntities();
            }
            return _context;
        }
        public UchPracticaEntities()
            : base("name=UchPracticaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CarStatus> CarStatus { get; set; }
        public virtual DbSet<Mark> Mark { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderCar> OrderCar { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<PickUpPoint> PickUpPoint { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
