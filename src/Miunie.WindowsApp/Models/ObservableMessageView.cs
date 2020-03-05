using GalaSoft.MvvmLight;
using Miunie.Core.Entities.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miunie.WindowsApp.Models
{
    public class ObservableMessageView : ObservableObject
    {
        public ObservableMessageView()
        {
            Images = new ObservableCollection<ObservableImage>();
        }

        private string _authorName;
        public string AuthorName
        {
            get { return _authorName; }
            set 
            { 
                _authorName = value;
                RaisePropertyChanged(nameof(AuthorName));
            }
        }

        private string _authorAvatarUrl;
        public string AuthorAvatarUrl
        {
            get { return _authorAvatarUrl; }
            set
            {
                _authorAvatarUrl = value;
                RaisePropertyChanged(nameof(AuthorAvatarUrl));
            }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        private DateTimeOffset _timeStamp;
        public DateTimeOffset TimeStamp
        {
            get { return _timeStamp; }
            set
            {
                _timeStamp = value;
                RaisePropertyChanged(nameof(TimeStamp));
            }
        }

        private ObservableCollection<ObservableImage> _images;
        public ObservableCollection<ObservableImage> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }
    }
}
