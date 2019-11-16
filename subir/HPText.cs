using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPText : MonoBehaviour
{
    protected Player myPlayer;
    protected Impacto impacto;

    public Text Text;

    public Text TextKills;

    private Text UsernameText;

    public InputField Username;

    public Button BtnAcept;

    public GameObject ocultar;


    void Awake()
    {
        //Username.text = "Vacio";

        //myPlayer = FindObjectOfType<Controller>().GetComponent<Player>();
        myPlayer = GetComponent<Player>();
        //UsernameText.text = Username.text;
        //impacto = GetComponent<Impacto>();
        //Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //UsernameText.text = Username. .text;
        Debug.Log("VIDAAAAAA");
        Debug.Log(myPlayer.HP + "VIDAAAAAAAAAAAAAAA");
        Text.text = myPlayer.HP.ToString() + " / 100 HP";

        TextKills.text = myPlayer.kills.ToString() + " / 9";


    }

    public void BtnClickAceptar()
    {
        Debug.Log("PRINTE CLICK");

        //ocultar.SetActive(false);
        //UsernameText .text = Username.text;
        //Debug.Log(UsernameText);
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);


    }

}
