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
    Console.WriteLine("Task Manager");
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. Show tasks");
    Console.WriteLine("3. Complete task");
    Console.WriteLine("4. Exit");
    Console.Write("Choice: ");
    var choice = Console.ReadLine();

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
        tasks.Add(new TaskItem { Id = id, Title = title });
    }
    else if (choice == "2")
    {
        foreach (var task in tasks)
            Console.WriteLine($"{task.Id}. [{(task.IsCompleted ? "X" : " ")}] {task.Title}");
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }
    else if (choice == "3")
    {
        Console.Write("Task ID to complete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null) task.IsCompleted = true;
        }
    }
    else if (choice == "4")
    {
        break;
    }

    File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
}