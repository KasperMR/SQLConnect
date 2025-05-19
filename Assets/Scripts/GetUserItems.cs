using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class ItemData
{
    public int itemID;
    public string itemName;
    public int amount;
    public string username;
}

[System.Serializable]
public class PlayerItemList
{
    public List<ItemData> items;
}

public class GetUserItems : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text itemList;
    public void GetItems()
    {
        StartCoroutine(GetItemsCR());
    }

    private IEnumerator GetItemsCR()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/fetch_user_bank.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            itemList.text = www.downloadHandler.text;
            string json = "{\"items\":" + www.downloadHandler.text + "}";

            PlayerItemList playerData = JsonUtility.FromJson<PlayerItemList>(json);
            string items = string.Empty;
            foreach (ItemData item in playerData.items)
            {
                //items += $"item: {item.itemID}, amount: {item.amount} \n";
                items += $"user {item.username} has {item.amount} of the item {item.itemName} \n";
                //Debug.Log($"item: {item.itemID}, amount: {item.amount}");
            }
            itemList.text = items;
        }
    }
}
