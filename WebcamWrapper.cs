using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WebcamWrapper {
	private WebCamTexture 
		myCamera;

	public WebcamWrapper(){
		myCamera = new WebCamTexture ();
	}

	private bool
		isPlaying,
		isPaused;

	public void StartCamera(){
		if (!isPlaying || isPaused) {
			myCamera.Play ();
			isPlaying = true;
			isPaused = false;
		}
	}
	public void PauseCamera(){
		if (isPlaying && !isPaused) {
			myCamera.Pause ();
			isPaused = true;
		}
	}
	public void StopCamera(){
		if (isPlaying) {
			myCamera.Stop ();
			isPlaying = false;
		}
	}

	public WebCamTexture GetCamera(){
		return myCamera;
	}
	/*
	private Texture2D
		myTexture;
	*/
	public Texture2D GetPicture(){
		Texture2D myTexture = new Texture2D (myCamera.width, myCamera.height);
		myTexture.SetPixels32 (myCamera.GetPixels32 ());
		return myTexture;
		//I tried to optimize this, but it seems to hang, fix this later
		/*
		if (myTexture == null) {
			myTexture = new Texture2D (myCamera.width, myCamera.height);
		} else {
			myTexture.width = myCamera.width;
			myTexture.height = myCamera.height;
		}

		myTexture.SetPixels32(myCamera.GetPixels32 ());
		return myTexture;*/
	}
	
	public void Paint(GameObject go){
		if(go == null){
			return;
		}
		Paint(go.GetComponent<MeshRenderer> ());
		Paint(go.GetComponent<RawImage> ());
	}
	public void Paint(MeshRenderer mr){
		if(mr == null){
			return;
		}
		mr.material.mainTexture = GetCamera();
	}
	public void Paint(RawImage image){
		if(image == null){
			return;
		}
		image.texture = GetCamera();
	}
}
