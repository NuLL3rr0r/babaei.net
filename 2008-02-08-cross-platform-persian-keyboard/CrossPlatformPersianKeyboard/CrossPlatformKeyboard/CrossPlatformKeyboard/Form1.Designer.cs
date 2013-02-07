namespace CrossPlatformKeyboard
{
    partial class Form1
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
            this.lblPersian = new System.Windows.Forms.Label();
            this.lblEnglish = new System.Windows.Forms.Label();
            this.txtPersian = new System.Windows.Forms.TextBox();
            this.txtEnglish = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPersian
            // 
            this.lblPersian.AutoSize = true;
            this.lblPersian.Location = new System.Drawing.Point(12, 44);
            this.lblPersian.Name = "lblPersian";
            this.lblPersian.Size = new System.Drawing.Size(42, 13);
            this.lblPersian.TabIndex = 0;
            this.lblPersian.Text = "Persian";
            // 
            // lblEnglish
            // 
            this.lblEnglish.AutoSize = true;
            this.lblEnglish.Location = new System.Drawing.Point(12, 70);
            this.lblEnglish.Name = "lblEnglish";
            this.lblEnglish.Size = new System.Drawing.Size(41, 13);
            this.lblEnglish.TabIndex = 0;
            this.lblEnglish.Text = "English";
            // 
            // txtPersian
            // 
            this.txtPersian.Location = new System.Drawing.Point(59, 41);
            this.txtPersian.Name = "txtPersian";
            this.txtPersian.Size = new System.Drawing.Size(301, 20);
            this.txtPersian.TabIndex = 0;
            this.txtPersian.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtPersian_PreviewKeyDown);
            this.txtPersian.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersian_KeyPress);
            // 
            // txtEnglish
            // 
            this.txtEnglish.Location = new System.Drawing.Point(59, 67);
            this.txtEnglish.Name = "txtEnglish";
            this.txtEnglish.Size = new System.Drawing.Size(301, 20);
            this.txtEnglish.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(50, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(272, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Just type a simple text below without changing language";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 102);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtEnglish);
            this.Controls.Add(this.txtPersian);
            this.Controls.Add(this.lblEnglish);
            this.Controls.Add(this.lblPersian);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Cross-Platform Persian Keyboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPersian;
        private System.Windows.Forms.Label lblEnglish;
        private System.Windows.Forms.TextBox txtPersian;
        private System.Windows.Forms.TextBox txtEnglish;
        private System.Windows.Forms.Label lblTitle;
    }
}

