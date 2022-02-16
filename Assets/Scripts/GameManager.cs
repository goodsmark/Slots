using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("То, сколько будет прибавлятся на (+) и (-)")]
    [SerializeField] int _userPoint;
    [Header("Множитель при показе 2-х и 3-х одинаковых итемок")]
    [SerializeField] byte _multiplyX2;
    [SerializeField] byte _multiplyX3;
    [Header("Длительность прокрутки")]
    [SerializeField] byte _minTimeRotate;
    [SerializeField] byte _maxTimeRotate;
    [Header("Задержка между сменой спрайта")]
    [SerializeField] float _deleyTime;

    [SerializeField] Image[] _imagesSlots;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] Text _slotText;
    [SerializeField] InputField _betInputText;


    [SerializeField] PluginTest _pluginTest;


    float _score;
    int _betScore;

    private void Start()
    {
        _betScore = 0;
        _betInputText.text = _betScore.ToString();

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        else
        {
            _pluginTest.OpenWebViewTapped();
        }


    }


    private void Update()
    {
        //Лучше использовать Action
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
    }
    public void DoSpin()
    {
        if (_betInputText.text == null || _betInputText.text == "")
        {
            _betInputText.text = _betScore.ToString();
        }
        StartCoroutine(StartSpin());

    }

    IEnumerator StartSpin()
    {
        int parsedValueImputText = Int32.Parse(_betInputText.text);
        int[] randNum = new int[3];
        //randNum[0] = Randomize();
        //randNum[1] = Randomize();
        //randNum[2] = Randomize();
        for (int i = 0; i < _imagesSlots.Length; i++)
        {
            int random = UnityEngine.Random.Range(_minTimeRotate, _maxTimeRotate);
            for (int j = 0; j < random; j++)
            {
                randNum[i] = Randomize();
                yield return new WaitForSeconds(_deleyTime);
                _imagesSlots[i].GetComponent<Image>().sprite = _sprites[randNum[i]];

            }

        }
        if (randNum[0] == randNum[1] || randNum[0] == randNum[2] || randNum[0] == randNum[2] || randNum[1] == randNum[2])
        {
            _score += _multiplyX2 * parsedValueImputText;
        }
        else if (randNum[0] == randNum[1] && randNum[0] == randNum[2] && randNum[0] == randNum[2])
        {
            _score += _multiplyX3 * parsedValueImputText;
        }

        _slotText.text = _score.ToString();
        yield break;
    }

    int Randomize()
    {
        int numer = UnityEngine.Random.Range(0, _sprites.Length);
        return numer;
    }


    public void AddPoint()
    {
        _betScore += _userPoint;
        _betInputText.text = _betScore.ToString();
    }
    public void SubtractPoint()
    {

        _betScore -= _userPoint;


        if (_betScore <= 0)
        {
            _betScore = 0;
        }
        _betInputText.text = _betScore.ToString();

    }
    public void OnEditEnd()
    {

        _betScore += Int32.Parse(_betInputText.text);
        _betInputText.text = _betScore.ToString();
    }
}
