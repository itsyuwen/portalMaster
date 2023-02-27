using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseDataSender : MonoBehaviour
{
    private static string firebaseUrl = "https://game-8da8a-default-rtdb.firebaseio.com/data.json";


    public static IEnumerator SendWinTime(int time, float timeBetweenPresses)
    {
        // Create a JSON object with the data you want to send to Firebase
        string data = "{\"winTime\":" + time + ",\"timeBetweenPresses\":" + timeBetweenPresses + "}";

        // Create a UnityWebRequest and set its URL to the Firebase database URL
        UnityWebRequest request = UnityWebRequest.Put(firebaseUrl, data);
        Debug.Log("sending");
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("sent");
        // Send the request and wait for the response
        yield return request.SendWebRequest();

        // Check the response for any errors
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Data sent to Firebase successfully");
        }
    }
}
