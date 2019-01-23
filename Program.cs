using McMaster.Extensions.CommandLineUtils;
using PTouchCli.Commands;

namespace PTouchCli
{
    [Command]
    [Subcommand(typeof(PrintCommand), typeof(ExportCommand))]
    class Program
    {
        static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        int OnExecute(IConsole console, CommandLineApplication app)
        {
            app.ShowHelp();
            return 1;
        }
    }
}
