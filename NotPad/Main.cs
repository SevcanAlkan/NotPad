using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotPad
{
    public partial class Main : Form
    {
        DataContext DbContext;
        private bool IsNew = false;

        public Main()
        {
            if (CurrentUser.User == null || CurrentUser.User.ID == null || CurrentUser.User.ID == 0)
            {
                Login frm = new Login();
                this.Visible = false;
                frm.ShowDialog();
                this.Close();
            }

            InitializeComponent();
            DbContext = new DataContext();

            Load();
        }

        private void Load()
        {
            UpdateList();

            lblCurrentUserName.Text = CurrentUser.User.DisplayName;
        }

        private void UpdateList()
        {
            if (!DbContext.Notes.Any())
                return;

            this.lstNotes.DataSource = null;
            this.lstNotes.DataSource = DbContext.Notes.Where(a => a.UserID == CurrentUser.User.ID).OrderBy(o => o.ID).ToArray();
            this.lstNotes.DisplayMember = "Subject";
            this.lstNotes.ValueMember = "ID";
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            CurrentUser.User = new User();
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            TxtSubject.Text = "";
            TxtSubject.Tag = "";
            TxtContent.Text = "";
            lblCreateDate.Text = "";
            lblUpdateDate.Text = "";

            IsNew = true;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Note item = (Note)lstNotes.SelectedItem;
            if (item != null)
            {
                DbContext.Notes.Remove(item);
                DbContext.SaveChanges();
            }

            UpdateList();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtSubject.Text == "" || TxtContent.Text == "")
            {
                MessageBox.Show("Lütfen gerekli alanların doldurun!", "Uyarı!", MessageBoxButtons.OK);
                return;
            }

            Note currentNote = new Note();
            int id = Convert.ToInt32(TxtSubject.Tag == "" ? 0 : TxtSubject.Tag);

            if (DbContext.Notes.Any(a => id != 0 && a.ID == id) && !IsNew)
            {
                currentNote = DbContext.Notes.Where(a => a.ID == (int)TxtSubject.Tag).SingleOrDefault();

                currentNote.Subject = TxtSubject.Text;
                currentNote.Content = TxtContent.Text;
                currentNote.UpdateTime = DateTime.Now;

                DbContext.Entry(currentNote).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                currentNote.Subject = TxtSubject.Text;
                currentNote.Content = TxtContent.Text;
                currentNote.CreateDate = DateTime.Now;
                currentNote.UserID = CurrentUser.User.ID;
                DbContext.Notes.Add(currentNote);

                IsNew = false;
            }


            DbContext.SaveChanges();
            UpdateList();
        }

        private void lstNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Note item = (Note)lstNotes.SelectedItem;

            if (item != null)
            {
                TxtSubject.Text = item.Subject;
                TxtSubject.Tag = item.ID;
                TxtContent.Text = item.Content;
                lblCreateDate.Text = item.CreateDate.ToString("dd/mm/yyyy HH:mm");
                lblUpdateDate.Text = (item.UpdateTime != null && item.UpdateTime != DateTime.MinValue) ? item.UpdateTime.Value.ToString("dd/mm/yyyy HH:mm") : "";
            }
        }
    }
}
