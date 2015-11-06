using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Winneres : System.Web.UI.Page
{
    string cmd;
    int result;
    string command;
    SqlDataReader reader;
    Database db = new Database();
    DataTable dt;
    DatabaseExecution AS = new DatabaseExecution();
    string School_Name;
    string Student_Standard;
    string Connection_String = ConfigurationManager.ConnectionStrings["MADSKOOL"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindEventDetatils();
            LoadEventDate();
        }
    }

    protected void BindEventDetatils()
    {
        dt = new DataTable();
        using (SqlConnection con = new SqlConnection(Connection_String))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from MSCK_WiinersList_Details", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        grdNewEvent.DataSource = dt;
        grdNewEvent.DataBind();
    }

    #region[Loading EvenDate dor Edit]
    public void LoadEventDateforEditDropdown()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct Event_ID, Event_Date FROM MSCK_EventDetails", con);
                adapter.Fill(subjects);

                drpEditEventDate.DataSource = subjects;
                drpEditEventDate.DataTextField = "Event_Date";
                drpEditEventDate.DataValueField = "Event_ID";
                drpEditEventDate.DataBind();
            }
            catch (Exception ex)
            {
                drpEditEventDate.Items.Insert(0, new ListItem("<No Students Found>", "0"));
                // Handle the error
            }

        }

        // Add the initial item - you can add this even if the options from the
        // db were not successfully loaded
        drpEditEventDate.Items.Insert(0, new ListItem("<Select Event Date>", "0"));

    }
    #endregion
    #region[Loading StudentName Edit]
    public void LoadStudentNameforEdit(string Category, string EvenyName, string EventDate)
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct Student_Name,Student_ID from MSCK_StudentDetails where Student_Category='" + Category.ToString() + "' AND Event_name='" + EvenyName.ToString() + "' AND Event_Date='" + EventDate.ToString() + "'", con);
                adapter.Fill(subjects);

                drpEditStudentName.DataSource = subjects;
                drpEditStudentName.DataTextField = "Student_Name";
                drpEditStudentName.DataValueField = "Student_ID";
                drpEditStudentName.DataBind();
            }
            catch (Exception ex)
            {
                drpEditStudentName.Items.Insert(0, new ListItem("<No Students Found>", "0"));
                // Handle the error
            }

        }

        // Add the initial item - you can add this even if the options from the
        // db were not successfully loaded
        drpEditStudentName.Items.Insert(0, new ListItem("<Select Student Name>", "0"));

    }
    #endregion
    #region[Loading SchoolName for Edit]
    public void LoadSchoolNameforEdit(string Category, string EventName, string EventDate, string StudentName)
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct School_Name,Student_Standard from MSCK_StudentDetails where Student_Category='" + Category.ToString() + "' AND Event_name='" + EventName.ToString() + "' AND Event_Date='" + EventDate.ToString() + "' AND Student_Name='" + StudentName.ToString() + "'", con);
                adapter.Fill(subjects);
                foreach (DataRow row in subjects.Rows)
                {
                    School_Name = row.Field<string>("School_Name");
                    Student_Standard = row.Field<string>("Student_Standard");

                }
                txtEditStudentSchool.Text = School_Name.ToString();
                txtEditStudentStandard.Text = Student_Standard.ToString();



            }
            catch (Exception ex)
            {

                // Handle the error
            }

        }



    }
    #endregion

    #region[Loading EvenName for Edit]
    public void LoadEventNameforEdit(string Date)
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct Event_ID, Event_Name FROM MSCK_EventDetails where Event_Date='" + Date.ToString() + "'", con);
                adapter.Fill(subjects);

                drpEditEventName.DataSource = subjects;
                drpEditEventName.DataTextField = "Event_Name";
                drpEditEventName.DataValueField = "Event_ID";
                drpEditEventName.DataBind();
            }
            catch (Exception ex)
            {
                drpEditEventName.Items.Insert(0, new ListItem("<No Students Found>", "0"));
                // Handle the error
            }

        }

        // Add the initial item - you can add this even if the options from the
        // db were not successfully loaded
        drpEditEventName.Items.Insert(0, new ListItem("<Select Event Name>", "0"));

    }
    #endregion






    #region[Loading StudentName Edit after ADD]
    public void LoadStudentNameforEditADD()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct Student_Name,Student_ID from MSCK_StudentDetails where Student_Category='" + drpEditCategoryLevel.SelectedItem.Text + "' AND Event_name='" + drpEditEventName.SelectedItem.Text + "' AND Event_Date='" + drpEditEventDate.SelectedItem.Text + "'", con);
                adapter.Fill(subjects);

                drpEditStudentName.DataSource = subjects;
                drpEditStudentName.DataTextField = "Student_Name";
                drpEditStudentName.DataValueField = "Student_ID";
                drpEditStudentName.DataBind();
            }
            catch (Exception ex)
            {
                drpEditStudentName.Items.Insert(0, new ListItem("<No Students Found>", "0"));
                // Handle the error
            }

        }

        // Add the initial item - you can add this even if the options from the
        // db were not successfully loaded
        drpEditStudentName.Items.Insert(0, new ListItem("<Select Student Name>", "0"));

    }
    #endregion
    #region[Loading SchoolName for Edit after ADD]
    public void LoadSchoolNameforEditADD()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct School_Name,Student_Standard from MSCK_StudentDetails where Student_Category='" + drpEditCategoryLevel.SelectedItem.Text + "' AND Event_name='" + drpEditEventName.SelectedItem.Text + "' AND Event_Date='" + drpEditEventDate.SelectedItem.Text + "' AND Student_Name='" + drpEditStudentName.SelectedItem.Text + "'", con);
                adapter.Fill(subjects);
                foreach (DataRow row in subjects.Rows)
                {
                    School_Name = row.Field<string>("School_Name");
                    Student_Standard = row.Field<string>("Student_Standard");

                }
                txtEditStudentSchool.Text = School_Name.ToString();
                txtEditStudentStandard.Text = Student_Standard.ToString();



            }
            catch (Exception ex)
            {

                // Handle the error
            }

        }



    }
    #endregion

    #region[Loading EvenName for Edit after Add]
    public void LoadEventNameforEditADD()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct Event_ID, Event_Name FROM MSCK_EventDetails where Event_Date='" + drpEditEventDate.SelectedItem.Text + "'", con);
                adapter.Fill(subjects);

                drpEditEventName.DataSource = subjects;
                drpEditEventName.DataTextField = "Event_Name";
                drpEditEventName.DataValueField = "Event_ID";
                drpEditEventName.DataBind();
            }
            catch (Exception ex)
            {
                drpEditEventName.Items.Insert(0, new ListItem("<No Students Found>", "0"));
                // Handle the error
            }

        }

        // Add the initial item - you can add this even if the options from the
        // db were not successfully loaded
        drpEditEventName.Items.Insert(0, new ListItem("<Select Event Name>", "0"));

    }
    #endregion


    #region[Loading Showdetails]
    public DataTable Showdetails(int WinnerID)
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * from  MSCK_WiinersList_Details where Winners_ID='" + WinnerID.ToString() + "'", con);
                adapter.Fill(subjects);
                return subjects;
              
            }
            catch (Exception ex)
            {
              
                // Handle the error
            }
          
        }

        return subjects;

    }
    #endregion

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName.Equals("detail"))
        {
            int Winners_ID = Convert.ToInt32(grdNewEvent.DataKeys[index].Value.ToString());
            //IEnumerable<DataRow> query = from i in dt.AsEnumerable()
            //                             where i.Field<String>("Student_Name").Equals(Student_Name)
            //                             select i;


            DataTable detailTable = Showdetails(Winners_ID);
            DetailsView1.DataSource = detailTable;
            DetailsView1.DataBind();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#detailModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DetailModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("editRecord"))
        {


            GridViewRow gvrow = grdNewEvent.Rows[index];
            lblEditWiinerID.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();
            string Edit_EventDate = HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();
            LoadEventDateforEditDropdown();
            drpEditEventDate.SelectedItem.Text = Edit_EventDate.ToString();
            LoadEventNameforEdit(Edit_EventDate);
            drpEditEventName.SelectedItem.Text = HttpUtility.HtmlDecode(gvrow.Cells[5].Text).ToString();
            drpEditCategoryLevel.SelectedItem.Text = HttpUtility.HtmlDecode(gvrow.Cells[6].Text);
            LoadStudentNameforEdit(HttpUtility.HtmlDecode(gvrow.Cells[6].Text), HttpUtility.HtmlDecode(gvrow.Cells[5].Text).ToString(), Edit_EventDate);
            drpEditStudentName.SelectedItem.Text = HttpUtility.HtmlDecode(gvrow.Cells[7].Text);
            LoadSchoolNameforEdit(HttpUtility.HtmlDecode(gvrow.Cells[6].Text), HttpUtility.HtmlDecode(gvrow.Cells[5].Text).ToString(), Edit_EventDate, HttpUtility.HtmlDecode(gvrow.Cells[7].Text));
            txtEditStudentSchool.Text = HttpUtility.HtmlDecode(gvrow.Cells[8].Text);
            txtEditPrize.Text = HttpUtility.HtmlDecode(gvrow.Cells[9].Text);
            lblResult.Visible = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);

        }
        else if (e.CommandName.Equals("deleteRecord"))
        {
            string code = grdNewEvent.DataKeys[index].Value.ToString();
            hfCode.Value = code;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }

    }



    // Handles Update Button Click Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string WinnerID = lblEditWiinerID.Text;
        string EvenDate = drpEditEventDate.SelectedItem.Text;
        string EventName = drpEditEventName.SelectedItem.Text;
        string StudentName = drpEditStudentName.SelectedItem.Text;
        string StudentCategory = drpEditCategoryLevel.SelectedItem.Text;
        string StudentStandard = txtEditStudentStandard.Text;
        string StudentSchool = txtEditStudentSchool.Text;
        string StudentPrize = txtEditPrize.Text;

        UpdatingData(EvenDate, EventName, StudentName, StudentCategory, StudentStandard, StudentSchool, StudentPrize, WinnerID);

        BindEventDetatils();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("alert('Records Updated Successfully');");
        sb.Append("$('#editModal').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
    }

    #region[Updating Data]
    private void UpdatingData(string PEvenDate, string PEventName, string PStudentName, string PStudentCategory, string PStudentStandard, string PStudentSchool, string PStudentPrize, string PWinnerID)
    {
        cmd = "update MSCK_WiinersList_Details set Event_Date='" + PEvenDate.ToString() + "',Event_Name='" + PEventName.ToString() + "',Category='" + PStudentCategory.ToString() + "',Student_Name='" + PStudentName.ToString() + "',School_Name='" + PStudentSchool.ToString() + "',Prize='" + PStudentPrize.ToString() + "',Student_Standard='" + PStudentStandard.ToString() + "'  where Winners_ID='" + PWinnerID.ToString() + "'   ";


        result = AS.DML(cmd);
        if (result > 0)
        {
            int a2 = 1;
            //            lblErrorMsg.ForeColor = System.Drawing.Color.Green;
            //          lblErrorMsg.Text = "Blood Ordered Successfull";
            //        clear();
        }

        else
        {
            int b2 = 1;
            // lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            //lblErrorMsg.Text = "Process Failed";

        }
    }

    #endregion



    protected void drpEditStudentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSchoolNameforEditADD();
    }

    protected void drpEditCategoryLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudentNameforEditADD();
    }

    protected void drpEditEventDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEventNameforEditADD();
    }

    protected void drpAddEventDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEventName();
    }

    protected void drpAddStudentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSchoolName();
    }

    protected void drpADDCategoryLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudentName();
    }





    //Handles Add record button click in main form
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

    }

    //Handles Add button click in add modal popup
    protected void btnAddRecord_Click(object sender, EventArgs e)
    {
        string Student_Standard = txtAddStudentStandard.Text;
        string EventName = drpAddEventName.SelectedItem.Text;
        string EvenDate = drpAddEventDate.SelectedItem.Text;
        string Category = drpADDCategoryLevel.SelectedItem.Text;
        string Strudent_Name = drpAddStudentName.SelectedItem.Text;
        string School_Name = txtAddStudentSchool.Text;
        string Prize = txtAddPrizeReward.Text;
        insertingData(EventName, EvenDate, Category, Strudent_Name, Student_Standard, School_Name, Prize);
        BindEventDetatils();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("alert('Record Added Successfully');");
        sb.Append("$('#addModal').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
    }


    #region[Inserting Data]
    private void insertingData(string PEventName, string PEventDate, string PCategory, string PStrudent_Name, string PStudent_Standard, string PSchool_Name, string PPrize)
    {

        cmd = "insert into MSCK_WiinersList_Details(Event_Date,Event_Name,Category,Student_Name,School_Name,Prize,Student_Standard) values('" + PEventDate.ToString() + "','" + PEventName.ToString() + "','" + PCategory.ToString() + "','" + PStrudent_Name.ToString() + "','" + PSchool_Name.ToString() + "','" + PPrize.ToString() + "','" + PStudent_Standard.ToString() + "')";

        result = AS.DML(cmd);
        if (result > 0)
        {
            int a1 = 1;
            //  lblErrorMsg.ForeColor = System.Drawing.Color.Green;
            //lblErrorMsg.Text = "Blood Ordered Successfull";
            //clear();
        }

        else
        {
            int b1 = 1;
            //  lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            // lblErrorMsg.Text = "Process Failed";

        }
    }

    #endregion



    // Handles Delete button click in delete modal popup
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string DeleteEventName = hfCode.Value;
        DeletingData(DeleteEventName);
        BindEventDetatils();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("alert('Record deleted Successfully');");
        sb.Append("$('#deleteModal').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
    }



    #region[Deleting Data]
    private void DeletingData(string code)
    {
        cmd = "delete from MSCK_WiinersList_Details where Student_Name='" + code.ToString() + "'";

        //cmd = "insert into Event_Details(Event_Name,Event_Date,Event_Venue,Brief_Description,Country,Date,Units_Needed,Purpose) values('" + txtUsername.Text + "','" + drpBloodneeded.SelectedItem.Text + "','" + State.ToString() + "','" + City.ToString() + "','" + LblshowCountry.Text + "','" + txtdate.Text + "','" + drpunitsneeded.SelectedItem.Text + "','" + txtpurpose.Text + "')";

        result = AS.DML(cmd);
        if (result > 0)
        {
            int a3 = 1;
            // lblErrorMsg.ForeColor = System.Drawing.Color.Green;
            //lblErrorMsg.Text = "Blood Ordered Successfull";
            //clear();
        }

        else
        {
            int b3 = 1;
            //    lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            //  lblErrorMsg.Text = "Process Failed";

        }
    }

    #endregion


    #region[Loading StudentName]
    public void LoadStudentName()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct Student_Name,Student_ID from MSCK_StudentDetails where Student_Category='" + drpADDCategoryLevel.SelectedItem.Text + "' AND Event_name='" + drpAddEventName.SelectedItem.Text + "' AND Event_Date='" + drpAddEventDate.SelectedItem.Text + "'", con);
                adapter.Fill(subjects);

                drpAddStudentName.DataSource = subjects;
                drpAddStudentName.DataTextField = "Student_Name";
                drpAddStudentName.DataValueField = "Student_ID";
                drpAddStudentName.DataBind();
            }
            catch (Exception ex)
            {
                drpAddStudentName.Items.Insert(0, new ListItem("<No Students Found>", "0"));
                // Handle the error
            }

        }

        // Add the initial item - you can add this even if the options from the
        // db were not successfully loaded
        drpAddStudentName.Items.Insert(0, new ListItem("<Select Student Name>", "0"));

    }
    #endregion






    #region[Loading SchoolName]
    public void LoadSchoolName()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct School_Name,Student_Standard from MSCK_StudentDetails where Student_Category='" + drpADDCategoryLevel.SelectedItem.Text + "' AND Event_name='" + drpAddEventName.SelectedItem.Text + "' AND Event_Date='" + drpAddEventDate.SelectedItem.Text + "' AND Student_Name='" + drpAddStudentName.SelectedItem.Text + "'", con);
                adapter.Fill(subjects);
                foreach (DataRow row in subjects.Rows)
                {
                    School_Name = row.Field<string>("School_Name");
                    Student_Standard = row.Field<string>("Student_Standard");

                }
                txtAddStudentSchool.Text = School_Name.ToString();
                txtAddStudentStandard.Text = Student_Standard.ToString();



            }
            catch (Exception ex)
            {

                // Handle the error
            }

        }



    }
    #endregion







    #region[Loading EvenName]
    public void LoadEventName()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct Event_ID, Event_Name FROM MSCK_EventDetails where Event_Date='" + drpAddEventDate.SelectedItem.Text + "'", con);
                adapter.Fill(subjects);

                drpAddEventName.DataSource = subjects;
                drpAddEventName.DataTextField = "Event_Name";
                drpAddEventName.DataValueField = "Event_ID";
                drpAddEventName.DataBind();
            }
            catch (Exception ex)
            {
                drpAddEventName.Items.Insert(0, new ListItem("<No Students Found>", "0"));
                // Handle the error
            }

        }

        // Add the initial item - you can add this even if the options from the
        // db were not success  fully loaded
        drpAddEventName.Items.Insert(0, new ListItem("<Select Event Name>", "0"));

    }
    #endregion





    #region[Loading EvenDate]
    public void LoadEventDate()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(Connection_String))
        {

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct Event_ID, Event_Date FROM MSCK_EventDetails", con);
                adapter.Fill(subjects);

                drpAddEventDate.DataSource = subjects;
                drpAddEventDate.DataTextField = "Event_Date";
                drpAddEventDate.DataValueField = "Event_ID";
                drpAddEventDate.DataBind();
            }
            catch (Exception ex)
            {
                drpAddEventDate.Items.Insert(0, new ListItem("<No Students Found>", "0"));
                // Handle the error
            }

        }

        // Add the initial item - you can add this even if the options from the
        // db were not successfully loaded
        drpAddEventDate.Items.Insert(0, new ListItem("<Select Event Date>", "0"));

    }
    #endregion

}
