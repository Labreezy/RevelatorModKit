using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement.Threading;
using IniParser;
using IniParser.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
namespace RevelatorFileSwapper
{
    public partial class MainForm : Form
    {
        IntPtr p1baseaddr = (IntPtr)0x015E3E8C;
        IntPtr p2baseaddr = (IntPtr)0x01620C28;
        int[] p1scriptptroffsets = {0xA0, 0x70, 0x8, 0x0};
        int[] p2scriptptroffsets = {0x4, 0x28, 0x70, 0x8, 0x0};
        int[] p1etcptroffsets = { 0xA0, 0x70, 0xC, 0x0 };
        int[] p2etcptroffsets = { 0x4, 0x28, 0x70, 0xC, 0x0 };
        const int SCRIPT_OFFSET = 0x3C;
        const int SIZE_1 = 0x40;
        const int SIZE_2 = 0x4C;
        const int SIZE_3 = 0x54;
        const string settingsfilename = "settings.ini";
        string[] shortcharanames = { "AXL", "BED", "CHP", "DZY", "ELP", "FAU", "INO", "JAM", "JHN", "JKO", "KUM", "KYK", "LEO", "MAY", "MLL", "POT", "RAM", "RVN", "SIN", "SLY", "SOL", "VEN", "ZAT" };
        string[] longcharanames = { "Axl", "Bedman", "Chipp", "Dizzy", "Elphelt", "Faust", "I-No", "Jam", "Johnny", "Jack'O", "Haehyun", "Ky", "Leo", "May", "Millia Rage", "Potemkin", "Ramlethal", "Raven", "Sin", "Slayer", "Sol", "Venom", "Zato" };
        const string scriptextension = ".ggscript";
        const string etcextention = "_ETC";
        MemorySharp ms;
        bool modsenabled;
        string moddir;
        const int ENABLE_IDX = -1;
        const int STRING_SIZE = 0x20;
        const int INT_SIZE = 0x4;
        public MainForm()
        {
            if (!File.Exists(settingsfilename))
            {
                createIni();
            }
            try {
                ms = new MemorySharp(Process.GetProcessesByName("GuiltyGearXrd")[0]);
            } catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Open Guilty Gear first, then open this program.");
                Close();
            }
            InitializeComponent();
            readIniData();
            
        }
        void readIniData()
        {
            IniData data;
            using (StreamReader sreader = new StreamReader(File.OpenRead(settingsfilename)))
            {
                StreamIniDataParser sidp = new StreamIniDataParser();
                data = new IniData(sidp.ReadData(sreader));
            }
            moddir = data.Sections.GetSectionData("ModDir").Keys.GetKeyData("Mod Directory").Value;
            SectionData secdata = data.Sections.GetSectionData("ModEnable");
            for (int i = 0; i < shortcharanames.Length; i++)
            {
                CharacterEnableCheckedListBox.Items.Add(longcharanames[i], Boolean.Parse(secdata.Keys.GetKeyData(shortcharanames[i] + "_ENABLED").Value));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Error: P1 or P2 not selected, select one to load the corresponding script!");
                return;
            }
            Stream binstream;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All files (*.*)|*.*";
            sfd.RestoreDirectory = true;
            sfd.OverwritePrompt = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if ((binstream = sfd.OpenFile()) != null)
                {
                    IntPtr[] ptrs = calcPtrs(comboBox1.SelectedIndex);
                    int scriptsize = ms.Read<int>(ptrs[1], false);
                    byte[] scriptbytes = ms.Read<byte>(ptrs[2], scriptsize, false);
                    BinaryWriter binwriter = new BinaryWriter(binstream);
                    binwriter.Write(scriptbytes);
                    binwriter.Close();  
                    MessageBox.Show("Wrote " + scriptsize.ToString() + " bytes to " + sfd.FileName + " successfully!");
                }
            }
        }
        private IntPtr[] calcPtrs(int player)
        {
            IntPtr[] results = new IntPtr[] { IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };
                if (player == 3)
                {
                    results[0] = ms.MakeAbsolute(p1baseaddr);
                    foreach (int offset in p1etcptroffsets)
                    {
                        results[0] = (IntPtr)(ms.Read<int>(results[0], false) + offset);
                    }
                }
                else if(player == 4){
                results[0] = ms.MakeAbsolute(p2baseaddr);
                foreach (int offset in p2etcptroffsets)
                {
                    results[0] = (IntPtr)(ms.Read<int>(results[0], false) + offset);
                }
            }
            else if (player == 1)
            {
                results[0] = ms.MakeAbsolute(p1baseaddr);
                foreach (int offset in p1scriptptroffsets)
                {
                    results[0] = (IntPtr)(ms.Read<int>(results[0], false) + offset);
                }
            }
            else if (player == 2)
            {
                results[0] = ms.MakeAbsolute(p2baseaddr);
                foreach (int offset in p2scriptptroffsets)
                {
                    results[0] = (IntPtr)(ms.Read<int>(results[0], false) + offset);
                }
            } 
            results[0] = IntPtr.Add(results[0], SCRIPT_OFFSET);
            results[1] = IntPtr.Add(results[0], 4);
            results[2] = (IntPtr)(ms.Read<int>(results[0], false));
            return results;
        }
        void createIni()
        {

            if (File.Exists(settingsfilename))
            {
                DialogResult dr = MessageBox.Show(settingsfilename + " already exists in the directory where this program is placed. Do you wish to continue?", "Warning!", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }
                FileStream inifile;
                    SectionDataCollection sdc = new SectionDataCollection();
                    sdc.AddSection("ModDir");
                    sdc.AddSection("ModEnable");
                    SectionData dirinfo = new SectionData("ModDir");
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Select a folder in which to place all the script mods.";
                    fbd.ShowNewFolderButton = true;
                    DialogResult result = fbd.ShowDialog();
                string modpath;
                if(result != DialogResult.OK)
                {
                    return;
                } else
                {
                    modpath = Path.GetFullPath(fbd.SelectedPath);
                    }
                dirinfo.Keys.AddKey("Mod Directory", modpath);
                sdc.SetSectionData("ModDir", dirinfo);
                SectionData charainfo = new SectionData("ModEnable");
                foreach (string shortcharaname in shortcharanames)
                {
                    charainfo.Keys.AddKey(shortcharaname + "_ENABLED", false.ToString());
                }
                sdc.SetSectionData("ModEnable", charainfo);
                inifile = File.Create(settingsfilename);
                if(inifile == null)
                {
                    MessageBox.Show("File settings.ini could not be created in the current directory.  Retry running as administrator?");
                    return;
                }
                FileIniDataParser fidp = new FileIniDataParser();
                StreamWriter swriter = new StreamWriter(inifile);
                fidp.WriteData(swriter, new IniData(sdc));
            swriter.Close();
           }
 
        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }
        private void CharacterEnableCheckedListBox_ItemCheck(object sender, EventArgs e)
        {
            ItemCheckEventArgs itemE = (ItemCheckEventArgs)e;
            if(itemE.NewValue == CheckState.Checked)
            { 
                if (File.Exists(moddir + Path.DirectorySeparatorChar + shortcharanames[itemE.Index] + scriptextension))
                {
                    writeCharaIniChange(itemE.Index, itemE.NewValue);
                }
                else
                {
                    MessageBox.Show("Error: " + shortcharanames[itemE.Index] + scriptextension + " does not exist in " + moddir + ".  Check that you haven't misspelled anything or that the file exists.");
                    CharacterEnableCheckedListBox.SetItemCheckState(itemE.Index, CheckState.Unchecked);
                }
            } else
            {
                writeCharaIniChange(itemE.Index, itemE.NewValue);
            }
            
        }
        private void writeCharaIniChange(int idx, CheckState val)
        {
            StreamIniDataParser sidp = new StreamIniDataParser();
            IniData data;
           using(StreamReader reader = new StreamReader(File.OpenRead(settingsfilename)))
            {
                data = sidp.ReadData(reader);
            }
            SectionData charadata = data.Sections.GetSectionData("ModEnable");
            KeyData modenable;
            if (idx == ENABLE_IDX)
            {
                modsenabled = val == CheckState.Checked;
            }
            else if (idx >= 0)
            {
                modenable = charadata.Keys.GetKeyData(shortcharanames[idx] + "_ENABLED");
                modenable.Value = val == CheckState.Checked ? Boolean.TrueString : Boolean.FalseString;
                charadata.Keys.SetKeyData(modenable);
                data.Sections.SetSectionData("ModEnable", charadata);
                using (StreamWriter writer = new StreamWriter(File.OpenWrite(settingsfilename)))
                {
                    sidp.WriteData(writer, data);
                    writer.Flush();
                }
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        
        private void enableAll_CheckedChanged(object sender, EventArgs e)
        {
            modsEnableBox.Checked = enableAll.Checked;
            for (int i = 0; i < longcharanames.Length; i++)
            {
                if (CharacterEnableCheckedListBox.GetItemChecked(i) != enableAll.Checked)
                {
                    CharacterEnableCheckedListBox.SetItemChecked(i, enableAll.Checked);
                }
            }
        }
        private void tryReplaceFile(int player)
        {
            int charidx = getWhichCharLoaded(player);
            if (CharacterEnableCheckedListBox.GetItemChecked(charidx))
            {   
                    string filename = shortcharanames[charidx] + scriptextension;
                    string etcname = shortcharanames[charidx] + etcextention + scriptextension;
                    string fullpath = moddir + Path.DirectorySeparatorChar + filename;
                    byte[] scriptbytes = File.ReadAllBytes(fullpath);
                    IntPtr[] ptrs = calcPtrs(player);
                    ms.Write<byte>(ptrs[2], scriptbytes, false);
                    ms.Write(ptrs[1], scriptbytes.Length, false);
                    ms.Write(IntPtr.Add(ptrs[1], SIZE_2 - SIZE_1), scriptbytes.Length, false);
                    ms.Write(IntPtr.Add(ptrs[1], SIZE_3 - SIZE_1), scriptbytes.Length, false);
                string etcpath = moddir + Path.DirectorySeparatorChar + etcname;
                if (File.Exists(etcpath))
                {
                    IntPtr[] etcptrs = calcPtrs(player + 2);
                    byte[] etcbytes = File.ReadAllBytes(etcpath);
                    ms.Write<byte>(etcptrs[2], etcbytes, false);
                    ms.Write(etcptrs[1], etcbytes.Length, false);
                    ms.Write(IntPtr.Add(etcptrs[1], SIZE_2 - SIZE_1), etcbytes.Length, false);
                    ms.Write(IntPtr.Add(etcptrs[1], SIZE_3 - SIZE_1), etcbytes.Length, false);
                }
            }
        }
        private int getWhichCharLoaded(int player)
        {
            IntPtr[] addrs = calcPtrs(player);
            int funccount = ms.Read<int>(addrs[2], false);
            int nameoffset = INT_SIZE + (STRING_SIZE + INT_SIZE) * funccount + INT_SIZE + STRING_SIZE + INT_SIZE;
            string charname = ms.ReadString(IntPtr.Add(addrs[2], nameoffset), false).ToUpper();
            if (charname.Length != 3)
            {
                return -1;
            }
            for (int i = 0; i < shortcharanames.Length; i++)
            {
                if (charname == shortcharanames[i])
                {
                    return i;
                }
            }
            return -1; 

        }
        private void modsEnableBox_CheckedChanged(object sender, EventArgs e)
        {
            if (modsEnableBox.Checked == true)
            {
                memp1checktimer.Enabled = true;
                memp2checktimer.Enabled = true;
            }
        }

        private void memchecktimer_Tick(object sender, EventArgs e)
        {
            IntPtr p1absaddr = ms.MakeAbsolute(p1baseaddr);
            IntPtr p1ptrnooffset;
            if ((p1ptrnooffset = pathExists(p1absaddr, p1scriptptroffsets)) != IntPtr.Zero)
            {
                if (!debugtimer.Enabled)
                {
                    debugtimer.Enabled = true;
                }
                int sizeone;
                if ((sizeone = ms.Read<int>(IntPtr.Add(p1ptrnooffset, SIZE_1), false)) != 0)
                {
                    int sizetwo = ms.Read<int>(IntPtr.Add(p1ptrnooffset, SIZE_2), false);
                    int sizethree = ms.Read<int>(IntPtr.Add(p1ptrnooffset, SIZE_3), false);
                    if (sizeone == sizetwo && sizeone == sizethree)
                    {
                        ms.Threads.SuspendAll();
                        tryReplaceFile(1);
                        ms.Threads.ResumeAll();
                        memp1checktimer.Enabled = false;
                        if(memp2checktimer.Enabled == false)
                        {
                            modsEnableBox.Checked = false;
                        }
                        if (pathNotExistTimer.Enabled == false)
                        {
                            pathNotExistTimer.Enabled = true;
                        }
                    }    
                }
            } 
        }
        private IntPtr pathExists(IntPtr absbase, int[] offsets)
        {
            IntPtr currptr = absbase;
            
            foreach(int off in offsets)
            {
                try {
                    currptr = (IntPtr)ms.Read<int>(currptr, false); 
                }
                catch (Win32Exception)
                {
                    return IntPtr.Zero;
                }
                if (currptr == IntPtr.Zero)
                {
                    return currptr;
                }
                currptr = IntPtr.Add(currptr, off);
            }
            int finalint;
            try
            {
                finalint = ms.Read<int>(IntPtr.Add(currptr, SCRIPT_OFFSET), false);
            } catch (Win32Exception)
            {
                return IntPtr.Zero;
            }
            if (finalint != 0) {
                return currptr;
                }
            return IntPtr.Zero;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr p2absaddr = ms.MakeAbsolute(p2baseaddr);
            IntPtr p2ptrnooffset;
            if ((p2ptrnooffset = pathExists(p2absaddr, p2scriptptroffsets)) != IntPtr.Zero)
            {
                int sizeone;
                if ((sizeone = ms.Read<int>(IntPtr.Add(p2ptrnooffset, SIZE_1), false)) != 0)
                {
                    int sizetwo = ms.Read<int>(IntPtr.Add(p2ptrnooffset, SIZE_2), false);
                    int sizethree = ms.Read<int>(IntPtr.Add(p2ptrnooffset, SIZE_3), false);
                    if (sizeone == sizetwo && sizeone == sizethree)
                    {
                        ms.Threads.SuspendAll();
                        tryReplaceFile(2);
                        ms.Threads.ResumeAll();
                        memp2checktimer.Enabled = false;
                        if(memp1checktimer.Enabled == false)
                        {
                            modsEnableBox.Checked = false;
                        }
                        pathNotExistTimer.Enabled = true;
                    }
                }
            }
        }

        private void pathNotExistTimer_Tick(object sender, EventArgs e)
        {
            if (pathExists(ms.MakeAbsolute(p1baseaddr), p1scriptptroffsets) == IntPtr.Zero)
            {
                modsEnableBox.Checked = true;
                pathNotExistTimer.Enabled = false;
                debugtimer.Enabled = false;   
            }
        }

        private void debugtimer_Tick(object sender, EventArgs e)
        {
            IntPtr p1ptr = pathExists(ms.MakeAbsolute(p1baseaddr), p1scriptptroffsets);
            string debug1, debug2;
            if (p1ptr != IntPtr.Zero)
            {
                p1ptr = IntPtr.Add(p1ptr, SCRIPT_OFFSET);
            }
            IntPtr p2ptr = pathExists(ms.MakeAbsolute(p2baseaddr), p2scriptptroffsets);
            if (p2ptr != IntPtr.Zero)
            {
                p2ptr = IntPtr.Add(p2ptr, SCRIPT_OFFSET);
            }
            int p1addr = (p1ptr != IntPtr.Zero) ? ms.Read<int>(p1ptr, false) : 0;
            int p1count = (p1addr != 0) ? ms.Read<int>((IntPtr)p1addr, false) : 0;
            int p2addr = (p2ptr != IntPtr.Zero) ? ms.Read<int>(p2ptr, false) : 0;
            int p2count = (p2addr != 0) ? ms.Read<int>((IntPtr)p2addr, false) : 0;
            debug1 = String.Format("P1:\r\n{0}\r\n{1}\r\n", p1addr, p1count);
            debug2 = String.Format("P2:\r\n{0}\r\n{1}\r\n", p2addr, p2count);
            label2.Text = debug1 + debug2;
        }

        private void GetScriptGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
