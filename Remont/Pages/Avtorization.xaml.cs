﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Remont.Pages
{
    /// <summary>
    /// Логика взаимодействия для Avtorization.xaml
    /// </summary>
    public partial class Avtorization : Page
    {
        public Avtorization()
        {
            InitializeComponent();
        }

        private void TextBoxLogin_Changed(object sender, RoutedEventArgs e)
        {
            txtHintLogin.Visibility = Visibility.Visible;
            if (TextBoxLogin.Text.Length > 0)
            {
                txtHintLogin.Visibility = Visibility.Hidden;
            }
        }
        private void PasswordBoxPassword_Changed(object sender, RoutedEventArgs e)
        {
            txtHintPassword.Visibility = Visibility.Visible;
            if (PasswordBoxPassword.Password.Length > 0)
            {
                txtHintPassword.Visibility = Visibility.Hidden;
            }
        }
        
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBoxPassword.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            using (var db = new EquipmentRepairDBEntities1())
            {

                var user = db.User.AsNoTracking().FirstOrDefault(u => u.NameUser == TextBoxLogin.Text && u.Password == PasswordBoxPassword.Password);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return;
                }
                else
                {
                    MessageBox.Show("Пользователь успешно найден!");
                    switch (user.Role)
                    {
                        case "Admin":
                            NavigationService?.Navigate(new AdminPage());
                            break;
                            //case "пользователь":
                            //    NavigationService?.Navigate(new UserPage());
                            //  break;
                    }
                }

            }
        }
    }
}
