using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GamePlay : MonoBehaviour
{
    //Карутина
    private IEnumerator coroutine;
    //Анимация
    Animator animator;

    //Определяем пины
    public Text[] p = new Text[4];

    //Определяем инструменты
    public Button Instrument1;
    public Button Instrument2;

    //Результат
    public Text RobotBob;

    void Start()
    {
        //Начальные условия
        p[0].text = p[1].text = p[2].text = p[3].text = Random.Range(2, 9).ToString();

        //Определяем цену инструментов
        for (int i = 0; i < Instrument1.GetComponent<Instrument>().updateP.Length; i++)
        {
            Instrument1.GetComponent<Instrument>().updateP[i] = Random.Range(-2, 3);
            Instrument2.GetComponent<Instrument>().updateP[i] = Random.Range(-2, 3);
        }

        //Выбираем "сложность" число шагов
        int difficulty = 4;

        //Инструмент с которым работаем
        int[] tempUpdateP = new int[4];

        //Проверка на возможность шага
        bool checkStep = true;

        //Создаём смещение
        for (int i = 0; i < difficulty; i++)
        {
            checkStep = true;
            //Определяем случайным образом с каким инструментом работать
            switch (Random.Range(0, 2))
            {
                case 0: tempUpdateP = Instrument1.GetComponent<Instrument>().updateP; break;
                case 1: tempUpdateP = Instrument2.GetComponent<Instrument>().updateP; break;
            }
            for (int j = 0; j < p.Length; j++)
            {
                int temp = int.Parse(p[j].text) - tempUpdateP[j];
                if (temp > 10 || temp < 0)
                    checkStep = false;
            }
            if (checkStep)
            {
                for (int j = 0; j < p.Length; j++)
                {
                    p[j].text = PinResult(p[j].text, -tempUpdateP[j]);
                }
            }
        }
    }

    /// <summary>
    /// Преобразует и проверяет на вхождение в [0..10] значения
    /// </summary>
    /// <param name="p">Текущий пин</param>
    /// <param name="up">Добавочное значение</param>
    /// <returns>Новое значение пина</returns>
    private string PinResult (string p, int up)
    {
        string result = p;
        int resultInt = int.Parse(p) + up;
        if (resultInt <= 10 && resultInt >= 0)
            result = resultInt.ToString();
        return result;
    }

    /// <summary>
    /// Событие по нажатию на инструмент
    /// </summary>
    /// <param name="Instrument">Принимаемый инструмент</param>
    public void ClickInstrument(Instrument Instrument)
    {
        string[] temp = new string[4];
        for (int i = 0; i < 4; i++)
        {
            temp[i] = p[i].text = PinResult(p[i].text, Instrument.updateP[i]);
        }
        Array.Sort(temp);
        if (temp.First() == temp.Last())
        {
            RobotBob.text = "You WIN";
        }
    }

    /// <summary>
    /// Активирует анимацию вращения шестерёнок при нажатии на один из инструментов
    /// </summary>
    public void ActiveAnimation()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("PlayInterfaceAnimation", 1);

        coroutine = WaitAndPrint(1.5f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetInteger("PlayInterfaceAnimation", 0);
        EventSystem.current.SetSelectedGameObject(null);
        //coroutine = WaitAndPrint(2.0f);
        //StartCoroutine(coroutine);
    }

    float Countdown = 15;
    private void Update()
    {
        if (RobotBob.text != "You WIN") 
        {
            if (Countdown > 0)
            {
                Countdown -= Time.deltaTime;
                RobotBob.text = Math.Round(Countdown).ToString();
            }
            if (Countdown <= 0)
                RobotBob.text = "You LOSE";
        }
    }
}
