using System;
using System.Linq;
using Spectre.Console;

// This is the entry point of the console application.
class Program
{
    // The Main method is the starting point of the application.
    static void Main(string[] args)
    {
        // Start an infinite loop to allow for multiple calculations.
        do
        {
            try
            {
                Console.Clear(); // Clear the console screen at the start.

                // Display the application title using ANSI escape sequences for colors and formatting.
                AnsiConsole.MarkupLine("[blue]           ______  _____  _______                        [/]");
                AnsiConsole.MarkupLine("[blue]          |  ____ |     | |_____| |                       [/]");
                AnsiConsole.MarkupLine("[blue]          |_____| |_____| |     | |_____                  [/]");
                AnsiConsole.MarkupLine("[blue]_______ _______ _______ _     _ _______  ______[/]");
                AnsiConsole.MarkupLine("[blue]|______ |______ |______ |____/  |______ |_____/[/]");
                AnsiConsole.MarkupLine("[blue]______| |______ |______ |    \\_ |______ |     \\[/]\n");
                AnsiConsole.MarkupLine("[bold yellow]   ---- Campaign Budget Optimization ----[/]\n");

                // Prompt the user to enter the total campaign budget.
                double totalBudget = -1;
                while (totalBudget <= 0)
                {
                    totalBudget = AnsiConsole.Ask<double>("Enter the total campaign budget ([green]Z[/]):");
                    if (totalBudget <= 0)
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input. Please enter a positive total campaign budget.[/]");
                    }
                }

                // Prompt the user to enter the agency fee percentage.
                double agencyFeePercentage = -1;
                while (agencyFeePercentage < 0 || agencyFeePercentage > 1)
                {
                    agencyFeePercentage = AnsiConsole.Ask<double>("Enter the agency fee percentage ([green]Y1[/]) (as a decimal, e.g., 0.1 for 10%):");
                    if (agencyFeePercentage < 0 || agencyFeePercentage > 1)
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input. Please enter a percentage between 0.0 and 1.0.[/]");
                    }
                }

                // Prompt the user to enter the third-party tool fee percentage.
                double thirdPartyToolFeePercentage = -1;
                while (thirdPartyToolFeePercentage < 0 || thirdPartyToolFeePercentage > 1)
                {
                    thirdPartyToolFeePercentage = AnsiConsole.Ask<double>("Enter the third-party tool fee percentage ([green]Y2[/]) (as a decimal, e.g., 0.05 for 5%):");
                    if (thirdPartyToolFeePercentage < 0 || thirdPartyToolFeePercentage > 1)
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input. Please enter a percentage between 0.0 and 1.0.[/]");
                    }
                }

                // Prompt the user to enter the fixed cost for agency hours.
                double fixedAgencyHoursCost = -1;
                while (fixedAgencyHoursCost <= 0)
                {
                    fixedAgencyHoursCost = AnsiConsole.Ask<double>("Enter the fixed cost for agency hours (in €, e.g. 500):");
                    if (fixedAgencyHoursCost <= 0)
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input. Please enter a positive fixed cost for agency hours.[/]");
                    }
                }

                // Prompt the user to enter the budgets for each ad.
                double[] budgets = null;
                while (budgets == null)
                {
                    try
                    {
                        string budgetsInput = AnsiConsole.Ask<string>("Enter the budgets for each ad ([green]comma-separated[/]):");
                        budgets = budgetsInput.Split(',').Select(double.Parse).ToArray();
                    }
                    catch (Exception)
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input. Please enter the budgets as comma-separated values.[/]");
                    }
                }

                // Validate that the budgets array is not null.
                if (budgets == null)
                {
                    throw new InvalidOperationException("Budgets cannot be null.");
                }

                // Prompt the user to enter the index of the ad to optimize.
                int targetAdIndex = -1;
                while (targetAdIndex < 0 || targetAdIndex >= budgets.Length)
                {
                    targetAdIndex = AnsiConsole.Ask<int>("Enter the index of the ad to optimize ([green]0-based[/]):");
                    if (targetAdIndex < 0 || targetAdIndex >= budgets.Length)
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid index. Please enter an index within the range of the ad budgets.[/]");
                    }
                }

                // Find the optimal budget for the specific ad.
                double optimalBudget = GoalSeeker.FindOptimalBudget(budgets, targetAdIndex, totalBudget, agencyFeePercentage, thirdPartyToolFeePercentage, fixedAgencyHoursCost);
                AnsiConsole.MarkupLine($"\nThe optimal budget for the specific ad is: [bold green]€{optimalBudget:F2}[/]");
                AnsiConsole.MarkupLine("\n[bold yellow]Thank you for using this optimization tool.![/]");
                AnsiConsole.MarkupLine("[bold]Press [green]<Spacebar>[/] for a new calculation or [red]<Esc>[/] to exit.[/]");

                // Wait for the user to press a key.
                while (true)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Spacebar)
                    {
                        break; // Break the inner loop and start a new calculation.
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        Console.Clear(); // Clear the console screen before exiting.
                        return; // Exit the application.
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs.
                AnsiConsole.MarkupLine($"[bold red]Error:[/] {ex.Message}");
                AnsiConsole.MarkupLine("[bold]Press any key to restart the tool.[/]");
                Console.ReadKey(true);
            }
        } while (true);
    }
}
