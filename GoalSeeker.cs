using System;

// This class contains an extension method for finding the root of a mathematical function.
public static class MathExtensions
{
    // This method uses the Newton-Raphson method to find the root of a function.
    // It takes in a function, an initial guess for the root, and an epsilon value to determine the accuracy of the answer.
    // It returns the root of the function.
    public static double FindRoot(Func<double, double> func, double x0, double x1, double epsilon)
    {
        // This loop continues until the difference between x1 and x0 is less than epsilon.
        while (Math.Abs(x1 - x0) > epsilon)
        {
            // Calculate the function at x0 and x1.
            double fx0 = func(x0);
            double fx1 = func(x1);
            
            // Calculate the next guess for the root using the Newton-Raphson method.
            double x2 = x0 - fx0 * (x1 - x0) / (fx1 - fx0);
            
            // Update x0 and x1 with the new guess.
            x0 = x1;
            x1 = x2;
        }
        
        // Return the final guess for the root.
        return x1;
    }
}

// This class contains methods for optimizing a campaign budget.
public static class GoalSeeker
{
    // This method calculates the total budget for a set of ads, given their budgets, agency fees, third-party tool fees, and fixed agency hours cost.
    // It takes in an array of ad budgets, agency fee percentage, third-party tool fee percentage, and fixed agency hours cost.
    // It returns the total budget.
    public static double CalculateTotalBudget(double[] adBudgets, double agencyFeePercentage, double thirdPartyToolFeePercentage, double fixedAgencyHoursCost)
    {
        // Calculate the total ad spend by summing the budgets of all ads.
        double totalAdSpend = adBudgets.Sum();
        
        // Calculate the agency fee by multiplying the total ad spend by the agency fee percentage.
        double agencyFee = totalAdSpend * agencyFeePercentage;
        
        // Calculate the third-party tool fee by multiplying the total ad spend by the third-party tool fee percentage.
        double thirdPartyToolFee = totalAdSpend * thirdPartyToolFeePercentage;
        
        // Return the total budget by adding the total ad spend, agency fee, third-party tool fee, and fixed agency hours cost.
        return totalAdSpend + agencyFee + thirdPartyToolFee + fixedAgencyHoursCost;
    }

    // This method finds the optimal budget for a specific ad by using the MathExtensions.FindRoot method to find the root of the objective function.
    // It takes in an array of ad budgets, the index of the target ad, the total campaign budget, agency fee percentage, third-party tool fee percentage, and fixed agency hours cost.
    // It returns the optimal budget for the target ad.
    public static double FindOptimalBudget(double[] adBudgets, int targetAdIndex, double totalBudget, double agencyFeePercentage, double thirdPartyToolFeePercentage, double fixedAgencyHoursCost)
    {
        // Set the initial guess for the target ad's budget to its current value.
        double initialGuess = adBudgets[targetAdIndex];
        
        // Define the objective function as the difference between the calculated total budget and the total campaign budget.
        Func<double, double> objectiveFunction = adBudget =>
        {
            // Update the target ad's budget in the array of ad budgets.
            adBudgets[targetAdIndex] = adBudget;
            
            // Calculate the total budget using the CalculateTotalBudget method.
            double calculatedTotalBudget = CalculateTotalBudget(adBudgets, agencyFeePercentage, thirdPartyToolFeePercentage, fixedAgencyHoursCost);
            
            // Return the difference between the calculated total budget and the total campaign budget.
            return calculatedTotalBudget - totalBudget;
        };
        
        // Use the MathExtensions.FindRoot method to find the root of the objective function.
        double optimalBudget = MathExtensions.FindRoot(objectiveFunction, 0, totalBudget, 1e-6);
        
        // Return the optimal budget for the target ad.
        return optimalBudget;
    }
}
