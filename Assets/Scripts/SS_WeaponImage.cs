using UnityEngine;
using UnityEngine.UI;

public class SS_WeaponImage : MonoBehaviour
{
    //Sprites in Inspector festlegen
    public Image image;
    public Sprite messer; 
    public Sprite pistole;
    public Sprite sturmgewehr;
    
    public void changeWeaponImageMesser() 
    {
        image.sprite = messer;
    }
    
    public void changeWeaponImagePistole() 
    {
        image.sprite = pistole;
    }

    public void changeWeaponImageSturmgewehr() 
    {
        image.sprite = sturmgewehr;
    }
}
