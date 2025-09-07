namespace RubikCubeFinal
{
    partial class CubeEncoder
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CubeEncoder));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.textMessageBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_求解 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(878, 567);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(12, 582);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(600, 21);
            this.txtCode.TabIndex = 1;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(618, 580);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(80, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "生成编码";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(704, 580);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "应用编码";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 567);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "魔方编码";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(790, 582);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // textMessageBox
            // 
            this.textMessageBox.BackColor = System.Drawing.SystemColors.Menu;
            this.textMessageBox.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textMessageBox.Location = new System.Drawing.Point(419, 367);
            this.textMessageBox.Multiline = true;
            this.textMessageBox.Name = "textMessageBox";
            this.textMessageBox.ReadOnly = true;
            this.textMessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textMessageBox.Size = new System.Drawing.Size(459, 197);
            this.textMessageBox.TabIndex = 9;
            this.textMessageBox.Tag = "1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(618, 619);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(80, 21);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "21";
            // 
            // btn_求解
            // 
            this.btn_求解.Location = new System.Drawing.Point(704, 619);
            this.btn_求解.Name = "btn_求解";
            this.btn_求解.Size = new System.Drawing.Size(80, 23);
            this.btn_求解.TabIndex = 10;
            this.btn_求解.Text = "求解";
            this.btn_求解.UseVisualStyleBackColor = true;
            this.btn_求解.Click += new System.EventHandler(this.btn_求解_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(791, 619);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "help？";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(511, 624);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "（建议21）最长解";
            // 
            // CubeEncoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 666);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_求解);
            this.Controls.Add(this.textMessageBox);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CubeEncoder";
            this.Text = "魔方编码编辑器还原工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox textMessageBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_求解;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    }
}
    