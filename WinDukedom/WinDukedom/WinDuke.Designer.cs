namespace WinDukedom
{
	partial class WinDuke
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
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(113, 12);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(67, 13);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "D U K E D O M";
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(13, 238);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(75, 23);
			this.simpleButton1.TabIndex = 1;
			this.simpleButton1.Text = "Start Game";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// checkEdit1
			// 
			this.checkEdit1.AutoSizeInLayoutControl = true;
			this.checkEdit1.Location = new System.Drawing.Point(13, 213);
			this.checkEdit1.Name = "checkEdit1";
			this.checkEdit1.Properties.Caption = "Do you want to skip detailed reports?";
			this.checkEdit1.Size = new System.Drawing.Size(267, 19);
			this.checkEdit1.TabIndex = 2;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(113, 50);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(66, 13);
			this.labelControl2.TabIndex = 3;
			this.labelControl2.Text = "Converted by";
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(113, 81);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(67, 13);
			this.labelControl3.TabIndex = 4;
			this.labelControl3.Text = "Bob Anderson";
			// 
			// WinDuke
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.checkEdit1);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.labelControl1);
			this.Name = "WinDuke";
			this.Text = "WinDukedom";
			((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.CheckEdit checkEdit1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.LabelControl labelControl3;
	}
}

