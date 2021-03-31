using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : MonoBehaviour
{
    bool actuation;
    [SerializeField] Sprite onImage;
    [SerializeField] Sprite offImage;
    SpriteRenderer spriteRenderer;
    EnemyController enemyController;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Actuation()
    {
        if(actuation)
        {
            spriteRenderer.sprite = offImage;
            actuation = false;
            if (enemyController)
            {
                enemyController.FreezeOff();
                enemyController = null;
            }
        }
        else
        {
            spriteRenderer.sprite = onImage;
            actuation = true;
            if (enemyController)
            {
                enemyController.Freeze();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy")
        {
            enemyController = collision.gameObject.GetComponent<EnemyColliderController>().enemyController;
            if (actuation)
            {
                enemyController.Freeze();
            }
        }
    }
}
