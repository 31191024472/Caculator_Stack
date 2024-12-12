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

                // Loại bỏ khoảng trắng không cần thiết
                currentInput = currentInput.Replace(" ", "");

                // Kiểm tra nếu biểu thức có vấn đề như thiếu số sau dấu toán tử
                if (currentInput.EndsWith("+") || currentInput.EndsWith("-") ||
                    currentInput.EndsWith("*") || currentInput.EndsWith("/"))
                {
                    textBoxResult.Text = "Error"; // Thông báo lỗi nếu biểu thức không hợp lệ
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

                // Kiểm tra kết quả
                if (double.IsNaN(result) || double.IsInfinity(result))
                {
                    textBoxResult.Text = "Error"; // Nếu kết quả không hợp lệ, hiển thị "Error"
                }
                else
                {
                    // Hiển thị kết quả (to, rõ ở dưới)
                    textBoxResult.Text = result.ToString();
                    currentInput = result.ToString(); // Cập nhật biểu thức hiện tại
                }

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

        private void TextBoxResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Nếu nhấn các ký tự không hợp lệ, bỏ qua chúng
            if (!char.IsDigit(e.KeyChar) && !"+-*/().^% ".Contains(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Không cho phép ký tự không hợp lệ
                return;
            }

            // Nếu nhấn dấu "=" hoặc "Enter", thực hiện phép tính
            if (e.KeyChar == '=' || e.KeyChar == (char)Keys.Enter)
            {
                EqualsButton_Click(sender, e);
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
            historyManager.SaveCalculation(labelExpression.Text, textBoxResult.Text); // Lưu phép toán vào history thông qua HistoryManager
        }

        // Tải các phép toán lịch sử vào ListBox
        private void LoadHistory()
        {
            // Lấy danh sách lịch sử từ HistoryManager
            var history = historyManager.LoadHistory();

            // Xóa tất cả các mục trong listBox trước khi tải mới
            listBoxHistory.Items.Clear();

            // Thêm từng phép toán từ lịch sử vào listBox
            foreach (var record in history)
            {
                listBoxHistory.Items.Add(record.ToString());
            }
        }


        // Xử lý khi nhấn đúp vào một phép toán trong lịch sử
        private void listBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxHistory.IndexFromPoint(e.Location); // Lấy chỉ số mục được chọn
            if (index != ListBox.NoMatches)
            {
                // Lấy phép toán đã chọn từ danh sách trong HistoryManager
                var history = historyManager.LoadHistory();
                var selectedRecord = history[index];

                if (selectedRecord != null)
                {
                    // Cập nhật label kết quả với phép toán đã chọn
                    textBoxResult.Text = selectedRecord.Result;
                    labelExpression.Text = selectedRecord.Expression;

                    // Đóng cửa sổ lịch sử
                    panelHistory.Visible = false;

                    // Cập nhật currentInput để người dùng có thể tiếp tục tính toán
                    currentInput = selectedRecord.Result;
                }
            }
        }


        private void buttonComma_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                // Lấy số cuối cùng trong biểu thức
                string[] tokens = currentInput.Split(' ');
                string lastNumber = tokens[^1];

                // Kiểm tra nếu số cuối chưa có dấu phẩy
                if (!lastNumber.Contains(","))
                {
                    tokens[^1] += ",";
                    currentInput = string.Join(" ", tokens);

                    // Cập nhật hiển thị
                    textBoxResult.Text = currentInput;
                }
            }
            else
            {
                // Nếu currentInput rỗng, bắt đầu với "0,"
                currentInput = "0,";
                textBoxResult.Text = currentInput;
            }
        }

        // Trước khi tính toán, thay dấu phẩy thành dấu chấm
        private string ConvertCommaToDot(string input)
        {
            return input.Replace(',', '.');
        }

        private void buttonPercent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                // Kiểm tra nếu số hiện tại đã có ký tự '%'
                if (!currentInput.EndsWith("%"))
                {
                    currentInput += "%"; // Thêm ký tự '%'
                    textBoxResult.Text = currentInput; // Cập nhật hiển thị
                }
            }
        }

        private Stack<string> GetCalculationHistory()
        {
            return calculationHistory;
        }

        private void labelDeleteAll_Click(object sender, EventArgs e)
        {

            // Xác nhận người dùng muốn xóa
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa toàn bộ lịch sử không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (dialogResult == DialogResult.Yes)
            {
                historyManager.ClearHistory();  // Gọi phương thức xóa lịch sử
                listBoxHistory.Items.Clear();   // Xóa các mục trong ListBox hiển thị lịch sử
                MessageBox.Show("Đã xóa toàn bộ lịch sử!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void labelDeleteHistory_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có mục nào được chọn trong ListBox hay không
            if (listBoxHistory.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một phép tính để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy biểu thức của phép tính từ mục được chọn
            string selectedExpression = listBoxHistory.SelectedItem.ToString();
            string expression = selectedExpression.Split('=')[0].Trim(); // Lấy phần biểu thức trước dấu '='

            // Xóa phép tính khỏi lịch sử
            bool isRemoved = historyManager.RemoveRecord(expression);

            if (isRemoved)
            {
                // Cập nhật lại giao diện ListBox
                listBoxHistory.Items.Remove(listBoxHistory.SelectedItem);
                MessageBox.Show("Đã xóa phép tính khỏi lịch sử!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không tìm thấy phép tính để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
