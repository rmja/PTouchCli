using bpac;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace PTouchCli.Commands
{
    [Command("print", ThrowOnUnexpectedArgument = false)]
    class PrintCommand
    {
        [Argument(0, "template", "The .lbx file to use as template")]
        [Required]
        public string Template { get; set; }

        [Option("-n|--count", "The number of copies to print", CommandOptionType.SingleValue)]
        public int Count { get; } = 1;

        public string[] RemainingArguments { get; }

        int OnExecute(IConsole console)
        {
            var document = new DocumentClass();

            if (!document.Open(Template))
            {
                console.WriteLine($"Unable to open template file '{Template}'");
                return 1;
            }

            for (var i = 0; i < RemainingArguments.Length; i += 2)
            {
                var key = RemainingArguments[i];
                var value = RemainingArguments[i + 1];

                document.GetObject(key).Text = value;
            }

            document.StartPrint(string.Empty, PrintOptionConstants.bpoDefault);
            document.PrintOut(Count, PrintOptionConstants.bpoDefault);
            document.EndPrint();

            console.WriteLine($"{Count} copies were sent to printer");
            document.Close();
            return 0;
        }
    }
}
