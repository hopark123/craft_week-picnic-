using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private GameObject items;
    [SerializeField]
    private Sprite itemDestroyed;

    private Image[] slots;

    private Color on = Color.white;
    private Color init = new Color(0.3f, 0.3f, 0.3f, 0.3f);

    void Awake()
    {
        slots = new Image[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            slots[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < items.transform.childCount; ++i)
        {
            slots[i].sprite = items.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
            slots[i].color = init;
        }
    }

    public void GetItem(int itemIndex)
    {
        slots[itemIndex].color = on;
    }

    public void LostItem()
    {
        for (int i = GameManager.itemNum - 1; i >= 0; --i)
        {
            if (slots[i].color == on && slots[i].sprite != itemDestroyed)
            {
                //slots[i].color = off;
                slots[i].sprite = itemDestroyed;
                GameManager.LostItem(i);
                break;
            }
        }
    }
}
