using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    public class ListViewInboxInfo : INotifyPropertyChanged
    {
        #region Fields

        private string profileName;
        private string name;
        private string subject;
        private string desc;
        private DateTime date;
        private ImageSource image;
        private bool? isAttached;
        private bool isOpened;
        private bool isImportant;
        private bool isFavorite = true;

        #endregion

        #region Constructor

        public ListViewInboxInfo()
        {

        }

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string ProfileName
        {
            get { return profileName; }
            set
            {
                profileName = value;
                OnPropertyChanged("Title");
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }

        public string Description
        {
            get
            {
                return desc;
            }

            set
            {
                desc = value;
                OnPropertyChanged("Description");
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public ImageSource Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public bool? IsAttached
        {
            get { return isAttached; }
            set
            {
                isAttached = value;
                OnPropertyChanged("IsAttached");
            }
        }

        public bool IsImportant
        {
            get { return isImportant; }
            set
            {
                isImportant = value;
                OnPropertyChanged("IsImportant");
            }
        }

        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                isFavorite = value;
                OnPropertyChanged("IsFavorite");
            }
        }

        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                OnPropertyChanged("IsOpened");
            }
        }

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
