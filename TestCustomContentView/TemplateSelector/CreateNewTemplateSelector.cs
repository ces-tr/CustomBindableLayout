using System;
using System.Collections.Generic;
using System.Linq;
using TestCustomContentView.ContentViews;
using TestCustomContentView.Models;
using Xamarin.Forms;

namespace TestCustomContentView.DataTemplateSelectors
{
    public class CreateNewTemplateSelector : DataTemplateSelector
    {
        private DataTemplate firstTemplate;
        public DataTemplate FirstTemplate {
            get => firstTemplate;
            set => firstTemplate= value;
        }

        private DataTemplate secondTemplate;
        public DataTemplate SecondTemplate {
            get => secondTemplate;
            set => secondTemplate= value;
        }

        Dictionary<Type, Func<DataTemplate>> Mapping = new Dictionary<Type, Func<DataTemplate>>();

        public CreateNewTemplateSelector() {

            Mapping = new Dictionary<Type, Func<DataTemplate>>()
            {
                { typeof(FirstContentView), ()=> FirstTemplate},
                { typeof(SecondContentView), ()=> SecondTemplate}
            };
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is TabItem tabItem) {

                KeyValuePair<Type, Func<DataTemplate>> itemTemplate = Mapping.FirstOrDefault(x => x.Key == tabItem.ContentType);

                return itemTemplate.Value.Invoke();

            }
            

            return null;
            
        }
    }
}
