using McMaster.Extensions.CommandLineUtils;
using ROITest.Data;
using ROITest.Models;
using System;
using System.Globalization;

namespace ROITest.CLI
{
    public class AddToDo
    {
        /// <summary>
        /// Add a new ToDo item into the database
        /// </summary>
        /// <param name="app">CommandLineApplication</param>
        /// <returns></returns>
        public CommandLineApplication AddNewToDo(CommandLineApplication app)
        {
            var titleOption = app.Option("-t|--title <Title>", "ToDo Title", CommandOptionType.SingleValue).IsRequired();
            var dueDateOption = app.Option("-d|--duedate <DueDate>", "ToDo due date", CommandOptionType.SingleValue);
            var completedOption = app.Option("-c|--completed <True/False>", "Status of the ToDo", CommandOptionType.SingleValue);
            var errors = false;

            app.HelpOption("-h | --help");

            app.OnExecute(() =>
            {
                var newToDo = new ToDoModel
                {
                    Title = titleOption.Value(),
                };

                if (dueDateOption.HasValue())
                    try
                    {
                        newToDo.DueDate = DateTime.Parse(dueDateOption.Value(), new CultureInfo("es-ES"));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The due date is not a valid date time format");
                        errors = true;
                    }

                if (completedOption.HasValue())
                    try
                    {
                        newToDo.Done = Convert.ToBoolean(completedOption.Value());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The status is not in a valid format, please introduce 'false' or 'true'");
                        errors = true;
                    }

                try
                {
                    if (!errors)
                    {
                        var toDoContext = new ToDoContext();
                        toDoContext.ToDos.Add(newToDo);
                        toDoContext.SaveChanges();
                        Console.WriteLine($"ToDo {newToDo} added to the database");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was a problem creating the item \n\r {ex}");
                }


            });
            return app;
        }
    }
}