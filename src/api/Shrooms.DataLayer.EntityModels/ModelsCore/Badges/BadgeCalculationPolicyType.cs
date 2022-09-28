namespace Shrooms.DataLayer.EntityModels.ModelsCore.Badges
{
    public enum BadgeCalculationPolicyType
    {
        PointsStrategy = 1, // SUM(multiplier * kudos count)

        MultiplierStrategy = 2 // SUM(multiplier)
    }
}