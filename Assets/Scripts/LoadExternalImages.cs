using UnityEngine;
using System.Collections;

public class LoadExternalImages : SteamVR_InteractableObject
{
    public enum MediaType
    {
        Sheets,
        Maps
    }

    public MediaType mediaType;
    public Renderer displaySurface;
    public ParticleSystem pageTurnEffect;

    string path;
    ArrayList filenames;
    ArrayList textures;
    int currentTexture = 0;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();

		//populate path
		path = Application.dataPath;

		if (Application.platform == RuntimePlatform.OSXPlayer)
		{
			path += "/../../" + mediaType.ToString() + "/";
		} 
		else if (Application.platform == RuntimePlatform.WindowsPlayer)
		{
			path += "/../" + mediaType.ToString() + "/";
		} 
		else
		{
			path +=	"/../" + mediaType.ToString() + "/";
		}

        filenames = new ArrayList();

        foreach (string file in System.IO.Directory.GetFiles(path))
        {
            filenames.Add(file);
        }

        StartCoroutine("LoadPathsToTexture");
		
	}

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);

        ApplyNextTexture(true);
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);

        ApplyNextTexture(true);
    }

    protected IEnumerator LoadPathsToTexture()
    {
        textures = new ArrayList();
        foreach (string filename in filenames)
        {
            Debug.Log(filename);
            WWW www = new WWW("file://" + filename);
            yield return www;

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(www.bytes);

            textures.Add(texture);

        }

        displaySurface.material.mainTexture = (Texture2D) textures[0];
    }

    private void ApplyNextTexture(bool forward)
    {
        if (forward)
        {
            if (currentTexture < textures.Count - 1)
                currentTexture++;
            else
                currentTexture = 0;
        }
        else
        {
            if (currentTexture > 0)
                currentTexture--;
            else
                currentTexture = textures.Count - 1;
        }

        displaySurface.material.mainTexture = (Texture2D) textures[currentTexture];

        if (pageTurnEffect)
            pageTurnEffect.Emit(150);
    }
}
