namespace FloorMaterialEstimator.Finish_Controls
{
    partial class LineFinishesEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LineFinishesEditForm));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSaveAsDefault = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbNotes = new System.Windows.Forms.TextBox();
            this.txbProduct = new System.Windows.Forms.TextBox();
            this.txbTag = new System.Windows.Forms.TextBox();
            this.btnLineFinishEditorHide = new System.Windows.Forms.Button();
            this.txbWallHeight = new System.Windows.Forms.TextBox();
            this.lblWallHeight = new System.Windows.Forms.Label();
            this.ucLineWidth = new FloorMaterialEstimator.Finish_Controls.UCLineWidth();
            this.ucCustomDashType = new FloorMaterialEstimator.Finish_Controls.UCCustomDashType();
            this.ucCustomColorPalette = new FloorMaterialEstimator.Finish_Controls.UCCustomColorPallet();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblCurrentColorA = new System.Windows.Forms.Label();
            this.lblCurrentColorB = new System.Windows.Forms.Label();
            this.lblCurrentColorG = new System.Windows.Forms.Label();
            this.lblCurrentColorR = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(102, 19);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 5;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(102, 58);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSaveAsDefault
            // 
            this.btnSaveAsDefault.Location = new System.Drawing.Point(159, 858);
            this.btnSaveAsDefault.Name = "btnSaveAsDefault";
            this.btnSaveAsDefault.Size = new System.Drawing.Size(96, 23);
            this.btnSaveAsDefault.TabIndex = 7;
            this.btnSaveAsDefault.Text = "Save As Default";
            this.btnSaveAsDefault.UseVisualStyleBackColor = true;
            this.btnSaveAsDefault.Click += new System.EventHandler(this.btnSaveAsDefault_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnMoveUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.BackgroundImage")));
            this.btnMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveUp.Location = new System.Drawing.Point(223, 217);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(32, 32);
            this.btnMoveUp.TabIndex = 8;
            this.btnMoveUp.UseVisualStyleBackColor = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnMoveDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.BackgroundImage")));
            this.btnMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveDown.Location = new System.Drawing.Point(223, 274);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(32, 32);
            this.btnMoveDown.TabIndex = 9;
            this.btnMoveDown.UseVisualStyleBackColor = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Line List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(264, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Colors";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(264, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Line Styles";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(384, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Line Width";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txbNotes);
            this.groupBox1.Controls.Add(this.txbProduct);
            this.groupBox1.Controls.Add(this.txbTag);
            this.groupBox1.Location = new System.Drawing.Point(36, 681);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(422, 171);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Notes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Product";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Tag";
            // 
            // txbNotes
            // 
            this.txbNotes.AcceptsReturn = true;
            this.txbNotes.AcceptsTab = true;
            this.txbNotes.Location = new System.Drawing.Point(71, 90);
            this.txbNotes.Multiline = true;
            this.txbNotes.Name = "txbNotes";
            this.txbNotes.Size = new System.Drawing.Size(335, 70);
            this.txbNotes.TabIndex = 2;
            this.txbNotes.TextChanged += new System.EventHandler(this.txbNotes_TextChanged);
            // 
            // txbProduct
            // 
            this.txbProduct.AcceptsReturn = true;
            this.txbProduct.AcceptsTab = true;
            this.txbProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbProduct.Location = new System.Drawing.Point(71, 45);
            this.txbProduct.Multiline = true;
            this.txbProduct.Name = "txbProduct";
            this.txbProduct.Size = new System.Drawing.Size(335, 36);
            this.txbProduct.TabIndex = 1;
            this.txbProduct.TextChanged += new System.EventHandler(this.txbProduct_TextChanged);
            // 
            // txbTag
            // 
            this.txbTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTag.Location = new System.Drawing.Point(71, 16);
            this.txbTag.Name = "txbTag";
            this.txbTag.Size = new System.Drawing.Size(335, 22);
            this.txbTag.TabIndex = 0;
            // 
            // btnLineFinishEditorHide
            // 
            this.btnLineFinishEditorHide.Location = new System.Drawing.Point(278, 858);
            this.btnLineFinishEditorHide.Name = "btnLineFinishEditorHide";
            this.btnLineFinishEditorHide.Size = new System.Drawing.Size(75, 23);
            this.btnLineFinishEditorHide.TabIndex = 15;
            this.btnLineFinishEditorHide.Text = "Hide";
            this.btnLineFinishEditorHide.UseVisualStyleBackColor = true;
            this.btnLineFinishEditorHide.Click += new System.EventHandler(this.btnLineFinishEditorHide_Click);
            // 
            // txbWallHeight
            // 
            this.txbWallHeight.Location = new System.Drawing.Point(46, 52);
            this.txbWallHeight.Name = "txbWallHeight";
            this.txbWallHeight.Size = new System.Drawing.Size(100, 20);
            this.txbWallHeight.TabIndex = 16;
            this.txbWallHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbWallHeight.TextChanged += new System.EventHandler(this.txbWallHeight_TextChanged);
            // 
            // lblWallHeight
            // 
            this.lblWallHeight.AutoSize = true;
            this.lblWallHeight.Location = new System.Drawing.Point(43, 23);
            this.lblWallHeight.Name = "lblWallHeight";
            this.lblWallHeight.Size = new System.Drawing.Size(92, 13);
            this.lblWallHeight.TabIndex = 17;
            this.lblWallHeight.Text = "Wall Height (Feet)";
            // 
            // ucLineWidth
            // 
            this.ucLineWidth.AutoScroll = true;
            this.ucLineWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucLineWidth.Location = new System.Drawing.Point(376, 326);
            this.ucLineWidth.Name = "ucLineWidth";
            this.ucLineWidth.Size = new System.Drawing.Size(86, 248);
            this.ucLineWidth.TabIndex = 3;
            // 
            // ucCustomDashType
            // 
            this.ucCustomDashType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucCustomDashType.Location = new System.Drawing.Point(267, 325);
            this.ucCustomDashType.Name = "ucCustomDashType";
            this.ucCustomDashType.Size = new System.Drawing.Size(100, 249);
            this.ucCustomDashType.TabIndex = 2;
            // 
            // ucCustomColorPalette
            // 
            this.ucCustomColorPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucCustomColorPalette.Location = new System.Drawing.Point(267, 27);
            this.ucCustomColorPalette.Name = "ucCustomColorPalette";
            this.ucCustomColorPalette.Size = new System.Drawing.Size(203, 268);
            this.ucCustomColorPalette.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDuplicate);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnInsert);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Location = new System.Drawing.Point(36, 584);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 96);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(10, 58);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(75, 23);
            this.btnDuplicate.TabIndex = 7;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblWallHeight);
            this.groupBox3.Controls.Add(this.txbWallHeight);
            this.groupBox3.Location = new System.Drawing.Point(267, 584);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(191, 96);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblCurrentColorA);
            this.groupBox5.Controls.Add(this.lblCurrentColorB);
            this.groupBox5.Controls.Add(this.lblCurrentColorG);
            this.groupBox5.Controls.Add(this.lblCurrentColorR);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(36, 543);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(191, 38);
            this.groupBox5.TabIndex = 49;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Current Color";
            // 
            // lblCurrentColorA
            // 
            this.lblCurrentColorA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentColorA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentColorA.Location = new System.Drawing.Point(137, 15);
            this.lblCurrentColorA.Name = "lblCurrentColorA";
            this.lblCurrentColorA.Size = new System.Drawing.Size(43, 17);
            this.lblCurrentColorA.TabIndex = 4;
            this.lblCurrentColorA.Text = "A-256";
            this.lblCurrentColorA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentColorB
            // 
            this.lblCurrentColorB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentColorB.Location = new System.Drawing.Point(95, 15);
            this.lblCurrentColorB.Name = "lblCurrentColorB";
            this.lblCurrentColorB.Size = new System.Drawing.Size(43, 17);
            this.lblCurrentColorB.TabIndex = 3;
            this.lblCurrentColorB.Text = "B-256";
            this.lblCurrentColorB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentColorG
            // 
            this.lblCurrentColorG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentColorG.Location = new System.Drawing.Point(51, 15);
            this.lblCurrentColorG.Name = "lblCurrentColorG";
            this.lblCurrentColorG.Size = new System.Drawing.Size(43, 17);
            this.lblCurrentColorG.TabIndex = 2;
            this.lblCurrentColorG.Text = "G-256";
            this.lblCurrentColorG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentColorR
            // 
            this.lblCurrentColorR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentColorR.Location = new System.Drawing.Point(10, 15);
            this.lblCurrentColorR.Name = "lblCurrentColorR";
            this.lblCurrentColorR.Size = new System.Drawing.Size(43, 17);
            this.lblCurrentColorR.TabIndex = 1;
            this.lblCurrentColorR.Text = "R-256";
            this.lblCurrentColorR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(7, 15);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(0, 15);
            this.label29.TabIndex = 0;
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LineFinishesEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 893);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnLineFinishEditorHide);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnSaveAsDefault);
            this.Controls.Add(this.ucLineWidth);
            this.Controls.Add(this.ucCustomDashType);
            this.Controls.Add(this.ucCustomColorPalette);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LineFinishesEditForm";
            this.Text = "Edit Line Finishes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCCustomColorPallet ucCustomColorPalette;
        private UCCustomDashType ucCustomDashType;
        private UCLineWidth ucLineWidth;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSaveAsDefault;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txbNotes;
        private System.Windows.Forms.TextBox txbProduct;
        private System.Windows.Forms.TextBox txbTag;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLineFinishEditorHide;
        private System.Windows.Forms.TextBox txbWallHeight;
        private System.Windows.Forms.Label lblWallHeight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblCurrentColorA;
        private System.Windows.Forms.Label lblCurrentColorB;
        private System.Windows.Forms.Label lblCurrentColorG;
        private System.Windows.Forms.Label lblCurrentColorR;
        private System.Windows.Forms.Label label29;
    }
}