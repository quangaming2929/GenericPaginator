using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator.Extensions
{
    public static class AddableExtension
    {
        /// <summary>
        /// Add an item into the <see cref="IPagedDataSet{TItem, TResult}"/> with the position relative to the <see cref="ISelectable{TItem}"/>'s caret
        /// </summary>
        /// <param name="addable">The adder to perform</param>
        /// <param name="item">The item to add</param>
        /// <param name="offset">The position to add relative to the <see cref="IPagedDataSet{TItem, TResult}"/> caret</param>
        /// <returns></returns>
        public static Task AddOffset<TItem, TResult>(this Addable<TItem, TResult> addable, TItem item, int offset) where TItem : class
            => AddOffset(addable, addable.PagedDataSet, item, offset);

        /// <summary>
        /// Add an item into the <see cref="IPagedDataSet{TItem, TResult}"/> with the position relative to the <see cref="ISelectable{TItem}"/>'s caret
        /// </summary>
        /// <param name="addable">The adder to perform</param>
        /// <param name="item">The item to add</param>
        /// <param name="dataSet">the dataset to modify</param>
        /// <param name="offset">The position to add relative to the <see cref="IPagedDataSet{TItem, TResult}"/> caret</param>
        /// <returns></returns>
        public static Task AddOffset<TItem>(this IAddable<TItem> addable, IPagedDataSet dataSet, TItem item, int offset) where TItem : class
            => AddOffset(addable, dataSet, item, offset);

        /// <summary>
        /// Add an item into the <see cref="IPagedDataSet"/> with the position relative to the <see cref="ISelectable"/>'s caret
        /// </summary>
        /// <param name="addable">The adder to perform</param>
        /// <param name="item">The item to add</param>
        /// <param name="dataSet">the dataset to modify</param>
        /// <param name="offset">The position to add relative to the <see cref="IPagedDataSet{TItem, TResult}"/> caret</param>
        /// <returns></returns>
        public static async Task AddOffset(this IAddable addable, IPagedDataSet dataSet, object item, int offset) 
        {
            var selector = await dataSet.GetSelector();
            if (selector == null)
                throw new InvalidOperationException("PagedDataSet must support selector to use this function!");
            var pi = await dataSet.GetCurrentPageIndex();
            var ps = await dataSet.GetPageSize();
            var caret = await selector.GetCaretPosition();
            await addable.Add(item, pi * ps + caret + offset);
        }
    }
}
