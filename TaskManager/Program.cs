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
    Console.WriteLine("===== TASK MANAGER =====");
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. Show tasks");
    Console.WriteLine("3. Complete task");
    Console.WriteLine("4. Delete task");
    Console.WriteLine("5. Show completed tasks");
    Console.WriteLine("6. Show pending tasks");
    Console.WriteLine("7. Search tasks");
    Console.WriteLine("8. Exit");
    Console.WriteLine("------------------------");
    Console.Write("Choice: ");

    var choice = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(choice))
    {
        Console.WriteLine("Please enter a valid option.");
        Console.WriteLine("Press any key...");
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
                Console.WriteLine("Title cannot be empty.");
        }
        while (string.IsNullOrWhiteSpace(title));

        int id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

        tasks.Add(new TaskItem
        {
            Id = id,
            Title = title
        });
    }

    else if (choice == "2")
    {
        ShowTasks(tasks);
    }

    else if (choice == "3")
    {
        Console.Write("Task ID to complete: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
                task.IsCompleted = true;
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
                Console.Write("Are you sure you want to delete this task? (y/n): ");
                var confirm = Console.ReadLine();

                if (confirm?.ToLower() == "y")
                {
                    tasks.Remove(task);
                    Console.WriteLine("Task deleted.");
                }
                else
                {
                    Console.WriteLine("Delete cancelled.");
                }

                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
        }
    }

    else if (choice == "5")
    {
        var completed = tasks.Where(t => t.IsCompleted);
        ShowTasks(completed);
    }

    else if (choice == "6")
    {
        var pending = tasks.Where(t => !t.IsCompleted);
        ShowTasks(pending);
    }

    else if (choice == "7")
    {
        Console.Write("Enter keyword: ");
        var keyword = Console.ReadLine() ?? "";

        var results = tasks
            .Where(t => t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        ShowTasks(results);
    }
    else if (choice == "8")
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

static void ShowTasks(IEnumerable<TaskItem> tasks)
{
    foreach (var task in tasks)
        Console.WriteLine($"{task.Id}. [{(task.IsCompleted ? "✔" : " ")}] {task.Title}");

    Console.WriteLine("Press any key...");
    Console.ReadKey();
}