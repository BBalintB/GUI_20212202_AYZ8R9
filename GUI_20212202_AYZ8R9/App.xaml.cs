using GUI_20212202_AYZ8R9.Logic;
using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.Services.MenuOptionsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_AYZ8R9
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    .AddSingleton<IMenuLogic, MenuLogic>()
                    .AddSingleton<INewGameViaWindow, NewGameViaWindow>()
                    .AddSingleton<INewGameLogic, NewGameLogic>()
                    .AddSingleton<IFightWindowLogic,FightWindowLogic>()
                    .AddSingleton<IInventoryLogic,InventoryLogic>()
                    .AddSingleton<IGameModel, MapLogic>()
                    .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
                    .BuildServiceProvider()
                );
        }
    }
}
