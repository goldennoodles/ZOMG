using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private Character _character;
    
    // Start is called before the first frame update
    void Start()
    {
        _character = new Character
        {
            Weapon = new Weapon()
        };

        Debug.Log(_character.Weapon.GetAllUnlockedWeaponsObjects.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
