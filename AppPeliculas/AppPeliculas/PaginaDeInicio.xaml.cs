namespace AppPeliculas;

public partial class PaginaDeInicio : ContentPage
{
    public PaginaDeInicio()
    {
        InitializeComponent();
        // por medio de c�digo manejamos el evento click(Clicked)
        DocumentalesBtn.Clicked += MostrarDocumentales;
        SeriesBtn.Clicked += (s, a) =>
        {
            ContenidoLbl.Text = "Series";
        };

    }
    public void MostrarPeliculas(object sender, EventArgs e)
    {
        //await DisplayAlert("Pel�culas", "Mostrando la lista de pel�culas", "OK");
        ContenidoLbl.Text = "Peliculas";
    }



    public void MostrarDocumentales(object sender, EventArgs e)
    {
        ContenidoLbl.Text = "Documentales";
    }

    private async void CerrarLaVentana(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}