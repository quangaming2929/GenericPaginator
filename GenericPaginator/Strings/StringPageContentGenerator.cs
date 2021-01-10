using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    public class StringPageContentGenerator<TItem> : IPageContentGenerator<TItem, string> where TItem : class
    {
        public IItemProvider<TItem> ItemProvider { get; set; } = new ToStringItemProvider<TItem>();
        public IPagedDataSet<TItem, string> PagedDataSet { get; private set; }
        public int DisplayPageOffset { get; set; } = 1;
        public bool UnfocusHighlight { get; set; }
        public string EmojiNextPage { get; set; } = " ▶️";
        public string EmojiPrevPage { get; set; } = "◀️ ";
        public string ItemSeperator { get; set; } = "\n";
        public Func<StringPageContentGenerator<TItem>, string, Task<string>> FocusDecorator 
            = (pcg, header) => Task.FromResult($"-> {header} <-");
        public Func<StringPageContentGenerator<TItem>, TItem, string, Task<string>> HighlighDecorator
            = (pcg, item, content) => Task.FromResult($"**\\> {content}**");

        public StringPageContentGenerator(IPagedDataSet<TItem, string> pagedDataSet)
        {
            PagedDataSet = pagedDataSet;
        }

        public async Task<IPageContent<string>> GeneratePageContent()
        {
            var currentPage = await PagedDataSet.GetCurrentPageIndex();
            var pageCount = await PagedDataSet.GetPageCount();
            var isFocus = await PagedDataSet.GetFocusState();
            var name = await PagedDataSet.GetName();

            var left = currentPage > 0 ? EmojiPrevPage : string.Empty;
            var right = currentPage < pageCount ? EmojiNextPage : string.Empty;

            var header = $"{left}{name} (Page {currentPage + DisplayPageOffset}/{pageCount}){right}";
            if (isFocus) header = await FocusDecorator(this, header);

            var pageItems = await PagedDataSet.GetPageItems();
            header = await OnHeaderGenerated(header, pageItems);

            StringBuilder sb = new StringBuilder();
            int i = 0;
            var sel = await PagedDataSet.GetSelector();
            foreach (var item in pageItems)
            {
                var isSel = sel != null && await sel.GetCaretPosition() == i;
                var content = await ItemProvider.GetItemString(item);
                if (isSel && (isFocus || UnfocusHighlight)) content = await HighlighDecorator(this, item, content);
                if (i > 0) sb.Append(ItemSeperator);
                sb.Append(await OnItemContentGenerated(content, item, isSel, pageItems));
                i++;    
            }

            return new StringPageContent(header, await OnContentGenerated(sb.ToString(), pageItems));
        }

        protected virtual Task<string> OnHeaderGenerated(string header, IEnumerable<TItem> pageItems) => Task.FromResult(header);
        protected virtual Task<string> OnItemContentGenerated(string itemContent, TItem item, bool isSelected, IEnumerable<TItem> pageItems) => Task.FromResult(itemContent);
        protected virtual Task<string> OnContentGenerated(string content, IEnumerable<TItem> pageItems) => Task.FromResult(content);
    }
}