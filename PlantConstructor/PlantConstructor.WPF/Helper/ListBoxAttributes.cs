using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PlantConstructor.WPF.Helper
{
    public class ListBoxAttributes :INotifyPropertyChanged
    {
		private string item;

		public string Item
		{
			get { return item; }
			set {
				
				item = value;
				this.RaisePropertyChanged("Item");
			}
		}

		private bool isSelected;

		public bool IsSelected
		{
			get { return isSelected; }
			set { isSelected = value;
				this.RaisePropertyChanged("IsSelected");

			}
		}

		public override string ToString()
		{
			return Item;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyname)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
			}
		}
	}
}
