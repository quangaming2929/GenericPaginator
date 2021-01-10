using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    /// <summary>
    /// A class that provide information about an item
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface IItemProvider<TItem>
    {
        /// <summary>
        /// get the string representation about the item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<string> GetItemString(TItem item);
    }
}
