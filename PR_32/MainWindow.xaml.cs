using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR_32
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public Pages.Records.Main mainRecords = new Pages.Records.Main();
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPage(mainRecords);
        }

        public void OpenPage(Page pages)
        {
            frame.Navigate(pages);
        }
        private void OpenRecordList(object sender, RoutedEventArgs e)
        {
            OpenPage(mainRecords);
            mainRecords.LoadRecord();
        }

        private void OpenRecordAdd(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Records.Add());
        }

        private void ExportRecord(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xslx";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
                Classes.Record.Export(saveFileDialog.FileName, mainRecords.searchRecords);
        }

        private void OpenManufacturesList(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Manufacturer.Main());
        }

        private void OpenManufacturesAdd(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Records.Add());
        }

        private void OpenSupplyList(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Supply.Main());
        }

        private void OpenSupplyAdd(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Supply.Add());
        }

        private void OpenStateList(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.State.Main());
        }

        private void OpenStateAdd(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.State.Add());
        }
    }
}

