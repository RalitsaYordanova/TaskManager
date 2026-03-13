    Console.WriteLine("4. Delete task");
    Console.WriteLine("5. Exit");
    Console.WriteLine("────────────────────────────");

    Console.Write("Choice: ");
    var choice = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(choice))
    {
        Console.WriteLine("Please enter a valid option.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        continue;
    }


    else if (choice == "2")
    {
        Console.Clear();

        Console.WriteLine("===== TASK LIST =====\n");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
        }
        else
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Id}. [{(task.IsCompleted ? "✔" : " ")}] {task.Title}");
            }
            Console.ReadKey();
        }
    }

    else if (choice == "4")
    {
        Console.Write("Task ID to delete: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Task deleted.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        Console.WriteLine("\n─────────────────────");
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }
        Console.Clear();
        Console.WriteLine("===== TASK MANAGER =====\n");
        if (tasks.Count == 0)
            Console.WriteLine("No tasks available.");
        else
            foreach (var task in tasks)
                Console.WriteLine($"{task.Id}. [{(task.IsCompleted ? "X" : " ")}] {task.Title}");
        Console.WriteLine("\n=======================\n");
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }
        Console.Write("Task ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Task deleted.");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Task not found. Press any key...");
                Console.ReadKey();
            }
        }
    }
    else if (choice == "5")
    {
        break;
    }
using System.Text.Json;
using TaskManager.Models;

List<TaskItem> tasks = new List<TaskItem>();
string filePath = "tasks.json";

if (File.Exists(filePath))
{
    string json = File.ReadAllText(filePath);
    tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
}

while (true)
{
    Console.Clear();

    Console.WriteLine("┌──────────────────────────┐");
    Console.WriteLine("│        TASK MANAGER      │");
    Console.WriteLine("└──────────────────────────┘");

    Console.WriteLine("1. Add task");
    Console.WriteLine("2. Show tasks");
    Console.WriteLine("3. Complete task");
    Console.WriteLine("4. Delete task");
    Console.WriteLine("5. Exit");
    Console.WriteLine("────────────────────────────");

    Console.Write("Choice: ");
    var choice = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(choice))
    {
        Console.WriteLine("Please enter a valid option.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        continue;
    }

    if (choice == "1")
    {
        string title;

        do
        {
            Console.Write("Task title: ");
            title = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(title))
                Console.WriteLine("Title cannot be empty. Please try again.");

        } while (string.IsNullOrWhiteSpace(title));

        int id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

        tasks.Add(new TaskItem
        {
            Id = id,
            Title = title
        });

        Console.WriteLine("Task added successfully.");
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    else if (choice == "2")
    {
        Console.Clear();

        Console.WriteLine("===== TASK LIST =====\n");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
        }
        else
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Id}. [{(task.IsCompleted ? "✔" : " ")}] {task.Title}");
            }
        }

        Console.WriteLine("\n─────────────────────");
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    else if (choice == "3")
    {
        Console.Write("Task ID to complete: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                task.IsCompleted = true;
                Console.WriteLine("Task marked as completed.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    else if (choice == "4")
    {
        Console.Write("Task ID to delete: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Task deleted.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    else if (choice == "5")
    {
        break;
    }

    else
    {
        Console.WriteLine("Invalid option.");
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
}