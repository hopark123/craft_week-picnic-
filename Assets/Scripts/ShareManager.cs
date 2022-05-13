using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShareManager : MonoBehaviour
{
    [SerializeField]
    GameObject uiCanvus1;
    [SerializeField]
    GameObject uiCanvus2;

    void Start()
    {
    }
    public AudioClip CaptureClip;

    public void CaptureScreenClick()
    {
        NativeGallery.Permission permission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Write);
        uiCanvus1.SetActive(false);
        uiCanvus2.SetActive(false);
        Screen.orientation = ScreenOrientation.Portrait;
        if (permission == NativeGallery.Permission.Denied)
        {
            if (NativeGallery.CanOpenSettings())
            {
                NativeGallery.OpenSettings();
            }
        }
        StartCoroutine(TakeScreenShotRoutine());
    }

    private IEnumerator TakeScreenShotRoutine()
    {
        yield return new WaitForEndOfFrame();

        CaptureScreenForMobile("creftWeek");
    }

    public void CaptureScreenForMobile(string fileName)
    {

            GameObject obj1 = GameObject.Find("Slidesound");
            SoundController.instance.SFXPlay("Capture", CaptureClip);
            Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

            // do something with texture
            string albumName = "BRUNCH";
            //Object.Destroy(texture);
            StartCoroutine(ScreenPortrait(texture));
            NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) =>
            {
                Debug.Log(success);
                Debug.Log(path);
            });
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
            .SetSubject("Subject goes here").SetText("Yard!!!").SetUrl("https://www.instagram.com/yard.kr/")
            //    //.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Object.Destroy(ss);
        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
        uiCanvus1.SetActive(true);
        uiCanvus2.SetActive(true);
    }


}
