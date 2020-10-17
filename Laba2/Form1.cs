using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Laba2
{
    
    public partial class Form1 : Form
    {
        private bool _dragging;
        private bool _eraser;
        Point lastPoint = Point.Empty;
        Bitmap bitmap;
        Graphics g;
        Color clr;
        DashStyle dashStyle;
        int count;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "0";
            label2.Text = "0";
            label3.Text = "X:";
            label4.Text = "Y:";
             _dragging= false;
            clr = Color.Black;
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 10;
            trackBar1.Value = 5;
            dashStyle = DashStyle.Dash;

            Dictionary<DashStyle,string> comboSource = new Dictionary<DashStyle, string>();
            comboSource.Add(DashStyle.Solid,"Solid");
            comboSource.Add(DashStyle.Dot,"Dot");
            comboSource.Add(DashStyle.DashDot,"DashDot");
            comboBox1.DataSource = new BindingSource(comboSource, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void penToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void styleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void fdsfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dashStyle = DashStyle.Solid;
            dotToolStripMenuItem.CheckState = CheckState.Unchecked;
            dashDotToolStripMenuItem.CheckState = CheckState.Unchecked;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Load("gwkdy7ys.jpg");
            bitmap = new Bitmap(pictureBox1.Image);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Create a new Bitmap object from the picture file on disk,
                    // and assign that to the PictureBox.Image property
                    pictureBox1.Image = new Bitmap(dlg.FileName);

                    bitmap = new Bitmap(dlg.FileName);
                }


                

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //вместо pictureBox1 укажите свой pictureBox

            if (pictureBox1.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                using (SaveFileDialog savedialog = new SaveFileDialog())
                {
                    savedialog.Title = "Сохранить картинку как...";
                    //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                    savedialog.OverwritePrompt = true;
                    //отображать ли предупреждение, если пользователь указывает несуществующий путь
                    savedialog.CheckPathExists = true;
                    //список форматов файла, отображаемый в поле "Тип файла"
                    savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                    //отображается ли кнопка "Справка" в диалоговом окне
                    savedialog.ShowHelp = true;
                    if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                    {
                        try
                        {
                            pictureBox1.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
           
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (_dragging==true&& pictureBox1.Image != null)
            {
              if (count>= 20)
                    {
                        Bitmap bmp = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
                        var pen = new Pen(clr, trackBar1.Value);
                        pen.DashStyle = dashStyle;
                        g.DrawLine(pen, lastPoint, e.Location);
                        pictureBox1.Invalidate();//refreshes the picturebox
                    lastPoint = e.Location;
                    count = 0;
                }
                count++;
                

                label1.Text = e.X.ToString();
                label2.Text = e.Y.ToString();

               
                
            }
            else
            {
                if (_eraser == true && pictureBox1.Image != null)
                {
                    try
                    {
                        Bitmap bmp = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
                        g.DrawLine(new Pen(bitmap.GetPixel(e.X, e.Y), trackBar1.Value), lastPoint, e.Location);
                        pictureBox1.Invalidate();//refreshes the picturebox
                        lastPoint = e.Location;
                    }
                    catch
                    {

                    }

                }
            }
                        
            
                
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && pictureBox1.Image != null)
            {
                lastPoint = e.Location;
                _dragging = true;
                label1.Text = e.X.ToString();
                label2.Text = e.Y.ToString();
                
                g = Graphics.FromImage(pictureBox1.Image);
            }
            if (e.Button==MouseButtons.Right && pictureBox1.Image != null)
            {
                lastPoint = e.Location;
                _eraser = true;
                label1.Text = e.X.ToString();
                label2.Text = e.Y.ToString();
                Cursor.Current = new Cursor(Application.StartupPath + "\\Cursor1.cur");
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && pictureBox1.Image != null)
            {
                _dragging = false;
                label1.Text = e.X.ToString();
                label2.Text = e.Y.ToString();
                lastPoint = Point.Empty;
                
            }
            else
            {
                if (e.Button == MouseButtons.Right && pictureBox1.Image != null)
                {
                    _eraser = false;
                    label1.Text = e.X.ToString();
                    label2.Text = e.Y.ToString();
                    lastPoint = Point.Empty;
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right && pictureBox1.Image != null)
            {
                g.Clear(Color.Transparent);
                pictureBox1.Image = new Bitmap(bitmap);  
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                g.Clear(Color.Transparent);
                pictureBox1.Image = new Bitmap(bitmap);
            }

        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clr=colorDialog1.Color;
            }
              

        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dashStyle = DashStyle.Dot;
            SolidToolStripMenuItem.CheckState= CheckState.Unchecked;
            dashDotToolStripMenuItem.CheckState = CheckState.Unchecked;
        }

        private void dashDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dashStyle = DashStyle.DashDot;
            SolidToolStripMenuItem.CheckState = CheckState.Unchecked;
            dotToolStripMenuItem.CheckState = CheckState.Unchecked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dashStyle= ((KeyValuePair<DashStyle,string>)comboBox1.SelectedItem).Key;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender,e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            aboutToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            colorToolStripMenuItem_Click(sender,e);
        }
    }
}
