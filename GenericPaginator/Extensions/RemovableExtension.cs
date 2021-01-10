using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator.Extensions
{
    public static class RemovableExtension
    {
        /// <summary>
        /// Remove an item from the <see cref="IPagedDataSet"/> with the specified position
        /// </summary>
        /// <param name="dataSet">The dataset to modify</param>
        /// <param name="offset">The position to remove relative to the dataset's caret</param>
        /// <returns></returns>
        public static Task RemoveAtOffset<TItem, TResult>(this Removable<TItem, TResult> removable, int offset) where TItem : class
            => removable.RemoveAtOffset(removable.PagedDataSet, offset);

        /// <summary>
        /// Remove an item from the <see cref="IPagedDataSet"/> with the specified position
        /// </summary>
        /// <param name="dataSet">The dataset to modify</param>
        /// <param name="offset">The position to remove relative to the dataset's caret</param>
        /// <returns></returns>
        public static async Task RemoveAtOffset(this IRemovable removable, IPagedDataSet dataSet, int offset)
        {
            var pi = await dataSet.GetCurrentPageIndex();
            var ps = await dataSet.GetPageSize();
            var sel = await dataSet.GetSelector();
            if (sel == null) 
                throw new InvalidOperationException("The paged data set does not support selecting items that is required for this method to work");

            await removable.RemoveAt(dataSet, pi * ps + offset + await sel.GetCaretPosition());
        }

        /// <summary>
        /// Remove an item from the <see cref="IPagedDataSet"/> with the specified position
        /// </summary>
        /// <param name="dataSet">The dataset to modify</param>
        /// <param name="index">The position to remove</param>
        /// <returns></returns>
        public static async Task RemoveAt(this IRemovable removable, IPagedDataSet dataSet, int index)
            => await removable.Remove((await dataSet.GetDataSource()).ElementAtOrDefault(index));
    }
}
