using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Enter the total campaign budget (Z): ");
            double totalBudget = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter the agency fee percentage (Y1) (as a decimal, e.g., 0.1 for 10%): ");
            double agencyFeePercentage = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter the third-party tool fee percentage (Y2) (as a decimal, e.g., 0.05 for 5%): ");
            double thirdPartyToolFeePercentage = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter the fixed cost for agency hours: ");
            double fixedAgencyHoursCost = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter the budgets for each ad (comma-separated): ");
            string[] budgetStrings = Console.ReadLine().Split(',');
            double[] budgets = Array.ConvertAll(budgetStrings, double.Parse);

            Console.Write("Enter the index of the ad to optimize (0-based): ");
            int targetAdIndex = Convert.ToInt32(Console.ReadLine());

            double optimalBudget = GoalSeeker.FindOptimalBudget(budgets, targetAdIndex, totalBudget, agencyFeePercentage, thirdPartyToolFeePercentage, fixedAgencyHoursCost);
            Console.WriteLine($"The optimal budget for the specific ad is: €{optimalBudget:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
