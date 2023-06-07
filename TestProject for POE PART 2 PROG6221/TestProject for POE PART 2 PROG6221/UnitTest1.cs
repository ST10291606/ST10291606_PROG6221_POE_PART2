
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using static RecipeApp.Program;

[TestClass]
public class RecipeTests
{
    [TestMethod]
    public void CalculateTotalCalories_NoIngredients_ReturnsZero()
    {
        // Arrange
        Recipe recipe = new Recipe("Test Recipe");

        // Act
        double totalCalories = recipe.CalculateTotalCalories();

        // Assert
        Assert.AreEqual(0, totalCalories, "Total calories should be zero when there are no ingredients.");
    }

    [TestMethod]
    public void CalculateTotalCalories_SingleIngredient_ReturnsCorrectTotal()
    {
        // Arrange
        Recipe recipe = new Recipe("Test Recipe");
        Ingredient ingredient = new Ingredient("Ingredient 1", 100, "g", 150, FoodGroup.Protein);
        recipe.AddIngredient(ingredient);

        // Act
        double totalCalories = recipe.CalculateTotalCalories();

        // Assert
        Assert.AreEqual(150, totalCalories, "Total calories should be equal to the calories of the single ingredient.");
    }

    [TestMethod]
    public void CalculateTotalCalories_MultipleIngredients_ReturnsCorrectTotal()
    {
        // Arrange
        Recipe recipe = new Recipe("Test Recipe");
        Ingredient ingredient1 = new Ingredient("Ingredient 1", 100, "g", 150, FoodGroup.Protein);
        Ingredient ingredient2 = new Ingredient("Ingredient 2", 50, "g", 75, FoodGroup.Carbohydrate);
        Ingredient ingredient3 = new Ingredient("Ingredient 3", 200, "g", 300, FoodGroup.Fat);
        recipe.AddIngredient(ingredient1);
        recipe.AddIngredient(ingredient2);
        recipe.AddIngredient(ingredient3);

        // Act
        double totalCalories = recipe.CalculateTotalCalories();

        // Assert
        Assert.AreEqual(525, totalCalories, "Total calories should be equal to the sum of the calories of all ingredients.");
    }

    [TestMethod]
    public void CalculateTotalCalories_EmptyIngredients_ReturnsZero()
    {
        // Arrange
        Recipe recipe = new Recipe("Test Recipe");

        // Act
        double totalCalories = recipe.CalculateTotalCalories();

        // Assert
        Assert.AreEqual(0, totalCalories, "Total calories should be zero when the ingredients list is empty.");
    }
}
