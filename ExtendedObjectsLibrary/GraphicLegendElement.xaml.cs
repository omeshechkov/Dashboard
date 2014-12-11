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
    /// Interaction logic for GraphicLegendElement.xaml
    /// </summary>
    public partial class GraphicLegendElement : UserControl
    {
        #region Properties

        private Color m_Color = new Color();

        public Color Color 
        {
            get { return m_Color; }
            set
            {
                m_Color = value;
                
                if (IsLoaded)
                    DrawFilledRectangle(value);
            }
        }

        public string OwnName { get; set; }

        private string m_Text = "";
        public string Text
        {
            get { return m_Text; }
            set 
            {
                textBlock.Text = value + " (x" + Math.Round(Scale, 2) + ")";
                m_Text = value;
            }
        }

        public string Description { get; set; }

        private bool m_IsChecked = false;
        public bool IsChecked 
        {
            get { return m_IsChecked; }
            set 
            {
                m_IsChecked = value;
                IsCheckedChange(m_IsChecked); 
            } 
        }

        private double m_Scale = 1;
        public double Scale 
        {
            get { return m_Scale; }
            set
            {
                textBlock.Text = m_Text + " (x" + Math.Round(value, 2) + ")";
                m_Scale = value;
            }
        }

        #endregion

        public event EventHandler CheckedStateChanged;

        public GraphicLegendElement(string OwnName)
        {
            InitializeComponent();

            this.OwnName = OwnName;

            Loaded += new RoutedEventHandler(GraphicLegendElement_Loaded);
        }

        void GraphicLegendElement_Loaded(object sender, RoutedEventArgs e)
        {
            DrawFilledRectangle(Color);
        }

        private void DrawFilledRectangle(Color color)
        {
            colorRectangle.Background = new SolidColorBrush(color);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.ChangedButton == MouseButton.Left)
                IsCheckedChange(!IsChecked);
        }

        private void IsCheckedChange(bool state)
        {
            m_IsChecked = state;

            switch (m_IsChecked)
            {
                case true:
                    layoutGrid.Background = Brushes.SteelBlue;
                    break;

                case false:
                    layoutGrid.Background = Brushes.Black;
                    break;
            }

            if (CheckedStateChanged != null)
                CheckedStateChanged(this, new EventArgs());
        }
    }
}