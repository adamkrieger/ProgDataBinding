using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GridExtensions;

namespace ProgDataBinding
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			BuildOneWaySourceToTarget();

			BuildTwoWayBindingControl();

			BuildMathToString();

			BuildVisualList();
		}

		private void BuildOneWaySourceToTarget()
		{
			var oneWayBindingControl = new OneWaySourceToTarget();

			AddToGridWithinBorder(oneWayBindingControl);
		}

		private void BuildTwoWayBindingControl()
		{
			var twoWayBindingControl = new TwoWayBindingControl();

			AddToGridWithinBorder(twoWayBindingControl);
		}

		private void BuildMathToString()
		{
			var mathToString = new MathToStringControl();

			AddToGridWithinBorder(mathToString);
		}

		private void BuildVisualList()
		{
			var visualListControl = new VisualListBindingControl();

			AddToGridWithinBorder(visualListControl);
		}

		private void AddToGridWithinBorder(UIElement control)
		{
			var border = new Border
			{
				BorderBrush = Brushes.DarkGray,
				BorderThickness = new Thickness(1)
			};

			border.Child = control;

			controlGrid.AddAutoHeightRow();
			controlGrid.AddChild(border, controlGrid.LastRowIndex(), 0);
		}

	}
}
