using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ExtendedObjectsLibrary
{
    /// <summary>
    /// Interaction logic for HistogramLegendElement.xaml
    /// </summary>
    public partial class HistogramLegendElement : UserControl
    {
        #region Properties

        private string m_Text = string.Empty;
        public string Text
        {
            get { return m_Text; }
            set
            {
                m_Text = value;
                textBlock.Text = value;
            }
        }

        private Brush m_FillBrush = null;
        public Brush FillBrush
        {
            get { return m_FillBrush; }
            set
            {
                m_FillBrush = value;
                colorRectangle.Background = value;
            }
        }

        #endregion

        public HistogramLegendElement()
        {
            InitializeComponent();
            
            FontSize = 12;
            FontFamily = new FontFamily("Verdana");
        }
    }
}
