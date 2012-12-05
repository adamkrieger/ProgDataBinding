using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridExtensions;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

namespace ProgDataBinding
{
	public class MathToStringControl : Grid
	{
		private SimpleDatabag _simpleDatabag = new SimpleDatabag();

		public MathToStringControl()
		{
			InitializeColumnsAndLabels();

			InitializeBoundControls();
		}

		private void InitializeBoundControls()
		{
			var firstNumBox = new TextBox();
			var secondNumBox = new TextBox();
			var productBox = new TextBox();

			firstNumBox.TextChanged += boundTextbox_TextChanged;
			secondNumBox.TextChanged += boundTextbox_TextChanged;

			this.AddAutoHeightRow();
			this.AddChild(firstNumBox, this.LastRowIndex(), 0);
			this.AddChild(secondNumBox, this.LastRowIndex(), 1);
			this.AddChild(productBox, this.LastRowIndex(), 2);

			var firstNumBinding = new Binding
			{
				Path = new PropertyPath("FirstNumber"),
				Source = _simpleDatabag,
				UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
				Converter = new ConvertProductToString()
			};

			firstNumBox.SetBinding(TextBox.TextProperty, firstNumBinding);

			var secondNumBinding = new Binding
			{
				Path = new PropertyPath("SecondNumber"),
				Source = _simpleDatabag,
				UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
				Converter = new ConvertProductToString()
			};

			secondNumBox.SetBinding(TextBox.TextProperty, secondNumBinding);

			var productBinding = new Binding
			{
				Path = new PropertyPath("Product"),
				Source = _simpleDatabag,
				Mode = BindingMode.OneWay,
				Converter = new ConvertProductToStringWithMessage()
			};

			productBox.SetBinding(TextBox.TextProperty, productBinding);
		}

		private void InitializeColumnsAndLabels()
		{
			this.AddMaxWidthColumns(3);
			this.AddAutoHeightRow();

			this.AddLabel("Number", 0, 0);
			this.AddLabel("Multiplier", 0, 1);
			this.AddLabel("Product", 0, 2);
		}

		private void boundTextbox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var castSender = sender as TextBox;

			if (castSender != null)
			{
				var bindingExpr = castSender.GetBindingExpression(TextBox.TextProperty);

				if (bindingExpr != null)
				{
					bindingExpr.UpdateSource();
				}
			}
		}
	}

	[ValueConversion(typeof(decimal?), typeof(string))]
	public class ConvertProductToString : IValueConverter
	{
		//This is implemented because of odd functionality in 'OneWayToSource' bindings in .Net 4.0
		//Using it in this case, it changes the binding to effectively match 'OneWayToSource' functionality
		// while using the binding mode default
		//http://stackoverflow.com/questions/4875751/onewaytosource-binding-seems-broken-in-net-4-0
		public object _lastValue;

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return _lastValue;
			else
				return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			decimal parsedDecimal;

			if (decimal.TryParse(value.ToString(), out parsedDecimal))
			{
				return parsedDecimal;
			}

			_lastValue = value;

			return null;
		}
	}

	[ValueConversion(typeof(decimal?), typeof(string))]
	public class ConvertProductToStringWithMessage : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return "NaN";
			else
				return value.ToString();			
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			decimal parsedDecimal;

			if (decimal.TryParse(value.ToString(), out parsedDecimal))
			{
				return parsedDecimal;
			}

			return null;
		}
	}

	public class SimpleDatabag : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private decimal? _decimalValue;
		private decimal? _decimalValue2;
		private decimal? _product;

		public decimal? FirstNumber
		{
			get { return _decimalValue; }
			set
			{
				_decimalValue = value;

				CalculateProduct();

				RaisePropertyChanged("FirstNumber");
			}
		}

		public decimal? SecondNumber
		{
			get { return _decimalValue2; }
			set
			{
				_decimalValue2 = value;

				CalculateProduct();

				RaisePropertyChanged("SecondNumber");
			}
		}

		public decimal? Product
		{
			get { return _product; }
			private set {
				_product = value;

				RaisePropertyChanged("Product");
			}
		}

		private void CalculateProduct()
		{
			if (_decimalValue != null && _decimalValue2 != null)
			{
				Product = _decimalValue * _decimalValue2;
			}
			else
			{ 
				//One or both numbers are invalid, set product to null
				Product = null;
			}
		}

		private void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
