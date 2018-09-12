using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
  // config
  public Dictionary<string, string> env = new Dictionary<string, string>();

  // display
  public Text display;

  void Start()
  {
    env["HOST"] = "localhost:3000";
  }

  public void requestA(){
    StartCoroutine(this.GetText());
  }

  IEnumerator GetText(){
    using (UnityWebRequest www = UnityWebRequest.Get(this.env["HOST"]))
    {
      yield return www.SendWebRequest();

      if (www.isNetworkError || www.isHttpError)
      {
        Debug.Log(www.error);
      }
      else
      {
        // Show results as text
        var text = www.downloadHandler.text;
        Debug.Log(text);

        // Or retrieve results as binary data
        byte[] results = www.downloadHandler.data;

        // display
        this.display.text = text;
      }
    }

  }
}