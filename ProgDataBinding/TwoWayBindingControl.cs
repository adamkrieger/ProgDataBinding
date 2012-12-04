using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using GridExtensions;
using System.ComponentModel;

namespace ProgDataBinding
{
	public class TwoWayBindingControl : Grid
	{
		private TextBox _twoWayUpdate;
		private SampleDatabag _sampleDatabag;

		public TwoWayBindingControl()
		{
			InitializeColumnsAndLabels();

			InitializeDataboundControls();
		}

		private void InitializeDataboundControls()
		{
			var realTimeInput = new TextBox();
			var realTimeOutput = new TextBox();

			_twoWayUpdate = new TextBox();
			_sampleDatabag = new SampleDatabag();

			_sampleDatabag.PropertyChanged += _sampleDatabag_PropertyChanged;

			realTimeInput.TextChanged += realTimeInput_TextChanged;

			var binding = new Binding
			{
				Path = new PropertyPath("Value"),
				Source = _sampleDatabag,
				Mode = BindingMode.TwoWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			};

			realTimeOutput.SetBinding(TextBox.TextProperty, binding);

			this.AddNewAutoHeightRow();
			this.AddChild(realTimeInput, this.LastRowIndex(), 0);
			this.AddChild(realTimeOutput, this.LastRowIndex(), 1);
			this.AddChild(_twoWayUpdate, this.LastRowIndex(), 2);
		}

		private void realTimeInput_TextChanged(object sender, TextChangedEventArgs e)
		{
			var inputBox = sender as TextBox;

			if (inputBox != null)
			{ _sampleDatabag.Value = inputBox.Text; }
		}

		public void _sampleDatabag_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			_twoWayUpdate.Text = (sender as SampleDatabag).Value;
		}

		private void InitializeColumnsAndLabels()
		{
			ColumnDefinitions.Add(new ColumnDefinition());
			ColumnDefinitions.Add(new ColumnDefinition());
			ColumnDefinitions.Add(new ColumnDefinition());

			var updateSourceLabel = new TextBlock { Text = "UpdateSource", HorizontalAlignment = HorizontalAlignment.Center };
			var targetLabel = new TextBlock { Text = "Target", HorizontalAlignment = HorizontalAlignment.Center };
			var dpLabel = new TextBlock { Text = "DP Content", HorizontalAlignment = HorizontalAlignment.Center };

			this.AddNewAutoHeightRow();
			this.AddChild(updateSourceLabel, this.LastRowIndex(), 0);
			this.AddChild(targetLabel, this.LastRowIndex(), 1);
			this.AddChild(dpLabel, this.LastRowIndex(), 2);
		}
	}

	public class SampleDatabag : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string _value;

		public string Value
		{
			get { return _value; }
			set
			{
				_value = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Value"));
				}
			}
		}

	}
}
