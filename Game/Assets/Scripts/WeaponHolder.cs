using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{ //this doesnt do much yet
    [SerializeField] private Image _melee;
    [SerializeField] private Image _secondary;
    [SerializeField] private Image _primary;
    [SerializeField] private Image assignImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void assignWeapon(WeaponCategory category, string GunName)
    {
        if (category == WeaponCategory.Melee)
        {

            //assign image to melee 
        }
        else if (category == WeaponCategory.Secondary)
        {
            print("Sprites/" + GunName + ".png");
            //assignImage.sprite = Resources.Load<Sprite>("Sprites/" + GunName + ".png");
            assignImage.sprite = Resources.Load<Sprite>("Sprites/Pistol.png") as Sprite;
            _melee.sprite = assignImage.sprite;
            //assige image to seondary
        }
        else if (category == WeaponCategory.Primary) 
        {

            //assing image to primary
        }
    }
}
