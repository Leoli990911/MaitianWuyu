using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 碰撞树木的物体，使物体半透明
/// </summary>
public class TriggerItemFader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();

        if (faders.Length > 0)
        {
            foreach (var item in faders)
            {
                item.FadeOut();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();

        if (faders.Length > 0)
        {
            foreach (var item in faders)
            {
                item.FadeIn();
            }
        }
    }
}
