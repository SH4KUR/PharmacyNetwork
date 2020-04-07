﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PharmacyNetwork.Infrastructure.Data.Migrations
{
    [DbContext(typeof(PharmacyNetworkContext))]
    partial class PharmacyNetworkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.Check", b =>
                {
                    b.Property<int>("MedItemId")
                        .HasColumnName("med_item_id")
                        .HasColumnType("int");

                    b.Property<int>("PurchId")
                        .HasColumnName("purch_id")
                        .HasColumnType("int");

                    b.Property<int>("ItemCount")
                        .HasColumnName("item_count")
                        .HasColumnType("int");

                    b.HasKey("MedItemId", "PurchId")
                        .HasName("PK_checks");

                    b.HasIndex("PurchId");

                    b.ToTable("check");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.Firm", b =>
                {
                    b.Property<int>("FirmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("firm_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirmAddress")
                        .IsRequired()
                        .HasColumnName("firm_address")
                        .HasColumnType("varchar(75)")
                        .HasMaxLength(75)
                        .IsUnicode(false);

                    b.Property<string>("FirmContact")
                        .IsRequired()
                        .HasColumnName("firm_contact")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<decimal>("FirmMarkup")
                        .HasColumnName("firm_markup")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("FirmName")
                        .IsRequired()
                        .HasColumnName("firm_name")
                        .HasColumnType("varchar(75)")
                        .HasMaxLength(75)
                        .IsUnicode(false);

                    b.HasKey("FirmId");

                    b.ToTable("firm");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.Income", b =>
                {
                    b.Property<int>("IncomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("income_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("IncomeDate")
                        .HasColumnName("income_date")
                        .HasColumnType("datetime");

                    b.Property<int>("PharmId")
                        .HasColumnName("pharm_id")
                        .HasColumnType("int");

                    b.HasKey("IncomeId");

                    b.HasIndex("PharmId");

                    b.ToTable("income");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.IncomeDetail", b =>
                {
                    b.Property<int>("MedItemId")
                        .HasColumnName("med_item_id")
                        .HasColumnType("int");

                    b.Property<int>("IncomeId")
                        .HasColumnName("income_id")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnName("count")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("MedItemId", "IncomeId")
                        .HasName("PK_incomes_detail");

                    b.HasIndex("IncomeId");

                    b.ToTable("income_detail");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.MedicalItem", b =>
                {
                    b.Property<int>("MedItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("med_item_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategId")
                        .HasColumnName("categ_id")
                        .HasColumnType("int");

                    b.Property<int>("FirmId")
                        .HasColumnName("firm_id")
                        .HasColumnType("int");

                    b.Property<string>("MedItemDescrip")
                        .HasColumnName("med_item_descrip")
                        .HasColumnType("text");

                    b.Property<string>("MedItemName")
                        .IsRequired()
                        .HasColumnName("med_item_name")
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60)
                        .IsUnicode(false);

                    b.Property<decimal>("MedItemPrice")
                        .HasColumnName("med_item_price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("MedItemPriceMarkup")
                        .HasColumnName("med_item_price_markup")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("MedItemId")
                        .HasName("PK_medical_items");

                    b.HasIndex("CategId");

                    b.HasIndex("FirmId");

                    b.ToTable("medical_item");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.Pharmacy", b =>
                {
                    b.Property<int>("PharmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pharm_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PharmAddress")
                        .IsRequired()
                        .HasColumnName("pharm_address")
                        .HasColumnType("varchar(75)")
                        .HasMaxLength(75)
                        .IsUnicode(false);

                    b.Property<string>("PharmName")
                        .IsRequired()
                        .HasColumnName("pharm_name")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PharmId")
                        .HasName("PK_pharmacies");

                    b.ToTable("pharmacy");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.PharmacyWharehouse", b =>
                {
                    b.Property<int>("PharmId")
                        .HasColumnName("pharm_id")
                        .HasColumnType("int");

                    b.Property<int>("MedItemId")
                        .HasColumnName("med_item_id")
                        .HasColumnType("int");

                    b.Property<int>("ItemCount")
                        .HasColumnName("item_count")
                        .HasColumnType("int");

                    b.HasKey("PharmId", "MedItemId")
                        .HasName("PK_pharmacy_wharehouses");

                    b.HasIndex("MedItemId");

                    b.ToTable("pharmacy_wharehouse");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.ProductCategory", b =>
                {
                    b.Property<int>("CategId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("categ_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CategMarkup")
                        .HasColumnName("categ_markup")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("CategName")
                        .IsRequired()
                        .HasColumnName("categ_name")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CategId")
                        .HasName("PK_product_categories");

                    b.ToTable("product_category");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.Purchase", b =>
                {
                    b.Property<int>("PurchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("purch_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PharmId")
                        .HasColumnName("pharm_id")
                        .HasColumnType("int");

                    b.Property<decimal>("PurchAmount")
                        .HasColumnName("purch_amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("PurchDate")
                        .HasColumnName("purch_date")
                        .HasColumnType("datetime");

                    b.Property<decimal>("PurchDiscountPercent")
                        .HasColumnName("purch_discount_percent")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("PurchId")
                        .HasName("PK_purchases");

                    b.HasIndex("PharmId");

                    b.ToTable("purchase");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.ReservedMedItem", b =>
                {
                    b.Property<int>("ReservedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("reserved_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnName("count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFinish")
                        .HasColumnName("date_finish")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateStart")
                        .HasColumnName("date_start")
                        .HasColumnType("datetime");

                    b.Property<int>("MedItemId")
                        .HasColumnName("med_item_id")
                        .HasColumnType("int");

                    b.Property<int>("PharmId")
                        .HasColumnName("pharm_id")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnName("telephone")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.HasKey("ReservedId")
                        .HasName("PK_reserved_med_items");

                    b.HasIndex("PharmId", "MedItemId");

                    b.ToTable("reserved_med_item");
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.Check", b =>
                {
                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.MedicalItem", "MedItem")
                        .WithMany("Check")
                        .HasForeignKey("MedItemId")
                        .HasConstraintName("FK_check_medical_item")
                        .IsRequired();

                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.Purchase", "Purch")
                        .WithMany("Check")
                        .HasForeignKey("PurchId")
                        .HasConstraintName("FK_check_purchase")
                        .IsRequired();
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.Income", b =>
                {
                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.Pharmacy", "Pharm")
                        .WithMany("Income")
                        .HasForeignKey("PharmId")
                        .HasConstraintName("FK_income_pharmacy")
                        .IsRequired();
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.IncomeDetail", b =>
                {
                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.Income", "Income")
                        .WithMany("IncomeDetail")
                        .HasForeignKey("IncomeId")
                        .HasConstraintName("FK_income_detail_income")
                        .IsRequired();

                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.MedicalItem", "MedItem")
                        .WithMany("IncomeDetail")
                        .HasForeignKey("MedItemId")
                        .HasConstraintName("FK_income_detail_medical_item")
                        .IsRequired();
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.MedicalItem", b =>
                {
                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.ProductCategory", "Categ")
                        .WithMany("MedicalItem")
                        .HasForeignKey("CategId")
                        .HasConstraintName("FK_medical_item_product_category")
                        .IsRequired();

                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.Firm", "Firm")
                        .WithMany("MedicalItem")
                        .HasForeignKey("FirmId")
                        .HasConstraintName("FK_medical_item_firm")
                        .IsRequired();
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.PharmacyWharehouse", b =>
                {
                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.MedicalItem", "MedItem")
                        .WithMany("PharmacyWharehouse")
                        .HasForeignKey("MedItemId")
                        .HasConstraintName("FK_pharmacy_wharehouse_medical_item")
                        .IsRequired();

                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.Pharmacy", "Pharm")
                        .WithMany("PharmacyWharehouse")
                        .HasForeignKey("PharmId")
                        .HasConstraintName("FK_pharmacy_wharehouse_pharmacy")
                        .IsRequired();
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.Purchase", b =>
                {
                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.Pharmacy", "Pharm")
                        .WithMany("Purchase")
                        .HasForeignKey("PharmId")
                        .HasConstraintName("FK_purchase_pharmacy")
                        .IsRequired();
                });

            modelBuilder.Entity("PharmacyNetwork.ApplicationCore.Entities.ReservedMedItem", b =>
                {
                    b.HasOne("PharmacyNetwork.ApplicationCore.Entities.PharmacyWharehouse", "PharmacyWharehouse")
                        .WithMany("ReservedMedItem")
                        .HasForeignKey("PharmId", "MedItemId")
                        .HasConstraintName("FK_reserved_med_item_pharmacy_wharehouse")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
