using UnityEngine;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using ZXing.QrCode.Internal;

public class QrCodeWrapper {
	
	private static 
		QRCodeReader CodeReader = new QRCodeReader();

	private static 
		QRCodeWriter CodeWriter = new QRCodeWriter();

	private static
		Color32Renderer ColorRenderer = new Color32Renderer();

	public const int 
		QR_DIMENSION = 256;

	public static string DecodeResult(Result res){
		string ret = null;
		if (res.Text != null) {
			ret = res.Text;
		}
		return ret;
	}
	
	public static Result DecodeImage(Texture2D tex)
	{
		Color32LuminanceSource colLumSource = new Color32LuminanceSource(tex.GetPixels32(), tex.width, tex.height);
		HybridBinarizer hybridBinarizer = new HybridBinarizer(colLumSource);
		BinaryBitmap bitMap = new BinaryBitmap(hybridBinarizer);
		//We reset before we decode for safety;


		CodeReader.reset();

		return CodeReader.decode(bitMap);
	}
	
	public static Texture2D EncodeImage(string data)
	{
		BitMatrix bitMat = CodeWriter.encode(data, BarcodeFormat.QR_CODE, QR_DIMENSION, QR_DIMENSION);
		Color32[] cols = ColorRenderer.Render(bitMat, BarcodeFormat.QR_CODE,"");
		Texture2D tex = new Texture2D(bitMat.Width,bitMat.Height);
		//we set pixels to the new image
		tex.SetPixels32(cols);

		return tex;
	}
}
