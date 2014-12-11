using System;
using System.Collections.Generic;
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
using System.Windows.Markup;
using System.ComponentModel;

namespace StandartObjectLibrary
{
    [ContentProperty("Child")]
    public partial class Group : UserControl
    {
        #region Properties

        [Category("Group Properties")]
        public object Child
        {
            get { return contentPresenter.Content; }
            set { contentPresenter.Content = value; }
        }

        #endregion

        #region Dependency Properties

        [Category("Group Properties")]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Group), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(TitleChangedCallback)));

        [Category("Title Properties")]
        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(Group), new FrameworkPropertyMetadata((double)11.7, new PropertyChangedCallback(TitleFontSizeChangedCallback)));

        [Category("Title Properties")]
        public FontFamily TitleFontFamily
        {
            get { return (FontFamily)GetValue(TitleFontFamilyProperty); }
            set { SetValue(TitleFontFamilyProperty, value); }
        }

        public static readonly DependencyProperty TitleFontFamilyProperty =
            DependencyProperty.Register("TitleFontFamily", typeof(FontFamily), typeof(Group), new FrameworkPropertyMetadata(new FontFamily("Tahoma"), new PropertyChangedCallback(TitleFontFamilyChangedCallback)));

        #endregion

        #region Callbacks

        private static void TitleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Group).titleTextBlock1.Text = e.NewValue.ToString();
            (d as Group).titleTextBlock2.Text = e.NewValue.ToString();
        }

        private static void TitleFontSizeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Group).titleTextBlock1.FontSize = (double)e.NewValue;
            (d as Group).titleTextBlock2.FontSize = (double)e.NewValue;
        }

        private static void TitleFontFamilyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Group).titleTextBlock1.FontFamily = (FontFamily)e.NewValue;
            (d as Group).titleTextBlock2.FontFamily = (FontFamily)e.NewValue;
        }

        #endregion

        public Group()
        {
            InitializeComponent();
        }
    }
}
