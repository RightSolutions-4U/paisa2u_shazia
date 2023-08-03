using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace paisa2u.common.Models;

public partial class PaisaDbContext : DbContext
{
    private IConfiguration configuration;
    public PaisaDbContext()
    {
    }

    public PaisaDbContext(DbContextOptions<PaisaDbContext> options, IConfiguration configuration)
        : base(options)
    {
        this.configuration = configuration;
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Siteuser> Siteusers { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Subscriptionperc> Subscriptionpercs { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VendorProduct> VendorProducts { get; set; }

    //Added by Mohtashim 03-07-2023
    public virtual DbSet<transactions> Transactins { get; set; }

    public virtual DbSet<ProductByVendor> GetProductByVendor { get; set; }

    public virtual DbSet<ProductByVendor> GetAllProductsByVendor { get; set; }
    public virtual DbSet<VendorList> VendorList { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(configuration.GetConnectionString("paisa2udb"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");*/
        modelBuilder
            .Entity<VendorList>()
            .ToView(nameof(VendorList))
            .HasKey(t => t.Vendorid);
        modelBuilder
            .Entity<ProductByVendor>()
            .ToView(nameof(GetProductByVendor))
            .HasKey(t => t.Vendorid);
        modelBuilder
            .Entity<ProductByVendor>()
            .ToView(nameof(GetAllProductsByVendor))
            .HasKey(t => t.Vendorid);

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Addid).HasName("PRIMARY");

            entity.ToTable("addresses");

            entity.HasIndex(e => e.Cityid, "cityid");

            entity.HasIndex(e => e.Countryid, "countryid");

            entity.HasIndex(e => e.RegId, "regId");

            entity.Property(e => e.Addid).HasColumnName("addid");
            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .HasColumnName("area");
            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.Latitude)
                .HasMaxLength(100)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasMaxLength(100)
                .HasColumnName("longitude");
            entity.Property(e => e.Postal)
                .HasMaxLength(10)
                .HasColumnName("postal");
            entity.Property(e => e.RegId).HasColumnName("regId");
            entity.Property(e => e.Streetaddress)
                .HasMaxLength(200)
                .HasColumnName("streetaddress");
            entity.Property(e => e.Townstehsil)
                .HasMaxLength(100)
                .HasColumnName("townstehsil");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("addresses_ibfk_3");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("addresses_ibfk_2");

            entity.HasOne(d => d.Reg).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.RegId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("addresses_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Catid).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.Catid).HasColumnName("catid");
            entity.Property(e => e.Catdesc)
                .HasMaxLength(100)
                .HasColumnName("catdesc");
            entity.Property(e => e.Endate)
                .HasColumnType("datetime")
                .HasColumnName("endate");
            entity.Property(e => e.Enuser)
                .HasMaxLength(50)
                .HasColumnName("enuser");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Cityid).HasName("PRIMARY");

            entity.ToTable("city");

            entity.HasIndex(e => e.Countryid, "countryid");

            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Cityname)
                .HasMaxLength(50)
                .HasColumnName("cityname");
            entity.Property(e => e.Countryid).HasColumnName("countryid");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("city_ibfk_1");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Countryid).HasName("PRIMARY");

            entity.ToTable("country");

            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Countryname)
                .HasMaxLength(50)
                .HasColumnName("countryname");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.Catid, "catid");

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Catid).HasColumnName("catid");
            entity.Property(e => e.Dicountcondition)
                .HasMaxLength(100)
                .HasColumnName("dicountcondition");
            entity.Property(e => e.Discountamount).HasColumnName("discountamount");
            entity.Property(e => e.Discountcode)
                .HasMaxLength(50)
                .HasColumnName("discountcode");
            entity.Property(e => e.Discountpercentage).HasColumnName("discountpercentage");
            entity.Property(e => e.Picture)
                .HasMaxLength(200)
                .HasColumnName("picture");
            entity.Property(e => e.Productcode)
                .HasMaxLength(50)
                .HasColumnName("productcode");
            entity.Property(e => e.Productname)
                .HasMaxLength(100)
                .HasColumnName("productname");

            entity.HasOne(d => d.Cat).WithMany(p => p.Products)
                .HasForeignKey(d => d.Catid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_ibfk_1");

            entity.HasMany(d => d.Vendors).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "Vendorproduct",
                    r => r.HasOne<Vendor>().WithMany()
                        .HasForeignKey("Vendorid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("vendorproducts_ibfk_2"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .HasConstraintName("vendorproducts_ibfk_1"),
                    j =>
                    {
                        j.HasKey("Productid", "Vendorid")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("vendorproducts");
                        j.HasIndex(new[] { "Vendorid" }, "vendorid");
                        j.IndexerProperty<int>("Productid").HasColumnName("productid");
                        j.IndexerProperty<int>("Vendorid").HasColumnName("vendorid");
                    });
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.RegId).HasName("PRIMARY");

            entity.ToTable("Users");

            entity.Property(e => e.RegId).HasColumnName("regId");
            entity.Property(e => e.Autorenewal).HasColumnName("autorenewal");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Endate)
                .HasColumnType("datetime")
                .HasColumnName("endate");
            entity.Property(e => e.Enuser)
                .HasMaxLength(50)
                .HasColumnName("enuser");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .HasColumnName("middlename");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(50)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Pwd)
                .HasMaxLength(50)
                .HasColumnName("pwd");
            entity.Property(e => e.Qrpicture)
                .HasMaxLength(100)
                .HasColumnName("qrpicture");
            entity.Property(e => e.Referredby)
                .HasMaxLength(50)
                .HasColumnName("referredby");
            entity.Property(e => e.Regstatus).HasColumnName("regstatus");
            entity.Property(e => e.Regtype)
                .HasMaxLength(1)
                .HasColumnName("regtype");
            entity.Property(e => e.Substype)
                .HasMaxLength(1)
                .HasColumnName("substype");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Vendortype)
                .HasMaxLength(1)
                .HasColumnName("vendortype");
        });

        modelBuilder.Entity<Siteuser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PRIMARY");

            entity.ToTable("siteuser");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Adminrole)
                .HasMaxLength(1)
                .HasColumnName("adminrole");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
             entity.Property(e => e.PasswordSalt)
                .HasMaxLength(500)
                .HasColumnName("PasswordSalt");
            entity.Property(e => e.PasswordHash)
               .HasMaxLength(500)
               .HasColumnName("PasswordHash");
            entity.Property(e => e.JwtToken)
               .HasMaxLength(2000)
               .HasColumnName("JwtToken");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Subsid).HasName("PRIMARY");

            entity.ToTable("subscription");

            entity.Property(e => e.Subsid).HasColumnName("subsid");
            entity.Property(e => e.Appowner).HasColumnName("appowner");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.Endate)
                .HasColumnType("datetime")
                .HasColumnName("endate");
            entity.Property(e => e.Enuser)
                .HasMaxLength(50)
                .HasColumnName("enuser");
            entity.Property(e => e.Subfee).HasColumnName("subfee");
            entity.Property(e => e.Subtype)
                .HasMaxLength(50)
                .HasColumnName("subtype");
            entity.Property(e => e.Subvendor).HasColumnName("subvendor");
            entity.Property(e => e.Vendor).HasColumnName("vendor");
        });

        modelBuilder.Entity<Subscriptionperc>(entity =>
        {
            entity.HasKey(e => e.RecId).HasName("PRIMARY");

            entity.ToTable("subscriptionperc");

            entity.HasIndex(e => e.RegId, "regId");

            entity.Property(e => e.RecId).HasColumnName("recId");
            entity.Property(e => e.Appowner).HasColumnName("appowner");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.Endate)
                .HasColumnType("datetime")
                .HasColumnName("endate");
            entity.Property(e => e.Enuser)
                .HasMaxLength(50)
                .HasColumnName("enuser");
            entity.Property(e => e.RegId).HasColumnName("regId");
            entity.Property(e => e.Subvendor).HasColumnName("subvendor");
            entity.Property(e => e.Vendor).HasColumnName("vendor");

            /*entity.HasOne(d => d.Reg).WithMany(p => p.Subscriptionpercs)
                .HasForeignKey(d => d.RegId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subscriptionperc_ibfk_1");*/
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Vendorid).HasName("PRIMARY");

            entity.ToTable("vendor");

            entity.HasIndex(e => e.RegId, "regId");

            entity.Property(e => e.Vendorid).HasColumnName("vendorid");
            entity.Property(e => e.Endate)
                .HasColumnType("datetime")
                .HasColumnName("endate");
            entity.Property(e => e.Enuser)
                .HasMaxLength(50)
                .HasColumnName("enuser");
            entity.Property(e => e.RegId).HasColumnName("regId");

            /*entity.HasOne(d => d.Reg).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.RegId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vendor_ibfk_1");*/
        });

        //Added by Mohtashim on 03-07-2023
        modelBuilder.Entity<transactions>(entity =>
        {
            entity.HasKey(e => e.Tranid).HasName("PRIMARY");

            entity.ToTable("transactions");

            entity.HasIndex(e => e.RegId, "regId");

            entity.Property(e => e.Endate)
                .HasColumnType("datetime")
                .HasColumnName("endate");
            entity.Property(e => e.Enuser)
                .HasMaxLength(50)
                .HasColumnName("enuser");
            entity.Property(e => e.RegId).HasColumnName("regId");

          
        });

        //Added by Mohtashim on 27-06-2023
        modelBuilder.Entity<VendorProduct>()
        .HasKey(bc => new { bc.Vendorid, bc.Productid });
        modelBuilder.Entity<VendorProduct>()
            .HasOne(bc => bc.Vendor)
            .WithMany(b => b.VendorProducts)
            .HasForeignKey(bc => bc.Vendorid);
        modelBuilder.Entity<VendorProduct>()
            .HasOne(bc => bc.Product)
            .WithMany(c => c.VendorProducts)
            .HasForeignKey(bc => bc.Productid);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
