﻿#pragma checksum "..\..\..\..\EditDataScreen\EditDataWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5CDF549D70409B8322AE0089EA66F53D10513403"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Core;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Bars.TypedStyles;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.TypedStyles;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.DataSources.TypedStyles;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.Serialization.TypedStyles;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.Core.ServerMode.TypedStyles;
using DevExpress.Xpf.Core.TypedStyles;
using DevExpress.Xpf.DXBinding;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Ribbon.TypedStyles;
using DevExpress.Xpf.Spreadsheet;
using DevExpress.Xpf.Spreadsheet.Menu;
using DevExpress.Xpf.Spreadsheet.Menu.TypedStyles;
using DevExpress.Xpf.Spreadsheet.TypedStyles;
using DevExpress.Xpf.Spreadsheet.UI;
using DevExpress.Xpf.Spreadsheet.UI.TypedStyles;
using DevExpress.Xpf.TypedStyles;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
using PlantConstructor.WPF.EditDataScreen;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.TypedStyles;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Primitives.TypedStyles;
using System.Windows.Controls.Ribbon;
using System.Windows.Controls.TypedStyles;
using System.Windows.Data;
using System.Windows.Data.TypedStyles;
using System.Windows.Documents;
using System.Windows.Documents.TypedStyles;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.TypedStyles;
using System.Windows.Markup;
using System.Windows.Markup.TypedStyles;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Animation.TypedStyles;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Media.TypedStyles;
using System.Windows.Navigation;
using System.Windows.Navigation.TypedStyles;
using System.Windows.Shapes;
using System.Windows.Shapes.TypedStyles;
using System.Windows.Shell;
using System.Windows.TypedStyles;


namespace PlantConstructor.WPF.EditDataScreen {
    
    
    /// <summary>
    /// EditDataWindow
    /// </summary>
    public partial class EditDataWindow : DevExpress.Xpf.Core.ThemedWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\EditDataScreen\EditDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Spreadsheet.SpreadsheetControl spreadsheet;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\EditDataScreen\EditDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LogText;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PlantConstructor.WPF;V2.0.0.0;component/editdatascreen/editdatawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\EditDataScreen\EditDataWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.spreadsheet = ((DevExpress.Xpf.Spreadsheet.SpreadsheetControl)(target));
            
            #line 26 "..\..\..\..\EditDataScreen\EditDataWindow.xaml"
            this.spreadsheet.BeforeExport += new DevExpress.Spreadsheet.BeforeExportEventHandler(this.RemoveProtectionBeforeSave);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\..\EditDataScreen\EditDataWindow.xaml"
            this.spreadsheet.DocumentSaved += new System.EventHandler(this.AddProtectionAfterSave);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 45 "..\..\..\..\EditDataScreen\EditDataWindow.xaml"
            ((DevExpress.Xpf.Bars.BarButtonItem)(target)).ItemClick += new DevExpress.Xpf.Bars.ItemClickEventHandler(this.CreateCode_ItemClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 48 "..\..\..\..\EditDataScreen\EditDataWindow.xaml"
            ((DevExpress.Xpf.Bars.BarButtonItem)(target)).ItemClick += new DevExpress.Xpf.Bars.ItemClickEventHandler(this.SaveToDB_ItemClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 51 "..\..\..\..\EditDataScreen\EditDataWindow.xaml"
            ((DevExpress.Xpf.Bars.BarButtonItem)(target)).ItemClick += new DevExpress.Xpf.Bars.ItemClickEventHandler(this.ImportData_ItemClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LogText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

