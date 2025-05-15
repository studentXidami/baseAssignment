using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPackage : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject cardPool;

    CardStore cardStore;
    public List<GameObject> cardObjects = new List<GameObject>();
    public PlayerData playerData;
    public bool opend = false;

    // Start is called before the first frame update
    void Start()
    {
        opend = false;
        cardStore = GetComponent<CardStore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetCards()
    {   if (opend) return;
        opend = true;
        ClearPool();
        
        for (int i = 0; i < 3; i++)
        {
            GameObject card = GameObject.Instantiate(cardPrefab,cardPool.transform); //���ɵ�card�ŵ�cardPool��
            card.GetComponent<CardDisplay>().card = cardStore.RandomCard();//�������
            cardObjects.Add(card);
        }
    }

    public void ClearPool() { 
        foreach(var card in cardObjects)
        {
            Destroy(card);
        }
    cardObjects.Clear();
    }


}
