using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class gameManager : MonoBehaviour
{
    public TMP_Text MainText; //メインテキスト
    public GameObject MainButton; //メインボタン
    public GameObject Choice1Button; //選択肢１ボタン
    public GameObject Choice2Button; //選択肢２ボタン
    public GameObject Choice3Button; //選択肢３ボタン
    public TMP_Text Choice1Text; //選択肢１テキスト
    public TMP_Text Choice2Text; //選択肢２テキスト
    public TMP_Text Choice3Text; //選択肢３テキスト
    public Image LeftImage; //左側に立つキャラ
    public Image RightImage; //右側に立つキャラ
    public TMP_Text LeftName; //左側に立つキャラの名前
    public TMP_Text RightName; //右側に立つキャラの名前



    [SerializeField]
    private float _delayDuration = 0.1f; // 次の文字を表示するまでの時間[s]
    private Coroutine _showCoroutine; //二重実行を防ぐ


    private int _lineNum = 1; //行番号
    // private int ColumnNum = 0; //列番号 //まだいらなさそう

    //main
    private int _idColumn = 0; //idの列番号 //どのcsvもidを一番左に書くので全部これ使う
    private int _leftColumn = 1; //leftの列番号
    private int _rightColumn = 2; //rightの列番号
    private int _textColumn = 4; //textの列番号
    private int _choice1Column = 5; //choice1の列番号
    private int _choice2Column = 6; //choice2の列番号
    private int _choice3Column = 7; //choice3の列番号
    private int _jump1Column = 11; //jump1の列番号
    private int _jump2Column = 12; //jump2の列番号
    private int _jump3Column = 13; //jump3の列番号

    //character
    private int _passColumn = 1; //passの列番号
    private int _nameColumn = 2; //nameの列番号

    private TextAsset MainCsvFile; //MainCSVファイル
    private List<string[]> MainCsvData = new List<string[]>(); //MainCSVファイルの中身を入れるリスト

    private TextAsset CharacterCsvFile; //CharacterCSVファイル
    private List<string[]> CharacterCsvData = new List<string[]>(); //MainCSVファイルの中身を入れるリスト


    void Start()
    {

        //選択肢ボタンを隠す
        ChoiceButtonHide();

        //maincsv
        MainCsvFile = Resources.Load("1-1") as TextAsset; //ResourcesにあるCSVファイルを格納
        StringReader mainreader = new StringReader(MainCsvFile.text); //TextAssetをStringReaderに変換
        while (mainreader.Peek() != -1)
        {
            string line = mainreader.ReadLine(); //1行ずつ読み込む
            MainCsvData.Add(line.Split(',')); //csvDataリストに追加する
        }

        //chractercsv
        CharacterCsvFile = Resources.Load("CharacterImageTemplate") as TextAsset; //ResourcesにあるCSVファイルを格納
        StringReader characterreader = new StringReader(CharacterCsvFile.text); //TextAssetをStringReaderに変換
        while (characterreader.Peek() != -1)
        {
            string line = characterreader.ReadLine(); //1行ずつ読み込む
            CharacterCsvData.Add(line.Split(',')); //csvDataリストに追加する
        }

        //maincsvにおける各Columnの位置をセット
        MainColumnNumSet();

        //charactercsvにおける各Columnの位置をセット
        CharacterColumnSet();

        //テキストに入力
        TextSet();

        //キャラクターimageをセット
        CharacterImageSet();
    }


    //character内の各columnの位置を探す
    private void CharacterColumnSet()
    {
        //""の中身が正しいか必ず確認
        for (int i = 0; i < CharacterCsvData[0].Length; i++)
        {

            if (CharacterCsvData[0][i] == "pass")
            {
                _passColumn = i;
            }

            if (CharacterCsvData[0][i] == "name")
            {
                _nameColumn = i;
            }
        }
    }


    //maincsv内の各columnの位置を探す
    private void MainColumnNumSet()
    {

        //""の中身が正しいか必ず確認
        for (int i = 0; i < MainCsvData[0].Length; i++)
        {


            if (MainCsvData[0][i] == "id")
            {
                _idColumn = i;
            }
            else if (MainCsvData[0][i] == "left")
            {
                _leftColumn = i;
            }
            else if (MainCsvData[0][i] == "right")
            {
                _rightColumn = i;
            }
            else if (MainCsvData[0][i] == "text")
            {
                _textColumn = i;
            }
            else if (MainCsvData[0][i] == "choice1")
            {
                _choice1Column = i;
            }
            else if (MainCsvData[0][i] == "choice2")
            {
                _choice2Column = i;
            }
            else if (MainCsvData[0][i] == "choice3")
            {
                _choice3Column = i;
            }
            else if (MainCsvData[0][i] == "jump1")
            {
                _jump1Column = i;
            }
            else if (MainCsvData[0][i] == "jump2")
            {
                _jump2Column = i;
            }
            else if (MainCsvData[0][i] == "jump3")
            {
                _jump3Column = i;
            }
        }
    }


    //次の会話に進む処理
    public void MainButtonClick()
    {

        if (_showCoroutine == null)
        {
            if (MainCsvData[_lineNum][_jump1Column] == "") //jump1にidが入力してあるか
            {
                _lineNum++; //ない場合は次の行に進む
            }
            else
            {
                for (int i = 0; i < MainCsvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
                {

                    if (MainCsvData[i][_idColumn] == MainCsvData[_lineNum][_jump1Column])
                    {
                        _lineNum = i;
                        break;
                    }

                }
            }

            //選択肢がその会話にあるかチェック
            //ChoiceCheck();
            TextSet();
            CharacterImageSet();
        }
        else //現在の会話のスキップ処理
        {
            StopCoroutine(_showCoroutine);
            _showCoroutine = null;
            MainText.maxVisibleCharacters = int.MaxValue;
            ChoiceCheck();
        }
    }


    //選択肢がその会話にあるかチェック
    private void ChoiceCheck()
    {
        if (MainCsvData[_lineNum][_choice1Column] != "") //次の行の選択肢の有無を確認
        {
            //選択肢があった場合
            MainButton.SetActive(false);
            Choice1Button.SetActive(true);
            Choice1Text.text = MainCsvData[_lineNum][_choice1Column];

        }
        if (MainCsvData[_lineNum][_choice2Column] != "")
        {
            Choice2Button.SetActive(true);
            Choice2Text.text = MainCsvData[_lineNum][_choice2Column];

        }
        if (MainCsvData[_lineNum][_choice3Column] != "")
        {
            Choice3Button.SetActive(true);
            Choice3Text.text = MainCsvData[_lineNum][_choice3Column];

        }
    }


    //テキストに表示
    //この処理にはShow()を含む
    //MainTextの_lineNumや_textColumnの値はこの関数外で指定したい
    private void TextSet()
    {
        //Show()の前に文字送りさせたい文章を指定
        //そのうちこの処理をShow()の中に入れる可能性あり
        MainText.text = MainCsvData[_lineNum][_textColumn];
        //メインテキストの文字送り
        Show();

        //Leftの名前をセット
        //ifの条件がImageSetと同じだからそのうち一緒にまとめてもいいかも
        if (MainCsvData[_lineNum][_leftColumn] != "")
        {
            for (int i = 0; i < CharacterCsvData.Count; i++)
            {

                if (CharacterCsvData[i][_idColumn] == MainCsvData[_lineNum][_leftColumn])
                {
                    LeftName.text = CharacterCsvData[i][_nameColumn];
                }
            }
        }

        //Rightの名前をセット
        if (MainCsvData[_lineNum][_rightColumn] != "")
        {
            for (int i = 0; i < CharacterCsvData.Count; i++)
            {
                if (CharacterCsvData[i][_idColumn] == MainCsvData[_lineNum][_rightColumn])
                {
                    RightName.text = CharacterCsvData[i][_nameColumn];
                }
            }
        }
    }


    //それぞれの選択に応じた処理を行う
    public void Choice1Click()
    {
        ChoiceButtonHide();

        //ジャンプ先のIDを検索してそこの会話に飛ぶ
        for (int i = 0; i < MainCsvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (MainCsvData[i][_idColumn] == MainCsvData[_lineNum][_jump1Column])
            {

                _lineNum = i;
                TextSet();
                break;
            }

        }

        CharacterImageSet();
        MainButton.SetActive(true);

    }

    public void Choice2Click()
    {
        ChoiceButtonHide();

        for (int i = 0; i < MainCsvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (MainCsvData[i][_idColumn] == MainCsvData[_lineNum][_jump2Column])
            {

                _lineNum = i;
                TextSet();
                break;
            }

        }

        CharacterImageSet();
        MainButton.SetActive(true);
    }

    public void Choice3Click()
    {
        ChoiceButtonHide();

        for (int i = 0; i < MainCsvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (MainCsvData[i][_idColumn] == MainCsvData[_lineNum][_jump3Column])
            {

                _lineNum = i;
                TextSet();
                break;
            }

        }

        CharacterImageSet();
        MainButton.SetActive(true);
    }

    //選択肢ボタンを隠す
    private void ChoiceButtonHide()
    {
        Choice1Button.SetActive(false);
        Choice2Button.SetActive(false);
        Choice3Button.SetActive(false);
    }

    //キャラクターのimageをセットする
    private void CharacterImageSet()
    {
        //Leftの確認
        if (MainCsvData[_lineNum][_leftColumn] != "")
        {
            for (int i = 0; i < CharacterCsvData.Count; i++)
            {

                if (CharacterCsvData[i][_idColumn] == MainCsvData[_lineNum][_leftColumn])
                {

                    LeftImage.sprite = Resources.Load<Sprite>(CharacterCsvData[i][_passColumn]);

                }
            }
        }

        //Rightの確認
        if (MainCsvData[_lineNum][_rightColumn] != "")
        {
            for (int i = 0; i < CharacterCsvData.Count; i++)
            {

                if (CharacterCsvData[i][_idColumn] == MainCsvData[_lineNum][_rightColumn])
                {

                    RightImage.sprite = Resources.Load<Sprite>(CharacterCsvData[i][_passColumn]);

                }
            }
        }
    }

    //呼び出すときはこっち
    private void Show() //一旦privateにしとく
    {
        // 前回の演出処理が走っていたら、停止
        if (_showCoroutine != null)
        {
            StopCoroutine(_showCoroutine);
        }

        // １文字ずつ表示する演出のコルーチンを実行する
        _showCoroutine = StartCoroutine(ShowCoroutine());

    }

    // １文字ずつ表示する演出のコルーチン
    private IEnumerator ShowCoroutine()
    {
        // 待機用コルーチン
        // GC Allocを最小化するためキャッシュしておく
        var delay = new WaitForSeconds(_delayDuration);

        // テキスト全体の長さ
        var length = MainText.text.Length;

        // １文字ずつ表示する演出
        for (var i = 0; i < length; i++)
        {
            // 徐々に表示文字数を増やしていく
            MainText.maxVisibleCharacters = i;

            // 一定時間待機
            yield return delay;
        }

        // 演出が終わったら全ての文字を表示する
        MainText.maxVisibleCharacters = length;

        _showCoroutine = null;

        ChoiceCheck();

    }


}