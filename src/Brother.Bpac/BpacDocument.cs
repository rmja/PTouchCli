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

        public int GetPrinterMediaId()
        {
            return _doc.Printer.GetMediaId();
        }

        public void SetMediaId(int mediaId, bool fitPage = false)
        {
            if (!_doc.SetMediaById(mediaId, fitPage))
            {
                throw new BpacException("Unable to set media");
            }
        }

        public string GetPrinterName()
        {
            return _doc.GetPrinterName();
        }

        public void SetPrinter(string printerName, bool fitPage = false)
        {
            if (!_doc.SetPrinter(printerName, fitPage))
            {
                throw new BpacException($"Unable to set printer '{printerName}'");
            }
        }

        public BpacObject GetObject(string objectName)
        {
            var obj = _doc.GetObject(objectName);

            if (obj == null)
            {
                return null;
            }

            return new BpacObject(obj);
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
                if (_doc.ErrorCode == 11) // Errorcode found in the SDK Help file
                {
                    throw new BpacException("The currently specified printer is not supported - make sure that the 32bit drivers are installed");
                }

                throw new BpacException($"Unable to print, error code: {_doc.ErrorCode}");
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
