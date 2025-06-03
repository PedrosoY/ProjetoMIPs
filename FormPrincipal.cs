using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace ProjetoMIPs
{
    public partial class FormPrincipal : Form
    {
        private readonly CpuMips cpu;
        private string? arquivoAtual;

        public FormPrincipal()
        {
            InitializeComponent();
            cpu = new CpuMips();

            // Bindings
            nudFrequencia.DataBindings.Add(nameof(nudFrequencia.Value), cpu, nameof(cpu.FrequenciaDecimal), true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamI.DataBindings.Add(nameof(nudParamI.Value), cpu, nameof(cpu.ParametroI), true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamJ.DataBindings.Add(nameof(nudParamJ.Value), cpu, nameof(cpu.ParametroJ), true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamR.DataBindings.Add(nameof(nudParamR.Value), cpu, nameof(cpu.ParametroR), true, DataSourceUpdateMode.OnPropertyChanged);

            // NumericUpDown settings
            nudFrequencia.Minimum = 0.01M;
            nudFrequencia.Maximum = 50M;
            nudFrequencia.DecimalPlaces = 2;
            nudFrequencia.Increment = 0.01M;
            foreach (var nud in new[] { nudParamI, nudParamJ, nudParamR })
            {
                nud.Minimum = 0;
                nud.Maximum = 1000;
                nud.Increment = 1;
            }

            // Dark theme grids
            ConfigureGrid(dgvRegistradores, new[] { ("Numero", "Numero"), ("Registrador", "Registrador"), ("Hexadecimal", "Hexadecimal"), ("Decimal", "Decimal") });
            dgvRegistradores.DataSource = cpu.RegistradoresView;
            ConfigureGrid(dgvMemoriaPrograma, new[] { ("Endereco", "Endereco"), ("Cod32", "Cod32"), ("Instrucao", "Instrucao"), ("Operacao", "Operacao") });
            dgvMemoriaPrograma.DataSource = cpu.MemoriaProgramaView;
            ConfigureGrid(dgvMemoriaDados, new[] { ("Endereco", "Endereco"), ("Tipo", "Tipo"), ("Hexadecimal", "Hexadecimal"), ("Decimal", "Decimal") });
            dgvMemoriaDados.DataSource = cpu.MemoriaDadosView;

            cpu.PropertyChanged += Cpu_PropertyChanged;
            AtualizarInterface();
        }

        private void ConfigureGrid(DataGridView grid, (string DataProperty, string Header)[] cols)
        {
            grid.Columns.Clear(); grid.AutoGenerateColumns = false; grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = System.Drawing.Color.FromArgb(37, 37, 38);
            grid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.LightGray;
            grid.ForeColor = System.Drawing.Color.LightGray;
            grid.RowHeadersVisible = false; grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (var (prop, header) in cols)
                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = prop, HeaderText = header, ReadOnly = true });
        }

        private void Cpu_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(cpu.ContadorPrograma) or nameof(cpu.TotalCiclos) or nameof(cpu.InstrucaoBinariaAtual) or nameof(cpu.InstrucaoHexAtual))
                UpdateStatusLabels();
        }
        private void UpdateStatusLabels()
        {
            txtInstrucaoBinaria.Text = cpu.InstrucaoBinariaAtual;
            txtInstrucaoHex.Text = cpu.InstrucaoHexAtual;
            lblPC.Text = $"PC: 0x{cpu.ContadorPrograma * 4:X8}";
            lblTempoTotal.Text = $"Tempo Total: {cpu.TotalCiclos}";
        }
        private void AtualizarInterface()
        {
            cpu.AtualizarTabelas();
            dgvRegistradores.Refresh(); dgvMemoriaPrograma.Refresh(); dgvMemoriaDados.Refresh();
            UpdateStatusLabels();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {

        }

        // File and control handlers omitted for brevity (unchanged)...
    }

    public class CpuMips : INotifyPropertyChanged
    {
        private readonly List<Instrucao> prog = new();
        private readonly Dictionary<string, uint> regs = new();
        private readonly Dictionary<int, byte> mem = new();
        private readonly Dictionary<string, int> labels = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        public BindingList<RegistroView> RegistradoresView { get; } = new();
        public BindingList<ProgramaView> MemoriaProgramaView { get; } = new();
        public BindingList<MemoriaView> MemoriaDadosView { get; } = new();
        private double freqGHz;
        public double FrequenciaGHz { get => freqGHz; set { freqGHz = value; OnProp(nameof(FrequenciaGHz)); OnProp(nameof(FrequenciaDecimal)); } }
        public decimal FrequenciaDecimal { get => (decimal)freqGHz; set => FrequenciaGHz = (double)value; }
        private int pI, pJ, pR;
        public int ParametroI { get => pI; set { pI = value; OnProp(nameof(ParametroI)); } }
        public int ParametroJ { get => pJ; set { pJ = value; OnProp(nameof(ParametroJ)); } }
        public int ParametroR { get => pR; set { pR = value; OnProp(nameof(ParametroR)); } }
        public int ContadorPrograma { get; private set; }
        public long TotalCiclos { get; private set; }
        public bool Parada { get; private set; }
        public string InstrucaoBinariaAtual { get; private set; } = string.Empty;
        public string InstrucaoHexAtual { get; private set; } = string.Empty;

        public CpuMips()
        {
            foreach (var n in new[] { "$zero", "$at", "$v0", "$v1", "$a0", "$a1", "$a2", "$a3", "$t0", "$t1", "$t2", "$t3", "$t4", "$t5", "$t6", "$t7", "$s0", "$s1", "$s2", "$s3", "$s4", "$s5", "$s6", "$s7", "$t8", "$t9", "$k0", "$k1", "$gp", "$sp", "$fp", "$ra" })
                regs[n] = 0;
            AtualizarTabelas();
        }

        public void CarregarPrograma(string src)
        {
            TotalCiclos = 0; ContadorPrograma = 0; Parada = false;
            prog.Clear(); labels.Clear(); mem.Clear();
            var lines = src.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(l => l.Trim()).Where(l => !l.StartsWith("//")).ToList();
            int idx = 0;
            foreach (var l in lines)
            {
                if (l.StartsWith("config_CPU")) { ParseConfig(l); continue; }
                if (l.EndsWith(":")) { labels[l.TrimEnd(':')] = idx; continue; }
                var ins = new Instrucao { TextoOriginal = l };
                ins.Decode(); prog.Add(ins); idx++;
            }
            AtualizarTabelas();
        }

        private void ParseConfig(string l)
        {
            var parts = l.Replace("config_CPU", "").Trim().Trim('[', ']').Split(',');
            foreach (var p in parts)
            {
                var kv = p.Split('=');
                if (kv.Length != 2) continue;
                var key = kv[0].Trim(); var val = kv[1].Trim().TrimEnd('G', 'H', 'z');
                switch (key)
                {
                    case var _ when key.StartsWith("20"): FrequenciaGHz = double.Parse(key.TrimEnd('G', 'H', 'z')); break;
                    case "i": ParametroI = int.Parse(val); break;
                    case "j": ParametroJ = int.Parse(val); break;
                    case "r": ParametroR = int.Parse(val); break;
                }
            }
        }

        public void ExecutarCiclo()
        {
            if (Parada || ContadorPrograma >= prog.Count) { Parada = true; return; }
            var ins = prog[ContadorPrograma];
            Execute(ins);
            ContadorPrograma++; TotalCiclos++;
            InstrucaoHexAtual = ins.HexCodigo32;
            InstrucaoBinariaAtual = ins.HexCodigo32;
            OnProp(nameof(ContadorPrograma)); OnProp(nameof(TotalCiclos));
            OnProp(nameof(InstrucaoHexAtual)); OnProp(nameof(InstrucaoBinariaAtual));
        }

        private void Execute(Instrucao ins)
        {
            var parts = ins.TextoOriginal.Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            switch (parts[0])
            {
                case "li":
                    uint imm;
                    var v = parts[2];
                    if (v.StartsWith("'") && v.EndsWith("'"))
                        imm = (uint)v.Trim('\'')[0];
                    else
                        imm = uint.Parse(v);
                    regs[parts[1]] = imm;
                    break;
                case "add":
                    regs[parts[1]] = regs[parts[2]] + regs[parts[3]];
                    break;
                case "sub":
                    regs[parts[1]] = regs[parts[2]] - regs[parts[3]];
                    break;
                case "addi":
                    short imm16 = parts[3].StartsWith("0x") ? Convert.ToInt16(parts[3], 16) : short.Parse(parts[3]);
                    int rsValue = (int)regs[parts[2]];
                    int result = rsValue + imm16;
                    regs[parts[1]] = (uint)result;
                    break;
                case "sw":
                    var vsw = parts[2];
                    var offsetsw = int.Parse(vsw.Split("(")[0]);
                    var registradorsw = (vsw.Split('(', ')')[1]);
                    int enderecosw = (int)regs[registradorsw] + offsetsw;
                    uint valorsw = regs[parts[1]];
                    var bitssw = BitConverter.GetBytes(valorsw);
                    for (int i = 0; i < 4; i++)
                    {
                        mem[enderecosw + i] = bitssw[i];
                    }
                    break;
                case "la":
                    regs[parts[1]] = (uint)int.Parse(parts[2]);
                    break;
                case "beq":
                    if (regs[parts[1]] == regs[parts[2]]) ContadorPrograma = labels[parts[3]] - 1;
                    break;
                case "j":
                    ContadorPrograma = labels[parts[1]] - 1;
                    break;
                case "lw":
                    var v2 = parts[2];
                    var offset = int.Parse(v2.Split("(")[0]);
                    var registrador = (v2.Split('(', ')')[1]);
                    int endereco = (int)regs[registrador] + offset;
                    byte b0 = mem.TryGetValue(endereco + 0, out var vb0) ? vb0 : (byte)0;
                    byte b1 = mem.TryGetValue(endereco + 1, out var vb1) ? vb1 : (byte)0;
                    byte b2 = mem.TryGetValue(endereco + 2, out var vb2) ? vb2 : (byte)0;
                    byte b3 = mem.TryGetValue(endereco + 3, out var vb3) ? vb3 : (byte)0;
                    uint word = (uint)(b0 | (b1 << 8) | (b2 << 16) | (b3 << 24));
                    regs[parts[1]] = word;
                    break;
                case "lh":
                    var vhalf = parts[2];
                    var offsethalf = int.Parse(vhalf.Split("(")[0]);
                    var registradorhalf = (vhalf.Split('(', ')')[1]);
                    int enderecohalf = (int)regs[registradorhalf] + offsethalf;
                    byte b0half = mem.TryGetValue(enderecohalf + 0, out var vb0half) ? vb0half : (byte)0;
                    byte b1half = mem.TryGetValue(enderecohalf + 1, out var vb1half) ? vb1half : (byte)0;

                    short halfword = (short)(b0half | (b1half << 8));
                    regs[parts[1]] = (uint)halfword;
                    break;

                case "sh":
                    var vsh = parts[2];
                    var offsetsh = int.Parse(vsh.Split("(")[0]);
                    var registradorsh = (vsh.Split('(', ')')[1]);
                    int enderecosh = (int)regs[registradorsh] + offsetsh;
                    short valorsh = (short)regs[parts[1]];
                    var bitssh = BitConverter.GetBytes(valorsh);
                    for (int i = 0; i < 2; i++)
                    {
                        mem[enderecosh + i] = bitssh[i];
                    }
                    break;
                case "lb":
                    var vb = parts[2];
                    var offsetvb = int.Parse(vb.Split("(")[0]);
                    var registradorvb = (vb.Split('(', ')')[1]);
                    int enderecovb = (int)regs[registradorvb] + offsetvb;
                    byte b0vb = mem.TryGetValue(enderecovb + 0, out var vb0vb) ? vb0vb : (byte)0;

                    sbyte signedb0vb = (sbyte)b0vb;
                    regs[parts[1]] = (uint)signedb0vb;
                    break;
                case "sb":
                    var vsb = parts[2];
                    var offsetsb = int.Parse(vsb.Split("(")[0]);
                    var registradorsb = (vsb.Split('(', ')')[1]);
                    int enderecosb = (int)regs[registradorsb] + offsetsb;
                    byte valorsb = (byte)regs[parts[1]];

                    mem[enderecosb] = valorsb;

                    break;
                case "and":
                    regs[parts[1]] = regs[parts[2]] & regs[parts[3]];

                    break;
                case "or":
                    regs[parts[1]] = regs[parts[2]] | regs[parts[3]];

                    break;
                case "nor":
                    regs[parts[1]] = ~(regs[parts[2]] | regs[parts[3]]);

                    break;
                case "andi":
                    var immandi = parts[3].StartsWith("0x") ? Convert.ToUInt32(parts[3], 16) : uint.Parse(parts[3]);
                    immandi &= 0xFFFF;
                    regs[parts[1]] = regs[parts[2]] & immandi;
                    break;
                case "ori":
                    var immori = parts[3].StartsWith("0x") ? Convert.ToUInt32(parts[3], 16) : uint.Parse(parts[3]);
                    immori &= 0xFFFF;
                    regs[parts[1]] = regs[parts[2]] | immori;
                    break;
                case "sll":
                    uint rtsll = regs[parts[2]];
                    int shamt = int.Parse(parts[3]) & 0x1F;
                    regs[parts[1]] = rtsll << shamt;
                    break;
                case "srl":
                    uint rtsrl = regs[parts[2]];
                    int shamtsrl = int.Parse(parts[3]) & 0x1F;
                    regs[parts[1]] = rtsrl >> shamtsrl;
                    break;

            }
        }

        public string LerStringDaMemoria(int start, int max)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < max; i++)
            {
                if (!mem.TryGetValue(start + i, out var b) || b == 0) break;
                sb.Append((char)b);
            }
            return sb.ToString();
        }

        public uint GetReg(string n) => regs.TryGetValue(n, out var v) ? v : 0;

        public void AtualizarTabelas()
        {
            RegistradoresView.Clear();
            int idx = 0;
            foreach (var kv in regs)
                RegistradoresView.Add(new RegistroView(idx++, kv.Key, $"0x{kv.Value:X8}", kv.Value));

            MemoriaProgramaView.Clear();
            for (int pc = 0; pc < prog.Count; pc++)
            {
                var ins = prog[pc];
                MemoriaProgramaView.Add(new ProgramaView($"0x{pc * 4:X8}", ins.HexCodigo32, ins.TextoOriginal, ins.Operacao));
            }

            MemoriaDadosView.Clear();
            var groups = mem.OrderBy(kv => kv.Key).GroupBy(kv => kv.Key / 4);
            foreach (var g in groups)
            {
                int addr = g.Key * 4;
                var bts = Enumerable.Range(0, 4).Select(j => mem.TryGetValue(addr + j, out var b) ? b : (byte)0).ToArray();
                uint val = BitConverter.ToUInt32(bts, 0);
                MemoriaDadosView.Add(new MemoriaView($"0x{addr:X8}", "Word", $"0x{val:X8}", val));
            }
        }

        protected void OnProp(string n) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }

    public class RegistroView
    {
        public int Numero { get; }
        public string Registrador { get; }
        public string Hexadecimal { get; }
        public uint Decimal { get; }
        public RegistroView(int n, string r, string h, uint d) => (Numero, Registrador, Hexadecimal, Decimal) = (n, r, h, d);
    }

    public class ProgramaView
    {
        public string Endereco { get; }
        public string Cod32 { get; }
        public string Instrucao { get; }
        public string Operacao { get; }
        public ProgramaView(string e, string c, string i, string o) => (Endereco, Cod32, Instrucao, Operacao) = (e, c, i, o);
    }

    public class MemoriaView
    {
        public string Endereco { get; }
        public string Tipo { get; }
        public string Hexadecimal { get; }
        public uint Decimal { get; }
        public MemoriaView(string e, string t, string h, uint d) => (Endereco, Tipo, Hexadecimal, Decimal) = (e, t, h, d);
    }

    public class Instrucao
    {
        public string TextoOriginal { get; set; } = string.Empty;
        public string Operacao { get; set; } = string.Empty;
        public string HexCodigo32 { get; set; } = "0x00000000";
        public void Decode()
        {
            // Placeholder: actual encoding logic goes here
            HexCodigo32 = "0x" + Guid.NewGuid().ToString("N").Substring(0, 8);
        }
    }
}
