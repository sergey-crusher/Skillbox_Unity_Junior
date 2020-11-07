using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Instrument : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Определяем пины
    public Text[] p = new Text[4];

    //Новые добавочные значения пинов
    public int[] updateP = new int[4];

    /// <summary>
    /// Форматирование вывода числа
    /// </summary>
    /// <param name="number">Принимаемое число</param>
    /// <returns>Отформатированное число (если =0, то пустая строка)</returns>
    private string AddSign(int number)
    {
        string result;
        if (number > 0)
            result = $"+{number}";
        else if (number == 0)
            result = "";
        else
            result = number.ToString();
        return result;

    }

    /// <summary>
    /// Событие наведения курсора
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        for (int i = 0; i < p.Length; i++)
        {
            p[i].text = AddSign(updateP[i]);
            p[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Событие отвода курсора
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < p.Length; i++)
        {
            p[i].gameObject.SetActive(false);
        }
    }
}
