namespace ProjetoMIPs
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainerPrincipal;
        private System.Windows.Forms.TabControl ControlerPages;
        private System.Windows.Forms.TabPage abaRegistradores;
        private System.Windows.Forms.DataGridView dgvRegistradores;
        private System.Windows.Forms.TabPage abaMemoriaPrograma;
        private System.Windows.Forms.DataGridView dgvMemoriaPrograma;
        private System.Windows.Forms.TabPage abaMemoriaDados;
        private System.Windows.Forms.DataGridView dgvMemoriaDados;
        private System.Windows.Forms.TabPage abaConfiguracao;
        private System.Windows.Forms.Label lblFrequencia;
        private System.Windows.Forms.NumericUpDown nudFrequencia;
        private System.Windows.Forms.Label lblParamI;
        private System.Windows.Forms.NumericUpDown nudParamI;
        private System.Windows.Forms.Label lblParamJ;
        private System.Windows.Forms.NumericUpDown nudParamJ;
        private System.Windows.Forms.Label lblParamR;
        private System.Windows.Forms.NumericUpDown nudParamR;
        private System.Windows.Forms.Button btnApplyConfig;
        private System.Windows.Forms.TextBox txtCodigoFonte;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblTempoTotal;
        private System.Windows.Forms.Label lblPC;
        private System.Windows.Forms.GroupBox grpControles;
        private System.Windows.Forms.Button btnProximoCiclo;
        private System.Windows.Forms.Button btnExecutarTudo;
        private System.Windows.Forms.Button btnStepInto;
        private System.Windows.Forms.Button btnStepOver;
        private System.Windows.Forms.GroupBox grpInstrucaoAtual;
        private System.Windows.Forms.TextBox txtInstrucaoHex;
        private System.Windows.Forms.TextBox txtInstrucaoBinaria;

        /// <summary>
        ///  Required method for Designer support
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            toolStripMenu = new ToolStrip();
            tsbOpen = new ToolStripButton();
            tsbSave = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            splitContainerPrincipal = new SplitContainer();
            ControlerPages = new TabControl();
            abaRegistradores = new TabPage();
            dgvRegistradores = new DataGridView();
            abaMemoriaPrograma = new TabPage();
            dgvMemoriaPrograma = new DataGridView();
            abaMemoriaDados = new TabPage();
            dgvMemoriaDados = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            abaConfiguracao = new TabPage();
            lblFrequencia = new Label();
            nudFrequencia = new NumericUpDown();
            lblParamI = new Label();
            nudParamI = new NumericUpDown();
            lblParamJ = new Label();
            nudParamJ = new NumericUpDown();
            lblParamR = new Label();
            nudParamR = new NumericUpDown();
            btnApplyConfig = new Button();
            txtCodigoFonte = new TextBox();
            grpStatus = new GroupBox();
            lblTempoTotal = new Label();
            lblPC = new Label();
            grpControles = new GroupBox();
            btnProximoCiclo = new Button();
            btnExecutarTudo = new Button();
            btnStepInto = new Button();
            btnStepOver = new Button();
            grpInstrucaoAtual = new GroupBox();
            txtInstrucaoHex = new TextBox();
            txtInstrucaoBinaria = new TextBox();
            toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerPrincipal).BeginInit();
            splitContainerPrincipal.Panel1.SuspendLayout();
            splitContainerPrincipal.Panel2.SuspendLayout();
            splitContainerPrincipal.SuspendLayout();
            ControlerPages.SuspendLayout();
            abaRegistradores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRegistradores).BeginInit();
            abaMemoriaPrograma.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMemoriaPrograma).BeginInit();
            abaMemoriaDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMemoriaDados).BeginInit();
            abaConfiguracao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFrequencia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudParamI).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudParamJ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudParamR).BeginInit();
            grpStatus.SuspendLayout();
            grpControles.SuspendLayout();
            grpInstrucaoAtual.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripMenu
            // 
            toolStripMenu.Items.AddRange(new ToolStripItem[] { tsbOpen, tsbSave, toolStripSeparator1 });
            toolStripMenu.Location = new Point(0, 0);
            toolStripMenu.Name = "toolStripMenu";
            toolStripMenu.Size = new Size(1150, 25);
            toolStripMenu.TabIndex = 0;
            // 
            // tsbOpen
            // 
            tsbOpen.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbOpen.Name = "tsbOpen";
            tsbOpen.Size = new Size(79, 22);
            tsbOpen.Text = "Abrir Código";
            //tsbOpen.Click += tsbOpen_Click;
            // 
            // tsbSave
            // 
            tsbSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbSave.Name = "tsbSave";
            tsbSave.Size = new Size(84, 22);
            tsbSave.Text = "Salvar Código";
            //tsbSave.Click += tsbSave_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // splitContainerPrincipal
            // 
            splitContainerPrincipal.Dock = DockStyle.Fill;
            splitContainerPrincipal.Location = new Point(0, 25);
            splitContainerPrincipal.Name = "splitContainerPrincipal";
            // 
            // splitContainerPrincipal.Panel1
            // 
            splitContainerPrincipal.Panel1.BackColor = Color.FromArgb(45, 45, 48);
            splitContainerPrincipal.Panel1.Controls.Add(ControlerPages);
            // 
            // splitContainerPrincipal.Panel2
            // 
            splitContainerPrincipal.Panel2.BackColor = Color.FromArgb(37, 37, 38);
            splitContainerPrincipal.Panel2.Controls.Add(txtCodigoFonte);
            splitContainerPrincipal.Panel2.Controls.Add(grpStatus);
            splitContainerPrincipal.Panel2.Controls.Add(grpControles);
            splitContainerPrincipal.Panel2.Controls.Add(grpInstrucaoAtual);
            splitContainerPrincipal.Size = new Size(1150, 575);
            splitContainerPrincipal.SplitterDistance = 460;
            splitContainerPrincipal.TabIndex = 1;
            // 
            // ControlerPages
            // 
            ControlerPages.Controls.Add(abaRegistradores);
            ControlerPages.Controls.Add(abaMemoriaPrograma);
            ControlerPages.Controls.Add(abaMemoriaDados);
            ControlerPages.Controls.Add(abaConfiguracao);
            ControlerPages.Dock = DockStyle.Fill;
            ControlerPages.Location = new Point(0, 0);
            ControlerPages.Name = "ControlerPages";
            ControlerPages.SelectedIndex = 0;
            ControlerPages.Size = new Size(460, 575);
            ControlerPages.TabIndex = 0;
            // 
            // abaRegistradores
            // 
            abaRegistradores.BackColor = Color.FromArgb(45, 45, 48);
            abaRegistradores.Controls.Add(dgvRegistradores);
            abaRegistradores.Location = new Point(4, 24);
            abaRegistradores.Name = "abaRegistradores";
            abaRegistradores.Size = new Size(452, 547);
            abaRegistradores.TabIndex = 0;
            abaRegistradores.Text = "Registradores";
            // 
            // dgvRegistradores
            // 
            dgvRegistradores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRegistradores.BackgroundColor = Color.FromArgb(37, 37, 38);
            dgvRegistradores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvRegistradores.Dock = DockStyle.Fill;
            dgvRegistradores.EnableHeadersVisualStyles = false;
            dgvRegistradores.Location = new Point(0, 0);
            dgvRegistradores.Name = "dgvRegistradores";
            dgvRegistradores.ReadOnly = true;
            dgvRegistradores.Size = new Size(452, 547);
            dgvRegistradores.TabIndex = 0;
            // 
            // abaMemoriaPrograma
            // 
            abaMemoriaPrograma.BackColor = Color.FromArgb(45, 45, 48);
            abaMemoriaPrograma.Controls.Add(dgvMemoriaPrograma);
            abaMemoriaPrograma.Location = new Point(4, 24);
            abaMemoriaPrograma.Name = "abaMemoriaPrograma";
            abaMemoriaPrograma.Size = new Size(452, 547);
            abaMemoriaPrograma.TabIndex = 1;
            abaMemoriaPrograma.Text = "Memória Programa";
            // 
            // dgvMemoriaPrograma
            // 
            dgvMemoriaPrograma.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMemoriaPrograma.BackgroundColor = Color.FromArgb(37, 37, 38);
            dgvMemoriaPrograma.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvMemoriaPrograma.Dock = DockStyle.Fill;
            dgvMemoriaPrograma.EnableHeadersVisualStyles = false;
            dgvMemoriaPrograma.Location = new Point(0, 0);
            dgvMemoriaPrograma.Name = "dgvMemoriaPrograma";
            dgvMemoriaPrograma.ReadOnly = true;
            dgvMemoriaPrograma.Size = new Size(452, 547);
            dgvMemoriaPrograma.TabIndex = 0;
            // 
            // abaMemoriaDados
            // 
            abaMemoriaDados.BackColor = Color.FromArgb(45, 45, 48);
            abaMemoriaDados.Controls.Add(dgvMemoriaDados);
            abaMemoriaDados.Location = new Point(4, 24);
            abaMemoriaDados.Name = "abaMemoriaDados";
            abaMemoriaDados.Size = new Size(452, 547);
            abaMemoriaDados.TabIndex = 2;
            abaMemoriaDados.Text = "Memória Dados";
            // 
            // dgvMemoriaDados
            // 
            dgvMemoriaDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMemoriaDados.BackgroundColor = Color.FromArgb(37, 37, 38);
            dgvMemoriaDados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvMemoriaDados.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dgvMemoriaDados.Dock = DockStyle.Fill;
            dgvMemoriaDados.EnableHeadersVisualStyles = false;
            dgvMemoriaDados.Location = new Point(0, 0);
            dgvMemoriaDados.Name = "dgvMemoriaDados";
            dgvMemoriaDados.ReadOnly = true;
            dgvMemoriaDados.Size = new Size(452, 547);
            dgvMemoriaDados.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Endereço";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Tipo";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Hexadecimal";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Decimal";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // abaConfiguracao
            // 
            abaConfiguracao.BackColor = Color.FromArgb(45, 45, 48);
            abaConfiguracao.Controls.Add(lblFrequencia);
            abaConfiguracao.Controls.Add(nudFrequencia);
            abaConfiguracao.Controls.Add(lblParamI);
            abaConfiguracao.Controls.Add(nudParamI);
            abaConfiguracao.Controls.Add(lblParamJ);
            abaConfiguracao.Controls.Add(nudParamJ);
            abaConfiguracao.Controls.Add(lblParamR);
            abaConfiguracao.Controls.Add(nudParamR);
            abaConfiguracao.Controls.Add(btnApplyConfig);
            abaConfiguracao.Location = new Point(4, 24);
            abaConfiguracao.Name = "abaConfiguracao";
            abaConfiguracao.Size = new Size(452, 547);
            abaConfiguracao.TabIndex = 3;
            abaConfiguracao.Text = "Configuração CPU";
            // 
            // lblFrequencia
            // 
            lblFrequencia.AutoSize = true;
            lblFrequencia.ForeColor = Color.LightGray;
            lblFrequencia.Location = new Point(10, 20);
            lblFrequencia.Name = "lblFrequencia";
            lblFrequencia.Size = new Size(101, 15);
            lblFrequencia.TabIndex = 0;
            lblFrequencia.Text = "Frequência (GHz):";
            // 
            // nudFrequencia
            // 
            nudFrequencia.DecimalPlaces = 2;
            nudFrequencia.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudFrequencia.Location = new Point(150, 18);
            nudFrequencia.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudFrequencia.Name = "nudFrequencia";
            nudFrequencia.Size = new Size(299, 23);
            nudFrequencia.TabIndex = 1;
            nudFrequencia.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblParamI
            // 
            lblParamI.AutoSize = true;
            lblParamI.ForeColor = Color.LightGray;
            lblParamI.Location = new Point(10, 60);
            lblParamI.Name = "lblParamI";
            lblParamI.Size = new Size(71, 15);
            lblParamI.TabIndex = 2;
            lblParamI.Text = "Parâmetro I:";
            // 
            // nudParamI
            // 
            nudParamI.Location = new Point(150, 58);
            nudParamI.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudParamI.Name = "nudParamI";
            nudParamI.Size = new Size(299, 23);
            nudParamI.TabIndex = 3;
            nudParamI.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblParamJ
            // 
            lblParamJ.AutoSize = true;
            lblParamJ.ForeColor = Color.LightGray;
            lblParamJ.Location = new Point(10, 100);
            lblParamJ.Name = "lblParamJ";
            lblParamJ.Size = new Size(72, 15);
            lblParamJ.TabIndex = 4;
            lblParamJ.Text = "Parâmetro J:";
            // 
            // nudParamJ
            // 
            nudParamJ.Location = new Point(150, 98);
            nudParamJ.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudParamJ.Name = "nudParamJ";
            nudParamJ.Size = new Size(299, 23);
            nudParamJ.TabIndex = 5;
            nudParamJ.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblParamR
            // 
            lblParamR.AutoSize = true;
            lblParamR.ForeColor = Color.LightGray;
            lblParamR.Location = new Point(10, 140);
            lblParamR.Name = "lblParamR";
            lblParamR.Size = new Size(75, 15);
            lblParamR.TabIndex = 6;
            lblParamR.Text = "Parâmetro R:";
            // 
            // nudParamR
            // 
            nudParamR.Location = new Point(150, 138);
            nudParamR.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudParamR.Name = "nudParamR";
            nudParamR.Size = new Size(299, 23);
            nudParamR.TabIndex = 7;
            nudParamR.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnApplyConfig
            // 
            btnApplyConfig.Location = new Point(10, 180);
            btnApplyConfig.Name = "btnApplyConfig";
            btnApplyConfig.Size = new Size(439, 25);
            btnApplyConfig.TabIndex = 8;
            btnApplyConfig.Text = "Aplicar";
            btnApplyConfig.UseVisualStyleBackColor = true;
            //btnApplyConfig.Click += btnApplyConfig_Click;
            // 
            // txtCodigoFonte
            // 
            txtCodigoFonte.BackColor = Color.FromArgb(37, 37, 38);
            txtCodigoFonte.Dock = DockStyle.Fill;
            txtCodigoFonte.Font = new Font("Consolas", 10F);
            txtCodigoFonte.ForeColor = Color.LightGray;
            txtCodigoFonte.Location = new Point(0, 198);
            txtCodigoFonte.Multiline = true;
            txtCodigoFonte.Name = "txtCodigoFonte";
            txtCodigoFonte.ScrollBars = ScrollBars.Both;
            txtCodigoFonte.Size = new Size(686, 377);
            txtCodigoFonte.TabIndex = 0;
            txtCodigoFonte.WordWrap = false;
            // 
            // grpStatus
            // 
            grpStatus.Controls.Add(lblTempoTotal);
            grpStatus.Controls.Add(lblPC);
            grpStatus.Dock = DockStyle.Top;
            grpStatus.ForeColor = Color.LightGray;
            grpStatus.Location = new Point(0, 134);
            grpStatus.Name = "grpStatus";
            grpStatus.Size = new Size(686, 64);
            grpStatus.TabIndex = 1;
            grpStatus.TabStop = false;
            grpStatus.Text = "Status da CPU";
            // 
            // lblTempoTotal
            // 
            lblTempoTotal.AutoSize = true;
            lblTempoTotal.Location = new Point(6, 22);
            lblTempoTotal.Name = "lblTempoTotal";
            lblTempoTotal.Size = new Size(85, 15);
            lblTempoTotal.TabIndex = 0;
            lblTempoTotal.Text = "Tempo Total: 0";
            // 
            // lblPC
            // 
            lblPC.AutoSize = true;
            lblPC.Location = new Point(6, 38);
            lblPC.Name = "lblPC";
            lblPC.Size = new Size(87, 15);
            lblPC.TabIndex = 1;
            lblPC.Text = "PC: 0x00000000";
            // 
            // grpControles
            // 
            grpControles.Controls.Add(btnProximoCiclo);
            grpControles.Controls.Add(btnExecutarTudo);
            grpControles.Controls.Add(btnStepInto);
            grpControles.Controls.Add(btnStepOver);
            grpControles.Dock = DockStyle.Top;
            grpControles.ForeColor = Color.LightGray;
            grpControles.Location = new Point(0, 74);
            grpControles.Name = "grpControles";
            grpControles.Size = new Size(686, 60);
            grpControles.TabIndex = 2;
            grpControles.TabStop = false;
            grpControles.Text = "Controles de Execução";
            // 
            // btnProximoCiclo
            // 
            btnProximoCiclo.Location = new Point(6, 22);
            btnProximoCiclo.Name = "btnProximoCiclo";
            btnProximoCiclo.Size = new Size(100, 23);
            btnProximoCiclo.TabIndex = 0;
            btnProximoCiclo.Text = "Próximo Ciclo";
            btnProximoCiclo.UseVisualStyleBackColor = true;
            //btnProximoCiclo.Click += btnProximoCiclo_Click;
            // 
            // btnExecutarTudo
            // 
            btnExecutarTudo.Location = new Point(112, 22);
            btnExecutarTudo.Name = "btnExecutarTudo";
            btnExecutarTudo.Size = new Size(125, 23);
            btnExecutarTudo.TabIndex = 1;
            btnExecutarTudo.Text = "Executar Até o Fim";
            btnExecutarTudo.UseVisualStyleBackColor = true;
            //btnExecutarTudo.Click += btnExecutarTudo_Click;
            // 
            // btnStepInto
            // 
            btnStepInto.Location = new Point(243, 22);
            btnStepInto.Name = "btnStepInto";
            btnStepInto.Size = new Size(100, 23);
            btnStepInto.TabIndex = 2;
            btnStepInto.Text = "Step Into";
            btnStepInto.UseVisualStyleBackColor = true;
            //btnStepInto.Click += btnStepInto_Click;
            // 
            // btnStepOver
            // 
            btnStepOver.Location = new Point(349, 22);
            btnStepOver.Name = "btnStepOver";
            btnStepOver.Size = new Size(100, 23);
            btnStepOver.TabIndex = 3;
            btnStepOver.Text = "Step Over";
            btnStepOver.UseVisualStyleBackColor = true;
            //btnStepOver.Click += btnStepOver_Click;
            // 
            // grpInstrucaoAtual
            // 
            grpInstrucaoAtual.Controls.Add(txtInstrucaoHex);
            grpInstrucaoAtual.Controls.Add(txtInstrucaoBinaria);
            grpInstrucaoAtual.Dock = DockStyle.Top;
            grpInstrucaoAtual.ForeColor = Color.LightGray;
            grpInstrucaoAtual.Location = new Point(0, 0);
            grpInstrucaoAtual.Name = "grpInstrucaoAtual";
            grpInstrucaoAtual.Size = new Size(686, 74);
            grpInstrucaoAtual.TabIndex = 3;
            grpInstrucaoAtual.TabStop = false;
            grpInstrucaoAtual.Text = "Instrução Atual";
            // 
            // txtInstrucaoHex
            // 
            txtInstrucaoHex.Dock = DockStyle.Top;
            txtInstrucaoHex.Location = new Point(3, 42);
            txtInstrucaoHex.Name = "txtInstrucaoHex";
            txtInstrucaoHex.ReadOnly = true;
            txtInstrucaoHex.Size = new Size(680, 23);
            txtInstrucaoHex.TabIndex = 0;
            // 
            // txtInstrucaoBinaria
            // 
            txtInstrucaoBinaria.Dock = DockStyle.Top;
            txtInstrucaoBinaria.Location = new Point(3, 19);
            txtInstrucaoBinaria.Name = "txtInstrucaoBinaria";
            txtInstrucaoBinaria.ReadOnly = true;
            txtInstrucaoBinaria.Size = new Size(680, 23);
            txtInstrucaoBinaria.TabIndex = 1;
            // 
            // FormPrincipal
            // 
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1150, 600);
            Controls.Add(splitContainerPrincipal);
            Controls.Add(toolStripMenu);
            MinimumSize = new Size(600, 400);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Simulador MIPS";
            toolStripMenu.ResumeLayout(false);
            toolStripMenu.PerformLayout();
            splitContainerPrincipal.Panel1.ResumeLayout(false);
            splitContainerPrincipal.Panel2.ResumeLayout(false);
            splitContainerPrincipal.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerPrincipal).EndInit();
            splitContainerPrincipal.ResumeLayout(false);
            ControlerPages.ResumeLayout(false);
            abaRegistradores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRegistradores).EndInit();
            abaMemoriaPrograma.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMemoriaPrograma).EndInit();
            abaMemoriaDados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMemoriaDados).EndInit();
            abaConfiguracao.ResumeLayout(false);
            abaConfiguracao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudFrequencia).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudParamI).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudParamJ).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudParamR).EndInit();
            grpStatus.ResumeLayout(false);
            grpStatus.PerformLayout();
            grpControles.ResumeLayout(false);
            grpInstrucaoAtual.ResumeLayout(false);
            grpInstrucaoAtual.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
