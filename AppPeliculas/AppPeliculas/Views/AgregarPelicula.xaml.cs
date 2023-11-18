using AppPeliculas.Modelos;
using AppPeliculas.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace AppPeliculas.Views;

public partial class AgregarPelicula : ContentPage   
{
    RepositoryPeliculas repositoryPeliculas = new RepositoryPeliculas();
	public AgregarPelicula()
	{
		InitializeComponent();
	}

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {


        Pelicula nuevaPelicula = new Pelicula()
        {
            nombre = txtNombre.Text,
            genero = txtGenero.Text,
            duracion = Convert.ToInt32(txtDuracion.Text),
            trailer_url = txtTrailerUrl.Text,
            sinopsis = txtSinopsis.Text,
            portada_url = txtPortadaUrl.Text,
        };

        // enviamos por POST el objeto que creamos a la URL de la API
        var agregada = await repositoryPeliculas.AddAsync(nuevaPelicula);

        //retorna el objeto que se agregó en la API ya con si ID generado por la base de datos


        if (agregada) {

            await DisplayAlert("¡ NOTIFICACIÓN !", "Se ha guardado la película", "OK");
            await Navigation.PopAsync();
        }

    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}