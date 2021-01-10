using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    /// <summary>
    /// Non-generic version of <see cref="IPageContent{TResult}"/>. This only used for compatibility. It is recommended to implement the generic version
    /// </summary>
    public interface IPageContent
    {
        /// <summary>
        /// Get the header of the page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent the page's header</returns>
        Task<object> GetHeader();

        /// <summary>
        /// Get the content of the page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent The page's content</returns>
        Task<object> GetContent();
    }


    /// <summary>
    /// The object with represent the current page
    /// </summary>
    /// <typeparam name="TResult">The type of the page</typeparam>
    public interface IPageContent<TResult> : IPageContent
    {
        /// <summary>
        /// Get the header of the page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent the page's header</returns>
        new Task<TResult> GetHeader();

        /// <summary>
        /// Get the content of the page
        /// </summary>
        /// <returns>A <see cref="Task"/> represent The page's content</returns>
        new Task<TResult> GetContent();
    }

}
