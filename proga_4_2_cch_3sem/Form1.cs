using System;
using System.Drawing;
using System.Windows.Forms;

namespace proga_4_2_cch_3sem
{
    public partial class Form1 : Form
    {
        private Point? selectedCell;
        const int n = 5;
        const double x = 5;

        public Form1()
        {
            InitializeComponent();

            tableLayoutPanel1.RowCount = n;
            tableLayoutPanel1.ColumnCount = n;
            tableLayoutPanel1.AutoSize = false;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));

            FillTable(tableLayoutPanel1);

            tableLayoutPanel1.AutoScroll = true;
            Controls.Add(tableLayoutPanel1);

            tableLayoutPanel1.MouseClick += tableLayoutPanel1_MouseDown;

            button1.Click += button1_Click;
            button2.Click += button2_Click; // Обработчик нажатия кнопки "Вычислить"

            tableLayoutPanel2.RowCount = n;
            tableLayoutPanel2.ColumnCount = n;
            tableLayoutPanel2.AutoSize = false;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            Controls.Add(tableLayoutPanel2); // Добавление таблицы Y на форму
        }

        private void FillTable(TableLayoutPanel table)
        {
            for (int j = 0; j < table.ColumnCount; j++)
            {
                for (int i = 0; i < table.RowCount; i++)
                {
                    double value = Math.Pow(x, j + 1);
                    TextBox textBox = new TextBox();
                    textBox.Text = value.ToString();
                    textBox.ReadOnly = true;
                    textBox.MouseDown += textBox_MouseDown;
                    table.Controls.Add(textBox, i, j);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedCell.HasValue)
            {
                int column = selectedCell.Value.X;
                int row = selectedCell.Value.Y;
                Control control = tableLayoutPanel1.GetControlFromPosition(column, row);

                if (control is TextBox textBox)
                {
                    if (double.TryParse(textEdit1.Text, out double value))
                    {
                        textBox.Text = value.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Введенное значение не является числом.", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите ячейку.", "Ячейка не выбрана", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получаем ссылку на таблицу X
            TableLayoutPanel xTable = tableLayoutPanel1;

            // Создаем таблицу Y
            TableLayoutPanel yTable = tableLayoutPanel2;
            yTable.RowCount = xTable.RowCount;
            yTable.ColumnCount = xTable.ColumnCount;
            yTable.Controls.Clear();
            // Переставляем столбцы местами
            for (int j = 0; j < xTable.ColumnCount; ++j)
            {
                
                for (int i = 0; i < xTable.RowCount; ++i)
                {
                    int newJ = (j + 1) % xTable.ColumnCount;
                    Control control = xTable.GetControlFromPosition(j, i);
                    if (control is TextBox textBox)
                    {
                        TextBox newTextBox = new TextBox();
                        newTextBox.Text = textBox.Text;
                        newTextBox.ReadOnly = true;
                        yTable.Controls.Add(newTextBox, newJ, i);
                    }
                }
            }

            // Обновляем геометрию таблицы Y
            yTable.Update();
        }


        private void textBox_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textEdit1.Text = textBox.Text;
            int column = tableLayoutPanel1.GetColumn(textBox);
            int row = tableLayoutPanel1.GetRow(textBox);
            selectedCell = new Point(column, row);
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}