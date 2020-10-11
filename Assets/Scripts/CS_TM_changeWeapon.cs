using UnityEngine;
using TMPro;

public class CS_TM_changeWeapon : MonoBehaviour
{
    GameObject weapon;
    int amount;

    public float damage_gun;
    public float damage_rifle;
    public float damage_knife;

    int wn;
    int[] ammo = new int[4];
    TextMeshProUGUI[] weapontext;

    string[] weaponar;

    GameObject rifle;
    GameObject gun;
    GameObject knife;


    public bool rdown = false;
    public bool rup = false;

    public SS_AmmoBar ammoBar;
    public SS_WeaponImage weaponImage;

    void Start()
    { 
        wn = 0;
        weaponar = new string[3];

        weapontext = GetComponentsInChildren<TextMeshProUGUI>();

        weaponar[0] = "Messer";
        weaponar[1] = "Pistole";
        weaponar[2] = "Sturmgewehr";

        weapontext[0].text = (weaponar[wn]);


        ammo[0] = 6;    //Pistole
        ammo[1] = 30;   //Sturmgewehr

        rifle = GameObject.Find("rifle");
        gun = GameObject.Find("Gun");
        knife = GameObject.Find("knife");
    }

    // Update is called once per frame
    void Update()
    {
        // Weaponchange
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (wn == 0) wn = 2;
            else wn--;
            weapontext[0].text = (weaponar[wn]);
            weapontext[4].text = "";
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (wn == 2) wn = 0;
            else wn++;
            weapontext[0].text = (weaponar[wn]);
            weapontext[4].text = "";
        }


        /* WAFFEN */


        // Messer, Code teilweise Tizian
        if (wn == 0)
        {
            knife.SetActive(true);

            rifle.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
            gun.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);

             Invoke("unequip_rifle", 1);
             Invoke("unequip_gun", 1);

             //GUI - Text
             weapontext[0].text = (weaponar[wn]);
             weapontext[1].text = ("-");
             weapontext[3].text = ("-");

            ammoBar.SetMaxAmmo(1);

            weaponImage.changeWeaponImageMesser();

        }
        // Pistole, Code teilweise Tizian
        if (wn == 1)
        {
            gun.SetActive(true);

            rifle.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
            knife.transform.Translate(new Vector3(0, -1, 0) * (Time.deltaTime));
            Invoke("unequip_rifle", 1);
            Invoke("unequip_knife", 1);

            weapontext[3].text = ("" + gun.GetComponentInChildren<TM_Gun>().GetmaxAmmo());

            //GUI - Text
            weapontext[0].text = (weaponar[wn]);
            weapontext[1].text = ("" + gun.GetComponentInChildren<TM_Gun>().GetMagazin());                                

            //GUI - Ammobar
            ammoBar.SetMaxAmmo(6);
            ammoBar.SetAmmo(gun.GetComponentInChildren<TM_Gun>().GetMagazin());

            weaponImage.changeWeaponImagePistole();
        }

        // Sturmgewehr, Code teilweise Tizian
        if (wn == 2)
        {
            rifle.SetActive(true);
            gun.transform.Translate(new Vector3(0, -1, 0) * (Time.deltaTime));
            knife.transform.Translate(new Vector3(0, -1, 0) * (Time.deltaTime));
            Invoke("unequip_gun", 1);
            Invoke("unequip_knife", 1);

            weapontext[3].text = ("" + rifle.GetComponentInChildren<TM_Gun>().GetmaxAmmo());

            //GUI - Text
            weapontext[0].text = (weaponar[wn]);
            weapontext[1].text = ("" + rifle.GetComponentInChildren<TM_Gun>().GetMagazin());
            //weapontext[3].text = ("8");

            //GUI - Ammo
            ammoBar.SetMaxAmmo(30);
            ammoBar.SetAmmo(rifle.GetComponentInChildren<TM_Gun>().GetMagazin());

            weaponImage.changeWeaponImageSturmgewehr();
        }
    }

    //Code Tizian
    public void reload(int a)
    {
        if(a == 2)
        {
            weapon = gun;
            amount = 6;
        } else if(a == 1)
        {
            weapon = rifle;
            amount = 30;
        } else
        {
            weapon = null;
        }

            while (weapon.GetComponentInChildren<TM_Gun>().GetMagazin() < amount && weapon.GetComponentInChildren<TM_Gun>().GetmaxAmmo() > 0)
            {
                weapon.GetComponentInChildren<TM_Gun>().SetMagazin(weapon.GetComponentInChildren<TM_Gun>().GetMagazin() + 1);
                weapon.GetComponentInChildren<TM_Gun>().SetmaxAmmo(weapon.GetComponentInChildren<TM_Gun>().GetmaxAmmo() - 1);
            }
            weapontext[1].text = ("" + weapon.GetComponentInChildren<TM_Gun>().GetMagazin());
            weapontext[3].text = ("" + weapon.GetComponentInChildren<TM_Gun>().GetmaxAmmo());
    }

    void unequip_rifle()
    {
        rifle.SetActive(false);
    }
    void unequip_gun()
    {
        gun.SetActive(false);
    }
    void unequip_knife()
    {
        knife.SetActive(false);
    }
}