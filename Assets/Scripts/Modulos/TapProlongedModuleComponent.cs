using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapProlongedModuleComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region variables
    [SerializeField] PlayTappingMinigameComponent playTappingMinigameComponent;
    [SerializeField] Animator animatorModuleCanvas;
    [SerializeField] Slider sliderHealth;
    int timer;

    public PlayTappingMinigameComponent PlayTappingMinigameComponent { get => playTappingMinigameComponent; set => playTappingMinigameComponent = value; }
    #endregion
    #region metodos publicos
    // Start is called before the first frame update
    void Start()
    {
        OpenModule();
    }
    #endregion
    #region metodos privados
    private void OpenModule()
    {
        //instanciar segun dificultad
        StartCoroutine(Cronometro());
    }
    private IEnumerator Cronometro()
    {
        yield return new WaitForSeconds(1f);
        if (timer == PlayTappingMinigameComponent.ModuleTapProTimer)
        {
            PlayTappingMinigameComponent.HealthHUB(false);
            gameObject.SetActive(false);
            StopCoroutine(Cronometro());
        }
        else
        {
            timer += 1;
            Debug.Log("tp de M_Prolonge es: " + timer);
            StartCoroutine(Cronometro());
        }
    }
    #endregion
    #region metodos publicos
    bool down, up;
    public void OnPointerDown(PointerEventData eventData)
    {
        up = false;
        down = true;
        StartCoroutine(GoDown());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        down = false;
        up = true;
        StartCoroutine(GoUp());
    }
    public IEnumerator GoDown()
    {
        while (down)
        {
            if (sliderHealth.value < playTappingMinigameComponent.TapProPtsMax)
            {
                sliderHealth.value += (playTappingMinigameComponent.TapProPtsAddition / 50);
            }
            if (sliderHealth.value == PlayTappingMinigameComponent.TapProPtsMax)
            {
                PlayTappingMinigameComponent.HealthHUB(true);
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    public IEnumerator GoUp()
    {
        while (up)
        {
            if (sliderHealth.value > playTappingMinigameComponent.TapProPtsMin)
            {
                sliderHealth.value -= (playTappingMinigameComponent.TapProPtsSustract / 50);
            }
            yield return null;
        }
    }
    #endregion
}
