﻿#pragma checksum "..\..\..\Controls\Cylinder.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7999F61F59673A9DF77767796F968A78"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace StandartObjectLibrary {
    
    
    /// <summary>
    /// Cylinder
    /// </summary>
    public partial class Cylinder : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\Controls\Cylinder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal StandartObjectLibrary.Cylinder UserControl;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Controls\Cylinder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.RectangleGeometry clipGeometry;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Controls\Cylinder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.GeometryDrawing fillGeometry;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Controls\Cylinder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.GeometryDrawing highlightEllipse;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Controls\Cylinder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.EllipseGeometry highlightEllipseGeometry;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StandartObjectLibrary;component/controls/cylinder.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\Cylinder.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserControl = ((StandartObjectLibrary.Cylinder)(target));
            return;
            case 2:
            this.clipGeometry = ((System.Windows.Media.RectangleGeometry)(target));
            return;
            case 3:
            this.fillGeometry = ((System.Windows.Media.GeometryDrawing)(target));
            return;
            case 4:
            this.highlightEllipse = ((System.Windows.Media.GeometryDrawing)(target));
            return;
            case 5:
            this.highlightEllipseGeometry = ((System.Windows.Media.EllipseGeometry)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
