using ImagiSekaiTechnologies.GenericPaginator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class PagedDataSetBuilder<TItem, TResult> where TItem : class where TResult : class
    {
        private Func<PagedDataSet<TItem, TResult>, Task<string>> _nameSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<int>> _pageSizeSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<int>> _pageIndexSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<bool>> _focusSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<IPageContentGenerator<TItem, TResult>>> _pageContentGenSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<ISelectable<TItem>>> _selectorSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<IMovable<TItem>>> _moverSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<IAddable<TItem>>> _adderSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<IRemovable<TItem>>> _removerSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<IEnumerable<TItem>>> _dataSourceSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<IItemFilter<TItem>>> _itemFilterSetter;
        private Func<PagedDataSet<TItem, TResult>, Task<string>> _filterSetter;


        public virtual async Task<PagedDataSet<TItem, TResult>> BuildPagedDataSource()
        {
            var pds = new PagedDataSet<TItem, TResult>();
            if(_nameSetter != null) await pds.SetName(await _nameSetter(pds));
            if (_pageSizeSetter != null) await pds.SetPageSize(await _pageSizeSetter(pds));
            if (_pageIndexSetter != null) await pds.SetCurrentPageIndex(await _pageIndexSetter(pds));
            if (_focusSetter != null) await pds.SetFocus(await _focusSetter(pds));
            if (_pageContentGenSetter != null) pds.ContentGenerator = await _pageContentGenSetter(pds);
            if (_selectorSetter != null) await pds.SetSelector(await _selectorSetter(pds));
            if (_moverSetter != null) await pds.SetMover(await _moverSetter(pds));
            if (_adderSetter != null) await pds.SetAdder(await _adderSetter(pds));
            if (_removerSetter != null) await pds.SetRemover(await _removerSetter(pds));
            if (_dataSourceSetter != null) await pds.SetDataSource(await _dataSourceSetter(pds));
            if (_itemFilterSetter != null) pds.ItemFilter = await _itemFilterSetter(pds);
            if (_filterSetter != null) await pds.SetFilter(await _filterSetter(pds));
            return pds;
        }

        public PagedDataSetBuilder<TItem, TResult> WithName(string name) => WithName(x => Task.FromResult(name));
        public PagedDataSetBuilder<TItem, TResult> WithName(Func<PagedDataSet<TItem, TResult>, Task<string>> nameSetter)
        {
            _nameSetter = nameSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithInitalPageIndex(int pageIndex) => WithInitalPageIndex(x => Task.FromResult(pageIndex));
        public PagedDataSetBuilder<TItem, TResult> WithInitalPageIndex(Func<PagedDataSet<TItem, TResult>, Task<int>> pageIndexSetter)
        {
            _pageIndexSetter = pageIndexSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithPageSize(int pageSize) => WithPageSize(x => Task.FromResult(pageSize));
        public PagedDataSetBuilder<TItem, TResult> WithPageSize(Func<PagedDataSet<TItem, TResult>, Task<int>> pageSizeSetter)
        {
            _pageSizeSetter = pageSizeSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithFilter(string filter) => WithFilter(x => Task.FromResult(filter));
        public PagedDataSetBuilder<TItem, TResult> WithNoFilter() => WithFilter((Func<PagedDataSet<TItem, TResult>, Task<string>>)null);
        public PagedDataSetBuilder<TItem, TResult> WithFilter(Func<PagedDataSet<TItem, TResult>, Task<string>> filterSetter)
        {
            _filterSetter = filterSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithItemFilter(IItemFilter<TItem> itemFilter) => WithItemFilter(x => Task.FromResult(itemFilter));
        public PagedDataSetBuilder<TItem, TResult> WithItemFilter(Func<PagedDataSet<TItem, TResult>, Task<IItemFilter<TItem>>> itemFilterSetter)
        {
            _itemFilterSetter = itemFilterSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithFocus() => WithFocusState(true);
        public PagedDataSetBuilder<TItem, TResult> WithUnfocus() => WithFocusState(false);
        public PagedDataSetBuilder<TItem, TResult> WithFocusState(bool isFocus) => WithFocusState(x => Task.FromResult(isFocus));
        public PagedDataSetBuilder<TItem, TResult> WithFocusState(Func<PagedDataSet<TItem, TResult>, Task<bool>> focusSetter)
        {
            _focusSetter = focusSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithDataSource(IEnumerable<TItem> dataSource) => WithDataSource(x => Task.FromResult(dataSource));
        public PagedDataSetBuilder<TItem, TResult> WithDataSource(Func<PagedDataSet<TItem, TResult>, Task<IEnumerable<TItem>>> dataSourceSetter)
        {
            _dataSourceSetter = dataSourceSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithSelector(ISelectable<TItem> selector) => WithSelector(x => Task.FromResult(selector));
        public PagedDataSetBuilder<TItem, TResult> WithSelector(Func<PagedDataSet<TItem, TResult>, Task<ISelectable<TItem>>> selectorSetter)
        {
            _selectorSetter = selectorSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithMover(IMovable<TItem> mover) => WithMover(x => Task.FromResult(mover));
        public PagedDataSetBuilder<TItem, TResult> WithMover(Func<PagedDataSet<TItem, TResult>, Task<IMovable<TItem>>> moverSetter)
        {
            _moverSetter = moverSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithAdder(IAddable<TItem> adder) => WithAdder(x => Task.FromResult(adder));
        public PagedDataSetBuilder<TItem, TResult> WithAdder(Func<PagedDataSet<TItem, TResult>, Task<IAddable<TItem>>> adderSetter)
        {
            _adderSetter = adderSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithRemover(IRemovable<TItem> remover) => WithRemover(x =>  Task.FromResult(remover));
        public PagedDataSetBuilder<TItem, TResult> WithRemover(Func<PagedDataSet<TItem, TResult>, Task<IRemovable<TItem>>> removerSetter)
        {
            _removerSetter = removerSetter;
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithDefaultCapabilities()
        {
            WithSelector(x => Task.FromResult(new Selectable<TItem, TResult>(x) as ISelectable<TItem>))
                .WithMover(x => Task.FromResult(new Movable<TItem, TResult>(x) as IMovable<TItem>))
                .WithAdder(x => Task.FromResult(new Addable<TItem, TResult>(x) as IAddable<TItem>))
                .WithRemover(x => Task.FromResult(new Removable<TItem, TResult>(x) as IRemovable<TItem>));
            return this;
        }

        public PagedDataSetBuilder<TItem, TResult> WithStringPageGenerator()
            => WithPageGenerator(x => Task.FromResult(new StringPageContentGenerator<TItem>(x as IPagedDataSet<TItem, string>) as IPageContentGenerator<TItem, TResult>));
        public PagedDataSetBuilder<TItem, TResult> WithStringPageGenerator(IItemProvider<TItem> itemProvider)
            => WithPageGenerator(x => Task.FromResult(new StringPageContentGenerator<TItem>(x as IPagedDataSet<TItem, string>) { ItemProvider = itemProvider } as IPageContentGenerator<TItem, TResult>));
        public PagedDataSetBuilder<TItem, TResult> WithPageGenerator(IPageContentGenerator<TItem, TResult> pageContentGen) => WithPageGenerator(x => Task.FromResult(pageContentGen));
        public PagedDataSetBuilder<TItem, TResult> WithPageGenerator(Func<PagedDataSet<TItem, TResult>, Task<IPageContentGenerator<TItem, TResult>>> pageContentGenSetter)
        {
            _pageContentGenSetter = pageContentGenSetter;
            return this;
        }


    }
}
