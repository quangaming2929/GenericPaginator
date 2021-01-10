using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    /// <summary>
    /// Non-generic version of <see cref="ISelectable{TItem}"/>. This only used for compatibility. It is recommended to implement the generic version
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Get the current caret position
        /// </summary>
        /// <returns></returns>
        Task<int> GetCaretPosition();

        /// <summary>
        /// Set the current caret position
        /// </summary>
        /// <param name="newPosition">The new position caret</param>
        /// <returns></returns>
        Task SetCaretPosition(int newPosition);

        /// <summary>
        /// If the position can be set directly without processing
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        Task IsValidCaretPosition(int position);

        /// <summary>
        /// Use <see cref="ISelectable{TItem}.GetSelectedItem()"/> unless you know what are you doing
        /// </summary>
        Task<object> GetSelectedItem();

        /// <summary>
        /// Use <see cref="ISelectable{TItem}.SetSelectedItem(TItem)"/> unless you know what are you doing
        /// </summary>
        Task SetSelectedItem(object item);
    }

    /// <summary>
    /// Provides functionalities to allow for selecting item inside a page
    /// Note: all passed value are not validated
    /// </summary>
    /// <typeparam name="TItem">The item's type</typeparam>
    public interface ISelectable<TItem> : ISelectable
    {
        /// <summary>
        /// Get the current selected item
        /// </summary>
        /// <returns></returns>
        new Task<TItem> GetSelectedItem();

        /// <summary>
        /// Move the caret to the specified item. This can be in different page
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task SetSelectedItem(TItem item);
    }

}
