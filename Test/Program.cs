using ImagiSekaiTechnologies.GenericPaginator;
using ImagiSekaiTechnologies.GenericPaginator.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var evenItems = Enumerable.Range(0, 197).Select(x => (x * 2).ToString());
            var oddItems = Enumerable.Range(0, 27).Select(x => (x * 2 + 1).ToString());
            var pdb = new PagedDataSetBuilder<string, string>()
                .WithName("Even page")
                .WithStringPageGenerator()
                .WithDefaultCapabilities()
                .WithDataSource(evenItems)
                .WithPageSize(10)
                .WithItemFilter(new StringItemFilter());
            var evenPd = await pdb.BuildPagedDataSource();
            var oddPd = await pdb.WithDataSource(oddItems)
                .WithName("Odd page")
                .BuildPagedDataSource();
            PaginatorController controller = new PaginatorController();
            controller.Add(evenPd);
            controller.Add(oddPd);
            await controller.SetFocusPage(evenPd);

            var ic = StringComparison.OrdinalIgnoreCase;
            while (true)
            {
                Console.Clear();
                foreach (var item in await controller.GetPageContents())
                {
                    Console.WriteLine("Header".Center(50, '-'));
                    Console.WriteLine(await item.GetHeader());
                    Console.WriteLine("Content".Center(50, '-'));
                    Console.WriteLine(await item.GetContent());
                    Console.WriteLine("-".Multiply(50));
                }

                var cmd = Console.ReadKey(true);
                if (cmd.Key == ConsoleKey.Q && cmd.Modifiers == ConsoleModifiers.Shift)
                {
                    var pds = await controller.GetCurrentFocusPage();
                    await controller.MovePagedDataSetOffset(pds, -1);
                }
                else if (cmd.Key == ConsoleKey.E && cmd.Modifiers == ConsoleModifiers.Shift)
                {
                    var pds = await controller.GetCurrentFocusPage();
                    await controller.MovePagedDataSetOffset(pds, 1);
                }
                else if (cmd.Key == ConsoleKey.Q)
                    await controller.SetFocusPageAtOffset(-1);
                else if (cmd.Key == ConsoleKey.E)
                    await controller.SetFocusPageAtOffset(1);
                else if (cmd.Key == ConsoleKey.D)
                    await controller.MoveCurrentPageIndex(1);
                else if (cmd.Key == ConsoleKey.A)
                    await controller.MoveCurrentPageIndex(-1);
                else if (cmd.Key == ConsoleKey.W && cmd.Modifiers == ConsoleModifiers.Shift)
                {
                    var sel = await controller.GetSelectedItem();
                    await controller.MoveOffset(sel, -1);
                    await controller.SetSelectedItem(sel);
                }
                else if (cmd.Key == ConsoleKey.S && cmd.Modifiers == ConsoleModifiers.Shift)
                {
                    var sel = await controller.GetSelectedItem();
                    await controller.MoveOffset(sel, 1);
                    await controller.SetSelectedItem(sel);
                }
                else if (cmd.Key == ConsoleKey.W)
                    await controller.MoveCaretBy(-1, true);
                else if (cmd.Key == ConsoleKey.S)
                    await controller.MoveCaretBy(1, true);
                else if (cmd.Key == ConsoleKey.Enter && cmd.Modifiers == ConsoleModifiers.Shift)
                    await controller.RemoveAtOffset(0);
                else if (cmd.Key == ConsoleKey.Enter)
                    await controller.AddOffset(DateTime.Now.ToString(), 0);
                else if (cmd.Key == ConsoleKey.Spacebar)
                    await controller.SetSelectedItem("20");


                else if (cmd.KeyChar == '/')
                {
                    Console.WriteLine("Line Command: ");
                    string command = Console.ReadLine();
                    if(command.StartsWith("f", ic))
                    {
                        var sp = command.Split(' ');
                        if (sp.Length > 1) await (await controller.GetCurrentFocusPage()).SetFilter(sp.Skip(1).Aggregate((l, r) => $"{l} {r}"));
                        else await (await controller.GetCurrentFocusPage()).SetFilter(string.Empty);
                    }
                }
            }
        } 


        static async Task OldTestCode(IPagedDataSet eventPd)
        {
            while (true)
            {
                Console.Clear();
                var t = await eventPd.GetPageContent();
                Console.WriteLine($"Header: {await t.GetHeader()}");
                Console.WriteLine($"Content: {await t.GetContent()}");

                Console.Write("Command: ");
                var input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.RightArrow)
                    await eventPd.MovePageBy(1);
                else if (input.Key == ConsoleKey.LeftArrow)
                    await eventPd.MovePageBy(-1);
                else if (input.Key == ConsoleKey.DownArrow)
                    await (await eventPd.GetSelector()).MoveCaretBy(eventPd, 1, true);
                else if (input.Key == ConsoleKey.UpArrow)
                    await (await eventPd.GetSelector()).MoveCaretBy(eventPd, -1, true);
                else if (input.Key == ConsoleKey.Spacebar)
                    await (await eventPd.GetSelector()).SetSelectedItem("20");
                else if (input.Key == ConsoleKey.A)
                    await (await eventPd.GetAdder()).AddOffset(eventPd, $"{DateTime.Now}", 0);
                else if (input.Key == ConsoleKey.D)
                    await (await eventPd.GetAdder()).Add($"{DateTime.Now}", 0);
                else if (input.Key == ConsoleKey.X)
                    await (await eventPd.GetRemover()).RemoveAtOffset(eventPd, 0);
                else if (input.Key == ConsoleKey.Z)
                    await (await eventPd.GetRemover()).RemoveAt(eventPd, 0);
                else if (input.Key == ConsoleKey.B)
                    await (await eventPd.GetRemover()).Remove("20");
                else if (input.Key == ConsoleKey.W)
                {
                    await (await eventPd.GetMover()).MoveOffset(eventPd, await (await eventPd.GetSelector()).GetSelectedItem(), -1);
                    await (await eventPd.GetSelector()).MoveCaretBy(eventPd, -1);
                }
                else if (input.Key == ConsoleKey.Q)
                {
                    await (await eventPd.GetMover()).MoveToTop(eventPd, await (await eventPd.GetSelector()).GetSelectedItem());
                    await (await eventPd.GetSelector()).SetCaretPosition(0);
                    await eventPd.SetCurrentPageIndex(0);
                }
                else if (input.Key == ConsoleKey.E)
                {
                    await (await eventPd.GetMover()).MoveToLast(eventPd, await (await eventPd.GetSelector()).GetSelectedItem());
                    await eventPd.SetCurrentPageIndex(await eventPd.GetPageCount() - 1);
                    await (await eventPd.GetSelector()).SetCaretPosition((await eventPd.GetPageItems()).Count() - 1);
                }
                else if (input.Key == ConsoleKey.S)
                {
                    await (await eventPd.GetMover()).MoveOffset(eventPd, await (await eventPd.GetSelector()).GetSelectedItem(), 1);
                    await (await eventPd.GetSelector()).MoveCaretBy(eventPd, 1);
                }
                else if (input.Key == ConsoleKey.Escape)
                    break;
            }
        }
    }

    public static class StringHelper
    {
        /// <summary>
        /// Imitating center() in Python
        /// </summary>
        /// <returns></returns>
        public static string Center(this string content, int length, char character = ' ')
        {
            var strChr = character.ToString();
            var ls = Math.Max(0, (length - content.Length) / 2);
            var rs = Math.Max(0, length - content.Length - ls);
            return $"{strChr.Multiply(ls)}{content}{strChr.Multiply(rs)}";
        }

        public static string Multiply(this string input, int multiplier)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(input);
            }

            return sb.ToString();
        }
    }
}
