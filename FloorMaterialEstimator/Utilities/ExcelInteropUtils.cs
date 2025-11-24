using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    using Excel = Microsoft.Office.Interop.Excel;
    using Microsoft.Office.Interop.Excel;


    public static class ExcelInteropUtils
    {
        public static Excel.Worksheet GetWorksheetByName(this Excel.Workbook workbook, string name)
        {
            return workbook.Worksheets.OfType<Excel.Worksheet>().FirstOrDefault(ws => ws.Name == name);
        }

        public static List<string> GetWorksheetNames(string selectedWorkbookName)
        {
            List<string> worksheetNames = new List<string>();

            Excel.Application exApp;

            exApp = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");

            try
            {
                List<string> openWkbkNames = new List<string>();

                Workbook exportWorkbook = exApp.Workbooks.get_Item(selectedWorkbookName);

                foreach (Worksheet ws in exportWorkbook.Worksheets)
                {
                    if (ws.Type == XlSheetType.xlWorksheet)
                    {
                        worksheetNames.Add(ws.Name);
                    }
                }

                return worksheetNames;
            }

            catch
            {
                return worksheetNames;
            }

        }

        public static List<char> invalidExcelWorksheetChars = new List<char>() { '/', '*', '?', ':', '[', ']', '\\' };

        public static bool ValidWorksheetName(string worksheetName)
        {
            if (string.IsNullOrEmpty(worksheetName))
            {
                return false;
            }

            if (worksheetName.Length > 31)
            {
                return false;
            }


            if (invalidExcelWorksheetChars.Any(c => worksheetName.Contains(c)))
            {
                return false;
            }

            return true;
        }
    }
}
