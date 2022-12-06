using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeYou.Wava.Extensions
{
  public  class ShellViewModel 
    {
        //IEventAggregator ea;

        //IContainerExtension _container;
        //IRegionManager _regionManager;
        //IRegion _region;
        //Login loginView;
        //MainWindow mainView;
        //private string _title = "Prism Unity Application";
        //public string Title
        //{
        //    get { return _title; }
        //    set { SetProperty(ref _title, value); }
        //}

        //public DelegateCommand LoadDataCommand { get; private set; }
        ////构造函数
        //public ShellViewModel(IContainerExtension Container, IRegionManager regionManager, IEventAggregator eventAggregator)
        //{

        //    #region 接受登陆消息
        //    ea = eventAggregator;
        //    ea.GetEvent<LoginSentEvent>().Subscribe(MessageReceived);
        //    #endregion

        //    _regionManager = regionManager;
        //    _container = Container;
        //    LoadDataCommand = new DelegateCommand(loadData);
        //}

        //private void MessageReceived(bool loginState)
        //{
        //    if (loginState)
        //    {
        //        _region.Deactivate(loginView);

        //        mainView = _container.Resolve<MainWindow>();

        //        _region.Add(mainView);
        //        Title = "现在是程序主窗体";
        //        //do other ...
        //    }
        //}

        //private void loadData()
        //{
        //    _region = _regionManager.Regions["MainRegion"];
        //    loginView = _container.Resolve<Login>();

        //    Title = "用户登陆";
        //    _region.Add(loginView);
        //}
    }

}
