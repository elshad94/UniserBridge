using System.Collections;
using System.ComponentModel;
using System.Reflection;
using ClosedXML.Excel;
using Project.Core.Utilities.Exceptions;

namespace Project.Core.Utilities.Tools
{
    public static class FileExporterTools
    {



        public static byte[] ExportDataToExcel(object data, string documentName)
        {
            try
            {
                byte[] content;
                using var workbook = new XLWorkbook();

                int cellCount = 1;
                IXLWorksheet worksheet =
                    workbook.Worksheets.Add(documentName);

                var getProperties = data.GetType().GetProperties();

                foreach (PropertyInfo property in getProperties)
                {

                    var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault();
                    worksheet.Cell(1, cellCount++).Value = attribute != null ? attribute.DisplayName : property.Name;

                }

                RenderCellValues(data, worksheet);

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                content = stream.ToArray();
                return content;
            }
            catch (Exception ex)
            {
                return null;

            }
        }


        private static void RenderCellValues(object data, IXLWorksheet worksheet)
        {
            var values = data as IList;

            if (values == null)
            {
                throw new InvalidRequestParameterException(nameof(data));
            }

            int currentRow = 1;

            for (int i = 0; i < values.Count; i++)
            {
                int countForData = 1;
                currentRow++;

                Type type = values[i].GetType();
                PropertyInfo[] props = type.GetProperties();
                foreach (var prop in props)
                {
                    var getItemValue = prop.GetValue(values[i]);
                    worksheet.Cell(currentRow, countForData++).SetValue(XLCellValue.FromObject(getItemValue));
                }

            }

        }

    }
}
