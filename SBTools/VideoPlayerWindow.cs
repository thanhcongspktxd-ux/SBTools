using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// Định danh tường minh tránh xung đột Color giữa System.Drawing và System.Windows.Media
using WpfColor = System.Windows.Media.Color;

namespace SBTools
{
    public class VideoPlayerWindow : Window
    {
        private MediaElement _mediaElement;

        public VideoPlayerWindow(string videoPath, string windowTitle)
        {
            Title = $"Hướng dẫn lệnh: {windowTitle}";
            Width = 850;
            Height = 600;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Background = new SolidColorBrush(WpfColor.FromRgb(30, 30, 30));

            // Khởi tạo MediaElement phát video phóng to
            _mediaElement = new MediaElement
            {
                LoadedBehavior = MediaState.Manual,
                UnloadedBehavior = MediaState.Stop,
                Stretch = Stretch.Uniform
            };

            if (File.Exists(videoPath))
            {
                _mediaElement.Source = new Uri(videoPath);
                _mediaElement.Play();
            }

            // Đăng ký sự kiện kết thúc video để tự động tua về đầu hoặc dừng an toàn
            _mediaElement.MediaEnded += (s, e) =>
            {
                _mediaElement.Position = TimeSpan.FromSeconds(0);
                _mediaElement.Play(); // Hoặc dừng hẳn tùy bạn chọn, ở đây để lặp lại hoặc giữ nguyên
            };

            Content = _mediaElement;
        }

        // Bắt sự kiện khi cửa sổ bắt đầu đóng để ngắt kết nối MediaElement triệt để trước khi hủy luồng
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_mediaElement != null)
            {
                try
                {
                    _mediaElement.Stop();
                    _mediaElement.Source = null; // Ngắt hoàn toàn tài nguyên video đang giữ
                }
                catch { }
            }
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _mediaElement = null;
        }
    }
}