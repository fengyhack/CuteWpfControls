using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CuteWpfControls
{
    public class VisualHelper
    {
        public static T FindVisualChild<T>(DependencyObject d) 
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(d, i);
                if (child != null && child is T t)
                {
                    return t;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject d) 
            where T : DependencyObject
        {
            if (d != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(d, i);
                    if (child != null && child is T t)
                    {
                        yield return t;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static List<T> FindVisualChildrenEx<T>(DependencyObject d) 
            where T : DependencyObject
        {
            try
            {
                List<T> children = new List<T> { };
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(d, i);
                    if (child != null && child is T t)
                    {
                        children.Add(t);
                        List<T> childrenOfChild = FindVisualChildrenEx<T>(child);
                        if (childrenOfChild != null)
                        {
                            children.AddRange(childrenOfChild);
                        }
                    }
                    else
                    {
                        List<T> childrenOfChild = FindVisualChildrenEx<T>(child);
                        if (childrenOfChild != null)
                        {
                            children.AddRange(childrenOfChild);
                        }
                    }
                }
                return children;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T FindParent<T>(DependencyObject d) 
            where T : DependencyObject
        {
            DependencyObject p = VisualTreeHelper.GetParent(d);
            if (p != null)
            {
                if (p is T t)
                {
                    return t;
                }
                else
                {
                    p = FindParent<T>(p);
                    if (p != null && p is T t1)
                    {
                        return t1;
                    }
                }
            }
            return null;
        }

        public static T FindParent<T>(DependencyObject d, string elementName) 
            where T : DependencyObject
        {
            DependencyObject p = VisualTreeHelper.GetParent(d);
            if (p != null)
            {
                if (p is T t && ((FrameworkElement)p).Name.Equals(elementName))
                {
                    return t;
                }
                else
                {
                    p = FindParent<T>(p);
                    if (p != null && p is T t1)
                    {
                        return t1;
                    }
                }
            }
            return null;
        }

        public static T FindVisualElement<T>(DependencyObject d, string elementName) 
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(d, i);
                if (child != null && child is T item && ((FrameworkElement)child).Name.Equals(elementName))
                {
                    return item;
                }
                else
                {
                    IEnumerator j = FindVisualChildren<T>(child).GetEnumerator();
                    while (j.MoveNext())
                    {
                        T childOfChild = (T)j.Current;

                        if (childOfChild != null && !(childOfChild as FrameworkElement).Name.Equals(elementName))
                        {
                            FindVisualElement<T>(childOfChild, elementName);
                        }
                        else
                        {
                            return childOfChild;
                        }

                    }
                }
            }
            return null;
        }

        public static bool HitTest<T>(DependencyObject d)
            where T : DependencyObject
        {
            return FindParent<T>(d) != null || FindVisualChild<T>(d) != null;
        }

        public static T FindEqualElement<T>(DependencyObject source, DependencyObject element)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(source); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(source, i);
                if (child != null && child is T t && child == element)
                {
                    return t;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }

                }
            }
            return null;
        }
    }
}
