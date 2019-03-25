using bpac;
using System;
using System.Collections.Generic;
using System.IO;

namespace Brother.Bpac
{
    public class BpacDocument
    {
        private readonly DocumentClass _doc;

        public BpacDocument()
        {
            _doc = new DocumentClass();
        }

        public void Open(string filePath)
        {
            if (!_doc.Open(filePath))
            {
                throw new BpacException($"Unable to open file '{filePath}'");
            }
        }

        public void Close()
        {
            if (!_doc.Close())
            {
                throw new BpacException("Unable to close document");
            }
        }

        public string GetPrinterName()
        {
            return _doc.GetPrinterName();
        }

        public void SetPrinter(string printerName, bool fitPage)
        {
            if (!_doc.SetPrinter(printerName, fitPage))
            {
                throw new BpacException($"Unable to set printer '{printerName}'");
            }
        }

        public BpacObject GetObject(string objectName)
        {
            return new BpacObject(_doc.GetObject(objectName));
        }

        public void StartPrint(string docName = "", PrintOptions options = PrintOptions.Default)
        {
            if (!_doc.StartPrint(docName, (PrintOptionConstants)options))
            {
                throw new BpacException("Unable to start printing");
            }
        }

        public void PrintOut(int copyCount, PrintOptions options = PrintOptions.Default)
        {
            if (!_doc.PrintOut(copyCount, (PrintOptionConstants)options))
            {
                throw new BpacException(_doc.Printer.ErrorString);
            }
        }

        public void EndPrint()
        {
            if (!_doc.EndPrint())
            {
                throw new BpacException("Unable to end printing");
            }
        }

        public void SaveAs(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            var extensions = new Dictionary<string, ExportType>(StringComparer.OrdinalIgnoreCase)
            {
                [".lbx"] = ExportType.bexLbx,
                [".lbl"] = ExportType.bexLbl
            };

            if (!extensions.TryGetValue(extension, out var exportType))
            {
                exportType = ExportType.bexOpened;
            }

            if (!_doc.SaveAs(exportType, filePath))
            {
                throw new BpacException($"Unable to save file to '{filePath}'");
            }
        }
    }
}
