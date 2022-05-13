namespace LoadersBot.Forms
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TB_TokenBot = new System.Windows.Forms.TextBox();
            this.Btn_SaveToken = new System.Windows.Forms.Button();
            this.Btn_SaveConnectString = new System.Windows.Forms.Button();
            this.TB_ConnectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Lbl_StatusDB = new System.Windows.Forms.Label();
            this.Btn_RecheckConnectDB = new System.Windows.Forms.Button();
            this.Btn_OnOffBot = new System.Windows.Forms.Button();
            this.Lbl_StatusBot = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Lbl_Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Токен";
            // 
            // TB_TokenBot
            // 
            this.TB_TokenBot.Location = new System.Drawing.Point(122, 12);
            this.TB_TokenBot.Name = "TB_TokenBot";
            this.TB_TokenBot.Size = new System.Drawing.Size(302, 20);
            this.TB_TokenBot.TabIndex = 1;
            // 
            // Btn_SaveToken
            // 
            this.Btn_SaveToken.Location = new System.Drawing.Point(430, 10);
            this.Btn_SaveToken.Name = "Btn_SaveToken";
            this.Btn_SaveToken.Size = new System.Drawing.Size(97, 23);
            this.Btn_SaveToken.TabIndex = 2;
            this.Btn_SaveToken.Text = "Сохранить";
            this.Btn_SaveToken.UseVisualStyleBackColor = true;
            this.Btn_SaveToken.Click += new System.EventHandler(this.Btn_SaveToken_Click);
            // 
            // Btn_SaveConnectString
            // 
            this.Btn_SaveConnectString.Location = new System.Drawing.Point(430, 39);
            this.Btn_SaveConnectString.Name = "Btn_SaveConnectString";
            this.Btn_SaveConnectString.Size = new System.Drawing.Size(97, 23);
            this.Btn_SaveConnectString.TabIndex = 5;
            this.Btn_SaveConnectString.Text = "Сохранить";
            this.Btn_SaveConnectString.UseVisualStyleBackColor = true;
            this.Btn_SaveConnectString.Click += new System.EventHandler(this.Btn_SaveConnectString_Click);
            // 
            // TB_ConnectionString
            // 
            this.TB_ConnectionString.Location = new System.Drawing.Point(122, 41);
            this.TB_ConnectionString.Name = "TB_ConnectionString";
            this.TB_ConnectionString.Size = new System.Drawing.Size(302, 20);
            this.TB_ConnectionString.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Подключение к БД";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Состояние БД";
            // 
            // Lbl_StatusDB
            // 
            this.Lbl_StatusDB.AutoSize = true;
            this.Lbl_StatusDB.Location = new System.Drawing.Point(119, 73);
            this.Lbl_StatusDB.Name = "Lbl_StatusDB";
            this.Lbl_StatusDB.Size = new System.Drawing.Size(54, 13);
            this.Lbl_StatusDB.TabIndex = 7;
            this.Lbl_StatusDB.Text = "Работает";
            // 
            // Btn_RecheckConnectDB
            // 
            this.Btn_RecheckConnectDB.Location = new System.Drawing.Point(430, 68);
            this.Btn_RecheckConnectDB.Name = "Btn_RecheckConnectDB";
            this.Btn_RecheckConnectDB.Size = new System.Drawing.Size(97, 23);
            this.Btn_RecheckConnectDB.TabIndex = 8;
            this.Btn_RecheckConnectDB.Text = "Перепроверить";
            this.Btn_RecheckConnectDB.UseVisualStyleBackColor = true;
            this.Btn_RecheckConnectDB.Click += new System.EventHandler(this.Btn_RecheckConnectDB_Click);
            // 
            // Btn_OnOffBot
            // 
            this.Btn_OnOffBot.Location = new System.Drawing.Point(430, 97);
            this.Btn_OnOffBot.Name = "Btn_OnOffBot";
            this.Btn_OnOffBot.Size = new System.Drawing.Size(97, 23);
            this.Btn_OnOffBot.TabIndex = 11;
            this.Btn_OnOffBot.Text = "Вкл/Выкл";
            this.Btn_OnOffBot.UseVisualStyleBackColor = true;
            this.Btn_OnOffBot.Click += new System.EventHandler(this.Btn_OnOffBot_Click);
            // 
            // Lbl_StatusBot
            // 
            this.Lbl_StatusBot.AutoSize = true;
            this.Lbl_StatusBot.Location = new System.Drawing.Point(119, 102);
            this.Lbl_StatusBot.Name = "Lbl_StatusBot";
            this.Lbl_StatusBot.Size = new System.Drawing.Size(70, 13);
            this.Lbl_StatusBot.TabIndex = 10;
            this.Lbl_StatusBot.Text = "Не работает";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Состояние Бота";
            // 
            // Lbl_Status
            // 
            this.Lbl_Status.AutoSize = true;
            this.Lbl_Status.Location = new System.Drawing.Point(12, 127);
            this.Lbl_Status.Name = "Lbl_Status";
            this.Lbl_Status.Size = new System.Drawing.Size(47, 13);
            this.Lbl_Status.TabIndex = 12;
            this.Lbl_Status.Text = "Ошибка";
            this.Lbl_Status.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 154);
            this.Controls.Add(this.Lbl_Status);
            this.Controls.Add(this.Btn_OnOffBot);
            this.Controls.Add(this.Lbl_StatusBot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Btn_RecheckConnectDB);
            this.Controls.Add(this.Lbl_StatusDB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Btn_SaveConnectString);
            this.Controls.Add(this.TB_ConnectionString);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_SaveToken);
            this.Controls.Add(this.TB_TokenBot);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Настройки бота грузчиков";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_TokenBot;
        private System.Windows.Forms.Button Btn_SaveToken;
        private System.Windows.Forms.Button Btn_SaveConnectString;
        private System.Windows.Forms.TextBox TB_ConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Lbl_StatusDB;
        private System.Windows.Forms.Button Btn_RecheckConnectDB;
        private System.Windows.Forms.Button Btn_OnOffBot;
        private System.Windows.Forms.Label Lbl_StatusBot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Lbl_Status;
    }
}

