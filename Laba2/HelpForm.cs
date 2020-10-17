using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba2
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox1.Font = new Font("Times New Roman", 12, FontStyle.Bold); ;
            textBox1.Text += $"Данное приложение предназначено для редактирования изображений  {System.Environment.NewLine}"+
                 $"1. Загружать, редактировать, создавать, сохранять изображения {System.Environment.NewLine}" +
                $"2. Рисовать с помощью мыши {System.Environment.NewLine} (При нажатии левой кнопки мыши  и её перемещении отображается кривая движения указателя мыши. При нажатии правой кнопки мыши появляется стирательная резинка. При двойном щелчке - стирается весь рисунок).{System.Environment.NewLine}" +
                $"3. Задавать цвет, толщину и стиль линии";
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {

        }
    }
}
