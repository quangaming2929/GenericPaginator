using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class ToStringItemProvider<TItem> : IItemProvider<TItem>
    {
        public Task<string> GetItemString(TItem item) => Task.FromResult(item.ToString());
    }
}