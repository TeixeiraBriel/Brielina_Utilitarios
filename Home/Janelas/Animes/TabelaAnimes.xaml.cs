using Animes;
using Infraestrutura.Entidades;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Input;
using System;
using System.Windows.Media.Imaging;

namespace Home.Janelas.Animes
{
    /// <summary>
    /// Interação lógica para TabelaAnimes.xam
    /// </summary>
    public partial class TabelaAnimes : Page
    {
        private MainWindow JanelaPrincipal;
        private Controlador _controlador;

        public TabelaAnimes(MainWindow janelaPrincipal = null)
        {
            InitializeComponent();
            _controlador = new Controlador();
            if (janelaPrincipal != null)
            {
                JanelaPrincipal = janelaPrincipal;
            }
            atualizarTabela();
        }

        private void Voltar(object sender, RoutedEventArgs e)
        {
            var destino = new Inicio(JanelaPrincipal);

            this.NavigationService.Navigate(destino);
        }

        public void atualizarTabela()
        {
            List<Anime> Animes = _controlador.buscarAnimes();
            foreach (var anime in Animes)
            {
                StackPanel novaLinha = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 0) };

                StackPanel painelFunc = new StackPanel { Margin = new Thickness(0, 0, 5, 0) , Cursor = Cursors.Hand};
                
                var deleteIcon = (BitmapImage)Application.Current.FindResource("IconDeletar");
                var editIcon = (BitmapImage)Application.Current.FindResource("IconEditar");

                Image ImageEdit = new Image() { Height = 20, Margin = new Thickness(0, 0, 0, 5) , Cursor = Cursors.Hand};
                ImageEdit.Source = editIcon;
                ImageEdit.MouseUp += (s, e) =>
                {
                    var destino = new EditAnime(this, anime.Id);

                    this.NavigationService.Navigate(destino);
                };

                Image ImageDelete = new Image() { Height = 20, Cursor = Cursors.Hand };
                ImageDelete.Source = deleteIcon;
                ImageDelete.MouseUp += (s, e) =>
                {
                    var destino = new DeleteAnime(this, anime.Id);

                    this.NavigationService.Navigate(destino);
                };

                painelFunc.Children.Add(ImageEdit);
                painelFunc.Children.Add(ImageDelete);


                string linkImage = "https://i.pinimg.com/originals/4c/d8/6e/4cd86e99205fc1331836cc28b43de474.jpg";
                if (!string.IsNullOrEmpty(anime.LinkImage))
                {
                    linkImage = anime.LinkImage;
                }
                var img = new ImageSourceConverter().ConvertFromString(linkImage) as ImageSource;

                Image imgAnime = new Image() {Height = 100};
                imgAnime.Source = img;

                StackPanel painelDados = new StackPanel { Margin = new Thickness(5, 0, 0, 0) };
                TextBlock[] campos = new TextBlock[6];
                campos[0] = new TextBlock{ Text = "Nome: " + anime.Nome };
                campos[1] = new TextBlock{ Text = "Episodios: " + anime.Episodios };
                campos[2] = new TextBlock{ Text = "Generos: " + anime.Generos };
                campos[3] = new TextBlock{ Text = "Completo: " + anime.Completo };
                campos[4] = new TextBlock{ Text = "Dia Lançamento: " + anime.DiaLancamento };
                campos[5] = new TextBlock{ Text = "Link: "};

                if (!string.IsNullOrEmpty(anime.Link))
                {
                    Hyperlink link = new Hyperlink { NavigateUri = new System.Uri(anime.Link)};
                    link.Inlines.Add("Acesse");
                    link.RequestNavigate += new RequestNavigateEventHandler(Hyperlink_RequestNavigate);

                    campos[5].Inlines.Add(link);
                }
                else
                {
                    campos[5].Inlines.Add("Vazio;");
                }

                foreach (var campo in campos)
                {
                    painelDados.Children.Add(campo);
                }
                
                novaLinha.Children.Add(painelFunc);
                novaLinha.Children.Add(imgAnime);
                novaLinha.Children.Add(painelDados);

                painelPrincipal.Children.Add(novaLinha);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void cadastrarAnime(object sender, RoutedEventArgs e)
        {
            var destino = new CadastrarAnime(null ,this);

            this.NavigationService.Navigate(destino);
        }
    }
}
