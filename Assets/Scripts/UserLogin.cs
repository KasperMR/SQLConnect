using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class UserLogin : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text feedbackField;
    public void LoginUser()
    {
        StartCoroutine(LoginUserCR());
    }
    private IEnumerator LoginUserCR()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/fetch_user.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            feedbackField.text = www.downloadHandler.text;
            //string json = "{\"players\":" + www.downloadHandler.text + "}";

            //PlayerDataList playerData = JsonUtility.FromJson<PlayerDataList>(json);

            //foreach (PlayerData player in playerData.players)
            //{
            //    Debug.Log($"Player: {player.username}, Score: {player.score}");
            //}
        }
    }
}
