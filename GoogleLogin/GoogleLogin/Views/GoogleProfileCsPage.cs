using System.Linq;
using GoogleLogin.ViewModels;
using GoogleLogin.Models;
using GoogleLogin.Services;
using Xamarin.Forms;

namespace GoogleLogin.Views
{
    public class GoogleProfileCsPage : ContentPage
    {

        private readonly GoogleViewModel _googleViewModel = new GoogleViewModel();

        public GoogleProfileCsPage()
        {

            BindingContext = _googleViewModel;

            Title = "Google Profile";
            BackgroundColor = Color.White;

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

                SetPageContent(_googleViewModel.GoogleProfile);
            }
        }

        private void SetPageContent(GoogleProfile googleProfile)
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(8, 30),
                Children =
                {
                    new Label
                    {
                        Text = googleProfile.DisplayName,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Id,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Verified.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Gender,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Tagline,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.CircledByCount.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Occupation,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Xamarin.Forms.Image
                    {
                         Source = googleProfile.Image.Url,
                         HeightRequest = 100
                    },
                     new Xamarin.Forms.Image
                    {
                         Source = googleProfile.Cover.CoverPhoto.Url,
                         HeightRequest = 100
                    },
                }
            };
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

// To use XAML pages, use the following code:
//
//<? xml version="1.0" encoding="utf-8" ?>
//<ContentPage xmlns = "http://xamarin.com/schemas/2014/forms"
//             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
//             xmlns:viewModels="clr-namespace:GoogleLogin.ViewModels;assembly=GoogleLogin"
//             x:Class="GoogleLogin.Views.GoogleProfilePage"
//             Title="Google Profile"
//             BackgroundColor="White">

//  <ContentPage.BindingContext>
//    <viewModels:GoogleViewModel/>
//  </ContentPage.BindingContext>

//  <StackLayout x:Name="MainStackLayout"
//               Padding="8,0"
//               Orientation="Vertical">

//    <StackLayout Orientation = "Horizontal"
//                 Padding="0,20,0,50">

//      <Image Source = "{Binding GoogleProfile.Image.Url}"
//             HeightRequest="100"
//             WidthRequest="100"
//             VerticalOptions="Start"/>

//      <StackLayout Orientation = "Vertical" >

//        < Label Text="{Binding GoogleProfile.DisplayName, StringFormat='DisplayName: {0:N}'}"
//               TextColor="Black"
//               Font="Bold"
//               FontSize="18"/>

//        <Label Text = "{Binding GoogleProfile.Id, StringFormat='Id: {0:N}'}"
//               TextColor="Black"
//               FontSize="16"/>

//        <Label Text = "{Binding GoogleProfile.Occupation, StringFormat='Occupation: {0:N}'}"
//                 TextColor="Black"
//                 FontSize="16"/>

//        <Label Text = "{Binding GoogleProfile.Tagline, StringFormat='Tagline: {0:N}'}"
//                  TextColor="Black"
//                  FontSize="16"/>

//      </StackLayout>

//    </StackLayout>

//    <Label Text = "{Binding GoogleProfile.CircledByCount, StringFormat='CircledByCount: {0:N}'}"
//           TextColor="Black"
//           FontSize="16"/>

//    <Label Text = "{Binding GoogleProfile.Verified, StringFormat='Verified: {0:N}'}"
//           TextColor="Black"
//           FontSize="16"/>

//    <Image Source = "{Binding GoogleProfile.Cover.CoverPhoto.Url}"
//           HeightRequest="200" />

//  </StackLayout>

//</ContentPage>
/////////////////////////////////////////////////////////////////////////////////////////////////
// and the following for xaml.cs
//
//using System.Linq;
//using GoogleLogin.ViewModels;
//using GoogleLogin.Models;
//using GoogleLogin.Services;
//using Xamarin.Forms;

//namespace GoogleLogin.Views
//{
//    public class GoogleProfileCsPage : ContentPage
//    {

//        private readonly GoogleViewModel _googleViewModel = new GoogleViewModel();

//        public GoogleProfileCsPage()
//        {

//            BindingContext = _googleViewModel;

//            Title = "Google Profile";
//            BackgroundColor = Color.White;

//            var authRequest =
//                  "https://accounts.google.com/o/oauth2/v2/auth"
//                  + "?response_type=code"
//                  + "&scope=openid"
//                  + "&redirect_uri=" + GoogleServices.RedirectUri
//                  + "&client_id=" + GoogleServices.ClientId;

//            var webView = new WebView
//            {
//                Source = authRequest,
//                HeightRequest = 1
//            };

//            webView.Navigated += WebViewOnNavigated;

//            Content = webView;
//        }

//        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
//        {

//            var code = ExtractCodeFromUrl(e.Url);

//            if (code != "")
//            {

//                var accessToken = await _googleViewModel.GetAccessTokenAsync(code);

//                await _googleViewModel.SetGoogleUserProfileAsync(accessToken);

//                SetPageContent(_googleViewModel.GoogleProfile);
//            }
//        }

//        private void SetPageContent(GoogleProfile googleProfile)
//        {
//            Content = new StackLayout
//            {
//                Orientation = StackOrientation.Vertical,
//                Padding = new Thickness(8, 30),
//                Children =
//                {
//                    new Label
//                    {
//                        Text = googleProfile.DisplayName,
//                        TextColor = Color.Black,
//                        FontSize = 22,
//                    },
//                    new Label
//                    {
//                        Text = googleProfile.Id,
//                        TextColor = Color.Black,
//                        FontSize = 22,
//                    },
//                    new Label
//                    {
//                        Text = googleProfile.Verified.ToString(),
//                        TextColor = Color.Black,
//                        FontSize = 22,
//                    },
//                    new Label
//                    {
//                        Text = googleProfile.Gender,
//                        TextColor = Color.Black,
//                        FontSize = 22,
//                    },
//                    new Label
//                    {
//                        Text = googleProfile.Tagline,
//                        TextColor = Color.Black,
//                        FontSize = 22,
//                    },
//                    new Label
//                    {
//                        Text = googleProfile.CircledByCount.ToString(),
//                        TextColor = Color.Black,
//                        FontSize = 22,
//                    },
//                    new Label
//                    {
//                        Text = googleProfile.Occupation,
//                        TextColor = Color.Black,
//                        FontSize = 22,
//                    },
//                    new Xamarin.Forms.Image
//                    {
//                         Source = googleProfile.Image.Url,
//                         HeightRequest = 100
//                    },
//                     new Xamarin.Forms.Image
//                    {
//                         Source = googleProfile.Cover.CoverPhoto.Url,
//                         HeightRequest = 100
//                    },
//                }
//            };
//        }

//        private string ExtractCodeFromUrl(string url)
//        {
//            if (url.Contains("code="))
//            {
//                var attributes = url.Split('&');

//                var code = attributes.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];

//                return code;
//            }

//            return string.Empty;
//        }
//    }
//}
