using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseSender : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SendData("PlayerTwo", 100));
    }

    IEnumerator SendData(string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", score);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/insert_score.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Server response: " + www.downloadHandler.text);
        }
    }
}
