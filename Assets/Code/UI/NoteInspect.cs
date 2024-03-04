using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteInspect : MonoBehaviour
{
    [SerializeField] private Sprite[] Symbols;
    [SerializeField] private Sprite[] Operators;

    [SerializeField] private Image[] NoteSymbols;
    [SerializeField] private Image[] NoteOperators;
    [SerializeField] private TMP_Text NoteAdd;

    public static string Value { get; private set; } = "";

    private bool generated = false; 
    public void Generate()
    {
        // Game.ResetFlag(Flag.Note);
        // Game.ResetFlag(Flag.NoteValue);
        // Game.ResetFlag(Flag.NoteAdd);

        if (generated) return;
        generated = true;

        string equation = "";
        string value = "";
        string add = "";

        if (Game.HasFlag(Flag.Note) && Game.HasFlag(Flag.NoteValue) && Game.HasFlag(Flag.NoteAdd))
        {
            equation = Game.GetOrGenerateString(Flag.Note, () => "");
            value = Game.GetOrGenerateString(Flag.NoteValue, () => "");
            add = Game.GetOrGenerateString(Flag.NoteAdd, () => "");
        }
        else
        {
            var result = GenerateEquation();

            equation = result.Equation;
            value = result.Value;
            add = result.Add;

            Debug.Log(result.Equation);
            Debug.Log(result.RealEquation);
            Debug.Log(result.Add);
            Debug.Log(result.Value);

            Game.SetString(Flag.Note, equation);
            Game.SetString(Flag.NoteValue, value);
            Game.SetString(Flag.NoteAdd, add);
        }

        Value = value;

        var symbol1 = System.Convert.ToInt32(equation[0].ToString());
        var symbol2 = System.Convert.ToInt32(equation[2].ToString());
        var symbol3 = System.Convert.ToInt32(equation[4].ToString());
        var symbol4 = System.Convert.ToInt32(equation[6].ToString());

        var operator1 = System.Convert.ToInt32(equation[1].ToString());
        var operator2 = System.Convert.ToInt32(equation[3].ToString());
        var operator3 = System.Convert.ToInt32(equation[5].ToString());


        NoteSymbols[0].sprite = Symbols[symbol1];
        NoteOperators[0].sprite = Operators[operator1];
        NoteSymbols[1].sprite = Symbols[symbol2];
        NoteOperators[1].sprite = Operators[operator2];
        NoteSymbols[2].sprite = Symbols[symbol3];
        NoteOperators[2].sprite = Operators[operator3];
        NoteSymbols[3].sprite = Symbols[symbol4];
        NoteAdd.text = "+" + add;
    }

    private (string Equation, string RealEquation, string Add, string Value) GenerateEquation()
    {
        string equation = "";
        string realEquation = "";
        string add = "";
        int value = 0;

        string[] letterValues = { "1", "3", "15", "5", "18", "25", "8", "14", "9" };
        string[] operators = { "-", "+", "*" };



        equation = "";
        equation += Random.Range(0, 9); // X
        equation += Random.Range(0, 3); // +
        equation += Random.Range(0, 9); // X
        equation += Random.Range(0, 3); // +
        equation += Random.Range(0, 9); // X
        equation += Random.Range(0, 3); // +
        equation += Random.Range(0, 9); // X

        var symbol1 = System.Convert.ToInt32(equation[0].ToString());
        var symbol2 = System.Convert.ToInt32(equation[2].ToString());
        var symbol3 = System.Convert.ToInt32(equation[4].ToString());
        var symbol4 = System.Convert.ToInt32(equation[6].ToString());

        var operator1 = System.Convert.ToInt32(equation[1].ToString());
        var operator2 = System.Convert.ToInt32(equation[3].ToString());
        var operator3 = System.Convert.ToInt32(equation[5].ToString());

        realEquation = letterValues[symbol1] + operators[operator1] + letterValues[symbol2] + operators[operator2] + letterValues[symbol3] + operators[operator3] + letterValues[symbol4];

        var addNumber = Random.Range(Game.SafeMinAdd, Game.SafeMaxAdd);
        add = addNumber.ToString();
        value = (int)(new System.Data.DataTable().Compute(realEquation, "")) + addNumber;



        return new(equation, realEquation, add, value.ToString());
    }

    private void Start()
    {
        Generate();
    }
}
