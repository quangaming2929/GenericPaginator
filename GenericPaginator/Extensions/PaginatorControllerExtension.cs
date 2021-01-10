using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator.Extensions
{
    public static class PaginatorControllerExtension
    {
        public static async Task SetFocusPageAtOffset(this IPaginatorController pie, int offset)
        {
            var ci = pie.IndexOf(await pie.GetCurrentFocusPage());
            await (ci != -1 ? pie.SetFocusPageAt(ci + offset) : pie.SetFocusPageAt(offset));
        }

        public static Task SetFocusPageAt(this IPaginatorController pie, int index) 
            => pie.SetFocusPage(pie[index.NormalizeIndex(pie.Count)]);

        public static Task MovePagedDataSetOffset(this IPaginatorController pie, IPagedDataSet pd, int offset)
        {
            var index = pie.IndexOf(pd);
            return pie.MovePagedDataSet(pd, index != -1 ? index + offset : offset);
        }

        public static async Task MoveCurrentPageIndex(this IPaginatorController pie, int offset)
            => await pie.SetCurrentPageIndex((await pie.GetPageIndex()) + offset);

        public static async Task<TItem> GetSelectedItem<TItem>(this PaginatorController pie) => (TItem)await pie.GetSelectedItem();
        public static async Task MoveCaretBy(this PaginatorController pie, int offset, bool throughPage = false)
            => await pie.MoveCaretBy(await pie.GetCurrentFocusPage(), offset, throughPage);

        public static async Task MoveOffset(this PaginatorController pie, object item, int offset) 
            => await pie.MoveOffset(await pie.GetCurrentFocusPage(), item, offset);
        public static async Task MoveToTop(this PaginatorController pie, object item) 
            => await pie.MoveToTop(await pie.GetCurrentFocusPage(), item);
        public static async Task MoveToLast(this PaginatorController pie, object item) 
            => await pie.MoveToLast(await pie.GetCurrentFocusPage(), item);
        public static async Task AddOffset(this PaginatorController pie, object item, int offset)
            => await pie.AddOffset(await pie.GetCurrentFocusPage(), item, offset);
        public static async Task RemoveAt(this PaginatorController pie, int index)
            => await pie.RemoveAt(await pie.GetCurrentFocusPage(), index);
        public static async Task RemoveAtOffset(this PaginatorController pie, int offset)
            => await pie.RemoveAtOffset(await pie.GetCurrentFocusPage(), offset);

    }
}
