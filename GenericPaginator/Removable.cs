using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class Removable<TItem, TResult> : IRemovable<TItem> where TItem : class
    {
        public bool MaterializeOnOperation { get; set; }
        public IPagedDataSet<TItem, TResult> PagedDataSet { get; private set; }

        public Removable(IPagedDataSet<TItem, TResult> pagedDataSet)
        {
            PagedDataSet = pagedDataSet;
        }

        public async Task Remove(TItem item)
        {
            var nds = (await PagedDataSet.GetDataSource()).Where(x => !x.Equals(item));
            if (MaterializeOnOperation)
                nds = nds.ToList();
            await PagedDataSet.SetDataSource(nds);
        }

        public Task Remove(object item)
        {
            if (item is TItem ts)
                return Remove(ts);
            else if (item == null)
                return Remove((TItem)item);
            else
                throw new ArgumentException("Invalid parameter type", nameof(item));
        }
    }
}