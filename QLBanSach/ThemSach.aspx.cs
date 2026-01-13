using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace QLBanSach
{
    public partial class ThemSach : System.Web.UI.Page
    {
        string strConn = ConfigurationManager.ConnectionStrings["BanSachDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NapChuDe();
            }
        }

        private void NapChuDe()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = "SELECT * FROM ChuDe";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlChuDe.DataSource = dt;
                ddlChuDe.DataTextField = "TenCD";
                ddlChuDe.DataValueField = "MaCD";
                ddlChuDe.DataBind();
            }
        }

        protected void btXuLy_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = "INSERT INTO Sach(TenSach, Dongia, MaCD, Hinh, KhuyenMai, NgayCapNhat) " +
                             "VALUES (@ten, @gia, @macd, @hinh, @km, @ngay)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@gia", txtDonGia.Text);
                cmd.Parameters.AddWithValue("@macd", ddlChuDe.SelectedValue);
                cmd.Parameters.AddWithValue("@km", chkKhuyenMai.Checked);
                cmd.Parameters.AddWithValue("@ngay", DateTime.Now);

                string tenFile = "no_image.jpg";
                if (FHinh.HasFile)
                {
                    tenFile = FHinh.FileName;
                    string path = Server.MapPath("~/Bia_sach/") + tenFile;
                    FHinh.SaveAs(path);
                }
                cmd.Parameters.AddWithValue("@hinh", tenFile);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                Response.Redirect("QTSach.aspx");
            }
        }
    }
}