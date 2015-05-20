namespace SpaceImpact.DesktopUI
{
    partial class SpaceImpact
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
            this.plGameSpace = new System.Windows.Forms.Panel();
            this.bottom_border = new System.Windows.Forms.PictureBox();
            this.top_border = new System.Windows.Forms.PictureBox();
            this.lblHeroLife = new System.Windows.Forms.Label();
            this.lblBossLife = new System.Windows.Forms.Label();
            this.lblHeroLifeValue = new System.Windows.Forms.Label();
            this.lblBossLifeValue = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblScoreValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bottom_border)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.top_border)).BeginInit();
            this.SuspendLayout();
            // 
            // plGameSpace
            // 
            this.plGameSpace.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.plGameSpace.Location = new System.Drawing.Point(0, 70);
            this.plGameSpace.Name = "plGameSpace";
            this.plGameSpace.Size = new System.Drawing.Size(1107, 500);
            this.plGameSpace.TabIndex = 0;
            // 
            // bottom_border
            // 
            this.bottom_border.Image = global::SpaceImpact.DesktopUI.Properties.Resources.bottom;
            this.bottom_border.Location = new System.Drawing.Point(0, 568);
            this.bottom_border.Name = "bottom_border";
            this.bottom_border.Size = new System.Drawing.Size(1107, 94);
            this.bottom_border.TabIndex = 2;
            this.bottom_border.TabStop = false;
            // 
            // top_border
            // 
            this.top_border.Image = global::SpaceImpact.DesktopUI.Properties.Resources.top;
            this.top_border.Location = new System.Drawing.Point(0, -2);
            this.top_border.Name = "top_border";
            this.top_border.Size = new System.Drawing.Size(1107, 100);
            this.top_border.TabIndex = 1;
            this.top_border.TabStop = false;
            // 
            // lblHeroLife
            // 
            this.lblHeroLife.AutoSize = true;
            this.lblHeroLife.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHeroLife.Location = new System.Drawing.Point(851, 21);
            this.lblHeroLife.Name = "lblHeroLife";
            this.lblHeroLife.Size = new System.Drawing.Size(74, 17);
            this.lblHeroLife.TabIndex = 3;
            this.lblHeroLife.Text = "Hero Life: ";
            // 
            // lblBossLife
            // 
            this.lblBossLife.AutoSize = true;
            this.lblBossLife.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBossLife.Location = new System.Drawing.Point(855, 50);
            this.lblBossLife.Name = "lblBossLife";
            this.lblBossLife.Size = new System.Drawing.Size(70, 17);
            this.lblBossLife.TabIndex = 4;
            this.lblBossLife.Text = "Boss Life:";
            // 
            // lblHeroLifeValue
            // 
            this.lblHeroLifeValue.AutoSize = true;
            this.lblHeroLifeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHeroLifeValue.Location = new System.Drawing.Point(944, 21);
            this.lblHeroLifeValue.Name = "lblHeroLifeValue";
            this.lblHeroLifeValue.Size = new System.Drawing.Size(0, 17);
            this.lblHeroLifeValue.TabIndex = 5;
            // 
            // lblBossLifeValue
            // 
            this.lblBossLifeValue.AutoSize = true;
            this.lblBossLifeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBossLifeValue.Location = new System.Drawing.Point(944, 50);
            this.lblBossLifeValue.Name = "lblBossLifeValue";
            this.lblBossLifeValue.Size = new System.Drawing.Size(0, 17);
            this.lblBossLifeValue.TabIndex = 6;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblScore.Location = new System.Drawing.Point(879, 589);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(49, 17);
            this.lblScore.TabIndex = 7;
            this.lblScore.Text = "Score:";
            // 
            // lblScoreValue
            // 
            this.lblScoreValue.AutoSize = true;
            this.lblScoreValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblScoreValue.Location = new System.Drawing.Point(934, 589);
            this.lblScoreValue.Name = "lblScoreValue";
            this.lblScoreValue.Size = new System.Drawing.Size(0, 17);
            this.lblScoreValue.TabIndex = 8;
            // 
            // SpaceImpact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 661);
            this.Controls.Add(this.lblScoreValue);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblBossLifeValue);
            this.Controls.Add(this.lblHeroLifeValue);
            this.Controls.Add(this.lblBossLife);
            this.Controls.Add(this.lblHeroLife);
            this.Controls.Add(this.bottom_border);
            this.Controls.Add(this.top_border);
            this.Controls.Add(this.plGameSpace);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "SpaceImpact";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpaceImpact";
            this.Load += new System.EventHandler(this.GameLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserKeyPressed);
            ((System.ComponentModel.ISupportInitialize)(this.bottom_border)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.top_border)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plGameSpace;
        private System.Windows.Forms.PictureBox top_border;
        private System.Windows.Forms.PictureBox bottom_border;
        private System.Windows.Forms.Label lblHeroLife;
        private System.Windows.Forms.Label lblBossLife;
        private System.Windows.Forms.Label lblHeroLifeValue;
        private System.Windows.Forms.Label lblBossLifeValue;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblScoreValue;
    }
}

