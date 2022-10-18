using SwipingSample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SwipingSample.ViewModel
{
    public class ListViewSwipingViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<ListViewInboxInfo> inboxInfo;
        private ObservableCollection<ListViewInboxInfo> archivedMessages;
        private Command undoCommand;
        private Command deleteImageCommand;
        private bool? isDeleted;
        private ListViewInboxInfo listViewItem;
        private int listViewItemIndex;
        private Command archiveCommand;
        private string popUpText;

        #endregion


        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region EventHandler
        public event EventHandler<ResetEventArgs> ResetSwipeView;

        protected virtual void OnResetSwipe(ResetEventArgs e)
        {
            EventHandler<ResetEventArgs> handler = ResetSwipeView;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Constructor

        public ListViewSwipingViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<ListViewInboxInfo> InboxInfo
        {
            get { return inboxInfo; }
            set { inboxInfo = value; OnPropertyChanged("InboxInfo"); }
        }

        public ObservableCollection<ListViewInboxInfo> ArchivedMessages
        {
            get { return archivedMessages; }
            set { archivedMessages = value; OnPropertyChanged("ArchivedMessages"); }
        }
        public Command DeleteImageCommand
        {
            get { return deleteImageCommand; }
            protected set { deleteImageCommand = value; }
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
            archivedMessages = new ObservableCollection<ListViewInboxInfo>();
            inboxInfo = inboxinfo.GetInboxInfo();
            deleteImageCommand = new Command(Delete);
            undoCommand = new Command(UndoAction);
            archiveCommand = new Command(Archive);
        }

        private async void Delete(object item)
        {
            PopUpText = "Deleted";
            listViewItem = (ListViewInboxInfo)item;
            listViewItemIndex = inboxInfo!.IndexOf(listViewItem);
            inboxInfo!.Remove(listViewItem);
            IsDeleted = true;
            await Task.Delay(3000);
            IsDeleted = false;
        }

        private async void Archive(object item)
        {
            PopUpText = "Archived";
            listViewItem = (ListViewInboxInfo)item;
            listViewItemIndex = inboxInfo!.IndexOf(listViewItem);
            inboxInfo!.Remove(listViewItem);
            archivedMessages!.Add(listViewItem);
            IsDeleted = true;
            await Task.Delay(3000);
            IsDeleted = false;
        }

        private void UndoAction()
        {
            IsDeleted = false;
            if (listViewItem != null)
            {
                inboxInfo!.Insert(listViewItemIndex, listViewItem);
            }
            listViewItemIndex = 0;
            listViewItem = null;
        }

        #endregion

        #region ResetEvent
        public class ResetEventArgs : EventArgs
        {

        }
        #endregion
    }
}

