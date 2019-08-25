namespace Uroboros.gui
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.runButton = new System.Windows.Forms.Button();
            this.locationBox = new System.Windows.Forms.TextBox();
            this.directoryButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.codeBox = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.codeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.runButton.Location = new System.Drawing.Point(12, 11);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(66, 33);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // locationBox
            // 
            this.locationBox.Enabled = false;
            this.locationBox.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.locationBox.Location = new System.Drawing.Point(84, 12);
            this.locationBox.Name = "locationBox";
            this.locationBox.Size = new System.Drawing.Size(978, 32);
            this.locationBox.TabIndex = 1;
            // 
            // directoryButton
            // 
            this.directoryButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.directoryButton.Location = new System.Drawing.Point(1068, 11);
            this.directoryButton.Name = "directoryButton";
            this.directoryButton.Size = new System.Drawing.Size(35, 33);
            this.directoryButton.TabIndex = 2;
            this.directoryButton.Text = "...";
            this.directoryButton.UseVisualStyleBackColor = true;
            this.directoryButton.Click += new System.EventHandler(this.directoryButton_Click);
            // 
            // logBox
            // 
            this.logBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.logBox.Location = new System.Drawing.Point(745, 50);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(358, 522);
            this.logBox.TabIndex = 4;
            // 
            // codeBox
            // 
            this.codeBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.codeBox.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.codeBox.AutoScrollMinSize = new System.Drawing.Size(41, 31);
            this.codeBox.BackBrush = null;
            this.codeBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.codeBox.CharHeight = 31;
            this.codeBox.CharWidth = 15;
            this.codeBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.codeBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.codeBox.Font = new System.Drawing.Font("Consolas", 20.25F);
            this.codeBox.IsReplaceMode = false;
            this.codeBox.Location = new System.Drawing.Point(12, 50);
            this.codeBox.Name = "codeBox";
            this.codeBox.Paddings = new System.Windows.Forms.Padding(0);
            this.codeBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.codeBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("codeBox.ServiceColors")));
            this.codeBox.Size = new System.Drawing.Size(727, 522);
            this.codeBox.TabIndex = 5;
            this.codeBox.Zoom = 100;
            this.codeBox.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.codeBox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 580);
            this.Controls.Add(this.codeBox);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.directoryButton);
            this.Controls.Add(this.locationBox);
            this.Controls.Add(this.runButton);
            this.Name = "MainForm";
            this.Text = "Meta File Manager";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.codeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox locationBox;
        private System.Windows.Forms.Button directoryButton;
        private System.Windows.Forms.TextBox logBox;
        private FastColoredTextBoxNS.FastColoredTextBox codeBox;
    }
}

