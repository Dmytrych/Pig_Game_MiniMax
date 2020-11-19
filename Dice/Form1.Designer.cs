namespace Dice
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
            this.diceRollButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.startButton = new System.Windows.Forms.Button();
            this.gameInfoBox = new System.Windows.Forms.Label();
            this.nextTurnButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // diceRollButton
            // 
            this.diceRollButton.Location = new System.Drawing.Point(261, 185);
            this.diceRollButton.Name = "diceRollButton";
            this.diceRollButton.Size = new System.Drawing.Size(238, 61);
            this.diceRollButton.TabIndex = 0;
            this.diceRollButton.Text = "Roll Dice";
            this.diceRollButton.UseVisualStyleBackColor = true;
            this.diceRollButton.Click += new System.EventHandler(this.diceRollButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(164, 155);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 267);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(487, 73);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start New Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // gameInfoBox
            // 
            this.gameInfoBox.AutoSize = true;
            this.gameInfoBox.Location = new System.Drawing.Point(196, 24);
            this.gameInfoBox.Name = "gameInfoBox";
            this.gameInfoBox.Size = new System.Drawing.Size(0, 13);
            this.gameInfoBox.TabIndex = 3;
            // 
            // nextTurnButton
            // 
            this.nextTurnButton.Location = new System.Drawing.Point(12, 185);
            this.nextTurnButton.Name = "nextTurnButton";
            this.nextTurnButton.Size = new System.Drawing.Size(238, 61);
            this.nextTurnButton.TabIndex = 4;
            this.nextTurnButton.Text = "Next Turn";
            this.nextTurnButton.UseVisualStyleBackColor = true;
            this.nextTurnButton.Click += new System.EventHandler(this.nextTurnButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 352);
            this.Controls.Add(this.nextTurnButton);
            this.Controls.Add(this.gameInfoBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.diceRollButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button diceRollButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label gameInfoBox;
        private System.Windows.Forms.Button nextTurnButton;
    }
}

