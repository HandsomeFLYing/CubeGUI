using System;
using System.Drawing;
using System.Text;

namespace RubikCubeFinal
{
    /**
     * <pre>
     * 多维数据集的面位置的名称
     * 
     *             |************|
     *             |*U1**U2**U3*|
     *             |************|
     *             |*U4**U5**U6*|
     *             |************|
     *             |*U7**U8**U9*|
     *             |************|
     * ************|************|************|************|
     * *L1**L2**L3*|*F1**F2**F3*|*R1**R2**F3*|*B1**B2**B3*|
     * ************|************|************|************|
     * *L4**L5**L6*|*F4**F5**F6*|*R4**R5**R6*|*B4**B5**B6*|
     * ************|************|************|************|
     * *L7**L8**L9*|*F7**F8**F9*|*R7**R8**R9*|*B7**B8**B9*|
     * ************|************|************|************|
     *             |************|
     *             |*D1**D2**D3*|
     *             |************|
     *             |*D4**D5**D6*|
     *             |************|
     *             |*D7**D8**D9*|
     *             |************|
     *             
     * </pre>
     * U白    R红   F绿   D黄   L橙   B蓝 
     */
    //##############################################################
    //          编码处理器，负责编码生成和解析
    //##############################################################
    /// <summary>
    /// 编码处理器，负责编码生成和解析
    /// </summary>
    public class CodeProcessor
    {
        private readonly RubikCube _cube;

        // 编码生成顺序：U(1-9)→F(10-18)→R(19-27)→L(28-36)→B(37-45)→D(46-54)
        private readonly int[] _codeOrder = {
            RubikCube.FACE_U,   // 0-8
            RubikCube.FACE_R,   // 9-17
            RubikCube.FACE_F,   // 18-26
            RubikCube.FACE_D,   // 27-35
            RubikCube.FACE_L,   // 36-44
            RubikCube.FACE_B    // 45-53
        };

        public CodeProcessor(RubikCube cube)
        {
            _cube = cube ?? throw new ArgumentNullException(nameof(cube));
        }

        /// <summary>
        /// 生成54位编码字符串
        /// </summary>
        public string GenerateCode()
        {
            StringBuilder sb = new StringBuilder(54);

            foreach (int face in _codeOrder)
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        Color color = _cube.GetColor(face, row, col);
                        sb.Append(_cube.GetCodeFromColor(color));
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 验证编码有效性
        /// </summary>
        public bool ValidateCode(string code)
        {
            if (string.IsNullOrEmpty(code) || code.Length != 54)
                return false;

            foreach (char c in code)
            {
                bool isValid = false;
                foreach (char validChar in _cube.ColorCodes)
                {
                    if (c == validChar)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (!isValid)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 应用编码到魔方
        /// </summary>
        public void ApplyCode(string code)
        {
            if (!ValidateCode(code))
                throw new ArgumentException("无效的魔方编码");

            int index = 0;
            foreach (int face in _codeOrder)
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        char codeChar = code[index];
                        Color color = _cube.GetColorFromCode(codeChar);
                        _cube.SetColor(face, row, col, color);
                        index++;
                    }
                }
            }
        }
    }

    //##############################################################
    //          魔方绘制器，负责所有图形绘制和点击区域判断
    //##############################################################
    /// <summary>
    /// 魔方绘制器，负责所有图形绘制和点击区域判断
    /// </summary>
    public class EncoderCubePainter
    {
        // 魔方面位置和大小（坐标基于800x500的PictureBox）
        private readonly Rectangle[] _faceRects = {
            new Rectangle(250, 30, 150, 150),   // U面（上）
            new Rectangle(420, 200, 150, 150),  // R面（右）
            new Rectangle(250, 200, 150, 150),  // F面（前）
            new Rectangle(250, 370, 150, 150),  // D面（下）
            new Rectangle(80, 200, 150, 150),   // L面（左）
            new Rectangle(590, 200, 150, 150)   // B面（后）
        };

        // 面标签位置
        private readonly Point[] _labelPositions = {
            new Point(325, 10),   // U
            new Point(495, 180),  // R
            new Point(325, 180),  // F
            new Point(325, 530),  // D（超出PictureBox范围，不显示）
            new Point(155, 180),  // L
            new Point(665, 180)   // B
        };

        private readonly string[] _faceLabels = { "U（上）", "R（右）", "F（前）", "D（下）", "L（左）", "B（后）" };

        // 右侧颜色选择区位置（两排三列）
        private readonly Rectangle[] _colorRects = new Rectangle[6];

        public EncoderCubePainter()
        {
            InitializeColorRects();
        }

        /// <summary>
        /// 初始化颜色选择区位置
        /// </summary>
        private void InitializeColorRects()
        {
            int x = 680;   // 右侧位置
            int y = 30;    // 顶部起始位置
            int size = 40; // 颜色块大小
            int gap = 10;  // 间距

            // 第一排（U, R, F）
            _colorRects[0] = new Rectangle(x, y, size, size);
            _colorRects[1] = new Rectangle(x + size + gap, y, size, size);
            _colorRects[2] = new Rectangle(x + 2 * (size + gap), y, size, size);

            // 第二排（D, L, B）
            _colorRects[3] = new Rectangle(x, y + size + gap, size, size);
            _colorRects[4] = new Rectangle(x + size + gap, y + size + gap, size, size);
            _colorRects[5] = new Rectangle(x + 2 * (size + gap), y + size + gap, size, size);
        }

        /// <summary>
        /// 绘制完整魔方
        /// </summary>
        public void DrawCube(Graphics g, RubikCube cube)
        {
            // 绘制六个面
            for (int face = 0; face < 6; face++)
            {
                DrawFace(g, cube, face, _faceRects[face]);
            }

            // 绘制面标签
            using (Font font = new Font("宋体", 9f, FontStyle.Bold))
            {
                for (int i = 0; i < 6; i++)
                {
                    g.DrawString(_faceLabels[i], font, Brushes.Black, _labelPositions[i]);
                }
            }
        }

        /// <summary>
        /// 绘制单个面
        /// </summary>
        private void DrawFace(Graphics g, RubikCube cube, int face, Rectangle rect)
        {
            int cellSize = rect.Width / 3;
            int spacing = 1; // 格子间距

            // 绘制面背景
            g.FillRectangle(Brushes.LightGray, rect);
            g.DrawRectangle(Pens.Black, rect);

            // 绘制3x3格子
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // 计算格子位置
                    int x = rect.X + col * cellSize + spacing;
                    int y = rect.Y + row * cellSize + spacing;
                    int width = cellSize - 2 * spacing;
                    int height = cellSize - 2 * spacing;

                    // 绘制格子颜色
                    using (SolidBrush brush = new SolidBrush(cube.GetColor(face, row, col)))
                    {
                        g.FillRectangle(brush, x, y, width, height);
                    }

                    // 绘制格子边框
                    g.DrawRectangle(Pens.Black, x, y, width, height);
                }
            }
        }

        /// <summary>
        /// 绘制颜色选择区
        /// </summary>
        public void DrawColorSelector(Graphics g, RubikCube cube, int selectedIndex)
        {
            // 绘制标题
            using (Font font = new Font("宋体", 9f, FontStyle.Bold))
            {
                g.DrawString("颜色选择", font, Brushes.Black, 680, 10);
            }

            // 绘制颜色块
            for (int i = 0; i < 6; i++)
            {
                // 填充颜色
                using (SolidBrush brush = new SolidBrush(cube.Colors[i]))
                {
                    g.FillRectangle(brush, _colorRects[i]);
                }

                // 绘制边框
                g.DrawRectangle(Pens.Black, _colorRects[i]);

                // 选中状态（加粗边框）
                if (i == selectedIndex)
                {
                    g.DrawRectangle(new Pen(Color.Black, 2), 
                        _colorRects[i].X - 1, _colorRects[i].Y - 1,
                        _colorRects[i].Width + 2, _colorRects[i].Height + 2);
                }

                // 绘制编码字符
                using (Font font = new Font("Arial", 10f, FontStyle.Bold))
                {
                    g.DrawString(cube.ColorCodes[i].ToString(), font, 
                        Brushes.Black, 
                        _colorRects[i].X + 15, 
                        _colorRects[i].Y + 10);
                }
            }
        }

        /// <summary>
        /// 检查是否点击了颜色选择区
        /// </summary>
        public int CheckColorSelectorClick(Point point)
        {
            for (int i = 0; i < 6; i++)
            {
                if (_colorRects[i].Contains(point))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 检查是否点击了魔方区域
        /// </summary>
        public (int face, int row, int col) CheckCubeClick(Point point)
        {
            for (int face = 0; face < 6; face++)
            {
                if (_faceRects[face].Contains(point))
                {
                    // 计算点击的行列
                    int cellSize = _faceRects[face].Width / 3;
                    int row = (point.Y - _faceRects[face].Y) / cellSize;
                    int col = (point.X - _faceRects[face].X) / cellSize;

                    // 验证行列有效性
                    if (row >= 0 && row < 3 && col >= 0 && col < 3)
                    {
                        return (face, row, col);
                    }
                }
            }
            return (-1, -1, -1); // 未点击有效区域
        }
    }
    //##############################################################
    //          魔方数据模型，管理颜色数据和映射关系
    //##############################################################
    /// <summary>
    /// 魔方数据模型，管理颜色数据和映射关系
    /// </summary>
    public class RubikCube
    {
        // 面索引常量
        public const int FACE_U = 0;  // 上
        public const int FACE_R = 1;  // 右
        public const int FACE_F = 2;  // 前
        public const int FACE_D = 3;  // 下
        public const int FACE_L = 4;  // 左
        public const int FACE_B = 5;  // 后

        // 颜色定义（与编码对应）
        public readonly Color[] Colors = {
            Color.White,    // U - 上
            Color.Red,      // R - 右
            Color.Green,    // F - 前
            Color.Yellow,   // D - 下
            Color.Orange,   // L - 左
            Color.Blue      // B - 后
        };

        public readonly char[] ColorCodes = { 'U', 'R', 'F', 'D', 'L', 'B' };

        // 魔方颜色数据 [面, 行, 列]
        private Color[,,] _cubeData = new Color[6, 3, 3];

        public RubikCube()
        {
            InitializeDefaultColors();
        }

        /// <summary>
        /// 初始化魔方默认颜色
        /// </summary>
        public void InitializeDefaultColors()
        {
            for (int face = 0; face < 6; face++)
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        _cubeData[face, row, col] = Colors[face];
                    }
                }
            }
        }

        /// <summary>
        /// 获取指定位置的颜色
        /// </summary>
        public Color GetColor(int face, int row, int col)
        {
            if (IsValidPosition(face, row, col))
                return _cubeData[face, row, col];
            throw new ArgumentOutOfRangeException("无效的魔方位置");
        }

        /// <summary>
        /// 设置指定位置的颜色
        /// </summary>
        public void SetColor(int face, int row, int col, Color color)
        {
            if (IsValidPosition(face, row, col))
                _cubeData[face, row, col] = color;
            else
                throw new ArgumentOutOfRangeException("无效的魔方位置");
        }

        /// <summary>
        /// 验证位置是否有效
        /// </summary>
        private bool IsValidPosition(int face, int row, int col)
        {
            return face >= 0 && face < 6 &&
                   row >= 0 && row < 3 &&
                   col >= 0 && col < 3;
        }

        /// <summary>
        /// 根据颜色获取编码字符
        /// </summary>
        public char GetCodeFromColor(Color color)
        {
            for (int i = 0; i < Colors.Length; i++)
            {
                if (Colors[i] == color)
                    return ColorCodes[i];
            }
            return ColorCodes[0]; // 默认返回U
        }

        /// <summary>
        /// 根据编码字符获取颜色
        /// </summary>
        public Color GetColorFromCode(char code)
        {
            for (int i = 0; i < ColorCodes.Length; i++)
            {
                if (ColorCodes[i] == code)
                    return Colors[i];
            }
            return Colors[0]; // 默认返回白色
        }
    }
}
    