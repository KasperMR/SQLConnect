using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class PlayerData
{
    public string username;
    public int score;
}

[System.Serializable]
public class PlayerDataList
{
    public List<PlayerData> players;
}

public class DatabaseFetcher : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/sqlconnect/fetch_scores.php");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string json = "{\"players\":" + www.downloadHandler.text + "}";

            PlayerDataList playerData = JsonUtility.FromJson<PlayerDataList>(json);

            foreach (PlayerData player in playerData.players)
            {
                Debug.Log($"Player: {player.username}, Score: {player.score}");
            }
        }
    }
}
