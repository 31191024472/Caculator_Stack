using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStack
{
    public class ExpressionHandler
    {
        // Hàm để tính toán biểu thức hậu tố
        public double EvaluatePostfix(List<string> postfixExpression)
        {
            Stack<double> evaluationStack = new Stack<double>();

            foreach (var token in postfixExpression)
            {
                if (double.TryParse(token, out double num)) // Nếu là số
                {
                    evaluationStack.Push(num);
                }
                else if (IsOperator(token)) // Nếu là toán tử
                {
                    double b = evaluationStack.Pop(); // Lấy toán hạng thứ hai
                    double a = evaluationStack.Pop(); // Lấy toán hạng thứ nhất
                    double result = 0;

                    switch (token)
                    {
                        case "+":
                            result = a + b;
                            break;
                        case "-":
                            result = a - b;
                            break;
                        case "*":
                            result = a * b;
                            break;
                        case "/":
                            result = a / b;
                            break;
                        case "%":
                            result = a * (b / 100); // Phép toán phần trăm
                            break;
                        case "^":
                            result = Math.Pow(a, b); // Lũy thừa
                            break;
                    }

                    evaluationStack.Push(result); // Đưa kết quả vào stack
                }
            }

            return evaluationStack.Pop(); // Trả về kết quả cuối cùng
        }

        // Hàm kiểm tra toán tử
        public bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "%" || token == "^";
        }

        // Độ ưu tiên toán tử
        public int Precedence(string operatorSymbol)
        {
            if (operatorSymbol == "+" || operatorSymbol == "-") return 1;
            if (operatorSymbol == "*" || operatorSymbol == "/" || operatorSymbol == "%") return 2;
            if (operatorSymbol == "^") return 3; // Lũy thừa có độ ưu tiên cao hơn
            return 0;
        }

        // Hàm tính toán biểu thức
        public double EvaluateExpression(string expression)
        {
            // Chuyển từ trung tố sang hậu tố
            var postfixExpression = InfixToPostfix(expression);

            // Tính toán biểu thức hậu tố
            return EvaluatePostfix(postfixExpression);
        }

        // Chuyển từ trung tố qua hậu tố
        public List<string> InfixToPostfix(string expression)
        {
            Stack<string> operatorStack = new Stack<string>();
            List<string> output = new List<string>();

            int i = 0;
            while (i < expression.Length)
            {
                char currentChar = expression[i];

                // Nếu là số hoặc số âm
                if (Char.IsDigit(currentChar) ||
                    (currentChar == '-' && (i == 0 || expression[i - 1] == '(' || IsOperator(expression[i - 1].ToString()))))
                {
                    string number = "";
                    if (currentChar == '-') // Xử lý số âm
                    {
                        number += "-";
                        i++;
                    }
                    while (i < expression.Length && (Char.IsDigit(expression[i]) || expression[i] == '.'))
                    {
                        number += expression[i];
                        i++;
                    }
                    output.Add(number);
                    continue;
                }

                // Nếu là dấu ngoặc mở
                if (currentChar == '(')
                {
                    operatorStack.Push(currentChar.ToString());
                }
                // Nếu là dấu ngoặc đóng
                else if (currentChar == ')')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        output.Add(operatorStack.Pop());
                    }
                    operatorStack.Pop(); // Loại bỏ dấu ngoặc mở
                }
                // Nếu là toán tử
                else if (IsOperator(currentChar.ToString()))
                {
                    while (operatorStack.Count > 0 && Precedence(currentChar.ToString()) <= Precedence(operatorStack.Peek()))
                    {
                        output.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(currentChar.ToString());
                }
                i++;
            }

            // Đưa tất cả toán tử còn lại trong stack vào output
            while (operatorStack.Count > 0)
            {
                output.Add(operatorStack.Pop());
            }

            return output;
        }
    }
}
