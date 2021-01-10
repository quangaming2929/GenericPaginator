using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public interface IPaginatorController : IList<IPagedDataSet> 
    {
#nullable enable
        Task<IPagedDataSet> GetCurrentFocusPage();
        Task SetFocusPage(IPagedDataSet? page);
        Task MovePagedDataSet(IPagedDataSet page, int index);

        Task<int> GetPageSize();
        Task<int> GetPageIndex();
        Task SetCurrentPageIndex(int index);
        Task<bool> IsValidPageIndex(int index);

        Task<IEnumerable<IPageContent>> GetPageContents();
#nullable restore
    }

    public class PaginatorController : Collection<IPagedDataSet>, IPaginatorController, ISelectable, IMovable, IAddable, IRemovable
    {
        private IPagedDataSet _focused;

        public Task SetFocusPage(IPagedDataSet page)
        {
            if(IndexOf(page) == -1) throw new ArgumentException("the provided page is not exist in this controller!");
            _focused?.SetFocus(false);
            _focused = page;
            _focused.SetFocus(true);
            return Task.CompletedTask;
        }
        public Task<IPagedDataSet> GetCurrentFocusPage() => Task.FromResult(_focused);
        public Task<int> GetPageIndex() => _focused.GetCurrentPageIndex();
        public Task<int> GetPageSize() => _focused.GetPageSize();
        public async Task<bool> IsValidPageIndex(int index) => NumberHelper.IsInRange(index, 0, await GetPageSize()); 
        public Task MovePagedDataSet(IPagedDataSet page, int index)
        {
            var pageIndex = IndexOf(page);
            if (pageIndex == -1) throw new ArgumentException("the provided page is not exist in this controller!");
            base.RemoveItem(pageIndex);
            base.InsertItem(index.NormalizeIndex(Count + 1), page);
            return Task.CompletedTask;
        }
        public Task SetCurrentPageIndex(int index)
        {
            _focused?.SetCurrentPageIndex(index);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<IPageContent>> GetPageContents()
        {
            List<IPageContent> contents = new List<IPageContent>();
            foreach (var item in this)
            {
                contents.Add(await item.GetPageContent());
            }
            return contents;
        }

        public async Task<int> GetCaretPosition() => await (await _focused?.GetSelector())?.GetCaretPosition();
        public async Task SetCaretPosition(int newPosition) => await (await _focused?.GetSelector())?.SetCaretPosition(newPosition);
        public async Task<object> GetSelectedItem() => await (await _focused?.GetSelector())?.GetSelectedItem();
        public async Task SetSelectedItem(object item) => await (await _focused?.GetSelector())?.SetSelectedItem(item);

        public async Task Move(object item, int index) => await (await _focused?.GetMover())?.Move(item, index);
        public async Task Add(object item, int index) => await (await _focused?.GetAdder())?.Add(item, index);
        public async Task Remove(object item) => await (await _focused?.GetRemover())?.Remove(item);
    }

}
