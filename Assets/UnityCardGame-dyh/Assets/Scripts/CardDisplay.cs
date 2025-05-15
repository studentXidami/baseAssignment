using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Text nameText;
    public Text attackText;
    public Text healthText;
    public Text sacrificeText;
    public Image background;

    public Card card;


    // Start is called before the first frame update
    void Start()
    {
        ShowCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCard()
    {
        nameText.text=card.cardName;

        if(card is MonsterCard)
        {
            var monster =  card as MonsterCard;
            attackText.text = monster.attack.ToString();
            healthText.text = monster.health.ToString();
            sacrificeText.text = monster .sacrifice.ToString();

            // Text.gameObject.SetActive(false)-- hide
        }
        //�������ӡ����ʾ
    }
}
