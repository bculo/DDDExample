using Dinner.Domain.Common.Models;
using Dinner.Domain.Host.ValueObjects;
using Dinner.Domain.Menu.ValueObjects;
using Dinner.Domain.MenuReview.ValueObjects;
using Dinner.Domain.User.ValueObjects;

namespace Dinner.Domain.MenuReview;

public sealed class MenuReview : AggregateRoot<MenuReviewId>
{
    public MenuReview(
        MenuReviewId id,
        float rating,
        string comment,
        HostId hostId,
        UserId gostId,
        MenuId menuId,
        DinnerId dinnerid,
        DateTime created,
        DateTime updated)
        : base(id)
    {
        Rating = rating;
        Comment = comment;
        HostId = hostId;
        GostId = gostId;
        MenuId = menuId;
        Dinnerid = dinnerid;
        CreatedDateTime = created;
        UpdatedDateTime = updated;
    }

    public float Rating { get; private set; }
    public string Comment { get; private set; }
    public HostId HostId { get; private set; }  
    public UserId GostId { get; private set; }
    public MenuId MenuId { get; private set; }
    public DinnerId Dinnerid { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static MenuReview Create(float rating,
        string comment,
        HostId hostid,
        UserId guestId,
        MenuId menuId,
        DinnerId dinnerId)
    {
        return new MenuReview(MenuReviewId.CreateUnique(),
            rating,
            comment,
            hostid,
            guestId,
            menuId,
            dinnerId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
