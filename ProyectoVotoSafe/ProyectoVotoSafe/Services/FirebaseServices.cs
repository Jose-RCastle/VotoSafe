using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Builder.Extensions;

namespace VotoSafe.Services;

public class FirebaseService
{
    private readonly FirestoreDb _firestoreDb;
    private readonly string _projectId;

    public FirebaseService(IConfiguration configuration)
    {
        _projectId = configuration["Firebase:ProjectId"]
            ?? "votosafe";

        if (FirebaseApp.DefaultInstance == null)
        {
           
            var credential = GoogleCredential.FromFile("Config/firebase-credentials.json");
            FirebaseApp.Create(new AppOptions
            {
                Credential = credential,
                ProjectId = _projectId
            });
        }

        _firestoreDb = FirestoreDb.Create(_projectId);
    }

 
    public FirestoreDb GetFirestoreDb() => _firestoreDb;
    public CollectionReference GetCollection(string collectionName) => _firestoreDb.Collection(collectionName);
}