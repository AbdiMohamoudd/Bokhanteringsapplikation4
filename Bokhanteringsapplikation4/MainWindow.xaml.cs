using System;
using System.Windows;
using System.Windows.Controls;

namespace GymApp
{
    public partial class MainWindow : Window
    {
        private Bokhantering bokhantering;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            bokhantering = new Bokhantering();

            bokhantering.LäggTillPass(new Pass { Type = "Boxning", Date = DateTime.Now.Date, Time = "10:00" });
            bokhantering.LäggTillPass(new Pass { Type = "Yoga", Date = DateTime.Now.Date, Time = "12:00" });
            bokhantering.LäggTillPass(new Pass { Type = "Vikter", Date = DateTime.Now.AddDays(1).Date, Time = "15:00" });

            UpdateListView();
        }

        private void UpdateListView()
        {
            listView.ItemsSource = bokhantering.HämtaPass();
        }

        private void ClearTextOnFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Clear();
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            var typ = TypeFilter.Text;
            var datum = DateFilter.Text;
            var tid = TimeFilter.Text;

            var filtreradePass = bokhantering.FiltreraPass(
                typ == "Pass-typ" ? string.Empty : typ,
                datum == "Datum (yyyy-mm-dd)" ? string.Empty : datum,
                tid == "Tid (hh:mm)" ? string.Empty : tid
            );

            listView.ItemsSource = filtreradePass;
        }

        private void BokaPass_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is Pass valtPass)
            {
                MessageBox.Show($"Du har bokat passet: {valtPass.Type} kl. {valtPass.Time} på {valtPass.Date.ToString("yyyy-MM-dd")}");
            }
            else
            {
                MessageBox.Show("Vänligen välj ett pass att boka.");
            }
        }

        private void TabortPass_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is Pass valtPass)
            {
                bokhantering.TaBortPass(valtPass);
                UpdateListView();
                MessageBox.Show($"Passet {valtPass.Type} har tagits bort.");
            }
            else
            {
                MessageBox.Show("Vänligen välj ett pass att ta bort.");
            }
        }
    }
}

