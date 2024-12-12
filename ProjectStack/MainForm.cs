using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectStack
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            panelHistory.Visible = false;
            // Đăng ký sự kiện MouseDoubleClick cho listBoxHistory
            listBoxHistory.MouseDoubleClick += listBoxHistory_MouseDoubleClick;


        }
        private Stack<string> redoHistory = new Stack<string>(); // Stack cho Redo
        private Stack<string> calculationHistory = new Stack<string>(); // Stack chứa lịch sử các phép toán
        private Stack<string> expressionHistory = new Stack<string>(); // Stack lưu trữ lịch sử biểu thức
        private HistoryManager historyManager = new HistoryManager(); // Đối tượng quản lý lịch sử
        private int currentIndex = -1; // Vị trí hiện tại trong lịch sử
        private string lastExpressionBeforeEquals = ""; // Biểu thức trước khi nhấn "="
        private ExpressionHandler expressionHandler = new ExpressionHandler();
        private bool isCalculated = false;  // Cờ theo dõi trạng thái đã tính toán hay chưa
        private bool hasStateChanged = false; // Cờ theo dõi thay đổi
        private void AdjustButtonImage()
        {
            // Đảm bảo rằng hình ảnh đã được thêm vào Resources dưới tên "menuIcon"
            Image originalImage = Properties.Resources.list; // Tên hình ảnh trong Resources

            // Cập nhật hình ảnh cho button
            buttonMenu.Image = originalImage;
        }


        // Lưu lại trạng thái hiện tại của biểu thức
        private void SaveCurrentState()
        {
            // Lưu trạng thái hiện tại vào stack
            if (!string.IsNullOrEmpty(currentInput))
            {
                expressionHistory.Push(currentInput);
            }
            // Clear redoHistory mỗi khi có thao tác mới
            redoHistory.Clear();

        }

        // Xử lý khi nhấn số
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Nếu đã tính toán và người dùng bắt đầu nhập số mới
                if (isCalculated)
                {
                    // Reset lại labelExpression và textBoxResult khi bắt đầu nhập số mới
                    labelExpression.Text = "";
                    textBoxResult.Text = button.Text;  // Hiển thị số mới
                    currentInput = button.Text;  // Cập nhật currentInput
                    isCalculated = false;  // Đặt lại cờ isCalculated sau khi nhập số mới
                }
                else
                {
                    // Nếu chưa tính toán, tiếp tục nối số mới vào currentInput
                    currentInput += button.Text;
                    textBoxResult.Text = currentInput;  // Cập nhật textBoxResult với số mới
                }

                // Lưu lại vào expressionHistory sau mỗi thao tác
                SaveCurrentState();
            }
        }


        // Xử lý khi nhấn toán tử
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Thêm toán tử nếu biểu thức không rỗng 
                if (!string.IsNullOrEmpty(currentInput))
                {
                    currentInput += " " + button.Text + " ";
                    labelExpression.Text = " ";
                    textBoxResult.Text = currentInput;  // Cập nhật LabelExpression với biểu thức mới
                    isCalculated = false;  // Đặt lại cờ isCalculated sau khi nhập số mới
                }


                // Lưu lại vào expressionHistory sau mỗi thao tác
                SaveCurrentState();
            }
        }

        // Xử lý khi nhấn dấu ngoặc
        private void ParenthesisButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string inputChar = button.Text; // Dấu ngoặc người dùng nhấn ( "(" hoặc ")" )

                if (inputChar == "(")
                {
                    // Nếu cuối cùng là số hoặc ')' thì thêm '*' trước khi thêm '('
                    if (!string.IsNullOrEmpty(currentInput) &&
                        (char.IsDigit(currentInput[^1]) || currentInput[^1] == ')'))
                    {
                        currentInput += "*";
                    }

                    // Thêm dấu '('
                    currentInput += "(";
                }
                else if (inputChar == ")")
                {
                    // Đếm số ngoặc mở và đóng hiện tại
                    int openCount = currentInput.Count(c => c == '(');
                    int closeCount = currentInput.Count(c => c == ')');

                    // Chỉ thêm ')' nếu trước đó là số hoặc ')' và số ngoặc đóng không vượt ngoặc mở
                    if (!string.IsNullOrEmpty(currentInput) &&
                        (char.IsDigit(currentInput[^1]) || currentInput[^1] == ')') &&
                        openCount > closeCount)
                    {
                        currentInput += ")";
                    }
                }

                // Cập nhật textbox và lưu trạng thái
                textBoxResult.Text = currentInput;
                isCalculated = false;  // Đặt lại cờ isCalculated sau khi nhập mới
                SaveCurrentState();    // Lưu lại vào expressionHistory sau mỗi thao tác
            }
        }


        // Xử lý khi nhấn dấu "=" để tính toán
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentInput))
                {
                    textBoxResult.Text = "0"; // Nếu không có biểu thức, hiển thị 0
                    return;
                }
                // Chuyển dấu phẩy thành dấu chấm trước khi tính toán
                currentInput = ConvertCommaToDot(currentInput);

                // Hiển thị biểu thức (mờ, nhỏ ở trên)
                labelExpression.Text = currentInput;

                // Lưu lại biểu thức trước khi tính toán
                lastExpressionBeforeEquals = currentInput;

                // Tính toán biểu thức
                var result = expressionHandler.EvaluateExpression(currentInput);
                // Hiển thị kết quả (to, rõ ở dưới)
                textBoxResult.Text = result.ToString();
                currentInput = result.ToString(); // Cập nhật biểu thức hiện tại

                expressionHistory.Push(lastExpressionBeforeEquals);

                // Lưu phép tính vào lịch sử
                SaveCalculation(lastExpressionBeforeEquals, result.ToString());

                // Xóa Redo stack sau khi tính toán xong
                redoHistory.Clear();
                // Đặt cờ isCalculated là true sau khi tính toán
                isCalculated = true;
            }
            catch (Exception ex)
            {
                textBoxResult.Text = "Error"; // Hiển thị lỗi nếu có
            }


        }
        private void UpdateLabels(string input)
        {
            labelExpression.Text = ""; // Làm trống biểu thức mờ
            textBoxResult.Text = input; // Hiển thị biểu thức hiện tại
        }


        // Xử lý khi nhấn nút "C" để xóa
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Xóa sạch undo stack và redo stack
            expressionHistory.Clear();
            redoHistory.Clear();

            currentInput = "";
            labelExpression.Text = ""; // Xóa biểu thức
            textBoxResult.Text = ""; // Xóa kết quả
        }

        // Xử lý khi nhấn nút "DEL" để xóa ký tự cuối
        private void DelButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentInput = currentInput.Remove(currentInput.Length - 1);
                textBoxResult.Text = currentInput;
                // Lưu lại vào expressionHistory sau mỗi thao tác
                SaveCurrentState();
            }
        }
        // Chuyển đổi trung tố sang hậu tố
        // Lưu lại trạng thái khi chuyển từ trung tố sang hậu tố

        // Sự kiện xử lý nhập liệu từ bàn phím
        private void TextBoxResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra ký tự hợp lệ (chỉ cho phép số, toán tử và xóa)
            if (!char.IsDigit(e.KeyChar) && !"+-*/().".Contains(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bỏ qua các ký tự không hợp lệ
            }
        }
        // Xử lý khi nhấn nút Undo
        private void buttonUndo_Click(object sender, EventArgs e)
        {
            if (expressionHistory.Count > 0)
            {
                // Lưu lại biểu thức hiện tại vào Redo stack trước khi Undo
                redoHistory.Push(currentInput);

                // Quay lại biểu thức trước đó
                currentInput = expressionHistory.Pop();
                textBoxResult.Text = currentInput; // Cập nhật TextBox
                labelExpression.Text = "";
            }
            else
            {
                // Nếu không có lịch sử Undo, reset lại biểu thức
                currentInput = "";
                textBoxResult.Text = "";
            }
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            if (redoHistory.Count > 0)
            {
                // Lấy biểu thức từ Redo stack
                string redoExpression = redoHistory.Pop();

                // Lưu lại biểu thức hiện tại vào Undo stack trước khi Redo
                expressionHistory.Push(currentInput);

                // Cập nhật biểu thức hiện tại
                currentInput = redoExpression;
                textBoxResult.Text = currentInput;
            }
            else
            {
                // Nếu không có lịch sử Redo, không làm gì
                textBoxResult.Text = "No action to redo";
            }
        }

        private void butoonMenu_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem panel đang ẩn hay đang hiển thị
            if (panelHistory.Visible)
            {
                // Ẩn panel (menu trượt xuống)
                panelHistory.Visible = false;
            }
            else
            {
                // Hiển thị panel (menu trượt lên)
                panelHistory.Visible = true;
                LoadHistory(); // Tải lịch sử vào ListBox mỗi khi mở menu
            }
        }

        // Lưu phép toán vào lịch sử
        private void SaveCalculation(string expression, string result)
        {
            historyManager.SaveCalculation(expression, result); // Lưu phép toán vào history thông qua HistoryManager
        }

        // Tải các phép toán lịch sử vào ListBox
        private void LoadHistory()
        {
            var history = historyManager.LoadHistory(); // Lấy danh sách lịch sử từ HistoryManager
            listBoxHistory.Items.AddRange(history.ToArray()); // Thêm tất cả các phép toán vào ListBox
        }


        // Xử lý khi nhấn đúp vào một phép toán trong lịch sử
        private void listBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxHistory.IndexFromPoint(e.Location); // Lấy chỉ số mục được chọn
            if (index != ListBox.NoMatches)
            {
                var selectedRecord = listBoxHistory.Items[index] as CalculationRecord; // Lấy phép toán đã chọn
                if (selectedRecord != null)
                {
                    // Cập nhật label kết quả với phép toán đã chọn
                    textBoxResult.Text = selectedRecord.Result;
                    labelExpression.Text = selectedRecord.Expression;

                    // Đóng cửa sổ lịch sử
                    panelHistory.Visible = false;

                    // Cập nhật currentInput để người dùng có thể tiếp tục tính toán
                    currentInput = selectedRecord.Result;
                    textBoxResult.Text = currentInput;
                }
            }
        }

        private void buttonComma_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu biểu thức hiện tại không rỗng
            if (!string.IsNullOrEmpty(currentInput))
            {
                // Kiểm tra nếu phần cuối của currentInput chưa có dấu phẩy
                if (!currentInput.Contains(","))
                {
                    // Thêm dấu phẩy vào biểu thức
                    currentInput += ",";
                    textBoxResult.Text = currentInput; // Cập nhật label kết quả
                }
            }
            else
            {
                // Nếu currentInput đang rỗng, chỉ cần thêm dấu phẩy ở đầu
                currentInput = "0,";
                textBoxResult.Text = currentInput;
            }

            // Lưu lại vào expressionHistory sau mỗi thao tác
            SaveCurrentState();
        }
        // Trước khi tính toán, thay dấu phẩy thành dấu chấm
        private string ConvertCommaToDot(string input)
        {
            return input.Replace(',', '.');
        }

    }
}
