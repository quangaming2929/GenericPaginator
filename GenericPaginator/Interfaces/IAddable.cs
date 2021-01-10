using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    /// <summary>
    /// Non-generic version of <see cref="IAddable{TItem}"/>. This only used for compatibility. It is recommended to implement the generic version
    /// </summary>
    public interface IAddable 
    {
        /// <summary>
        /// Use <see cref="IAddable{TItem}.Add(TItem, int)"/> unless you know what are you doing
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <param name="index">The position to add</param>
        /// <returns></returns>
        Task Add(object item, int index);
    }

    /// <summary>
    /// Provides functionalities to allow for adding items into a <see cref="IPagedDataSet{TItem, TResult}"/>
    /// </summary>
    /// <typeparam name="TItem">The item's type</typeparam>
    public interface IAddable<in TItem> : IAddable
    {
        /// <summary>
        /// Add an item into the <see cref="IPagedDataSet{TItem, TResult}"/> with the specified position
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <param name="index">The position to add</param>
        /// <returns></returns>
        Task Add(TItem item, int index);
    }

}
