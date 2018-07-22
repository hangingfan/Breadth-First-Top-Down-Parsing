using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Breadth_TopDown
{

    class Program
    {
        private static int csvMax = 5;
        private static char StartSymbol = 'S';
        private static char endSymbol = '#';
        private static int intToChar = 48;

        private static Dictionary<char, List<string>> getFileContent(string name)
        {
            var csvReader = new StreamReader(name);
            int index = 0; //the first line is for description ,not real data, skip it
            var content = new Dictionary<char, List<string>>();
            while (!csvReader.EndOfStream)
            {
                var line = csvReader.ReadLine();

                index++;
                if (index == 1)
                {
                    continue;
                }

                var values = line.Split(',');
                if (values[0] != "" && values[0] != " ")
                {
                    var List = new List<string>();
                    for (int i = 1; i <= csvMax; ++i)
                    {
                        if (values[i] != "" && values[i] != " ")
                        {
                            List.Add(values[i]);
                        }
                    }
                    content[values[0][0]] = List;
                }
            }

            return content;
        }

        private static Dictionary<char, List<string>> content;

        private static Dictionary<Stack<char>, Stack<char>> analysis = new Dictionary<Stack<char>, Stack<char>>();

        private static HashSet<char> nonTerminalHashset = new HashSet<char>()
        {
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'I',
            'J',
            'K',
            'L',
            'M',
            'N',
            'O',
            'P',
            'Q',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z'
        };

        private static Stack<char> restInputStack = new Stack<char>();

        static void Main(string[] args)
        {
            content = getFileContent("rules.csv");
            if (content.ContainsKey(StartSymbol) == false)
            {
                return;
            }

            restInputStack.Clear();
            restInputStack.Push(endSymbol);
            Console.WriteLine("input your input string:");
            var UserInput = Console.ReadLine();
            if (UserInput.Length == 0)
            {
                return;
            }
            for (int i = UserInput.Length - 1; i > -1; i--)
            {
                restInputStack.Push(UserInput[i]);
            }

            var startStack = new Stack<char>();
            startStack.Push(endSymbol);
            startStack.Push(StartSymbol);

            var startStackList = new List<Stack<char>>();
            startStackList.Add(startStack);

            analysis[startStack] = startStack;

            replaceNonTerminal(startStackList);
            Console.ReadLine();
        }

        // when the front is non-terminal,replace it until all the front is terminal
        private static void replaceNonTerminal(List<Stack<char>> hasNonTerminalFrontList)
        {
            bool nonTerminalExistInFront = false;
            List<Stack<char>> newHasNonTerminalFrontList = new List<Stack<char>>();
            for (int i = 0; i < hasNonTerminalFrontList.Count; i++)
            {
                Stack<char> keyStack = hasNonTerminalFrontList[i];
                Stack<char> valueStack = analysis[keyStack];
                char replaceChar = valueStack.Pop();
                analysis.Remove(keyStack);
                var replaceList = content[replaceChar];  //get all right-sides rule

                for (int replaceIndex = 0; replaceIndex < replaceList.Count; ++replaceIndex)
                {
                    Stack<char> analysisStack = new Stack<char>(keyStack.Reverse());
                    analysisStack.Push(replaceChar);
                    analysisStack.Push(Convert.ToChar(replaceIndex + 1 + intToChar));

                    Stack<char> predictionStack = new Stack<char>(valueStack.Reverse());

                    string rightString = replaceList[replaceIndex];
                    for (int j = rightString.Length - 1; j > -1; j--)
                    {
                        predictionStack.Push(rightString[j]);
                    }
                    analysis[analysisStack] = predictionStack;

                    //just for show
                    Console.WriteLine("replace the front non-terminal:");
                    Console.WriteLine(getReplaceRules(analysisStack) + " ---> " + getReplaceRules(predictionStack));

                    if (nonTerminalHashset.Contains(predictionStack.Peek()))
                    {
                        newHasNonTerminalFrontList.Add(analysisStack);
                        nonTerminalExistInFront = true;
                    }
                }
            }

            if (nonTerminalExistInFront)  // recursive replace non-Terminal
            {
                replaceNonTerminal(newHasNonTerminalFrontList);
            }

            //strike out all the inpropriate prediction
            AnalysisRecursive();
        }

        //judge the front is terminal and remove it
        private static void AnalysisRecursive()
        {
            if (restInputStack.Count == 0)
            {
                return;
            }

            //just for show
            Console.WriteLine("current analysis:");
            foreach (var VARIABLE in analysis)
            {
                Console.WriteLine(getReplaceRules(VARIABLE.Key) + " ---> " + getReplaceRules(VARIABLE.Value));
            }


            char currentSymbol = restInputStack.Pop();
            
            var _removeList = new List<Stack<char>>();
            bool nonTerminalExistInFront = false;
            var hasNonTerminalFrontList = new List<Stack<char>>();
            foreach (var VARIABLE in analysis)
            {
                if (VARIABLE.Value.Peek() != currentSymbol)
                {
                    _removeList.Add(VARIABLE.Key);
                }
                else
                {
                    char removeChar = VARIABLE.Value.Pop();
                    Console.WriteLine("remove front char, " + removeChar + " rest: " + getReplaceRules(VARIABLE.Value));
                    if (currentSymbol == endSymbol && removeChar == endSymbol)  //find one correct rule
                    {
                        Console.WriteLine("finded, replace rules: " + getReplaceRules(VARIABLE.Key));
                        return;
                    }

                    //after strike out the terminal, replace again the non-terminal in the front
                    if (nonTerminalHashset.Contains(VARIABLE.Value.Peek()))
                    {
                        hasNonTerminalFrontList.Add(VARIABLE.Key);
                        nonTerminalExistInFront = true;
                    }
                }
            }

            for (int i = 0; i < _removeList.Count; i++)
            {
                Console.WriteLine("remove bad prediction, " + getReplaceRules(_removeList[i]));
                analysis.Remove(_removeList[i]);
            }

            if (nonTerminalExistInFront)
            {
                replaceNonTerminal(hasNonTerminalFrontList);
            }
            else
            {
                AnalysisRecursive();
            }
        }


        private static string getReplaceRules(Stack<char> currentStack)
        {
            Stack<char> tempStack = new Stack<char>(currentStack);
            string content = "";
            while (tempStack.Count != 0)
            {
                content = tempStack.Pop() + content;
            }
            return content;
        }
    }
}
