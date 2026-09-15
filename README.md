# Achieve Outlook-Like Swiping Using .NET MAUI ListView
This repository controls the project that explains how to achieve Outlook-like swiping using the Syncfusion .NET MAUI ListView control.

## Blog reference
[Achieve Outlook-Like Swiping Using .NET MAUI ListView](https://www.syncfusion.com/blogs/post/achieve-outlook-like-swiping-using-net-maui-listview.aspx)

## Sample

```xaml
<ListView:SfListView Grid.Row="1"
                    x:Name="listView"
                    ItemsSource="{Binding InboxInfos}"
                    AllowSwiping="True"
                    SwipeThreshold="100"
                    ItemSize="70"
                    SelectionMode="Multiple"
                    SelectionGesture="LongPress"
                    ScrollBarVisibility="Always"
                    AutoFitMode="Height">

    <ListView:SfListView.StartSwipeTemplate>
        <DataTemplate>
            <code>
            . . .
            . . .
            <code>
        </DataTemplate>
    </ListView:SfListView.StartSwipeTemplate>
    <ListView:SfListView.EndSwipeTemplate>
        <DataTemplate>
            <code>
            . . .
            . . .
            <code>
        </DataTemplate>
    </ListView:SfListView.EndSwipeTemplate>

    <ListView:SfListView.GroupHeaderTemplate>
        <DataTemplate x:Name="GroupHeaderTemplate">
            <code>
            . . .
            . . .
            <code>
        </DataTemplate>
    </ListView:SfListView.GroupHeaderTemplate>

    <ListView:SfListView.ItemTemplate>
        <DataTemplate>
            <code>
            . . .
            . . .
            <code>
        </DataTemplate>
    </ListView:SfListView.ItemTemplate>
</ListView:SfListView>

C#:

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
        await Task.Delay(400);
        ViewModel.ArchiveCommand.Execute(e.DataItem);
    }

    if (e.Direction == SwipeDirection.Left)
    {
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

public class CustomGroupComparer : IComparer<GroupResult>
{
    public int Compare(GroupResult x, GroupResult y)
    {
        var xenum = (GroupName)x.Key;
        var yenum = (GroupName)y.Key;

        if (xenum.CompareTo(yenum) == -1)
        {
            return -1;
        }
        else if (xenum.CompareTo(yenum) == 1)
        {
            return 1;
        }

        return 0;
    }
}

ViewModel.cs:

deleteCommand = new Command(OnDelete);
undoCommand = new Command(OnUndo);
archiveCommand = new Command(OnArchive);

private async void OnDelete(object item)
{
    PopUpText = "Deleted";
    listViewItem = (InboxInfo)item;
    listViewItemIndex = inboxInfos.IndexOf(listViewItem);
    inboxInfos!.Remove(listViewItem);
    IsDeleted = true;
    await Task.Delay(3000);
    IsDeleted = false;
}

private async void OnArchive(object item)
{
    PopUpText = "Archived";
    listViewItem = (InboxInfo)item;
    listViewItemIndex = inboxInfos.IndexOf(listViewItem);
    inboxInfos!.Remove(listViewItem);
    archivedMessages!.Add(listViewItem);
    IsDeleted = true;
    await Task.Delay(3000);
    IsDeleted = false;
}

private void OnUndo()
{
    IsDeleted = false;

    if (listViewItem != null)
    {
        inboxInfos!.Insert(listViewItemIndex, listViewItem);

        var archivedItem = archivedMessages.Where(x => x.Name.Equals(listViewItem.Name));

        if (archivedItem != null)
        {
            foreach (var item in archivedItem)
            {
                archivedMessages.Remove(listViewItem);
                break;
            }
        }
    }

    listViewItemIndex = 0;
    listViewItem = null;
}
```
