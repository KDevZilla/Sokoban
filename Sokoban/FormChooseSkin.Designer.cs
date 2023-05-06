namespace Sokoban
{
    partial class FormChooseSkin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboSkin = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(212, 86);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 42);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(116, 86);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(90, 42);
            this.btnChoose.TabIndex = 6;
            this.btnChoose.Text = "Choose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Skin";
            // 
            // comboSkin
            // 
            this.comboSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSkin.FormattingEnabled = true;
            this.comboSkin.Items.AddRange(new object[] {
            "Boxxle",
            "BoxxleColor",
            "BoxWorldClassic",
            "Yoshi"});
            this.comboSkin.Location = new System.Drawing.Point(82, 23);
            this.comboSkin.Name = "comboSkin";
            this.comboSkin.Size = new System.Drawing.Size(220, 29);
            this.comboSkin.TabIndex = 4;
            // 
            // FormChooseSkin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 146);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboSkin);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormChooseSkin";
            this.Text = "Choose Skin";
            this.Load += new System.EventHandler(this.FormChooseSkin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboSkin;
    }
}