using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Infrastructure.Data
{
    public partial class PharmacyNetworkContext : DbContext
    {
        public PharmacyNetworkContext()
        {
        }

        public PharmacyNetworkContext(DbContextOptions<PharmacyNetworkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Check> Check { get; set; }
        public virtual DbSet<Firm> Firm { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<IncomeDetail> IncomeDetail { get; set; }
        public virtual DbSet<MedicalItem> MedicalItem { get; set; }
        public virtual DbSet<Pharmacy> Pharmacy { get; set; }
        public virtual DbSet<PharmacyWharehouse> PharmacyWharehouse { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<ReservedMedItem> ReservedMedItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-M3LJRB3;Initial Catalog=PharmacyNetwork;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Check>(entity =>
            {
                entity.HasKey(e => new { e.MedItemId, e.PurchId })
                    .HasName("PK_checks");

                entity.ToTable("check");

                entity.Property(e => e.MedItemId).HasColumnName("med_item_id");

                entity.Property(e => e.PurchId).HasColumnName("purch_id");

                entity.Property(e => e.ItemCount).HasColumnName("item_count");

                entity.HasOne(d => d.MedItem)
                    .WithMany(p => p.Check)
                    .HasForeignKey(d => d.MedItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_check_medical_item");

                entity.HasOne(d => d.Purch)
                    .WithMany(p => p.Check)
                    .HasForeignKey(d => d.PurchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_check_purchase");
            });

            modelBuilder.Entity<Firm>(entity =>
            {
                entity.ToTable("firm");

                entity.Property(e => e.FirmId).HasColumnName("firm_id");

                entity.Property(e => e.FirmAddress)
                    .IsRequired()
                    .HasColumnName("firm_address")
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.FirmContact)
                    .IsRequired()
                    .HasColumnName("firm_contact")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirmMarkup)
                    .HasColumnName("firm_markup")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FirmName)
                    .IsRequired()
                    .HasColumnName("firm_name")
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.ToTable("income");

                entity.Property(e => e.IncomeId).HasColumnName("income_id");

                entity.Property(e => e.IncomeDate)
                    .HasColumnName("income_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PharmId).HasColumnName("pharm_id");

                entity.HasOne(d => d.Pharm)
                    .WithMany(p => p.Income)
                    .HasForeignKey(d => d.PharmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_income_pharmacy");
            });

            modelBuilder.Entity<IncomeDetail>(entity =>
            {
                entity.HasKey(e => new { e.MedItemId, e.IncomeId })
                    .HasName("PK_incomes_detail");

                entity.ToTable("income_detail");

                entity.Property(e => e.MedItemId).HasColumnName("med_item_id");

                entity.Property(e => e.IncomeId).HasColumnName("income_id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Income)
                    .WithMany(p => p.IncomeDetail)
                    .HasForeignKey(d => d.IncomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_income_detail_income");

                entity.HasOne(d => d.MedItem)
                    .WithMany(p => p.IncomeDetail)
                    .HasForeignKey(d => d.MedItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_income_detail_medical_item");
            });

            modelBuilder.Entity<MedicalItem>(entity =>
            {
                entity.HasKey(e => e.MedItemId)
                    .HasName("PK_medical_items");

                entity.ToTable("medical_item");

                entity.Property(e => e.MedItemId).HasColumnName("med_item_id");

                entity.Property(e => e.CategId).HasColumnName("categ_id");

                entity.Property(e => e.FirmId).HasColumnName("firm_id");

                entity.Property(e => e.MedItemDescrip)
                    .HasColumnName("med_item_descrip")
                    .HasColumnType("text");

                entity.Property(e => e.MedItemName)
                    .IsRequired()
                    .HasColumnName("med_item_name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.MedItemPrice)
                    .HasColumnName("med_item_price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MedItemPriceMarkup)
                    .HasColumnName("med_item_price_markup")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Categ)
                    .WithMany(p => p.MedicalItem)
                    .HasForeignKey(d => d.CategId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_medical_item_product_category");

                entity.HasOne(d => d.Firm)
                    .WithMany(p => p.MedicalItem)
                    .HasForeignKey(d => d.FirmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_medical_item_firm");
            });

            modelBuilder.Entity<Pharmacy>(entity =>
            {
                entity.HasKey(e => e.PharmId)
                    .HasName("PK_pharmacies");

                entity.ToTable("pharmacy");

                entity.Property(e => e.PharmId).HasColumnName("pharm_id");

                entity.Property(e => e.PharmAddress)
                    .IsRequired()
                    .HasColumnName("pharm_address")
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.PharmName)
                    .IsRequired()
                    .HasColumnName("pharm_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PharmacyWharehouse>(entity =>
            {
                entity.HasKey(e => new { e.PharmId, e.MedItemId })
                    .HasName("PK_pharmacy_wharehouses");

                entity.ToTable("pharmacy_wharehouse");

                entity.Property(e => e.PharmId).HasColumnName("pharm_id");

                entity.Property(e => e.MedItemId).HasColumnName("med_item_id");

                entity.Property(e => e.ItemCount).HasColumnName("item_count");

                entity.HasOne(d => d.MedItem)
                    .WithMany(p => p.PharmacyWharehouse)
                    .HasForeignKey(d => d.MedItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pharmacy_wharehouse_medical_item");

                entity.HasOne(d => d.Pharm)
                    .WithMany(p => p.PharmacyWharehouse)
                    .HasForeignKey(d => d.PharmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pharmacy_wharehouse_pharmacy");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategId)
                    .HasName("PK_product_categories");

                entity.ToTable("product_category");

                entity.Property(e => e.CategId).HasColumnName("categ_id");

                entity.Property(e => e.CategMarkup)
                    .HasColumnName("categ_markup")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CategName)
                    .IsRequired()
                    .HasColumnName("categ_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.PurchId)
                    .HasName("PK_purchases");

                entity.ToTable("purchase");

                entity.Property(e => e.PurchId).HasColumnName("purch_id");

                entity.Property(e => e.PharmId).HasColumnName("pharm_id");

                entity.Property(e => e.PurchAmount)
                    .HasColumnName("purch_amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PurchDate)
                    .HasColumnName("purch_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PurchDiscountPercent)
                    .HasColumnName("purch_discount_percent")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Pharm)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.PharmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_purchase_pharmacy");
            });

            modelBuilder.Entity<ReservedMedItem>(entity =>
            {
                entity.HasKey(e => e.ReservedId)
                    .HasName("PK_reserved_med_items");

                entity.ToTable("reserved_med_item");

                entity.Property(e => e.ReservedId).HasColumnName("reserved_id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.DateFinish)
                    .HasColumnName("date_finish")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateStart)
                    .HasColumnName("date_start")
                    .HasColumnType("datetime");

                entity.Property(e => e.MedItemId).HasColumnName("med_item_id");

                entity.Property(e => e.PharmId).HasColumnName("pharm_id");

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasColumnName("telephone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.PharmacyWharehouse)
                    .WithMany(p => p.ReservedMedItem)
                    .HasForeignKey(d => new { d.PharmId, d.MedItemId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reserved_med_item_pharmacy_wharehouse");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
