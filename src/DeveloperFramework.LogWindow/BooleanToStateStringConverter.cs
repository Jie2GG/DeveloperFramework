using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DeveloperFramework.LogWindow
{
	internal class BooleanToStateStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null)
			{
				return string.Empty;
			}

			return ((bool?)value).Value ? "√" : "×";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (((string)value).CompareTo("√") == 0)
			{
				return true;
			}

			return false;
		}
	}
}
