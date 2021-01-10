using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    /// <summary>
    /// Non-generic version of <see cref="IPagedDataSet{TItem, TResult}"/>. This only used for compatibility. It is recommended to implement the generic version
    /// </summary>
    public interface IPagedDataSet
    {
        /// <summary>
        /// The the content of the current page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent the page content </returns>
        Task<IPageContent> GetPageContent();

        /// <summary>
        /// Retrive items displayed on the current page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent a sequence of items</returns>
        Task<IEnumerable<object>> GetPageItems();

        /// <summary>
        /// Get the underlying data source
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<object>> GetDataSource();

        /// <summary>
        /// Get the data source that is ready for display
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<object>> GetDisplayedDataSource();

        /// <summary>
        /// Set the underlying data source
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task SetDataSource(IEnumerable<object> items);

        /// <summary>
        /// Get name of the current paged data set
        /// </summary>
        /// <returns></returns>
        Task<string> GetName();

        /// <summary>
        /// Set the the focus state of the paged dataset
        /// </summary>
        /// <returns></returns>
        Task<bool> GetFocusState();

        /// <summary>
        /// Set the the focus state of the paged dataset
        /// </summary>
        /// <param name="isFocused">A value represent whelther the current <see cref="IPagedDataSet{TItem, TResult}"/> is in focus</param>
        /// <returns></returns>
        Task SetFocus(bool isFocused);

        /// <summary>
        /// Get the current item filter string
        /// </summary>
        /// <returns></returns>
        Task<string> GetFilter();

        /// <summary>
        /// Set the new item filter
        /// </summary>
        /// <param name="filter">The new item filter. Null or empty to clear filter</param>
        /// <returns></returns>
        Task SetFilter(string filter);

        // Page navigation
        /// <summary>
        /// Get the index of the current page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent the current page index</returns>
        Task<int> GetCurrentPageIndex();

        /// <summary>
        /// Set the index of the current page
        /// </summary>
        /// <param name="index">The new page index</param>
        /// <returns></returns>
        Task SetCurrentPageIndex(int index);

        /// <summary>
        /// If the index can be set directly without processing
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<bool> IsValidPageIndex(int index);

        /// <summary>
        /// Get the total pages of the current <see cref="IPagedDataSet{TItem, TResult}"/>
        /// </summary>
        /// <returns></returns>
        Task<int> GetPageCount();

        /// <summary>
        /// Jump to another page
        /// </summary>
        /// <param name="offset">The number of page to jump. A positive value will jump forward, a negative value will jump backward</param>
        /// <returns></returns>
        Task MovePageBy(int offset);

        /// <summary>
        /// Get the maximum number of items can be displayed in the a page
        /// </summary>
        /// <returns></returns>
        Task<int> GetPageSize();

        /// <summary>
        /// Set the maximum number of items can be displayed in the a page
        /// </summary>
        /// <param name="pageSize">The new page size</param>
        /// <returns></returns>
        Task SetPageSize(int pageSize);

#nullable enable
        // Optional Features
        /// <summary>
        /// Get the selector if the current <see cref="IPagedDataSet{TItem, TResult}"/> supports it
        /// </summary>
        /// <returns></returns>
        Task<ISelectable?> GetSelector();

        /// <summary>
        /// Get the mover if the current <see cref="IPagedDataSet{TItem, TResult}"/> supports it
        /// </summary>
        /// <returns></returns>
        Task<IMovable?> GetMover();

        /// <summary>
        /// Get the adder if the current <see cref="IPagedDataSet{TItem, TResult}"/> supports it
        /// </summary>
        /// <returns></returns>
        Task<IAddable?> GetAdder();

        /// <summary>
        /// Get the remover if the current <see cref="IPagedDataSet{TItem, TResult}"/> supports it
        /// </summary>
        /// <returns></returns>
        Task<IRemovable?> GetRemover();
#nullable restore
    }

    /// <summary>
    /// Represent a dataset can be manipulated by <see cref="PaginatorController"/>
    /// Note: all passed value are not validated
    /// </summary>
    /// <typeparam name="TItem">The item's type</typeparam>
    /// <typeparam name="TResult">The result page's type</typeparam>
    public interface IPagedDataSet<TItem, TResult> : IPagedDataSet
    {

        /// <summary>
        /// The the content of the current page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent the page content </returns>
        new Task<IPageContent<TResult>> GetPageContent();

        /// <summary>
        /// Retrive items displayed on the current page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent a sequence of items</returns>
        new Task<IEnumerable<TItem>> GetPageItems();

        /// <summary>
        /// Get the underlying data source
        /// </summary>
        /// <returns></returns>
        new Task<IEnumerable<TItem>> GetDataSource();

        /// <summary>
        /// Get the data source that is ready for display
        /// </summary>
        /// <returns></returns>
        new Task<IEnumerable<TItem>> GetDisplayedDataSource();

        /// <summary>
        /// Set the underlying data source
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task SetDataSource(IEnumerable<TItem> items);

        #nullable enable
        // Optional Features
        /// <summary>
        /// Get the selector if the current <see cref="IPagedDataSet{TItem, TResult}"/> supports it
        /// </summary>
        /// <returns></returns>
        new Task<ISelectable<TItem>?> GetSelector();

        /// <summary>
        /// Get the mover if the current <see cref="IPagedDataSet{TItem, TResult}"/> supports it
        /// </summary>
        /// <returns></returns>
        new Task<IMovable<TItem>?> GetMover();

        /// <summary>
        /// Get the adder if the current <see cref="IPagedDataSet{TItem, TResult}"/> supports it
        /// </summary>
        /// <returns></returns>
        new Task<IAddable<TItem>?> GetAdder();

        /// <summary>
        /// Get the remover if the current <see cref="IPagedDataSet{TItem, TResult}"/> supports it
        /// </summary>
        /// <returns></returns>
        new Task<IRemovable<TItem>?> GetRemover();
        #nullable restore
    }
}
