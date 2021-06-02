using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Entities;
using Models.Enums;

namespace Data.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Email).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Name).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();

            builder.OwnsMany<Order>("Orders", x =>
            {
                x.WithOwner().HasForeignKey("CustomerId");
                x.ToTable("Orders");
                x.Property<Guid>("Id");
                x.HasKey("Id");
                x.Property<DateTime>("OrderDate").HasColumnName("OrderDate");
                x.Property<DateTime?>("ChangeDate").HasColumnName("ChangeDate");
                x.Property<bool>("IsCancelled").HasColumnName("IsCancelled");
                x.Property("Status").HasColumnName("StatusId").HasConversion(new EnumToNumberConverter<OrderStatus, byte>());

                x.OwnsOne(order => order.TotalPrice, builder =>
                {
                    builder.Property(e => e.Value).HasColumnName("TotalPrice").HasColumnType("decimal(5,2)");
                    builder.OwnsOne(e => e.Currency, bc =>
                    {
                        bc.Property(e => e.Name).HasColumnName("Currency").HasMaxLength(5).IsRequired();
                    });
                });

                x.OwnsMany<OrderLine>("OrderLines", builder =>
                {
                    builder.WithOwner().HasForeignKey("OrderId");
                    builder.ToTable("OrderLines");
                    builder.Property<Guid>("OrderId");
                    builder.Property<Guid>("ProductId");
                    builder.HasKey("OrderId", "ProductId");

                    builder.OwnsOne(e => e.ProductBasePrice, b =>
                    {
                        b.Property(e => e.Value).HasColumnName("BasePrice").HasColumnType("decimal(5,2)");
                        b.OwnsOne(e => e.Currency, bc =>
                        {
                            bc.Property(e => e.Name).HasColumnName("BaseCurrency").HasMaxLength(5);
                        });
                    });

                    builder.OwnsOne(e => e.ProductExchangePrice, b =>
                    {
                        b.Property(e => e.Value).HasColumnName("ExchangePrice").HasColumnType("decimal(5,2)");
                        b.OwnsOne(e => e.Currency, bc =>
                        {
                            bc.Property(e => e.Name).HasColumnName("ExchangeCurrency").HasMaxLength(5);
                        });
                    });
                });
            });
        }
    }
}