using Brother.Bpac;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace PTouch.Commands
{
    [Command("print", ThrowOnUnexpectedArgument = false)]
    class PrintCommand
    {
        [Argument(0, "template", "The .lbx file to use as template")]
        [Required]
        public string Template { get; set; }

        [Option("-n|--count", "The number of copies to print", CommandOptionType.SingleValue)]
        public int Count { get; } = 1;

        [Option("-p|--printer-name")]
        public string PrinterName { get; set; }

        public string[] RemainingArguments { get; }

        public int OnExecute(IConsole console)
        {
            var bpac = new BpacDocument();

            if (PrinterName != null)
            {
                bpac.SetPrinter(PrinterName, fitPage: false);
            }

            bpac.Open(Template);

            for (var i = 0; i < RemainingArguments.Length; i += 2)
            {
                var key = RemainingArguments[i];
                var value = RemainingArguments[i + 1];

                bpac.GetObject(key).Text = value;
            }

            bpac.StartPrint();
            bpac.PrintOut(Count);
            bpac.EndPrint();

            var printerName = bpac.GetPrinterName();
            console.WriteLine($"{Count} copies were sent to printer '{printerName}'");

            bpac.Close();

            return 0;
        }
    }
}
