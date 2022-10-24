using System;
using UnityEngine;

/// <summary>
/// Float�̊ۂߌ덷�e�X�g
/// �Q�l��Double�^���g�p���Ă���
/// </summary>
public class FloatToInt : MonoBehaviour
{
    [SerializeField] private int testParam;
    [SerializeField] private float testRatio;

    private void Start()
    {
       int result = CalcParameter(testParam, testRatio, 2);
       DebugExtension.Log($"�v�Z���� : {result}");
    }

    /// <summary>
    /// �p�����[�^�[��ratio(%)�㏸�������{�I�Ȍv�Z���s���֐�(�����_�ȉ��؂�̂�)
    /// </summary>
    /// <param name="param">�p�����[�^�[(�����l)</param>
    /// <param name="ratio">�㏸���i�����j</param>
    /// <param name="significant">�L������</param>
    private int CalcParameter(int param, float ratio, int significant = 0)
    {
        int sf = (int)Math.Pow(10, significant);
        DebugExtension.Log($"�L�������{���@�F {sf}");
        ratio *= sf;
        DebugExtension.Log($"int�ϊ��O�@�F {ratio:G17}");
        int r = (int)Math.Round(ratio);
        DebugExtension.Log($"int�ϊ��@�F {r}");
        return param * (sf * 100 + r) / (sf * 100);
    }

    
    /// <summary>
    /// �p�����[�^�[��ratio(��)�㏸�������{�I�Ȍv�Z���s���֐��i�����_�ȉ��؂�̂āj
    /// </summary>
    /// <param name="param">�p�����[�^�[�i�����l�j</param>
    /// <param name="ratio">�㏸�l�i�����j</param>
    /// <returns></returns>
    private int CalcParameter(int param, int ratio)
    {
        //�v�Z�Ŏg�p�����ϐ���S�āiint�^�j�ɂ��낦�邱�Ƃɂ����float�̊ۂߌ덷��h��
        return param * (100 + ratio) / 100;
        //NG �p�^�[�� :
        //return Mathf.FloorToInt(param * (1 + ratio * 0.01f));
        //��L�̎�����Mathf.FloorToInt(float)�̊֐�����float�̊ۂߌ덷���N����\��������
        //����ios����float�̐��x��������A�Ӑ}���Ȃ��v�Z���ʂɂȂ邱�Ƃ�
        // param = 140 , ratio = 60 ����[224]�����������A[223.9999999678]�Ƃ��ɂȂ���[223]�Ɋۂ߂��邱�Ƃ��������B
        //c#�̌���g�p��Aint * float �́@double�ɕϊ�����Čv�Z���A�Ō��float�ŃL���X�g�����
    }

    /// <summary>
    /// �T���v���e�X�g�p
    /// </summary>
    private void Test()
    {
        //�덷���o�Ă��邩�����葁��Editor�ł݂����Ȃ�A�������Ƃ����Double�^�ɕς���Log���o���Ă݂�(��ToString("G17")�ŏo�͂��邱��)
        double calc_doudble = testParam * (1 + testRatio * 0.01f);
        float calc_float = testParam * (1 + testRatio * 0.01f);
        int floor_double = (int)calc_doudble;
        int floor_float = (int)calc_float;

        //float��double�Ȃǂ̌^�����̂܂�String�N���X��Format�ɑ������ƁA�������ԂŐ؂�グ�������s����.
        DebugExtension.Log($"double�̕ϐ��̂܂܏o�� : {calc_doudble}");
        DebugExtension.Log($"float�̕ϐ��̂܂܏o�� : {calc_float}");

        //�������Ԏ��iString interpolation�j��Format���g�p����ۂɁu�F�v�̊ԂɃX�y�[�X���J����Ɛ��������삵�Ȃ�
        DebugExtension.Log($"double�̕ϐ���GeneralFormat�ŏo�� : {calc_doudble:G17}");
        DebugExtension.Log($"float�̕ϐ���GeneralFormat�ŏo�� : {calc_float:G17}");

        DebugExtension.Log($"double�̕ϐ���؂�̂ĂĂ���o�� : {floor_double}");
        DebugExtension.Log($"float�̕ϐ���؂�̂ĂĂ���o�� : {floor_float}");


        // int result = testParam * (100 + testRatio) / 100; //�؂�̂ď����Ɠ���
        // DebugExtension.Log($"100�{�ɂ���Float�v�Z��r�����Ă݂� : {result}");
    }
}