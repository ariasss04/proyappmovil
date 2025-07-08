using System;
using Microsoft.Maui.Controls;

namespace PROY10A_DEMO
{
    public partial class MainPage : ContentPage
    {
        private readonly FirebaseAuthService _authService;

        public MainPage()
        {
            InitializeComponent();
            _authService = new FirebaseAuthService();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string email = emailEntry?.Text?.Trim();
            string password = passwordEntry?.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor ingresa el correo y la contraseña.", "OK");
                return;
            }

            string result = await _authService.RegisterUserAsync(email, password);
            await DisplayAlert("Registro", result, "OK");
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailEntry?.Text?.Trim();
            string password = passwordEntry?.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor ingresa el correo y la contraseña.", "OK");
                return;
            }

            // Solo verificamos si el usuario existe (no se autentica con contraseña)
            string result = await _authService.GetUserByEmailAsync(email);
            await DisplayAlert("Inicio de Sesión", result, "OK");
        }
    }
}