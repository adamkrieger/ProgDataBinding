using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace GridExtensions
{
	public static class GridExtensions
	{
		public static void AddNewAutoHeightRow(this Grid grid)
		{
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		}

		public static void AddChild(this Grid grid, UIElement control, int row, int column)
		{
			control.SetValue(Grid.RowProperty, row);
			control.SetValue(Grid.ColumnProperty, column);

			grid.Children.Add(control);
		}

		public static int LastRowIndex(this Grid grid)
		{
			return grid.RowDefinitions.Count - 1;
		}
	}
}
