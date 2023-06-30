using Dinner.Domain.Common.Models;
using Dinner.Domain.Host.ValueObjects;
using Dinner.Domain.Menu.Entities;
using Dinner.Domain.Menu.ValueObjects;
using Dinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Domain.Menu;

public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();

    public string Name { get; }
    public string Description { get; }
    public float AverageRating { get; }
    public HostId HostId { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    public ImmutableList<MenuSection> Sections => _sections.ToImmutableList();
    public ImmutableList<DinnerId> DinnerIds => _dinnerIds.ToImmutableList();
    public ImmutableList<MenuReviewId> MenuReviewIds => _menuReviewIds.ToImmutableList();

    private Menu(MenuId menuId,
        string name,
        string description,
        HostId hostId,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(menuId)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Menu Create(
        string name, 
        string description,
        HostId hostId)
    {
        return new(
            MenuId.CreateUnique(),
            name,
            description,
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
