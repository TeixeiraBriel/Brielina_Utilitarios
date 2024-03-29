﻿using BrielinaUtilitarios.Janelas.Contador;
using BrielinaUtilitarios.Util;
using Home;
using Home.Janelas;
using Infraestrutura.Entidades;
using Infraestrutura.Enumeradores;
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
        MainWindow janelaPrincipal;
        public Aplicativos(Usuario userLogado, MainWindow JanelaPrincipal)
        {
            InitializeComponent();
            janelaPrincipal = JanelaPrincipal;

            foreach (var funcionalidadeUsuario in userLogado.FuncionalidadesPermitidas.Split(';'))
            {
                switch (funcionalidadeUsuario)
                {
                    case "0":
                        criarFuncionalidade(Funcionalidades.Animes);
                        break;
                    case "1":
                        criarFuncionalidade(Funcionalidades.Financeiro);
                        break;
                    case "2":
                        criarFuncionalidade(Funcionalidades.Steam);
                        break;
                    case "3":
                        criarFuncionalidade(Funcionalidades.Contador);
                        break;
                }
            }

        }

        public void criarFuncionalidade(Funcionalidades funcionalidade)
        {
            Button novaFuncionalidade = new Button();
            novaFuncionalidade.Content = funcionalidade.ToString();
            novaFuncionalidade.Click += (s, e) => NavegaJanela(funcionalidade);
            novaFuncionalidade.HorizontalAlignment = HorizontalAlignment.Center;
            novaFuncionalidade.VerticalAlignment = VerticalAlignment.Center;
            novaFuncionalidade.Margin = new Thickness(0, 15, 0, 0);

            Style estilo = new Style(typeof(Button));
            estilo.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.White));
            estilo.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
            estilo.Setters.Add(new Setter(Button.FontSizeProperty, 20.0));
            estilo.Setters.Add(new Setter(Button.CursorProperty, Cursors.Hand));

            Trigger gatilho = new Trigger();
            gatilho.Property = IsMouseOverProperty;
            gatilho.Value = true;
            gatilho.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Black));
            gatilho.Setters.Add(new Setter(Button.ForegroundProperty, new SolidColorBrush(Color.FromRgb(54, 69, 93))));
            
            estilo.Triggers.Add(gatilho);

            ControlTemplate ct = new ControlTemplate(typeof(Button));
            FrameworkElementFactory borda = new FrameworkElementFactory(typeof(Border));
            borda.SetValue(Border.WidthProperty , 120.0);
            borda.SetValue(Border.HeightProperty, 100.0);
            borda.SetValue(Border.CornerRadiusProperty, new CornerRadius(12));
            borda.SetValue(Border.BackgroundProperty, new SolidColorBrush(Color.FromRgb(238,238,238)));

            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
            contentPresenter.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);

            ct.VisualTree = borda;
            borda.AppendChild(contentPresenter);

            novaFuncionalidade.Style = estilo;
            novaFuncionalidade.Template = ct;

            PainelAplicativos.Children.Add(novaFuncionalidade);
        }

        public void NavegaJanela(Funcionalidades funcionalidade)
        {
            if (funcionalidade == Funcionalidades.Animes)
            {
                this.NavigationService.Navigate(new Home.Janelas.Inicio());
            }
            else if (funcionalidade == Funcionalidades.Financeiro)
            {
                this.NavigationService.Navigate(new Financeiro.inicio());
            }
            else if (funcionalidade == Funcionalidades.Steam)
            {
                this.NavigationService.Navigate(new SteamJogos.Inicio());
            }
            else if (funcionalidade == Funcionalidades.Contador)
            {
                janelaPrincipal.WindowState = WindowState.Minimized;
                DispatcherUtil.Dispatcher(() =>
                {
                    JanelaContador.Inicializa(janelaPrincipal);
                    JanelaContador.Mostrar();
                    JanelaContador.Focar();
                });
            }
        }
    }
}
