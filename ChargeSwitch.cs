using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Microsoft.Win32.SafeHandles;
class ChargeApp : Form {
 [DllImport("kernel32.dll",CharSet=CharSet.Unicode,SetLastError=true)] static extern SafeFileHandle CreateFile(string n,uint a,uint s,IntPtr sec,uint c,uint f,IntPtr t);
 [DllImport("kernel32.dll",SetLastError=true)] static extern bool DeviceIoControl(SafeFileHandle h,uint c,byte[] i,uint il,byte[] o,uint ol,out uint r,IntPtr p);
 Label status = new Label();
 public ChargeApp(){ Text="ASUS Charge Switch"; ClientSize=new Size(430,285); FormBorderStyle=FormBorderStyle.FixedDialog; MaximizeBox=false; StartPosition=FormStartPosition.CenterScreen; Font=new Font("Segoe UI",10);
 var title=new Label {Text="Choose your charging limit",Location=new Point(22,20),AutoSize=true,Font=new Font("Segoe UI",15,FontStyle.Bold)}; Controls.Add(title);
 status.SetBounds(22,62,385,55);status.Text="Select a mode to apply it. No change is made on launch.";Controls.Add(status);
 AddButton("Smart charging ON - 80%",122,80);AddButton("Smart charging OFF - 100%",170,100);
 Controls.Add(new Label {Text="MyASUS or G-Helper may override this setting.\nSwitch back to 80% manually when you are done.",Location=new Point(22,225),Size=new Size(390,50)});
 }
 void AddButton(string text,int y,int limit){var b=new Button {Text=text,Location=new Point(22,y),Size=new Size(385,40)};b.Click+=(s,e)=>Apply(limit);Controls.Add(b);}
 void Apply(int limit){try {using(var h=CreateFile(@"\\.\ATKACPI",0xC0000000,3,IntPtr.Zero,3,0x80,IntPtr.Zero)){
 if(h.IsInvalid)throw new Win32Exception(Marshal.GetLastWin32Error(),"Cannot open ASUS control interface. Check that ASUS System Control Interface is installed.");
 byte[] input=new byte[16];Buffer.BlockCopy(new uint[]{0x53564544,8,0x00120057,(uint)limit},0,input,0,16);byte[] output=new byte[16];uint returned;
 if(!DeviceIoControl(h,0x0022240C,input,16,output,16,out returned,IntPtr.Zero))throw new Win32Exception(Marshal.GetLastWin32Error());
 if(returned<4 || BitConverter.ToInt32(output,0)!=1)throw new Exception("ASUS firmware did not confirm the change. Response: "+BitConverter.ToInt32(output,0));
 status.Text="ASUS accepted the "+limit+"% limit.\n"+(limit==100?"Full charging is allowed.":"Battery protection is enabled.");
 }}catch(Exception ex){status.Text="Setting could not be applied.";MessageBox.Show(this,ex.Message,"Charging control",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
 [STAThread] static void Main(){Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);Application.Run(new ChargeApp());}
}
