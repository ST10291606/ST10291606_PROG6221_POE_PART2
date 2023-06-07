using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    public class Program
    {
        private static List<Recipe> recipes = new List<Recipe>(); // List to store recipes

        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue; // Changes the colour of the text background in the application (w3schools.com)
            Console.ForegroundColor = ConsoleColor.Black; // Changes the colour of the text presented to the user (w3schools.com)

        repeat: // label needed for the goto statement to operate correctly. The application restarts from this point when the goto statement is read. (programiz.com)

            Console.WriteLine("\n*************************************");
            Console.WriteLine("Welcome to my recipe application");
            Console.WriteLine("*************************************");

            Console.WriteLine("\nChoose an option number:");
            Console.WriteLine("(1) Add a recipe");
            Console.WriteLine("(2) Display recipes");
            Console.WriteLine("(3) Scale a recipe");
            Console.WriteLine("(4) Reset ingredient amounts in a recipe");
            Console.WriteLine("(5) Clear a recipe");
            Console.WriteLine("(6) Exit the application");

            string option = Console.ReadLine(); // Read user input for selected option

            bool exit = false;

            while (!exit)
            {
                try
                {
                    string option = Console.ReadLine(); // Read user input for selected option

                    switch (option)
                    {
                        case "1":
                            AddRecipe(); // Call method to add a recipe
                            goto repeat;
                        case "2":
                            DisplayRecipes(); // Call method to display recipes
                            goto repeat;
                        case "3":
                            ScaleRecipe(); // Call method to scale a recipe
                            goto repeat;
                        case "4":
                            ResetRecipeAmounts(); // Call method to reset recipe amounts
                            goto repeat;
                        case "5":
                            ClearRecipe(); // Call method to clear a recipe
                            goto repeat;
                        case "6":
                            Console.WriteLine("Exiting the application...");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            goto repeat;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nAn error occurred: " + ex.Message);
                }
            }
        }

        private static void AddRecipe()
        {
            Console.WriteLine("\nEnter the recipe name:");
            string name = Console.ReadLine(); // Read user input for recipe name

            Recipe recipe = new Recipe(name); // Create a new recipe object


            Console.WriteLine("\nHow many ingredients does the recipe have?");
            int numofIngredients = Convert.ToInt32(Console.ReadLine()); // Read user input for the number of ingredients


            for (int i = 0; i < numofIngredients; i++)
            {
                Console.WriteLine($"\nIngredient {i + 1}:");
                Console.WriteLine("Enter the name:");
                string ingredientName = Console.ReadLine(); // Read user input for ingredient name

                Console.WriteLine("Enter the quantity:");
                double quantity = Convert.ToDouble(Console.ReadLine()); // Read user input for ingredient quantity


                Console.WriteLine("Enter the unit of measurement:");
                string unitofMeasurement = Console.ReadLine(); // Read user input for unit of measurement


                Console.WriteLine("Enter the number of calories:");
                double calories = Convert.ToDouble(Console.ReadLine()); // Read user input for number of calories


                Console.WriteLine("Enter the food group (1 - Protein, 2 - Carbohydrate, 3 - Fat):");
                int foodGroupOption = Convert.ToInt32(Console.ReadLine()); // Read user input for food group option

                FoodGroup foodGroup = GetFoodGroup(foodGroupOption);  // Get the corresponding FoodGroup based on the user's input

                Ingredient ingredient = new Ingredient(ingredientName, quantity, unitofMeasurement, calories, foodGroup);
                recipe.AddIngredient(ingredient); // Add the ingredient to the recipe
            }

            Console.WriteLine("\nHow many steps are involved in making the recipe?");
            int numofSteps = Convert.ToInt32(Console.ReadLine()); // Read user input for the number of steps

            for (int i = 0; i < numofSteps; i++)
            {
                Console.WriteLine($"\nStep {i + 1}:");
                string step = Console.ReadLine(); // Read user input for each step
                recipe.AddStep(step); // Add the step to the recipe
            }

            recipes.Add(recipe); // Add the recipe to the list of recipes
            Console.WriteLine("\nRecipe added successfully!");
        }

        private static void DisplayRecipes()
        {
            // Check if the recipes list is empty
            if (recipes.Count == 0)
            {
                // If no recipes found, print a message and return
                Console.WriteLine("\nNo recipes found.");
                return;
            }

            // Sort the recipes list by name
            recipes = recipes.OrderBy(r => r.Name).ToList();

            // Set console colors for the header
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Blue;

            // Print the header
            Console.WriteLine("Here are the recipes:");

            // Iterate over each recipe in the list
            for (int i = 0; i < recipes.Count; i++)
            {
                // Print the recipe number and name
                Console.WriteLine($"({i + 1}) {recipes[i].Name}");
            }

            // Prompt the user to enter a recipe number or '0' to go back
            Console.WriteLine("\nEnter the recipe number to display details (or '0' to go back):");

            // Read the user's input and convert it to an integer
            int recipeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            // Check if the recipe index is valid
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                // Get the selected recipe from the list
                Recipe recipe = recipes[recipeIndex];

                // Display the details of the selected recipe
                DisplayRecipeDetails(recipe);
            }
            else if (recipeIndex == -1)
            {
                // If the user entered '0', print a message and go back to the main menu
                Console.WriteLine("\nGoing back to the main menu...");
            }
            else
            {
                // If the user entered an invalid recipe number, print an error message
                Console.WriteLine("\nInvalid recipe number.");
            }
        }


        private static void DisplayRecipeDetails(Recipe recipe)
        {
            // Set console colors for the header
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            // Print the recipe name
            Console.WriteLine($"\nRecipe: {recipe.Name}");

            // Print the list of ingredients
            Console.WriteLine("\nIngredients:");
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                // Get the current ingredient
                Ingredient ingredient = recipe.Ingredients[i];

                // Print the ingredient details
                Console.WriteLine($"Ingredient {i + 1}: {ingredient.Name}, Quantity: {ingredient.Quantity} {ingredient.UnitofMeasurement}, Calories: {ingredient.Calories}, Food Group: {GetFoodGroupDescription(ingredient.FoodGroup)}");
            }

            // Print the list of steps
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                // Print each step
                Console.WriteLine($"Step {i + 1}: {recipe.Steps[i]}");
            }

            // Calculate and print the total calories of the recipe
            double totalCalories = recipe.CalculateTotalCalories();
            Console.WriteLine($"\nTotal Calories: {totalCalories}");

            // Check if the total calories exceed 300
            if (totalCalories > 300)
            {
                // Print an alert message if the recipe exceeds 300 calories
                Console.WriteLine("\n*** Alert: This recipe exceeds 300 calories! ***");

                // Notify the user about exceeding calories
                NotifyUserExceedsCalories(totalCalories);
            }
        }


        private static void NotifyUserExceedsCalories(double totalCalories)
        {
            // Delegate to notify the user about calorie exceedance
            Action<double> calorieExceedsNotification = (calories) =>
            {
                Console.WriteLine($"*** ALERT: This recipe has {calories} calories, which exceeds the recommended limit! ***");
                Console.WriteLine("***Please consider adjusting the ingredients or portion size.***\n");
            };

            // Invoke the delegate
            calorieExceedsNotification(totalCalories);
        }

        private static FoodGroup GetFoodGroup(int option)
        {
            // Switch statement to determine the FoodGroup based on the option value
            switch (option)
            {
                case 1:
                    // If option is 1, return FoodGroup.Protein
                    return FoodGroup.Protein;
                case 2:
                    // If option is 2, return FoodGroup.Carbohydrate
                    return FoodGroup.Carbohydrate;
                case 3:
                    // If option is 3, return FoodGroup.Fat
                    return FoodGroup.Fat;
                default:
                    // For any other option value, return FoodGroup.Unknown
                    return FoodGroup.Unknown;
            }
        }


        private static string GetFoodGroupDescription(FoodGroup foodGroup)
        {
            switch (foodGroup)
            {
                case FoodGroup.Protein:
                    return "Protein";
                case FoodGroup.Carbohydrate:
                    return "Carbohydrate";
                case FoodGroup.Fat:
                    return "Fat";
                default:
                    return "Unknown";
            }
        }

        private static void ScaleRecipe()
        {
            // Prompt the user to enter the recipe number to scale or '0' to go back
            Console.WriteLine("\nEnter the recipe number to scale (or '0' to go back):");

            // Read the user's input and convert it to an integer
            int recipeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            // Check if the recipe index is valid
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                // Get the selected recipe from the list
                Recipe recipe = recipes[recipeIndex];

                // Prompt the user to enter the scaling factor
                Console.WriteLine("Enter the scaling factor (e.g., 2 to double the recipe, 0.5 to halve the recipe):");

                // Read the user's input and convert it to a double
                double scalingFactor = Convert.ToDouble(Console.ReadLine());

                // Scale the recipe using the scaling factor
                recipe.Scale(scalingFactor);

                // Print a success message
                Console.WriteLine("\nRecipe scaled successfully!");
            }
            else if (recipeIndex == -1)
            {
                // If the user entered '0', print a message and go back to the main menu
                Console.WriteLine("\nGoing back to the main menu...");
            }
            else
            {
                // If the user entered an invalid recipe number, print an error message
                Console.WriteLine("\nInvalid recipe number.");
            }
        }

        private static void ResetRecipeAmounts()
        {
            // Prompt the user to enter the recipe number to reset ingredient amounts or '0' to go back
            Console.WriteLine("\nEnter the recipe number to reset ingredient amounts (or '0' to go back):");

            // Read the user's input and convert it to an integer
            int recipeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            // Check if the recipe index is valid
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                // Get the selected recipe from the list
                Recipe recipe = recipes[recipeIndex];

                // Reset the ingredient amounts in the recipe
                recipe.ResetAmounts();

                // Print a success message
                Console.WriteLine("\nIngredient amounts in the recipe reset successfully!");
            }
            else if (recipeIndex == -1)
            {
                // If the user entered '0', print a message and go back to the main menu
                Console.WriteLine("\nGoing back to the main menu...");
            }
            else
            {
                // If the user entered an invalid recipe number, print an error message
                Console.WriteLine("\nInvalid recipe number.");
            }
        }

        private static void ClearRecipe()
        {
            // Prompt the user to enter the recipe number to clear or '0' to go back
            Console.WriteLine("\nEnter the recipe number to clear (or '0' to go back):");

            // Read the user's input and convert it to an integer
            int recipeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            // Check if the recipe index is valid
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                // Get the selected recipe from the list
                Recipe recipe = recipes[recipeIndex];

                // Remove the recipe from the list
                recipes.Remove(recipe);

                // Print a success message
                Console.WriteLine("\nRecipe cleared successfully!");
            }
            else if (recipeIndex == -1)
            {
                // If the user entered '0', print a message and go back to the main menu
                Console.WriteLine("\nGoing back to the main menu...");
            }
            else
            {
                // If the user entered an invalid recipe number, print an error message
                Console.WriteLine("\nInvalid recipe number.");
            }
        }


        public class Recipe
        {
            public string Name { get; set; }
            public List<Ingredient> Ingredients { get; set; }
            public List<string> Steps { get; set; }

            public Recipe(string name)
            {
                Name = name;
                Ingredients = new List<Ingredient>();
                Steps = new List<string>();
            }

            public void AddIngredient(Ingredient ingredient)
            {
                Ingredients.Add(ingredient);
            }

            public void AddStep(string step)
            {
                Steps.Add(step);
            }

            public double CalculateTotalCalories()
            {
                double totalCalories = 0;

                foreach (Ingredient ingredient in Ingredients)
                {
                    totalCalories += ingredient.Calories;
                }

                return totalCalories;
            }

            public void Scale(double scalingFactor)
            {
                foreach (Ingredient ingredient in Ingredients)
                {
                    ingredient.Quantity *= scalingFactor;
                    ingredient.Calories *= scalingFactor;
                }
            }

            public void ResetAmounts()
            {
                foreach (Ingredient ingredient in Ingredients)
                {
                    ingredient.ResetQuantity();
                }
            }
        }

        public class Ingredient
        {
            public string Name { get; set; }
            public double Quantity { get; set; }
            public string UnitofMeasurement { get; set; }
            public double Calories { get; set; }
            public FoodGroup FoodGroup { get; set; }
            private double originalQuantity; // Stores the original quantity of the ingredient
            private double originalCalories;
            public Ingredient(string name, double quantity, string unitofMeasurement, double calories, FoodGroup foodGroup)
            {
                Name = name;
                Quantity = quantity;
                UnitofMeasurement = unitofMeasurement;
                Calories = calories;
                FoodGroup = foodGroup;
                originalQuantity = quantity; // Set the original quantity
                originalCalories = calories;
            }

            public void ResetQuantity()
            {
                Quantity = originalQuantity; // Reset the quantity to the original value
                Calories = originalCalories;

            }
        }

        public enum FoodGroup //Enums are used to define a set of named constants
        {
            Protein,
            Carbohydrate,
            Fat,
            Unknown
        }
    }
}

