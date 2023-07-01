using Dinner.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Domain.Common.ValueObjects;

public sealed class AverageRating : ValueObject
{
    private AverageRating(double rating, int numRatings)
    {
        Value = rating;
        NumRatings = numRatings;
    }

    private AverageRating() { }

    public double Value { get; private set; }
    public double NumRatings { get; private set; }


    public static AverageRating CreateNew(double rating = 0, int numRatings = 0)
    {
        return new AverageRating(rating, numRatings);
    }

    public void AddNewRating(float ratingValue)
    {
        Value = ((Value * NumRatings) + ratingValue) / ++NumRatings;
    }

    public void RemoveRating(float ratingValue)
    {
        Value = ((Value * NumRatings) - ratingValue) / --NumRatings;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value; 
        yield return NumRatings;
    }
}
