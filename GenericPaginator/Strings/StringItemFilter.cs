using ImagiSekaiTechnologies.GenericPaginator.Interfaces;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class StringItemFilter : IItemFilter<string>
    {
        public bool Filter(string item, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return true;
            return item.Contains(keyword, System.StringComparison.OrdinalIgnoreCase);
        }

        public bool Filter(object item, string keyword) => Filter(item.ToString(), keyword);
    }
}