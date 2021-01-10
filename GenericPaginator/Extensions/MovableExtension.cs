using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator.Extensions
{
    public static class MovableExtension
    {
        /// <summary>
        /// Move a item into a new position relative to the current position in the <see cref="IPagedDataSet{TItem, TResult}"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <param name="offset">a new position relative to the current item position</param>
        /// <returns></returns>
        public static async Task MoveOffset<TItem, TResult>(this Movable<TItem, TResult> movable, TItem item, int offset) where TItem : class
            => await movable.MoveOffset(movable.PagedDataSet, item, offset);

        /// <summary>
        /// Move item to the first position in the <see cref="IPagedDataSet{TItem, TResult}"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <returns></returns>
        public static async Task MoveToTop<TItem, TResult>(this Movable<TItem, TResult> movable, TItem item) where TItem : class
            => await movable.MoveToTop(movable.PagedDataSet, item);

        /// <summary>
        /// Move item to the last position in the <see cref="IPagedDataSet{TItem, TResult}"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <returns></returns>
        public static async Task MoveToLast<TItem, TResult>(this Movable<TItem, TResult> movable, TItem item) where TItem : class
            => await movable.MoveToLast(movable.PagedDataSet, item);

        /// <summary>
        /// Move a item into a new position relative to the current position in the <see cref="IPagedDataSet{TItem, TResult}"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <param name="offset">a new position relative to the current item position</param>
        /// <returns></returns>
        public static async Task MoveOffset<TItem>(this IMovable<TItem> movable, IPagedDataSet dataSet, TItem item, int offset)
            => await movable.MoveOffset(dataSet, item, offset);

        /// <summary>
        /// Move item to the first position in the <see cref="IPagedDataSet{TItem, TResult}"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <returns></returns>
        public static async Task MoveToTop<TItem>(this IMovable<TItem> movable, IPagedDataSet dataSet, TItem item)
            => await movable.MoveToTop(dataSet, item);

        /// <summary>
        /// Move item to the last position in the <see cref="IPagedDataSet{TItem, TResult}"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <returns></returns>
        public static async Task MoveToLast<TItem>(this IMovable<TItem> movable, IPagedDataSet dataSet, TItem item)
            => await movable.MoveToLast(dataSet, item);

        /// <summary>
        /// Move a item into a new position relative to the current position in the <see cref="IPagedDataSet"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <param name="offset">a new position relative to the current item position</param>
        /// <returns></returns>
        public static async Task MoveOffset(this IMovable movable, IPagedDataSet dataSet, object item, int offset)
            => await movable.Move(item, (await dataSet.GetDataSource()).FindIndex(item) + offset);

        /// <summary>
        /// Move item to the first position in the <see cref="IPagedDataSet"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <returns></returns>
        public static async Task MoveToTop(this IMovable movable, IPagedDataSet dataSet, object item)
            => await movable.Move(item, 0);

        /// <summary>
        /// Move item to the last position in the <see cref="IPagedDataSet"/>
        /// </summary>
        /// <param name="dataSet">the dataset to move item</param>
        /// <param name="item">The item to move</param>
        /// <returns></returns>
        public static async Task MoveToLast(this IMovable movable, IPagedDataSet dataSet, object item)
            => await movable.Move(item, (await dataSet.GetDataSource()).Count() - 1);
    }
}
