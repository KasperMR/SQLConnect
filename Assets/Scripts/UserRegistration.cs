using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class UserRegistration : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text feedbackField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CreateUser()
    {
        StartCoroutine(CreateUserCR());
    }
    IEnumerator CreateUserCR()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/user_registration.php", form);

        yield return www.SendWebRequest();

        
        if (www.result != UnityWebRequest.Result.Success)
        {
            feedbackField.text = www.error.ToString();
            Debug.Log("Error: " + www.error);
        }
        else
        {
            //if ()
            //feedbackField.text = www.downloadHandler.text.ToString();
            feedbackField.text = www.downloadHandler.text;
            Debug.Log("Server response: " + www.downloadHandler.text);
        }
    }
}
