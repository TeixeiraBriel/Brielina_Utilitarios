﻿using Home.Janelas;
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

namespace BrielinaUtilitarios.Janelas
{
    /// <summary>
    /// Interação lógica para Aplicativos.xam
    /// </summary>
    public partial class Aplicativos : Page
    {
        public Aplicativos()
        {
            InitializeComponent();
        }

        private void AnimesApp(object sender, RoutedEventArgs e)
        {
            Inicio AnimeInicio = new Inicio();

            this.NavigationService.Navigate(AnimeInicio);
        }
    }
}
