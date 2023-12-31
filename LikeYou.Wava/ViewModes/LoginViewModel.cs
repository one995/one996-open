﻿using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LikeYou.Wava.ViewModes
{
    public class LoginViewModel:BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            LoginCommand = new AsyncRelayCommand(Login);
        }

        public string? loginName;
        public string? loginPassword;
        public string? LoginName { get => loginName; set => SetProperty(ref loginName, value); }

        public string? Password { get => loginPassword; set => SetProperty(ref loginPassword, value); }
        public async Task<bool> Login()
        {

            Console.WriteLine(LoginName);
            Console.WriteLine(Password);
            if (loginName=="123456"&&Password=="123456")
            {
                await Task.Delay(100);
                new MainWindow().Show();
                
            }
            return true;
        }
    }
}
