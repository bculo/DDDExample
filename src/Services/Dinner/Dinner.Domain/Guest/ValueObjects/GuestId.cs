using Dinner.Domain.Common.Models;

namespace Dinner.Domain.Guest.ValueObjects;

public sealed class GuestId : ValueObject
{
    public Guid Value { get; }

    private GuestId(Guid value)
    {
        Value = value;
    }

    public static GuestId CreateUnique() => new GuestId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
