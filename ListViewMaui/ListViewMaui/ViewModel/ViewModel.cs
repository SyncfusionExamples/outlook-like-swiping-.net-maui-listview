using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.DataSource.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

#nullable disable
namespace ListViewMaui
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<InboxInfo> inboxInfos;
        private ObservableCollection<InboxInfo> archivedMessages;
        private Command undoCommand;
        private Command deleteCommand;
        private bool? isDeleted;
        private InboxInfo listViewItem;
        private Command archiveCommand;
        private string popUpText;
        private int listViewItemIndex;
        public GroupResult itemGroup;

        #endregion


        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Constructor

        public ViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<InboxInfo> InboxInfos
        {
            get { return inboxInfos; }
            set { inboxInfos = value; OnPropertyChanged("InboxInfos"); }
        }

        public ObservableCollection<InboxInfo> ArchivedMessages
        {
            get { return archivedMessages; }
            set { archivedMessages = value; OnPropertyChanged("ArchivedMessages"); }
        }
        public Command DeleteCommand
        {
            get { return deleteCommand; }
            protected set { deleteCommand = value; }
        }

        public Command UndoCommand
        {
            get { return undoCommand; }
            protected set { undoCommand = value; }
        }

        public Command ArchiveCommand
        {
            get { return archiveCommand; }
            protected set { archiveCommand = value; }
        }

        public bool? IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; OnPropertyChanged("IsDeleted"); }
        }

        public string PopUpText
        {
            get { return popUpText; }
            set { popUpText = value; OnPropertyChanged("PopUpText"); }
        }


        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            IsDeleted = false;
            ListViewInboxInfoRepository inboxinfo = new ListViewInboxInfoRepository();
            archivedMessages = new ObservableCollection<InboxInfo>();
            inboxInfos = inboxinfo.GetInboxInfo();
            deleteCommand = new Command(OnDelete);
            undoCommand = new Command(OnUndo);
            archiveCommand = new Command(OnArchive);
        }

        private async void OnDelete(object item)
        {
            PopUpText = "Deleted";
            listViewItem = (InboxInfo)item;
            listViewItemIndex = inboxInfos.IndexOf(listViewItem);
            inboxInfos!.Remove(listViewItem);
            IsDeleted = true;

            // Added Delay in order to maintain the Delete message popUp at screen bottom.
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

            // Added Delay in order to maintain the Archive message popUp at screen bottom.
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
        #endregion
    }
}

