using Brother.Bpac;
using McMaster.Extensions.CommandLineUtils;

namespace PTouch.Commands
{
    [Command("printers")]
    class PrintersCommand
    {
        public int OnExecute(IConsole console)
        {
            var bpac = new BpacPrinter();

            var printerNames = bpac.GetInstalledPrinters();

            foreach (var printerName in printerNames)
            {
                console.Write(printerName);
                console.WriteLine(
                    bpac.IsPrinterOnline(printerName)
                        ? " (ONLINE)"
                        : " (OFFLINE)");
            }

            return 0;
        }
    }
}
