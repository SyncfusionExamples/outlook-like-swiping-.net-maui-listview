using Syncfusion.Maui.DataSource.Extensions;

namespace ListViewMaui
{
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
}
