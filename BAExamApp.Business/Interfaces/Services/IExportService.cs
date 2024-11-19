using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services;
public interface IExportService<T>
{
    /// <summary>
    /// Verileri bir Excel dosyasına dönüştürür ve byte dizisi olarak döndürür.
    /// </summary>
    /// <param name="data">Excel dosyasına dönüştürülecek veri koleksiyonu.</param>
    /// <returns>Excel dosyasının byte dizisi olarak döndürülen sonucu.</returns>
   
        byte[] ExportToExcel(IEnumerable<T> data);

    /// <summary>
    /// Belirli bir filtreleme kriterine göre verileri filtreler.
    /// </summary>
    /// <param name="data">Filtrelenecek veri koleksiyonu.</param>
    /// <param name="filter">Filtreleme kriterlerini tanımlayan bir işlev.</param>
    /// <returns>Filtrelenmiş veri koleksiyonu.</returns>
    IEnumerable<T> FilterData(IEnumerable<T> data, Func<T, bool> filter);
}
