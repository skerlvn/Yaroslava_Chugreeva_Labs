namespace Lab3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освобождение всех используемых ресурсов.
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
            this.components = new System.ComponentModel.Container();
            this.comboBoxDataGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxSortGroup = new System.Windows.Forms.ComboBox();
            this.buttonGenerateArrays = new System.Windows.Forms.Button();
            this.buttonRunTests = new System.Windows.Forms.Button();
            this.buttonSaveResults = new System.Windows.Forms.Button();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.labelDataGroup = new System.Windows.Forms.Label();
            this.labelSortGroup = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxDataGroup
            // 
            this.comboBoxDataGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataGroup.FormattingEnabled = true;
            this.comboBoxDataGroup.Items.AddRange(new object[] {
            "Случайные числа",
            "Разбитые подмассивы",
            "Отсортированные массивы с перестановками",
            "Массивы с заменами"});
            this.comboBoxDataGroup.Location = new System.Drawing.Point(30, 50);
            this.comboBoxDataGroup.Name = "comboBoxDataGroup";
            this.comboBoxDataGroup.Size = new System.Drawing.Size(250, 24);
            this.comboBoxDataGroup.TabIndex = 0;
            // 
            // comboBoxSortGroup
            // 
            this.comboBoxSortGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSortGroup.FormattingEnabled = true;
            this.comboBoxSortGroup.Items.AddRange(new object[] {
            "Группа 1 (простые)",
            "Группа 2 (продвинутые)",
            "Группа 3 (быстрые)"});
            this.comboBoxSortGroup.Location = new System.Drawing.Point(30, 120);
            this.comboBoxSortGroup.Name = "comboBoxSortGroup";
            this.comboBoxSortGroup.Size = new System.Drawing.Size(250, 24);
            this.comboBoxSortGroup.TabIndex = 1;
            // 
            // buttonGenerateArrays
            // 
            this.buttonGenerateArrays.Location = new System.Drawing.Point(30, 180);
            this.buttonGenerateArrays.Name = "buttonGenerateArrays";
            this.buttonGenerateArrays.Size = new System.Drawing.Size(250, 40);
            this.buttonGenerateArrays.TabIndex = 2;
            this.buttonGenerateArrays.Text = "Сгенерировать массивы";
            this.buttonGenerateArrays.UseVisualStyleBackColor = true;
            this.buttonGenerateArrays.Click += new System.EventHandler(this.buttonGenerateArrays_Click);
            // 
            // buttonRunTests
            // 
            this.buttonRunTests.Location = new System.Drawing.Point(30, 240);
            this.buttonRunTests.Name = "buttonRunTests";
            this.buttonRunTests.Size = new System.Drawing.Size(250, 40);
            this.buttonRunTests.TabIndex = 3;
            this.buttonRunTests.Text = "Запустить тесты";
            this.buttonRunTests.UseVisualStyleBackColor = true;
            this.buttonRunTests.Click += new System.EventHandler(this.buttonRunTests_Click);
            // 
            // buttonSaveResults
            // 
            this.buttonSaveResults.Location = new System.Drawing.Point(30, 300);
            this.buttonSaveResults.Name = "buttonSaveResults";
            this.buttonSaveResults.Size = new System.Drawing.Size(250, 40);
            this.buttonSaveResults.TabIndex = 4;
            this.buttonSaveResults.Text = "Сохранить результаты";
            this.buttonSaveResults.UseVisualStyleBackColor = true;
            this.buttonSaveResults.Click += new System.EventHandler(this.buttonSaveResults_Click);
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zedGraphControl.Location = new System.Drawing.Point(320, 13);
            this.zedGraphControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 0D;
            this.zedGraphControl.ScrollMaxY2 = 0D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 0D;
            this.zedGraphControl.ScrollMinY2 = 0D;
            this.zedGraphControl.Size = new System.Drawing.Size(467, 424);
            this.zedGraphControl.TabIndex = 5;
            this.zedGraphControl.UseExtendedPrintDialog = true;
            // 
            // labelDataGroup
            // 
            this.labelDataGroup.AutoSize = true;
            this.labelDataGroup.Location = new System.Drawing.Point(30, 30);
            this.labelDataGroup.Name = "labelDataGroup";
            this.labelDataGroup.Size = new System.Drawing.Size(167, 16);
            this.labelDataGroup.TabIndex = 6;
            this.labelDataGroup.Text = "Группа тестовых данных";
            // 
            // labelSortGroup
            // 
            this.labelSortGroup.AutoSize = true;
            this.labelSortGroup.Location = new System.Drawing.Point(30, 100);
            this.labelSortGroup.Name = "labelSortGroup";
            this.labelSortGroup.Size = new System.Drawing.Size(215, 16);
            this.labelSortGroup.TabIndex = 7;
            this.labelSortGroup.Text = "Группа алгоритмов сортировки";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelSortGroup);
            this.Controls.Add(this.labelDataGroup);
            this.Controls.Add(this.zedGraphControl);
            this.Controls.Add(this.buttonSaveResults);
            this.Controls.Add(this.buttonRunTests);
            this.Controls.Add(this.buttonGenerateArrays);
            this.Controls.Add(this.comboBoxSortGroup);
            this.Controls.Add(this.comboBoxDataGroup);
            this.Name = "Form1";
            this.Text = "Сравнение алгоритмов сортировки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxDataGroup;
        private System.Windows.Forms.ComboBox comboBoxSortGroup;
        private System.Windows.Forms.Button buttonGenerateArrays;
        private System.Windows.Forms.Button buttonRunTests;
        private System.Windows.Forms.Button buttonSaveResults;
        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.Label labelDataGroup;
        private System.Windows.Forms.Label labelSortGroup;
    }
}

