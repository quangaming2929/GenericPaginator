using ImagiSekaiTechnologies.GenericPaginator.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class Movable<TItem, TResult> : IMovable<TItem> where TItem : class
    {
        public bool MaterializeOnOperation { get; set; }
        public IPagedDataSet<TItem, TResult> PagedDataSet { get; private set; }

        public Movable(IPagedDataSet<TItem, TResult> pagedDataSet)
        {
            PagedDataSet = pagedDataSet;
        }

        public async Task Move(TItem item, int index)
        {
            var nds = (await PagedDataSet.GetDataSource()).Move(item, index);
            if (MaterializeOnOperation)
                nds = nds.ToList();
            await PagedDataSet.SetDataSource(nds);
        }

        public Task Move(object item, int index)
        {
            if (item is TItem ti)
                return Move(ti, index);
            else if (item == null)
                return Move((TItem)item, index);
            else
                throw new ArgumentException("Invalid type parameter", nameof(item));
        }
    }
}