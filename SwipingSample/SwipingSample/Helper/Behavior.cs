using SwipingSample.Model;
using SwipingSample.ViewModel;
using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.ListView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SwipingSample.ViewModel.ListViewSwipingViewModel;

#nullable disable
namespace SwipingSample.Helper
{
    public class ListViewSwipingBehavior : Behavior<ContentPage>
    {
        private SfListView ListView;
        private ListViewSwipingViewModel ViewModel;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            ViewModel = new ListViewSwipingViewModel();
            bindable.BindingContext = ViewModel;
            ListView = bindable.FindByName<SfListView>("listView");
            (bindable.BindingContext as ListViewSwipingViewModel).ResetSwipeView += ListViewSwipingBehavior_ResetSwipeView;
            ListView.PropertyChanged += ListView_PropertyChanged;
            ListView.ItemTapped += ListView_ItemTapped;
            ListView.QueryItemSize += ListView_QueryItemSize;
            ListView.SwipeEnded += ListView_SwipeEnded;
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "Date",
                KeySelector = (obj) =>
                {
                    var groupName = ((ListViewInboxInfo)obj).Date;

                    return SetGroup(groupName);
                }
            });
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
            (e.DataItem as ListViewInboxInfo).IsOpened = true;
        }

        private void ListViewSwipingBehavior_ResetSwipeView(object sender, ResetEventArgs e)
        {
            ListView!.ResetSwipeItem();
        }

        private async void ListView_SwipeEnded(object sender, Syncfusion.Maui.ListView.SwipeEndedEventArgs e)
        {
            if (e.Offset <= 100)
            {
                return;
            }

            if (e.Direction == SwipeDirection.Right)
            {
                await Task.Delay(400);
                ViewModel.ArchiveCommand.Execute(null);
                ViewModel.InboxInfo.Remove(e.DataItem as ListViewInboxInfo);
            }

            if (e.Direction == SwipeDirection.Left)
            {
                await Task.Delay(400);
                ViewModel.DeleteImageCommand.Execute(null);
                ViewModel.InboxInfo.Remove(e.DataItem as ListViewInboxInfo);
            }
        }

        private void ListView_QueryItemSize(object sender, QueryItemSizeEventArgs e)
        {
            if (e.ItemType == ItemType.GroupHeader && e.ItemIndex == 0)
            {
                e.ItemSize = 0;
                e.Handled = true;
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            (bindable.BindingContext as ListViewSwipingViewModel).ResetSwipeView -= ListViewSwipingBehavior_ResetSwipeView;
            ListView.ItemTapped -= ListView_ItemTapped;
            ListView = null;
            ViewModel = null;
            base.OnDetachingFrom(bindable);
        }

        private string SetGroup(DateTime groupName)
        {
            int compare = groupName.Date.CompareTo(DateTime.Now.Date);

            if (compare == 0)
            {
                return "Today";
            }
            else if (groupName.Date.CompareTo(DateTime.Now.AddDays(-1).Date) == 0)
            {
                return "Yesterday";
            }
            else if (IsThisWeek(groupName))
            {
                return "This Week";
            }
            else if (IsLastWeek(groupName))
            {
                return "Last Week";
            }
            else if (IsThisMonth(groupName))
            {
                return "This Month";
            }
            else if (IsLastMonth(groupName))
            {
                return "Last Month";
            }
            else
            {
                return "Older";
            }
        }

        private bool IsThisWeek(DateTime groupName)
        {
            var groupWeekSunDay = groupName.AddDays(-(int)groupName.DayOfWeek).Day;
            var currentSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).Day;

            return currentSunday == groupWeekSunDay;
        }

        private bool IsLastWeek(DateTime groupName)
        {
            var groupWeekSunDay = groupName.AddDays(-(int)groupName.DayOfWeek).Day;
            var lastSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).Day - 7;

            return lastSunday == groupWeekSunDay;
        }

        private bool IsThisMonth(DateTime groupName)
        {
            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.Month;

            return groupMonth == currentMonth;
        }

        private bool IsLastMonth(DateTime groupName)
        {
            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.AddMonths(-1).Month;

            return groupMonth == currentMonth;
        }
    }
}
