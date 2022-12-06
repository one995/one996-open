using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LikeYou.Wava.ViewModes
{
    public class MainViewModel:BaseViewModel
    {
        public ICommand SwitchItemCmd { get; set; }

        public MainViewModel()
        {
            SwitchItemCmd = new AsyncRelayCommand(switchItem);
        }

        private async Task<bool> switchItem()
        {
            await Task.Delay(1000);
            return true;
        }

        public string? _TextBlockFabricIcons;
        public string? TextBlockFabricIcons { get => _TextBlockFabricIcons; set => SetProperty(ref _TextBlockFabricIcons, value); }


    }
}
