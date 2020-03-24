using System;
using System.Collections;
using System.Collections.Specialized;
using global::Xamarin.Forms;
using global::Xamarin.Forms.Internals;
using System.Linq;
using TestCustomContentView.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace TestCustomContentView.BindableLayouts
{
	public static class CustomBindableLayout
	{
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.CreateAttached("ItemsSource", typeof(IEnumerable<TabItem>), typeof(Layout<View>), default(IEnumerable<TabItem>),
				propertyChanged: (b, o, n) => { GetBindableLayoutController(b).ItemsSource = (IEnumerable<TabItem>)n; });

		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.CreateAttached("ItemTemplate", typeof(DataTemplate), typeof(Layout<View>), default(DataTemplate),
				propertyChanged: (b, o, n) => { GetBindableLayoutController(b).ItemTemplate = (DataTemplate)n; });

		public static readonly BindableProperty ItemTemplateSelectorProperty =
			BindableProperty.CreateAttached("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(Layout<View>), default(DataTemplateSelector),
				propertyChanged: (b, o, n) => { GetBindableLayoutController(b).ItemTemplateSelector = (DataTemplateSelector)n; });

		static readonly BindableProperty BindableLayoutControllerProperty =
			BindableProperty.CreateAttached("BindableLayoutController", typeof(BindableLayoutController), typeof(Layout<View>), default(BindableLayoutController),
				 defaultValueCreator: (b) => { var a = new BindableLayoutController((Layout<View>)b);
					 return a;
                 },
				 propertyChanged: (b, o, n) => OnControllerChanged(b, (BindableLayoutController)o, (BindableLayoutController)n));

		public static readonly BindableProperty EmptyViewProperty =
			BindableProperty.Create("EmptyView", typeof(object), typeof(Layout<View>), null, propertyChanged: (b, o, n) => { GetBindableLayoutController(b).EmptyView = n; });

		public static readonly BindableProperty EmptyViewTemplateProperty =
			BindableProperty.Create("EmptyViewTemplate", typeof(DataTemplate), typeof(Layout<View>), null,
                propertyChanged: (b, o, n) => { GetBindableLayoutController(b).EmptyViewTemplate = (DataTemplate)n; });


		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.CreateAttached("SelectedItem", typeof(object), typeof(Layout<View>), null, BindingMode.TwoWay,
			    propertyChanged: (b, o, n) => { GetBindableLayoutController(b).SelectedItem = n; });

        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(Layout<View>), null,
                propertyChanged: (b, o, n) => { GetBindableLayoutController(b).Command = (ICommand)n; });


        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(Layout<View>), null);


		public static ICommand GetCommand(BindableObject b)
		{
			return (ICommand)b.GetValue(CommandProperty);
		}

		public static void SetCommand(BindableObject b, ICommand value)
		{
			b.SetValue(CommandProperty, value);
		}

		public static void SetSelectedItem(BindableObject b, object value)
		{
			b.SetValue(SelectedItemProperty, value);
		}

		public static object GetSelectedItem(BindableObject b)
		{
			return b.GetValue(SelectedItemProperty);
		}
		
		public static void SetItemsSource(BindableObject b, IEnumerable<TabItem> value)
		{
			b.SetValue(ItemsSourceProperty, value);
		}

		public static IEnumerable<TabItem> GetItemsSource(BindableObject b)
		{
			return (IEnumerable<TabItem>)b.GetValue(ItemsSourceProperty);
		}

		public static void SetItemTemplate(BindableObject b, DataTemplate value)
		{
			b.SetValue(ItemTemplateProperty, value);
		}

		public static DataTemplate GetItemTemplate(BindableObject b)
		{
			return (DataTemplate)b.GetValue(ItemTemplateProperty);
		}

		public static void SetItemTemplateSelector(BindableObject b, DataTemplateSelector value)
		{
			b.SetValue(ItemTemplateSelectorProperty, value);
		}

		public static DataTemplateSelector GetItemTemplateSelector(BindableObject b)
		{
			return (DataTemplateSelector)b.GetValue(ItemTemplateSelectorProperty);
		}

		public static object GetEmptyView(BindableObject b)
		{
			return b.GetValue(EmptyViewProperty);
		}

		public static void SetEmptyView(BindableObject b, object value)
		{
			b.SetValue(EmptyViewProperty, value);
		}

		public static DataTemplate GetEmptyViewTemplate(BindableObject b)
		{
			return (DataTemplate)b.GetValue(EmptyViewTemplateProperty);
		}

		public static void SetEmptyViewTemplate(BindableObject b, DataTemplate value)
		{
			b.SetValue(EmptyViewProperty, value);
		}

		static BindableLayoutController GetBindableLayoutController(BindableObject b)
		{
			return (BindableLayoutController)b.GetValue(BindableLayoutControllerProperty);
		}

		static void SetBindableLayoutController(BindableObject b, BindableLayoutController value)
		{
			b.SetValue(BindableLayoutControllerProperty, value);
		}

		static void OnControllerChanged(BindableObject b, BindableLayoutController oldC, BindableLayoutController newC)
		{
			if (oldC != null)
			{
				oldC.ItemsSource = null;
			}

			if (newC == null)
			{
				return;
			}

			newC.StartBatchUpdate();
			newC.ItemsSource = GetItemsSource(b);
			//newC.ItemTemplate = GetItemTemplate(b);
			newC.ItemTemplateSelector = GetItemTemplateSelector(b);
			newC.EmptyView = GetEmptyView(b);
			//newC.EmptyViewTemplate = GetEmptyViewTemplate(b);
			newC.EndBatchUpdate();
		}

		
	}

	class BindableLayoutController
	{
		readonly WeakReference<Layout<View>> _layoutWeakReference;
		WeakReference<View> _viewWeakReference;
		IEnumerable<TabItem> _itemsSource;
		DataTemplate _itemTemplate;
		DataTemplateSelector _itemTemplateSelector;
		bool _isBatchUpdate;
		object _emptyView;
		DataTemplate _emptyViewTemplate;
		View _currentEmptyView;
		private object _selectedItem;
		ICommand command;

		public IEnumerable<TabItem> ItemsSource { get => _itemsSource; set => SetItemsSource(value); }
		public DataTemplate ItemTemplate { get => _itemTemplate; set => SetItemTemplate(value); }
		public DataTemplateSelector ItemTemplateSelector { get => _itemTemplateSelector; set => SetItemTemplateSelector(value); }

		public object EmptyView { get => _emptyView; set => SetEmptyView(value); }
		public DataTemplate EmptyViewTemplate { get => _emptyViewTemplate; set => SetEmptyViewTemplate(value); }

		public object SelectedItem { get => _selectedItem; set => SetSelectedItem(value); }

		public ICommand Command{ get => command; set => command = value; }


		public BindableLayoutController(Layout<View> layout)
		{
			_layoutWeakReference = new WeakReference<Layout<View>>(layout);
		}

		internal void StartBatchUpdate()
		{
			_isBatchUpdate = true;
		}

		internal void EndBatchUpdate()
		{
			_isBatchUpdate = false;
			//CreateChildren();
			CreateChild();
		}

		void SetItemsSource(IEnumerable<TabItem> itemsSource)
		{
			if (_itemsSource is INotifyCollectionChanged c)
			{
				c.CollectionChanged -= ItemsSourceCollectionChanged;
			}

			_itemsSource = itemsSource;

			if (_itemsSource is INotifyCollectionChanged c1)
			{
				c1.CollectionChanged += ItemsSourceCollectionChanged;
			}

			if (!_isBatchUpdate)
			{
				//CreateChildren();
				//CreateChild();
			}
		}

		void SetItemTemplate(DataTemplate itemTemplate)
		{
			if (itemTemplate is DataTemplateSelector)
			{
				throw new NotSupportedException($"You are using an instance of {nameof(DataTemplateSelector)} to set the {nameof(BindableLayout)}.{BindableLayout.ItemTemplateProperty.PropertyName} property. Use {nameof(BindableLayout)}.{BindableLayout.ItemTemplateSelectorProperty.PropertyName} property instead to set an item template selector");
			}

			_itemTemplate = itemTemplate;

			if (!_isBatchUpdate)
			{
				//CreateChildren();
				CreateChild();
			}
		}

		void SetItemTemplateSelector(DataTemplateSelector itemTemplateSelector)
		{
			_itemTemplateSelector = itemTemplateSelector;

			if (!_isBatchUpdate)
			{
				CreateChildren();
			}
		}

		void SetEmptyView(object emptyView)
		{
			_emptyView = emptyView;

			_currentEmptyView = CreateEmptyView(_emptyView, _emptyViewTemplate);

			if (!_isBatchUpdate)
			{
				CreateChildren();
			}
		}

		void SetEmptyViewTemplate(DataTemplate emptyViewTemplate)
		{
			_emptyViewTemplate = emptyViewTemplate;

			_currentEmptyView = CreateEmptyView(_emptyView, _emptyViewTemplate);

			if (!_isBatchUpdate)
			{
				CreateChildren();
			}
		}

		void CreateChildren()
		{
			//original behavior

			//if (!_layoutWeakReference.TryGetTarget(out Layout<View> layout))
			//{
			//	return;
			//}

			//layout.Children.Clear();

			//UpdateEmptyView(layout);

			//if (_itemsSource == null)
			//	return;

			//foreach (object item in _itemsSource)
			//{
			//    layout.Children.Add(CreateItemView(item, layout));
			//}
		}

		private void CreateChild()
		{
			if (!_layoutWeakReference.TryGetTarget(out Layout<View> layout))
			{
				return;
			}
			layout.Children.Clear();

			if (_itemsSource == null )
				return;


			if (_selectedItem == null)
			{
				SelectedItem = _itemsSource.FirstOrDefault();
            }


			var child= CreateItemView(SelectedItem, layout);

            layout.Children.Add(child);

		}

		void UpdateEmptyView(Layout<View> layout)
		{
			if (_currentEmptyView == null)
				return;

			if (!_itemsSource?.GetEnumerator().MoveNext() ?? true)
			{
				layout.Children.Add(_currentEmptyView);
				return;
			}

			layout.Children.Remove(_currentEmptyView);
		}

		View CreateItemView(object item, Layout<View> layout)
		{
			return CreateItemView(item, _itemTemplate ?? _itemTemplateSelector?.SelectTemplate(item, layout));
		}

		View CreateItemView(object item, DataTemplate dataTemplate)
		{
			if (dataTemplate != null)
			{
				var view = (View)dataTemplate.CreateContent();
				//view.BindingContext = item;

				if (_viewWeakReference != null && _viewWeakReference.TryGetTarget(out View vireWeakRef)) {
					(vireWeakRef as IDisposable)?.Dispose();
				}

				_viewWeakReference = new WeakReference<View>(view);
				return view;
			}
			else
			{
				return new Label { Text = item?.ToString(), HorizontalTextAlignment = TextAlignment.Center };
			}
		}

		View CreateEmptyView(object emptyView, DataTemplate dataTemplate)
		{
			if (!_layoutWeakReference.TryGetTarget(out Layout<View> layout))
			{
				return null;
			}

			if (dataTemplate != null)
			{
				var view = (View)dataTemplate.CreateContent();
				view.BindingContext = layout.BindingContext;
				return view;
			}

			if (emptyView is View emptyLayout)
			{
				return emptyLayout;
			}

			return new Label { Text = emptyView?.ToString(), HorizontalTextAlignment = TextAlignment.Center };
		}

		private void SetSelectedItem(object itemSelected)
		{
			_selectedItem = itemSelected;

			if (!_isBatchUpdate)
			{
				//CreateChildren();
				CreateChild();
			}
		}

		void ItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (!_layoutWeakReference.TryGetTarget(out Layout<View> layout))
			{
				return;
			}

			e.Apply(
				insert: (item, index, _) => layout.Children.Insert(index, CreateItemView(item, layout)),
				removeAt: (item, index) => layout.Children.RemoveAt(index),
				reset: CreateChildren);

			// UpdateEmptyView is called from within CreateChildren, therefor skip it for Reset
			if (e.Action != NotifyCollectionChangedAction.Reset)
				UpdateEmptyView(layout);
		}
	}

}
