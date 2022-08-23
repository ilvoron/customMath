using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace customMath
{
    // Class for settings for comfortable using
    public class Settings
    {
        public char[] Numerals { get; }
        public char[][] Brackets { get; }
        public IDictionary<string, string>[] Operators { get; }
        public IDictionary<string, string> Functions { get; }
        public IDictionary<string, string> Constants { get; }

        public Settings(JsonElement settings)
        {
            Numerals = JsonSerializer.Deserialize<char[]>(settings.GetProperty("Numerals").GetRawText());
            Brackets = JsonSerializer.Deserialize<char[][]>(settings.GetProperty("Brackets").GetRawText());
            Operators = new Dictionary<string, string>[settings.GetProperty("Operators").GetArrayLength()];
            for (int i = 0; i < Operators.Length; i++) { Operators[i] = JsonSerializer.Deserialize<Dictionary<string, string>>(settings.GetProperty("Operators")[i].GetRawText()); }
            Functions = JsonSerializer.Deserialize<Dictionary<string, string>>(settings.GetProperty("Functions").GetRawText());
            Constants = JsonSerializer.Deserialize<Dictionary<string, string>>(settings.GetProperty("Constants").GetRawText());
        }

        // Misc.
        private int GetPriority(string opStr)
        {
            opStr = opStr.Replace(" ", "");

            for (int i = 0; i < Operators.Length; i++)
            {
                foreach (var op in Operators[i].Keys)
                {
                    if (op.Equals(opStr)) { return i; }
                }
            }

            return -1;
        }
        public bool IsLAssoc(string opStrStacked, string opStrToStack)
        {
            if (GetPriority(opStrStacked) >= GetPriority(opStrToStack)) { return true; }

            return false;
        }
        public void AddToStack(ref ArrayList stack, ref ArrayList output, string addStr)
        {
            addStr = addStr.Replace(" ", "");

            if (CheckFunc(addStr)) { stack.Add(addStr); }
            else if (stack.Count > 0)
            {
                string stackItem = stack[stack.Count - 1].ToString();
                
                while (IsLAssoc(stackItem, addStr))
                {
                    output.Add(stackItem);
                    stack.RemoveAt(stack.Count - 1);

                    if (stack.Count > 0) { stackItem = stack[stack.Count - 1].ToString(); }
                    else { break; }
                }

                stack.Add(addStr);
            } else { stack.Add(addStr); }
        }

        // Checkers
        public string[] CheckBrackets(char letter)
        {
            string[] brackets = new string[2] { "none", "-1" };

            for (int i = 0; i < Brackets.Length; i++)
            {
                if (Brackets[i][0] == letter)
                {
                    brackets = new string[2] { "open", i.ToString() };
                    break;
                }
                else if (Brackets[i][1] == letter)
                {
                    brackets = new string[2] { "close", i.ToString() };
                    break;
                }
            }

            return brackets;
        }
        public bool CheckOp(string opStr)
        {
            opStr = opStr.Replace(" ", "");

            foreach (var opList in Operators)
            {
                foreach (var op in opList.Keys)
                {
                    if (op.Equals(opStr)) { return true; }
                }
            }

            return false;
        }
        public bool CheckFunc(string funcStr)
        {
            funcStr = funcStr.Replace(" ", "");

            foreach (var func in Functions.Keys)
            {
                if (func.Equals(funcStr)) { return true; }
            }

            return false;
        }
        public bool CheckConst(string constStr)
        {
            constStr = constStr.Replace(" ", "");

            foreach (var _const in Constants.Keys)
            {
                if (_const.Equals(constStr)) { return true; }
            }

            return false;
        }

        // Get method
        public string GetOpMethod(string opStr)
        {
            opStr = opStr.Replace(" ", "");

            foreach (var opList in Operators)
            {
                foreach (var op in opList.Keys)
                {
                    if (op.Equals(opStr)) { return opList[op] ; }
                }
            }

            return "";
        }
        public string GetFuncMethod(string funcStr)
        {
            funcStr = funcStr.Replace(" ", "");

            foreach (var func in Functions.Keys)
            {
                if (func.Equals(funcStr)) { return Functions[func]; }
            }

            return "";
        }
        public string GetConstMethod(string constStr)
        {
            constStr = constStr.Replace(" ", "");

            foreach (var _const in Constants.Keys) {
                if (_const.Equals(constStr)) { return Constants[_const]; }
            }

            return "";
        }
    }
    
    
    
    // Class for methods which used when calculating occurs
    class Methods
    {
        public Methods() {}

        // Operators
        public static ArrayList plus(ArrayList arr, int index)
        {
            double res = double.Parse((string)arr[index - 2]) + double.Parse((string)arr[index - 1]);
            arr[index - 2] = res.ToString();
            arr.RemoveAt(index - 1);
            arr.RemoveAt(index - 1);

            return arr;
        }
        public static ArrayList minus(ArrayList arr, int index)
        {
            double res = double.Parse((string)arr[index - 2]) - double.Parse((string)arr[index - 1]);
            arr[index - 2] = res.ToString();
            arr.RemoveAt(index - 1);
            arr.RemoveAt(index - 1);

            return arr;
        }
        public static ArrayList multiply(ArrayList arr, int index)
        {
            double res = double.Parse((string)arr[index - 2]) * double.Parse((string)arr[index - 1]);
            arr[index - 2] = res.ToString();
            arr.RemoveAt(index - 1);
            arr.RemoveAt(index - 1);

            return arr;
        }
        public static ArrayList divide(ArrayList arr, int index)
        {
            double res = double.Parse((string)arr[index - 2]) / double.Parse((string)arr[index - 1]);
            arr[index - 2] = res.ToString();
            arr.RemoveAt(index - 1);
            arr.RemoveAt(index - 1);

            return arr;
        }
        public static ArrayList powerOp(ArrayList arr, int index)
        {
            double res = Math.Pow(double.Parse((string)arr[index - 2]), double.Parse((string)arr[index - 1]));
            arr[index - 2] = res.ToString();
            arr.RemoveAt(index - 1);
            arr.RemoveAt(index - 1);

            return arr;
        }
        public static ArrayList deg(ArrayList arr, int index)
        {
            double res = (Math.PI / 180) * double.Parse((string)arr[index - 1]);
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }
        public static ArrayList percent(ArrayList arr, int index)
        {
            double res = double.Parse((string)arr[index - 1]) / 100;
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }

        // Functions
        public static ArrayList sin(ArrayList arr, int index)
        {
            double res = Math.Sin(double.Parse((string)arr[index - 1]));
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }
        public static ArrayList cos(ArrayList arr, int index)
        {
            double res = Math.Cos(double.Parse((string)arr[index - 1]));
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }
        public static ArrayList tan(ArrayList arr, int index)
        {
            double res = Math.Tan(double.Parse((string)arr[index - 1]));
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }
        public static ArrayList cot(ArrayList arr, int index)
        {
            double res = 1/ Math.Tan(double.Parse((string)arr[index - 1]));
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }
        public static ArrayList degToRad(ArrayList arr, int index)
        {
            double res = (Math.PI / 180) * double.Parse((string)arr[index - 1]);
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }
        public static ArrayList radToDeg(ArrayList arr, int index)
        {
            double res = (180 / Math.PI) * double.Parse((string)arr[index - 1]);
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }
        public static ArrayList power(ArrayList arr, int index)
        {
            double res = Math.Pow(double.Parse((string)arr[index - 2]), double.Parse((string)arr[index - 1]));
            arr[index - 2] = res.ToString();
            arr.RemoveAt(index - 1);
            arr.RemoveAt(index - 1);

            return arr;
        }
        public static ArrayList sqrt(ArrayList arr, int index)
        {
            double res = Math.Sqrt(double.Parse((string)arr[index - 1]));
            arr[index - 1] = res.ToString();
            arr.RemoveAt(index);

            return arr;
        }

        // Constants
        public static ArrayList pi(ArrayList arr, int index)
        {
            arr[index] = Math.PI.ToString();

            return arr;
        }
    }
    
    
    
    // Main class
    class Program
    {
        static string settingsJSON;
        static Settings settings;

        // ArrayList to String. Ex. "[ '1', '3435', 'gagji9' ]"
        public static string ArrayListToString(ArrayList arr)
        {
            string str = "[ ";

            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    str = $"{str} '{arr[i]}'";
                    if (i != arr.Count - 1) { str = str + " , "; }
                }
            }

            str = str + " ]";
            return str;
        }

        // Initialization
        static void Main(string[] args)
        {
            settingsJSON = File.ReadAllText("settings.json");
            settings = new Settings(JsonDocument.Parse(settingsJSON).RootElement);

            Console.WriteLine("[~~~INPUT~~~]");
            string input = Console.ReadLine();
            Console.WriteLine();

            ArrayList output = Parse(input);
            string calcRes = Calculate(output);
            Console.WriteLine("[~~~OUTPUT~~~]");
            Console.WriteLine(calcRes);
        }

        // Parsing to something like postfix notation (but it's not)
        static ArrayList Parse(string str)
        {
            string numbStr = "", multiStr = "";
            ArrayList output = new ArrayList(), stack = new ArrayList();
            str = str.Replace(" ", "");

            foreach (var letter in str)
            {
                if (Array.IndexOf(settings.Numerals, letter) > -1) {
                    if (multiStr.Length > 0)
                    {
                        settings.AddToStack(ref stack, ref output, multiStr);
                        multiStr = "";
                    }

                    numbStr += letter;
                    continue;
                }

                if (letter == ',') { output.Add(numbStr.Replace(" ", "")); numbStr = ""; }
                else if (letter == '.') { numbStr += "."; }
                else {
                    if (numbStr.Length > 0)
                    {
                        output.Add(numbStr.Replace(" ", ""));
                        numbStr = "";
                    }

                    string[] brackets = settings.CheckBrackets(letter);

                    if (brackets[0] != "none")
                    {
                        if (brackets[0] == "open") { stack.Add(letter); }
                        else
                        {
                            char openBracket = settings.Brackets[int.Parse(brackets[1])][0];

                            int i = stack.Count - 1;
                            while (!stack[i].ToString().Equals(openBracket.ToString()))
                            {
                                output.Add(stack[i].ToString().Replace(" ", ""));
                                stack.RemoveAt(i);
                                i--;
                            }
                            stack.RemoveAt(i);

                            if (stack.Count > 0)
                            {
                                if (settings.CheckFunc(stack[stack.Count - 1].ToString()))
                                {
                                    output.Add(stack[stack.Count - 1]);
                                    stack.RemoveAt(stack.Count - 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        multiStr += letter;
    
                        if (settings.CheckOp(multiStr) || settings.CheckFunc(multiStr))
                        {
                            settings.AddToStack(ref stack, ref output, multiStr);
                            multiStr = "";
                        }

                        if (settings.CheckConst(multiStr))
                        {
                            numbStr = multiStr;
                            multiStr = "";
                        }
                    }
                }
            }

            // Adding last number and/or last items in stack to output (if they are)
            if (numbStr != "") { output.Add(numbStr); }
            if (stack.Count > 0) { for (int i = stack.Count - 1; i > -1; i--) { output.Add(stack[i].ToString().Replace(" ", "")); } }

            return output;
        }

        // Calculate this "postfix notation" like ArrayList to string with result of the calculation
        public static string Calculate(ArrayList arr)
        {
            int i = 0;

            while (arr.Count > 1)
            {
                string method = "";

                if (settings.CheckOp(arr[i].ToString())) { method = settings.GetOpMethod(arr[i].ToString()); }
                else if (settings.CheckFunc(arr[i].ToString())) { method = settings.GetFuncMethod(arr[i].ToString()); }
                else if (settings.CheckConst(arr[i].ToString())) { method = settings.GetConstMethod(arr[i].ToString()); }

                if (method.Length > 0)
                {
                    MethodInfo methodsMethod = typeof(Methods).GetMethod(method);
                    object methodsValue = methodsMethod.Invoke(null, new object[] { arr, i });
                    arr = ArrayList.Adapter((IList)methodsValue);
                    i = 0;
                    continue;
                }

                i++;
            }

            return arr[0].ToString();
        }

    }
}
