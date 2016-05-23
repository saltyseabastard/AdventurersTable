using UnityEngine;
using System.Collections;

public class LoadExternalMap : MonoBehaviour {

	string path;
	string filename = "licktoad.jpg";
	Renderer map;

	// Use this for initialization
	IEnumerator Start () {

		map = GetComponent<Renderer> ();

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
			path +=	"/../Maps/";
		}

		print (path);

		WWW www = new WWW("file://" + path + filename);
		yield return www;

		print (www.bytes.Length);

		Texture2D texture = new Texture2D(2, 2);
		texture.LoadImage(www.bytes);

		map.material.mainTexture = texture;
	}
	

}
