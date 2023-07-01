using Dinner.Domain.Common.Models;
using Dinner.Domain.Guest.ValueObjects;
using Dinner.Domain.Host.ValueObjects;
using Dinner.Domain.MenuReview.ValueObjects;
using Dinner.Domain.User.ValueObjects;
using System.Collections.Immutable;

namespace Dinner.Domain.Guest;

public sealed class Guest : AggregateRoot<GuestId>
{
    private readonly List<DinnerId> _upcomingDinnerIds = new();
    private readonly List<DinnerId> _pastDinnerIds = new();
    private readonly List<DinnerId> _pendingDinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();

    private Guest(
        GuestId id,
        string firstName,
        string lastName,
        string profileImage,
        float averageRating,
        UserId userId,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfileImage = profileImage;
        AverageRating = averageRating;
        UserId = userId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string ProfileImage { get; private set; }
    public float AverageRating { get; private set; }
    public UserId UserId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public ImmutableList<DinnerId> UpcomingDinnerIds => _upcomingDinnerIds.ToImmutableList();
    public ImmutableList<DinnerId> PastDinnerIds => _pastDinnerIds.ToImmutableList();
    public ImmutableList<DinnerId> PendingDinnerIds => _pendingDinnerIds.ToImmutableList();
    public ImmutableList<MenuReviewId> MenuReviewIds => _menuReviewIds.ToImmutableList();

    public static Guest Create(
        string firstName,
        string lastName,
        string profileImage,
        float averageRating,
        UserId userId)
    {
        return new Guest(
            GuestId.CreateUnique(),
            firstName,
            lastName,
            profileImage,
            averageRating,
            userId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
