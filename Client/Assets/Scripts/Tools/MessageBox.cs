using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessageBox : BaseWindow
{
    private Text _text;

    private Button _btnOK;
    private Button _btnCancel;
    private Button _btnOKCenter;

    private Action _actionOK;
    private Action _actionCancel;

    public static void Show(string text, Action actionOK = null, Action actionCancel = null)
    {
        MessageBox box = UIManager.instance.Open<MessageBox>();
        box.Init(text, actionOK, actionCancel);
    }

    public void Init(string text, Action actionOK = null, Action actionCancel = null)
    {
        _text = _transform.Find("Text").GetComponent<Text>();
        _btnOK = _transform.Find("BtnOK").GetComponent<Button>();
        _btnCancel = _transform.Find("BtnCancel").GetComponent<Button>();
        _btnOKCenter = _transform.Find("BtnOKCenter").GetComponent<Button>();

        _text.text = text;
        _actionOK = actionOK;
        _actionCancel = actionCancel;

        if (_actionOK == null && _actionCancel == null)
        {
            _btnOKCenter.gameObject.SetActive(true);
            UIEventListener.Get(_btnOKCenter.gameObject).onPointerClick = OnOKCenter;
        }

        if (_actionOK != null)
        {
            _btnOK.gameObject.SetActive(true);
            UIEventListener.Get(_btnOK.gameObject).onPointerClick = OnOK;
        }


        if (_actionCancel != null)
        {
            _btnCancel.gameObject.SetActive(false);
            UIEventListener.Get(_btnCancel.gameObject).onPointerClick = OnCancel;
        }

    }

    private void OnOKCenter(PointerEventData ped)
    {
        UIManager.instance.Close<MessageBox>();
    }

    private void OnOK(PointerEventData ped)
    {
        if (_actionOK != null)
        {
            _actionOK();
        }

        UIManager.instance.Close<MessageBox>();
    }

    private void OnCancel(PointerEventData ped)
    {
        if (_actionCancel != null)
        {
            _actionCancel();
        }

        UIManager.instance.Close<MessageBox>();
    }
}