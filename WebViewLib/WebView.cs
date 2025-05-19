using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms.Integration;

namespace WebViewLib
{
    public class WebViewHost : WindowsFormsHost
    {
        public WebView2 WebView { get; private set; }
        CoreWebView2Environment _environment;
        bool _isIPhoneMode = true;

        public WebViewHost(bool isIPhoneMode = true)
        {
            _isIPhoneMode = isIPhoneMode;
            WebView = new WebView2 { AllowExternalDrop = false };
            this.Child = WebView;           
            SetCore(isIPhoneMode);
        }

        public async void Navigate(string url)
        {
            try
            {
                await WebView.EnsureCoreWebView2Async(_environment);
                if (_isIPhoneMode)
                {
                    if (url.Contains("dicta.org") == true)
                        WebView.CoreWebView2.Settings.UserAgent = null;
                    else
                        WebView.CoreWebView2.Settings.UserAgent = "Mozilla/5.0 (Linux; Android 12; Pixel 6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.100 Mobile Safari/537.36";
                }
                WebView.CoreWebView2.Navigate(url);
            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
        }

        async void SetCore(bool iPhoneMode = true)
        {
            string tempWebCacheDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _environment = await CoreWebView2Environment.CreateAsync(userDataFolder: tempWebCacheDir);           
        }
    }
}
