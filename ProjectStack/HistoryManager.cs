using System;
using System.Collections.Generic;

namespace ProjectStack
{

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
                Time = DateTime.Now
            });
        }


        // Lấy toàn bộ lịch sử (đảm bảo phần tử trên cùng là mới nhất)
        public List<CalculationRecord> LoadHistory()
        {
            return new List<CalculationRecord>(calculationHistory);
        }
        public void ClearHistory()
        {
            calculationHistory.Clear();
        }
        public bool RemoveRecord(string expression)
        {
            Stack<CalculationRecord> tempStack = new Stack<CalculationRecord>();
            bool isRemoved = false;

            // Duyệt qua stack, tìm và loại bỏ phần tử có biểu thức khớp
            while (calculationHistory.Count > 0)
            {
                var record = calculationHistory.Pop();
                if (!isRemoved && record.Expression == expression)
                {
                    isRemoved = true;
                }
                else
                {
                    tempStack.Push(record);
                }
            }

            // Đưa các phần tử còn lại trở lại stack chính
            while (tempStack.Count > 0)
            {
                calculationHistory.Push(tempStack.Pop());
            }

            return isRemoved; // Trả về true nếu đã xóa, false nếu không tìm thấy
        }
        // Cập nhật một phép toán trong lịch sử
        public void UpdateHistory(string oldExpression, string newExpression, string newResult)
        {
            Stack<CalculationRecord> tempStack = new Stack<CalculationRecord>();
            bool isUpdated = false;

            // Duyệt qua stack, loại bỏ phép tính cũ nếu tồn tại
            while (calculationHistory.Count > 0)
            {
                var record = calculationHistory.Pop();
                if (!isUpdated && record.Expression == oldExpression)
                {
                    isUpdated = true;
                }
                else
                {
                    tempStack.Push(record);
                }
            }

            // Đưa tất cả phép tính cũ trở lại stack
            while (tempStack.Count > 0)
            {
                calculationHistory.Push(tempStack.Pop());
            }

            // Thêm phép tính mới vào đầu stack nếu đã cập nhật
            if (isUpdated)
            {
                SaveCalculation(newExpression, newResult);
            }
        }

    }
}
