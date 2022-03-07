using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItem : MonoBehaviour
{
    public GameObject img;

    public void AddItem(int membercnt, GameObject item)
    {

        // 이미지Object 새로생성
        //GameObject image = Instantiate(img, new Vector3 (0,0,0), Quaternion.identity);
        //image.transform.parent = this.transform;

        // 기존Object활성화
        GameObject image = this.transform.GetChild(membercnt - 1).gameObject;
        image.SetActive(true);
        image.GetComponent<RectTransform>().anchoredPosition = new Vector2((membercnt - 1) * 100, 0);
        image.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
    }
    public void DeleteItem(int membercnt)
    {
        GameObject image = this.transform.GetChild(membercnt - 1).gameObject;
        image.SetActive(false);
    }
}
