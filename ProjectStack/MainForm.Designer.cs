
namespace ProjectStack
{
    partial class MainForm
    {

        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private string currentInput = "";
        private Stack<string> calculationStack = new Stack<string>();

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxResult = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            button0 = new Button();
            buttonAdd = new Button();
            buttonSubtract = new Button();
            buttonMultiply = new Button();
            buttonDivide = new Button();
            buttonEquals = new Button();
            buttonClear = new Button();
            buttonNgoac = new Button();
            buttonNgoac2 = new Button();
            buttonDel = new Button();
            buttonUndo = new Button();
            buttonRedo = new Button();
            buttonPercent = new Button();
            buttonCircum = new Button();
            buttonMenu = new Button();
            buttonComma = new Button();
            displayPanel = new Panel();
            butoonMenu = new Button();
            labelExpression = new Label();
            panelHistory = new Panel();
            labelDeleteAll = new Label();
            labelDeleteHistory = new Label();
            listBoxHistory = new ListBox();
            displayPanel.SuspendLayout();
            panelHistory.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxResult
            // 
            textBoxResult.BorderStyle = BorderStyle.None;
            textBoxResult.Dock = DockStyle.Bottom;
            textBoxResult.Font = new Font("Microsoft Sans Serif", 28F);
            textBoxResult.Location = new Point(0, 149);
            textBoxResult.Name = "textBoxResult";
            textBoxResult.Size = new Size(407, 53);
            textBoxResult.TabIndex = 1;
            textBoxResult.TextAlign = HorizontalAlignment.Right;
            textBoxResult.KeyPress += TextBoxResult_KeyPress;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 14F);
            button1.Location = new Point(15, 298);
            button1.Name = "button1";
            button1.Size = new Size(76, 72);
            button1.TabIndex = 1;
            button1.Text = "1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += NumberButton_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft Sans Serif", 14F);
            button2.Location = new Point(97, 298);
            button2.Name = "button2";
            button2.Size = new Size(76, 72);
            button2.TabIndex = 2;
            button2.Text = "2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += NumberButton_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft Sans Serif", 14F);
            button3.Location = new Point(179, 298);
            button3.Name = "button3";
            button3.Size = new Size(76, 72);
            button3.TabIndex = 3;
            button3.Text = "3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += NumberButton_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Microsoft Sans Serif", 14F);
            button4.Location = new Point(15, 376);
            button4.Name = "button4";
            button4.Size = new Size(76, 72);
            button4.TabIndex = 4;
            button4.Text = "4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += NumberButton_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Microsoft Sans Serif", 14F);
            button5.Location = new Point(97, 376);
            button5.Name = "button5";
            button5.Size = new Size(76, 72);
            button5.TabIndex = 5;
            button5.Text = "5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += NumberButton_Click;
            // 
            // button6
            // 
            button6.Font = new Font("Microsoft Sans Serif", 14F);
            button6.Location = new Point(179, 376);
            button6.Name = "button6";
            button6.Size = new Size(76, 72);
            button6.TabIndex = 6;
            button6.Text = "6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += NumberButton_Click;
            // 
            // button7
            // 
            button7.Font = new Font("Microsoft Sans Serif", 14F);
            button7.Location = new Point(15, 454);
            button7.Name = "button7";
            button7.Size = new Size(76, 72);
            button7.TabIndex = 7;
            button7.Text = "7";
            button7.UseVisualStyleBackColor = true;
            button7.Click += NumberButton_Click;
            // 
            // button8
            // 
            button8.Font = new Font("Microsoft Sans Serif", 14F);
            button8.Location = new Point(97, 454);
            button8.Name = "button8";
            button8.Size = new Size(76, 72);
            button8.TabIndex = 8;
            button8.Text = "8";
            button8.UseVisualStyleBackColor = true;
            button8.Click += NumberButton_Click;
            // 
            // button9
            // 
            button9.Font = new Font("Microsoft Sans Serif", 14F);
            button9.Location = new Point(179, 454);
            button9.Name = "button9";
            button9.Size = new Size(76, 72);
            button9.TabIndex = 9;
            button9.Text = "9";
            button9.UseVisualStyleBackColor = true;
            button9.Click += NumberButton_Click;
            // 
            // button0
            // 
            button0.Font = new Font("Microsoft Sans Serif", 14F);
            button0.Location = new Point(11, 532);
            button0.Name = "button0";
            button0.Size = new Size(162, 72);
            button0.TabIndex = 10;
            button0.Text = "0";
            button0.UseVisualStyleBackColor = true;
            button0.Click += NumberButton_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Font = new Font("Microsoft Sans Serif", 14F);
            buttonAdd.Location = new Point(261, 298);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(76, 72);
            buttonAdd.TabIndex = 11;
            buttonAdd.Text = "+";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += OperatorButton_Click;
            // 
            // buttonSubtract
            // 
            buttonSubtract.Font = new Font("Microsoft Sans Serif", 14F);
            buttonSubtract.Location = new Point(261, 376);
            buttonSubtract.Name = "buttonSubtract";
            buttonSubtract.Size = new Size(76, 72);
            buttonSubtract.TabIndex = 12;
            buttonSubtract.Text = "-";
            buttonSubtract.UseVisualStyleBackColor = true;
            buttonSubtract.Click += OperatorButton_Click;
            // 
            // buttonMultiply
            // 
            buttonMultiply.Font = new Font("Microsoft Sans Serif", 14F);
            buttonMultiply.Location = new Point(261, 454);
            buttonMultiply.Name = "buttonMultiply";
            buttonMultiply.Size = new Size(76, 72);
            buttonMultiply.TabIndex = 13;
            buttonMultiply.Text = "*";
            buttonMultiply.UseVisualStyleBackColor = true;
            buttonMultiply.Click += OperatorButton_Click;
            // 
            // buttonDivide
            // 
            buttonDivide.Font = new Font("Microsoft Sans Serif", 14F);
            buttonDivide.Location = new Point(261, 532);
            buttonDivide.Name = "buttonDivide";
            buttonDivide.Size = new Size(76, 72);
            buttonDivide.TabIndex = 14;
            buttonDivide.Text = "/";
            buttonDivide.UseVisualStyleBackColor = true;
            buttonDivide.Click += OperatorButton_Click;
            // 
            // buttonEquals
            // 
            buttonEquals.Font = new Font("Microsoft Sans Serif", 14F);
            buttonEquals.Location = new Point(179, 532);
            buttonEquals.Name = "buttonEquals";
            buttonEquals.Size = new Size(76, 72);
            buttonEquals.TabIndex = 15;
            buttonEquals.Text = "=";
            buttonEquals.UseVisualStyleBackColor = true;
            buttonEquals.Click += EqualsButton_Click;
            // 
            // buttonClear
            // 
            buttonClear.BackColor = SystemColors.ActiveCaption;
            buttonClear.Font = new Font("Microsoft Sans Serif", 14F);
            buttonClear.Location = new Point(259, 217);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(78, 72);
            buttonClear.TabIndex = 16;
            buttonClear.Text = "C";
            buttonClear.UseVisualStyleBackColor = false;
            buttonClear.Click += ClearButton_Click;
            // 
            // buttonNgoac
            // 
            buttonNgoac.Font = new Font("Microsoft Sans Serif", 14F);
            buttonNgoac.Location = new Point(15, 220);
            buttonNgoac.Name = "buttonNgoac";
            buttonNgoac.Size = new Size(76, 72);
            buttonNgoac.TabIndex = 17;
            buttonNgoac.Text = "(";
            buttonNgoac.UseVisualStyleBackColor = true;
            buttonNgoac.Click += ParenthesisButton_Click;
            // 
            // buttonNgoac2
            // 
            buttonNgoac2.Font = new Font("Microsoft Sans Serif", 14F);
            buttonNgoac2.Location = new Point(97, 220);
            buttonNgoac2.Name = "buttonNgoac2";
            buttonNgoac2.Size = new Size(76, 72);
            buttonNgoac2.TabIndex = 18;
            buttonNgoac2.Text = ")";
            buttonNgoac2.UseVisualStyleBackColor = true;
            buttonNgoac2.Click += ParenthesisButton_Click;
            // 
            // buttonDel
            // 
            buttonDel.BackColor = SystemColors.AppWorkspace;
            buttonDel.Font = new Font("Microsoft Sans Serif", 10F);
            buttonDel.Location = new Point(179, 220);
            buttonDel.Name = "buttonDel";
            buttonDel.Size = new Size(78, 72);
            buttonDel.TabIndex = 19;
            buttonDel.Text = "DEL";
            buttonDel.UseVisualStyleBackColor = false;
            buttonDel.Click += DelButton_Click;
            // 
            // buttonUndo
            // 
            buttonUndo.BackColor = SystemColors.AppWorkspace;
            buttonUndo.Font = new Font("Microsoft Sans Serif", 10F);
            buttonUndo.Location = new Point(343, 217);
            buttonUndo.Name = "buttonUndo";
            buttonUndo.Size = new Size(78, 72);
            buttonUndo.TabIndex = 21;
            buttonUndo.Text = "Undo";
            buttonUndo.UseVisualStyleBackColor = false;
            buttonUndo.Click += buttonUndo_Click;
            // 
            // buttonRedo
            // 
            buttonRedo.BackColor = SystemColors.AppWorkspace;
            buttonRedo.Font = new Font("Microsoft Sans Serif", 10F);
            buttonRedo.Location = new Point(343, 295);
            buttonRedo.Name = "buttonRedo";
            buttonRedo.Size = new Size(78, 72);
            buttonRedo.TabIndex = 22;
            buttonRedo.Text = "Redo";
            buttonRedo.UseVisualStyleBackColor = false;
            buttonRedo.Click += buttonRedo_Click;
            // 
            // buttonPercent
            // 
            buttonPercent.Font = new Font("Microsoft Sans Serif", 14F);
            buttonPercent.Location = new Point(343, 373);
            buttonPercent.Name = "buttonPercent";
            buttonPercent.Size = new Size(76, 72);
            buttonPercent.TabIndex = 23;
            buttonPercent.Text = "%";
            buttonPercent.UseVisualStyleBackColor = true;
            buttonPercent.Click += buttonPercent_Click;
            // 
            // buttonCircum
            // 
            buttonCircum.Font = new Font("Microsoft Sans Serif", 14F);
            buttonCircum.Location = new Point(343, 451);
            buttonCircum.Name = "buttonCircum";
            buttonCircum.Size = new Size(76, 72);
            buttonCircum.TabIndex = 24;
            buttonCircum.Text = "^";
            buttonCircum.UseVisualStyleBackColor = true;
            buttonCircum.Click += OperatorButton_Click;
            // 
            // buttonMenu
            // 
            buttonMenu.BackColor = SystemColors.AppWorkspace;
            buttonMenu.Font = new Font("Microsoft Sans Serif", 10F);
            buttonMenu.Location = new Point(15, 12);
            buttonMenu.Name = "buttonMenu";
            buttonMenu.Size = new Size(34, 24);
            buttonMenu.TabIndex = 25;
            buttonMenu.Text = "iii";
            buttonMenu.UseVisualStyleBackColor = false;
            // 
            // buttonComma
            // 
            buttonComma.Font = new Font("Microsoft Sans Serif", 14F);
            buttonComma.Location = new Point(343, 532);
            buttonComma.Name = "buttonComma";
            buttonComma.Size = new Size(76, 72);
            buttonComma.TabIndex = 26;
            buttonComma.Text = ",";
            buttonComma.UseVisualStyleBackColor = true;
            buttonComma.Click += buttonComma_Click;
            // 
            // displayPanel
            // 
            displayPanel.BackColor = Color.White;
            displayPanel.Controls.Add(butoonMenu);
            displayPanel.Controls.Add(labelExpression);
            displayPanel.Controls.Add(textBoxResult);
            displayPanel.Location = new Point(11, 12);
            displayPanel.Name = "displayPanel";
            displayPanel.Size = new Size(407, 202);
            displayPanel.TabIndex = 0;
            // 
            // butoonMenu
            // 
            butoonMenu.BackColor = Color.Tomato;
            butoonMenu.BackgroundImageLayout = ImageLayout.Stretch;
            butoonMenu.Image = Properties.Resources.list;
            butoonMenu.Location = new Point(4, 3);
            butoonMenu.Name = "butoonMenu";
            butoonMenu.Size = new Size(38, 29);
            butoonMenu.TabIndex = 28;
            butoonMenu.Text = "iii";
            butoonMenu.UseVisualStyleBackColor = false;
            butoonMenu.Click += butoonMenu_Click;
            // 
            // labelExpression
            // 
            labelExpression.Dock = DockStyle.Top;
            labelExpression.Font = new Font("Microsoft Sans Serif", 16F);
            labelExpression.ForeColor = Color.Gray;
            labelExpression.Location = new Point(0, 0);
            labelExpression.Name = "labelExpression";
            labelExpression.Size = new Size(407, 133);
            labelExpression.TabIndex = 1;
            labelExpression.TextAlign = ContentAlignment.BottomRight;
            // 
            // panelHistory
            // 
            panelHistory.BackColor = SystemColors.ControlDarkDark;
            panelHistory.Controls.Add(labelDeleteAll);
            panelHistory.Controls.Add(labelDeleteHistory);
            panelHistory.Controls.Add(listBoxHistory);
            panelHistory.Location = new Point(3, 241);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(418, 363);
            panelHistory.TabIndex = 29;
            // 
            // labelDeleteAll
            // 
            labelDeleteAll.AutoSize = true;
            labelDeleteAll.BackColor = SystemColors.ButtonFace;
            labelDeleteAll.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelDeleteAll.Location = new Point(340, 330);
            labelDeleteAll.Name = "labelDeleteAll";
            labelDeleteAll.Size = new Size(69, 23);
            labelDeleteAll.TabIndex = 31;
            labelDeleteAll.Text = "Xóa hết";
            labelDeleteAll.Click += labelDeleteAll_Click;
            // 
            // labelDeleteHistory
            // 
            labelDeleteHistory.AutoSize = true;
            labelDeleteHistory.BackColor = SystemColors.ButtonFace;
            labelDeleteHistory.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelDeleteHistory.Location = new Point(9, 330);
            labelDeleteHistory.Name = "labelDeleteHistory";
            labelDeleteHistory.Size = new Size(39, 23);
            labelDeleteHistory.TabIndex = 30;
            labelDeleteHistory.Text = "Xóa";
            labelDeleteHistory.Click += labelDeleteHistory_Click;
            // 
            // listBoxHistory
            // 
            listBoxHistory.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listBoxHistory.FormattingEnabled = true;
            listBoxHistory.ItemHeight = 28;
            listBoxHistory.Location = new Point(0, 0);
            listBoxHistory.Name = "listBoxHistory";
            listBoxHistory.Size = new Size(418, 368);
            listBoxHistory.TabIndex = 0;
            // 
            // MainForm
            // 
            BackColor = SystemColors.Window;
            ClientSize = new Size(429, 631);
            Controls.Add(panelHistory);
            Controls.Add(displayPanel);
            Controls.Add(buttonComma);
            Controls.Add(buttonMenu);
            Controls.Add(buttonCircum);
            Controls.Add(buttonPercent);
            Controls.Add(buttonRedo);
            Controls.Add(buttonUndo);
            Controls.Add(buttonDel);
            Controls.Add(buttonClear);
            Controls.Add(buttonEquals);
            Controls.Add(buttonDivide);
            Controls.Add(buttonMultiply);
            Controls.Add(buttonSubtract);
            Controls.Add(buttonAdd);
            Controls.Add(button0);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(buttonNgoac);
            Controls.Add(buttonNgoac2);
            Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "MainForm";
            Text = "Calculator";
            displayPanel.ResumeLayout(false);
            displayPanel.PerformLayout();
            panelHistory.ResumeLayout(false);
            panelHistory.PerformLayout();
            ResumeLayout(false);
        }



        #endregion
        private TextBox textBoxResult;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button0;
        private Button buttonAdd;
        private Button buttonSubtract;
        private Button buttonMultiply;
        private Button buttonDivide;
        private Button buttonEquals;
        private Button buttonClear;
        private Button buttonNgoac2;
        private Button buttonNgoac;
        private Button buttonDel;
        private Button buttonUndo;
        private Button buttonRedo;
        private Button buttonPercent;
        private Button buttonCircum;
        private Button buttonMenu;
        private Button buttonComma;
        private Panel displayPanel;
        private Label labelExpression;
        private Button butoonMenu;
        private Panel panelHistory;
        private ListBox listBoxHistory;
        private Label labelDeleteHistory;
        private Label labelDeleteAll;
    }


    
}
