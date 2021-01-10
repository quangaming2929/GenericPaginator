using ImagiSekaiTechnologies.GenericPaginator.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class Selectable<TItem, TResult> : ISelectable<TItem> where TItem : class
    {
        public IPagedDataSet<TItem, TResult> PagedDataSet { get; private set; }
        private int _caretPosition;

        public Selectable(IPagedDataSet<TItem, TResult> pagedDataSet)
        {
            PagedDataSet = pagedDataSet;
        }

        public async Task<int> GetCaretPosition()
        {
            var items = await PagedDataSet.GetDisplayedDataSource();
            var pos = Math.Min(Math.Max(0, items.Count() - 1), _caretPosition);
            if(_caretPosition != pos)
                await SetCaretPosition(pos);
            return _caretPosition;
        }

        public async Task SetCaretPosition(int newPosition)
        {
            var pageItems = await PagedDataSet.GetPageItems();
            _caretPosition = newPosition.NormalizeIndex(pageItems.Count());
        }

        public async Task<TItem> GetSelectedItem()
             => (await PagedDataSet.GetPageItems()).ElementAtOrDefault(await GetCaretPosition());

        public async Task SetSelectedItem(TItem item)
        {
            var pageSize = await PagedDataSet.GetPageSize();
            var index = (await PagedDataSet.GetDisplayedDataSource()).FindIndex(item);
            if (index == -1) return; //throw new InvalidOperationException("Cannot find the specified item in the data source!");
            await PagedDataSet.SetCurrentPageIndex(index / pageSize);
            var items = await PagedDataSet.GetDisplayedDataSource();
            await SetCaretPosition(index % items.Count());
        }

        async Task<object> ISelectable.GetSelectedItem() => await GetSelectedItem();

        public Task SetSelectedItem(object item)
        {
            if (item is TItem ts)
                return SetSelectedItem(ts);
            else if (item == null)
                return SetSelectedItem((TItem)item);
            else
                throw new ArgumentException("Invalid type argument", nameof(item));
        }

        public async Task IsValidCaretPosition(int position) => NumberHelper.IsInRange(position, 0, (await PagedDataSet.GetPageItems()).Count());
    }
}