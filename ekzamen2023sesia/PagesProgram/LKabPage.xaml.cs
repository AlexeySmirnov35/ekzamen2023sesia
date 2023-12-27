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
    /// Логика взаимодействия для LKabPage.xaml
    /// </summary>
    public partial class LKabPage : Page
    {
        private User _user = new User();
        public LKabPage(User selUser)
        {
            InitializeComponent();
            _user = selUser;
            listView.ItemsSource = AppEntities.GetContext().Request.Where(x => x.IdUser == selUser.Id).ToList();
        }

    }
}
