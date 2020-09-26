using McMaster.Extensions.CommandLineUtils;

namespace ROITest.CLI
{
    public class Program
    {
        //Create the main Command options for the CLI
        public static void Main(string[] args)
        {
            var app = new CommandLineApplication<Program>();
            app.UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue;

            app.HelpOption();

            app.Command("add",
                (addToDo) => { var cmd = new AddToDo().AddNewToDo(addToDo); });

            app.Command("list",
                (list) => { var cmd = new ListToDo().List(list); });

            app.Command("done",
                (done) => { var cmd = new Done().MarkAsCompleted(done); });

            app.Command("bulk",
                (bulk) => { var cmd = new Bulk().FilterAndBulk(bulk); });

            app.HelpOption("-h|--help|-?");
            app.Execute(args);
        }
    }
}