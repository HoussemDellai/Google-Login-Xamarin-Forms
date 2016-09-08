using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GoogleLogin.Models;
using GoogleLogin.Services;

namespace GoogleLogin.ViewModels
{
    public class GoogleViewModel : INotifyPropertyChanged
    {
        private GoogleProfile _googleProfile;
        private readonly GoogleServices _googleServices;

        public GoogleProfile GoogleProfile
        {
            get { return _googleProfile; }
            set
            {
                _googleProfile = value;
                OnPropertyChanged();
            }
        }

        public GoogleViewModel()
        {
            _googleServices = new GoogleServices();
        }
        
        public async Task<string> GetAccessTokenAsync(string code)
        {

            var accessToken = await _googleServices.GetAccessTokenAsync(code);

            return accessToken;
        }

        public async Task SetGoogleUserProfileAsync(string accessToken)
        {

            GoogleProfile = await _googleServices.GetGoogleUserProfileAsync(accessToken);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
