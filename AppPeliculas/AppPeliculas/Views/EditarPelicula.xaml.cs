using AppPeliculas.Modelos;
using AppPeliculas.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace AppPeliculas.Views;

public partial class EditarPelicula : ContentPage
{
    RepositoryPeliculas repositoryPeliculas = new RepositoryPeliculas();
    public Pelicula PeliculaSeleccionada { get; }

    public EditarPelicula()
	{
		InitializeComponent();
	}

    public EditarPelicula(Pelicula peliculaSeleccionada)
    {
        InitializeComponent();
        PeliculaSeleccionada = peliculaSeleccionada;
        CargarDatosEnPantalla();
    }

    private void CargarDatosEnPantalla()
    {
        txtNombre.Text = PeliculaSeleccionada.nombre;
        txtGenero.Text = PeliculaSeleccionada.genero;
        txtDuracion.Text = PeliculaSeleccionada.duracion.ToString();
        txtTrailerUrl.Text = PeliculaSeleccionada.trailer_url;
        txtSinopsis.Text = PeliculaSeleccionada.sinopsis;
        txtPortadaUrl.Text = PeliculaSeleccionada.portada_url;

    }

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
       

      

        Pelicula peliculaEdidata = new Pelicula()
        {
            _id = PeliculaSeleccionada._id,
            nombre = txtNombre.Text,
            genero = txtGenero.Text,
            duracion = Convert.ToInt32(txtDuracion.Text),
            trailer_url = txtTrailerUrl.Text,
            sinopsis = txtSinopsis.Text,
            portada_url = txtPortadaUrl.Text,
        };

        // enviamos por PUT el objeto que creamos a la URL de la API
        var guardada = await repositoryPeliculas.UpdateAsync(peliculaEdidata);

        //retorna el objeto que se agreg� en la API ya con si ID generado por la base de datos

        

        if (guardada)
        {

            await DisplayAlert("� NOTIFICACI�N !", "Pel�cula almacenada", "OK");
            await Navigation.PopAsync();
            
            
        }
    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
         await Navigation.PopAsync();
    }
}