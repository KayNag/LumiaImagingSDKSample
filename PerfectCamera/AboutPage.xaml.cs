/*
 * Copyright (c) 2014 Microsoft Mobile
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using PerfectCamera.Resources;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;

namespace PerfectCamera
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();

            // Application version number

            var xElement = XDocument.Load("WMAppManifest.xml").Root;
            if (xElement != null)
            {
                var version = xElement.Element("App").Attribute("Version").Value;

                var versionRun = new Run()
                {
                    Text = String.Format(AppResources.AboutPage_VersionRun, version) + "\n"
                };

                VersionParagraph.Inlines.Add(versionRun);
            }

            // Application about text
            var termlink = new Hyperlink();

            termlink.Inlines.Add(AppResources.Aboutservicetermlink);

            var aboutterms = new Run()
            {
                Text = AppResources.Aboutterms + "\n"
            };
            termlink.Click += ProjectsLink_Click;
            termlink.Foreground = new SolidColorBrush((Color)App.Current.Resources["PhoneForegroundColor"]);
            termlink.MouseOverForeground = new SolidColorBrush((Color)App.Current.Resources["PhoneAccentColor"]);

            AboutParagraph.Inlines.Add(aboutterms);

            // Application about text
            var policylink = new Hyperlink();

            termlink.Inlines.Add(AppResources.AboutPrivacyPolicylink);
            var aboutpolicy = new Run()
            {
                Text = AppResources.Aboutprivacy + "\n"
            };

            AboutParagraph.Inlines.Add(aboutpolicy);
            policylink.Click += ProjectsLink_Click;
            policylink.Foreground = new SolidColorBrush((Color)App.Current.Resources["PhoneForegroundColor"]);
            policylink.MouseOverForeground = new SolidColorBrush((Color)App.Current.Resources["PhoneAccentColor"]);

            // Link to project homepage

            var projectRunText = AppResources.AboutPage_ProjectRun;
            var projectRunTextSpans = projectRunText.Split(new string[] { "{0}" }, StringSplitOptions.None);

            var projectRunSpan1 = new Run {Text = projectRunTextSpans[0]};

           

            var projectRunSpan2 = new Run {Text = projectRunTextSpans[1] + "\n"};

            ProjectParagraph.Inlines.Add(projectRunSpan1);
            ProjectParagraph.Inlines.Add(termlink);
            ProjectParagraph.Inlines.Add(policylink);
            ProjectParagraph.Inlines.Add(projectRunSpan2);
        }

        private void ProjectsLink_Click(object sender, RoutedEventArgs e)
        {
            var webBrowserTask = new WebBrowserTask()
            {
                Uri = new Uri(AppResources.AboutPage_Hyperlink_Project, UriKind.Absolute)
            };

            webBrowserTask.Show();
        }
    }
}