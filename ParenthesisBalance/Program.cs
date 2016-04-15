using System;
using System.Collections.Generic;

namespace ParanthesisBalance {
    public static class Program {
        private static Dictionary<char, int> _parantheses = new Dictionary<char, int>();
        private static Dictionary<string, Stack<Bracket>> _foundParanthesis = new Dictionary<string, Stack<Bracket>>();

        static void Main(string[] args) {

            // value - even is open, odd is closed
            _parantheses.Add('(', 0);
            _parantheses.Add(')', 1);
            _parantheses.Add('[', 2);
            _parantheses.Add(']', 3);
            _parantheses.Add('{', 4);
            _parantheses.Add('}', 5);

            Console.WriteLine("Please input text for paranthesis analysis: ");
            //string input = Console.ReadLine();
            string input = "( demo text ( [test] ( just a small ( text ( with all kind of { parentheses in {( different ( locations ) who ) then } also } with ) each other ) should ) match ) or ) not... )";
            //string input = "( a ] b [ c ( x [";

            Console.WriteLine(input);

            char lastOpenParanthesis = new Char();
            bool isLocalBalance = true;
            if (!string.IsNullOrEmpty(input)
                && !(_parantheses.ContainsKey(input[0]) && _parantheses[input[0]] % 2 == 1)) {
                for (int i = 0; i < input.Length; i++) {
                    if (_parantheses.ContainsKey(input[i])) {
                        if (IsOpen(input[i])) {
                            lastOpenParanthesis = input[i];
                            PushSymbol(new Bracket() { Symbol = input[i], Index = i });
                            isLocalBalance = false;
                        }
                        else {
                            if (PopSymbol(input[i])) {
                                if (_parantheses[input[i]] - _parantheses[lastOpenParanthesis] == 1) {
                                    isLocalBalance = true;
                                }
                                else {
                                    if (!isLocalBalance && Math.Abs(_parantheses[input[i]] - _parantheses[lastOpenParanthesis]) > 1) {
                                        if (input.Length - i - 1 > 3) {
                                            Console.WriteLine("Fault sequence: {0}", input.Substring(i - 3, 7));
                                        }
                                        else Console.WriteLine("Fault bracket: {0}, index: {1}", input[i], i);
                                        break;
                                    }
                                }
                            }
                            else {
                                if (input.Length - i - 1 > 3) {
                                    Console.WriteLine("Fault sequence: {0}", input.Substring(i - 3, 7));
                                }
                                else Console.WriteLine("Fault bracket: {0}, index: {1}", input[i], i);
                            }
                        }
                    }
                }

            }

            Console.ReadKey();

        }

        private static bool IsOpen(char p) {
            if (_parantheses[p] % 2 == 0)
                return true;
            else return false;
        }

        private static void PushSymbol(Bracket symbol) {
            switch (symbol.Symbol) {
                case '(':
                    if (!_foundParanthesis.ContainsKey("round"))
                        _foundParanthesis.Add("round", new Stack<Bracket>());
                    _foundParanthesis["round"].Push(symbol);
                    break;
                case '[':
                    if (!_foundParanthesis.ContainsKey("square"))
                        _foundParanthesis.Add("square", new Stack<Bracket>());
                    _foundParanthesis["square"].Push(symbol);
                    break;
                case '{':
                    if (!_foundParanthesis.ContainsKey("curly"))
                        _foundParanthesis.Add("curly", new Stack<Bracket>());
                    _foundParanthesis["curly"].Push(symbol);
                    break;
            }
        }

        private static bool PopSymbol(char symbol) {
            switch (symbol) {
                case ')':
                    if (_foundParanthesis.ContainsKey("round")) {
                        if (_foundParanthesis["round"].Count > 0) {
                            _foundParanthesis["round"].Pop();
                            return true;
                        }
                    }
                    return false;
                case ']':
                    if (_foundParanthesis.ContainsKey("square")) {
                        if (_foundParanthesis["square"].Count > 0) {
                            _foundParanthesis["square"].Pop();
                            return true;
                        }
                    }
                    return false;
                case '}':
                    if (_foundParanthesis.ContainsKey("curly")) {
                        if (_foundParanthesis["curly"].Count > 0) {
                            _foundParanthesis["curly"].Pop();
                            return true;
                        }
                    }
                    return false;
                default: return false;
            }
        }
    }
}