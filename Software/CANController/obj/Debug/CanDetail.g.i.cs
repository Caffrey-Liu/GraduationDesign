﻿#pragma checksum "..\..\CanDetail.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "233DA576E74E267551C914CD1D86041FB1D19DE48002E53F5B355FC6C43BD83F"
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
    /// CanDetail
    /// </summary>
    public partial class CanDetail : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CANController.CanDetail CANInfo;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DeviceType;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DeviceIndex;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Tunnel;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox BaudRate;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FilteringMethod;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Mode;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SendingType;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FrameForm;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FrameType;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FrameID;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FrameData;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\CanDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FrameNumber;
        
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
            System.Uri resourceLocater = new System.Uri("/CANController;component/candetail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CanDetail.xaml"
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
            this.CANInfo = ((CANController.CanDetail)(target));
            return;
            case 2:
            this.DeviceType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.DeviceIndex = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.Tunnel = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.BaudRate = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.FilteringMethod = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.Mode = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.SendingType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.FrameForm = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.FrameType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            
            #line 114 "..\..\CanDetail.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CAN_SendData);
            
            #line default
            #line hidden
            return;
            case 12:
            this.FrameID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.FrameData = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            this.FrameNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

