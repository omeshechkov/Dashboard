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
using System.ComponentModel;
using System.Collections;

namespace DashboardEngine
{
    /// <summary>
    /// Interaction logic for LoadingDashboardInfo.xaml
    /// </summary>
    public partial class LoadingDashboardInfo : Page
    {
        public static readonly DependencyProperty LoadedAssembliesProperty =
            DependencyProperty.Register("LoadedAssemblies", typeof(IEnumerable), typeof(LoadingDashboardInfo), new FrameworkPropertyMetadata(null));

        [Bindable(true)]
        public IEnumerable LoadedAssemblies
        {
            get { return (IEnumerable)GetValue(LoadedAssembliesProperty); }
            set { SetValue(LoadedAssembliesProperty, value); }
        }

        public LoadingDashboardInfo()
        {
            InitializeComponent();
        }
    }
}
