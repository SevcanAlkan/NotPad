using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotPad
{
    public partial class Login : Form
    {
        DataContext DbContext;

        public Login()
        {
            InitializeComponent();
            DbContext = new DataContext();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (TxtUserName.Text == null || TxtPassword.Text==null|| TxtPassword.Text ==""|| TxtUserName.Text== "")
            {
                MessageBox.Show("Lütfen gerekli alanların doldurun!", "Uyarı!", MessageBoxButtons.OK);
                return;
            }

            List<User> users = new List<User>();
            users = DbContext.Users.ToList();

            User user = users.Where(a => a.UserName == TxtUserName.Text && a.Password == TxtPassword.Text).FirstOrDefault();
            if (users.Any(a => a.UserName == TxtUserName.Text && a.Password == TxtPassword.Text))
            {
                CurrentUser.User = user;
                Main frm = new Main();
                this.Visible = false;
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya Sifre uyusmuyor!", "Uyarı!", MessageBoxButtons.OK);
                return;
            }

        }
    }
}
