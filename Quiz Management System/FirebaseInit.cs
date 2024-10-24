using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;

public static class FirebaseInitializer
{
    private static bool firebaseInitialized = false;

    public static void InitializeFirebase()
    {
        if (!firebaseInitialized)
        {
            // The JSON file is located in a 'Resources' folder in your project
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "smart-learning-system-a2c86-firebase-adminsdk-265q7-82bc2c0986.json");

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(jsonFilePath), // Use relative path
            });
            firebaseInitialized = true; // Mark Firebase as initialized
        }
    }
}
