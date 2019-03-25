using McMaster.Extensions.CommandLineUtils;
using PTouch.Commands;

namespace PTouch
{
    [Command("ptouch")]
    [Subcommand(
        typeof(PrintersCommand),
        typeof(ExportCommand),
        typeof(PrintCommand))]
    class Program
    {
        static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        public int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();

            return 1;
        }
    }
}
