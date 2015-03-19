using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace Snitch_Map_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Snitch_Map.ExcelReader exr;
        private static Snitch_Map.Map map;
        private Thread mainThread;
        private Thread mapThread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result==true)
            {
                string filename = dlg.FileName;
                Input.Text = filename;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            Output.Text = dialog.SelectedPath;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (IsValidFile(Output.Text) && IsValidFile(Input.Text))
            {
                int fact = Int16.Parse(Factor.Text); 
                int rad = Int16.Parse(Radius.Text);
                map = new Snitch_Map.Map(rad, fact, Output.Text);
                exr = new Snitch_Map.ExcelReader(Input.Text);

                mainThread = new Thread(new ThreadStart(Threader));
                mainThread.Start();
            }
        }

        public void Threader()
        {
            exr.Scan();
            map.X = exr.X;
            map.Y = exr.Y;
            map.Create();
        }

        public bool IsValidFile(string path)
        {
            System.Text.RegularExpressions.Regex containsABadCharacter = new System.Text.RegularExpressions.Regex("["
                  + System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidPathChars())) + "]");
            if (containsABadCharacter.IsMatch(path))
                return false; 
            return true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void progBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
