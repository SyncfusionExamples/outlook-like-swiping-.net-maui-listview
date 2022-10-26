using System.Collections.ObjectModel;

namespace ListViewMaui
{
    public class ListViewInboxInfoRepository
    {
        #region Fields

        private Random random = new Random();
        private int dayCount = 0;
        private int monthCount = 0;
        #endregion

        #region Constructor

        public ListViewInboxInfoRepository()
        {

        }

        #endregion

        #region Get inbox info

        internal ObservableCollection<ListViewInboxInfo> GetInboxInfo()
        {
            var empInfo = new ObservableCollection<ListViewInboxInfo>();
            int k = 0;
            for (int i = 0; i < Subject.Count(); i++)
            {
                if (k > 5)
                {
                    k = 0;
                }
                var record = new ListViewInboxInfo()
                {
                    ProfileName = ProfileList[i],
                    Name = NameList[i],
                    Subject = Subject[i],
                    Date = GetDate(i),
                    Description = Descriptions[i],
                    Image = Images[k],
                    IsAttached = Attachments[i],
                    IsImportant = Importants[i],
                    IsOpened = false,
                };
                record.IsFavorite = i < 7 && i % 2 == 0 ? true : false;
                empInfo.Add(record);
                k++;
            }
            return empInfo;
        }

        #endregion

        #region Employee Info

        string[] ProfileList = new string[]
        {
            "JL",
            "FM",
            "JR",
            "NR",
            "LC",
            "BA",
            "BK",
            "LT",
            "RS",
            "XB",
            "HS",
            "BA",
        };

        string[] NameList = new string[]
        {
            "Jenifer Larence",
            "Frank Michael",
            "Junior Richord",
            "Nico Robin",
            "Larry Caden",
            "Barry Allen",
            "Bill Kyle",
            "Logan Texas",
            "Rachiel San",
            "Xaviour Brush",
            "Holly Steve",
            "Benjamin Alexander",
        };

        string[] Images = new string[]
        {
            "bluecircle.png",
            "greencircle.png",
            "lightbluecircle.png",
            "redcircle.png",
            "violetcircle.png",
            "yellowcircle.png",
        };

        bool[] Attachments = new bool[]
        {
            false,
            false,
            false,
            true,
            false,
            true,
            false,
            true,
            true,
            false,
            false,
            true,
        };

        bool[] Importants = new bool[]
        {
            false,
            true,
            false,
            false,
            false,
            false,
            true,
            false,
            false,
            true,
            true,
            false,
        };

        string[] Subject = new string[]
        {
            "Happy birthday to an amazing employee!",
            "Happy New Year!",
            "Get well soon!!",
            "Merry Christmas!",
            "Happy Halloween!",
            "Happy Thanksgiving!!",
            "Happy St Patrick's Day!",
            "Congratulations on the move!",
            "Never doubt yourself. You’re always...",
            "Warmest wishes...",
            "Like a vintage auto, your value increases...",
            "Happy Turkey Day!!",
        };

        string[] Descriptions = new string[] {
            "Happy birthday to one of the best and most loyal employees ever!",
            "May you be blessed with health, wealth, and happiness this new year.",
            "Wishing you a speedy recovery. Get well soon!",
            "Wishing you a happy Christmas. May your Christmas be filled with love, happiness, and prosperity.",
            "Wishing you a night full of frights and a bag full of delights..",
            "Wishing you hope, joy, peace, good health, favor, and love on this Thanksgiving Day!",
            "May you find lots 'o' gold at the end of your rainbow this St. Patrick's Day!",
            "Congratulations! May you find great happiness in your new home.",
            "Never doubt yourself. You’re always the best! Just continue to be like that!",
            "Happy wedding anniversary to you both. You are special.",
            "Wishing you great achievements in this career, And I hope that you have a great day today!",
            "Happy Turkey Day!. Don't forget to give thanks for being so blessed.",
        };

        #endregion

        private DateTime GetDate(int i)
        {
            DateTime yesterday = DateTime.Now;

            if (i < 2)
            {
                var time = DateTime.Now.AddMinutes(-i * 37);
                return time;
            }
            else if (i < 4)
            {
                yesterday = DateTime.Now.AddDays(-1);
                return yesterday;
            }
            else if (i < 8)
            {
                dayCount++;
                var time = yesterday.AddDays(-dayCount);
                return time;
            }
            else
            {
                monthCount++;
                var time = DateTime.Now.AddDays(-monthCount * 15);
                return time;
            }
        }
    }
}