using AppPeliculas.Views;
using Firebase.Auth;
using System.Diagnostics;

namespace AppPeliculas;

public partial class App : Application
{
    public App(FirebaseAuthClient firebaseAuthClient)
    {
        InitializeComponent();

        MainPage = new NavigationPage(new IniciarSesion(firebaseAuthClient));
    }


    protected override Window CreateWindow(IActivationState activationState)
    {
        Window appWindow = base.CreateWindow(activationState);

        appWindow.Created += AlCargarLaApp;
        appWindow.Resumed += AlVolverALaApp;
        appWindow.Deactivated += AlSalirDeLaApp;
        appWindow.Destroying += AlQuitarDeMemoriaLaApp;


        return appWindow;
    }




    private void AlQuitarDeMemoriaLaApp(object sender, EventArgs e)
    {
        Debug.Print("<<<<<<<<<<<<<<<<<<Has quitado de memoria la App>>>>>>>>>>>>>>>");
    }

    private void AlCargarLaApp(object sender, EventArgs e)
    {
        Debug.Print("<<<<<<<<<<<<<<<<<Se esta iniciando la App>>>>>>>>>>>>>>>>");
    }
    private void AlVolverALaApp(object sender, EventArgs e)
    {
        Debug.Print("<<<<<<<<<<<<<<<<Has salido de la App>>>>>>>>>>>>>>");
    }
    private void AlSalirDeLaApp(object sender, EventArgs e)
    {
        Debug.Print("<<<<<<<<<<<<<<Has vuelto a la App>>>>>>>>>>>>>>>>>");
    }

}
