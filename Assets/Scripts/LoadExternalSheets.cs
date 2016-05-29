using UnityEngine;
using System.Collections;

public class LoadExternalSheets : MonoBehaviour {

	string path;
	string filename = "duane.jpg";
	Renderer sheet;

	// Use this for initialization
	IEnumerator Start () {

		sheet = GetComponent<Renderer> ();

		//populate path
		path = Application.dataPath;

		if (Application.platform == RuntimePlatform.OSXPlayer)
		{
			path += "/../../";
		} 
		else if (Application.platform == RuntimePlatform.WindowsPlayer)
		{
			path += "/../";
		} 
		else
		{
			path +=	"/../Sheets/";
		}

		WWW www = new WWW("file://" + path + filename);
		yield return www;

		Texture2D texture = new Texture2D(2, 2);
		texture.LoadImage(www.bytes);

		sheet.material.mainTexture = texture;
	}
	

}
