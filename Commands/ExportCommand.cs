using bpac;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace PTouchCli.Commands
{
    [Command("export", ThrowOnUnexpectedArgument = false)]
    class ExportCommand
    {
        [Argument(0, "template", "The .lbx file to use as template")]
        [Required]
        public string Template { get; set; }

        [Argument(1, "destination", "The .lbl file to export to")]
        [Required]
        public string Destination { get; set; }

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

            if (!document.SaveAs(ExportType.bexLbl, Destination))
            {
                console.WriteLine($"Unable to write to file '{Destination}'");
                document.Close();
                return 1;
            }

            document.Close();
            return 0;
        }
    }
}
