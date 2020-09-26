using McMaster.Extensions.CommandLineUtils;
using ROITest.Data;
using System;
using System.Linq;

namespace ROITest.CLI
{
    public class Done
    {
        /// <summary>
        /// Mark a ToDo item as done
        /// </summary>
        /// <param name="app">CommandLineApplication</param>
        /// <returns></returns>
        public CommandLineApplication MarkAsCompleted(CommandLineApplication app)
        {
            var idOption = app.Option("-i|--id", "ID of the task to mark as completed", CommandOptionType.SingleValue).IsRequired();
            var toDoContext = new ToDoContext();

            app.HelpOption("-h | --help");

            app.OnExecute(() =>
            {
                try
                {
                    var item = toDoContext.ToDos.Where(td => td.Id.Equals(Convert.ToInt32(idOption.Value()))).FirstOrDefault();

                    if (item != null)
                    {
                        item.Done = true;
                        toDoContext.SaveChanges();
                    }
                    else
                        Console.WriteLine("Item not found");
                }
                catch (Exception)
                {
                    Console.WriteLine("There was a problem changing the status of the task");
                }
            });
            return app;
        }
    }
}