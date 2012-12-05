using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridExtensions;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace ProgDataBinding
{
	public class VisualListBindingControl : Grid
	{
		private ItemCollectionDatabag _itemCollectionDatabag = new ItemCollectionDatabag();

		public VisualListBindingControl()
		{
			InitializeColumnsAndLabels();

			InitializeBoundControls();
		}

		private void InitializeColumnsAndLabels()
		{
			this.AddAutoWidthColumns(2);
			this.AddMaxWidthColumn();

			this.AddAutoHeightRow();

			this.AddLabel("Add", 0, 0);
			this.AddLabel("Mod", 0, 1);
			this.AddLabel("Collection", 0, 2);
		}

		private void InitializeBoundControls()
		{
			var addItemToCollectionButton = new Button { Content = "+", Margin = new Thickness(2, 0, 2, 0) };
			var modItemInCollectionButton = new Button { Content = "~", Margin = new Thickness(2, 0, 2, 0) };
			var visualItemCollection = new ListBox();

			var factory = new FrameworkElementFactory(typeof(VirtualizingStackPanel));
			factory.SetValue(VirtualizingStackPanel.OrientationProperty, Orientation.Horizontal);

			visualItemCollection.ItemsPanel = new ItemsPanelTemplate(factory);

			this.AddAutoHeightRow();
			this.AddChild(addItemToCollectionButton, this.LastRowIndex(), 0);
			this.AddChild(modItemInCollectionButton, this.LastRowIndex(), 1);
			this.AddChild(visualItemCollection, this.LastRowIndex(), 2);


			addItemToCollectionButton.Click += addItemToCollectionButton_Click;
			modItemInCollectionButton.Click += modItemInCollectionButton_Click;

			var binding = new Binding
			{
				Source = _itemCollectionDatabag,
				Path = new PropertyPath("TextBlockList")
			};

			visualItemCollection.SetBinding(ListBox.ItemsSourceProperty, binding);
		}

		private void addItemToCollectionButton_Click(object sender, RoutedEventArgs e)
		{
			_itemCollectionDatabag.AddTextBlock(new TextBlock { Text = DateTime.Now.ToLongTimeString() });
		}

		private void modItemInCollectionButton_Click(object sender, RoutedEventArgs e)
		{
			_itemCollectionDatabag.ModifyValues(DateTime.Now.ToLongTimeString());
		}
	}

	public class ItemCollectionDatabag
	{
		private ObservableCollection<TextBlock> _textBlockList;

		public void AddTextBlock(TextBlock input)
		{
			_textBlockList.Add(input);
		}

		public ObservableCollection<TextBlock> TextBlockList
		{
			get
			{
				if (_textBlockList == null)
				{
					_textBlockList = new ObservableCollection<TextBlock>();
				}
				return _textBlockList;
			}
		}

		internal void ModifyValues(string newText)
		{
			foreach (var tb in _textBlockList)
			{
				tb.Text = newText;
			}
		}
	}
}