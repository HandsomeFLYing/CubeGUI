using System;
using System.Drawing;
using System.Windows.Forms;
using Kociemba;

namespace RubikCubeFinal
{
    public partial class CubeEncoder : Form
    {
        // 核心模块
        private RubikCube _cube;
        private EncoderCubePainter _painter;
        private CodeProcessor _codeProcessor;
        
        // 当前选中的颜色索引
        private int _selectedColorIndex = 0;

        public CubeEncoder()
        {
            InitializeComponent();
            InitializeModules();
            
            
        }

        private void InitializeModules()
        {
            // 初始化核心模块
            _cube = new RubikCube();
            _painter = new EncoderCubePainter();
            _codeProcessor = new CodeProcessor(_cube);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 初始化界面
            pictureBox1.BackColor = Color.WhiteSmoke;
        }

        // 绘制魔方和颜色选择区
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // 绘制魔方六面
            _painter.DrawCube(e.Graphics, _cube);
            
            // 绘制右侧颜色选择区（两排三列）
            _painter.DrawColorSelector(e.Graphics, _cube, _selectedColorIndex);
        }

        // 处理PictureBox点击事件（选择颜色和修改魔方）
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // 检查是否点击了颜色选择区
            int colorIndex = _painter.CheckColorSelectorClick(e.Location);
            if (colorIndex != -1)
            {
                _selectedColorIndex = colorIndex;
                pictureBox1.Invalidate();
                return;
            }

            // 检查是否点击了魔方区域
            var (face, row, col) = _painter.CheckCubeClick(e.Location);
            if (face != -1 && row != -1 && col != -1)
            {
                // 修改魔方颜色
                _cube.SetColor(face, row, col, _cube.Colors[_selectedColorIndex]);
                pictureBox1.Invalidate();
                string code = _codeProcessor.GenerateCode();
                txtCode.Text = code;
            }
        }

        // 生成编码按钮点击事件
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string code = _codeProcessor.GenerateCode();
                txtCode.Text = code;
                MessageBox.Show("编码生成成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成编码失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 应用编码按钮点击事件
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtCode.Text.Trim();
                if (_codeProcessor.ValidateCode(code))
                {
                    _codeProcessor.ApplyCode(code);
                    pictureBox1.Invalidate();
                    MessageBox.Show("编码应用成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("编码无效！必须是54位包含U、R、F、D、L、B各9个的字符串", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("应用编码失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                string code = "UUUUUUUUURRRRRRRRRFFFFFFFFFDDDDDDDDDLLLLLLLLLBBBBBBBBB";
                if (_codeProcessor.ValidateCode(code))
                {
                    _codeProcessor.ApplyCode(code);
                    pictureBox1.Invalidate();
                    MessageBox.Show("重置应用成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("重置编码无效！必须是54位包含U、R、F、D、L、B的字符串", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("重置失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_求解_Click(object sender, EventArgs e)
        {
            string code = _codeProcessor.GenerateCode();
            txtCode.Text = code;
            string info = "";
            int resultSU;

            // 尝试转换，返回值为 true 表示成功
            if (int.TryParse(textBox1.Text, out resultSU))
            {
                Console.WriteLine($"有效值：{resultSU}\r\n");
                //求得解
                var result = Kociemba.solution(code, out info, resultSU);
                //MessageBox.Show("求解步骤：" + result + "\r\n\r\n用时：" + info, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textMessageBox.Text = "求解步骤：\r\n" + result + "\r\n\r\n用时反馈：\r\n" + info;
            }
            else
            {
                MessageBox.Show("最长步骤限制失败：输入了不是有效的整数\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
         "* 错误 1：每种颜色 并不完全有一个 正确数量魔方块或魔方编码\r\n" +
         "* 错误 2：并非所有 12 条边都恰好存在一次\r\n" +
         "* 错误 3：翻转错误：必须翻 转一条边\r\n" +
         "* 错误 4：并非所有角都恰好存在一次\r\n" +
         "* 错误 5：扭角错误：必须扭一个角\r\n" +
         "* 错误 6：奇偶校验错误：必须交换 两个角或两个边\r\n" +
         "* 错误 7：给定的 <最长解> 不存在解决方案\r\n" +
         "* 错误 8：超时，在给定时间内没有解决方案\r\n", "错误代码说明"
         );
        }
    }
}

    
