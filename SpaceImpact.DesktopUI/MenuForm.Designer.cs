namespace SpaceImpact.DesktopUI
{
    partial class MenuForm
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
            this.btnMenuStart = new System.Windows.Forms.Button();
            this.btnMenuControls = new System.Windows.Forms.Button();
            this.btnMenuAbout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMenuStart
            // 
            this.btnMenuStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMenuStart.Location = new System.Drawing.Point(305, 103);
            this.btnMenuStart.Name = "btnMenuStart";
            this.btnMenuStart.Size = new System.Drawing.Size(172, 54);
            this.btnMenuStart.TabIndex = 0;
            this.btnMenuStart.Text = "Start Game";
            this.btnMenuStart.UseVisualStyleBackColor = true;
            this.btnMenuStart.Click += new System.EventHandler(this.btnMenuStart_Click);
            // 
            // btnMenuControls
            // 
            this.btnMenuControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMenuControls.Location = new System.Drawing.Point(305, 189);
            this.btnMenuControls.Name = "btnMenuControls";
            this.btnMenuControls.Size = new System.Drawing.Size(172, 54);
            this.btnMenuControls.TabIndex = 1;
            this.btnMenuControls.Text = "Controls";
            this.btnMenuControls.UseVisualStyleBackColor = true;
            this.btnMenuControls.Click += new System.EventHandler(this.btnMenuControls_Click);
            // 
            // btnMenuAbout
            // 
            this.btnMenuAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMenuAbout.Location = new System.Drawing.Point(305, 276);
            this.btnMenuAbout.Name = "btnMenuAbout";
            this.btnMenuAbout.Size = new System.Drawing.Size(172, 54);
            this.btnMenuAbout.TabIndex = 2;
            this.btnMenuAbout.Text = "About";
            this.btnMenuAbout.UseVisualStyleBackColor = true;
            this.btnMenuAbout.Click += new System.EventHandler(this.btnMenuAbout_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SpaceImpact.DesktopUI.Properties.Resources.MenuPic1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnMenuAbout);
            this.Controls.Add(this.btnMenuControls);
            this.Controls.Add(this.btnMenuStart);
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMenuStart;
        private System.Windows.Forms.Button btnMenuControls;
        private System.Windows.Forms.Button btnMenuAbout;
    }
}