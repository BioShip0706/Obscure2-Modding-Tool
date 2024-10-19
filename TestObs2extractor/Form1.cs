using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TestObs2extractor
{
    public partial class Form1 : Form
    {

        static String directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        static String toolPath; //path of the modding tool
        static String gamePath; //path of the game


        public Form1()
        {
            InitializeComponent();

            textBox1.Text = directoryPath;
            //hvpchoice.SelectedIndex = 0;
            hvpchoice.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            cbackup.Enabled = false;
            rbackup.Enabled = false;

            label1.Enabled = false;

            moddingToolStripMenuItem.Enabled = false;
            cheatsToolStripMenuItem.Enabled = false;
            randomizerStripMenuItem1.Enabled = false;

            //wiiMainMenuToolStripMenuItem.Enabled = false;
            //wiiMainMenuToolStripMenuItem.Text += " (Kinepack.hvp not found!)";

            //wiiInventoryLayoutToolStripMenuItem.Enabled = false;
            //wiiInventoryLayoutToolStripMenuItem.Text += " (Datapack.hvp not found!)";


            //saveEverywhereToolStripMenuItem.Enabled = false;
            //saveEverywhereToolStripMenuItem.Text += " (Datapack.hvp not found!)";

            //hydravisionEntertainmentIntroToolStripMenuItem.Enabled = false;
            //hydravisionEntertainmentIntroToolStripMenuItem.Text += " (Kinepack.hvp not found!)";

            //selectedPath.Text = directoryPath;
            gamePath = selectedPath.Text;
            toolPath = directoryPath;

            restoresAnHiddenButtonOnThePauseMenuThatAllowsYouToSaveAnywhereYouAreToolStripMenuItem.Text += "\n that allows you to save wherever you are";

            replacesMightyRocketStudioIntroToolStripMenuItem.Text += "\n with the Hydravision Entertainment one";

        }

        /* TODO
         * FATTO un bottone che faccia scegliere all'utente la cartella del gioco FATTO
         * FATTO una textbox in cui andrà il percorso selezionato dall'utente  FATTO
         * FATTO un bottone per autodetectare il percorso (da usare nel caso in cui metti l'extractor dove ci sono gli hvp e il .exe SEMPLICE AUTOLOAD 
         * FATTO dopo che scegli il percorso di gioco, se detecta degli hvp, rende possibile interagire con la combobox e gli altri pulsanti, altrimenti sono nascosti FATTO
         * 
         * 25/04
         * FATTO completare pulsante DeXiaZ nel wii inventory
         * FATTO mod per restorare schermata hydravision quando si apre il igoco anzichè MightyRocketStudio
         * FATTO 2/2 fixare gli enable delle due mod wii, perchè serve il file dentro la cartella per fare il backup. guardare cosa ho fatto nel bottone Hydravision Intro, controllare
         *                  l'esistenza del file per poterlo prendere per fare il backup, altrimenti unpackare e prenderlo da lì
        */

        private void button1_Click(object sender, EventArgs e) //bottone unpack
        {

            DialogResult result = MessageBox.Show("Are you sure you want to Unpack?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Controlla la risposta dell'utente
            if (result == DialogResult.Yes)
            {
                String scelta = hvpchoice.Text.ToLower();
                switch (scelta)
                {

                    case "datapack":
                        unpack(scelta);
                        break;

                    case "cachpack":
                        unpack(scelta);
                        break;

                    case "kinepack":
                        unpack(scelta);
                        break;

                    case "loadpack":
                        unpack(scelta);
                        break;
                }
            }
        }

        private void unpack(String scelta)
        {


            //DialogResult result = MessageBox.Show("Are you sure you want to Unpack?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //// Controlla la risposta dell'utente
            //if (result == DialogResult.Yes)
            //{
            if (Directory.Exists(@gamePath + "\\" + scelta))
            {
                DialogResult continueunpack =
                MessageBox.Show("The " + scelta.ToUpper() + " folder is already Unpacked .\nThis may cause problems \ncause some files already exist in that folder \nContinue anyways?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (continueunpack == DialogResult.Yes)
                {

                    textBox1.Text = @gamePath;
                    //Console.WriteLine("CIAOOOOOOOO");
                    System.Diagnostics.Debug.WriteLine(gamePath);

                    Process pilia = new Process();

                    pilia.StartInfo.WorkingDirectory = @gamePath;
                    pilia.StartInfo.FileName = gamePath + "\\obscure.hvptool.exe";
                    textBox1.Text = pilia.StartInfo.FileName;
                    pilia.StartInfo.Arguments = "/e " + scelta + ".hvp " + scelta;
                    pilia.Start();

                    pilia.WaitForExit();

                    registro.Items.Add("Unpacked " + scelta);

                }
            }
            else
            {
                textBox1.Text = gamePath;
                //Console.WriteLine("CIAOOOOOOOO");
                System.Diagnostics.Debug.WriteLine(gamePath);

                Process pilia = new Process();

                pilia.StartInfo.WorkingDirectory = @gamePath;
                pilia.StartInfo.FileName = gamePath + "\\obscure.hvptool.exe";
                textBox1.Text = pilia.StartInfo.FileName;
                pilia.StartInfo.Arguments = "/e " + scelta + ".hvp " + scelta;
                pilia.Start();

                pilia.WaitForExit();

                registro.Items.Add("Unpacked " + scelta);
            }
            //}
        }

        private void repack(String scelta)
        {
            if (Directory.GetFiles(gamePath + "\\" + scelta).Length < 50) //if the number of files equals or bigger than 50, Warning
            {
                textBox1.Text = directoryPath;
                //Console.WriteLine("CIAOOOOOOOO");
                System.Diagnostics.Debug.WriteLine(directoryPath);

                Process pilia = new Process();

                pilia.StartInfo.WorkingDirectory = @gamePath;
                pilia.StartInfo.FileName = gamePath + "\\obscure.hvptool.exe";
                textBox1.Text = pilia.StartInfo.FileName;
                pilia.StartInfo.Arguments = "/p " + scelta + ".hvp " + scelta;
                pilia.Start();

                pilia.WaitForExit();

                registro.Items.Add("Repacked " + scelta);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to repack? \n The folder contains a lot of files \n Proceed if you know what you're doing ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                // Controlla la risposta dell'utente
                if (result == DialogResult.Yes)
                {
                    textBox1.Text = directoryPath;
                    //Console.WriteLine("CIAOOOOOOOO");
                    System.Diagnostics.Debug.WriteLine(directoryPath);

                    Process pilia = new Process();

                    pilia.StartInfo.WorkingDirectory = @gamePath;
                    pilia.StartInfo.FileName = gamePath + "\\obscure.hvptool.exe";
                    textBox1.Text = pilia.StartInfo.FileName;
                    pilia.StartInfo.Arguments = "/p " + scelta + ".hvp " + scelta;
                    pilia.Start();

                    pilia.WaitForExit();

                    registro.Items.Add("Repacked " + scelta);
                }
            }
        }

        private void moddingcachpack() //metodo per aprire la cartella cachpack
        {

            String cachpackpath = @directoryPath + "\\cachpack";

            Process.Start("explorer.exe", cachpackpath);

            List<String> listanomi = new List<String>();


        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //bottone repack
        {

            DialogResult result = MessageBox.Show("Are you sure you want to Repack?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Controlla la risposta dell'utente
            if (result == DialogResult.Yes)
            {
                String scelta = hvpchoice.Text.ToLower();
                switch (scelta)
                {

                    case "datapack":
                        repack(scelta);
                        break;

                    case "cachpack":
                        repack(scelta);
                        break;

                    case "kinepack":
                        repack(scelta);
                        break;

                    case "loadpack":
                        repack(scelta);
                        break;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            registro.Items.Clear();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select your game folder (or where .hvp files are)";
            folderBrowserDialog.UseDescriptionForTitle = true;

            DialogResult result = folderBrowserDialog.ShowDialog();

            gamePath = folderBrowserDialog.SelectedPath;

            this.selectedPath.Text = gamePath;

            bool datapackExists = File.Exists(gamePath + "\\datapack.hvp");
            bool kinepackExists = File.Exists(gamePath + "\\kinepack.hvp");
            bool cachpackExists = File.Exists(gamePath + "\\cachpack.hvp");
            bool loadpackExists = File.Exists(gamePath + "\\loadpack.hvp");

            hvpchoice.Items.Clear();
            hvpchoice.Text = "";

            wiiInventoryLayoutToolStripMenuItem.Text = "Wii Inventory Layout";
            saveEverywhereToolStripMenuItem.Text = "Save everywhere";
            wiiMainMenuToolStripMenuItem.Text = "Wii Main Menu";
            hydravisionEntertainmentIntroToolStripMenuItem.Text = "Hydravision Entertainment Intro";

            if (datapackExists)
            {
                hvpchoice.Items.Add("DATAPACK");
                wiiInventoryLayoutToolStripMenuItem.Enabled = true;
                wiiInventoryLayoutToolStripMenuItem.Text = "Wii Inventory Layout";


                saveEverywhereToolStripMenuItem.Enabled = true;
                saveEverywhereToolStripMenuItem.Text = "Save everywhere";
            }
            else
            {
                wiiInventoryLayoutToolStripMenuItem.Enabled = false;
                wiiInventoryLayoutToolStripMenuItem.Text += " (Datapack.hvp not found!)";

                saveEverywhereToolStripMenuItem.Enabled = false;
                saveEverywhereToolStripMenuItem.Text += " (Datapack.hvp not found!)";
            }
            if (kinepackExists)
            {
                hvpchoice.Items.Add("KINEPACK");
                wiiMainMenuToolStripMenuItem.Enabled = true;
                wiiMainMenuToolStripMenuItem.Text = "Wii Main Menu";

                hydravisionEntertainmentIntroToolStripMenuItem.Enabled = true;
                hydravisionEntertainmentIntroToolStripMenuItem.Text = "Hydravision Entertainment Intro";
            }
            else
            {
                wiiMainMenuToolStripMenuItem.Enabled = false;
                wiiMainMenuToolStripMenuItem.Text += " (Kinepack.hvp not found!)";

                hydravisionEntertainmentIntroToolStripMenuItem.Enabled = false;
                hydravisionEntertainmentIntroToolStripMenuItem.Text += " (Kinepack.hvp not found!)";
            }
            if (cachpackExists)
            {
                hvpchoice.Items.Add("CACHPACK");
            }
            if (loadpackExists)
            {
                hvpchoice.Items.Add("LOADPACK");
            }

            if (hvpchoice.Items.Count > 0)
                hvpchoice.SelectedIndex = 0;

            if (datapackExists || kinepackExists || cachpackExists || loadpackExists)
            {
                moddingToolStripMenuItem.Enabled = true;
                hvpchoice.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                cbackup.Enabled = true;
                rbackup.Enabled = true;

                cheatsToolStripMenuItem.Enabled = true;
                randomizerStripMenuItem1.Enabled = true;

                label1.Enabled = true;


            }
            else
            {
                moddingToolStripMenuItem.Enabled = false;
                hvpchoice.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                cbackup.Enabled = false;
                rbackup.Enabled = false;

                cheatsToolStripMenuItem.Enabled = false;
                randomizerStripMenuItem1.Enabled = false;

                label1.Enabled = false;
            }

            File.Copy(toolPath + "\\hvptool\\" + "obscure.hvptool.exe", gamePath + "\\obscure.hvptool.exe", true);
        }

        private void cbackup_Click(object sender, EventArgs e)
        {

            String scelta = hvpchoice.Text.ToLower();
            DialogResult result = MessageBox.Show("Create backup of " + scelta + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Controlla la risposta dell'utente
            if (result == DialogResult.Yes)
            {
                //String scelta = hvpchoice.Text.ToLower();
                switch (scelta)
                {

                    case "datapack":
                        createbackup(scelta);
                        break;

                    case "cachpack":
                        createbackup(scelta);
                        break;

                    case "kinepack":
                        createbackup(scelta);
                        break;

                    case "loadpack":
                        createbackup(scelta);
                        break;
                }
            }



        }



        private void cbackup_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                String itemss = "\n";
                foreach (String item in hvpchoice.Items)
                {
                    itemss += item + "," + "";
                }

                DialogResult result = MessageBox.Show("Create backup of " + itemss + " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Controlla la risposta dell'utente
                if (result == DialogResult.Yes)
                {
                    foreach (String item in hvpchoice.Items)
                    {
                        createbackup(item.ToLower());
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        private void createbackup(String scelta)
        {
            String hvpname = scelta + ".hvp";
            String backupname = gamePath + "\\" + hvpname + ".bak";

            bool sceltaexists = File.Exists(gamePath + "\\" + hvpname); //se datapack.hvp esiste

            if (sceltaexists)
            {
                File.Copy(gamePath + "\\" + hvpname, backupname, true);
                MessageBox.Show(hvpname + " backup created!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                registro.Items.Add("Created backup of " + scelta);

            }
        }

        private void rbackup_Click(object sender, EventArgs e)
        {
            String scelta = hvpchoice.Text.ToLower();

            DialogResult result = MessageBox.Show("Restore backup of " + scelta + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Controlla la risposta dell'utente
            if (result == DialogResult.Yes)
            {
                String hvpname = scelta + ".hvp";
                String filetodelete = gamePath + "\\" + hvpname;
                String backupname = gamePath + "\\" + hvpname + ".bak";

                bool sceltaexists = File.Exists(gamePath + "\\" + hvpname); //se datapack.hvp esiste

                if (sceltaexists)
                {
                    File.Delete(filetodelete);
                    File.Move(backupname, filetodelete);
                    //File.Copy(gamePath + "\\" + hvpname, backupname, true);
                    MessageBox.Show(hvpname + " backup restored!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    registro.Items.Add("Restored backup of " + scelta);
                }

            }

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Created by BioShip0706 :)");
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form imageForm = new Form();
            imageForm.Text = "Wii menu preview";
            imageForm.Size = new Size(960, 570);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            //pictureBox.Image = System.Drawing.Image.FromFile(wiimenupath);
            pictureBox.Image = Properties.Resources.Screenshot;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            imageForm.Controls.Add(pictureBox);

            imageForm.ShowDialog();

        }

        private void enableToolStripMenuItem_Click(object sender, EventArgs e) //bottone abilita wii menu
        {
            // AGGIUNGERE CONTROLLO SE ESISTE KINEPACK.HVP PRIMA DI FARE IL TUTTO
            if (!enableToolStripMenuItem.Checked)
            {
                DialogResult proseguire = MessageBox.Show("Enable Wii main menu mod?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (proseguire == DialogResult.Yes)
                {
                    if (Directory.Exists(gamePath + "\\kinepack"))
                    {
                        DialogResult result = MessageBox.Show("Kinepack folder detected. \nThis mod will replace the 0000000f.dat file of Kinepack. \nCreate a backup of this file and Proceed?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            if (!File.Exists(gamePath + "\\kinepack" + "\\0000000f.dat"))
                            {
                                Directory.Move(gamePath + "\\kinepack", gamePath + "\\kinepackTEMP"); //rinomino kinepack
                                Thread.Sleep(2000);

                                unpack("kinepack");
                                Thread.Sleep(5000);

                                File.Copy(gamePath + "\\kinepack" + "\\0000000f.dat", toolPath + "\\Mods" + "\\Wii Main Menu" + "\\0000000f.dat.bak", true);
                                Thread.Sleep(1500);

                                Directory.Delete(gamePath + "\\kinepack", true);
                                Thread.Sleep(1500);

                                Directory.Move(gamePath + "\\kinepackTEMP", gamePath + "\\kinepack");
                                Thread.Sleep(1000);

                                registro.Items.Add("Created backup of Kinepack file 0000000f.dat");
                                MessageBox.Show("Created backup of Kinepack file 0000000f.dat");


                            }
                            else
                            {
                                File.Copy(gamePath + "\\kinepack" + "\\0000000f.dat", toolPath + "\\Mods" + "\\Wii Main Menu" + "\\0000000f.dat.bak", true);
                                registro.Items.Add("Created backup of Kinepack file 0000000f.dat");
                                MessageBox.Show("Created backup of Kinepack file 0000000f.dat");
                            }

                            wiiMainMenuWithFolder("0000000f.dat", "0000000f.dat");

                            registro.Items.Add("Enabled Wii Main Menu Mod");
                            MessageBox.Show("Enabled Wii Main Menu Mod");

                            enableToolStripMenuItem.Checked = !enableToolStripMenuItem.Checked; //SPUNTA ENABLED
                            disabletoolStripMenuItem1.Checked = false;
                        }
                    }
                    else //se non esiste la cartella
                    {

                        DialogResult result = MessageBox.Show("Kinepack folder not detected. \nThis mod will replace the 0000000f.dat file of Kinepack. \nCreate a backup of this file and Proceed?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            unpack("kinepack");
                            Thread.Sleep(5000);

                            File.Copy(gamePath + "\\kinepack" + "\\0000000f.dat", toolPath + "\\Mods" + "\\Wii Main Menu" + "\\0000000f.dat.bak", true);
                            Thread.Sleep(1500);

                            Directory.Delete(gamePath + "\\kinepack", true);
                            Thread.Sleep(1500);

                            registro.Items.Add("Created backup of Kinepack file 0000000f.dat");
                            MessageBox.Show("Created backup of Kinepack file 0000000f.dat");

                            wiiMainMenuWithOutFolder("0000000f.dat", "0000000f.dat");


                            registro.Items.Add("Enabled Wii Main Menu Mod");
                            MessageBox.Show("Enabled Wii Main Menu Mod");



                            enableToolStripMenuItem.Checked = !enableToolStripMenuItem.Checked; //SPUNTA ENABLED
                            disabletoolStripMenuItem1.Checked = false;

                        }
                    }
                }
                this.Focus();
            }
        }


        private void disabletoolStripMenuItem1_Click(object sender, EventArgs e) //disable Wii main menu mod
        {
            if (!disabletoolStripMenuItem1.Checked)
            {
                DialogResult proseguire = MessageBox.Show("Disable Wii main menu mod?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (proseguire == DialogResult.Yes)
                {
                    if (File.Exists(toolPath + "\\Mods" + "\\Wii Main Menu" + "\\0000000f.dat.bak"))
                    {
                        if (Directory.Exists(gamePath + "\\kinepack"))
                        {
                            DialogResult result = MessageBox.Show("Backup file detected.\nRestore from backup?\nCancel to restore from Vanilla game", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                wiiMainMenuWithFolder("0000000f.dat.bak", "0000000f.dat");

                                registro.Items.Add("Disabled Wii Main Menu Mod");
                                MessageBox.Show("Wii Main Menu mod Disabled!");

                                disabletoolStripMenuItem1.Checked = !disabletoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem.Checked = false;
                            }
                            else //se repacko vanilla
                            {
                                wiiMainMenuWithFolder("0000000f.dat.vanilla", "0000000f.dat");
                                registro.Items.Add("Disabled Wii Main Menu Mod");
                                MessageBox.Show("Wii Main Menu mod Disabled!");

                                disabletoolStripMenuItem1.Checked = !disabletoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem.Checked = false;
                            }
                        }
                        else //Se la cartella non esiste
                        {
                            DialogResult result = MessageBox.Show("Backup file detected.\nRestore from backup file?\nCancel to restore from Vanilla game", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                wiiMainMenuWithOutFolder("0000000f.dat.bak", "0000000f.dat");
                                registro.Items.Add("Disabled Wii Main Menu Mod");
                                MessageBox.Show("Wii Main Menu mod Disabled!");

                                disabletoolStripMenuItem1.Checked = !disabletoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem.Checked = false;
                            }
                            else //se repacko vanilla
                            {
                                wiiMainMenuWithOutFolder("0000000f.dat.vanilla", "0000000f.dat");
                                registro.Items.Add("Disabled Wii Main Menu Mod");
                                MessageBox.Show("Wii Main Menu mod Disabled!");

                                disabletoolStripMenuItem1.Checked = !disabletoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem.Checked = false;

                            }
                        }

                    }
                    else //se backup non esiste entra qua
                    {

                        if (Directory.Exists(gamePath + "\\kinepack"))
                        {
                            DialogResult result = MessageBox.Show("Backup file not detected.\nRestore from vanilla game?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                wiiMainMenuWithFolder("0000000f.dat.vanilla", "0000000f.dat");

                                registro.Items.Add("Disabled Wii Main Menu Mod");
                                MessageBox.Show("Wii Main Menu mod Disabled!");

                                disabletoolStripMenuItem1.Checked = !disabletoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem.Checked = false;

                            }
                        }
                        else //Se la cartella non esiste
                        {
                            DialogResult result = MessageBox.Show("Backup file not detected.\nRestore from vanilla game?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                wiiMainMenuWithOutFolder("0000000f.dat.vanilla", "0000000f.dat");

                                registro.Items.Add("Disabled Wii Main Menu Mod");
                                MessageBox.Show("Wii Main Menu mod Disabled!");

                                disabletoolStripMenuItem1.Checked = !disabletoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem.Checked = false;

                            }
                        }
                    }
                }
                this.Focus();
            }
        }



        private void wiiMainMenuWithFolder(String sourceFile, String destFile)
        {
            //devo rinominare la cartella kinepack in kinepackTEMP SE ESISTE PERO' 
            Directory.Move(gamePath + "\\kinepack", gamePath + "\\kinepackTEMP");
            Thread.Sleep(1000);
            //creare la cartella kinepack
            Directory.CreateDirectory(gamePath + "\\kinepack");
            Thread.Sleep(1000);
            //metterci dentro il file 0000000f.dat
            File.Copy(toolPath + "\\Mods" + "\\Wii Main Menu" + "\\" + sourceFile, gamePath + "\\kinepack" + "\\" + destFile, true);
            Thread.Sleep(1500);
            //repackare
            repack("kinepack");
            Thread.Sleep(1500);
            //cancellare la cartella kinepack
            Directory.Delete(gamePath + "\\kinepack", true);
            Thread.Sleep(1000);
            //rinominare kinepackTEMP in kinepack
            Directory.Move(gamePath + "\\kinepackTEMP", gamePath + "\\kinepack");
            //registro.Items.Add("Enabled Wii Main Menu Mod");


        }

        private void wiiMainMenuWithOutFolder(String sourceFile, String destFile)
        {
            //creare la cartella kinepack
            Directory.CreateDirectory(gamePath + "\\kinepack");
            Thread.Sleep(1000);
            //metterci dentro il file 0000000f.dat
            File.Copy(toolPath + "\\Mods" + "\\Wii Main Menu" + "\\" + sourceFile, gamePath + "\\kinepack" + "\\" + destFile, true);
            Thread.Sleep(1500);
            //repackare
            repack("kinepack");
            Thread.Sleep(1500);
            //cancellare la cartella kinepack
            Directory.Delete(gamePath + "\\kinepack", true);
            Thread.Sleep(1000);
            //rinominare kinepackTEMP in kinepack
            //Directory.Move(gamePath + "\\kinepackTEMP", gamePath + "\\kinepack");
            //registro.Items.Add("Enabled Wii Main Menu Mod");


        }



        private void enableToolStripMenuItem1_Click(object sender, EventArgs e) //enable wii inventory layout
        {
            // AGGIUNGERE CONTROLLO SE ESISTE DATAPACK.HVP PRIMA DI FARE IL TUTTO
            if (!enableToolStripMenuItem1.Checked)
            {

                DialogResult proseguire = MessageBox.Show("Enable Wii Inventory Layout mod?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (proseguire == DialogResult.Yes)
                {
                    if (Directory.Exists(gamePath + "\\datapack"))
                    {
                        DialogResult result = MessageBox.Show("Datapack folder detected. \nThis mod will replace 15 files of Datapack. \nCreate a backup of these files and Proceed?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            bool backup = false;

                            string[] fileNames = {
                                                  "000004e9.dat",
                                                  "000004ea.dat",
                                                  "000004eb.dat",
                                                  "000004ec.dat",
                                                  "000004ed.dat",
                                                  "000004ef.dat",
                                                  "000004f0.dat",
                                                  "000004f1.dat",
                                                  "000004f2.dat",
                                                  "000004f3.dat",
                                                  "000004f4.dat",
                                                  "000004f5.dat",
                                                  "000004f6.dat",
                                                  "000004f7.dat",
                                                  "000004f8.dat"
                                                  };

                            foreach (string fileName in fileNames)
                            {
                                if (File.Exists(gamePath + "\\kinepack" + "\\" + fileName))
                                {
                                    backup = true;
                                }
                                else
                                {
                                    backup = false;
                                    break;
                                }

                            }

                            if (!backup) //if backup is false
                            {

                                Directory.Move(gamePath + "\\datapack", gamePath + "\\datapackTEMP"); //rinomino kinepack
                                Thread.Sleep(2000);

                                unpack("datapack");
                                Thread.Sleep(5000);


                                foreach (string fileName in fileNames)
                                {
                                    File.Copy(gamePath + "\\datapack" + "\\" + fileName, toolPath + "\\Mods" + "\\Wii Inventory" + "\\backup" + "\\" + fileName + ".bak", true);
                                }
                                Thread.Sleep(1500);


                                Directory.Delete(gamePath + "\\datapack", true);
                                Thread.Sleep(1500);


                                Directory.Move(gamePath + "\\datapackTEMP", gamePath + "\\datapack");
                                Thread.Sleep(1000);

                                registro.Items.Add("Created backup of 15 Datapack files");
                                MessageBox.Show("Created backup of 15 Datapack files");

                            }
                            else
                            {

                                foreach (string fileName in fileNames)
                                {
                                    File.Copy(gamePath + "\\datapack" + "\\" + fileName, toolPath + "\\Mods" + "\\Wii Inventory" + "\\backup" + "\\" + fileName + ".bak", true);
                                }
                                Thread.Sleep(1500);

                                registro.Items.Add("Created backup of 15 Datapack files");
                                MessageBox.Show("Created backup of 15 Datapack files");

                            }

                            wiiInventoryWithFolder("dat", fileNames, fileNames);

                            registro.Items.Add("Wii Inventory Layout Mod Enabled");
                            MessageBox.Show("Wii Inventory Layout Mod Enabled");

                            enableToolStripMenuItem1.Checked = !enableToolStripMenuItem1.Checked; //SPUNTA ENABLED
                            disableWILtoolStripMenuItem1.Checked = false;
                        }
                    }
                    else //se non esiste la cartella
                    {

                        DialogResult result = MessageBox.Show("Datapack folder detected. \nThis mod will replace 15 files of Datapack. \nCreate a backup of these files and Proceed?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {

                            string[] fileNames = {
                                                  "000004e9.dat",
                                                  "000004ea.dat",
                                                  "000004eb.dat",
                                                  "000004ec.dat",
                                                  "000004ed.dat",
                                                  "000004ef.dat",
                                                  "000004f0.dat",
                                                  "000004f1.dat",
                                                  "000004f2.dat",
                                                  "000004f3.dat",
                                                  "000004f4.dat",
                                                  "000004f5.dat",
                                                  "000004f6.dat",
                                                  "000004f7.dat",
                                                  "000004f8.dat"
                                                  };

                            unpack("datapack");
                            Thread.Sleep(5000);


                            foreach (string fileName in fileNames)
                            {
                                File.Copy(gamePath + "\\datapack" + "\\" + fileName, toolPath + "\\Mods" + "\\Wii Inventory" + "\\backup" + "\\" + fileName + ".bak", true);
                            }
                            Thread.Sleep(1500);


                            Directory.Delete(gamePath + "\\datapack", true);
                            Thread.Sleep(1500);

                            //createbackup("datapack");

                            registro.Items.Add("Created backup of 15 Datapack files");
                            MessageBox.Show("Created backup of 15 Datapack files");

                            wiiInventoryWithOutFolder("dat", fileNames, fileNames);


                            registro.Items.Add("Wii Inventory Layout Mod Enabled");
                            MessageBox.Show("Wii Inventory Layout Mod Enabled");

                            enableToolStripMenuItem1.Checked = !enableToolStripMenuItem1.Checked; //SPUNTA ENABLED
                            disableWILtoolStripMenuItem1.Checked = false;

                        }
                    }
                }
                this.Focus();
            }

        }

        private void disableWILtoolStripMenuItem1_Click(object sender, EventArgs e) //disable wii inventory layout
        {
            if (!disableWILtoolStripMenuItem1.Checked)
            {
                DialogResult proseguire = MessageBox.Show("Disable Wii Inventory layout mod?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (proseguire == DialogResult.Yes)
                {
                    if (File.Exists(toolPath + "\\Mods" + "\\Wii Inventory" + "\\backup" + "\\000004e9.dat.bak"))
                    {
                        if (Directory.Exists(gamePath + "\\datapack"))
                        {
                            DialogResult result = MessageBox.Show("Backup file detected.\nRestore from backup?\nCancel to restore from Vanilla game", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                string[] fileNames = {
                                                    "000004e9.dat.bak",
                                                    "000004ea.dat.bak",
                                                    "000004eb.dat.bak",
                                                    "000004ec.dat.bak",
                                                    "000004ed.dat.bak",
                                                    "000004ef.dat.bak",
                                                    "000004f0.dat.bak",
                                                    "000004f1.dat.bak",
                                                    "000004f2.dat.bak",
                                                    "000004f3.dat.bak",
                                                    "000004f4.dat.bak",
                                                    "000004f5.dat.bak",
                                                    "000004f6.dat.bak",
                                                    "000004f7.dat.bak",
                                                    "000004f8.dat.bak",
                                                     };

                                string[] fileNames2 = {
                                                    "000004e9.dat",
                                                    "000004ea.dat",
                                                    "000004eb.dat",
                                                    "000004ec.dat",
                                                    "000004ed.dat",
                                                    "000004ef.dat",
                                                    "000004f0.dat",
                                                    "000004f1.dat",
                                                    "000004f2.dat",
                                                    "000004f3.dat",
                                                    "000004f4.dat",
                                                    "000004f5.dat",
                                                    "000004f6.dat",
                                                    "000004f7.dat",
                                                    "000004f8.dat",
                                                     };

                                wiiInventoryWithFolder("bak", fileNames, fileNames2);

                                registro.Items.Add("Disabled Wii Inventory layout Mod");
                                MessageBox.Show("Wii Inventory layout Disabled!");

                                disableWILtoolStripMenuItem1.Checked = !disableWILtoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem1.Checked = false;
                            }
                            else //se repacko vanilla
                            {

                                string[] fileNames = {
                                                    "000004e9.dat.vanilla",
                                                    "000004ea.dat.vanilla",
                                                    "000004eb.dat.vanilla",
                                                    "000004ec.dat.vanilla",
                                                    "000004ed.dat.vanilla",
                                                    "000004ef.dat.vanilla",
                                                    "000004f0.dat.vanilla",
                                                    "000004f1.dat.vanilla",
                                                    "000004f2.dat.vanilla",
                                                    "000004f3.dat.vanilla",
                                                    "000004f4.dat.vanilla",
                                                    "000004f5.dat.vanilla",
                                                    "000004f6.dat.vanilla",
                                                    "000004f7.dat.vanilla",
                                                    "000004f8.dat.vanilla",
                                                     };

                                string[] fileNames2 = {
                                                    "000004e9.dat",
                                                    "000004ea.dat",
                                                    "000004eb.dat",
                                                    "000004ec.dat",
                                                    "000004ed.dat",
                                                    "000004ef.dat",
                                                    "000004f0.dat",
                                                    "000004f1.dat",
                                                    "000004f2.dat",
                                                    "000004f3.dat",
                                                    "000004f4.dat",
                                                    "000004f5.dat",
                                                    "000004f6.dat",
                                                    "000004f7.dat",
                                                    "000004f8.dat",
                                                     };

                                wiiInventoryWithFolder("vanilla", fileNames, fileNames2);

                                registro.Items.Add("Disabled Wii Inventory layout Mod");
                                MessageBox.Show("Wii Inventory layout Disabled!");

                                disableWILtoolStripMenuItem1.Checked = !disableWILtoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem1.Checked = false;
                            }
                        }
                        else //Se la cartella non esiste
                        {
                            DialogResult result = MessageBox.Show("Backup file detected.\nRestore from backup file?\nCancel to restore from Vanilla game", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {

                                string[] fileNames = {
                                                    "000004e9.dat.bak",
                                                    "000004ea.dat.bak",
                                                    "000004eb.dat.bak",
                                                    "000004ec.dat.bak",
                                                    "000004ed.dat.bak",
                                                    "000004ef.dat.bak",
                                                    "000004f0.dat.bak",
                                                    "000004f1.dat.bak",
                                                    "000004f2.dat.bak",
                                                    "000004f3.dat.bak",
                                                    "000004f4.dat.bak",
                                                    "000004f5.dat.bak",
                                                    "000004f6.dat.bak",
                                                    "000004f7.dat.bak",
                                                    "000004f8.dat.bak",
                                                     };

                                string[] fileNames2 = {
                                                    "000004e9.dat",
                                                    "000004ea.dat",
                                                    "000004eb.dat",
                                                    "000004ec.dat",
                                                    "000004ed.dat",
                                                    "000004ef.dat",
                                                    "000004f0.dat",
                                                    "000004f1.dat",
                                                    "000004f2.dat",
                                                    "000004f3.dat",
                                                    "000004f4.dat",
                                                    "000004f5.dat",
                                                    "000004f6.dat",
                                                    "000004f7.dat",
                                                    "000004f8.dat",
                                                     };

                                wiiInventoryWithOutFolder("bak", fileNames, fileNames2);

                                registro.Items.Add("Disabled Wii Inventory layout Mod");
                                MessageBox.Show("Wii Inventory layout Disabled!");

                                disableWILtoolStripMenuItem1.Checked = !disableWILtoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem1.Checked = false;
                            }
                            else //se repacko vanilla
                            {

                                string[] fileNames = {
                                                    "000004e9.dat.vanilla",
                                                    "000004ea.dat.vanilla",
                                                    "000004eb.dat.vanilla",
                                                    "000004ec.dat.vanilla",
                                                    "000004ed.dat.vanilla",
                                                    "000004ef.dat.vanilla",
                                                    "000004f0.dat.vanilla",
                                                    "000004f1.dat.vanilla",
                                                    "000004f2.dat.vanilla",
                                                    "000004f3.dat.vanilla",
                                                    "000004f4.dat.vanilla",
                                                    "000004f5.dat.vanilla",
                                                    "000004f6.dat.vanilla",
                                                    "000004f7.dat.vanilla",
                                                    "000004f8.dat.vanilla",
                                                     };

                                string[] fileNames2 = {
                                                    "000004e9.dat",
                                                    "000004ea.dat",
                                                    "000004eb.dat",
                                                    "000004ec.dat",
                                                    "000004ed.dat",
                                                    "000004ef.dat",
                                                    "000004f0.dat",
                                                    "000004f1.dat",
                                                    "000004f2.dat",
                                                    "000004f3.dat",
                                                    "000004f4.dat",
                                                    "000004f5.dat",
                                                    "000004f6.dat",
                                                    "000004f7.dat",
                                                    "000004f8.dat",
                                                     };


                                wiiInventoryWithOutFolder("vanilla", fileNames, fileNames2);

                                registro.Items.Add("Disabled Wii Inventory layout Mod");
                                MessageBox.Show("Wii Inventory layout Disabled!");

                                disableWILtoolStripMenuItem1.Checked = !disableWILtoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem1.Checked = false;

                            }
                        }

                    }
                    else //se backup non esiste entra qua
                    {

                        if (Directory.Exists(gamePath + "\\datapack"))
                        {
                            DialogResult result = MessageBox.Show("Backup file not detected.\nRestore from vanilla game?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {

                                string[] fileNames = {
                                                    "000004e9.dat.vanilla",
                                                    "000004ea.dat.vanilla",
                                                    "000004eb.dat.vanilla",
                                                    "000004ec.dat.vanilla",
                                                    "000004ed.dat.vanilla",
                                                    "000004ef.dat.vanilla",
                                                    "000004f0.dat.vanilla",
                                                    "000004f1.dat.vanilla",
                                                    "000004f2.dat.vanilla",
                                                    "000004f3.dat.vanilla",
                                                    "000004f4.dat.vanilla",
                                                    "000004f5.dat.vanilla",
                                                    "000004f6.dat.vanilla",
                                                    "000004f7.dat.vanilla",
                                                    "000004f8.dat.vanilla",
                                                     };

                                string[] fileNames2 = {
                                                    "000004e9.dat",
                                                    "000004ea.dat",
                                                    "000004eb.dat",
                                                    "000004ec.dat",
                                                    "000004ed.dat",
                                                    "000004ef.dat",
                                                    "000004f0.dat",
                                                    "000004f1.dat",
                                                    "000004f2.dat",
                                                    "000004f3.dat",
                                                    "000004f4.dat",
                                                    "000004f5.dat",
                                                    "000004f6.dat",
                                                    "000004f7.dat",
                                                    "000004f8.dat",
                                                     };

                                wiiInventoryWithFolder("vanilla", fileNames, fileNames2);

                                registro.Items.Add("Disabled Wii Inventory layout Mod");
                                MessageBox.Show("Wii Inventory layout Disabled!");

                                disableWILtoolStripMenuItem1.Checked = !disableWILtoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem1.Checked = false;

                            }
                        }
                        else //Se la cartella non esiste
                        {
                            DialogResult result = MessageBox.Show("Backup file not detected.\nRestore from vanilla game?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {

                                string[] fileNames = {
                                                    "000004e9.dat.vanilla",
                                                    "000004ea.dat.vanilla",
                                                    "000004eb.dat.vanilla",
                                                    "000004ec.dat.vanilla",
                                                    "000004ed.dat.vanilla",
                                                    "000004ef.dat.vanilla",
                                                    "000004f0.dat.vanilla",
                                                    "000004f1.dat.vanilla",
                                                    "000004f2.dat.vanilla",
                                                    "000004f3.dat.vanilla",
                                                    "000004f4.dat.vanilla",
                                                    "000004f5.dat.vanilla",
                                                    "000004f6.dat.vanilla",
                                                    "000004f7.dat.vanilla",
                                                    "000004f8.dat.vanilla",
                                                     };

                                string[] fileNames2 = {
                                                    "000004e9.dat",
                                                    "000004ea.dat",
                                                    "000004eb.dat",
                                                    "000004ec.dat",
                                                    "000004ed.dat",
                                                    "000004ef.dat",
                                                    "000004f0.dat",
                                                    "000004f1.dat",
                                                    "000004f2.dat",
                                                    "000004f3.dat",
                                                    "000004f4.dat",
                                                    "000004f5.dat",
                                                    "000004f6.dat",
                                                    "000004f7.dat",
                                                    "000004f8.dat",
                                                     };


                                wiiInventoryWithFolder("vanilla", fileNames, fileNames2);

                                registro.Items.Add("Disabled Wii Inventory layout Mod");
                                MessageBox.Show("Wii Inventory layout Disabled!");

                                disableWILtoolStripMenuItem1.Checked = !disableWILtoolStripMenuItem1.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem1.Checked = false;

                            }
                        }
                    }
                }
                this.Focus();
            }

        }






        private void wiiInventoryWithFolder(String tipo, String[] fileNames, String[] fileNames2)
        {
            //devo rinominare la cartella datapack in datapackTEMP SE ESISTE PERO' 
            Directory.Move(gamePath + "\\datapack", gamePath + "\\datapackTEMP");
            Thread.Sleep(2000);
            //creare la cartella datapack
            Directory.CreateDirectory(gamePath + "\\datapack");
            Thread.Sleep(1500);
            //metterci dentro i file

            if (tipo == "dat")
            {
                //foreach (string fileName in fileNames)
                //{
                //    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\mod\\" + fileName, gamePath + "\\datapack" + "\\" + fileNames, true);

                //}

                for (int i = 0; i < fileNames.Length; i++)
                {
                    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\mod\\" + fileNames[i], gamePath + "\\datapack" + "\\" + fileNames[i], true);
                }

            }
            else if (tipo == "bak")
            {
                //foreach (string fileName in fileNames)
                //{
                //    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\backup\\" + fileName, gamePath + "\\datapack" + "\\" + fileNames2, true);

                //}

                for (int i = 0; i < fileNames.Length; i++)
                {
                    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\backup\\" + fileNames[i], gamePath + "\\datapack" + "\\" + fileNames2[i], true);
                }
            }
            else if (tipo == "vanilla")
            {
                //foreach (string fileName in fileNames)
                //{
                //    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\vanilla\\" + fileName, gamePath + "\\datapack" + "\\" + fileNames2, true);

                //}

                for (int i = 0; i < fileNames.Length; i++)
                {
                    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\vanilla\\" + fileNames[i], gamePath + "\\datapack" + "\\" + fileNames2[i], true);
                }
            }

            Thread.Sleep(2500);
            //repackare
            repack("datapack");
            Thread.Sleep(2500);
            //cancellare la cartella datapack
            Directory.Delete(gamePath + "\\datapack", true);
            Thread.Sleep(1500);
            //rinominare datapackTEMP in datapack
            Directory.Move(gamePath + "\\datapackTEMP", gamePath + "\\datapack");
            //registro.Items.Add("Enabled Wii Main Menu Mod");


        }

        private void wiiInventoryWithOutFolder(String tipo, String[] fileNames, String[] fileNames2)
        {
            //devo rinominare la cartella datapack in datapackTEMP SE ESISTE PERO' 
            //Directory.Move(gamePath + "\\datapack", gamePath + "\\datapackTEMP");
            Thread.Sleep(1000);
            //creare la cartella datapack
            Directory.CreateDirectory(gamePath + "\\datapack");
            Thread.Sleep(1500);
            //metterci dentro i file

            if (tipo == "dat")
            {
                //foreach (string fileName in fileNames)
                //{
                //    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\mod\\" + fileName, gamePath + "\\datapack" + "\\" + fileNames, true);

                //}

                for (int i = 0; i < fileNames.Length; i++)
                {
                    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\mod\\" + fileNames[i], gamePath + "\\datapack" + "\\" + fileNames[i], true);
                }
            }
            else if (tipo == "bak")
            {
                //foreach (string fileName in fileNames)
                //{
                //    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\backup\\" + fileName, gamePath + "\\datapack" + "\\" + fileNames2, true);

                //}

                for (int i = 0; i < fileNames.Length; i++)
                {
                    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\backup\\" + fileNames[i], gamePath + "\\datapack" + "\\" + fileNames2[i], true);
                }
            }
            else if (tipo == "vanilla")
            {
                //foreach (string fileName in fileNames)
                //{
                //    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\vanilla\\" + fileName, gamePath + "\\datapack" + "\\" + fileNames2, true);

                //}

                for (int i = 0; i < fileNames.Length; i++)
                {
                    File.Copy(toolPath + "\\Mods" + "\\Wii Inventory" + "\\vanilla\\" + fileNames[i], gamePath + "\\datapack" + "\\" + fileNames2[i], true);
                }
            }
            Thread.Sleep(2000);
            //repackare
            repack("datapack");
            Thread.Sleep(2500);
            //cancellare la cartella datapack
            Directory.Delete(gamePath + "\\datapack", true);
            Thread.Sleep(1500);
            //rinominare datapackTEMP in datapack
            //Directory.Move(gamePath + "\\datapackTEMP", gamePath + "\\datapack");
            //registro.Items.Add("Enabled Wii Main Menu Mod");


        }




        private void madeByToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://shorturl.at/uEY89"); //shortened link cause full doesn't work
        }

        private void previewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*

            Form imageForm = new Form();
            imageForm.Text = "Wii inventory layout preview";
            imageForm.Size = new Size(960, 570);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = System.Drawing.Image.FromFile(preview1);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            imageForm.Controls.Add(pictureBox);

            imageForm.ShowDialog();*/
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           

            Form imageForm = new Form();
            imageForm.Text = "Wii inventory layout preview";
            imageForm.Size = new Size(960, 570);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            //pictureBox.Image = System.Drawing.Image.FromFile(preview1);
            pictureBox.Image = Properties.Resources.INV1;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            imageForm.Controls.Add(pictureBox);

            imageForm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
           

            Form imageForm = new Form();
            imageForm.Text = "Wii inventory layout preview";
            imageForm.Size = new Size(960, 570);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            // pictureBox.Image = System.Drawing.Image.FromFile(preview2);
            pictureBox.Image = Properties.Resources.INV2;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            imageForm.Controls.Add(pictureBox);

            imageForm.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            


            Form imageForm = new Form();
            imageForm.Text = "Wii inventory layout preview";
            imageForm.Size = new Size(960, 570);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            //pictureBox.Image = System.Drawing.Image.FromFile(preview3);
            pictureBox.Image = Properties.Resources.INV3;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            imageForm.Controls.Add(pictureBox);

            imageForm.ShowDialog();
        }

        private void madeByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://rb.gy/yty1e5"); //shortened link cause full doesn't work

        }

        private void madeByBioShip0706ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://www.youtube.com/@BioShip0706");
        }

        private void moddingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void wiiMainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //mi dice il contenuto in quel punto, quindi "ogre"
        {
            String filePath = gamePath + "\\datapack\\" + "0000024c.dat";
            int lineNumber = 187;
            int startCharIndex = 209;
            int endCharIndex = 213;

            // Leggi il contenuto della riga specificata
            string lineContent = ReadLineContent(filePath, lineNumber);

            if (lineContent != null)
            {
                // Estrai la sottostringa specificata
                string extractedSubstring = lineContent.Substring(startCharIndex - 1, endCharIndex - startCharIndex + 1);
                Console.WriteLine("Contenuto estratto: " + extractedSubstring);
                MessageBox.Show(extractedSubstring);
            }
            else
            {
                Console.WriteLine("La riga specificata non è valida o il file non esiste.");
            }
        }



        static string ReadLineContent(string filePath, int lineNumber)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                for (int i = 1; i <= lineNumber; i++)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        // Se il file termina prima della riga specificata, restituisci null
                        return null;
                    }
                    else if (i == lineNumber)
                    {
                        // Se è la riga desiderata, restituisci il contenuto della riga
                        return line;
                    }
                }
            }

            // Se il numero di riga è maggiore del numero di righe nel file, restituisci null
            return null;
        }

        private void button5_Click(object sender, EventArgs e) //rimpiazza ogre con qualcosa di random tra homm,prog,shom però non worka bene perchè va a perdere i caratteri speciali.
        {
            //    String filePath = gamePath + "\\datapack\\" + "0000024c.dat";
            //    int lineNumber = 187;
            //    int startCharIndex = 209;
            //    int endCharIndex = 213;
            //    //string replacementText = "homm";

            //    string[] replacementOptions = { "homm", "prog", "shom" };
            //    Random random = new Random();
            //    string replacementText = replacementOptions[random.Next(0, replacementOptions.Length)];


            //    // Controlla se il file esiste
            //    if (!File.Exists(filePath))
            //    {
            //        Console.WriteLine("Il file non esiste.");
            //        return;
            //    }

            //    // Leggi la riga specificata
            //    string[] lines = File.ReadAllLines(filePath);
            //    if (lineNumber >= 1 && lineNumber <= lines.Length)
            //    {
            //        string originalLine = lines[lineNumber - 1];

            //        // Modifica la sottostringa specificata
            //        string modifiedLine = originalLine.Substring(0, startCharIndex - 1) +
            //                              replacementText +
            //                              originalLine.Substring(endCharIndex);

            //        // Assegna la riga modificata all'array di linee
            //        lines[lineNumber - 1] = modifiedLine;

            //        // Sovrascrivi il file con la riga modificata
            //        File.WriteAllLines(filePath, lines);
            //        Thread.Sleep(1500);
            //        repack("datapack");
            //        Console.WriteLine("Il file è stato modificato con successo.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("La riga specificata non è valida.");
            //    }

        }



        private void button6_Click(object sender, EventArgs e) //BOTTONE PER RANDOMIZZARE NEMICI
        {
            //NOTE IMPORTANTISSIME
            //Non randomizzare nemici di 3 lettere con 4 lettere, perchè non fa per ora
            //Non randomizzare una stanza con solo progs con altri nemici, ANIMAZIONI MANCANTI = SOFTLOCK
            //Non randomizzare una stanza che non contiene originariamente moth, con moth perchè ANIMAZIONI MANCANTI = SOFTLOCK
            //Controllare prima di randomizzare le fly perchè i nemici potrebbero crearsi incastrati.
            //Non raccogliere cuori dei nemici da stanze che non prevedevano questa cosa,   ANIMAZIONI MANCANTI = SOFTLOCK

            //byte[] hommVect = new byte[] { 0x68, 0x6F, 0x6D, 0x6D }; //h o m m 
            //byte[] progVect = new byte[] { 0x70, 0x72, 0x6F, 0x67 }; //p r o g
            //byte[] ramVect = new byte[] { 0x72, 0x61, 0x6D, 0x00 };  //r a m .
            //byte[] ogreVect = new byte[] { 0x6F, 0x67, 0x72, 0x65 }; //o g r e
            //byte[] flyVect = new byte[] { 0x66, 0x6C, 0x79, 0x00 };  //f l y .
            //byte[] mothVect = new byte[] { 0x6D, 0x6F, 0x74, 0x68 }; //m o t h
            //byte[] shomVect = new byte[] { 0x73, 0x68, 0x6F, 0x6D }; //s h o m

            //C102 hallway con ogre vicino biliardo
            randomizeEnemy(0x000136C2, "0000024c.dat", false, false, false); //ogre

            //C001 main hall di AOL con i ragni e il tizio tagliato a meta
            //randomizeEnemy(0x00017559, "000001fe.dat", false, false, false); //prog
            //Thread.Sleep(150);
            //randomizeEnemy(0x00017744, "000001fe.dat", false, false, false); //prog
            //Thread.Sleep(150);
            //randomizeEnemy(0x00017871, "000001fe.dat", false, false, false); //prog
            //Thread.Sleep(150);
            //randomizeEnemy(0x000179A0, "000001fe.dat", false, false, false); //prog

            //C005 libreria con mobili da muovere
            randomizeEnemy(0x00025176, "00000221.dat", false, false, false); //prog1
            Thread.Sleep(150);
            randomizeEnemy(0x000253F9, "00000221.dat", false, false, false); //prog2 , appare solo questo
            Thread.Sleep(150);
            //randomizeEnemy(0x00025508, "00000221.dat", false, false, false); //ram1 LIMITATO A FLY PER ORA. 4 LETTERE NO
            //Thread.Sleep(150);
            //randomizeEnemy(0x000256DB, "00000221.dat", false, false, false); //ram2 LIMITATO A FLY PER ORA. 4 LETTERE NO
            //Thread.Sleep(150);

            //C002 2 ogre e 2 ram con taser box (1 ogre appare quando si torna con stan e shannon)
            randomizeEnemy(0x0001A692, "00000207.dat", false, false, false); //ogre1 questo è dietro il muro da sfondare
            Thread.Sleep(150);
            //randomizeEnemy(0x0001AAD9, "00000207.dat", false, false, false); //ogre2 questo appare al ritorno. se si sostituisce appare subito, forse modificare solo in hardmode
            //randomizeEnemy(0x0001A7F1, "00000207.dat", false, false, false); //ram1 LIMITATO A FLY PER ORA. 4 LETTERE NO
            //randomizeEnemy(0x0001A8FE, "00000207.dat", false, false, false); //ram2 LIMITATO A FLY PER ORA. 4 LETTERE NO

            //C004 DA FARE stanza con riflettori
            //randomizeEnemy(0x0002B944, "00000217.dat", false, false, false); //ram LIMITATO A FLY PER ORA. 4 LETTERE NO
            //randomizeEnemy(0x0002BD4F, "00000217.dat", false, false, false); //ram LIMITATO A FLY PER ORA. 4 LETTERE NO
            //randomizeEnemy(0x0002BE60, "00000217.dat", false, false, false); //ram LIMITATO A FLY PER ORA. 4 LETTERE NO
            randomizeEnemy(0x0002BF6B, "00000217.dat", false, false, false); //moth vedere se lasciare o no o solo in hardmode bo
            Thread.Sleep(150);
            randomizeEnemy(0x0002C080, "00000217.dat", false, false, false); //prog se si lascia moth lasciare anche lui

            //C007 stanza professori con tanti prog NON FA PERCHè ANIMAZIONI DI IMPATTO MANCANTI
            //randomizeEnemy(0x000C3B7, "00000233.dat", false, false, false); //prog 
            //Thread.Sleep(150);
            //randomizeEnemy(0x000C4F3, "00000233.dat", false, false, false); //prog 
            //Thread.Sleep(150);
            //randomizeEnemy(0x000C606, "00000233.dat", false, false, false); //prog 
            //Thread.Sleep(150);
            //randomizeEnemy(0x000C71A, "00000233.dat", false, false, false); //prog 
            //Thread.Sleep(150);
            //randomizeEnemy(0x000C82E, "00000233.dat", false, false, false); //prog 


            //D000
            randomizeEnemy(0x000440B0, "00000267.dat", false, false, false); //Ogre
            //randomizeEnemy(, "", false, false, false); //ram1
            //randomizeEnemy(, "", false, false, false); //ram2

            //B001
            //randomizeEnemy(, "", false, false, false); //Fly



            //I003


            //E000 arrampicata per ospedlae con corey e amy e fly
            //randomizeEnemy(1, "00000299.dat", false, false, false); //fly

            //J003 2 Ram rimossi (vedere se si possono restorare)
            //randomizeEnemy(1, "00000480.dat", false, false, false); //ram1
            //randomizeEnemy(1, "00000480.dat", false, false, false); //ram2
            MessageBox.Show("Done!");




        }

        public static void randomizeEnemy(Int32 offset, String filename, bool includeFly, bool includeMoth, bool hardMode)
        {
            byte[] hommVect = new byte[] { 0x68, 0x6F, 0x6D, 0x6D }; //h o m m 
            byte[] progVect = new byte[] { 0x70, 0x72, 0x6F, 0x67 }; //p r o g
            byte[] ramVect = new byte[] { 0x72, 0x61, 0x6D, 0x00 };  //r a m .
            byte[] ogreVect = new byte[] { 0x6F, 0x67, 0x72, 0x65 }; //o g r e
            byte[] flyVect = new byte[] { 0x66, 0x6C, 0x79, 0x00 };  //f l y .
            byte[] mothVect = new byte[] { 0x6D, 0x6F, 0x74, 0x68 }; //m o t h
            byte[] shomVect = new byte[] { 0x73, 0x68, 0x6F, 0x6D }; //s h o m

            int replacementEnemyNumber = 0;
            Random rand = new Random();

            string specifiedfile = gamePath + "\\datapack\\" + filename;

            using (BinaryWriter bw = new BinaryWriter(File.Open(specifiedfile, FileMode.Open)))
            {
                bw.Seek(offset, SeekOrigin.Begin); //inizia dal byte iniziale

                byte firstByte = (byte)bw.BaseStream.ReadByte(); //read the first byte to understand which enemy is there originally.

                bw.Seek(offset, SeekOrigin.Begin); //risetto dall'inizio perchè l'istruzione sopra readByte mi skippa un byte

                //ho letto, ho trovato il primo byte, ora faccio lo switch, e randomizzo senza quel nemico originale

                List<int> numeriList = new List<int> { 1, 2, 3, 4, 5, 6, 7 }; //dichiaro i numeri delle scelte, con tutti, poi tolgo in base al nemico originale

                switch (firstByte) //cosi rimuovo il nemico originale dalla lista
                {
                    case 0x68: //homm
                        numeriList.Remove(1);
                        break;

                    case 0x70: //prog
                        numeriList.Remove(2);
                        break;

                    case 0x72: //ram
                        numeriList.Remove(3);
                        break;

                    case 0x6F: //ogre
                        numeriList.Remove(4);
                        break;

                    case 0x73: //shom
                        numeriList.Remove(5);
                        break;

                    case 0x66: //fly
                        numeriList.Remove(6);
                        break;

                    case 0x6D: //moth
                        numeriList.Remove(7);
                        break;

                    default:

                        return;
                }

                if (includeFly && includeMoth && !hardMode)
                {

                }
                else if (!includeFly && !includeMoth && !hardMode)
                {
                    numeriList.Remove(6); //rimuove fly
                    numeriList.Remove(7); //rimuove moth
                }
                else if (!includeFly && !hardMode)
                {
                    numeriList.Remove(6); //rimuovi fly
                }
                else if (!includeMoth && !hardMode)
                {
                    numeriList.Remove(7); //rimuovi moth
                }

                replacementEnemyNumber = numeriList[rand.Next(0, numeriList.Count)];


                switch (replacementEnemyNumber)
                {
                    case 1:
                        bw.Write(hommVect);
                        break;
                    case 2:
                        bw.Write(progVect);
                        break;
                    case 3:
                        bw.Write(ramVect);
                        break;
                    case 4:
                        bw.Write(ogreVect);
                        break;
                    case 5:
                        bw.Write(shomVect);
                        break;
                    case 6:
                        bw.Write(flyVect);
                        break;
                    case 7:
                        bw.Write(mothVect);
                        break;
                }






            }

            //bw.Write(ramVect);
        }

        private void cheatsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void restoresAnHiddenButtonOnThePauseMenuThatAllowsYouToSaveAnywhereYouAreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void enableToolStripMenuItem2_Click(object sender, EventArgs e) //ENABLE SAVE EVERYWHERE CHEAT
        {
            if (!enableToolStripMenuItem2.Checked)
            {
                DialogResult proseguire = MessageBox.Show("Enable Save anywhere cheat?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (proseguire == DialogResult.Yes)
                {
                    if (Directory.Exists(gamePath + "\\datapack"))
                    {
                        Directory.Move(gamePath + "\\datapack", gamePath + "\\datapackTEMP"); //rinomino datapack
                        Thread.Sleep(2000);

                        //creare la cartella datapack
                        Directory.CreateDirectory(gamePath + "\\datapack");
                        Thread.Sleep(1500);
                        //metterci dentro i file

                        File.Copy(toolPath + "\\Cheats" + "\\SaveEverywhere" + "\\" + "000004c0.dat", gamePath + "\\datapack" + "\\" + "000004c0.dat", true);
                        Thread.Sleep(1500);

                        //Modificare file ora
                        using (BinaryWriter bw = new BinaryWriter(File.Open(gamePath + "\\datapack\\" + "000004c0.dat", FileMode.Open)))
                        {
                            bw.Seek(0x00004472, SeekOrigin.Begin); //inizia dal byte iniziale
                            bw.Write((byte)0x30);
                            Thread.Sleep(1500);
                            bw.Seek(0x0000466E, SeekOrigin.Begin); //inizia dal byte iniziale
                            bw.Write((byte)0x30);
                        }

                        Thread.Sleep(2000);
                        repack("datapack");
                        Thread.Sleep(1500);

                        Directory.Delete(gamePath + "\\datapack", true);
                        Thread.Sleep(1500);

                        Directory.Move(gamePath + "\\datapackTEMP", gamePath + "\\datapack");

                        enableToolStripMenuItem2.Checked = true;
                        disableToolStripMenuItem.Checked = false;

                        registro.Items.Add("Enabled Save everywhere cheat");

                        MessageBox.Show("Enabled Save everywhere cheat");

                    }
                    else
                    {
                        //creare la cartella datapack
                        Directory.CreateDirectory(gamePath + "\\datapack");
                        Thread.Sleep(1500);
                        //metterci dentro i file

                        File.Copy(toolPath + "\\Cheats" + "\\SaveEverywhere" + "\\" + "000004c0.dat", gamePath + "\\datapack" + "\\" + "000004c0.dat", true);
                        Thread.Sleep(1500);

                        //Modificare file ora
                        using (BinaryWriter bw = new BinaryWriter(File.Open(gamePath + "\\datapack\\" + "000004c0.dat", FileMode.Open)))
                        {
                            bw.Seek(0x00004472, SeekOrigin.Begin); //inizia dal byte iniziale
                            bw.Write((byte)0x30);
                            Thread.Sleep(1500);
                            bw.Seek(0x0000466E, SeekOrigin.Begin); //inizia dal byte iniziale
                            bw.Write((byte)0x30);
                        }

                        Thread.Sleep(2000);
                        repack("datapack");
                        Thread.Sleep(1500);

                        Directory.Delete(gamePath + "\\datapack", true);
                        Thread.Sleep(1500);

                        enableToolStripMenuItem2.Checked = true;
                        disableToolStripMenuItem.Checked = false;

                        registro.Items.Add("Enabled Save everywhere cheat");

                        MessageBox.Show("Enabled Save everywhere cheat");

                    }
                }
                this.Focus();
            }


        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e) //DISABLE SAVE EVERYWHERE CHEAT
        {
            if (!disableToolStripMenuItem.Checked)
            {
                DialogResult proseguire = MessageBox.Show("Disable Save anywhere cheat?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (proseguire == DialogResult.Yes)
                {
                    if (Directory.Exists(gamePath + "\\datapack"))
                    {
                        Directory.Move(gamePath + "\\datapack", gamePath + "\\datapackTEMP"); //rinomino datapack
                        Thread.Sleep(2000);

                        //creare la cartella datapack
                        Directory.CreateDirectory(gamePath + "\\datapack");
                        Thread.Sleep(1500);
                        //metterci dentro i file

                        File.Copy(toolPath + "\\Cheats" + "\\SaveEverywhere" + "\\" + "000004c0.dat", gamePath + "\\datapack" + "\\" + "000004c0.dat", true);
                        Thread.Sleep(1500);

                        //Modificare file ora
                        using (BinaryWriter bw = new BinaryWriter(File.Open(gamePath + "\\datapack\\" + "000004c0.dat", FileMode.Open)))
                        {
                            bw.Seek(0x00004472, SeekOrigin.Begin); //inizia dal byte iniziale
                            bw.Write((byte)0x31);
                            Thread.Sleep(1500);
                            bw.Seek(0x0000466E, SeekOrigin.Begin); //inizia dal byte iniziale
                            bw.Write((byte)0x31);
                        }

                        Thread.Sleep(2000);
                        repack("datapack");
                        Thread.Sleep(1500);

                        Directory.Delete(gamePath + "\\datapack", true);
                        Thread.Sleep(1500);

                        Directory.Move(gamePath + "\\datapackTEMP", gamePath + "\\datapack");

                        disableToolStripMenuItem.Checked = true;
                        enableToolStripMenuItem2.Checked = false;


                        registro.Items.Add("Disabled Save everywhere cheat");

                        MessageBox.Show("Disabled Save everywhere cheat");

                    }
                    else
                    {
                        //creare la cartella datapack
                        Directory.CreateDirectory(gamePath + "\\datapack");
                        Thread.Sleep(1500);
                        //metterci dentro i file

                        File.Copy(toolPath + "\\Cheats" + "\\SaveEverywhere" + "\\" + "000004c0.dat", gamePath + "\\datapack" + "\\" + "000004c0.dat", true);
                        Thread.Sleep(1500);

                        //Modificare file ora
                        using (BinaryWriter bw = new BinaryWriter(File.Open(gamePath + "\\datapack\\" + "000004c0.dat", FileMode.Open)))
                        {
                            bw.Seek(0x00004472, SeekOrigin.Begin); //inizia dal byte iniziale
                            bw.Write((byte)0x31);
                            Thread.Sleep(1500);
                            bw.Seek(0x0000466E, SeekOrigin.Begin); //inizia dal byte iniziale
                            bw.Write((byte)0x31);
                        }

                        Thread.Sleep(2000);
                        repack("datapack");
                        Thread.Sleep(1500);

                        Directory.Delete(gamePath + "\\datapack", true);
                        Thread.Sleep(1500);

                        disableToolStripMenuItem.Checked = true;
                        enableToolStripMenuItem2.Checked = false;

                        registro.Items.Add("Disabled Save everywhere cheat");

                        MessageBox.Show("Disabled Save everywhere cheat");

                    }
                }
                this.Focus();
            }
        }


        private void enableToolStripMenuItem3_Click(object sender, EventArgs e) //Enable Hydravision Intro 
        {
            // AGGIUNGERE CONTROLLO SE ESISTE KINEPACK.HVP PRIMA DI FARE IL TUTTO
            if (!enableToolStripMenuItem3.Checked)
            {
                DialogResult proseguire = MessageBox.Show("Enable Hydravision Entertainment intro?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (proseguire == DialogResult.Yes)
                {
                    if (Directory.Exists(gamePath + "\\kinepack"))
                    {
                        DialogResult result = MessageBox.Show("Kinepack folder detected. \nThis mod will replace the 00000010.dat file of Kinepack. \nCreate a backup of this file and Proceed?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            if (!File.Exists(gamePath + "\\kinepack" + "\\00000010.dat"))
                            {
                                Directory.Move(gamePath + "\\kinepack", gamePath + "\\kinepackTEMP"); //rinomino kinepack
                                Thread.Sleep(2000);

                                unpack("kinepack");
                                Thread.Sleep(5000);

                                File.Copy(gamePath + "\\kinepack" + "\\00000010.dat", toolPath + "\\Mods" + "\\HydravisionIntro" + "\\00000010.dat.bak", true);
                                Thread.Sleep(1500);

                                Directory.Delete(gamePath + "\\kinepack", true);
                                Thread.Sleep(1500);

                                Directory.Move(gamePath + "\\kinepackTEMP", gamePath + "\\kinepack");
                                Thread.Sleep(1000);
                                registro.Items.Add("Created backup of Kinepack file 00000010.dat");
                                MessageBox.Show("Created backup of Kinepack file 00000010.dat");
                            }
                            else
                            {
                                File.Copy(gamePath + "\\kinepack" + "\\00000010.dat", toolPath + "\\Mods" + "\\HydravisionIntro" + "\\00000010.dat.bak", true);
                                Thread.Sleep(1000);
                                registro.Items.Add("Created backup of Kinepack file 00000010.dat");
                                MessageBox.Show("Created backup of Kinepack file 00000010.dat");
                            }

                            hydravisionIntroWithFolder("00000010.dat", "00000010.dat");

                            registro.Items.Add("Enabled Hydravision Intro mod");
                            MessageBox.Show("Enabled Hydravision Intro mod!");

                            enableToolStripMenuItem3.Checked = !enableToolStripMenuItem3.Checked; //SPUNTA ENABLED
                            disableToolStripMenuItem2.Checked = false;

                        }
                    }
                    else //se non esiste la cartella
                    {
                        DialogResult result = MessageBox.Show("Kinepack folder not detected. \nThis mod will replace the 00000010.dat file of Kinepack. \nCreate a backup of this file and Proceed?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {

                            unpack("kinepack");
                            Thread.Sleep(2000);

                            File.Copy(gamePath + "\\kinepack" + "\\00000010.dat", toolPath + "\\Mods" + "\\HydravisionIntro" + "\\00000010.dat.bak", true);
                            Thread.Sleep(1500);

                            Directory.Delete(gamePath + "\\kinepack", true);
                            Thread.Sleep(1500);


                            registro.Items.Add("Created backup of Kinepack file 00000010.dat");
                            MessageBox.Show("Created backup of Kinepack file 00000010.dat");


                            hydravisionIntroWithOutFolder("00000010.dat", "00000010.dat");

                            registro.Items.Add("Enabled Hydravision Intro mod");
                            MessageBox.Show("Enabled Hydravision Intro mod!");

                            enableToolStripMenuItem3.Checked = !enableToolStripMenuItem3.Checked; //SPUNTA ENABLED
                            disableToolStripMenuItem2.Checked = false;

                        }
                    }
                }
                this.Focus();
            }
        }





        private void hydravisionIntroWithFolder(String sourceFile, String destFile)
        {
            //devo rinominare la cartella kinepack in kinepackTEMP SE ESISTE PERO' 
            Directory.Move(gamePath + "\\kinepack", gamePath + "\\kinepackTEMP");
            Thread.Sleep(1000);
            //creare la cartella kinepack
            Directory.CreateDirectory(gamePath + "\\kinepack");
            Thread.Sleep(1000);
            //metterci dentro il file 0000000f.dat
            File.Copy(toolPath + "\\Mods" + "\\HydravisionIntro" + "\\" + sourceFile, gamePath + "\\kinepack" + "\\" + destFile, true);
            Thread.Sleep(1500);
            //repackare
            repack("kinepack");
            Thread.Sleep(1500);
            //cancellare la cartella kinepack
            Directory.Delete(gamePath + "\\kinepack", true);
            Thread.Sleep(1000);
            //rinominare kinepackTEMP in kinepack
            Directory.Move(gamePath + "\\kinepackTEMP", gamePath + "\\kinepack");
            //registro.Items.Add("Enabled Wii Main Menu Mod");


        }

        private void hydravisionIntroWithOutFolder(String sourceFile, String destFile)
        {
            //creare la cartella kinepack
            Directory.CreateDirectory(gamePath + "\\kinepack");
            Thread.Sleep(1000);
            //metterci dentro il file 0000000f.dat
            File.Copy(toolPath + "\\Mods" + "\\HydravisionIntro" + "\\" + sourceFile, gamePath + "\\kinepack" + "\\" + destFile, true);
            Thread.Sleep(1500);
            //repackare
            repack("kinepack");
            Thread.Sleep(1500);
            //cancellare la cartella kinepack
            Directory.Delete(gamePath + "\\kinepack", true);
            Thread.Sleep(1000);
            //rinominare kinepackTEMP in kinepack
            //Directory.Move(gamePath + "\\kinepackTEMP", gamePath + "\\kinepack");
            //registro.Items.Add("Enabled Wii Main Menu Mod");


        }



        private void disableToolStripMenuItem2_Click(object sender, EventArgs e) //Disable Hydravision Intro 
        {
            if (!disableToolStripMenuItem2.Checked)
            {
                DialogResult proseguire = MessageBox.Show("Disable Hydravision Entertainment intro?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (proseguire == DialogResult.Yes)
                {
                    if (File.Exists(toolPath + "\\Mods" + "\\HydravisionIntro" + "\\00000010.dat.bak"))
                    {
                        if (Directory.Exists(gamePath + "\\kinepack"))
                        {
                            DialogResult result = MessageBox.Show("Backup file detected.\nRestore from backup?\nCancel to restore from Vanilla game", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                hydravisionIntroWithFolder("00000010.dat.bak", "00000010.dat");

                                registro.Items.Add("Disabled Hydravision Intro mod");
                                MessageBox.Show("Disabled Hydravision Intro mod");

                                disableToolStripMenuItem2.Checked = !disableToolStripMenuItem2.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem3.Checked = false;
                            }
                            else //se repacko vanilla
                            {
                                hydravisionIntroWithFolder("00000010.dat.vanilla", "00000010.dat");
                                registro.Items.Add("Disabled Hydravision Intro mod");
                                MessageBox.Show("Disabled Hydravision Intro mod");

                                disableToolStripMenuItem2.Checked = !disableToolStripMenuItem2.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem3.Checked = false;
                            }
                        }
                        else //Se la cartella non esiste
                        {
                            DialogResult result = MessageBox.Show("Backup file detected.\nRestore from backup file?\nCancel to restore from Vanilla game", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                hydravisionIntroWithOutFolder("00000010.dat.bak", "00000010.dat");
                                registro.Items.Add("Disabled Hydravision Intro mod");
                                MessageBox.Show("Disabled Hydravision Intro mod");

                                disableToolStripMenuItem2.Checked = !disableToolStripMenuItem2.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem3.Checked = false;
                            }
                            else //se repacko vanilla
                            {
                                hydravisionIntroWithOutFolder("00000010.dat.vanilla", "00000010.dat");
                                registro.Items.Add("Disabled Hydravision Intro mod");
                                MessageBox.Show("Disabled Hydravision Intro mod");

                                disableToolStripMenuItem2.Checked = !disableToolStripMenuItem2.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem3.Checked = false;

                            }
                        }

                    }
                    else //se backup non esiste entra qua
                    {

                        if (Directory.Exists(gamePath + "\\kinepack"))
                        {
                            DialogResult result = MessageBox.Show("Backup file not detected.\nRestore from vanilla game?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                hydravisionIntroWithFolder("00000010.dat.vanilla", "00000010.dat");

                                registro.Items.Add("Disabled Hydravision Intro mod");
                                MessageBox.Show("Disabled Hydravision Intro mod");

                                disableToolStripMenuItem2.Checked = !disableToolStripMenuItem2.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem3.Checked = false;

                            }
                        }
                        else //Se la cartella non esiste
                        {
                            DialogResult result = MessageBox.Show("Backup file not detected.\nRestore from vanilla game?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.OK)
                            {
                                hydravisionIntroWithOutFolder("00000010.dat.vanilla", "00000010.dat");

                                registro.Items.Add("Disabled Hydravision Intro mod");
                                MessageBox.Show("Disabled Hydravision Intro mod");

                                disableToolStripMenuItem2.Checked = !disableToolStripMenuItem2.Checked; //SPUNTA DISABLED
                                enableToolStripMenuItem3.Checked = false;

                            }
                        }
                    }
                }
                this.Focus();
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void joinObscureDiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://discord.gg/gseBWq5xCf");
        }

        private void moreInfosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Check the modding tool folder,\nit contains the Cheat engine trainer and instructions on how to manually mod the game.\nRight click the 'Create Backup' button to backup\nall available HVP files", "More info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        }
    }

}
    
