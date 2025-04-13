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
using System.Windows.Shapes;
using Tema2_MemoryGame.ViewModel;

namespace Tema2_MemoryGame.View
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();

            (DataContext as SignInVM).PlayButtonClicked += OpenMenu;
        }

        private void OpenMenu(object sender, EventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow("Welcome!", (DataContext as SignInVM).SelectedUser);
            menuWindow.Show();
            Close();
        }
    }
}
