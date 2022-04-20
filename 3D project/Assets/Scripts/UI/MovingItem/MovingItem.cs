using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingItem : MonoBehaviour
{
    Vector2 hidePo;
    RectTransform rt;
    Image itemImage;

    bool _isActive;
    public bool isActive { get { return _isActive; } }

    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        hidePo = rt.anchoredPosition;

        itemImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Vector2 po)
    {
        rt.anchoredPosition = po;
        _isActive = true;
    }

    public void Hide()
    {
        rt.anchoredPosition = hidePo;
        _isActive = false;
    }

    public void Moving(Vector2 po)
    {
        rt.anchoredPosition = po;
    }

    public void SetImage(Sprite sp)
    {
        itemImage.sprite = sp;
    }
}
