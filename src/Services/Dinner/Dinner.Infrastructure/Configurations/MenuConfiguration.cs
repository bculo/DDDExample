using Dinner.Domain.Host.ValueObjects;
using Dinner.Domain.Menu;
using Dinner.Domain.Menu.Entities;
using Dinner.Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Infrastructure.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTabe(builder);
        ConfigureMenuSectionsTable(builder);
        ConfigureMenuDinnerIdsTable(builder);
        ConfigureMenuReviewsIdsTable(builder);
    }

    private void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(x => x.DinnerIds, dib =>
        {
            dib.ToTable("MenuDinnersIds");

            dib.WithOwner().HasForeignKey("MenuId");
            dib.HasKey("Id"); //Creates shadow property that will be populated by DB

            dib.Property(d => d.Value)
                .HasColumnName("DinnerId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuReviewsIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(x => x.MenuReviewIds, dib =>
        {
            dib.ToTable("MenuReviewsIds");

            dib.WithOwner().HasForeignKey("MenuId");
            dib.HasKey("Id"); //Creates shadow property that will be populated by DB

            dib.Property(d => d.Value)
                .HasColumnName("ReviewId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.Sections, sb =>
        {
            sb.ToTable("MenuSections");

            sb.WithOwner().HasForeignKey("MenuId");

            sb.Property(x => x.Id)
                .HasColumnName("MenuSectionId")
                .ValueGeneratedNever()
                .HasConversion(
                    x => x.Value,
                    y => MenuSectionId.Create(y));

            sb.Property(x => x.Name)
                .HasMaxLength(100);

            sb.Property(x => x.Description)
                .HasMaxLength(100);

            sb.HasKey("Id", "MenuId");

            sb.OwnsMany(s => s.Items, ib =>
            {
                ib.ToTable("MenuItems");

                ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

                ib.Property(x => x.Id)
                    .HasColumnName("MenuItemId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        x => x.Value,
                        y => MenuItemId.Create(y));

                ib.Property(x => x.Name)
                    .HasMaxLength(100);

                ib.Property(x => x.Description)
                    .HasMaxLength(100);

                ib.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");
            });

            sb.Navigation(i => i.Items).Metadata.SetField("_items");
            sb.Navigation(i => i.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata.FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenusTabe(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                x => x.Value, 
                y => MenuId.Create(y));

        builder.Property(x => x.Name)
            .HasMaxLength(100);
        builder.Property(x => x.Description)
            .HasMaxLength(100);

        builder.OwnsOne(m => m.AverageRating, ab =>
        {
            //ab.Property(a => a.Value).HasColumnName("Name");
        });

        builder.Property(x => x.HostId)
            .ValueGeneratedNever()
            .HasConversion(
                x => x.Value,
                y => HostId.Create(y));
    }
}
