namespace capapresentacion
{
    partial class imprimirsolicitud
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(imprimirsolicitud));
            this.spimprimirsolicitudBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetprincipal = new capapresentacion.DataSetprincipal();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.spimprimirsolicitudTableAdapter = new capapresentacion.DataSetprincipalTableAdapters.spimprimirsolicitudTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.spimprimirsolicitudBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetprincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // spimprimirsolicitudBindingSource
            // 
            this.spimprimirsolicitudBindingSource.DataMember = "spimprimirsolicitud";
            this.spimprimirsolicitudBindingSource.DataSource = this.DataSetprincipal;
            // 
            // DataSetprincipal
            // 
            this.DataSetprincipal.DataSetName = "DataSetprincipal";
            this.DataSetprincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.spimprimirsolicitudBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "capapresentacion.solicitud.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1105, 641);
            this.reportViewer1.TabIndex = 0;
            // 
            // spimprimirsolicitudTableAdapter
            // 
            this.spimprimirsolicitudTableAdapter.ClearBeforeFill = true;
            // 
            // imprimirsolicitud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 641);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "imprimirsolicitud";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imprimir Solicitud";
            this.Load += new System.EventHandler(this.imprimirsolicitud_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spimprimirsolicitudBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetprincipal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource spimprimirsolicitudBindingSource;
        private DataSetprincipal DataSetprincipal;
        private DataSetprincipalTableAdapters.spimprimirsolicitudTableAdapter spimprimirsolicitudTableAdapter;
    }
}