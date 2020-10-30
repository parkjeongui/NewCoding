using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp4
{
    public partial class Form1 : Form
    {
        public string GetToken(int n, string str, string sep = ",") // ","
        {
            int i, j, k;  // local index
            int n1 = 0, n2 = 0, n3 = 0;  // temp int 변수
            string sRet;

            for (i = 0, n1 = 0; i < n; i++)  // 0  1  2  3  4  5 ...
            {   // IndexOf : 문자가 없을 경우 -1
                n1 = str.IndexOf(sep, n1) + 1;  // i 번째 구분자   
                if (n1 == 0) return "";
            }   // n1 : n 번째 필드 시작

            n2 = str.IndexOf(sep, n1);  // n+1 번째 구분자
            if (n2 == -1) n2 = str.Length;
            n3 = n2 - n1;  // 문자열 길이 계산

            sRet = str.Substring(n1, n3);
            return sRet;
        }
        public Form1()
        {
            InitializeComponent();
        }
        delegate void AddTextCB(string str);

        private void AddText(string str)
        {
            if(this.tbMemo.InvokeRequired)
            {
                AddTextCB d = new AddTextCB(AddText);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                tbMemo.Text += str;
            }
        }

        TcpListener _Listen;
        TcpClient _Sock;
        byte[] buf = new byte[20000];
        private void btnStart_Click(object sender, EventArgs e)
        {
            if(_Listen == null)
                _Listen = new TcpListener(int.Parse(tbPort.Text));
            _Listen.Start();



            Thread ServerThread = new Thread(ServerProcess);
            ServerThread.Start();
           // _Listen.Stop();
        }
        private void ServerProcess() //서버 Thread 함수
        {
            while (true)
            {
                _Sock = _Listen.AcceptTcpClient();  //Session open
                //tbMemo.Text+= _Sock.Client.RemoteEndPoint.ToString();
                //Cross Thread 오류 : tbMemo에 직접 접근 불가 ==> invoke 필요
                string s1 = GetToken(0, _Sock.Client.RemoteEndPoint.ToString(), ":");
                AddText($"원격 접속 요청 : {s1}\r\n");
                
                NetworkStream ns=  _Sock.GetStream();
                if (ns.DataAvailable)
                {
                    ns.Read(buf, 0, (int)(buf.Length)); // buf : Byte array
                    AddText(Encoding.Default.GetString(buf) + "\r\n");
                }
            }
        }
    }
}
