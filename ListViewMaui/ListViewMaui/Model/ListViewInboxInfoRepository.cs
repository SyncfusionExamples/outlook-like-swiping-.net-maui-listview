using System.Collections.ObjectModel;

namespace ListViewMaui
{
    public class ListViewInboxInfoRepository
    {
        #region Fields

        private Random random = new Random();

        #endregion

        #region Constructor

        public ListViewInboxInfoRepository()
        {

        }

        #endregion

        #region Get inbox info

        internal ObservableCollection<InboxInfo> GetInboxInfo()
        {
            var empInfo = new ObservableCollection<InboxInfo>();
            int k = 0;
            for (int i = 0; i < Subject.Count(); i++)
            {
                if (k > 5)
                {
                    k = 0;
                }
                var record = new InboxInfo()
                {
                    ProfileName = ProfileList[i],
                    Name = NameList[i],
                    Subject = Subject[i],
                    Date = dates[i],
                    Description = Descriptions[i],
                    Image = Images[k],
                    IsAttached = Attachments[i],
                    IsImportant = Importants[i],
                    IsOpened = Opens[i],
                };
                empInfo.Add(record);
                k++;
            }
            return empInfo;
        }

        #endregion

        #region Employee Info

        string[] ProfileList = new string[]
        {
            "M",
            "MV",
            "MV",
            "T",
            "M",
            "LI",
            "M",
            "M",
            "SO",
            "OT",
            "MO",
            "MA",
            "BT",
            "M",
            "M",
        };

        string[] NameList = new string[]
        {
            "Microsoft",
            "Microsoft Viva",
            "Microsoft Viva",
            "Twitter",
            "Microsoft",
            "LinkedIn",
            "Microsoft",
            "Microsoft",
            "Stack Overflow",
            "Outlook Team",
            "Microsoft Outlook",
            "My Analytics",
            "Blog Team Site",
            "Microsoft",
            "Microsoft",
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
            false,
            true,
            false,
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
            true,
            false,
            false,
        };

        bool[] Opens = new bool[]
        {
            true,
            false,
            true,
            false,
            false,
            true,
            false,
            false,
            true,
            false,
            true,
            false,
            false,
            true,
            false,
        };

        string[] Subject = new string[]
        {
            "Dev Essentials: Learn about the future of .NET and celebrate Visual Studio's 25th anniversary",
            "Your daily briefing",
            "Your digest email",
            "Be more recognizable",
            "Dev Essentials: Announcing .NET Multiplatform App UI is generally available",
            "You have two new messages",
            "Start learning .NET MAUI and discover a new AI pair programmer",
            "Dev Essentials: Learn how to code with Java",
            "Your friendly, fear-free guide to getting started",
            "Get to know what's new in Outlook",
            "Microsoft Outlook test Message",
            "My Analytics | Collaboration Edition",
            "You've joined the Blog Team Site group",
            "Microsoft .NET News: Get started with .NET 6.0 and watch sessions from .NET Conf 2022 on demand",
            "Microsoft .NET News: Learn about new tools and updates for .NET developers",
        };

        string[] Descriptions = new string[] {
            "Developer news, updates, and training resources",
            "Dear developer, It's almost the end if the week",
            "Dear developer, discover trends in yout work habits",
            "Stand out with a profile photo",
            "On codebase, many platforms: .NET Multiplatform App UI is generally available",
            "You have two new messages",
            "Explore resources to get started with .NET MAUI",
            "Get started: Java for beginners",
            "How to learn and shart on Stack Overflow",
            "Hello and welcome to Outlook",
            "This is an email message sent automatically by Microsoft Outlook while testing settings of your account",
            "Descover your habits. Work smarter.",
            "Welcome to the Blog Team Site group",
            "The Xamarin Newsletter is now .NET News",
            "Now available: Visual Studio 2019 version 16.9",
        };

        #endregion

        DateTime[] dates = new DateTime[]
        {
            new DateTime(2022, 03, 25, 12, 00, 00),
            new DateTime(2022, 10, 28, 08, 38, 00),
            new DateTime(2022, 10, 28, 03, 10, 00),
            new DateTime(2022, 10, 12, 12, 10, 00),
            new DateTime(2022, 06, 25, 12, 10, 00),
            new DateTime(2022, 10, 20, 10, 10, 00),
            new DateTime(2022, 07, 20, 10, 10, 00),
            new DateTime(2022, 05, 18, 12, 10, 00),
            new DateTime(2022, 01, 25, 12, 10, 00),
            new DateTime(2022, 01, 29, 12, 10, 00),
            new DateTime(2022, 10, 28, 03, 25, 00),
            new DateTime(2022, 08, 23, 12, 00, 00),
            new DateTime(2022, 01, 04, 12, 00, 00),
            new DateTime(2021, 11, 19, 12, 00, 00),
            new DateTime(2021, 12, 12, 12, 00, 00),
        };
    }
}