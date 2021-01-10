using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    /// <summary>
    /// Non-generic version of <see cref="IRemovable"/>. This only used for compatibility. It is recommended to implement the generic version
    /// </summary>
    public interface IRemovable
    {
        /// <summary>
        /// Use <see cref="IRemovable{TItem}.Move(TItem, int)"/> unless you know what are you doing
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns></returns>
        Task Remove(object item);
    }

    /// <summary>
    /// Provides functionalities to allow for removing items from a <see cref="IPagedDataSet{TItem, TResult}"/>
    /// </summary>
    /// <typeparam name="TItem">The item's type</typeparam>
    public interface IRemovable<TItem> : IRemovable
    {
        /// <summary>
        /// Remove an item from the <see cref="IPagedDataSet{TItem, TResult}"/>
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns></returns>
        Task Remove(TItem item);
    }

}
