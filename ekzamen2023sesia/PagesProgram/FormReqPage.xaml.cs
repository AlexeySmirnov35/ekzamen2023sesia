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
    /// Логика взаимодействия для FormReqPage.xaml
    /// </summary>
    public partial class FormReqPage : Page
    {
        User _user=new User();
        Request _requ=new Request();
        public FormReqPage(Request selReq,User autuser)
        {
            InitializeComponent();
            if (selReq!=null)
            {
                _requ = selReq;
            }
            DataContext = _requ;
            if(autuser!=null)
            {
                _user= autuser;
            }
            if (_user.IdRole != 1)
            {
                cbStatus.Visibility= Visibility.Collapsed;
            }
            cbStatus.ItemsSource = AppEntities.GetContext().Status.ToList();
        }

        private void SaveRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(tbDesc.Text))
                {
                    MessageBox.Show("net");
                    return;
                }
                    
                var dbContext = AppEntities.GetContext();

                if (_requ.Id == 0) // Новая заявка
                {
                    Request req;
                    if (_user.IdRole == 1) // Если администратор
                    {
                        var selectedStatus = cbStatus.SelectedItem as Status;
                        req = new Request
                        {
                            StartDate = DateTime.Now,
                            IdUser = Convert.ToInt32(_user.Id),
                            Description = tbDesc.Text,
                            IdStatus= selectedStatus.Id,
                        };
                    }
                    else if(_user.IdRole==3) // Если пользователь 
                    {
                        req = new Request
                        {
                            StartDate = DateTime.Now,
                            IdUser = Convert.ToInt32(_user.Id),
                            Description = tbDesc.Text,
                            IdStatus = 1,
                        };

                    }

                    else
                    {
                        req = new Request
                        {
                            StartDate = DateTime.Now,
                            IdUser = 1002,
                            Description = tbDesc.Text,
                            IdStatus = 1,
                        };
                    }

                    dbContext.Request.Add(req);
                }
                else // Редактирование существующей заявки
                {
                   if(string.IsNullOrWhiteSpace(tbDesc.Text)) 
                    { 
                        MessageBox.Show("net");
                        return;
                    }
                   if(cbStatus.SelectedItem==null)
                    {
                        MessageBox.Show("net");
                        return;
                    }
                    var selectedStatus = cbStatus.SelectedItem as Status;
                    _requ.IdStatus = selectedStatus.Id;
                    _requ.Description = tbDesc.Text;
                }

                dbContext.SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
