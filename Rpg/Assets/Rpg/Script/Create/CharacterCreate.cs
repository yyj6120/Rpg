using UnityEngine;
using Rpg.Character;
public class CharacterCreate : MonoBehaviour
{
    Character character;
    public Character Create(string character)
    {
        //if(character.Equals("warrior"))
        //{
        //    this.character = new Warroir();
        //}
        //else if(character.Equals("magicion"))
        //{
        //    this.character = new Magicion();
        //}
        //else if (character.Equals("archer"))
        //{
        //    this.character = new Archer();
        //}
        return this.character;
    }
}
