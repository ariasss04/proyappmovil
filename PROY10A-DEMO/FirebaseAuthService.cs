using System;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace PROY10A_DEMO
{
    public class FirebaseAuthService
    {
        private bool IsFirebaseReady => FirebaseApp.DefaultInstance != null;

        public async Task<string> RegisterUserAsync(string email, string password)
        {
            if (!IsFirebaseReady)
                return "❌ Firebase no está inicializado. Revisa tu configuración en MauiProgram.cs.";

            try
            {
                var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs
                {
                    Email = email,
                    Password = password
                });

                return $"✅ Usuario registrado con UID: {userRecord.Uid}";
            }
            catch (FirebaseAuthException ex)
            {
                return $"❌ Error de Firebase: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"❌ Error general: {ex.Message}";
            }
        }

        public async Task<string> GetUserByEmailAsync(string email)
        {
            if (!IsFirebaseReady)
                return "❌ Firebase no está inicializado. Revisa tu configuración en MauiProgram.cs.";

            try
            {
                var user = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
                return $"🔍 Usuario encontrado. UID: {user.Uid}";
            }
            catch (FirebaseAuthException ex)
            {
                return $"❌ Usuario no encontrado: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"❌ Error general: {ex.Message}";
            }
        }
    }
}