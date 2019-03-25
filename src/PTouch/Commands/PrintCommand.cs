using Brother.Bpac;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            var doc = new BpacDocument();
            var printer = new BpacPrinter();

            doc.Open(Template);

            var printerName = PrinterName ?? printer.GetInstalledPrinters().FirstOrDefault();

            if (printerName == null)
            {
                console.Error.WriteLine("No printer found");

                return 1;
            }

            doc.SetPrinter(printerName);

            for (var i = 0; i < RemainingArguments.Length; i += 2)
            {
                var key = RemainingArguments[i];
                var value = RemainingArguments[i + 1];

                var obj = doc.GetObject(key);

                if (obj == null)
                {
                    console.Error.WriteLine($"The object '{key}' was not found in the template");

                    return 1;
                }

                obj.Text = value;
            }

            doc.StartPrint();
            doc.PrintOut(Count);
            doc.EndPrint();

            //console.WriteLine($"{Count} copies were sent to printer '{printerName}'");

            doc.Close();

            return 0;
        }
    }
}
