using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShareManager : MonoBehaviour
{
    [SerializeField]
    Image blackScreen;

    void Start()
    {
        blackScreen.gameObject.SetActive(false);
    }
    public AudioClip CaptureClip;

    public void CaptureScreenForMobile(string fileName)
    {
        SoundController.instance.SFXPlay("Capture", CaptureClip);
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        // do something with texture
        string albumName = "BRUNCH";
        NativeGallery.Permission permission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Write);
        blackScreen.gameObject.SetActive(true);
        Screen.orientation = ScreenOrientation.Portrait;

        if (permission == NativeGallery.Permission.Denied)
        {
            if (NativeGallery.CanOpenSettings())
            {
                NativeGallery.OpenSettings();
            }
        }
        NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        });
        //Object.Destroy(texture);
        StartCoroutine(ScreenPortrait(texture));
    }

    private IEnumerator ScreenPortrait(Texture2D texture)
    {
        while (Screen.orientation != ScreenOrientation.Portrait)
        {
            yield return null;
        }
        StartCoroutine(TakeScreenshotAndShare(texture));
    }



    private IEnumerator TakeScreenshotAndShare(Texture2D ss)
    {
        yield return new WaitForEndOfFrame();
        //Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        //Destroy(ss);

        new NativeShare().AddFile(filePath)
        //    //.SetSubject("Subject goes here").SetText("Hello world!").SetUrl("https://github.com/yasirkula/UnityNativeShare")
        //    //.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        blackScreen.gameObject.SetActive(false);
        Object.Destroy(ss);
        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }


}
