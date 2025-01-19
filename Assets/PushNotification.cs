using System.Threading.Tasks;
using Firebase;
using Firebase.Messaging;
using UnityEngine;
using UnityEngine.Android;

public class PushNotification : MonoBehaviour
{
    private bool firebaseInitialized = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private async void Start()
    {
        await InitializeFirebase();
        
        if (firebaseInitialized)
        {
            FirebaseMessaging.TokenReceived += OnTokenReceived;
            FirebaseMessaging.MessageReceived += OnMessageReceived;
            RequestNotificationPermission();
        }
    }

    // Initialize Firebase dependencies
    private async Task InitializeFirebase()
    {
        Debug.Log("Checking Firebase dependencies...");
        var dependencyStatus = await FirebaseApp.CheckDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            firebaseInitialized = true;
            Debug.Log("Firebase is ready.");
        }
        else
        {
            Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            firebaseInitialized = false;
        }
    }

    // Request notification permission for Android
    public void RequestNotificationPermission()
    {
        Debug.Log("Checking for notification permission...");

        string postNotificationsPermission = "android.permission.POST_NOTIFICATIONS";

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Permission.HasUserAuthorizedPermission(postNotificationsPermission))
            {
                Debug.Log("Notification permission already granted.");
            }
            else
            {
                Debug.Log("Requesting notification permission...");
                Permission.RequestUserPermission(postNotificationsPermission);
            }
        }
        else
        {
            Debug.LogWarning("Notification permission request not applicable on this platform.");
        }
    }

    // Called when Firebase receives a new token
    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    // Called when Firebase receives a new message
    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }

    // Unsubscribe from Firebase events when object is destroyed
    private void OnDestroy()
    {
        FirebaseMessaging.TokenReceived -= OnTokenReceived;
        FirebaseMessaging.MessageReceived -= OnMessageReceived;
    }
}
