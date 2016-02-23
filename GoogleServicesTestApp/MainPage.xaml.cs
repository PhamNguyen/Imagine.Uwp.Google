using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GoogleServicesTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var clientId = "1076108784587-km9dnh2v3b3n5f5hi6p5mjstb7hqsn19.apps.googleusercontent.com";
            var serectKey = "ydQJtfJicoxfh5TF8vpHfcSj";

            var googleServices = GoogleServices.GoogleApis.Instance;

            googleServices.SetGoogleAppInfo(clientId, serectKey);
            var result = await googleServices.LoginGoogle();

            MessageDialog dialog = null;

            switch (result)
            {
                case GoogleServices.GoogleApis.GoogleApiResult.AuthorizeSuccess:
                    dialog = new MessageDialog("Đăng nhập google thành công");
                    break;
                case GoogleServices.GoogleApis.GoogleApiResult.AuthorizeFailed:
                    dialog = new MessageDialog("Đăng nhập google không thành công. Thử lại đê cưng");
                    break;
                case GoogleServices.GoogleApis.GoogleApiResult.LackOfInfo:
                    dialog = new MessageDialog("Thiếu thông tin App Google");
                    break;
                case GoogleServices.GoogleApis.GoogleApiResult.ExceptionError:
                    dialog = new MessageDialog("Lỗi hệ thống");
                    break;
            }

            if (dialog == null)
            {
                dialog = new MessageDialog("Lỗi hệ thống");
            }

            await dialog.ShowAsync();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var googleServices = GoogleServices.GoogleApis.Instance;

            var result = await googleServices.LoadUserProfile();

            MessageDialog dialog = null;

            switch (result)
            {
                case GoogleServices.GoogleApis.GoogleApiResult.NotAuthorize:
                    dialog = new MessageDialog("Chưa đăng nhập. Vui lòng đăng nhập!");
                    break;
                case GoogleServices.GoogleApis.GoogleApiResult.GetUserInfoSuccess:
                    dialog = new MessageDialog("Lấy thông tin thành công!");
                    break;
                case GoogleServices.GoogleApis.GoogleApiResult.GetUserInfoFailed:
                    dialog = new MessageDialog("Lấy thông tin  không thành công. Thử lại đê cưng!");
                    break;
                case GoogleServices.GoogleApis.GoogleApiResult.LackOfInfo:
                    dialog = new MessageDialog("Thiếu thông tin App Google!");
                    break;
                case GoogleServices.GoogleApis.GoogleApiResult.ExceptionError:
                    dialog = new MessageDialog("Lỗi hệ thống!");
                    break;
            }

            if (dialog == null)
            {
                dialog = new MessageDialog("Lỗi hệ thống!");
            }

            await dialog.ShowAsync();
        }
    }
}
