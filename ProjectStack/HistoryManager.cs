using System;
using System.Collections.Generic;

namespace ProjectStack
{
    public class CalculationRecord
    {
        public string Expression { get; set; } // Biểu thức
        public string Result { get; set; }    // Kết quả
        public DateTime Time { get; set; }    // Thời gian thực hiện

        public override string ToString()
        {
            // Định dạng hiển thị: căn trái phép tính và căn phải thời gian
            string formattedExpression = $"{Expression} = {Result}";
            string formattedDate = Time.ToString("MM/dd/yyyy HH:mm:ss");

            // Căn chỉnh bằng cách sử dụng PadRight và PadLeft
            return formattedExpression.PadRight(20) + formattedDate.PadLeft(30);
        }
    }

    public class HistoryManager
    {
        private Stack<CalculationRecord> calculationHistory = new Stack<CalculationRecord>();

        // Lưu phép tính vào lịch sử
        public void SaveCalculation(string expression, string result)
        {
            calculationHistory.Push(new CalculationRecord
            {
                Expression = expression,
                Result = result,
                Time = DateTime.Now // Lưu thời gian thực hiện
            });
        }

        // Tải lịch sử phép tính vào ListBox
        public List<CalculationRecord> LoadHistory()
        {
            // Chuyển stack thành list bằng cách sử dụng Linq
            return calculationHistory.Reverse().ToList(); // Đảo ngược thứ tự vì Stack là LIFO
        }
    }



}
