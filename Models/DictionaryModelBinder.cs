using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCodeFlowClientManual.Models
{
    public class DictionaryModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var dictionary = new Dictionary<ItemCategory, List<Item>>();

            foreach (string key in request.Form.Keys)
            {
                if (key.StartsWith("ItemDictionary"))
                {
                    var categoryName = key.Replace("ItemDictionary", "");
                    var category = (ItemCategory)Enum.Parse(typeof(ItemCategory), categoryName);

                    var items = request.Form.GetValues(key);
                    var itemList = new List<Item>();

                    foreach (var item in items)
                    {
                        itemList.Add(new Item { Name = item });
                    }

                    dictionary.Add(category, itemList);
                }
            }

            return dictionary;
        }
    }
}