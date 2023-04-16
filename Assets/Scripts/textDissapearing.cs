using TMPro;
using UnityEngine;
public class textDissapearing : MonoBehaviour
{
    private TextMeshProUGUI helptxt;
    GameObject ChildGameObject1;
    public bool defaultColor=true;
    [SerializeField] Color32 color;
    private void Start()
    {
       ChildGameObject1 = transform.GetChild(0).gameObject;
        helptxt = ChildGameObject1.GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            FadeOut();
    }

    void FadeOut()
    {
        if(defaultColor)
            helptxt.color= new Color32(217, 88, 132, 215);
        else
        {
            helptxt.color = color;
        }
       // Destroy(gameObject,1f);
    }
}
