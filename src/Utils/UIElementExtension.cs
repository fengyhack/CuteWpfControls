using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace CuteWpfControls
{
    public static class UIElementExtension
    {
        public static Adorner GetOrAddAdorner(this UIElement element, Type type)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(element);
            if (adornerLayer == null)
            {
                throw new Exception("Visual parents must have AdornerDecorator!");
            }
            Adorner adorner = adornerLayer.GetAdorners(element)?.FirstOrDefault(x => x?.GetType() == type);
            if (adorner == null)
            {
                lock (element)
                {
                    if (adorner == null)
                    {
                        adorner = (Adorner)Activator.CreateInstance(type, new object[] { element });
                        adornerLayer.Add(adorner);
                    }
                }
            }
            return adorner;
        }
    }
}
