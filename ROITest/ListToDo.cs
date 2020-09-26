using McMaster.Extensions.CommandLineUtils;
using ROITest.Data;
using System;
using System.Linq;

namespace ROITest.CLI
{
    public class ListToDo
    {
        /// <summary>
        /// Sort nor filter the records from the database
        /// </summary>
        /// <param name="app">CommandLineApplication</param>
        /// <returns></returns>
        public CommandLineApplication List(CommandLineApplication app)
        {
            var sortOption = app.Option("-s|--sort", "Sort results by 'id', 'idrev', 'title', 'titlerev', 'status', 'statusrev', 'duedate' or 'duedaterev'", CommandOptionType.SingleValue);
            var filterOption = app.Option("-f|--filter", "Filter results by 'done','pending','ongoing','past', 'title' (can be applied multiple times)", CommandOptionType.MultipleValue);
            var filter = app.Argument("title", "Word to filter by");
            var toDoContext = new ToDoContext();

            app.HelpOption("-h | --help");

            app.OnExecute(() =>
            {
                var items = toDoContext.ToDos.ToList();

                if (sortOption.HasValue())
                    try
                    {
                        switch (sortOption.Value())
                        {
                            case "id":
                                items = items.OrderBy(td => td.Id).ToList();
                                break;

                            case "idrev":
                                items = items.OrderByDescending(td => td.Id).ToList();
                                break;

                            case "title":
                                items = items.OrderBy(td => td.Title).ToList();
                                break;

                            case "titlerev":
                                items = items.OrderByDescending(td => td.Title).ToList();
                                break;

                            case "status":
                                items = items.OrderBy(td => td.Done).ToList();
                                break;

                            case "statusrev":
                                items = items.OrderByDescending(td => td.Done).ToList();
                                break;

                            case "duedate":
                                items = items.OrderBy(td => td.DueDate).ToList();
                                break;

                            case "duedaterev":
                                items = items.OrderByDescending(td => td.DueDate).ToList();
                                break;

                            default:
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("There was a problem listing the items");
                    }

                if (filterOption.HasValue())
                    try
                    {
                        switch (filterOption.Value())
                        {
                            case "done":
                                items = items.Where(td => td.Done.Equals(true)).ToList();
                                break;

                            case "pending":
                                items = items.Where(td => td.Done.Equals(false)).ToList();
                                break;

                            case "past":
                                items = items.Where(td => td.DueDate < DateTime.Now).ToList();
                                break;

                            case "ongoing":
                                items = items.Where(td => td.DueDate > DateTime.Now).ToList();
                                break;

                            case "title":
                                items = items.Where(td => td.Title.Contains(filter.Value)).ToList();
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("There was a problem with the filters");
                    }

                if (items.Count == 0)
                {
                    Console.WriteLine("The list is empty");
                }
                else
                    foreach (var item in items)
                    {
                        Console.WriteLine(item);
                    }
            });
            return app;
        }
    }
}