using bpac;
using System;
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
            var printers = (object[])_printer.GetInstalledPrinters();
            if (printers is null)
            {
                return Array.Empty<string>();
            }
            return printers.Cast<string>().ToArray();
        }

        public bool IsPrinterOnline(string printerName)
        {
            return _printer.IsPrinterOnline(printerName);
        }
    }
}
