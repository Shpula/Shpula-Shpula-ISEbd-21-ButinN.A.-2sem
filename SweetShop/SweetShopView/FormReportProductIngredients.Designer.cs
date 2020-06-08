namespace SweetShopView
{
    partial class FormReportProductIngredients
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportProductIngredientViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.buttonMake = new System.Windows.Forms.Button();
            this.buttonToPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProductIngredientViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportProductIngredientViewModelBindingSource
            // 
            this.ReportProductIngredientViewModelBindingSource.DataSource = typeof(SweetShopBusinessLogic.ViewModels.ReportProductIngredientViewModel);
            // 
            // reportViewer
            // 
            reportDataSource2.Name = "DataSetAD";
            reportDataSource2.Value = this.ReportProductIngredientViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "SweetShopView.ReportProductIngredients.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(18, 42);
            this.reportViewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1409, 644);
            this.reportViewer.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(1446, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(855, 2);
            this.buttonMake.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(246, 31);
            this.buttonMake.TabIndex = 2;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.buttonMake_Click);
            // 
            // buttonToPdf
            // 
            this.buttonToPdf.Location = new System.Drawing.Point(1125, 2);
            this.buttonToPdf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonToPdf.Name = "buttonToPdf";
            this.buttonToPdf.Size = new System.Drawing.Size(249, 31);
            this.buttonToPdf.TabIndex = 3;
            this.buttonToPdf.Text = "В PDF";
            this.buttonToPdf.UseVisualStyleBackColor = true;
            this.buttonToPdf.Click += new System.EventHandler(this.buttonToPdf_Click);
            // 
            // FormReportProductIngredients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1446, 698);
            this.Controls.Add(this.buttonToPdf);
            this.Controls.Add(this.buttonMake);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormReportProductIngredients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Детали пакетов";
            this.Load += new System.EventHandler(this.FormReportProductIngredients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportProductIngredientViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.Button buttonToPdf;
        private System.Windows.Forms.BindingSource ReportProductIngredientViewModelBindingSource;
    }
}