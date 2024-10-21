using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

public static class FirebaseInitializer
{
    private static bool firebaseInitialized = false;

    public static void InitializeFirebase()
    {
        if (!firebaseInitialized)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("F:\\UTS\\Sem-IV\\.NET App Dev\\Quiz_Management_System-master\\Quiz Management System\\smart-learning-system-a2c86-firebase-adminsdk-265q7-82bc2c0986.json"),
            });
            firebaseInitialized = true; // Mark Firebase as initialized
        }
    }
}
