﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_X
{
    public partial class Uc_CustomerCheckout : UserControl
    {
        function fn = new function();
        string query = "";
        public Uc_CustomerCheckout()
        {
            InitializeComponent();
        }

        private void Uc_CustomerCheckout_Load(object sender, EventArgs e)
        {
            query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,rooms.roomNo,rooms.roomtype,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout = 'NO'";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        // searching
        private void txtName_TextChanged(object sender, EventArgs e)
        {

            query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,rooms.roomNo,rooms.roomtype,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where cname like '" + txtName.Text + "%' and checkout = 'NO'";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        int id;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txt_Cname.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRoomNo.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
        }

        private void btn_checkout_Click(object sender, EventArgs e)
        {

            if (txt_Cname.Text != "")
            {
                if (MessageBox.Show("Are You Sure?", "Confimation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String cdate = txtCheckout_date.Text;
                    query = "update customer set checkout = 'YES', checkout='" + cdate + "' where cid = " + id + " update rooms set booked = 'NO' where roomNo = '" + txtRoomNo.Text + "' ";
                    fn.setData(query, "Check Out Successful.");
                    Uc_CustomerCheckout_Load(this, null);
                    clearAll();

                }

                else
                {
                    MessageBox.Show("No Customer Selected.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

       public void clearAll()
        {
            txt_Cname.Clear();
            txtName.Clear();
            txtRoomNo.Clear();
            txtCheckout_date.ResetText();
        }


        private void guna2DataGridView1_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
