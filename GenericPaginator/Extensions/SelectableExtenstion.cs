using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagiSekaiTechnologies.GenericPaginator.Extensions
{
    public static class SelectableExtenstion
    {
        /// <summary>
        /// Jump the caret to a new position
        /// </summary>
        /// <param name="offset">the number of item to jump. A positive value will jump forward, a negative value will jump backward</param>
        /// <param name="throughPage">should overflow offset move to a new page</param>
        /// <returns></returns>
        public static Task MoveCaretBy<TItem, TResult>(this Selectable<TItem, TResult> selectable, int offset, bool throughPage = false) where TItem : class
            => MoveCaretBy(selectable, selectable.PagedDataSet, offset, throughPage);

        // TODO: BIGTODO Find a better way to move through pages. This is a very smelly code!
        /// <summary>
        /// Jump the caret to a new position
        /// </summary>
        /// <param name="offset">the number of item to jump. A positive value will jump forward, a negative value will jump backward</param>
        /// <param name="dataSet">The data set to move caret</param>
        /// <param name="throughPage">should overflow offset move to a new page</param>
        /// <returns></returns>
        public static async Task MoveCaretBy(this ISelectable selectable, IPagedDataSet dataSet, int offset, bool throughPage = false)
        {
            if (throughPage)
            {
                var pageCount = await dataSet.GetPageCount();
                if (offset > 0)
                {
                    var tempCaret = await selectable.GetCaretPosition();
                    var tempPageIndex = await dataSet.GetCurrentPageIndex();
                    var currentPageSize = (await dataSet.GetPageItems()).Count();
                    if (currentPageSize == 0)
                        return;
                    if (currentPageSize - tempCaret - offset <= 0)
                    {
                        offset -= currentPageSize - tempCaret;
                        tempPageIndex = (tempPageIndex + 1).NormalizeIndex(pageCount);
                    }
                    else
                    {
                        await selectable.SetCaretPosition(tempCaret + offset);
                        return;
                    }

                    while (true)
                    {
                        await dataSet.SetCurrentPageIndex(tempPageIndex);
                        currentPageSize = (await dataSet.GetPageItems()).Count();
                        if (offset - currentPageSize >= 0)
                        {
                            offset -= currentPageSize;
                            tempPageIndex = (tempPageIndex + 1).NormalizeIndex(pageCount);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await dataSet.SetCurrentPageIndex(tempPageIndex);
                    await selectable.SetCaretPosition(offset);
                }
                else
                {
                    var tempCaret = await selectable.GetCaretPosition();
                    var tempPageIndex = await dataSet.GetCurrentPageIndex();
                    int currentPageSize;
                    if (tempCaret + offset < 0)
                    {
                        offset = Math.Abs(offset) - tempCaret - 1;
                        tempPageIndex = (tempPageIndex - 1).NormalizeIndex(pageCount);
                        await dataSet.SetCurrentPageIndex(tempPageIndex);
                        currentPageSize = (await dataSet.GetPageItems()).Count();
                        if (currentPageSize == 0)
                            return;
                        tempCaret = currentPageSize - 1;
                    }
                    else
                    {
                        await selectable.SetCaretPosition(tempCaret + offset);
                        return;
                    }

                    while (true)
                    {
                        if (offset - currentPageSize >= 0)
                        {
                            offset -= currentPageSize;
                            tempPageIndex = (tempPageIndex - 1).NormalizeIndex(pageCount);
                            await dataSet.SetCurrentPageIndex(tempPageIndex);
                            currentPageSize = (await dataSet.GetPageItems()).Count();
                            tempCaret = currentPageSize - 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    await selectable.SetCaretPosition(tempCaret - offset);
                }
            }
            else
            {
                await selectable.SetCaretPosition(await selectable.GetCaretPosition() + offset);
            }
        }
    }
}
