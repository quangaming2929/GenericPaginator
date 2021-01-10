using ImagiSekaiTechnologies.GenericPaginator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class PagedDataSet<TItem, TResult> : IPagedDataSet<TItem, TResult> where TItem : class
    {
        public IPageContentGenerator<TItem, TResult> ContentGenerator { get; set; }
        public IItemFilter<TItem> ItemFilter { get; set; }
        public bool ExtraPage { get; set; }

        private string _name;
        private int _currentPageIndex;
        private int _pageCount;
        private int _pageSize;
        private string _filter;
        private bool _isFocus;
        private IEnumerable<TItem> _dataSource;
        private IEnumerable<TItem> _filteredDataSource;

        public Task<string> GetName() => Task.FromResult(_name);

        public Task SetName(string name)
        {
            _name = name;
            return Task.CompletedTask;
        }

        public Task<int> GetCurrentPageIndex() => Task.FromResult(_currentPageIndex);

        public async Task SetCurrentPageIndex(int index)
        {
            _currentPageIndex = index.NormalizeIndex(await GetPageCount());
        }

        public async Task MovePageBy(int offset) => await SetCurrentPageIndex(await GetCurrentPageIndex() + offset);

        public Task<int> GetPageCount() => Task.FromResult(_pageCount);

        public Task<int> GetPageSize() => Task.FromResult(_pageSize);

        public Task<string> GetFilter() => Task.FromResult(_filter);

        public Task SetFilter(string filter)
        {
            _filter = filter;
            UpdatePageInfo();
            return Task.CompletedTask;
        }

        public Task SetPageSize(int pageSize)
        {
            _pageSize = pageSize;
            UpdatePageInfo();
            return Task.CompletedTask;
        }

        public Task SetFocus(bool isFocused)
        {
            _isFocus = isFocused;
            return Task.CompletedTask;
        }

        public Task<bool> GetFocusState() => Task.FromResult(_isFocus);

        public Task SetDataSource(IEnumerable<TItem> dataSource) 
        {
            _dataSource = dataSource;
            UpdatePageInfo();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TItem>> GetDataSource() => Task.FromResult(_dataSource);

        public Task<IPageContent<TResult>> GetPageContent() => ContentGenerator.GeneratePageContent();

        public Task<IEnumerable<TItem>> GetPageItems() 
            => Task.FromResult(_filteredDataSource.Skip(_currentPageIndex * _pageSize).Take(_pageSize));

        private void UpdatePageInfo()
        {
            if (_dataSource == null) return;
            _filteredDataSource = _dataSource;
            if(ItemFilter != null)
               _filteredDataSource = _filteredDataSource.Where(x => ItemFilter.Filter(x, _filter));
            _pageCount = (int) Math.Ceiling(_filteredDataSource.Count() / (double)_pageSize);
            _currentPageIndex = Math.Min(Math.Max(0, _pageCount - 1), _currentPageIndex);
            OnPageInfoUpdated();
        }

        public Task<IEnumerable<TItem>> GetDisplayedDataSource()
            => Task.FromResult(_filteredDataSource);

        #region Capabilities
        private ISelectable<TItem> _selector;
        private IMovable<TItem> _mover;
        private IAddable<TItem> _adder;
        private IRemovable<TItem> _remover;

        public Task<ISelectable<TItem>> GetSelector() => Task.FromResult(_selector);
        public Task SetSelector(ISelectable<TItem> selector)
        {
            _selector = selector;
            return Task.CompletedTask;
        }
        public Task<IMovable<TItem>> GetMover() => Task.FromResult(_mover);
        public Task SetMover(IMovable<TItem> mover)
        {
            _mover = mover;
            return Task.CompletedTask;
        }
        public Task<IAddable<TItem>> GetAdder() => Task.FromResult(_adder);
        public Task SetAdder(IAddable<TItem> adder)
        {
            _adder = adder;
            return Task.CompletedTask;
        }
        public Task<IRemovable<TItem>> GetRemover() => Task.FromResult(_remover);
        public Task SetRemover(IRemovable<TItem> remover)
        {
            _remover = remover;
            return Task.CompletedTask;
        }
        #endregion

        public event EventHandler<EventArgs> PageInfoUpdated;
        protected virtual void OnPageInfoUpdated() => PageInfoUpdated?.Invoke(this, EventArgs.Empty);

        #region Implements non-generic IPagedDataSet
        async Task<IPageContent> IPagedDataSet.GetPageContent() => await GetPageContent();
        async Task<IEnumerable<object>> IPagedDataSet.GetPageItems() => await GetPageItems();
        async Task<IEnumerable<object>> IPagedDataSet.GetDataSource() => await GetDataSource();
        async Task<IEnumerable<object>> IPagedDataSet.GetDisplayedDataSource() => await GetDisplayedDataSource();
        public Task SetDataSource(IEnumerable<object> items)
        {
            if (items is IEnumerable<TItem> gis)
                return SetDataSource(gis);
            else
                throw new InvalidEnumArgumentException($"Invaid parameter type or parameter {nameof(items)}");
        }
        async Task<ISelectable> IPagedDataSet.GetSelector() => await GetSelector();
        async Task<IMovable> IPagedDataSet.GetMover() => await GetMover();
        async Task<IAddable> IPagedDataSet.GetAdder() => await GetAdder();
        async Task<IRemovable> IPagedDataSet.GetRemover() => await GetRemover();
        #endregion
    }
}