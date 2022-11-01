using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.DataSource.Extensions;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.ListView.Helpers;
using System.ComponentModel;
using System.Reflection;

#nullable disable
namespace ListViewMaui
{
    public class ListViewSwipingBehavior : Behavior<ContentPage>
    {
        private SfListView ListView;
        private ViewModel ViewModel;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            ViewModel = new ViewModel();
            bindable.BindingContext = ViewModel;
            ListView = bindable.FindByName<SfListView>("listView");
            ListView.PropertyChanged += ListView_PropertyChanged;
            ListView.ItemTapped += ListView_ItemTapped;
            ListView.QueryItemSize += ListView_QueryItemSize;
            ListView.SwipeEnded += ListView_SwipeEnded;
            ListView.DataSource.SortDescriptors.Add(new SortDescriptor()
            {
                PropertyName = "Date",
                Direction = Syncfusion.Maui.DataSource.ListSortDirection.Descending,
            });
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "Date",
                KeySelector = (obj) =>
                {
                    var groupName = ((InboxInfo)obj).Date;
                    return GetKey(groupName);
                },
                Comparer = new CustomGroupComparer(),
            });
            ListView.DataSource.LiveDataUpdateMode = LiveDataUpdateMode.AllowDataShaping;
            base.OnAttachedTo(bindable);
        }
        private void ListView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Width" && ListView.Orientation == ItemsLayoutOrientation.Vertical && ListView.SwipeOffset != ListView.Width)
                ListView.SwipeOffset = ListView.Width;
            else if (e.PropertyName == "Height" && ListView.Orientation == ItemsLayoutOrientation.Horizontal && ListView.SwipeOffset != ListView.Height)
                ListView.SwipeOffset = ListView.Height;
        }

        private void ListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            (e.DataItem as InboxInfo).IsOpened = true;
        }

        private async void ListView_SwipeEnded(object sender, Syncfusion.Maui.ListView.SwipeEndedEventArgs e)
        {
            if (e.Offset <= 100)
            {
                return;
            }

            if (e.Direction == SwipeDirection.Right)
            {
                // Adding Delay in order to maintain the Swiping animaion before Archiving an Item.
                await Task.Delay(400);
                ViewModel.ArchiveCommand.Execute(e.DataItem);
            }

            if (e.Direction == SwipeDirection.Left)
            {
                // Adding Delay in order to maintain the Swiping animaion before Deleting an Item.
                await Task.Delay(400);
                ViewModel.DeleteCommand.Execute(e.DataItem);
            }
        }

        private void ListView_QueryItemSize(object sender, QueryItemSizeEventArgs e)
        {
            if (e.ItemType == ItemType.GroupHeader && e.ItemIndex == 0)
            {
                var groupName = e.DataItem as GroupResult;

                if (groupName != null && (GroupName)groupName.Key == GroupName.Today)
                {
                    e.ItemSize = 0;
                    e.Handled = true;
                }
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView.ItemTapped -= ListView_ItemTapped;
            ListView.PropertyChanged -= ListView_PropertyChanged;
            ListView.QueryItemSize -= ListView_QueryItemSize;
            ListView.SwipeEnded -= ListView_SwipeEnded;
            ListView = null;
            ViewModel = null;
            base.OnDetachingFrom(bindable);
        }

        private GroupName GetKey(DateTime groupName)
        {
            int compare = groupName.Date.CompareTo(DateTime.Now.Date);

            if (compare == 0)
            {
                return GroupName.Today;
            }
            else if (groupName.Date.CompareTo(DateTime.Now.AddDays(-1).Date) == 0)
            {
                return GroupName.Yesterday;
            }
            else if (IsLastWeek(groupName))
            {
                return GroupName.LastWeek;
            }
            else if (IsThisWeek(groupName))
            {
                return GroupName.ThisWeek;
            }
            else if (IsThisMonth(groupName))
            {
                return GroupName.ThisMonth;
            }
            else if (IsLastMonth(groupName))
            {
                return GroupName.LastMonth;
            }
            else
            {
                return GroupName.Older;
            }
        }

        private bool IsThisWeek(DateTime groupName)
        {
            var groupWeekSunDay = groupName.AddDays(-(int)groupName.DayOfWeek).Day;
            var currentSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).Day;

            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.Month;

            var isCurrentYear = groupName.Year == DateTime.Today.Year;

            return currentSunday == groupWeekSunDay && (groupMonth == currentMonth || groupMonth == currentMonth - 1) && isCurrentYear;
        }

        private bool IsLastWeek(DateTime groupName)
        {
            var groupWeekSunDay = groupName.AddDays(-(int)groupName.DayOfWeek).Day;
            var lastSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).Day - 7;

            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.Month;

            var isCurrentYear = groupName.Year == DateTime.Today.Year;

            return lastSunday == groupWeekSunDay && (groupMonth == currentMonth || groupMonth == currentMonth -1) && isCurrentYear;
        }

        private bool IsThisMonth(DateTime groupName)
        {
            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.Month;

            var isCurrentYear = groupName.Year == DateTime.Today.Year;

            return groupMonth == currentMonth && isCurrentYear;
        }

        private bool IsLastMonth(DateTime groupName)
        {
            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.AddMonths(-1).Month;

            var isCurrentYear = groupName.Year == DateTime.Today.Year;

            return groupMonth == currentMonth && isCurrentYear ;
        }
    }

    public enum GroupName
    {
        Today = 0,
        Yesterday,
        ThisWeek,
        LastWeek,
        ThisMonth,
        LastMonth,
        Older
    }
}
