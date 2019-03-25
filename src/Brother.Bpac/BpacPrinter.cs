using bpac;
using System.Linq;

namespace Brother.Bpac
{
    public class BpacPrinter
    {
        private readonly PrinterClass _printer;

        public BpacPrinter()
        {
            _printer = new PrinterClass();
        }

        public string[] GetInstalledPrinters()
        {
            return ((object[])_printer.GetInstalledPrinters()).Cast<string>().ToArray();
        }

        public bool IsPrinterOnline(string printerName)
        {
            return _printer.IsPrinterOnline(printerName);
        }
    }
}
