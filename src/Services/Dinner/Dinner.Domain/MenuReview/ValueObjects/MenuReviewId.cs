using Dinner.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Domain.MenuReview.ValueObjects;

public sealed class MenuReviewId : ValueObject
{
    public Guid Value { get; }

    private MenuReviewId(Guid value)
    {
        Value = value;
    }

    private MenuReviewId() { }

    public static MenuReviewId CreateUnique() => new MenuReviewId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
