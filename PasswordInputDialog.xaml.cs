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

namespace Now_You_See_Me
{
    /// <summary>
    /// Interaction logic for PasswordInputDialog.xaml
    /// </summary>
    public partial class PasswordInputDialog : Window
    {
        public string Password { get; private set; }

        public PasswordInputDialog()
        {
            InitializeComponent();
            PasswordInput.Focus();


        }

        private void PasswordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Password = PasswordInput.Password;
                DialogResult = true;
            }
        }


    }
}
