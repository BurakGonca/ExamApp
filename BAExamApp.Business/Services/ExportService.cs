using OfficeOpenXml;

namespace BAExamApp.Business.Services;

public class ExportService<T> : IExportService<T> where T : class
{
    public byte[] ExportToExcel(IEnumerable<T> data)
    {
        using (var package = new ExcelPackage())
        {
            var workSheet = package.Workbook.Worksheets.Add("VeriSayfasi");

            int colIndex = 1;
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                workSheet.Cells[1, colIndex].Value = property.Name;
                colIndex++;
            }

            int rowIndex = 2;
            foreach (var item in data)
            {
                colIndex = 1;
                foreach (var property in properties)
                {
                    workSheet.Cells[rowIndex, colIndex].Value = property.GetValue(item);
                    colIndex++;
                }
                rowIndex++;
            }

            return package.GetAsByteArray();
        }
    }

    public IEnumerable<T> FilterData(IEnumerable<T> data, Func<T, bool> filter)
    {
        return data.Where(filter);
    }

}

