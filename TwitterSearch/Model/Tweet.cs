using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TwitterSearch
{
    public class Tweet : NotifyBase
    {
        private string _author;
        public string Author { get
        {
            return _author;
        }
            set
            {
                if (_author != value)
                {
                    _author = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                if (_body != value)
                {
                    _body = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _publishDate;
        public DateTime PublishDate
        {
            get
            { return _publishDate; }
            set
            {
                if (_publishDate != value)
                {
                    _publishDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Id;
        public string ID
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _avatarUrl;
        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                if (_avatarUrl != value)
                { _avatarUrl = value;
                NotifyPropertyChanged();
                }
            }
        }

        private string _geo;
        public string Geo
        {
            get
            {
                return _geo;
            }
            set
            {
                if (_geo != value)
                {
                    _geo = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }

}
