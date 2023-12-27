using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ekzamen2023sesia.PagesProgram;
namespace ekzamen2023sesia.PagesProgram
{
    /// <summary>
    /// Логика взаимодействия для RequstInfoPage.xaml
    /// </summary>
    public partial class RequstInfoPage : Page
    {
        User _user = new User();
        public RequstInfoPage(User autorUser)
        {
            InitializeComponent();
            if (autorUser != null)
            {
                _user = autorUser;
            }
            var reu= AppEntities.GetContext().Request.ToList();
            lView.ItemsSource = reu.OrderBy(x => x.StartDate);
            tbFIO.Text = $"{_user.Surname.ToString()} {_user.Name.ToString()}";
            var alltype = AppEntities.GetContext().Status.ToList();
            alltype.Insert(0, new Status
            {
                Name = "All"
            });
            FiltrBox.ItemsSource = alltype;
            FiltrBox.SelectedIndex = 0;
            SortBox.SelectedIndex = 0;
            UpdateReq();
        }

        private void EditReq_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FormReqPage((sender as Button).DataContext as Request,_user));
        }

        private void DelitReq_Click(object sender, RoutedEventArgs e)
        {
            var reqDel = lView.SelectedItems.Cast<Request>().ToList();
            if (MessageBox.Show("del?", "DDELTE", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    AppEntities.GetContext().Request.RemoveRange(reqDel);
                    AppEntities.GetContext().SaveChanges();
                    MessageBox.Show("uspexDel");
                    lView.ItemsSource = AppEntities.GetContext().Request.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void UpdateReq()
        {
            var curReq = AppEntities.GetContext().Request.ToList();
            if (FiltrBox.SelectedIndex > 0)
            {
                var selectedStatus = FiltrBox.SelectedItem as Status;
                if (selectedStatus != null)
                {
                    curReq = curReq.Where(x => x.Status.Name.Contains(selectedStatus.Name)).ToList();
                }
            }
            curReq = curReq.Where(x => x.Description.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            switch (SortBox.SelectedIndex)
            {
                case 0:
                    curReq = curReq.OrderBy(date => date.StartDate).ToList();
                    break;
                case 1:
                    curReq = curReq.OrderByDescending(date => date.StartDate).ToList();
                    break;
                
            }
            
            lView.ItemsSource = curReq;
        }
        private void Search_Click(object sender, TextChangedEventArgs e)
        {
            UpdateReq();
        }

        private void FiltrChange(object sender, SelectionChangedEventArgs e)
        {
            UpdateReq();
        }

        private void SortChange(object sender, SelectionChangedEventArgs e)
        {
            UpdateReq();
        }

        private void TextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            tbSearch.Text = null;
            FiltrBox.SelectedIndex = 0;
            SortBox.SelectedIndex = 0;

        }

        private void UpdateReqVis_change(object sender, DependencyPropertyChangedEventArgs e)
        {            
                UpdateReq();
        }
    }
}
