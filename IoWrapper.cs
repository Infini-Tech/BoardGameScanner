using UnityEngine;
using System.Collections;
using System.IO;

public class IoWrapper {

	public IoWrapper(){
		myTex = new Texture2D (QrCodeWrapper.QR_DIMENSION,QrCodeWrapper.QR_DIMENSION);
	}

	private Texture2D 
		myTex;

	public void SaveImage(string fName){
		SaveImage (myTex, fName);
	}

	public static void SaveImage(Texture2D tex, string fName) {
		byte[] bytes = tex.EncodeToPNG();
		System.IO.File.WriteAllBytes(Application.dataPath + "/" + fName, bytes);
	}

	public IEnumerator LoadImage(string fName) {
		WWW www = new WWW ("file://" + Application.dataPath + "/" + fName);
		
		yield return www;
		
		myTex = new Texture2D (QrCodeWrapper.QR_DIMENSION, QrCodeWrapper.QR_DIMENSION);
		myTex.name = fName;
		
		www.LoadImageIntoTexture (myTex);
	}

	public static bool IsUrl(string url){
		return System.Uri.IsWellFormedUriString (url, System.UriKind.RelativeOrAbsolute);
	}
	public static string ConvertToAndroidUrl(string url){
		string androidHeader = "http://";
		if(!url.StartsWith(androidHeader)){
			url = url.Insert(0,androidHeader);
		}
		return url;
	}

	public static IEnumerator AttemptUrl(string url, bool isAndroidUrl = false){
		if (isAndroidUrl) {
			url = ConvertToAndroidUrl(url);
		}

		WWW www = new WWW (url);

		yield return www;

		if (string.IsNullOrEmpty (www.error)) {
			Application.OpenURL (www.url);
		}
		
	}
}
