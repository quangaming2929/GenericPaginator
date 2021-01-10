using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator.Interfaces
{
    /// <summary>
    /// Non-generic version of <see cref="ISelectable{TItem}"/>. This only used for compatibility. It is recommended to implement the generic version
    /// </summary>
    public interface IItemFilter
    {
        /// <summary>
        /// Use <see cref="IItemFilter{TItem}.Filter(TItem, string)"/> unless you know what are you doing
        /// </summary>
        bool Filter(object item, string keyword);
    }

    /// <summary>
    /// Provides filtering for <see cref="IPagedDataSet{TItem, TResult}"/>
    /// </summary>
    /// <typeparam name="TItem">The item's type</typeparam>
    public interface IItemFilter<TItem> : IItemFilter
    {
        /// <summary>
        /// Filter the item, return true to include the item, false to exlude it
        /// </summary>
        /// <param name="item">The item to filter</param>
        /// <param name="keyword">The keyword</param>
        /// <returns></returns>
        bool Filter(TItem item, string keyword);
    }
}
