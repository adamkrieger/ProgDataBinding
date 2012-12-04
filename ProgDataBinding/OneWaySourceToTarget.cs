using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using GridExtensions;

namespace ProgDataBinding
{
	public class OneWaySourceToTarget : Grid
	{
		public OneWaySourceToTarget()
		{
			InitializeColumnsAndLabels();

			InitializeDataboundControls();
		}

		private void InitializeDataboundControls()
		{
			var realTimeInput = new TextBox();
			var realTimeOutput = new TextBox();

			realTimeInput.TextChanged += new TextChangedEventHandler(realTimeInput_TextChanged);

			var binding = new Binding
			{
				Path = new PropertyPath(RealTimeUpdateProperty),
				Source = this
			};

			realTimeOutput.SetBinding(TextBox.TextProperty, binding);

			this.AddNewAutoHeightRow();
			this.AddChild(realTimeInput, this.LastRowIndex(), 0);
			this.AddChild(realTimeOutput, this.LastRowIndex(), 1);
		}

		private void InitializeColumnsAndLabels()
		{
			ColumnDefinitions.Add(new ColumnDefinition());
			ColumnDefinitions.Add(new ColumnDefinition());

			var updateSourceLabel = new TextBlock { Text = "UpdateSource", HorizontalAlignment = HorizontalAlignment.Center };
			var targetLabel = new TextBlock { Text = "Target", HorizontalAlignment = HorizontalAlignment.Center };

			this.AddNewAutoHeightRow();
			this.AddChild(updateSourceLabel, this.LastRowIndex(), 0);
			this.AddChild(targetLabel, this.LastRowIndex(), 1);
		}

		public DependencyProperty RealTimeUpdateProperty = DependencyProperty.Register(
			"RealTimeUpdate", typeof(string), typeof(OneWaySourceToTarget), new PropertyMetadata(null));

		public string RealTimeUpdate
		{
			set { SetValue(RealTimeUpdateProperty, value); }
		}

		private void realTimeInput_TextChanged(object sender, TextChangedEventArgs e)
		{
			var inputBox = sender as TextBox;

			if (inputBox != null)
			{ RealTimeUpdate = inputBox.Text; }
		}
	}
}
