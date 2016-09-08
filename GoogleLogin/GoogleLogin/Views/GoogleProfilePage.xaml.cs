using System.Linq;
using GoogleLogin.Services;
using GoogleLogin.ViewModels;
using Xamarin.Forms;

namespace GoogleLogin.Views
{
    public partial class GoogleProfilePage : ContentPage
    {

        private readonly GoogleViewModel _googleViewModel;

        public GoogleProfilePage()
        {
            InitializeComponent();

            _googleViewModel = BindingContext as GoogleViewModel;

            var authRequest =
                  "https://accounts.google.com/o/oauth2/v2/auth"
                  + "?response_type=code"
                  + "&scope=openid"
                  + "&redirect_uri=" + GoogleServices.RedirectUri
                  + "&client_id=" + GoogleServices.ClientId;

            var webView = new WebView
            {
                Source = authRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var code = ExtractCodeFromUrl(e.Url);

            if (code != "")
            {

                var accessToken = await _googleViewModel.GetAccessTokenAsync(code);

                await _googleViewModel.SetGoogleUserProfileAsync(accessToken);

                Content = MainStackLayout;
            }
        }

        private string ExtractCodeFromUrl(string url)
        {
            if (url.Contains("code="))
            {
                var attributes = url.Split('&');

                var code = attributes.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];

                return code;
            }

            return string.Empty;
        }
    }
}
