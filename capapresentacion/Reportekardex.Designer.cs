namespace capapresentacion
{
    partial class Reportekardex
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportekardex));
            this.spimprimir_kardexBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetprincipal = new capapresentacion.DataSetprincipal();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.spimprimir_kardexTableAdapter = new capapresentacion.DataSetprincipalTableAdapters.spimprimir_kardexTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.spimprimir_kardexBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetprincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // spimprimir_kardexBindingSource
            // 
            this.spimprimir_kardexBindingSource.DataMember = "spimprimir_kardex";
            this.spimprimir_kardexBindingSource.DataSource = this.DataSetprincipal;
            // 
            // DataSetprincipal
            // 
            this.DataSetprincipal.DataSetName = "DataSetprincipal";
            this.DataSetprincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.spimprimir_kardexBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "capapresentacion.rptkardex.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1193, 711);
            this.reportViewer1.TabIndex = 0;
            // 
            // spimprimir_kardexTableAdapter
            // 
            this.spimprimir_kardexTableAdapter.ClearBeforeFill = true;
            // 
            // Reportekardex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 711);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Reportekardex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportekardex";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Reportekardex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spimprimir_kardexBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetprincipal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource spimprimir_kardexBindingSource;
        private DataSetprincipal DataSetprincipal;
        private DataSetprincipalTableAdapters.spimprimir_kardexTableAdapter spimprimir_kardexTableAdapter;
    }
}