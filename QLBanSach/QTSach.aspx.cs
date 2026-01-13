using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace QLBanSach
{
    public partial class QTSach : System.Web.UI.Page
    {
        string strConn = ConfigurationManager.ConnectionStrings["BanSachDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NapDuLieu();
            }
        }

        private void NapDuLieu()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = "SELECT * FROM Sach WHERE TenSach LIKE @ten ORDER BY NgayCapNhat DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@ten", "%" + txtTen.Text + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvSach.DataSource = dt;
                gvSach.DataBind();
            }
        }

        protected void btTraCuu_Click(object sender, EventArgs e)
        {
            gvSach.EditIndex = -1;
            gvSach.PageIndex = 0;
            NapDuLieu();
        }

        protected void gvSach_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSach.PageIndex = e.NewPageIndex;
            NapDuLieu();
        }

        protected void gvSach_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSach.EditIndex = e.NewEditIndex;
            NapDuLieu();
        }

        protected void gvSach_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSach.EditIndex = -1;
            NapDuLieu();
        }

        protected void gvSach_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int maSach = Convert.ToInt32(gvSach.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvSach.Rows[e.RowIndex];
            string tenSach = ((TextBox)row.FindControl("txtTenSua")).Text;
            string gia = ((TextBox)row.FindControl("txtGiaSua")).Text;
            bool khuyenMai = ((CheckBox)row.FindControl("chkKMSua")).Checked;

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = "UPDATE Sach SET TenSach=@ten, Dongia=@gia, KhuyenMai=@km WHERE MaSach=@ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", tenSach);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@km", khuyenMai);
                cmd.Parameters.AddWithValue("@ma", maSach);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            gvSach.EditIndex = -1;
            NapDuLieu();
        }

        protected void gvSach_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int maSach = Convert.ToInt32(gvSach.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = "DELETE FROM Sach WHERE MaSach = @ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", maSach);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            NapDuLieu();
        }
    }
}