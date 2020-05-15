using Gios_mvcSolution.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GiosDao;


namespace Gios_mvcSolution
{
    public class SVC
    {

        #region Business Unit Functions
        //Purpose: Calls stored procedure to Create/Modify BU based on the Flag value 
        public string CreateEditBUsinessUnit(string AddUpdateFlag, BusinessUnit modifiedBU)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_AddUpdateBusinessUnit", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddupdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@strBUID", modifiedBU.strBUID));
                objCom.Parameters.Add(new SqlParameter("@strBUDescr", modifiedBU.strBUDescr));
                objCom.Parameters.Add(new SqlParameter("@strGroupID", modifiedBU.strGroupID));
                if (AddUpdateFlag == "I") { objCom.Parameters.Add(new SqlParameter("@strUserID", modifiedBU.strCreatorID)); }
                else { objCom.Parameters.Add(new SqlParameter("@strUserID", modifiedBU.strModifierID)); }
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Sucess";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;

        }

        //Purpose: Find BU - Pass ID and retrieve all details in table on it
        public BusinessUnit FindBusinessUnit(string id)
        {
            BusinessUnit _retVal = new BusinessUnit();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            //BusinessUnit BUS;

            ConnectionString(objCon);
            objCom = new SqlCommand("SELECT strBUID, strBUDescr, strGroupID, dtmCreated, strCreatorID, dtmModified, strModifierID from GIOS.GIOS_SVC.BusinessUnit Where strBUID='" + id + "'", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {

                _retVal.strBUID = reader["strBUID"].ToString();
                _retVal.strBUDescr = reader["strBUDescr"].ToString();
                _retVal.strGroupID = reader["strGroupID"].ToString();
                _retVal.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]);
                _retVal.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { _retVal.dtmModified = null; }
                else { _retVal.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }

                _retVal.strModifierID = reader["strModifierID"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        //Purpose: Get Values to create Select Lists (Dropdowns) on Pages within GIOS
        public IEnumerable<BusinessUnit> GetBusinessUnits()
        {
            List<BusinessUnit> _retVal = new List<BusinessUnit>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            BusinessUnit locHierData;

            ConnectionString(objCon);
            // objCon.ConnectionString = _conn.GetConnection();
            objCom = new SqlCommand("GIOS.GIOS_GetBusinessUnit", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                locHierData = new BusinessUnit();
                locHierData.strBUID = reader["strBUID"].ToString();
                locHierData.strBUDescr = reader["strBUDescr"].ToString();
                _retVal.Add(locHierData);

            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }

        //Purpose: Used to list all possible BU's stored with all details for them, Used on LocAdmin Pages in prep for Creating/Modifying
        public IEnumerable<BusinessUnit> ListBusinessUnits()
        {
            List<BusinessUnit> _retVal = new List<BusinessUnit>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            BusinessUnit BUS;

            ConnectionString(objCon);
            objCom = new SqlCommand("SELECT strBUID, strBUDescr, strGroupID, dtmCreated, strCreatorID, dtmModified, strModifierID from GIOS.GIOS_SVC.BusinessUnit", objCon);



            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                BUS = new BusinessUnit();
                BUS.strBUID = reader["strBUID"].ToString();
                BUS.strBUDescr = reader["strBUDescr"].ToString();
                BUS.strGroupID = reader["strGroupID"].ToString();
                BUS.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]);
                BUS.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { BUS.dtmModified = null; }
                else { BUS.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }

                BUS.strModifierID = reader["strModifierID"].ToString();

                _retVal.Add(BUS);

            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }

        //Purpose: Allows deletion of a specific BU -- NOT DONE
        public string RemoveBusinessUnit(BusinessUnit deleteBU)
        {
            //Still not completed -- Need to think through how deletes of each level will work
            //Do we cascade? or do we force them to remove all children first?
            // Are we sure we truly want to remove everything for the DB?

            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_AddUpdateBusinessUnit", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@strBUID", deleteBU.strBUID));
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Sucess";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        #endregion

        #region Segment Functions 
        //Purpose: Get Values to create Select Lists (Dropdowns) on Pages within GIOS
        public IEnumerable<BUSegment> GetSegments(string BUID)
        {
            List<BUSegment> _retVal = new List<BUSegment>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            BUSegment locHierData;
            ConnectionString(objCon);
            objCom = new SqlCommand("GIOS.GIOS_GetBUSegment", objCon);
            objCom.CommandType = System.Data.CommandType.StoredProcedure;
            objCom.Parameters.Add(new SqlParameter("@BUID", BUID));
            // objCom.Parameters.AddWithValue("@BU", BUID);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                locHierData = new BUSegment();
                locHierData.strSegmentID = reader["strSegmentID"].ToString();
                locHierData.strSegmentDescr = reader["strSegmentDescr"].ToString();
                _retVal.Add(locHierData);

            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }

        //Purpose: Used to list all possible Segments stored with all details for them, Used on LocAdmin Pages in prep for Creating/Modifying
        public IEnumerable<BUSegment> ListSegments()
        {
            List<BUSegment> _retVal = new List<BUSegment>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            BUSegment Seg;

            ConnectionString(objCon);
            objCom = new SqlCommand("SELECT strSegmentID, strSegmentDescr, strBUID, dtmCreated, strCreatorID, dtmModified, strModifierID FROM[GIOS].[GIOS_SVC].[BUSegment]", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                Seg = new BUSegment();
                Seg.strSegmentID = reader["strSegmentID"].ToString();
                Seg.strSegmentDescr = reader["strSegmentDescr"].ToString();
                Seg.strBUID = reader["strBUID"].ToString();
                Seg.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]);
                Seg.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { Seg.dtmModified = null; }
                else { Seg.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }

                Seg.strModifierID = reader["strModifierID"].ToString();

                _retVal.Add(Seg);

            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        #endregion

        #region Plant Functions
        public IEnumerable<Plant> GetPlants(string SegID)
        {
            List<Plant> _retVal = new List<Plant>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            Plant locHierData;
            ConnectionString(objCon);

            objCom = new SqlCommand("GIOS.GIOS_GetPlant", objCon);
            objCom.CommandType = System.Data.CommandType.StoredProcedure;
            objCom.Parameters.Add(new SqlParameter("@SegID", SegID));
            // objCom.Parameters.AddWithValue("@BU", BUID);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                locHierData = new Plant();
                locHierData.intPlantID = Convert.ToInt32(reader["IntPlantID"]);
                locHierData.strPlantName = reader["strPlantName"].ToString();
                locHierData.strPlantDescr = reader["strPlantDescr"].ToString();
                _retVal.Add(locHierData);

            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        #endregion

        #region Processes / Department Functions 

        public IEnumerable<ProcessDepartment> GetProcessDepartments(int PlantID)
        {
            List<ProcessDepartment> _retVal = new List<ProcessDepartment>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            ProcessDepartment locHierData;
            ConnectionString(objCon);

            objCom = new SqlCommand("GIOS.GIOS_GetProcessDepartment", objCon);
            objCom.CommandType = System.Data.CommandType.StoredProcedure;
            objCom.Parameters.Add(new SqlParameter("@PlantID", PlantID));
            // objCom.Parameters.AddWithValue("@BU", BUID);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                locHierData = new ProcessDepartment();
                locHierData.strProcessID = reader["strProcessID"].ToString();
                locHierData.strProcessDescr = reader["strDescr"].ToString();

                _retVal.Add(locHierData);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        #endregion


        #region Process Plant Functions

        public IEnumerable<ProcessPlant> GetProcessPlants(int PlantID)
        {
            List<ProcessPlant> _retVal = new List<ProcessPlant>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            ProcessPlant locHierData;
            ConnectionString(objCon);

            objCom = new SqlCommand("GIOS.GIOS_GetProcessPlant", objCon);
            objCom.CommandType = System.Data.CommandType.StoredProcedure;
            objCom.Parameters.Add(new SqlParameter("@PlantID", PlantID));
            // objCom.Parameters.AddWithValue("@BU", BUID);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                locHierData = new ProcessPlant();
                locHierData.strProcessID = reader["strProcessID"].ToString();
                locHierData.intPlantID = Convert.ToInt32(reader["IntPlantID"]);
                locHierData.intProductID = Convert.ToInt32(reader["IntProductID"]);

                _retVal.Add(locHierData);
            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }


        #endregion

        #region Process Line Functions
        public IEnumerable<ProcessLine> GetProcessLines(int ProductID)
        {
            List<ProcessLine> _retVal = new List<ProcessLine>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            ProcessLine locHierData;
            ConnectionString(objCon);

            objCom = new SqlCommand("GIOS.GIOS_GetProcessLine", objCon);
            objCom.CommandType = System.Data.CommandType.StoredProcedure;
            objCom.Parameters.Add(new SqlParameter("@ProductID", ProductID));
            // objCom.Parameters.AddWithValue("@BU", BUID);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                locHierData = new ProcessLine();
                locHierData.strLineID = reader["strLineID"].ToString();
                locHierData.IntProductID = Convert.ToInt32(reader["IntProductID"]);
                locHierData.ProductProcessID = Convert.ToInt32(reader["ProductProcessID"]);

                _retVal.Add(locHierData);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        #endregion

        #region Training Module
        public List<Role> GetRoleNames()
        {
           
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtRole = null;
            DataSet dsRole = new DataSet();
            List<Role> roleList = new List<Role>();
            Role role = null;
            try
            {
                ConnectionString(objCon);                
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_GetJobRole";
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsRole);
                if (dsRole != null && dsRole.Tables.Count > 0)
                {
                    dtRole = dsRole.Tables[0];
                    if (dtRole.Rows.Count > 0)
                    {
                        roleList = new List<Role>();
                        foreach (DataRow dr in dtRole.Rows)
                        {
                            role = new Role();
                            role.Id = dr["ID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["ID"]);
                            role.Name = Convert.ToString(dr["NAME"]);
                            roleList.Add(role);
                        }

                    }
                }
                return roleList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<Competency> GetCompetencyNames()
        {

           
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtComp = null;
            DataSet dsComp = new DataSet();
            Competency comp = null;
            List<Competency> competencyList = new List<Competency>();
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_GetCompetency";
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                if (dsComp != null && dsComp.Tables.Count > 0)
                {
                    dtComp = dsComp.Tables[0];
                    if (dtComp.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtComp.Rows)
                        {
                            comp = new Competency();
                            comp.Id = dr["ID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["ID"]);
                            comp.Name = Convert.ToString(dr["NAME"]);
                            competencyList.Add(comp);
                        }

                    }
                }
                return competencyList;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }

        }
        public bool checkCompetencyName(Competency compobj)
        {

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtComp = null;
            DataSet dsComp = new DataSet();
            Competency comp = null;
            
            bool uniquecompetencies = true;
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_GetCompetency";
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                if (dsComp != null && dsComp.Tables.Count > 0)
                {
                    dtComp = dsComp.Tables[0];
                    if (dtComp.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtComp.Rows)
                        {
                            comp = new Competency();

                            comp.Name = Convert.ToString(dr["NAME"]);
                            if (comp.Name.Equals(compobj.Name))
                            {
                                uniquecompetencies = false;
                                break;

                            }
                          
                        }
                    }
                }
                return uniquecompetencies;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }
  }
    


        public List<Question> GetListofNewQues()
        {
            SqlConnection objCon = new SqlConnection();
        SqlCommand objCom = new SqlCommand();
        SqlDataAdapter sqlAD;
        DataTable dtComp = null;
        DataSet dsComp = new DataSet();
            Question quesobj = null;
        List<Question> questionList = new List<Question>();
            try
            {
                ConnectionString(objCon);
        objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_GetListofNewQues";
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                if (dsComp != null && dsComp.Tables.Count > 0)
                {
                    dtComp = dsComp.Tables[0];
                    if (dtComp.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtComp.Rows)
                        {
                           quesobj = new Question();
                            quesobj.Id= dr["ID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["ID"]);
                            quesobj.QuestionStatement = Convert.ToString(dr["Description"]);
                            questionList.Add(quesobj);
                        }

}
                }
                return questionList;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }

        }
        
        public List<Competency> ViewComp()
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtComp = null;
            DataSet dsComp = new DataSet();
            Competency comp = null;
            List<Competency> competencyList = new List<Competency>();
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_ViewCompdetails";
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                if (dsComp != null && dsComp.Tables.Count > 0)
                {
                    dtComp = dsComp.Tables[0];
                    if (dtComp.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtComp.Rows)
                        {
                            comp = new Competency();
                            comp.Id = dr["ID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["ID"]);
                            comp.Name = Convert.ToString(dr["NAME"]);
                          
                            competencyList.Add(comp);
                        }

                    }
                }
                return competencyList;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }

        }
        public Competency DeleteComp(Competency compobj)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataSet ds= new DataSet();
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
        
              objCom.CommandText = "[GIOS].[GIOS_DeleteCompetency]";
              
                objCom.Parameters.Add("@CompId", SqlDbType.Int).Value = compobj.Id;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(ds);
                objCon.Close();

                return compobj;
            }                                          
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }

        }
        public Competency AddComp(Competency compobj)
        {
            List<Question> quesobj = compobj.QuestionList;
            string xml = "<QuestionList>";
            string UserId = "Anonymous";
            for (int j = 0; j < compobj.QuestionList.Count; j++)
            {
                xml = xml + "<Question><Ques_ID>"+ compobj.QuestionList[j].Id+"</Ques_ID><Ques_Description>" + compobj.QuestionList[j].QuestionStatement + "</Ques_Description><CreaterID>" + UserId+ "</CreaterID></Question>";
             }

            xml = xml + "</QuestionList>";
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter sqlAD;
            try
            {                
                    ConnectionString(objCon);
                    ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "[GIOS].[GIOS_AddCompetency]";
                objCom.Parameters.Add("@CompName", SqlDbType.VarChar).Value = compobj.Name;
                objCom.Parameters.Add("@Compdata", SqlDbType.Xml).Value = xml;
                //objCom.Parameters.Add("@Quesdesc", SqlDbType.Int).Value = compobj.QuestionList;

                //objCom.Parameters["@CompId"].Direction = ParameterDirection.Output;
                //
                // int  retVal = Convert.ToInt32(objCom.Parameters["@CompId"].Value);


                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(ds);
                objCon.Close();

               return compobj;
            }
            catch (Exception ex)
            {
                CloseConnection(objCon);
                throw ex;
            }
        }
        public Competency EditComp(Competency Editobj)
        {   
            List<Question> quesList = new List<Question>();
          //  quesList.Add(Editobj);

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            DataSet ds = new DataSet();
            DataTable dtComp = null;
            SqlDataAdapter sqlAD;
            try
            {
                ConnectionString(objCon);
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "[GIOS].[GIOS_EditCompetency]";
                objCom.Parameters.Add("@compid", SqlDbType.VarChar).Value = Editobj.Id;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(ds);
                objCon.Close();
                if (ds!= null && ds.Tables.Count > 0)
                {
                    dtComp = ds.Tables[0];
                    if (dtComp.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtComp.Rows)
                        {
                            Question ques = new Question();
                            //ques.Id = dr["Ques_ID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["Ques_ID"]);
                            ques.Id = dr["Question_Detail_Id"] == DBNull.Value ? -1 : Convert.ToInt32(dr["Question_Detail_Id"]);
                            ques.QuestionStatement = Convert.ToString(dr["Ques_Description"]);
                            quesList.Add(ques);
                            
                        }

                    }
                }

                Editobj.QuestionList = quesList;
                return Editobj;
            }
            catch (Exception ex)
            {
                CloseConnection(objCon);
                throw ex;
            }
        }
        public Competency SaveCompetencyDetail(Competency compObj)
        {
               string xml = "<QuestionList>";
               string UserId = "Anonymous";
            for (int j = 0; j < compObj.QuestionList.Count; j++)
            {
                xml = xml + "<Question><Ques_ID>" + compObj.QuestionList[j].Id + "</Ques_ID><Ques_Description>" + compObj.QuestionList[j].QuestionStatement + "</Ques_Description><CreaterID>"+UserId+ "</CreaterID></Question>";
            }

            xml = xml + "</QuestionList>";
            SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                DataSet ds = new DataSet();
                DataTable dtComp = null;
                SqlDataAdapter sqlAD;
                try
                {
                    ConnectionString(objCon);
                    ConnectionString(objCon);
                    objCom.Connection = objCon;
                    objCom.CommandType = CommandType.StoredProcedure;
                    objCom.CommandText = "[GIOS].[GIOS_SaveCompetency]";
                // objCom.Parameters.Add("@Queslist", SqlDbType.VarChar).Value = compobj.QuestionList;
                // objCom.Parameters.Add("@Quesid", SqlDbType.Int).Value = compobj.QuestionList[0].Id;
                objCom.Parameters.Add("@Quesdata", SqlDbType.Xml).Value = xml;
                objCom.Parameters.Add("@compname", SqlDbType.VarChar).Value = compObj.Name;
                if (compObj.Id == -1)
                {
                    objCom.Parameters.Add("@compid", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    objCom.Parameters.Add("@compid", SqlDbType.Int).Value = compObj.Id;
                }
                objCom.Parameters.Add("@generatedCompId", SqlDbType.Int);
                objCom.Parameters["@generatedCompId"].Direction = ParameterDirection.Output;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(ds);
                compObj.Id= Convert.ToInt32(objCom.Parameters["@generatedCompId"].Value);
                objCon.Close();
                return compObj;
                }
            catch (Exception ex)
            {
                CloseConnection(objCon);
                throw ex;
            }
        }

            public Survey StartSurvey(Survey survey)
        {
           
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter sqlAD;

            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "[GIOS].[GIOS_StartSurvey]";
                objCom.Parameters.Add("@takeAsAGuest", SqlDbType.Bit).Value = survey.TakeAsAGuest;
                objCom.Parameters.Add("@roleId", SqlDbType.Int).Value = survey.RoleId;
                objCom.Parameters.Add("@trainingId", SqlDbType.Int).Value = DBNull.Value;
                objCom.Parameters.Add("@compId", SqlDbType.Int).Value = survey.CompetencyId;
                objCom.Parameters.Add("@userId", SqlDbType.NVarChar, 100).Value = survey.TakenBy;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(ds);
                objCon.Close();
               
                //time mapp the data
                if (ds != null && ds.Tables.Count > 0)
                {
                    //prepare list of questions
                    var competencyList = new List<Competency>();
                    survey.CompetencyList = MapDatatableToCometencyList(ds.Tables[1]);
                    if (ds.Tables.Count > 1)
                    {
                        survey.Id = ds.Tables[0].Rows[0]["SurveyId"] == DBNull.Value ? -1 : Convert.ToInt32(ds.Tables[0].Rows[0]["SurveyId"]);

                    }

                }

                return survey;
            }
            catch (Exception ex)
            {
                CloseConnection(objCon);
                throw ex;
            }

        }
        private void CloseConnection(SqlConnection objCon)
        {
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }
        }
        private List<Competency> MapDatatableToCometencyList(DataTable dt)
        {
            DataTable competencyQuestionDataTable = null;
            var competencyList = new List<Competency>();
            Survey survey = new Survey();
            //survey.Id = ds.Tables[1].Rows[0]["SurveyId"] == DBNull.Value ? -1 : Convert.ToInt32(ds.Tables[1].Rows[0]["SurveyId"]);
            competencyQuestionDataTable = dt;
            DataTable uniqueCols = competencyQuestionDataTable.DefaultView.ToTable(true, "Comp_ID", "Comp_Name");
            if (competencyQuestionDataTable.Rows.Count > 0)
            {

                // foreach (DataRow dr in dtcomQues.Rows)
                foreach (DataRow dp in uniqueCols.Rows)
                {
                    Competency competencyObj = new Competency();
                    var questionList = new List<Question>();
                    // var id = int.Parse(dr["Comp_ID"].ToString());
                    foreach (DataRow row in competencyQuestionDataTable.Rows)
                    {
                        if (int.Parse(dp["Comp_ID"].ToString()) == int.Parse(row["Comp_ID"].ToString()))
                        {
                            Question questionsObj = new Question();
                            questionsObj.Id = int.Parse(row["Ques_ID"].ToString());
                            questionsObj.QuestionStatement = Convert.ToString(row["Ques_Description"]);
                            if (row["Ques_Display_IsRev"] == DBNull.Value)
                            {
                                questionsObj.Relevant = null;
                            }
                            else
                            {
                                questionsObj.Relevant = Convert.ToBoolean(row["Ques_Display_IsRev"]);
                            }
                            if (row["Response"] == DBNull.Value)
                            {
                                questionsObj.Answer = null;
                            }
                            else
                            {
                                questionsObj.Answer = Convert.ToBoolean(row["Response"]);
                            }
                            questionList.Add(questionsObj);
                        }
                    }
                    competencyObj.Id = int.Parse(dp["Comp_ID"].ToString());
                    competencyObj.Name = Convert.ToString(dp["Comp_Name"]);
                    competencyObj.QuestionList = questionList;
                    competencyList.Add(competencyObj);
                }

            }
            return competencyList;
        }

        //Updates the GIOS_TUserTraining_Details table
        public void UpdateSurvey(Survey surveyObj)
        {
            var competencyList = new List<Competency>();
            competencyList = surveyObj.CompetencyList;
            int trainingId = surveyObj.Id;
            string userId = surveyObj.TakenBy;
            int roleId = surveyObj.RoleId;

            for (int j = 0; j < competencyList.Count; j++)
            {
                string xml = "<QuestionList>";
                var competencyObj = new Competency();
                competencyObj = competencyList[j];
                int compId = competencyObj.Id;
                int competencyLevId = competencyObj.LevelId;

                var competencyQuestionList = new List<Question>();
                competencyQuestionList = competencyObj.QuestionList;
                //preparing the xml
                for (int n = 0; n < competencyQuestionList.Count; n++)
                {
                    string isRelevant = (competencyQuestionList[n].Relevant == true) ? "1" : "0";
                    string answer = (competencyQuestionList[n].Answer == true) ? "1" : "0";
                    string statement = competencyQuestionList[n].QuestionStatement;
                    string questionId = (competencyQuestionList[n].Id).ToString();
                    xml = xml + "<Question><Users_ID>" + userId + "</Users_ID><Role_ID>" + roleId.ToString() + "</Role_ID><Comp_ID>" + compId.ToString() + "</Comp_ID><Ques_ID>" + questionId + "</Ques_ID><Lev_ID>" + competencyLevId + "</Lev_ID><Training_ID>" + trainingId + "</Training_ID><Response>" + answer + "</Response><Is_Rev>" + isRelevant + "</Is_Rev></Question>";
                }
                xml = xml + "</QuestionList>";
               
                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                SqlDataAdapter sqlAD;              
                DataSet dsComp = new DataSet();
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_UpdateSurvey";
                objCom.Parameters.Add("@data", SqlDbType.Xml).Value = xml;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
            }
        }
        //Updates the GIOS_TUserTraining_Result table
        public string updateSurveyResult(Survey surveyObj)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataSet dsComp = new DataSet();
            try {
                string retVal = string.Empty;
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_UpdateTrainingResult";
                objCom.Parameters.Add("@trainingId", SqlDbType.Int).Value = surveyObj.Id;
                objCom.Parameters.Add("@levelId", SqlDbType.Int).Value = surveyObj.LevelId;
                objCom.Parameters.Add("@level", SqlDbType.Char, 100);
                objCom.Parameters["@level"].Direction = ParameterDirection.Output;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                retVal = Convert.ToString(objCom.Parameters["@level"].Value);
                return retVal;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }
        }
        //Fetches basic training result details from GIOS_TUserTraining_Result for  a particular user 
        public List<Survey> FetchSurveyHistory(string userId)
        {           
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtComp = null;
            DataSet dsComp = new DataSet();          
            List<Competency> competencyList = new List<Competency>();
            try
            {

                var surveyList = new List<Survey>();
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_GetSurveyHistory";
                objCom.Parameters.Add("@userId", SqlDbType.VarChar).Value = userId;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                if (dsComp != null && dsComp.Tables.Count > 0)
                {
                    dtComp = dsComp.Tables[0];
                    if (dtComp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtComp.Rows)
                        {
                            var surveyObj = new Survey();
                            surveyObj.Id = dr["Training_Id"] == DBNull.Value ? -1 : Convert.ToInt32(dr["Training_Id"]);
                            surveyObj.TakenBy = dr["Users_ID"] == DBNull.Value ? "" : Convert.ToString(dr["Users_ID"]);
                            surveyObj.LevelId = dr["Lev_ID"] == DBNull.Value ?-1 : Convert.ToInt32(dr["Lev_ID"]);

                            surveyObj.LevelName = dr["Lev_Name"] == DBNull.Value ? "" : Convert.ToString(dr["Lev_Name"]); ;
                            surveyObj.TakeOn = dr["Training_Date"] == DBNull.Value ? "" : Convert.ToString(dr["Training_Date"]); 
                            surveyObj.RoleId = dr["RoleID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["RoleID"]);
                            surveyObj.CompetencyId = dr["CompetencyID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["CompetencyID"]);
                            surveyList.Add(surveyObj);
                        }
                    }
                }
                return surveyList;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }


        }

        public Survey FetchSurveyDetailById(Survey survey)
        {
           
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtComp = null;
            DataSet dsComp = new DataSet();
            List<Competency> competencyList = new List<Competency>();
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_FetchSurveyDetails";
                if (String.IsNullOrEmpty(survey.Id.ToString()))
                {
                    objCom.Parameters.Add("@trainingId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    objCom.Parameters.Add("@trainingId", SqlDbType.Int).Value = survey.Id;
                }
                if (String.IsNullOrEmpty(survey.RoleId.ToString()))
                {
                    objCom.Parameters.Add("@roleId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    objCom.Parameters.Add("@roleId", SqlDbType.Int).Value = survey.RoleId;
                }
                if (String.IsNullOrEmpty(survey.CompetencyId.ToString()))

                {
                    objCom.Parameters.Add("@compId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    objCom.Parameters.Add("@compId", SqlDbType.Int).Value = survey.CompetencyId;
                }

                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                if (dsComp != null && dsComp.Tables.Count > 0)
                {
                    dtComp = dsComp.Tables[0];
                    competencyList = MapDatatableToCometencyList(dtComp);
                    survey.CompetencyList = competencyList;
                }
                return survey;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }

        }

        #endregion



        public List<Role> GetRoleDetailsAdmin()
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtRole = null;
            DataSet dsRole = new DataSet();
            List<Role> roleList = new List<Role>();

            var compList = new List<Competency>();
            Competency compObj = new Competency();
            //Role role = null;
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_GetJobRoleAdmin";
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsRole);
                if (dsRole != null && dsRole.Tables.Count > 0)
                {
                    dtRole = dsRole.Tables[0];
                    DataTable uniqueCols = dtRole.DefaultView.ToTable(true, "Role_ID", "Role_Name", "Role_Description");
                    if (dtRole.Rows.Count > 0)
                    {
                        foreach (DataRow dp in uniqueCols.Rows)
                        {
                            Role roleObj = new Role();
                            var competencyList = new List<Competency>();
                            foreach (DataRow row in dtRole.Rows)
                            {

                                if (int.Parse(dp["Role_ID"].ToString()) == int.Parse(row["Role_ID"].ToString()))
                                {
                                    //if (row["Ques_Display_IsRev"] == DBNull.Value)
                                    //{
                                    //    questionsObj.Relevant = null;
                                    //}
                                    //else
                                    //{
                                    //    questionsObj.Relevant = Convert.ToBoolean(row["Ques_Display_IsRev"]);
                                    //}

                                    // compObj.Id = int.Parse(row["Comp_Id"].ToString());
                                    compObj.Id = row["Comp_Id"] == DBNull.Value ? -1 : int.Parse(row["Comp_Id"].ToString());

                                    if (row["Comp_Id"] == DBNull.Value)
                                    {
                                        compObj.Name = null;
                                    }
                                    else
                                    {
                                        compObj.Name = Convert.ToString(row["Comp_Name"]);
                                    }
                                    compObj.LevelId = row["Lev_ID"] == DBNull.Value ? -1 : int.Parse(row["Lev_ID"].ToString());
                                    //compObj.LevelId= int.Parse(row["Lev_ID"].ToString());
                                    competencyList.Add(compObj);
                                }

                            }
                            //           roleObj.Id = int.Parse(dp["Role_ID"].ToString());
                            roleObj.Id = dp["Role_ID"] == DBNull.Value ? -1 : int.Parse(dp["Role_ID"].ToString());
                            if (dp["Role_Name"] == DBNull.Value)
                            {
                                roleObj.Name = null;
                            }
                            else
                            {
                                roleObj.Name = Convert.ToString(dp["Role_Name"]);
                            }
                            if (dp["Role_Description"] == DBNull.Value)
                            {
                                roleObj.Description = null;
                            }
                            else
                            {
                                roleObj.Description = Convert.ToString(dp["Role_Description"]);
                            }
                            //roleObj.Name = Convert.ToString(dp["Role_Name"]);
                            roleObj.CompetencyList = competencyList;
                            roleList.Add(roleObj);
                        }

                    }
                }
                return roleList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool deleteRoleDetail(int roleId)
        {
            bool retVal = false;
            string sError = string.Empty;

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_DeleteRoleDetail";
                objCom.Parameters.Add("@role_id", SqlDbType.Int).Value = roleId;
                objCom.Parameters.Add("@error", SqlDbType.Char, 500);
                objCom.Parameters["@error"].Direction = ParameterDirection.Output;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                objCom.ExecuteScalar();
                sError = Convert.ToString(objCom.Parameters["@error"].Value);
                if (sError == string.Empty)
                {
                    retVal = true;
                }
                objCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return retVal;
        }
        public List<Role> GetRoleCompetencyDetails(Role role)
        {
            //List<Competency> compList = new List<Competency>();

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;

            DataTable dtRole = null;
            DataSet dsRole = new DataSet();
            List<Role> roleList = new List<Role>();
            var compList = new List<Competency>();
            //Competency compObj = new Competency();
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_FetchRoleCompDetails";
                objCom.Parameters.Add("@role_id", SqlDbType.Int).Value = role.Id;
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsRole);
                if (dsRole != null && dsRole.Tables.Count > 0)
                {
                    dtRole = dsRole.Tables[2];
                    if (dtRole.Rows.Count > 0)
                    {
                        Role roleObj = new Role();
                        foreach (DataRow row in dtRole.Rows)
                        {
                            Competency compObj = new Competency();
                            compObj.Id = row["Comp_ID"] == DBNull.Value ? -1 : int.Parse(row["Comp_ID"].ToString());
                            compObj.LevelId = row["Lev_ID"] == DBNull.Value ? -1 : int.Parse(row["Lev_ID"].ToString());

                            if (row["Comp_Name"] == DBNull.Value)
                            {
                                compObj.Name = null;
                            }
                            else
                            {
                                compObj.Name = Convert.ToString(row["Comp_Name"]);
                            }
                            compList.Add(compObj);
                        }
                        roleObj.Name = role.Name;
                        roleObj.Description = role.Description;
                        roleObj.CompetencyList = compList;
                        roleList.Add(roleObj);
                    }

                }
                return roleList;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }
        }
        public bool checkUniqueRoleName(Role role)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtComp = null;
            DataSet dsComp = new DataSet();
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_GetJobRole";
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                bool uniqueFlag = true;
                if (dsComp != null && dsComp.Tables.Count > 0)
                {
                    dtComp = dsComp.Tables[0];
                    if (dtComp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtComp.Rows)
                        {
                            if (role.Name == (Convert.ToString(dr["Name"])))
                            {
                                uniqueFlag = false;
                                break;
                            }


                        }

                    }
                }
                return uniqueFlag;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }

        }
        public Role saveRoleDetails(Role role)
        {

            int retVal;
            string userId = "v-kard1";
            var competencyList = new List<Competency>();
            DataTable dtRole = null;
            competencyList = role.CompetencyList;
            string roleName = role.Name;
            string description = role.Description;
            int roleId = role.Id;
            string xml = "<competencyList>";
            List<Role> roleList = new List<Role>();
            Competency compObj = new Competency();
            Role roleObj = new Role();
            //var compList = new List<Competency>();
            for (int i = 0; i < competencyList.Count; i++)
            {
                var competencyObj = new Competency();
                competencyObj = competencyList[i];
                int compId = competencyObj.Id;
                int competencyLevId = competencyObj.LevelId;
                xml = xml + "<Competency><Users_ID>" + userId + "</Users_ID><Comp_ID>" + compId.ToString() + "</Comp_ID>><Lev_ID>" + competencyLevId + "</Lev_ID></Competency>";
            }
            xml = xml + "</competencyList>";
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataSet dsRole = new DataSet();
            ConnectionString(objCon);
            objCom.Connection = objCon;
            objCom.CommandType = CommandType.StoredProcedure;
            if (roleId == -1)
            {
                objCom.CommandText = "GIOS.GIOS_SaveRoleDeatils";
            }
            else
            {
                objCom.CommandText = "GIOS.GIOS_SaveRoleDeatils";
            }

            if (roleId == -1)
            {
                objCom.Parameters.Add("@role_id", SqlDbType.Int).Value = DBNull.Value;
            }
            else
            {
                objCom.Parameters.Add("@role_id", SqlDbType.Int).Value = roleId;
            }

            objCom.Parameters.Add("@role_name", SqlDbType.Char, 500).Value = roleName;
            objCom.Parameters.Add("@role_description", SqlDbType.Char, 500).Value = description;
            objCom.Parameters.Add("@data", SqlDbType.Xml).Value = xml;
            objCom.Parameters.Add("@generatedRoleId", SqlDbType.Int);
            objCom.Parameters["@generatedRoleId"].Direction = ParameterDirection.Output;
            sqlAD = new SqlDataAdapter(objCom);
            objCon.Open();
            sqlAD.Fill(dsRole);
            if (dsRole != null && dsRole.Tables.Count > 0)
            {
                dtRole = dsRole.Tables[1];
                DataTable uniqueCols = dtRole.DefaultView.ToTable(true, "Role_ID", "Role_Name", "Role_Description");
                if (dtRole.Rows.Count > 0)
                {
                    foreach (DataRow dp in uniqueCols.Rows)
                    {

                        var compList = new List<Competency>();
                        foreach (DataRow row in dtRole.Rows)
                        {

                            if (int.Parse(dp["Role_ID"].ToString()) == int.Parse(row["Role_ID"].ToString()))
                            {
                                compObj.Id = row["Comp_Id"] == DBNull.Value ? -1 : int.Parse(row["Comp_Id"].ToString());

                                if (row["Comp_Id"] == DBNull.Value)
                                {
                                    compObj.Name = null;
                                }
                                else
                                {
                                    compObj.Name = Convert.ToString(row["Comp_Name"]);
                                }
                                compObj.LevelId = row["Lev_ID"] == DBNull.Value ? -1 : int.Parse(row["Lev_ID"].ToString());

                                compList.Add(compObj);
                            }

                        }

                        roleObj.Id = dp["Role_ID"] == DBNull.Value ? -1 : int.Parse(dp["Role_ID"].ToString());
                        if (dp["Role_Name"] == DBNull.Value)
                        {
                            roleObj.Name = null;
                        }
                        else
                        {
                            roleObj.Name = Convert.ToString(dp["Role_Name"]);
                        }
                        if (dp["Role_Description"] == DBNull.Value)
                        {
                            roleObj.Description = null;
                        }
                        else
                        {
                            roleObj.Description = Convert.ToString(dp["Role_Description"]);
                        }
                        //roleObj.Name = Convert.ToString(dp["Role_Name"]);
                        roleObj.CompetencyList = compList;
                        //roleList.Add(roleObj);

                    }

                }
            }
            roleObj.Id = Convert.ToInt32(objCom.Parameters["@generatedRoleId"].Value);
            objCon.Close();
            return roleObj;
        }
        public List<Level> getLevelNames()
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            SqlDataAdapter sqlAD;
            DataTable dtComp = null;
            DataSet dsComp = new DataSet();
            Level lev = null;
            List<Level> levelList = new List<Level>();
            try
            {
                ConnectionString(objCon);
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandText = "GIOS.GIOS_GetLevel";
                sqlAD = new SqlDataAdapter(objCom);
                objCon.Open();
                sqlAD.Fill(dsComp);
                objCon.Close();
                if (dsComp != null && dsComp.Tables.Count > 0)
                {
                    dtComp = dsComp.Tables[0];
                    if (dtComp.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtComp.Rows)
                        {
                            lev = new Level();
                            lev.Id = dr["Lev_ID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["Lev_ID"]);
                            lev.Name = Convert.ToString(dr["Lev_Name"]);
                            levelList.Add(lev);
                        }

                    }
                }
                return levelList;
            }
            catch (Exception ex)
            {
                objCon.Close();
                throw ex;
            }

        }
        private static void ConnectionString(SqlConnection objCon)
        {
            objCon.ConnectionString = "Data Source = NOATDC-D16SQC92\\VALCOMMON2016;Initial Catalog = GIOS; Persist Security Info=True;User ID=arconic_webservices;Password=Gioswebservice2018;";
        }
    }
}

