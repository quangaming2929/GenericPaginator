using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    /// <summary>
    /// A class that generate content based on <see cref="IPagedDataSet{TItem, TResult}"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IPageContentGenerator<TItem, TResult>
    {
        /// <summary>
        /// Generate page content
        /// </summary>
        /// <returns></returns>
        Task<IPageContent<TResult>> GeneratePageContent();
    }
}
