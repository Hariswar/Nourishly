using DiningAPI.Models;

namespace DiningAPI.Data;

// Adds sample locations, menus and items when the database is empty
public static class DbSeeder
{
    public static void Seed(DiningContext db)
    {
        if (db.Locations.Any()) return;

        var northHall = new Location { Name = "North Dining Hall", Address = "100 Campus Dr" };
        var foodCourt = new Location { Name = "Student Center Food Court", Address = "200 Student Center Way" };
        var cafe = new Location { Name = "Library Cafe", Address = "300 Library Ln" };

        var breakfast = new Menu { Name = "Breakfast", Description = "Served 7am - 10am", Locations = { northHall, cafe } };
        var lunch = new Menu { Name = "Lunch", Description = "Served 11am - 2pm", Locations = { northHall, foodCourt } };
        var dinner = new Menu { Name = "Dinner", Description = "Served 5pm - 8pm", Locations = { northHall, foodCourt } };

        MenuItem Item(string name, string description, decimal price, int calories, decimal protein, decimal fat, decimal carbs, params Menu[] menus)
        {
            var item = new MenuItem { Name = name, Description = description, Price = price };
            item.Nutritions.Add(new Nutrition { Calories = calories, Protein = protein, Fat = fat, Carbs = carbs });
            item.Menus.AddRange(menus);
            return item;
        }

        db.MenuItems.AddRange(
            Item("Scrambled Eggs", "Two eggs scrambled with butter", 3.50m, 200, 13, 15, 2, breakfast),
            Item("Oatmeal Bowl", "Rolled oats with berries and honey", 3.00m, 250, 7, 4, 45, breakfast),
            Item("Greek Yogurt Parfait", "Yogurt layered with granola and fruit", 4.25m, 280, 15, 6, 40, breakfast),
            Item("Grilled Chicken Sandwich", "Chicken breast on a whole wheat bun", 7.50m, 450, 38, 12, 42, lunch, dinner),
            Item("Garden Salad", "Mixed greens, tomato, cucumber, vinaigrette", 5.00m, 150, 4, 9, 14, lunch, dinner),
            Item("Veggie Burrito Bowl", "Rice, black beans, corn, salsa, guacamole", 7.25m, 620, 18, 20, 90, lunch),
            Item("Cheese Pizza Slice", "Hand-tossed crust with mozzarella", 3.25m, 285, 12, 10, 36, lunch, dinner),
            Item("Salmon with Rice", "Baked salmon, jasmine rice, steamed broccoli", 9.50m, 560, 40, 18, 55, dinner),
            Item("Spaghetti Marinara", "Pasta with tomato basil sauce", 6.50m, 480, 16, 8, 85, dinner)
        );
        db.SaveChanges();
    }
}
