using UnityEngine;
using UnityEngine.SceneManagement;
public class restartLevelOncollide : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GameControlScript.GameIsOver = true;
        if (collision.CompareTag("pill"))
        {
            Destroy(collision.gameObject);
            GameControlScript.GameIsOver = true;
        }
        // LevelRestart();
    }

}
