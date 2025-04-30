﻿using Domain.Entities.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress,address => address.WithOwner());

            builder.HasMany(o=>o.OrderItems)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(o=>o.PaymentStatus)
                   .HasConversion(s=>s.ToString(),s=>Enum.Parse<OrderPaymentStatus>(s));

            builder.Property(o=>o.SubTotal)
                   .HasColumnType("decimal(18,4)");
        }
    }
}
