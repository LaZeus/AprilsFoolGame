using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : DropZone {

    // add stats here
    // and more stuff

    [Header("Enemy Health")]
    [Tooltip("Enemy's max health points")]
    public int health = 60;

    [SerializeField]
    private TextMeshProUGUI hpText;  

	void Awake ()
    {
        Initialization();
    }

    protected void Initialization() // gonna be used from the inherited class
    {
        OnDropActions = CardEffect; // when a card is placed on enemy
        hpText.text = health.ToString();
    }

    protected void CardEffect(Card card) 
    {
        StartCoroutine(ActivateCardEffect(card));
    }
	
    // make this a delegate to add some cool onDeath effects
    private void HealthControl()
    {
        if(health <= 0)
        {
            Debug.Log("Enemy Died");
            StartCoroutine(DestroyEnemy());
        }
    }

    // comment this.
    protected IEnumerator ActivateCardEffect(Card card)
    {
        if(card.CardAttack != null) // fool proof
            card.CardAttack(this);  // perform attack

        HealthControl(); // check if enemy's health is above 0

        hpText.text = health.ToString(); // update visual for health
        yield return null;

        card.ToDiscardPile(); // send the just activated card to discard pile
    }

    IEnumerator DestroyEnemy()
    {
        yield return null;
        Destroy(transform.parent.gameObject); 
        //maybe add a placeholder instead
    }
}
