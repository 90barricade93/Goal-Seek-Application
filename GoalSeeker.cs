using System;

public static class MathExtensions
{
    public static double FindRoot(Func<double, double> func, double x0, double x1, double epsilon)
    {
        while (Math.Abs(x1 - x0) > epsilon)
        {
            double fx0 = func(x0);
            double fx1 = func(x1);
            double x2 = x0 - fx0 * (x1 - x0) / (fx1 - fx0);
            x0 = x1;
            x1 = x2;
        }
        return x1;
    }
}

public static class GoalSeeker
{
    public static double CalculateTotalBudget(double[] adBudgets, double agencyFeePercentage, double thirdPartyToolFeePercentage, double fixedAgencyHoursCost)
    {
        double totalAdSpend = 0;
        foreach (var adBudget in adBudgets)
        {
            totalAdSpend += adBudget;
        }

        double agencyFee = totalAdSpend * agencyFeePercentage;
        double thirdPartyToolFee = totalAdSpend * thirdPartyToolFeePercentage;

        return totalAdSpend + agencyFee + thirdPartyToolFee + fixedAgencyHoursCost;
    }

    public static double FindOptimalBudget(double[] adBudgets, int targetAdIndex, double totalBudget, double agencyFeePercentage, double thirdPartyToolFeePercentage, double fixedAgencyHoursCost)
    {
        double initialGuess = adBudgets[targetAdIndex];

        Func<double, double> objectiveFunction = adBudget =>
        {
            adBudgets[targetAdIndex] = adBudget;
            double calculatedTotalBudget = CalculateTotalBudget(adBudgets, agencyFeePercentage, thirdPartyToolFeePercentage, fixedAgencyHoursCost);
            return calculatedTotalBudget - totalBudget;
        };

        double optimalBudget = MathExtensions.FindRoot(objectiveFunction, 0, totalBudget, 1e-6);
        return optimalBudget;
    }
}
