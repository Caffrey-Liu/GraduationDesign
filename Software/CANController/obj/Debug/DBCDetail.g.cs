﻿#pragma checksum "..\..\DBCDetail.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C381A9E7B11BE8BA5702CF8D9396571986FE3C365896FADF604DD7A5470ABF45"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using CANController;
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
using System.Windows.Shell;


namespace CANController {
    
    
    /// <summary>
    /// DBCDetail
    /// </summary>
    public partial class DBCDetail : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\DBCDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DBCName;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\DBCDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Baudrate;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\DBCDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DBCSimulation;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\DBCDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid MSGInfoDataGrid;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\DBCDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid BITPicture;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\DBCDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid BITPictureText;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\DBCDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid SignalInfoDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/CANController;component/dbcdetail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DBCDetail.xaml"
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
            this.DBCName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Baudrate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            
            #line 45 "..\..\DBCDetail.xaml"
            ((System.Windows.Controls.Grid)(target)).Drop += new System.Windows.DragEventHandler(this.DBC_Drop);
            
            #line default
            #line hidden
            
            #line 45 "..\..\DBCDetail.xaml"
            ((System.Windows.Controls.Grid)(target)).DragEnter += new System.Windows.DragEventHandler(this.DBC_DrapEnter);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DBCSimulation = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\DBCDetail.xaml"
            this.DBCSimulation.Click += new System.Windows.RoutedEventHandler(this.showDBCSimulation);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MSGInfoDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 74 "..\..\DBCDetail.xaml"
            this.MSGInfoDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MSGInfoDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BITPicture = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.BITPictureText = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.SignalInfoDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

