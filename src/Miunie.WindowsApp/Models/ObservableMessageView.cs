// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace Miunie.WindowsApp.Models
{
    public class ObservableMessageView : ObservableObject
    {
        private string _authorName;
        private string _authorAvatarUrl;
        private string _content;
        private DateTimeOffset _timeStamp;
        private ObservableCollection<ObservableImage> _images;

        public ObservableMessageView()
        {
            Images = new ObservableCollection<ObservableImage>();
        }

        public string AuthorName
        {
            get => _authorName;
            set
            {
                _authorName = value;
                RaisePropertyChanged(nameof(AuthorName));
            }
        }

        public string AuthorAvatarUrl
        {
            get => _authorAvatarUrl;
            set
            {
                _authorAvatarUrl = value;
                RaisePropertyChanged(nameof(AuthorAvatarUrl));
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        public DateTimeOffset TimeStamp
        {
            get => _timeStamp;
            set
            {
                _timeStamp = value;
                RaisePropertyChanged(nameof(TimeStamp));
            }
        }

        public ObservableCollection<ObservableImage> Images
        {
            get => _images;
            set
            {
                _images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }
    }
}
