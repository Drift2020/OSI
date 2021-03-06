﻿using Chat.Code;
using Chat.View;
using Chat.View_model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


using System.Data.Entity;
namespace Chat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            PresentationTraceSources.DataBindingSource.Listeners.Add(new BindingErrorTraceListener());
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {


            ApplicationContext db =new ApplicationContext();
            db.Users.Load();
                bool work = true;

                Login view = new Login();



            Viwe_Model_Login viewModel = new Viwe_Model_Login(db);
                view.DataContext = viewModel;

            if (viewModel._Visibility_off == null)
                viewModel._Visibility_off = new Action(view.Visibility_off);


            if (viewModel._Visibility_on == null)
                viewModel._Visibility_on = new Action(view.Visibility_on);

            if (viewModel._NO == null)
                viewModel._NO = new Action(view.No);

            if (viewModel._OK == null)
                viewModel._OK = new Action(view.Ok);

            if (viewModel._NONE_USER == null)
                viewModel._NONE_USER = new Action(view.None_user);


         //   view.Closing += viewModel.OnWindowClosing;

            //do
            //{
            view.ShowDialog();
            

        }

      
    }


  
}
