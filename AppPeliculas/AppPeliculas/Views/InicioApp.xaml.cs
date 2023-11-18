using AppPeliculas.Modelos;
using AppPeliculas.Repositories;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace AppPeliculas.Views;

public partial class InicioApp : ContentPage
{
	public ObservableCollection<Pelicula> Peliculas { get; set; }
    public Pelicula PeliculaSeleccionada { get; private set; }
	RepositoryPeliculas repositoryPeliculas = new RepositoryPeliculas();

    public InicioApp()
	{
		InitializeComponent();
	
		PeliculasCollectionView.SelectionChanged += SeleccionarPelicula;
		Peliculas = new ObservableCollection<Pelicula>();
		
	}

    public void SeleccionarPelicula(object sender, SelectionChangedEventArgs e)
    {
       if (PeliculasCollectionView != null)
		{
			PeliculaSeleccionada = (Pelicula)PeliculasCollectionView.SelectedItem;
		}
    }
    // trae las peliculas y las asigna 
    public async void GetAllPeliculas(object sender, EventArgs e)
	{
		

		 Peliculas =  await repositoryPeliculas.GetAllAsync();

		PeliculasCollectionView.ItemsSource = Peliculas;
		
	}
	
    public void SeleccionarPeliculaEnCollectionWiew()
    {
 // recorremos las peliculas hasta encontrar la que coincide con la pelicula seleccionada, al encontrarla la utilizaremos para indicar que es el SelectedItem del collectionWiew e interrumpiremos la iteración.


       foreach(var pelicula in Peliculas)
		{
			if(pelicula._id == PeliculaSeleccionada._id)
			{
				PeliculasCollectionView.SelectedItem = pelicula;
				break;
			}
		}
    }

    //método! inicio de la página.. donde hacemos que carguen las peliculas con getAllPeliculas..
    protected override void OnAppearing()
    {
		//Debug.Print("<<<<<<<<<<<<<<<<Se ha cargado la pantalla que muestra la lista de las películas>>>>>>>>>>>>>>");
		NetworkAccess conexionInternet = Connectivity.Current.NetworkAccess;
		if(conexionInternet == NetworkAccess.Internet)
		{
            GetAllPeliculas(this, EventArgs.Empty);
        }
		

    }

	protected override bool OnBackButtonPressed()
	{
		Debug.Print("<<<<<<<<<<<<<<<<Se ha pulsado el botón de atrás>>>>>>>>>>>>>>");
		return false;
	}

    protected override void OnDisappearing()
    {
		Debug.Print("<<<<<<<<<<Se ha cerrado la ventana de la lista de las películas>>>>>>>>>>>");
    }

	public async void AbrirPaginaInicio(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new PaginaDeInicio());
	}

    public async void AbrirPaginaControlesMaui(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ControlesMaui());
    }

    public async void AgregarPeliculaBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarPelicula());
    }

    private async void EliminarPeliculaBtn_Clicked(object sender, EventArgs e)
    {
		if (PeliculaSeleccionada != null)
		{
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Está seguro que desea eliminar la película:{PeliculaSeleccionada.nombre}?","SI" ,"NO");
			if (respuesta)
			{


				var eliminado = await repositoryPeliculas.RemoveAsync(PeliculaSeleccionada._id);
				if (eliminado)
				{
                    await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Se ha eliminado la película:{PeliculaSeleccionada.nombre}", "OK");
					GetAllPeliculas(this, EventArgs.Empty);
                }
				
            }
        }
		else
		{
			await Application.Current.MainPage.DisplayAlert("Eliminar", "ERROR: Debe seleccionar la película que desea borrar", "OK");
		}
    }


    private async void EditarPeliculaBtn_Clicked(object sender, EventArgs e)
    {
		if (PeliculaSeleccionada!= null)
		{
            await Navigation.PushAsync(new EditarPelicula(PeliculaSeleccionada));
        }
		else
		{
			await Application.Current.MainPage.DisplayAlert("Editar", "ERROR: Debe seleccionar la película que quiere editar", "OK");
		}
    }

    private async  void VerTrailerBtn_Clicked(object sender, EventArgs e)
    {
		if(PeliculaSeleccionada != null)
		{
			 await Navigation.PushAsync(new TrailerPelicula(PeliculaSeleccionada.trailer_url));
		}
		else
		{
            await Application.Current.MainPage.DisplayAlert("Ver trailer", "ERROR: Debe seleccionar la película que desea ver", "OK");
        }
    }
}