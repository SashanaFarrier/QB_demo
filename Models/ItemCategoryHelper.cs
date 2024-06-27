using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcCodeFlowClientManual.Models
{
     public class ItemCategoryHelper
     {
        //private ItemCategory ItemCategory {  get; set; }
        //private string Category {  get; set; }


        //public ItemCategoryHelper(ItemCategory itemCategory, string category)
        //{
        //    ItemCategory = itemCategory;
        //    Category = category;
        //}

        //public ItemCategory GetStringFromEnum()
        //{
        //    foreach (ItemCategory s in Enum.GetValues(typeof(ItemCategory)))
        //    {
        //        if (s.ToString() == this.Category) return s;
        //    }

        //    return null;
        //}


        public static string RemoveWhitespace(string category)
        {
           // string category = itemCategory;

             StringBuilder sb = new StringBuilder();
            if(category != null)
            {
                for (int i = 0; i < category.Length; i++)
                {
                    if (category[i] != 32)
                    {
                        sb.Append(category[i]);
                    }
                }

            }

            return sb.ToString();
        }


        public static string AddWhitespace(string category)
        {
            // string category = itemCategory;

            StringBuilder sb = new StringBuilder();
            if (category != null)
            {
                for (int i = 0; i < category.Length; i++)
                {
                    if (i != 0 && char.IsUpper(category[i]))
                    {
                        sb.Append(' ').Append(category[i]);
                    } else
                    {
                        sb.Append(category[i]);
                    }
                }

            }

            return sb.ToString();
        }

        public static T ParseEnum<T>(string value)
        {
           
            return (T)Enum.Parse(typeof(T), RemoveWhitespace(value), true);
        }

    }
}