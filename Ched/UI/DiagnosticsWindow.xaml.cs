using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Ched.Plugins;
using System.Globalization;

namespace Ched.UI
{
    /// <summary>
    /// Interaction logic for DiagnosticsView.xaml
    /// </summary>
    public partial class DiagnosticsWindow : Window
    {
        public DiagnosticsWindow()
        {
            InitializeComponent();
        }
    }

    public class DiagnosticsViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string message;
        private ObservableCollection<Diagnostic> diagnostics;

        public string Message
        {
            get => message;
            set
            {
                if (value == message) return;
                message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }
        public ObservableCollection<Diagnostic> Diagnostics
        {
            get => diagnostics;
            set
            {
                if (value == diagnostics) return;
                diagnostics = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Diagnostics)));
            }
        }
    }

    public class DiagnosticViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Diagnostic source;

        public DiagnosticViewModel(Diagnostic diagnostic)
        {
            source = diagnostic;
        }

        public DiagnosticSeverity Severity
        {
            get => source.Severity;
        }

        public string Message
        {
            get => source.Message;
        }
    }

    public class BitmapImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stream = new System.IO.MemoryStream();
            ((System.Drawing.Bitmap)value).Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var image = new BitmapImage();
            image.BeginInit();
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
