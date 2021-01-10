using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public interface IMovable
    {
        /// <summary>
        /// Use <see cref="IMovable{TItem}.Move(TItem, int)"/> unless you know what are you doing
        /// </summary>
        /// <param name="item">The item to move</param>
        /// <param name="index">the item index</param>
        /// <returns></returns>
        Task Move(object item, int index);
    }

    /// <summary>
    /// Provides functionalities to allow for moving item inside a <see cref="IPagedDataSet{TItem, TResult}"/>
    /// Note: all passed value are not validated
    /// </summary>
    /// <typeparam name="TItem">The item's type</typeparam>
    public interface IMovable<in TItem> : IMovable
    {
        /// <summary>
        /// Move a item to a new position
        /// </summary>
        /// <param name="item">The item to move</param>
        /// <param name="index">the item index</param>
        /// <returns></returns>
        Task Move(TItem item, int index);
    }
}
