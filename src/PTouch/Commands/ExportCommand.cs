using Brother.Bpac;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace PTouch.Commands
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

        public int OnExecute(IConsole console)
        {
            var bpac = new BpacDocument();

            bpac.Open(Template);

            for (var i = 0; i < RemainingArguments.Length; i += 2)
            {
                var key = RemainingArguments[i];
                var value = RemainingArguments[i + 1];

                bpac.GetObject(key).Text = value;
            }

            bpac.SaveAs(Destination);
            console.WriteLine($"Label was written to file '{Destination}'");

            bpac.Close();

            return 0;
        }
    }
}
