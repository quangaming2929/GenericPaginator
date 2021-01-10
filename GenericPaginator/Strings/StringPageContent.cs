using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class StringPageContent : IPageContent<string> 
    {
        private string _header;
        private string _content;
        public StringPageContent(string header, string content)
        {
            _header = header;
            _content = content;
        }

        public Task<string> GetHeader() => Task.FromResult(_header);
        public Task<string> GetContent() => Task.FromResult(_content);

        #region Implements non-generic IPageContent
        async Task<object> IPageContent.GetHeader() => await GetHeader();
        async Task<object> IPageContent.GetContent() => await GetContent();
        #endregion
    }
}