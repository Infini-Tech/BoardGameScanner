using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QrCodeScanner : MonoBehaviour {
	private WebcamWrapper 
		myWrapper;

	public GameObject
		myCameraPlane;

	public Toggle
		myStartSearch;

	public Text
		myText;

	private string 
		myContent;

	private bool
		isScanning;

	public virtual void Awake(){
		myWrapper = new WebcamWrapper ();
		myWrapper.StartCamera ();
		myWrapper.Paint (myCameraPlane);
		myWrapper.StopCamera ();
		isScanning = false;
		myStartSearch.isOn = false;
	}
	public virtual void Update(){
		if (!isScanning && myStartSearch.isOn) {
			StartCoroutine(Scan ());
		}
		if (isScanning && !myStartSearch.isOn) {
			isScanning = false;
		}
	}

	public float ScanDelay;

	public IEnumerator Scan(){
		myWrapper.StartCamera();
		isScanning = true;
		bool hasFound = false;
		while (myStartSearch.isOn && !hasFound) {
			myWrapper.PauseCamera ();
			Texture2D scan = myWrapper.GetPicture ();
			myWrapper.StartCamera();

			if(scan != null){
				try{
					ZXing.Result scanResult = QrCodeWrapper.DecodeImage(scan);
					if(scanResult != null){
						string result = scanResult.Text;
						if(result != null){
							myContent = result;
							hasFound = true;
						}
					}
				}
				catch(System.Exception e){
					Debug.LogException(e);
				}
			}
			yield return new WaitForSeconds(ScanDelay);
		}
		myWrapper.StopCamera();
		myStartSearch.isOn = false;
		myText.text = myContent;

		if (IoWrapper.IsUrl (myContent)) {
			yield return StartCoroutine (IoWrapper.AttemptAndroidUrl (myContent));//,myDebug));
		}
		isScanning = false;
		yield return null;
	}
}
