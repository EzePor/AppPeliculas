namespace AppPeliculas.Views;

public partial class TrailerPelicula : ContentPage
{


    public TrailerPelicula(string trailer_url)
    {
        InitializeComponent();

        ReproductorWebWiew.Source = trailer_url;
        
    }
}