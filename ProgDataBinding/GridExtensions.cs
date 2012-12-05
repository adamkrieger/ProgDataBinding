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
		public static void AddAutoHeightRow(this Grid grid)
		{
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		}

		public static void AddMaxHeightRow(this Grid pGrid)
		{
			pGrid.RowDefinitions.Add(new RowDefinition());
		}

		public static void AddMaxWidthColumn(this Grid pGrid)
		{
			pGrid.ColumnDefinitions.Add(new ColumnDefinition());
		}

		public static void AddAutoWidthColumn(this Grid pGrid)
		{
			pGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
		}

		public static void AddMaxWidthColumns(this Grid pGrid, int numberOfColumnsToAdd)
		{
			for (int x = 0; x < numberOfColumnsToAdd; x++)
			{
				pGrid.AddMaxWidthColumn();
			}
		}

		public static void AddAutoWidthColumns(this Grid pGrid, int numberOfColumnsToAdd)
		{
			for (int x = 0; x < numberOfColumnsToAdd; x++)
			{
				pGrid.AddAutoWidthColumn();
			}
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

		public static void AddLabel(this Grid pGrid, string text, int row, int column)
		{
			var textBlock = new TextBlock { Text = text, HorizontalAlignment = HorizontalAlignment.Center };

			pGrid.AddChild(textBlock, row, column);
		}
	}
}
