﻿#pragma checksum "C:\Users\cynos_000\Documents\newsample\LumiaImagingSDKSample\PerfectCamera\Download.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D1272550FA75E5B88A385C2422E4482B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PerfectCamera {
    
    
    public partial class Download : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Shapes.Rectangle top1;
        
        internal System.Windows.Controls.Image fbar;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Image icol;
        
        internal System.Windows.Controls.Image irow;
        
        internal System.Windows.Media.VideoBrush viewfinderBrush;
        
        internal System.Windows.Media.CompositeTransform videoTransform;
        
        internal System.Windows.Controls.Image myimage;
        
        internal System.Windows.Controls.Image MainImage;
        
        internal System.Windows.Controls.TextBlock txtHtml;
        
        internal System.Windows.Controls.TextBlock txtDebug;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton btnFile;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton BtnCam;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton tglbtn;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PerfectCamera;component/Download.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.top1 = ((System.Windows.Shapes.Rectangle)(this.FindName("top1")));
            this.fbar = ((System.Windows.Controls.Image)(this.FindName("fbar")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.icol = ((System.Windows.Controls.Image)(this.FindName("icol")));
            this.irow = ((System.Windows.Controls.Image)(this.FindName("irow")));
            this.viewfinderBrush = ((System.Windows.Media.VideoBrush)(this.FindName("viewfinderBrush")));
            this.videoTransform = ((System.Windows.Media.CompositeTransform)(this.FindName("videoTransform")));
            this.myimage = ((System.Windows.Controls.Image)(this.FindName("myimage")));
            this.MainImage = ((System.Windows.Controls.Image)(this.FindName("MainImage")));
            this.txtHtml = ((System.Windows.Controls.TextBlock)(this.FindName("txtHtml")));
            this.txtDebug = ((System.Windows.Controls.TextBlock)(this.FindName("txtDebug")));
            this.btnFile = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("btnFile")));
            this.BtnCam = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("BtnCam")));
            this.tglbtn = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("tglbtn")));
        }
    }
}

