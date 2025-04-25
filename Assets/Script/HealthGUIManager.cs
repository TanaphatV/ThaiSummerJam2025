using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGUIManager : MonoBehaviour
{
    [SerializeField] private GameObject heartIconTemplate;
    [SerializeField] private RectTransform heartTransform;
    private List<GameObject> iconList = new();

    public void Init(int maxHeart)
    {
        heartIconTemplate.SetActive(false);
        for (int i = 0; i < maxHeart; i++)
        {
            GameObject newIcon = Instantiate(heartIconTemplate, heartTransform);
            newIcon.SetActive(true);
            iconList.Add(newIcon);
        }
    }

    public void DecreaseHealth()
    {
        if (iconList.Count > 0)
        {
            Destroy(iconList[0].gameObject);
            iconList.RemoveAt(0);
        }
    }

    public void ClearIcon()
    {
        foreach (var item in iconList)
        {
            Destroy(item.gameObject);
        }
    }
}
