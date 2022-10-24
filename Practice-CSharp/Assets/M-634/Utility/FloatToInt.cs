using System;
using UnityEngine;

/// <summary>
/// Floatの丸め誤差テスト
/// 参考にDouble型を使用している
/// </summary>
public class FloatToInt : MonoBehaviour
{
    [SerializeField] private int testParam;
    [SerializeField] private float testRatio;

    private void Start()
    {
       int result = CalcParameter(testParam, testRatio, 2);
       DebugExtension.Log($"計算結果 : {result}");
    }

    /// <summary>
    /// パラメーターをratio(%)上昇させる基本的な計算を行う関数(小数点以下切り捨て)
    /// </summary>
    /// <param name="param">パラメーター(整数値)</param>
    /// <param name="ratio">上昇率（少数）</param>
    /// <param name="significant">有効数字</param>
    private int CalcParameter(int param, float ratio, int significant = 0)
    {
        int sf = (int)Math.Pow(10, significant);
        DebugExtension.Log($"有効数字倍率　： {sf}");
        ratio *= sf;
        DebugExtension.Log($"int変換前　： {ratio:G17}");
        int r = (int)Math.Round(ratio);
        DebugExtension.Log($"int変換　： {r}");
        return param * (sf * 100 + r) / (sf * 100);
    }

    
    /// <summary>
    /// パラメーターをratio(％)上昇させる基本的な計算を行う関数（小数点以下切り捨て）
    /// </summary>
    /// <param name="param">パラメーター（整数値）</param>
    /// <param name="ratio">上昇値（整数）</param>
    /// <returns></returns>
    private int CalcParameter(int param, int ratio)
    {
        //計算で使用される変数を全て（int型）にそろえることによってfloatの丸め誤差を防ぐ
        return param * (100 + ratio) / 100;
        //NG パターン :
        //return Mathf.FloorToInt(param * (1 + ratio * 0.01f));
        //上記の式だとMathf.FloorToInt(float)の関数内でfloatの丸め誤差が起きる可能性がある
        //特にiosだとfloatの精度があがり、意図しない計算結果になることも
        // param = 140 , ratio = 60 だと[224]が正しいが、[223.9999999678]とかになって[223]に丸められることがおきた。
        //c#の言語使用上、int * float は　doubleに変換されて計算し、最後にfloatでキャストされる
    }

    /// <summary>
    /// サンプルテスト用
    /// </summary>
    private void Test()
    {
        //誤差が出ているか手っ取り早くEditorでみたいなら、怪しいところをDouble型に変えてLogを出してみる(＊ToString("G17")で出力すること)
        double calc_doudble = testParam * (1 + testRatio * 0.01f);
        float calc_float = testParam * (1 + testRatio * 0.01f);
        int floor_double = (int)calc_doudble;
        int floor_float = (int)calc_float;

        //floatやdoubleなどの型をそのままStringクラスのFormatに代入すると、文字列補間で切り上げ処理が行われる.
        DebugExtension.Log($"doubleの変数のまま出力 : {calc_doudble}");
        DebugExtension.Log($"floatの変数のまま出力 : {calc_float}");

        //文字列補間式（String interpolation）でFormatを使用する際に「：」の間にスペースを開けると正しく動作しない
        DebugExtension.Log($"doubleの変数をGeneralFormatで出力 : {calc_doudble:G17}");
        DebugExtension.Log($"floatの変数をGeneralFormatで出力 : {calc_float:G17}");

        DebugExtension.Log($"doubleの変数を切り捨ててから出力 : {floor_double}");
        DebugExtension.Log($"floatの変数を切り捨ててから出力 : {floor_float}");


        // int result = testParam * (100 + testRatio) / 100; //切り捨て処理と同じ
        // DebugExtension.Log($"100倍にしてFloat計算を排除してみた : {result}");
    }
}