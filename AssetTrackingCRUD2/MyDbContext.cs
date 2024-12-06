using AssetTrackingCRUD2;
using Microsoft.EntityFrameworkCore;

namespace AssetTrackingCRUD2
    {
    internal class MyDbContext : DbContext
        {
        //Creates a local db
        string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=dbassets2;Integrated Security=True;";

        public DbSet<Asset> Assets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);

            //Creates a column for Asset types (phone and computer)
            modelBuilder.Entity<Asset>()
                .HasDiscriminator<string>("asset_type")
                .HasValue<Asset>("asset_base")
                .HasValue<Computer>("computer")
                .HasValue<Phone>("phone");

            //The complex datatype Price is divided into value and currency
            modelBuilder.Entity<Asset>()
               .OwnsOne(a => a.Price, p =>
               {
                   p.Property(pp => pp.Value).HasColumnName("price_value");
                   p.Property(pp => pp.Currency).HasConversion<string>().HasColumnName("price_currency");
               });

            //Country enum converts to string
            modelBuilder.Entity<Asset>()
                .Property(a => a.Country)
                .HasConversion<string>();

            //Setting other column names
            modelBuilder.Entity<Computer>()
                .Property(c => c.PurchasedDate)
                .HasColumnName("purchased_date");

            modelBuilder.Entity<Computer>()
                .Property(c => c.Brand)
                .HasColumnName("brand");

            modelBuilder.Entity<Computer>()
                .Property(c => c.Model)
                .HasColumnName("model");

            modelBuilder.Entity<Computer>()
                .Property(c => c.Country)
                .HasColumnName("country");

            modelBuilder.Entity<Phone>()
                .Property(p => p.PurchasedDate)
                .HasColumnName("purchased_date");

            modelBuilder.Entity<Phone>()
                .Property(p => p.Brand)
                .HasColumnName("brand");

            modelBuilder.Entity<Phone>()
                .Property(p => p.Model)
                .HasColumnName("model");

            modelBuilder.Entity<Phone>()
                .Property(p => p.Country)
                .HasColumnName("country");


            //Seeding data with Assets
            modelBuilder.Entity<Asset>().HasData(

         new
             {
             Id = 1,
             asset_type = "phone",
             Brand = "Apple",
             Model = "iPhone 12",
             Country = Country.germany,
             PurchasedDate = new DateTime(2020, 10, 22)
             },
          new
              {
              Id = 2,
              asset_type = "computer",
              Brand = "Dell",
              Model = "XPS 13",
              Country = Country.germany,
              PurchasedDate = new DateTime(2021, 8, 15)
              },
        new
            {
            Id = 3,
            asset_type = "computer",
            Brand = "HP",
            Model = "Spectre x360",
            Country = Country.sweden,
            PurchasedDate = new DateTime(2020, 5, 10)
            },
        new
            {
            Id = 4,
            asset_type = "computer",
            Brand = "Apple",
            Model = "MacBook Air",
            Country = Country.usa,
            PurchasedDate = new DateTime(2019, 11, 20)
            },
        new
            {
            Id = 5,
            asset_type = "computer",
            Brand = "Lenovo",
            Model = "ThinkPad X1",
            Country = Country.usa,
            PurchasedDate = new DateTime(2022, 3, 5)
            },
        new
            {
            Id = 6,
            asset_type = "computer",
            Brand = "Asus",
            Model = "ZenBook",
            Country = Country.sweden,
            PurchasedDate = new DateTime(2023, 1, 25)
            },
        new
            {
            Id = 7,
            asset_type = "phone",
            Brand = "Samsung",
            Model = "Galaxy S21",
            Country = Country.sweden,
            PurchasedDate = new DateTime(2021, 12, 01)
            },
        new
            {
            Id = 8,
            asset_type = "phone",
            Brand = "Google",
            Model = "Pixel 5",
            Country = Country.germany,
            PurchasedDate = new DateTime(2020, 10, 15)
            },
        new
            {
            Id = 9,
            asset_type = "phone",
            Brand = "OnePlus",
            Model = "8T",
            Country = Country.usa,
            PurchasedDate = new DateTime(2022, 4, 14)
            },
        new
            {
            Id = 10,
            asset_type = "phone",
            Brand = "Sony",
            Model = "Xperia 1 II",
            Country = Country.sweden,
            PurchasedDate = new DateTime(2023, 6, 1)
            }
         );


            //Seeding connected Price data. Connection with id from Asset.
            modelBuilder.Entity<Asset>()
                .OwnsOne(a => a.Price)
                .HasData(
                 new
                     {
                     AssetId = 1,
                     Value = 1020m,
                     Currency = Currency.EUR,
                     },
                  new
                      {
                      AssetId = 2,
                      Value = 1500m,
                      Currency = Currency.EUR,
                      },
                  new
                      {
                      AssetId = 3,
                      Value = 1400m,
                      Currency = Currency.SEK,
                      },
                   new
                       {
                       AssetId = 4,
                       Value = 1200m,
                       Currency = Currency.USD,
                       },
            new
                {
                AssetId = 5,
                Value = 1600m,
                Currency = Currency.USD,
                },
            new
                {
                AssetId = 6,
                Value = 1300m,
                Currency = Currency.SEK,
                },

            new
                {
                AssetId = 7,
                Value = 900m,
                Currency = Currency.SEK,
                },
            new
                {
                AssetId = 8,
                Value = 700m,
                Currency = Currency.EUR,
                },
            new
                {
                AssetId = 9,
                Value = 600m,
                Currency = Currency.USD,
                },
            new
                {
                AssetId = 10,
                Value = 950m,
                Currency = Currency.SEK,
                }
            );
            }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }