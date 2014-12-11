using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows;
using System.ComponentModel;

namespace StandartObjectLibrary
{
    [ContentProperty("Items")]
    public abstract class Fill<T1, T2> : INotifyPropertyChanged where T1 : new()
    {
        private T1 m_Items = new T1();
        public T1 Items
        {
            get { return m_Items; }
            set 
            { 
                m_Items = value;
                NotifyPropertyChanged("Items");
            }
        }

        public abstract T2 this[double value]
        {
            get;
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}