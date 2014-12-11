using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DashboardEngine
{
    public partial class MainPage
    {
        #region Properties

        private readonly DispatcherTimer _timer;

        private readonly CommandBinding _navigateUriCommandBinding = new CommandBinding();
        private XElement _config;
        private bool _isDashboardListCollapsed;
        private double _mainMenuColumnWidth;
        private string _currentDashboard = string.Empty;

        private readonly TreeView _allDashboardsTreeView = new TreeView();
        private readonly ListBox _liveDashboardsListBox = new ListBox();

        private readonly Dictionary<string, DashboardObjectsScope> _liveDashboardObjectScopes = new Dictionary<string, DashboardObjectsScope>();

        private double TitleWidth
        {
            get
            {
                if (titleTextBlock.ActualWidth + titleTextBlock.Margin.Right < 150)
                    return 150;

                return titleTextBlock.ActualWidth + titleTextBlock.Margin.Right;
            }
        }

        #endregion

        public MainPage()
        {
            InitializeComponent();

            _navigateUriCommandBinding.Command = NavigationCommands.NavigateUri;
            _navigateUriCommandBinding.CanExecute += NavigateUriCanExecuteHandler;

            CommandBindings.Add(_navigateUriCommandBinding);

            Initialize();

            Loaded += MainPage_Loaded;

            _timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Background, TimerTick, Application.Current.Dispatcher);
            _timer.Start();
        }

        #region Event Handlers

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            JustifyMainFrameDecorator();
            Resources["ToggleButtonBrush"] = DashboardStyleResources.RadialFillBrushes[Severity.Normal];

            layoutGrid.SizeChanged += layoutGrid_SizeChanged;
            titleTextBlock.SizeChanged += titleTextBlock_SizeChanged;

            ShowLiveDashboardsMenu();
        }

        private void titleTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            JustifyMainFrameDecorator();
        }

        private void layoutGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            JustifyMainFrameDecorator();
        }

        private void mainFrame_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            JustifyMainFrameDecorator();
        }

        private void dashboardTreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var currentTreeViewItem = (TreeViewItem)sender;
            var dashboardContext = (DashboardContext)currentTreeViewItem.Tag;
            AddLiveDashboard(dashboardContext);
        }

        private void ToggleMainMenu_Click(object sender, RoutedEventArgs e)
        {

            if (_isDashboardListCollapsed)
            {
                mainMenuColumn.Width = new GridLength(_mainMenuColumnWidth, _mainMenuColumnWidth == 1 ? GridUnitType.Auto : GridUnitType.Pixel);
            }
            else
            {
                _mainMenuColumnWidth = mainMenuColumn.Width.Value;
                mainMenuColumn.Width = new GridLength(16);
            }

            _isDashboardListCollapsed = !_isDashboardListCollapsed;
        }

        private void LiveDashboardsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowLiveDashboardsMenu();
        }

        private void AllDashboardsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAllDashboardsMenu();
        }

        private void liveDashboardlistBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            var currentListBoxItem = (ListBoxItem)sender;
            var dashboardName = (string)currentListBoxItem.Content;

            ShowLiveDashboard(dashboardName);
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var name = (string)(_liveDashboardsListBox.SelectedItem as ListBoxItem).Content;
            ShowLiveDashboard(name);
        }

        private void DisconnectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var name = (string)(_liveDashboardsListBox.SelectedItem as ListBoxItem).Content;
            RemoveLiveDashboard(name);
        }

        #endregion

        #region Functionality

        private void Initialize()
        {
            try
            {
                var stream = Application.GetRemoteStream(new Uri("/Config.xml", UriKind.Relative)).Stream;
                using (var reader = new StreamReader(stream))
                {
                    _config = XElement.Parse(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                mainFrame.Content = new ErrorPage("Reading 'Config.xml' file. Exception: {0}", ex.ToString());
                return;
            }

            var loadAssembliesErrors = new List<string>();

            try
            {
                if (_config != null)
                {
                    var assemblyUrls = _config.XPathSelectElements("Assemblies/Assembly").Select(el => el.Attribute("url").Value);

                    foreach (var assemblyUrl in assemblyUrls)
                    {
                        try
                        {
                            LoadAssembly(assemblyUrl);
                        }
                        catch (Exception ex)
                        {
                            loadAssembliesErrors.Add(string.Format("Cannot load assembly '{0}'. Exception: {1}", assemblyUrl, ex.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mainFrame.Content = new ErrorPage("Loading assemblies.", ex.ToString());
                return;
            }

            if (loadAssembliesErrors.Count > 0)
            {
                var error = new StringBuilder(string.Format("There are {0} assemblies not loaded.\n", loadAssembliesErrors.Count));
                foreach (var err in loadAssembliesErrors)
                    error.AppendLine(err);

                mainFrame.Content = new ErrorPage("Loading assemblies.", error.ToString());
            }

            try
            {
                var xDirections = _config.XPathSelectElements("Dashboard/Directions/Direction").Select(el => el);

                _allDashboardsTreeView.Items.Clear();

                foreach (var xDirection in xDirections)
                {
                    var directionTreeViewItem = new TreeViewItem { Header = xDirection.Attribute("name").Value };

                    foreach (var xDashboard in xDirection.XPathSelectElements("Dashboards/Dashboard"))
                    {
                        var dashboardContext = DashboardContext.Load(xDashboard);
                        var dashboardTreeViewItem = new TreeViewItem();
                        dashboardTreeViewItem.Selected += dashboardTreeViewItem_Selected;
                        dashboardTreeViewItem.Header = dashboardContext.DashboardName;
                        dashboardTreeViewItem.Tag = dashboardContext;;

                        directionTreeViewItem.Items.Add(dashboardTreeViewItem);
                    }

                    _allDashboardsTreeView.Items.Add(directionTreeViewItem);
                }
            }
            catch (Exception ex)
            {
                mainFrame.Content = new ErrorPage("Loading directions.", ex.ToString());
            }
        }

        private void TimerTick(object sender, EventArgs eventArgs)
        {
            foreach (var dashboardObjectsScope in _liveDashboardObjectScopes.Values)
            {
                var dashboardContext = dashboardObjectsScope.DashboardContext;
                if (!dashboardContext.RefreshDataProviders())
                    continue;

                var severity = AggregateStates.GetDashboardTotalSeverity(dashboardContext.DashboardName);

                if (_liveDashboardObjectScopes.ContainsKey(dashboardContext.DashboardName))
                {
                    var scope = _liveDashboardObjectScopes[dashboardContext.DashboardName];
                    scope.State = severity;
                    var listBoxItem = scope.ListBoxItem;

                    listBoxItem.Foreground = severity == Severity.Disabled
                        ? new SolidColorBrush(Colors.White)
                        : DashboardStyleResources.SolidFillBrushes[severity];
                }

                if (titleTextBlock.Text == dashboardContext.DashboardName)
                {
                    titleTextBlock.Foreground = severity != Severity.Disabled
                        ? DashboardStyleResources.SolidFillBrushes[severity]
                        : new SolidColorBrush(Colors.White);
                }

                var resultSeverity = Severity.Disabled;

                var dashboardSeverities = _liveDashboardObjectScopes.Select(d => d.Value.State);

                foreach (var dashboardSeverity in dashboardSeverities)
                {
                    if (resultSeverity == Severity.Disabled)
                        resultSeverity = dashboardSeverity;
                    else if (dashboardSeverity != Severity.Disabled && dashboardSeverity > resultSeverity)
                        resultSeverity = dashboardSeverity;
                }

                liveDashboardsButton.Background = DashboardStyleResources.LinearFillBrushes[resultSeverity];

                var color = DashboardStyleResources.FillColors[severity];

                Resources["LeftTopCornerButtonBrush"] = new SolidColorBrush(ColorHelper.ChangeColor(color, 0.3F, 1));
                Resources["RightBottomCornerButtonBrush"] = new SolidColorBrush(ColorHelper.ChangeColor(color, 0.3F, -1));
                Resources["ToggleButtonBrush"] = DashboardStyleResources.RadialFillBrushes[severity];
            }
        }

        private void ShowLiveDashboard(string dashboardName)
        {
            mainFrame.Content = _liveDashboardObjectScopes[dashboardName].DashboardContext.Page;
            titleTextBlock.Text = dashboardName;

            var state = _liveDashboardObjectScopes[dashboardName].State;

            titleTextBlock.Foreground = state == Severity.Disabled 
                ? new SolidColorBrush(Colors.White) 
                : DashboardStyleResources.SolidFillBrushes[state];

            JustifyMainFrameDecorator();
        }

        private void ShowLiveDashboardsMenu()
        {
            DockPanel.SetDock(allDashboardsButton, Dock.Bottom);
            menuContent.Content = _liveDashboardsListBox;
        }

        private void ShowAllDashboardsMenu()
        {
            DockPanel.SetDock(allDashboardsButton, Dock.Top);
            menuContent.Content = _allDashboardsTreeView;
        }

        private static void NavigateUriCanExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            var isOk = true;

            try
            {
                new Uri(e.Parameter.ToString(), UriKind.RelativeOrAbsolute);
            }
            catch
            {
                isOk = false;
            }

            e.CanExecute = isOk;
        }

        private static void LoadAssembly(string Address)
        {
            if (Address == null) throw new ArgumentNullException("Address");
            var address = new Uri(Address, UriKind.Relative);

            var resource = Application.GetRemoteStream(address);
            if (resource == null)
                throw new NullReferenceException("'resource' is null at 'LoadAssembly' method.");

            using (var assemblyStream = resource.Stream)
            {
                if (assemblyStream == null)
                    throw new NullReferenceException("'assemblyStream' is null at 'LoadAssembly' method.");

                var buffer = new byte[65536];
                var rawAssembly = new List<byte>();

                while (true)
                {
                    var n = assemblyStream.Read(buffer, 0, buffer.Length);

                    if (n == 0)
                        break;

                    for (var i = 0; i < n; i++)
                        rawAssembly.Add(buffer[i]);
                }

                AppDomain.CurrentDomain.Load(rawAssembly.ToArray());
                return;
            }
        }

        private void AddLiveDashboard(DashboardContext dashboardContext)
        {
            try
            {
                var name = dashboardContext.DashboardName;

                if (!_liveDashboardObjectScopes.ContainsKey(name))
                {
                    var liveDashboardlistBoxItem = new ListBoxItem
                    {
                        Cursor = Cursors.Hand,
                        Content = name,
                        IsSelected = true,
                        ContextMenu = Resources["liveDashboardContextMenu"] as ContextMenu
                    };

                    liveDashboardlistBoxItem.Selected += liveDashboardlistBoxItem_Selected;
                    _liveDashboardsListBox.Items.Add(liveDashboardlistBoxItem);

                    _liveDashboardObjectScopes.Add(name, new DashboardObjectsScope(dashboardContext, liveDashboardlistBoxItem));
                }
                else
                {
                    _liveDashboardObjectScopes[name].ListBoxItem.IsSelected = true;
                    _liveDashboardObjectScopes[name].DashboardContext = dashboardContext;
                }

                titleTextBlock.Text = name;
                _currentDashboard = name;

                mainFrame.Content = dashboardContext.Page;

                var severity = AggregateStates.GetDashboardTotalSeverity(name);

                var textBrush = _liveDashboardObjectScopes[name].State == Severity.Disabled
                    ? new SolidColorBrush(Colors.White)
                    : DashboardStyleResources.SolidFillBrushes[_liveDashboardObjectScopes[name].State];

                titleTextBlock.Foreground = textBrush;
                _liveDashboardObjectScopes[name].ListBoxItem.Foreground = textBrush;

                _liveDashboardObjectScopes[name].State = severity;

                ShowLiveDashboardsMenu();

                var color = DashboardStyleResources.FillColors[severity];

                Resources["LeftTopCornerButtonBrush"] = new SolidColorBrush(ColorHelper.ChangeColor(color, 0.3F, 1));
                Resources["RightBottomCornerButtonBrush"] = new SolidColorBrush(ColorHelper.ChangeColor(color, 0.3F, -1));
            }
            catch (Exception ex)
            {
                mainFrame.Content = new ErrorPage("AddLiveDashboard", string.Format("Cannot add dashboard '{0}'. Exception: {1}", dashboardContext.DashboardName, ex));
            }
        }

        private void RemoveLiveDashboard(string name)
        {
            if (_currentDashboard == name)
                mainFrame.Content = null;

            _liveDashboardObjectScopes.Remove(name);
            AggregateStates.UnregisterDashboard(name);

            for (var i = 0; i < _liveDashboardsListBox.Items.Count; i++)
            {
                var currentName = (string)(_liveDashboardsListBox.Items[i] as ListBoxItem).Content;

                if (name != currentName)
                    continue;

                var nextDashboardIndex = i + 1;

                if (_liveDashboardsListBox.Items.Count > 1)
                {
                    if (nextDashboardIndex > _liveDashboardsListBox.Items.Count - 1)
                        nextDashboardIndex = 0;
                    var nextDashboardName = (string)(_liveDashboardsListBox.Items[nextDashboardIndex] as ListBoxItem).Content;

                    ShowLiveDashboard(nextDashboardName);
                }
                else
                {
                    mainFrame.Content = null;
                }

                _liveDashboardsListBox.Items.RemoveAt(i);
                break;
            }
        }

        private void JustifyMainFrameDecorator()
        {
            mainFrameDecoratorFigure.StartPoint = new Point(0, upperRow.Height.Value);
            mainFrameUpperDecorator.StartPoint = new Point(0, upperRow.Height.Value);

            double gridWidth = layoutGrid.ActualWidth;
            double gridHeight = layoutGrid.ActualHeight;

            double figure1PositionX = gridWidth - TitleWidth - 25;

            mainFrameDecoratorFigure.Segments.Clear();

            // Top Decorator Border
            mainFrameDecoratorFigure.Segments.Add(new LineSegment(new Point(figure1PositionX, upperRow.Height.Value), false));
            mainFrameDecoratorFigure.Segments.Add(new LineSegment(new Point(figure1PositionX + 25, upperRow.Height.Value - 25), false));
            mainFrameDecoratorFigure.Segments.Add(new LineSegment(new Point(gridWidth, upperRow.Height.Value - 25), false));

            // Right
            mainFrameDecoratorFigure.Segments.Add(new LineSegment(new Point(gridWidth, gridHeight - lowerRow.Height.Value), false));

            // Bottom Decorator
            double bottomLeftPoint = gridWidth - mainFrame.ActualWidth;

            mainFrameDecoratorFigure.Segments.Add(new LineSegment(new Point(bottomLeftPoint + 60, gridHeight - lowerRow.Height.Value), true));
            mainFrameDecoratorFigure.Segments.Add(new LineSegment(new Point(bottomLeftPoint + 35, gridHeight - lowerRow.Height.Value + 25), true));
            mainFrameDecoratorFigure.Segments.Add(new LineSegment(new Point(bottomLeftPoint, gridHeight - lowerRow.Height.Value + 25), true));


            mainFrameUpperDecorator.Segments.Clear();
            // Top Decorator
            mainFrameUpperDecorator.Segments.Add(new LineSegment(new Point(figure1PositionX, upperRow.Height.Value), true));
            mainFrameUpperDecorator.Segments.Add(new LineSegment(new Point(figure1PositionX + 25, upperRow.Height.Value - 25), true));
            mainFrameUpperDecorator.Segments.Add(new LineSegment(new Point(gridWidth, upperRow.Height.Value - 25), true));
        }

        #endregion
    }
}