using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTextManager : MonoBehaviour
{
    private bool isRubyWin,draw;
    private void Start()
    {
        //���U���g��ʂ̃e�L�X�g���擾
        GameObject Ruby_Win = GameObject.Find("Ruby_Win");
        GameObject Sapphire_Win = GameObject.Find("Sapphire_Win");
        GameObject Draw = GameObject.Find("Draw");

        //��U��\���ɂ���
        Ruby_Win.SetActive(false);
        Sapphire_Win.SetActive(false);
        Draw.SetActive(false);

        //GameManager����Q�[�����ʂ��擾
        isRubyWin = PlayerPrefs.GetInt("isRubyWin",0) == 1;
        draw = PlayerPrefs.GetInt("draw", 0) == 1;

        //�Q�[�����ʂɉ����ēK�؂ȃ��U���g��\��
        if (isRubyWin && !draw)
        {
            Ruby_Win.SetActive(true);
        }
        else if (!isRubyWin && !draw)
        {
            Sapphire_Win.SetActive(true);
        }
        else
        {
            Draw.SetActive(true);
        }
    }
}
