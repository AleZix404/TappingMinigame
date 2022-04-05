using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapIntermitenteModuleComponent : MonoBehaviour
{
    #region variables
    [SerializeField] PlayTappingMinigameComponent playTappingMinigameComponent;
    [SerializeField] Animator animatorModuleCanvas;
    [SerializeField] RectTransform originalHealth;
    [SerializeField] RectTransform copyHealth;
    [SerializeField] Button btnAction;
    int timer;

    public PlayTappingMinigameComponent PlayTappingMinigameComponent { get => playTappingMinigameComponent; set => playTappingMinigameComponent = value; }
    #endregion
    #region metodos publicos
    // Start is called before the first frame update
    void Start()
    {
        btnAction.gameObject.GetComponent<RectTransform>().sizeDelta = copyHealth.sizeDelta;
        alcance = (int)(originalHealth.sizeDelta.x - 40f);
        //anchord de copia en 0.5 en 4
        OpenModule();
        //StartCoroutine(GoAction());
        AddListenerAction(btnAction, delegate
        {
            StartCoroutine(GoAction());
        });
    }
    //private void Update()
    //{
    //    GoAction();
    //}
    #endregion
    #region metodos publicos
    void AddListenerAction(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
    void OpenModule()
    {
        //instanciar segun dificultad
        //StartCoroutine(Cronometro());
        StartCoroutine(HeartScale());
    }
    public IEnumerator HeartScale()
    {
        while (copyHealth.sizeDelta.x >= 0)
        {
            copyHealth.sizeDelta -= new Vector2(1,0)/2;
            yield return null;
        }
    }
    int alcance;
    public IEnumerator GoAction()
    {
        Debug.Log("copyHealth: " + copyHealth.sizeDelta.x + "alcance: " + alcance);
        if ((int)copyHealth.sizeDelta.x >= alcance && (int)copyHealth.sizeDelta.x <= (originalHealth.sizeDelta.x + 30))
        {
            Debug.Log("EXCATS");
            gameObject.SetActive(false);
        }
        yield return null;
    }
    #endregion
    #region metodos privados
    private IEnumerator Cronometro()
    {
        yield return new WaitForSeconds(1f);
        if (PlayTappingMinigameComponent.ModuleTapTimer == timer)
        {
            if (timer == PlayTappingMinigameComponent.ModuleTapTimer)
            {
                PlayTappingMinigameComponent.HealthHUB(false);
                gameObject.SetActive(false);
            }
            StopCoroutine(Cronometro());
        }
        else
        {
            timer += 1;
            Debug.Log("tp es: " + timer);
            StartCoroutine(Cronometro());
        }
    }
    #endregion
}