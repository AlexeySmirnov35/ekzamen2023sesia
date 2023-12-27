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

namespace ekzamen2023sesia.PagesProgram
{
    /// <summary>
    /// Логика взаимодействия для AutorizatPage.xaml
    /// </summary>
    public partial class AutorizatPage : Page
    {
        public AutorizatPage()
        {
            InitializeComponent();
        }
        
        private void Autor_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = AppEntities.GetContext().User.FirstOrDefault(x => x.Login == tbLog.Text && x.Password == tbPas.Password);
                if (userObj==null)
                {
                    return;
                }
                else
                {
                    switch (userObj.IdRole)
                    {
                        case 1:
                            NavigationService.Navigate(new RequstInfoPage(userObj));
                            break;
                        case 3:
                            NavigationService.Navigate(new LKabPage(userObj));
                            break;
                        default: 
                            break;
                    }
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }

        private void AutorGuest_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Guest");
            NavigationService.Navigate(new FormReqPage(null,null));

        }
    }
}
