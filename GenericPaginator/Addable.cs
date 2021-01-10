using ImagiSekaiTechnologies.GenericPaginator.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class Addable<TItem, TResult> : IAddable<TItem> where TItem : class
    {
        public bool MaterializeOnOperation { get; set; }

        public PagedDataSet<TItem, TResult> PagedDataSet { get; private set; }

        public Addable(PagedDataSet<TItem, TResult> pagedDataSet) 
        {
            PagedDataSet = pagedDataSet;
        }

        public async Task Add(TItem item, int index)
        {
            var nds = (await PagedDataSet.GetDataSource()).Insert(item, index);
            if (MaterializeOnOperation)
                nds = nds.ToList();
            await PagedDataSet.SetDataSource(nds);
        }

        public Task Add(object item, int index)
        {
            if (item is TItem ti)
                return Add(ti, index);
            if (item == null)
                return Add((TItem)item, index);
            else
                throw new ArgumentException("Wrong argument type", nameof(item));
        }
    }
}