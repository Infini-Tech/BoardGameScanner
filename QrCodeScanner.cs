using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QrCodeScanner : MonoBehaviour {
	//Remove Debug Info

	private WebcamWrapper 
		myWrapper;

	public GameObject
		myCameraPlane;

	public Toggle
		myStartSearch;

	public Text
		myText,
		myDebug;

	private string 
		myContent;

	private bool
		isScanning;

	public virtual void Awake(){
		myWrapper = new WebcamWrapper ();
		myWrapper.StartCamera ();
		myWrapper.Paint (myCameraPlane);
		isScanning = false;
		myStartSearch.isOn = false;
		myDebug.text = "Awake";


	}
	public virtual void Update(){
		if (!isScanning && myStartSearch.isOn) {
			myDebug.text = "Start Scan";
			StartCoroutine(Scan ());
		}
		if (isScanning && !myStartSearch.isOn) {
			myDebug.text = "Fixing Dirty Scan";
			isScanning = false;
			myDebug.text = "Dirty Camera Restarted";
			myWrapper.StartCamera();
		}
	}

	public float ScanDelay;

	public IEnumerator Scan(){
		myDebug.text = "Scan Began";
		isScanning = true;
		bool hasFound = false;
		while (myStartSearch.isOn && !hasFound) {
			myDebug.text = "Scan Start Cycle";
			myDebug.text = "Pause Camera";
			myWrapper.PauseCamera ();
			myDebug.text = "Scan";
			Texture2D scan = myWrapper.GetPicture ();
			myDebug.text = "Camera Restarted";
			myWrapper.StartCamera();
			myDebug.text = "Check Scan";

			if(scan != null){
				myDebug.text = "Analyze Scan";
				try{
					ZXing.Result scanResult = QrCodeWrapper.DecodeImage(scan);
					myText.text = "Scan Not Null";
					if(scanResult != null){
						myText.text = "Scan Result Not Null";
						string result = scanResult.Text;
						if(result != null){
							myText.text = "Result Not Null";
							myContent = result;
							hasFound = true;
						}
					}
				}
				catch(System.Exception e){
					myDebug.text = e.Message;
					//myText.text = e.Message;
				}
			}
			yield return new WaitForSeconds(ScanDelay);
		}
		myDebug.text = "Scan Ended";
		myWrapper.StartCamera();
		myDebug.text = "Camera Restarted";
		myStartSearch.isOn = false;
		myDebug.text = "Disabling Search Button";
		myText.text = myContent;
		myDebug.text = "Disabling Scan";
		isScanning = false;
		yield return null;
	}

}
