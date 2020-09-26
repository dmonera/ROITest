using McMaster.Extensions.CommandLineUtils;
using ROITest.Data;
using System;
using System.Linq;

namespace ROITest.CLI
{
    public class Bulk
    {
        /// <summary>
        /// Filter and apply changes in bulk
        /// Please be aware that the order to apply 
        /// </summary>
        /// <param name="app">CommandLineApplication</param>
        /// <returns></returns>
        public CommandLineApplication FilterAndBulk(CommandLineApplication app)
        {
            var bulkOption = app.Option("-b|--bulk", "Apply changes to this field in every record. 'done', 'pending', 'duedate:???', 'title:???'", CommandOptionType.SingleValue).IsRequired();
            var filterOption = app.Option("-f|--filter", "Filter results by 'done','pending','ongoing','past', 'title:???' (can be applied multiple times)", CommandOptionType.MultipleValue);

            var toDoContext = new ToDoContext();

            app.HelpOption("-h | --help");

            app.OnExecute(() =>
            {
                var items = toDoContext.ToDos.ToList();

                if (filterOption.HasValue())
                    try
                    {
                        var option = filterOption.Value().Split(':');
                        switch (option.FirstOrDefault())
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
                                if (!string.IsNullOrEmpty(option[1]))
                                    items = items.Where(td => td.Title.Contains(option[1])).ToList();
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("There was a problem with the filters");
                    }

                try
                {
                    var option = bulkOption.Value().Split(':');
                    switch (option.FirstOrDefault())
                    {
                        case "done":
                            items = items.Select(td => { td.Done = true; return td; }).ToList();
                            break;

                        case "pending":
                            items = items.Select(td => { td.Done = false; return td; }).ToList();
                            break;

                        case "duedate":
                            if (!string.IsNullOrEmpty(option[1]))
                                items = items.Select(td => { td.DueDate = DateTime.Parse(option[1]); return td; }).ToList();
                            break;

                        case "title":
                            if (!string.IsNullOrEmpty(option[1]))
                                items = items.Select(td => { td.Title = option[1]; return td; }).ToList();
                            break;
                    }
                }
                catch (Exception)
                {
                }

                if (items.Count == 0)
                {
                    Console.WriteLine("The list is empty");
                }
                else
                    foreach (var item in items)
                    {
                        Console.WriteLine(item);
                        toDoContext.SaveChanges();
                    }
            });
            return app;
        }
    }
}